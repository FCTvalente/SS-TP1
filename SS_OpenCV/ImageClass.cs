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
            //imgCopy.SmoothMedian(3).CopyTo(img);
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
                            if(((prevHSV[0] < 25 && prevHSV[0] >= 0) || prevHSV[0] > 325) && prevHSV[1] > .35d && prevHSV[2] > .20d)
                            {
                                prevHSV[2] = 1;
                            } else
                            {
                                prevHSV[2] = 0;
                            }
                            prevRGB = HSVtoRGB(prevHSV[0], prevHSV[1], prevHSV[2]);
                            //prevRGB = IntensifyRed((int)red, (int)blue, (int)green);

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
            RedChannel(img);
        }

        public static double[] IntensifyRed(double red, double blue, double green)
        {
            double newRed = red / 255;
            double newBlue = blue / 255;
            double newGreen = green / 255;

            double min = Math.Min(Math.Min(newRed, newGreen), newBlue);
            double max = Math.Max(Math.Max(newRed, newGreen), newBlue);

            double delta = max - min;

            double saturation;

            if (max == 0)
            {
                saturation = 0;
            }
            else
            {
                saturation = delta / max;
            }

            double value = 0;

            double hue = 180;

            if (delta != 0)
            {
                if (max == newRed)
                {
                    /*
                    value = max;
                    hue = 60 * (0 + (newGreen - newBlue) / delta);
                    hue = hue < 0 ? hue + 360 : hue;
                    double mult = hue <= 60.0d && hue >= 0.0d ? (60.0d - hue) / 60.0d : (hue - 300.0d) / 60.0d;
                    mult = mult > .5d ? (mult * 2.0d) - 1.0d : 0.0d;
                    mult = (mult + .3d) * (saturation * 2 - 1) * 2;
                    value = value + mult > 1 ? 1 : value + mult;
                    value = value > 0 ? value : 0;
                    value = saturation < .5d ? saturation : value;
                    */
                    value = max;
                    hue = 60 * (0 + (newGreen - newBlue) / delta);
                    hue = hue < 0 ? hue + 360 : hue;
                    double add = hue >= 300 ? 1 - ((hue - 300) / 60) : 0;
                    add = add * (saturation * 2 - 0);
                    value = value + add > 1 ? 1 : value + add;
                    value = value < 0 ? 0 : value;
                    value = saturation < .5d ? saturation : value;
                }
                else if (max == newBlue)
                {
                    value = max;
                    hue = 60 * (4 + (newRed - newGreen) / delta);
                    double add = hue >= 240 ? (hue - 240) / 60 : 0;
                    add = add * (saturation * 2 - 1);
                    value = value + add > 1 ? 1 : value + add;
                    value = value < 0 ? 0 : value;
                    value = saturation < .5d ? saturation : value;
                }
            }
            value = (hue <= 60 && hue > 0) || (hue <= 360 && hue > 240) ? value : 0;
            double c = 0f, hueNewValue = 0f, x = 0f, m = 0f;
            double[] prevRGB = new double[3];

            c = value * saturation;
            hueNewValue = hue / 60;
            x = c * (1 - Math.Abs(hueNewValue % 2 - 1));

            m = value - c;

            if (0 <= hue && hue <= 60)
            {
                prevRGB[0] = c; prevRGB[1] = x; prevRGB[2] = 0;
            }
            else if (240 < hue && hue <= 300)
            {
                prevRGB[0] = x; prevRGB[1] = 0; prevRGB[2] = c;
            }
            else if (300 < hue && hue <= 360)
            {
                prevRGB[0] = c; prevRGB[1] = 0; prevRGB[2] = x;
            }
            else
            {
                prevRGB[0] = 0; prevRGB[1] = 0; prevRGB[2] = 0; m = 0;
            }

            prevRGB[0] = (prevRGB[0] + m) * 255;
            prevRGB[1] = (prevRGB[1] + m) * 255;
            prevRGB[2] = (prevRGB[2] + m) * 255;

            return prevRGB;
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


        public static int[,] connectedComponents(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* offsetPtr = dataPtr;
                byte  color;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int label = 0;
                int[,] labels = new int[width,height];
                
                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //retrive 3 colour components
                            color = dataPtr[0];
                            
                            if (color == 0)
                            {
                                labels[x, y] = label;
                                label = label + 1;
                            }
                            else
                            {
                                labels[x, y] = Int32.MaxValue;
                            }


                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                    int dx, dy, rx, ry;
                    int step = 3;
                    

                    Boolean changed = true;
                    while (changed)
                    {
                        changed = false;
                        for (y = 0; y < height; y++)
                        {
                            for (x = 0; x < width; x++)
                            {
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
                                        
                                        
                                        if (labels[rx,ry] < labels[x,y] && labels[x,y] != Int32.MaxValue)
                                        {   
                                            labels[x, y] = labels[rx, ry];
                                            changed = true;
                                        }
                                    }
                                }
                              
                            }

                     
                        }                       
                    }

                }
                return labels;
            }
        }

    }
}
