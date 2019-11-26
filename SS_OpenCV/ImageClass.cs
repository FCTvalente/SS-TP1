using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Diagnostics;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            dataPtr[0] = (byte)(255 - dataPtr[0]);
                            dataPtr[1] = (byte)(255 - dataPtr[1]);
                            dataPtr[2] = (byte)(255 - dataPtr[2]);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


        /// <summary>
        /// Convert to 1 component
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToOneComponent(Image<Bgr, byte> img, String color)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            //gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            switch (color)
                            {
                                case "blue":
                                    dataPtr[0] = blue;
                                    dataPtr[1] = blue;
                                    dataPtr[2] = blue;
                                    break;
                                case "green":
                                    dataPtr[0] = green;
                                    dataPtr[1] = green;
                                    dataPtr[2] = green;
                                    break;
                                case "red":
                                    dataPtr[0] = red;
                                    dataPtr[1] = red;
                                    dataPtr[2] = red;
                                    break;
                            }

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


        /// <summary>
        /// Image translation
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void Translate(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, double xoffset, double yoffset)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                int width = img.Width;
                int height = img.Height;

                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                byte* offsetPtr = dataPtr;
                int x, y, rx, ry;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ry = (int)Math.Round(y + yoffset);
                        if (ry >= 0 && ry < height)
                        {
                            for (x = 0; x < width; x++)
                            {
                                rx = (int)Math.Round(x - xoffset);
                                if (rx >= 0 && rx < width)
                                {
                                    offsetPtr = startPtr + ry * padding + (ry * width + rx) * nChan;

                                    // store in the image
                                    dataPtr[0] = offsetPtr[0];
                                    dataPtr[1] = offsetPtr[1];
                                    dataPtr[2] = offsetPtr[2];

                                }
                                // advance the pointer to the next pixel
                                dataPtr += nChan;
                            }
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Rotate(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, double angle)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                int width = img.Width;
                int height = img.Height;

                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                byte* offsetPtr = dataPtr;
                int x, y, rx, ry;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ry = (int)Math.Round(y + 0d);
                        if (ry >= 0 && ry < height)
                        {
                            for (x = 0; x < width; x++)
                            {
                                rx = (int)Math.Round(x - 0d);
                                if (rx >= 0 && rx < width)
                                {
                                    offsetPtr = startPtr + ry * padding + (ry * width + rx) * nChan;

                                    // store in the image
                                    dataPtr[0] = offsetPtr[0];
                                    dataPtr[1] = offsetPtr[1];
                                    dataPtr[2] = offsetPtr[2];

                                }
                                // advance the pointer to the next pixel
                                dataPtr += nChan;
                            }
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                int range = 3;
                int area = range * range;
                int step = (range - 1) / 2;
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                int width = img.Width;
                int height = img.Height;

                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                byte* offsetPtr = dataPtr;
                int x, y, sumr, sumg, sumb, dx, dy, rx, ry;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            sumb = 0;
                            sumg = 0;
                            sumr = 0;
                            for (dx = -step; dx < step + 1; dx++)
                            {
                                rx = x + dx;
                                if (rx < 0)
                                {
                                    rx = 0;
                                }
                                else if (rx >= width)
                                {
                                    rx = width - 1;
                                }
                                for (dy = -step; dy < step + 1; dy++)
                                {
                                    ry = y + dy;
                                    if (ry < 0)
                                    {
                                        ry = 0;
                                    }
                                    else if (ry >= height)
                                    {
                                        ry = height - 1;
                                    }
                                    offsetPtr = startPtr + ry * padding + (ry * width + rx) * nChan;
                                    sumb += offsetPtr[0];
                                    sumg += offsetPtr[1];
                                    sumr += offsetPtr[2];
                                }
                            }

                            // store in the image
                            dataPtr[0] = (byte)(sumb / area);
                            dataPtr[1] = (byte)(sumg / area);
                            dataPtr[2] = (byte)(sumr / area);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[][] matrix, float matrixWeight)
        {
            unsafe
            {
                int range = 3;
                int area = range * range;
                int step = (range - 1) / 2;
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                int width = img.Width;
                int height = img.Height;

                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                byte* offsetPtr = dataPtr;
                int x, y, sumr, sumg, sumb, dx, dy, rx, ry;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            sumb = 0;
                            sumg = 0;
                            sumr = 0;
                            for (dx = -step; dx < step + 1; dx++)
                            {
                                rx = x + dx;
                                if (rx < 0)
                                {
                                    rx = 0;
                                }
                                else if (rx >= width)
                                {
                                    rx = width - 1;
                                }
                                for (dy = -step; dy < step + 1; dy++)
                                {
                                    ry = y + dy;
                                    if (ry < 0)
                                    {
                                        ry = 0;
                                    }
                                    else if (ry >= height)
                                    {
                                        ry = height - 1;
                                    }
                                    offsetPtr = startPtr + ry * padding + (ry * width + rx) * nChan;
                                    sumb += (int)(offsetPtr[0] * matrix[dx + 1][dy + 1]);
                                    sumg += (int)(offsetPtr[1] * matrix[dx + 1][dy + 1]);
                                    sumr += (int)(offsetPtr[2] * matrix[dx + 1][dy + 1]);
                                }
                            }

                            sumb = sumb >= 0 ? sumb : 0;
                            sumg = sumg >= 0 ? sumg : 0;
                            sumr = sumr >= 0 ? sumr : 0;

                            // store in the image
                            dataPtr[0] = (byte)(sumb / matrixWeight);
                            dataPtr[1] = (byte)(sumg / matrixWeight);
                            dataPtr[2] = (byte)(sumr / matrixWeight);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                int range = 3;
                int area = range * range;
                int step = (range - 1) / 2;
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                int width = img.Width;
                int height = img.Height;

                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                int x, y, sxr, sxg, sxb, syr, syg, syb, dx, dy, rx, ry, counter;
                byte*[] pixels;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            sxb = 0;
                            sxg = 0;
                            sxr = 0;

                            syb = 0;
                            syg = 0;
                            syr = 0;

                            counter = 0;
                            pixels = new byte*[9];
                            for (dx = -step; dx < step + 1; dx++)
                            {
                                rx = x + dx;
                                if (rx < 0)
                                {
                                    rx = 0;
                                }
                                else if (rx >= width)
                                {
                                    rx = width - 1;
                                }
                                for (dy = -step; dy < step + 1; dy++)
                                {
                                    ry = y + dy;
                                    if (ry < 0)
                                    {
                                        ry = 0;
                                    }
                                    else if (ry >= height)
                                    {
                                        ry = height - 1;
                                    }
                                    pixels[counter++] = startPtr + ry * padding + (ry * width + rx) * nChan;
                                }
                            }

                            sxb = (int)(pixels[0][0] + 2 * pixels[1][0] + pixels[2][0] - pixels[6][0] - 2 * pixels[7][0] - pixels[8][0]);
                            sxg = (int)(pixels[0][1] + 2 * pixels[1][1] + pixels[2][1] - pixels[6][1] - 2 * pixels[7][1] - pixels[8][1]);
                            sxr = (int)(pixels[0][2] + 2 * pixels[1][2] + pixels[2][2] - pixels[6][2] - 2 * pixels[7][2] - pixels[8][2]);

                            syb = (int)(pixels[0][0] + 2 * pixels[3][0] + pixels[6][0] - pixels[2][0] - 2 * pixels[5][0] - pixels[8][0]);
                            syg = (int)(pixels[0][1] + 2 * pixels[3][1] + pixels[6][1] - pixels[2][1] - 2 * pixels[5][1] - pixels[8][1]);
                            syr = (int)(pixels[0][2] + 2 * pixels[3][2] + pixels[6][2] - pixels[2][2] - 2 * pixels[5][2] - pixels[8][2]);

                            // store in the image
                            dataPtr[0] = (byte)Math.Sqrt(sxb * sxb + syb * syb);
                            dataPtr[1] = (byte)Math.Sqrt(sxg * sxg + syg * syg);
                            dataPtr[2] = (byte)Math.Sqrt(sxr * sxr + syr * syr);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                int range = 3;
                int area = range * range;
                int step = (range - 1) / 2;
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                int width = img.Width;
                int height = img.Height;

                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                byte* a, b, c;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height - 1; y++)
                    {
                        for (x = 0; x < width - 1; x++)
                        {
                            a = startPtr + y * padding + (y * width + x) * nChan;
                            b = startPtr + y * padding + (y * width + x + 1) * nChan;
                            c = startPtr + (y + 1) * padding + ((y + 1) * width + x) * nChan;

                            // store in the image
                            dataPtr[0] = (byte)Math.Sqrt((a[0] - b[0]) * (a[0] - b[0]) + (a[0] - c[0]) * (a[0] - c[0]));
                            dataPtr[1] = (byte)Math.Sqrt((a[1] - b[1]) * (a[1] - b[1]) + (a[1] - c[1]) * (a[1] - c[1]));
                            dataPtr[2] = (byte)Math.Sqrt((a[2] - b[2]) * (a[2] - b[2]) + (a[2] - c[2]) * (a[2] - c[2]));

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        dataPtr += nChan;
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            imgCopy.SmoothMedian(3).CopyTo(img);
        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[] res = new int[256];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, red, green, blue, gray;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        gray = (blue + green + red) / 3;
                        res[gray]++;

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                return res;

            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[,] res = new int[256, 3];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, red, green, blue;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        res[red, 0]++;
                        res[green, 1]++;
                        res[blue, 2]++;

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                return res;
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                int[,] res = new int[256, 4];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, red, green, blue, gray;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        res[red, 0]++;
                        res[green, 1]++;
                        res[blue, 2]++;

                        gray = (blue + green + red) / 3;
                        res[gray, 3]++;

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }

                return res;
            }
        }

        public static void Binarization(Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];
                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);
                            gray = gray < threshold ? (byte)0 : (byte)255;
                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            /*
                            blue = blue < threshold ? (byte)0 : (byte)255;
                            green = green < threshold ? (byte)0 : (byte)255;
                            red = red < threshold ? (byte)0 : (byte)255;

                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;
                            */

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RGBtoHSVPrime(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                
                double[] prevHSV;
                double[] prevRGB = new double[3];

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            prevHSV = RGBtoHSV((int)red, (int)blue, (int)green);
                            
                            prevRGB = HSVtoRGB(prevHSV[0], prevHSV[1], prevHSV[2]);

                            blue = (byte)(int)prevRGB[2];
                            green = (byte)(int)prevRGB[1];
                            red = (byte)(int)prevRGB[0];

                            

                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }

            }

        }


        public static double[] RGBtoHSV(double red, double blue, double green)
        {
            double[] HSV = new double[3];

            double newRed = red / 255;
            double newBlue = blue / 255;
            double newGreen = green / 255;

            double min = Math.Min(Math.Min(newRed, newGreen), newBlue);
            double max = Math.Max(Math.Max(newRed, newGreen), newBlue);

            double delta = max - min;

            double saturation;

            if(max == 0)
            {
                saturation = 0;
            } else
            {
                saturation = delta / max;
            }

            double value = max;

            double hue;

            if(delta == 0)
            {
                hue = 0;
            } else if(max == newRed)
            {
                hue = 60 * (0 + (newGreen - newBlue) / delta);
                hue = hue < 0 ? hue + 360 : hue;
                double dif = newRed + newRed - newGreen - newBlue;
                saturation = saturation + dif > 1 ? 1 : saturation + dif;
                value = value + dif > 1 ? 1 : value + dif;
            } else if(max == newGreen)
            {
                hue = 60 * (2 + (newBlue - newRed) / delta);
            } else
            {
                hue = 60 * (4 + (newRed - newGreen) / delta);
                hue = hue > 360 ? hue - 360 : hue;
            }

            HSV[0] = hue;
            HSV[1] = saturation;
            HSV[2] = value;

            return HSV;
        }

        public static double[] HSVtoRGB(double hue, double saturation, double value)
        {
            unsafe
            {
                double c = 0f, hueNewValue = 0f, x = 0f, m = 0f;
                double[] prevRGB = new double[3];

                c = value * saturation;
                hueNewValue = hue / 60;
                x = c * (1 - Math.Abs(hueNewValue % 2 - 1));

                if (0 <= hue && hue <= 60)
                {
                    prevRGB[0] = c; prevRGB[1] = x; prevRGB[2] = 0;
                }
                else if (60 < hue && hue<= 120)
                {
                    prevRGB[0] = x; prevRGB[1] = c; prevRGB[2] = 0;
                }
                else if (120 < hue && hue <= 180)
                {
                    prevRGB[0] = 0; prevRGB[1] = c; prevRGB[2] = x;
                }
                else if (180 < hue && hue <= 240)
                {
                    prevRGB[0] = 0; prevRGB[1] = x; prevRGB[2] = c;
                }
                else if (240 < hue && hue <= 300)
                {
                    prevRGB[0] = x; prevRGB[1] = 0; prevRGB[2] = c;
                }
                else if (300 < hue && hue <= 360)
                {
                    prevRGB[0] = c; prevRGB[1] = 0; prevRGB[2] = x;
                }

                m = value - c;

                prevRGB[0] = (prevRGB[0] + m)*255;
                prevRGB[1] = (prevRGB[1] + m)*255;
                prevRGB[2] = (prevRGB[2] + m)*255;

                return prevRGB;
            }

        }

    }
}
