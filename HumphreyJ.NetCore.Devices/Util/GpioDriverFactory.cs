using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Text;

namespace HumphreyJ.NetCore.Devices.Util
{
    internal static class GpioDriverFactory
    {
        internal static GpioDriver GetDriver()
        {

            try
            {
                return new RaspberryPi3Driver();
            }
            catch
            {
                try
                {
                    return new HummingBoardDriver();
                }
                catch
                {
                    try
                    {
                        return new UnixDriver();
                    }
                    catch
                    {
                        try
                        {
                            return new Windows10Driver();
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

        }
    }
}
