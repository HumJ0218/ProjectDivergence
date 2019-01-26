using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.Text;

namespace HumphreyJ.NetCore.Devices.Util
{
    internal static class SpiDeviceFactory
    {
        internal static SpiDevice GetDevice(int busId, int chipSelectLine, SpiMode mode = 0, int dataBitLength = 8, int clockFrequency = 500000)
        {
            var settings = new SpiConnectionSettings(busId, chipSelectLine)
            {
                Mode = mode,
                DataBitLength = dataBitLength,
                ClockFrequency = clockFrequency,
            };

            try
            {
                return new System.Device.Spi.Drivers.UnixSpiDevice(settings);
            }
            catch
            {
                try
                {
                    return new System.Device.Spi.Drivers.Windows10SpiDevice(settings);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}