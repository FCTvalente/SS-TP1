using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Diagnostics;

using Emgu.CV.UI;

namespace SS_OpenCV
{
    class ImageClass
    {

        static readonly List<bool[,]> digits = Digits();

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

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
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
                float conv = (float)contrast;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            dataPtr[0] = (byte)(contrast * dataPtr[0] + bright);
                            dataPtr[1] = (byte)(contrast * dataPtr[1] + bright);
                            dataPtr[2] = (byte)(contrast * dataPtr[2] + bright);

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

        public static void RedChannel(Image<Bgr, byte> img)
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
                            dataPtr[0] = dataPtr[2];
                            dataPtr[1] = dataPtr[2];
                            dataPtr[2] = dataPtr[2];

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
        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
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
                        ry = y - dy;
                        if (ry >= 0 && ry < height)
                        {
                            for (x = 0; x < width; x++)
                            {
                                rx = x - dx;
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
                        } else
                        {
                            dataPtr += nChan * width;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
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
                int x, y, nx, ny;
                double rx, ry;
                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);
                double hwidth = width * .5d;
                double hheight = height * .5d;
                double ycos, ysin;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ry = hheight - y;
                        ycos = ry * cos;
                        ysin = ry * sin;
                        for (x = 0; x < width; x++)
                        {
                            rx = x - hwidth;

                            nx = (int)Math.Round(rx * cos - ysin + hwidth);
                            ny = (int)Math.Round(hheight - rx * sin - ycos);

                            if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                            {
                                offsetPtr = startPtr + ny * padding + (ny * width + nx) * nChan;

                                dataPtr[0] = offsetPtr[0];
                                dataPtr[1] = offsetPtr[1];
                                dataPtr[2] = offsetPtr[2];
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

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
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
                int x, y, nx, ny;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ny = (int)Math.Round(y / scaleFactor);
                        if (ny >= 0 && ny < height)
                        {
                            for (x = 0; x < width; x++)
                            {
                                nx = (int)Math.Round(x / scaleFactor);
                                if (nx >= 0 && nx < width)
                                {
                                    offsetPtr = startPtr + ny * padding + (ny * width + nx) * nChan;

                                    // store in the image
                                    dataPtr[0] = offsetPtr[0];
                                    dataPtr[1] = offsetPtr[1];
                                    dataPtr[2] = offsetPtr[2];

                                }
                                // advance the pointer to the next pixel
                                dataPtr += nChan;
                            }
                        }
                        else
                        {
                            dataPtr += nChan * width;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
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
                int x, y, nx, ny;
                double hwidth = width * .5d;
                double hheight = height * .5d;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        ny = (int)Math.Round((y - hheight) / scaleFactor + centerY);
                        if (ny >= 0 && ny < height)
                        {
                            for (x = 0; x < width; x++)
                            {
                                nx = (int)Math.Round((x - hwidth) / scaleFactor + centerX);
                                if (nx >= 0 && nx < width)
                                {
                                    offsetPtr = startPtr + ny * padding + (ny * width + nx) * nChan;

                                    // store in the image
                                    dataPtr[0] = offsetPtr[0];
                                    dataPtr[1] = offsetPtr[1];
                                    dataPtr[2] = offsetPtr[2];

                                }
                                // advance the pointer to the next pixel
                                dataPtr += nChan;
                            }
                        }
                        else
                        {
                            dataPtr += nChan * width;
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
        
        
        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
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
                                    sumb += (int)(offsetPtr[0] * matrix[dx + 1,dy + 1]);
                                    sumg += (int)(offsetPtr[1] * matrix[dx + 1,dy + 1]);
                                    sumr += (int)(offsetPtr[2] * matrix[dx + 1,dy + 1]);
                                }
                            }

                            sumb = sumb / matrixWeight < 0 ? 0 : (int)(sumb / matrixWeight);
                            sumb = sumb > 255 ? 255 : sumb;
                            sumg = sumg / matrixWeight < 0 ? 0 : (int)(sumg / matrixWeight);
                            sumg = sumg > 255 ? 255 : sumg;
                            sumr = sumr / matrixWeight < 0 ? 0 : (int)(sumr / matrixWeight);
                            sumr = sumr > 255 ? 255 : sumr;

                            // store in the image
                            dataPtr[0] = (byte)sumb;
                            dataPtr[1] = (byte)sumg;
                            dataPtr[2] = (byte)sumr;

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

                            sxb = (pixels[0][0] + 2 * pixels[1][0] + pixels[2][0]) - (pixels[6][0] + 2 * pixels[7][0] + pixels[8][0]);
                            sxg = (pixels[0][1] + 2 * pixels[1][1] + pixels[2][1]) - (pixels[6][1] + 2 * pixels[7][1] + pixels[8][1]);
                            sxr = (pixels[0][2] + 2 * pixels[1][2] + pixels[2][2]) - (pixels[6][2] + 2 * pixels[7][2] + pixels[8][2]);

                            syb = (pixels[0][0] + 2 * pixels[3][0] + pixels[6][0]) - (pixels[2][0] + 2 * pixels[5][0] + pixels[8][0]);
                            syg = (pixels[0][1] + 2 * pixels[3][1] + pixels[6][1]) - (pixels[2][1] + 2 * pixels[5][1] + pixels[8][1]);
                            syr = (pixels[0][2] + 2 * pixels[3][2] + pixels[6][2]) - (pixels[2][2] + 2 * pixels[5][2] + pixels[8][2]);

                            // store in the image
                            dataPtr[0] = Math.Abs(sxb) + Math.Abs(syb) > 255 ? (byte)255 : (byte)(Math.Abs(sxb) + Math.Abs(syb));
                            dataPtr[1] = Math.Abs(sxg) + Math.Abs(syg) > 255 ? (byte)255 : (byte)(Math.Abs(sxg) + Math.Abs(syg));
                            dataPtr[2] = Math.Abs(sxr) + Math.Abs(syr) > 255 ? (byte)255 : (byte)(Math.Abs(sxr) + Math.Abs(syr));

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
                            dataPtr[0] = Math.Abs(a[0] - b[0]) + Math.Abs(a[0] - c[0]) > 255 ? (byte)255 : (byte)(Math.Abs(a[0] - b[0]) + Math.Abs(a[0] - c[0]));
                            dataPtr[1] = Math.Abs(a[1] - b[1]) + Math.Abs(a[1] - c[1]) > 255 ? (byte)255 : (byte)(Math.Abs(a[1] - b[1]) + Math.Abs(a[1] - c[1]));
                            dataPtr[2] = Math.Abs(a[2] - b[2]) + Math.Abs(a[2] - c[2]) > 255 ? (byte)255 : (byte)(Math.Abs(a[2] - b[2]) + Math.Abs(a[2] - c[2]));

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
                int x, y, dx, dy, rx, ry, i, j, d, min;
                int[] arrayb, arrayg, arrayr, dists;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            i = 0;
                            arrayb = new int[9];
                            arrayg = new int[9];
                            arrayr = new int[9];
                            dists = new int[9];
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
                                    arrayb[i] = offsetPtr[0];
                                    arrayg[i] = offsetPtr[1];
                                    arrayr[i] = offsetPtr[2];
                                    i++;
                                }
                            }

                            for(i = 0; i < 8; i++)
                            {
                                for(j = i + 1; j < 9; j++)
                                {
                                    d = Math.Abs(arrayb[i] - arrayb[j]) + Math.Abs(arrayg[i] - arrayg[j]) + Math.Abs(arrayr[i] - arrayr[j]);
                                    dists[i] += d;
                                    dists[j] += d;
                                }
                            }

                            i = 0;
                            min = 255 * 9;
                            for(j = 0; j < 9; j++)
                            {
                                if(dists[j] < min)
                                {
                                    i = j;
                                    min = dists[j];
                                }
                            }

                            // store in the image
                            dataPtr[0] = (byte)arrayb[i];
                            dataPtr[1] = (byte)arrayg[i];
                            dataPtr[2] = (byte)arrayr[i];

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
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

                        gray = (int)Math.Round((blue + green + red) / 3d);
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

        public static float[] Histogram_Gray_Prob(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                float[] res = new float[256];

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                float step = 1f / (width * height);
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

                        gray = (int)Math.Round((blue + green + red) / 3d);
                        res[gray] += step;

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

        public static void ConvertToBW(Image<Bgr, byte> img, int threshold)
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
                            gray = gray <= threshold ? (byte)0 : (byte)255;
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

        public static void ConvertToBW_Otsu(Image<Bgr, byte> img)
        {
            float[] hist = Histogram_Gray_Prob(img);
            float max = 0f;
            int res = 0;
            float q1 = hist[0];
            float q2 = 1f - q1;
            float ul1 = hist[0];
            float ul2 = 0f;
            float t;
            for(int i = 1; i < hist.Length; i++)
            {
                ul2 += (i + 1) * hist[i];
            }
            if(ul1 > 0)
            {
                max = q1 * q2 * (ul1 / q1 - ul2 / q2) * (ul1 / q1 - ul2 / q2);
            }
            for (int i = 1; i < hist.Length; i++)
            {
                q1 += hist[i];
                if(q1 == 0f)
                {
                    continue;
                }
                q2 -= hist[i];
                if(q2 == 0f)
                {
                    break;
                }
                ul1 += (i + 1) * hist[i];
                ul2 -= (i + 1) * hist[i];
                t = q1 * q2 * (ul1 / q1 - ul2 / q2) * (ul1 / q1 - ul2 / q2);
                if(t > max)
                {
                    max = t;
                    res = i;
                }
            }
            ConvertToBW(img, res);
        }

        public static void BradleyRoth(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                int width = img.Width;
                int height = img.Height;
                img.Bitmap = new System.Drawing.Bitmap(width, height);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m1.imageData.ToPointer(); // Pointer to the image
                byte* startPtr = (byte*)m2.imageData.ToPointer(); // Pointer to the image
                int range = width / 8;
                range = range + range % 2 + 1;
                range = 3;
                int area = range * range;
                int step = (range - 1) / 2;

                int nChan = m1.nChannels; // number of channels - 3
                int padding = m1.widthStep - m1.nChannels * m1.width; // alinhament bytes (padding)
                byte* offsetPtr = dataPtr;
                int x, y, sum, gray, current, dx, dy, rx, ry;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            sum = 0;
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
                                    sum += offsetPtr[0];
                                }
                            }
                            offsetPtr = startPtr + y * padding + (y * width + x) * nChan;
                            current = offsetPtr[0];
                            sum = sum / area;
                            gray = current < sum * (1.0f - 0.15f) ? 0 : 255;

                            // store in the image
                            dataPtr[0] = (byte)gray;
                            dataPtr[1] = (byte)gray;
                            dataPtr[2] = (byte)gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        ///<summary>
        ///Traffic Signs Detection
        ///</summary>
        ///<param name="img">Input image</param>
        ///<param name="imgCopy">Image Copy</param>
        ///<param name="limitSign">List of speed limit value and positions (speed limit value, Left-x,Top-y,Right-x,Bottom-y) of all detected limit signs</param>
        ///<param name="warningSign">List of value (-1) and positions (-1, Left-x,Top-y,Right-x,Bottom-y) of all detected warning signs</param>
        ///<param name="prohibitionSign">List of value (-1) and positions (-1, Left-x,Top-y,Right-x,Bottom-y) of all detected prohibition signs</param>
        ///<param name="level">Image Level</param>
        ///<returns>image with traffic signs detected</returns>
        public static Image<Bgr, byte> Signs(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, out List<string[]> limitSign, out List<string[]> warningSign, out List<string[]> prohibitionSign, int level)
        {
            byte[] colour = { 50, 0, 255 };
            int i, j, value, shape, fx, fy;
            int minSize = 50;
            float minRatio = .60f;
            double formFact, per;
            int[] t, a;
            string[] s;
            limitSign = new List<string[]>();
            warningSign = new List<string[]>();
            prohibitionSign = new List<string[]>();
            //Mean(imgCopy, img);
            RedImage(imgCopy);
            RedChannel(imgCopy);
            List<int> areas, nareas;
            List<int[]> coords, subcoords, ncoords;
            List<int[]> found = new List<int[]>();
            List<bool[,]> subs, matrixes, nums;
            HVProjections(imgCopy, minSize, out coords, out subs);

            for(i = 0; i < coords.Count; i++)
            {
                ConnectedComponents(subs[i], minSize, minRatio, out subcoords, out matrixes, out areas);
                for(j = 0; j < subcoords.Count; j++)
                {
                    t = subcoords[j];
                    per = Perimeter(matrixes[j]);
                    formFact = (Math.PI * 4d * areas[j]) / (per * per);
                    a = new int[]
                    {
                        t[0] + coords[i][0],
                        t[1] + coords[i][1],
                        t[2] + coords[i][0],
                        t[3] + coords[i][1]
                    };
                    if (formFact > 0.08d && formFact < 0.5d)
                    {
                        s = new string[] {
                            "-1",
                            a[0].ToString(),
                            a[1].ToString(),
                            a[2].ToString(),
                            a[3].ToString()
                        };
                        shape = Shape(matrixes[j], 0.15f);
                        if (shape == 0)
                        {
                            fx = (int)((a[2] - a[0]) / 6f);
                            fy = (int)((a[3] - a[1]) / 3.5f);
                            ConnectedComponents(OtsuBinary(Crop(img, new int[] { a[0] + fx, a[1] + fy, a[2] - fx, a[3] - fy })), 5, 0f, out ncoords, out nums, out nareas);
                            value = GetValue(nums, nareas, 0.8f);
                            if(value == -1)
                            {
                                prohibitionSign.Add(s);
                            } else
                            {
                                s[0] = value.ToString();
                                limitSign.Add(s);
                            }
                        } else if(shape == 1)
                        {
                            warningSign.Add(s);
                        } else
                        {
                            continue;
                        }
                        found.Add(a);
                    }
                }
            }
            
            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* startPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr = startPtr;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                foreach(int[] c in found)
                {
                    for (y = c[1] - 1; y < c[3] + 1; y++)
                    {
                        x = c[0] - 1;
                        dataPtr = startPtr + y * padding + (y * width + x) * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];
                    }

                    for (y = c[1] - 1; y < c[3] + 1; y++)
                    {
                        x = c[2] - 2;
                        dataPtr = startPtr + y * padding + (y * width + x) * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];
                    }

                    for(x = c[0] + 2; x < c[2] - 2; x++)
                    {
                        y = c[1] - 1;
                        dataPtr = startPtr + y * padding + (y * width + x) * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += padding + width * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += padding + width * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];
                    }

