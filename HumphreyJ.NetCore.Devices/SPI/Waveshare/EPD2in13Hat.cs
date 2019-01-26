using HumphreyJ.NetCore.Devices.Util;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Spi;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace HumphreyJ.NetCore.Devices.SPI.Waveshare
{
    /// <summary>
    /// 微雪 2.13 吋墨水屏模块驱动
    /// </summary>
    public class EPD2in13Hat
    {
        /// <summary>
        /// 初始化墨水屏模块
        /// </summary>
        public EPD2in13Hat(int EPD_RST_PIN = 17, int EPD_DC_PIN = 25, int EPD_CS_PIN = 8, int EPD_BUSY_PIN = 24, bool isFullUpdate = true)
        {
            this.EPD_RST_PIN = EPD_RST_PIN;
            this.EPD_DC_PIN = EPD_DC_PIN;
            this.EPD_CS_PIN = EPD_CS_PIN;
            this.EPD_BUSY_PIN = EPD_BUSY_PIN;

            DEV_ModuleInit();
            this.IsFullUpdate = isFullUpdate;
        }

        /// <summary>
        /// 使用指定像素填充屏幕
        /// </summary>
        public void ClearScreen(byte pixel = 0xFF)
        {
            EPD_Clear(pixel);
        }

        /// <summary>
        /// 绘制整个屏幕
        /// </summary>
        public void FillScreen(byte[] image)
        {
            for (var i = 0; i < bufferLength; i++)
            {
                ImageBuffer[i] = image[i];
            }
            EPD_Display(ImageBuffer);
        }

        /// <summary>
        /// 绘制部分屏幕
        /// </summary>
        public void FillPartial(byte[] image, int toX, int toY, int imageWidth, int imageHeight)
        {
            for (var imageY = 0; imageY < imageHeight; imageY++)
            {
                var bufferY = toY + imageY;
                for (var imageX = 0; imageX < imageWidth; imageX++)
                {
                    var bufferX = toY + imageX;
                    var imageI = imageX + imageY * imageWidth;
                    var bufferI = bufferX + bufferY * bufferWidth;
                    ImageBuffer[bufferI] = image[imageI];
                }
            }
            EPD_Display(ImageBuffer);
        }

        /// <summary>
        /// 进入休眠模式
        /// </summary>
        public void Sleep()
        {
            EPD_Sleep();
        }

        /// <summary>
        /// 重置模块状态
        /// </summary>
        public void Reset()
        {
            EPD_Reset();
        }

        /// <summary>
        /// 获取或设置模块在重绘是是否需要全屏刷新
        /// </summary>
        public bool IsFullUpdate
        {
            get => IsFullUpdate; set
            {
                if (value == true && isFullUpdate != true)
                {
                    Console.WriteLine("lut_full_update");
                    EPD_Init(lut_full_update);
                }
                else if (value == false && isFullUpdate != false)
                {
                    Console.WriteLine("lut_partial_update");
                    EPD_Init(lut_partial_update);
                }
                isFullUpdate = value;
            }
        }

        /// <summary>
        /// 缓冲区宽度
        /// </summary>
        public const int bufferWidth = EPD_WIDTH_LOGICAL / 8;

        /// <summary>
        /// 缓冲区高度
        /// </summary>
        public const int bufferHeight = EPD_HEIGHT;

        /// <summary>
        /// 缓冲区长度
        /// </summary>
        public const int bufferLength = bufferWidth * bufferHeight;

        /// <summary>
        /// 获取缓冲区中的图像
        /// </summary>
        public byte[] ImageBuffer { get; private set; } = new byte[bufferLength];

        ////////////////////////////////////////////////////////////////

        private bool? isFullUpdate = null;

        private GpioController gpioController;
        private SpiDevice spiDevice;

        ////////////////////////////////////////////////////////////////

        private byte[] lut_full_update = { 0x22, 0x55, 0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1E, 0x1E, 0x1E, 0x1E, 0x1E, 0x1E, 0x1E, 0x1E, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] lut_partial_update = { 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        ////////////////////////////////////////////////////////////////

        private void DEV_ModuleInit()
        {
            DEV_SetupGpio();
            DEV_SetupSpi(0, 0, 32000000, 0);
        }

        private void DEV_SetupSpi(int busid, int chipSelectLine, int speed, int mode)
        {
            spiDevice = SpiDeviceFactory.GetDevice(busid, chipSelectLine, (SpiMode)mode, 8, speed);
        }

        private void DEV_SetupGpio()
        {
            gpioController = new GpioController(PinNumberingScheme.Logical, GpioDriverFactory.GetDriver());

            gpioController.OpenPin(EPD_RST_PIN, PinMode.Output);
            gpioController.OpenPin(EPD_DC_PIN, PinMode.Output);
            gpioController.OpenPin(EPD_CS_PIN, PinMode.Output);
            gpioController.OpenPin(EPD_BUSY_PIN, PinMode.Input);
        }

        ////////////////////////////////////////////////////////////////

        public const int EPD_WIDTH = 122;
        public const int EPD_WIDTH_LOGICAL = 128;
        public const int EPD_HEIGHT = 250;


        private int EPD_RST_PIN;
        private int EPD_DC_PIN;
        private int EPD_CS_PIN;
        private int EPD_BUSY_PIN;

        /// <summary>
        /// Initialize the e-Paper register
        /// </summary>
        /// <param name="update"></param>
        private void EPD_Init(byte[] update)
        {
            EPD_Reset();

            EPD_SendCommand(0x01); // DRIVER_OUTPUT_CONTROL
            EPD_SendData((EPD_HEIGHT - 1));
            EPD_SendData(((EPD_HEIGHT - 1) >> 8));
            EPD_SendData(0x00);         // GD = 0; SM = 0; TB = 0;

            EPD_SendCommand(0x0C);  // BOOSTER_SOFT_START_CONTROL
            EPD_SendData(0xD7);
            EPD_SendData(0xD6);
            EPD_SendData(0x9D);

            EPD_SendCommand(0x2C);  // WRITE_VCOM_REGISTER
            EPD_SendData(0xA8);     // VCOM 7C

            EPD_SendCommand(0x3A);  // SET_DUMMY_LINE_PERIOD
            EPD_SendData(0x1A);         // 4 dummy lines per gate

            EPD_SendCommand(0x3B);  // SET_GATE_TIME
            EPD_SendData(0x08);         // 2us per line

            EPD_SendCommand(0X3C);  // BORDER_WAVEFORM_CONTROL
            EPD_SendData(0x63);         // 2us per lin

            EPD_SendCommand(0X11);  // DATA_ENTRY_MODE_SETTING
            EPD_SendData(0x03);         // X increment; Y increment

            // WRITE_LUT_REGISTER
            EPD_SendCommand(0x32);
            foreach (var b in update)
            {
                EPD_SendData(b);
            }

        }

        /// <summary>
        /// Clear screen
        /// </summary>
        private void EPD_Clear(byte pixel = 0xFF)
        {
            int Width, Height;
            Width = (EPD_WIDTH % 8 == 0) ? (EPD_WIDTH / 8) : (EPD_WIDTH / 8 + 1);
            Height = EPD_HEIGHT;

            EPD_SetWindows(0, 0, EPD_WIDTH, EPD_HEIGHT);
            for (int j = 0; j < Height; j++)
            {
                EPD_SetCursor(0, j);
                EPD_SendCommand(0x24);
                for (int i = 0; i < Width; i++)
                {
                    EPD_SendData(pixel);
                }
            }
            EPD_TurnOnDisplay();
        }

        /// <summary>
        /// Sends the image buffer in RAM to e-Paper and displays
        /// </summary>
        /// <param name="Image"></param>
        private void EPD_Display(byte[] Image)
        {
            int Width, Height;
            Width = (EPD_WIDTH % 8 == 0) ? (EPD_WIDTH / 8) : (EPD_WIDTH / 8 + 1);
            Height = EPD_HEIGHT;

            EPD_SetWindows(0, 0, EPD_WIDTH, EPD_HEIGHT);
            for (int j = 0; j < Height; j++)
            {
                EPD_SetCursor(0, j);
                EPD_SendCommand(0x24);
                for (int i = 0; i < Width; i++)
                {
                    EPD_SendData(Image[i + j * Width]);
                }
            }
            EPD_TurnOnDisplay();
        }

        /// <summary>
        /// Enter sleep mode
        /// </summary>
        private void EPD_Sleep()
        {
            EPD_SendCommand(0x10); //DEEP_SLEEP_MODE
            EPD_SendData(0x01);
        }

        /// <summary>
        /// Software reset
        /// </summary>
        private void EPD_Reset()
        {
            gpioController.Write(EPD_RST_PIN, PinValue.High);
            Thread.Sleep(200);
            gpioController.Write(EPD_RST_PIN, 0);
            Thread.Sleep(200);
            gpioController.Write(EPD_RST_PIN, PinValue.High);
            Thread.Sleep(200);
        }

        /// <summary>
        /// send command
        /// </summary>
        /// <param name="Reg">Command register</param>
        private void EPD_SendCommand(byte Reg)
        {
            gpioController.Write(EPD_DC_PIN, 0);
            gpioController.Write(EPD_CS_PIN, 0);
            spiDevice.WriteByte(Reg);
            gpioController.Write(EPD_CS_PIN, PinValue.High);
        }

        /// <summary>
        /// send data
        /// </summary>
        /// <param name="Data">Write data</param>
        private void EPD_SendData(byte Data)
        {
            gpioController.Write(EPD_DC_PIN, PinValue.High);
            gpioController.Write(EPD_CS_PIN, 0);
            spiDevice.WriteByte(Data);
            gpioController.Write(EPD_CS_PIN, PinValue.High);
        }

        /// <summary>
        /// Wait until the busy_pin goes LOW
        /// </summary>
        private void EPD_WaitUntilIdle()
        {
            Debug.Print("e-Paper busy");
            while (gpioController.Read(EPD_BUSY_PIN) == PinValue.High)
            {      //LOW: idle, HIGH: busy
                Thread.Sleep(100);
            }
            Debug.Print("e-Paper busy release");
        }

        /// <summary>
        /// Turn On Display
        /// </summary>
        private void EPD_TurnOnDisplay()
        {
            EPD_SendCommand(0x22); // DISPLAY_UPDATE_CONTROL_2
            EPD_SendData(0xC4);
            EPD_SendCommand(0X20);  // MASTER_ACTIVATION
            EPD_SendCommand(0xFF);  // TERMINATE_FRAME_READ_WRITE

            EPD_WaitUntilIdle();
        }

        private void EPD_SetWindows(int x_start, int y_start, int x_end, int y_end)
        {
            EPD_SendCommand(0x44);
            /* x point must be the multiple of 8 or the last 3 bits will be ignored */
            EPD_SendData((byte)(x_start >> 3));
            EPD_SendData((byte)(x_end >> 3));
            EPD_SendCommand(0x45);
            EPD_SendData((byte)y_start);
            EPD_SendData((byte)(y_start >> 8));
            EPD_SendData((byte)y_end);
            EPD_SendData((byte)(y_end >> 8));
        }

        private void EPD_SetCursor(int x, int y)
        {
            EPD_SendCommand(0x4E);
            /* x point must be the multiple of 8 or the last 3 bits will be ignored */
            EPD_SendData((byte)(x >> 3));
            EPD_SendCommand(0x4F);
            EPD_SendData((byte)y);
            EPD_SendData((byte)(y >> 8));
            //    EPD_WaitUntilIdle();
        }

    }

}