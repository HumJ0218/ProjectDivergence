using HumphreyJ.NetCore.Devices.SPI.Waveshare;
using System;
using System.IO;

namespace EPD2in13Hattest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("refreash seq ( eg. '010101...' or 'f' for full update) ? ");
            var a = Console.ReadLine().ToLower();           

            var epd = new EPD2in13Hat(17, 25, 8, 24, a=="f");

            while (true)
            {
                for (var i = 0; i < 10; i++)
                {
                    if (a != "f")
                    foreach (var c in a) {
                        if (c == '0')
                        {
                            epd.ClearScreen(0x00);
                        }
                        if (c == '1')
                        {
                            epd.ClearScreen(0xFF);
                        }
                    }
                    epd.FillScreen($"./img/{i}.bmp");
                }
            }

        }

    }

    public static class EPD2in13HatExtend
    {
        public static void FillScreen(this EPD2in13Hat epd, string filename)
        {
            epd.FillScreen(LoadMonochromeBitmap(filename));
        }

        public static byte[] LoadMonochromeBitmap(string filename)
        {

            byte[] image;

            using (var stream = File.OpenRead(filename))
            using (var reader = new BinaryReader(stream))
            {

                var bfType = string.Join("", reader.ReadChars(2));    // "BM"
                var bfSize = reader.ReadInt32();
                var bfReserved1 = reader.ReadInt16();
                var bfReserved2 = reader.ReadInt16();
                var bfOffBits = reader.ReadInt32();

                var biSize = reader.ReadInt32();
                var biWidth = reader.ReadInt32();
                var biHeight = reader.ReadInt32();
                var biPlanes = reader.ReadInt16();
                var biBitCount = reader.ReadInt16();
                var biCompression = reader.ReadInt32();
                var biSizeImage = reader.ReadInt32();
                //var biXPelsPerMeter = reader.ReadInt32();
                //var biYPelsPerMeter = reader.ReadInt32();
                //var biClrUsed = reader.ReadInt32();
                //var biClrImportant = reader.ReadInt32();

                stream.Seek(bfOffBits, SeekOrigin.Begin);

                image = new byte[biSizeImage];

                var byteWidth = (biWidth + 7) / 8;
                var height = biSizeImage / byteWidth;

                for (var iy = 0; iy < height; iy++)
                {
                    var y = height - 1 - iy;
                    stream.Read(image, y * byteWidth, byteWidth);
                }
            }

            return image;
        }

    }
}