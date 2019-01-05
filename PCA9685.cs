//  Author: HumJ0218 @ GitHub
//  From python code: https://github.com/adafruit/Adafruit_Python_PCA9685

using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace HumphreyJ.NetCore.Adafruit.PCA9685PwmDriver
{
    /// <summary>
    /// PCA9685 PWM LED/servo controller.
    /// </summary>
    public class PCA9685
    {

        // Registers/etc:
        const int PCA9685_ADDRESS = 0x40;
        const int MODE1 = 0x00;
        const int MODE2 = 0x01;
        const int SUBADR1 = 0x02;
        const int SUBADR2 = 0x03;
        const int SUBADR3 = 0x04;
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
        const int RESTART = 0x80;
        const int SLEEP = 0x10;
        const int ALLCALL = 0x01;
        const int INVRT = 0x10;
        const int OUTDRV = 0x04;

        // I2C Device
        private readonly I2cDevice _device;

        /// <summary>
        /// Initialize the PCA9685.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="busId"></param>
        public PCA9685(int address = PCA9685_ADDRESS, int busId = 0)
        {

            // Setup I2C interface for the device.
            var settings = new I2cConnectionSettings(busId, address);
            try
            {
                this._device = new System.Device.I2c.Drivers.UnixI2cDevice(settings);
            }
            catch
            {
                this._device = new System.Device.I2c.Drivers.Windows10I2cDevice(settings);
            }

            this.SetAllPwm(0, 0);
            this._device.Write(new byte[] { MODE2, OUTDRV });
            this._device.Write(new byte[] { MODE1, ALLCALL });
            Thread.Sleep(5); // wait for oscillator

            int mode1 = this._device.ReadByte();
            mode1 = mode1 & ~SLEEP; // wake up (reset sleep)
            this._device.Write(new byte[] { MODE1, (byte)mode1 });
            Thread.Sleep(5); // wait for oscillator

        }

        /// <summary>
        /// Set the PWM frequency to the provided value in hertz.
        /// </summary>
        /// <param name="freq_hz"></param>
        public void SetPwmFrequency(int freq_hz)
        {
            var prescaleval = 25000000.0; // 25MHz
            prescaleval /= 4096.0; // 12-bit
            prescaleval /= freq_hz;
            prescaleval -= 1.0;
            Debug.Print($"Setting PWM frequency to {freq_hz} Hz");
            Debug.Print($"Estimated pre-scale: {prescaleval}");

            var prescale = (int)(Math.Floor(prescaleval + 0.5));
            Debug.Print($"Final pre-scale: {prescale}");

            var oldmode = this._device.ReadByte();
            var newmode = (oldmode & 0x7F) | 0x10; // sleep
            this._device.Write(new byte[] { MODE1, (byte)newmode }); // go to sleep
            this._device.Write(new byte[] { PRESCALE, (byte)prescale });
            this._device.Write(new byte[] { MODE1, oldmode });
            Thread.Sleep(5);

            this._device.Write(new byte[] { MODE1, (byte)(oldmode | 0x80) });
        }

        /// <summary>
        /// Sets a single PWM channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public void SetPwm(int channel, int on, int off)
        {
            this._device.Write(new byte[] { (byte)(LED0_ON_L + 4 * channel), (byte)(on & 0xFF) });
            this._device.Write(new byte[] { (byte)(LED0_ON_H + 4 * channel), (byte)(on >> 8) });
            this._device.Write(new byte[] { (byte)(LED0_OFF_L + 4 * channel), (byte)(off & 0xFF) });
            this._device.Write(new byte[] { (byte)(LED0_OFF_H + 4 * channel), (byte)(off >> 8) });
        }

        /// <summary>
        /// Sets all PWM channels.
        /// </summary>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public void SetAllPwm(int on, int off)
        {
            this._device.Write(new byte[] { (byte)ALL_LED_ON_L, (byte)(on & 0xFF) });
            this._device.Write(new byte[] { (byte)ALL_LED_ON_H, (byte)(on >> 8) });
            this._device.Write(new byte[] { (byte)ALL_LED_OFF_L, (byte)(off & 0xFF) });
            this._device.Write(new byte[] { (byte)ALL_LED_OFF_H, (byte)(off >> 8) });
        }

    }
}
