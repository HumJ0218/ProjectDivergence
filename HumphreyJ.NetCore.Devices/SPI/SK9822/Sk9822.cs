// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Spi;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Iot.Device.Sk9822
{
    /// <summary>
    /// Driver for SK9822. A double line transmission integrated control LED
    /// </summary>
    public class Sk9822 : IDisposable
    {
        /// <summary>
        /// The SPI device used for communication.
        /// </summary>
        public SpiDevice SpiDevice { get; }

        /// <summary>
        /// Number of LEDs
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Colors of LEDs
        /// </summary>
        public Span<Color> Pixels => _pixels;

        private Color[] _pixels;
        private byte[] _buffer;

        /// <summary>
        /// Initializes a new instance of the Sk9822 device.
        /// </summary>
        /// <param name="spi">The SPI device used for communication.</param>
        /// <param name="length">Number of LEDs</param>
        public Sk9822(SpiDevice spi, int length)
        {
            SpiDevice = spi ?? throw new ArgumentNullException(nameof(spi));
            Length = length;

            _pixels = new Color[Length];
            _buffer = new byte[(Length + 2) * 4];

            _buffer.AsSpan(0, 4).Fill(0x00);                //  start frame
            _buffer.AsSpan((Length + 1) * 4, 4).Fill(0xFF);  //  end frame
        }

        /// <summary>
        /// Update color data to LEDs
        /// </summary>
        public void Update()
        {
            for (var i = 0; i < Length; i++)
            {
                var pixel = _buffer.AsSpan((i + 1) * 4);
                pixel[0] = (byte)((_pixels[i].A >> 3) | 0b11100000);   //  global brighrness (alpha)
                pixel[1] = _pixels[i].B;   //  blue
                pixel[2] = _pixels[i].G;   //  green
                pixel[3] = _pixels[i].R;   //  red
            }

            SpiDevice.Write(_buffer);
        }

        public void Dispose()
        {
            SpiDevice.Dispose();

            _pixels = null;
            _buffer = null;
        }
    }
}
