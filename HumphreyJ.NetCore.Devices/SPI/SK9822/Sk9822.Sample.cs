// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Device.Gpio;
using System.Device.Spi;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Iot.Device.Sk9822.Samples
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var spiDevice = SpiDevice.Create(new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 30_000_000,    //  up to 30 MHz
                DataBitLength = 8,
                DataFlow = DataFlow.MsbFirst,
                Mode = SpiMode.Mode0    //  just ensure data is ready at clock rising edge
            });

            var sk9822 = new Sk9822(spiDevice, 16);

            var random = new Random();
            for (var i = 0; i < sk9822.Length; i++) {
                sk9822.Pixels[i] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));
            }

            while (true) {
                Flow(sk9822.Pixels);
                sk9822.Update();
                Thread.Sleep(10);
            }
        }

        private static void Flow(Span<Color> pixels)
        {
            for (var i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.FromArgb(pixels[i].A, pixels[i].R, pixels[i].G, pixels[i].B);
            }
        }
    }
}
