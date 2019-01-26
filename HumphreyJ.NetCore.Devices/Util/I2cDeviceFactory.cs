using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.I2c.Drivers;
using System.Text;

namespace HumphreyJ.NetCore.Devices.Util
{
    internal static class I2cDeviceFactory
    {
        internal static I2cDevice GetDevice(int busId, int deviceAddress)
        {
            var settings = new I2cConnectionSettings(busId, deviceAddress);

            try
            {
                return new UnixI2cDevice(settings);
            }
            catch
            {
                try
                {
                    return new Windows10I2cDevice(settings);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}