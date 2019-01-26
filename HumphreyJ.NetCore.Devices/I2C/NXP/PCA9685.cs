using HumphreyJ.NetCore.Devices.Util;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.I2c.Drivers;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace HumphreyJ.NetCore.Devices.I2C.NXP
{
    /// <summary>
    /// NXP PCA9685 芯片驱动
    /// </summary>
    public class PCA9685
    {
        // Registers/etc:
        const int PCA9685_ADDRESS = 0x40;
        const int MODE1 = 0x00;
        const int MODE2 = 0x01;
        //const int SUBADR1 = 0x02;
        //const int SUBADR2 = 0x03;
        //const int SUBADR3 = 0x04;
        const int PRESCALE = 0xFE;
        const int LED0_ON_L = 0x06;
        const int LED0_ON_H = 0x07;
        const int LED0_OFF_L = 0x08;
        const int LED0_OFF_H = 0x09;
        const int ALL_LED_ON_L = 0xFA;
        const int ALL_LED_ON_H = 0xFB;
        const int ALL_LED_OFF_L = 0xFC;
        const int ALL_LED_OFF_H = 0xFD;

        // Bits:
        //const int RESTART = 0x80;
        const int SLEEP = 0x10;
        const int ALLCALL = 0x01;
        //const int INVRT = 0x10;
        const int OUTDRV = 0x04;

        // I2C Device
        private readonly I2cDevice device;

        public int Address { get; private set; }
        public int BusId { get; private set; }
        public double ClockRate { get; set; } = 25000000;

        /// <summary>
        /// 初始化 PCA9685
        /// </summary>
        public PCA9685(int address = PCA9685_ADDRESS, int busId = 1)
        {
            Address = address;
            BusId = busId;

            // Setup I2C interface for the device.
            device = I2cDeviceFactory.GetDevice(BusId, Address);

            SetPwm(0, 0);
            device.Write(new byte[] { MODE2, OUTDRV });
            device.Write(new byte[] { MODE1, ALLCALL });
            Thread.Sleep(5); // wait for oscillator

            int mode1 = device.ReadByte();
            mode1 = mode1 & ~SLEEP; // wake up (reset sleep)
            device.Write(new byte[] { MODE1, (byte)mode1 });
            Thread.Sleep(5); // wait for oscillator

        }

        /// <summary>
        /// 设置 PWM 频率
        /// </summary>
        public void SetPwmFrequency(double freq_hz)
        {
            SetPwmFrequency(GetPrescale(freq_hz));
        }

        /// <summary>
        /// 获取 PWM 频率对应的 prescale
        /// </summary>
        public byte GetPrescale(double freq_hz)
        {
            //var prescaleval = 25000000.0; // 25MHz
            //prescaleval /= 4096.0; // 12-bit
            //prescaleval /= freq_hz;
            //prescaleval -= 1.0;

            var prescaleval = ClockRate / 4096 / freq_hz - 1;
            //Debug.Print($"Setting PWM frequency to {freq_hz} Hz");
            //Debug.Print($"Estimated pre-scale: {prescaleval}");

            var prescale = (byte)Math.Round(prescaleval);
            //Debug.Print($"Final pre-scale: {prescale}");

            return prescale;
        }

        /// <summary>
        /// 获取 prescale 对应的频率
        /// </summary>
        public double GetFreq(byte prescale)
        {
            return ClockRate / 4096 / (prescale + 1);
        }

        /// <summary>
        /// 使用 prescale 来设置 PWM 频率
        /// </summary>
        public void SetPwmFrequency(byte prescale)
        {
            var oldmode = device.ReadByte();
            var newmode = (oldmode & 0x7F) | 0x10; // sleep
            device.Write(new byte[] { MODE1, (byte)newmode }); // go to sleep
            device.Write(new byte[] { PRESCALE, prescale });
            device.Write(new byte[] { MODE1, oldmode });
            Thread.Sleep(5);

            device.Write(new byte[] { MODE1, (byte)(oldmode | 0x80) });
        }

        /// <summary>
        /// 设置单个通道的 PWM 参数
        /// </summary>
        public void SetPwm(int on, int off, int channel)
        {
            device.Write(new byte[] { (byte)(LED0_ON_L + 4 * channel), (byte)(on & 0xFF) });
            device.Write(new byte[] { (byte)(LED0_ON_H + 4 * channel), (byte)(on >> 8) });
            device.Write(new byte[] { (byte)(LED0_OFF_L + 4 * channel), (byte)(off & 0xFF) });
            device.Write(new byte[] { (byte)(LED0_OFF_H + 4 * channel), (byte)(off >> 8) });
        }

        /// <summary>
        /// 设置所有通道的 PWM 参数
        /// </summary>
        public void SetPwm(int on, int off)
        {
            device.Write(new byte[] { (byte)ALL_LED_ON_L, (byte)(on & 0xFF) });
            device.Write(new byte[] { (byte)ALL_LED_ON_H, (byte)(on >> 8) });
            device.Write(new byte[] { (byte)ALL_LED_OFF_L, (byte)(off & 0xFF) });
            device.Write(new byte[] { (byte)ALL_LED_OFF_H, (byte)(off >> 8) });
        }
    }
}