                    for (x = c[0] + 2; x < c[2] - 2; x++)
                    {
                        y = c[3] - 2;
                        dataPtr = startPtr + y * padding + (y * width + x) * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += padding + width * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];

                        dataPtr += padding + width * nChan;
                        dataPtr[0] = colour[0];
                        dataPtr[1] = colour[1];
                        dataPtr[2] = colour[2];
                    }
                }
                return img;
            }
        }

        public static void HVProjections(Image<Bgr, byte> img, int minSize, out List<int[]> coords, out List<bool[,]> subs)
        {
            unsafe
            {
                coords = new List<int[]>();
                subs = new List<bool[,]>();
                Stack<int[]> toExamine = new Stack<int[]>();
                
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;

                int x, y, start;
                bool first = true;
                bool[] lines, cols;
                int[] cur, t1, t2;
                List<int[]> fy = new List<int[]>();
                List<int[]> fx = new List<int[]>();
                bool[,] bin = new bool[width, height];
                bool[,] i;

                toExamine.Push(new int[] { 0, 0, width, height });
                while (toExamine.Count != 0)
                {
                    cur = toExamine.Pop();

                    lines = new bool[cur[3] - cur[1]];
                    cols = new bool[cur[2] - cur[0]];
                    i = new bool[cur[2] - cur[0], cur[3] - cur[1]];
                    fy.Clear();
                    fx.Clear();

                    if (first)
                    {
                        for (y = 0; y < height; y++)
                        {
                            for (x = 0; x < width; x++)
                            {
                                if (dataPtr[0] != 0)
                                {
                                    lines[y] = true;
                                    cols[x] = true;
                                    bin[x, y] = true;
                                }
                                dataPtr += nChan;
                            }
                            dataPtr += padding;
                        }
                        first = false;
                    }
                    else
                    {
                        for (y = cur[1]; y < cur[3]; y++)
                        {
                            for (x = cur[0]; x < cur[2]; x++)
                            {
                                if(bin[x, y])
                                {
                                    lines[y - cur[1]] = true;
                                    cols[x - cur[0]] = true;
                                    i[x - cur[0], y - cur[1]] = bin[x, y];
                                }
                            }
                        }
                    }

                    start = -1;
                    for(y = 0; y < cur[3] - cur[1]; y++)
                    {
                        if(start == -1)
                        {
                            if(lines[y])
                            {
                                start = y;
                            }
                        } else
                        {
                            if(!lines[y])
                            {
                                fy.Add(new int[] { start + cur[1], y + cur[1] });
                                start = -1;
                            }
                        }
                    }
                    if(start != -1)
                    {
                        fy.Add(new int[] { start + cur[1], cur[3] });
                    }
                    start = -1;
                    for (x = 0; x < cur[2] - cur[0]; x++)
                    {
                        if (start == -1)
                        {
                            if (cols[x])
                            {
                                start = x;
                            }
                        }
                        else
                        {
                            if (!cols[x])
                            {
                                fx.Add(new int[] { start + cur[0], x + cur[0] });
                                start = -1;
                            }
                        }
                    }
                    if (start != -1)
                    {
                        fx.Add(new int[] { start + cur[0], cur[2] });
                    }

                    if(fx.Count ==  0 && fy.Count == 0)
                    {
                        continue;
                    }
                    if (fx.Count == 1 && fy.Count == 1)
                    {
                        t1 = fx[0];
                        t2 = fy[0];
                        if ((t1[1] - t1[0]) == (cur[2] - cur[0]) && (t2[1] - t2[0]) == (cur[3] - cur[1]))
                        {
                            coords.Add(cur);
                            subs.Add(i);
                            continue;
                        }
                    }
                    for(y = 0; y < fy.Count; y++)
                    {
                        t1 = fy[y];
                        if(t1[1] - t1[0] < minSize)
                        {
                            continue;
                        }
                        for(x = 0; x < fx.Count; x++)
                        {
                            t2 = fx[x];
                            if(t2[1] - t2[0] < minSize)
                            {
                                continue;
                            }
                            toExamine.Push(new int[] { t2[0], t1[0], t2[1], t1[1] });
                        }
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
            } else if(max == newGreen)
            {
                hue = 60 * (2 + (newBlue - newRed) / delta);
            } else
            {
                hue = 60 * (4 + (newRed - newGreen) / delta);
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

        public static void RedImage(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* startPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr = startPtr;
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
                            if (((prevHSV[0] < 20 && prevHSV[0] >= 0) || prevHSV[0] > 325) && prevHSV[1] > .35d && prevHSV[2] > .20d)
                            {
                                prevHSV[2] = 1;
                            }
                            else
                            {
                                prevHSV[2] = 0;
                            }
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

        public static void BlackImage(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* startPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr = startPtr;
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
                            if (prevHSV[1] < 1d && prevHSV[2] < .60d)
                            {
                                prevHSV[2] = 1;
                            }
                            else
                            {
                                prevHSV[2] = 0;
                            }
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

        public static void ConnectedComponents(bool[,] img, int minSize, float minRatio, out List<int[]> coords, out List<bool[,]> matrixes, out List<int> areas)
        {
            List<int[]> objects = new List<int[]>();
            coords = new List<int[]>();
            List<int> found = new List<int>();
            areas = new List<int>();
            matrixes = new List<bool[,]>();
            int height = img.GetLength(1);
            int width = img.GetLength(0);
            int x, y, label, dx, dy, rx, ry, i, area;
            int[] t;
            int[,] labels = new int[width, height];
            bool[,] mat;
            bool flip = false;
            bool changed = true;
            float ratio;

            label = 0;
            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (img[x, y])
                    {
                        labels[x, y] = label;
                        label = label + 1;
                    }
                    else
                    {
                        labels[x, y] = int.MaxValue;
                    }
                }
            }
            
            while (changed)
            {
                changed = false;
                if(flip)
                {
                    for (x = width - 1; x > -1; x--)
                    {
                        for (y = height - 1; y > -1; y--)
                        {
                            for (dx = -1; dx < 2; dx++)
                            {
                                rx = x + dx;
                                if (rx < 0)
                                {
                                    continue;
                                }
                                else if (rx >= width)
                                {
                                    continue;
                                }
                                for (dy = -1; dy < 2; dy++)
                                {
                                    ry = y + dy;
                                    if (ry < 0)
                                    {
                                        continue;
                                    }
                                    else if (ry >= height)
                                    {
                                        continue;
                                    }

                                    if (labels[rx, ry] < labels[x, y] && labels[x, y] != int.MaxValue)
                                    {
                                        labels[x, y] = labels[rx, ry];
                                        changed = true;
                                    }
                                }
                            }
                        }
                    }
                    flip = false;
                } else
                {
                    for (x = 0; x < width; x++)
                    {
                        for (y = 0; y < height; y++)
                        {
                            for (dx = -1; dx < 2; dx++)
                            {
                                rx = x + dx;
                                if (rx < 0)
                                {
                                    continue;
                                }
                                else if (rx >= width)
                                {
                                    continue;
                                }
                                for (dy = -1; dy < 2; dy++)
                                {
                                    ry = y + dy;
                                    if (ry < 0)
                                    {
                                        continue;
                                    }
                                    else if (ry >= height)
                                    {
                                        continue;
                                    }

                                    if (labels[rx, ry] < labels[x, y] && labels[x, y] != int.MaxValue)
                                    {
                                        labels[x, y] = labels[rx, ry];
                                        changed = true;
                                    }
                                }
                            }
                        }
                    }
                    flip = true;
                }
            }

            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    if(labels[x, y] != int.MaxValue)
                    {
                        if(found.Contains(labels[x, y]))
                        {
                            i = found.IndexOf(labels[x, y]);
                            t = objects[i];
                            t[0] = x < t[0] ? x : t[0];
                            t[1] = y < t[1] ? y : t[1];
                            t[2] = x > t[2] ? x : t[2];
                            t[3] = y > t[3] ? y : t[3];
                            objects[i] = t;
                        } else
                        {
                            found.Add(labels[x, y]);
                            objects.Add(new int[] { x, y, x, y });
                        }
                    }
                }
            }

            for(i = 0; i < objects.Count; i++) //change to classic for for the label, get sub image matrix
            {
                t = objects[i];
                rx = t[2] - t[0];
                ry = t[3] - t[1];
                ratio = (float)Math.Min(rx, ry) / Math.Max(rx, ry);
                if (rx < minSize || ry < minSize || ratio < minRatio)
                {
                    continue;
                }
                label = found[i];
                area = 0;
                mat = new bool[rx, ry];
                for(x = t[0]; x < t[2]; x++)
                {
                    for(y = t[1]; y < t[3]; y++)
                    {
                        if(labels[x, y] == label)
                        {
                            mat[x - t[0], y - t[1]] = true;
                            area++;
                        }
                    }
                }
                coords.Add(t);
                matrixes.Add(mat);
                areas.Add(area);
            }
        }

        public static double Perimeter(bool[,] img)
        {
            int height = img.GetLength(1);
            int width = img.GetLength(0);
            int[] modx = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] mody = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] first;
            int dir = 2;
            int x = 0;
            int y = 0;
            double hv = 0d;
            double d = 0d;
            while(!img[x, y])
            {
                x++;
                if(x >= width)
                {
                    x = 0;
                    y++;
                }
            }
            while(!img[x + modx[dir], y + mody[dir]]) {
                dir++;
            }
            first = new int[] { x, y, dir };
            if(dir % 2 == 0)
            {
                hv++;
            } else
            {
                d++;
            }
            x += modx[dir];
            y += mody[dir];
            dir = dir - 1 > 0 ? dir - 1 : dir + 7;
            while(x != first[0] || y != first[1] || dir != first[2])
            {
                if (x + modx[dir] >= 0 && x + modx[dir] < width && y + mody[dir] >= 0 && y + mody[dir] < height && img[x + modx[dir], y + mody[dir]])
                {
                    if (dir % 2 == 0)
                    {
                        hv++;
                    }
                    else
                    {
                        d++;
                    }
                    x += modx[dir];
                    y += mody[dir];
                    dir = dir - 1 >= 0 ? dir - 1 : dir + 7;
                } else
                {
                    dir = dir + 1 < 8 ? dir + 1 : dir - 7;
                }
            }
            return hv + d * Math.Sqrt(2); ;
        }

        public static int Shape(bool[,] img, float tolerance)
        {
            int height = img.GetLength(1);
            int width = img.GetLength(0);
            float w, z;
            int x, y;
            int a = -1;
            int b = height - 1;
            int c = -1;
            int d = height - 1;
            x = 1;
            for (y = 0; y < height; y++)
            {
                if (img[x, y])
                {
                    if (a == -1)
                    {
                        a = y;
                    } else
                    {
                        b = y;
                    }
                }
            }
            x = width - 2;
            for (y = 0; y < height; y++)
            {
                if (img[x, y])
                {
                    if (c == -1)
                    {
                        c = y;
                    } else
                    {
                        d = y;
                    }
                }
            }
            w = ((b + a) / 2f) / height;
            z = ((d + c) / 2f) / height;
            if(Math.Abs(w - z) < tolerance)
            {
                if(Math.Abs(0.5 - z) < tolerance) //checking if the horizontal ends are at mid height
                {
                    return 0;
                }
                if(Math.Abs(0.1 - z) < tolerance || Math.Abs(0.9 - z) < tolerance) //checking if the horizontal ends are at the top or bottom
                {
                    return 1;
                }
            }
            return -1;
        }

        public static List<bool[,]> Digits()
        {
            List<bool[,]> digits = new List<bool[,]>();
            bool[,] t;
            Image<Bgr, byte> img;
            MIplImage m;

            int width;
            int height;
            int nChan;
            int padding;
            int x, y;
            string prefix = "../../Resources/imagens/digitos/";
            string sufix = ".png";
            int i;
            for(i = 0; i < 10; i++)
            {
                img = new Image<Bgr, byte>(prefix + i + sufix);
                Negative(img);
                ConvertToBW_Otsu(img);

                unsafe
                {
                    m = img.MIplImage;
                    byte* dataPtr = (byte*)m.imageData.ToPointer();
                    width = img.Width;
                    height = img.Height;
                    nChan = m.nChannels;
                    padding = m.widthStep - m.nChannels * m.width;

                    t = new bool[width, height];
                    for(y = 0; y < height; y++)
                    {
                        for(x = 0; x < width; x++)
                        {
                            t[x, y] = dataPtr[0] != 0;
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                    digits.Add(t);
                }
            }
            return digits;
        }

        public static int GetValue(List<bool[,]> components, List<int> areas, float minLikeness)
        {
            bool[,] curr, cnum;
            int i, j, x, y, nx, ny, maxLikeVal, count, width, nwidth, height, nheight;
            int res = 0;
            float maxLikeness;
            int mult = 1;
            for(i = 0; i < components.Count - 1; i++)
            {
                mult *= 10;
            }
            for (i = 0; i < components.Count; i++)
            {
                curr = components[i];
                width = curr.GetLength(0);
                height = curr.GetLength(1);
                maxLikeness = -1f;
                maxLikeVal = -1;
                for(j = 0; j < 10; j++)
                {
                    cnum = digits[j];
                    nwidth = cnum.GetLength(0);
                    nheight = cnum.GetLength(1);
                    count = 0;
                    for (x = 0; x < width; x++)
                    {
                        nx = (int)Math.Floor((x / (float)width) * nwidth);
                        for(y = 0; y < height; y++)
                        {
                            ny = (int)Math.Floor((y / (float)height) * nheight);
                            if (curr[x, y] && cnum[nx, ny])
                            {
                                count++;
                            }
                        }
                    }
                    if(maxLikeness < count / (float)areas[i])
                    {
                        maxLikeness = count / (float)areas[i];
                        maxLikeVal = j;
                    }
                }
                if(maxLikeness < minLikeness)
                {
                    return -1;
                }
                res += mult * maxLikeVal;
                mult /= 10;
                System.Diagnostics.Debug.WriteLine(maxLikeness); 
            }
            System.Diagnostics.Debug.WriteLine(res);
            return res;
        }

        public static Image<Bgr, byte> Crop(Image<Bgr, byte> img, int[] coords)
        {

            unsafe
            {
                Image<Bgr, byte> res = new Image<Bgr, byte>(coords[2] - coords[0], coords[3] - coords[1]);
                MIplImage m1 = img.MIplImage;
                MIplImage m2 = res.MIplImage;
                byte* startPtr = (byte*)m1.imageData.ToPointer();
                byte* dPtr = (byte*)m2.imageData.ToPointer();
                byte* dataPtr = startPtr;

                int width = img.Width;
                int nChan1 = m1.nChannels;
                int nChan2 = m2.nChannels;
                int padding1 = m1.widthStep - m1.nChannels * m1.width;
                int padding2 = m2.widthStep - m2.nChannels * m2.width;
                int x, y;

                for (y = coords[1]; y < coords[3]; y++)
                {
                    for (x = coords[0]; x < coords[2]; x++)
                    {
                        dataPtr = startPtr + y * padding1 + (y * width + x) * nChan1;
                        dPtr[0] = dataPtr[0];
                        dPtr[1] = dataPtr[1];
                        dPtr[2] = dataPtr[2];
                        dPtr += nChan2;
                    }
                    dPtr += padding2;
                }
                return res;
            }
        }

        public static bool[,] OtsuBinary(Image<Bgr, byte> img)
        {
            ConvertToBW_Otsu(img);
            //ImageViewer view = new ImageViewer();
            //view.Image = img;
            //view.ShowDialog();
            
            bool[,] res;
            MIplImage m;

            int width;
            int height;
            int nChan;
            int padding;
            int x, y;

            unsafe
            {
                m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                width = img.Width;
                height = img.Height;
                nChan = m.nChannels;
                padding = m.widthStep - m.nChannels * m.width;

                res = new bool[width, height];
                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        res[x, y] = dataPtr[0] == 0;
                        dataPtr += nChan;
                    }
                    dataPtr += padding;
                }
            }
            return res;
        }
    }
}
