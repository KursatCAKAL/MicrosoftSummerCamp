using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageLibrary
{
    public partial class ImageLibrary
    {
        private static double[,] _cooccurrenceMatrix = null;
        public static double cooccurrenceMax = 0.0;
        public static int cooccurrenceTotal = 0;

        public unsafe static bool setCooccurrenceMatrix(int order, int[,] directions, int distance, bool isGrayscale, bool symetric)
        {
            if (_imageMatrix == null)
                return false;
            int i, j, k;

            byte[] monochromeImage = getMonochromeImage(isGrayscale);
            _cooccurrenceMatrix = new double[256, 256];
            for (i = 0; i < 256; i++)
                for (j = 0; j < 256; j++)
                    _cooccurrenceMatrix[i, j] = 0;

            cooccurrenceTotal = 0;
            int indexX, indexY, curPx, neighborPx;
            for (k = 0; k < order; k++) 
            {
                for (i = 0; i < _imageHeight; i++) 
                {
                    for (j = 0; j < _imageWidth; j++) 
                    {
                        indexX = j + distance * directions[k, 0];
                        indexY = i + distance * directions[k, 1];
                        if (indexX > 0 && indexX < _imageWidth && indexY > 0 && indexY < _imageHeight)
                        {
                            curPx = monochromeImage[i*_imageWidth + j];
                            neighborPx = monochromeImage[indexY*_imageWidth + indexX];
                            _cooccurrenceMatrix[curPx, neighborPx]++;
                            if (symetric)
                            {
                                _cooccurrenceMatrix[neighborPx, curPx]++;
                                cooccurrenceTotal += 2;
                            }
                            else
                                cooccurrenceTotal++;
                        }
                    }
                }
            }

            cooccurrenceMax = 0.0;
            for (i = 0; i < 256; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    if (_cooccurrenceMatrix[i, j] > cooccurrenceMax)
                        cooccurrenceMax = _cooccurrenceMatrix[i, j];
                }
            }
            if (cooccurrenceTotal != 0.0)
            {
                double divider = cooccurrenceTotal * (double)order;
                for (i = 0; i < 256; i++)
                    for (j = 0; j < 256; j++)
                        _cooccurrenceMatrix[i, j] /= divider;
            }
            return true;
        }

        private static byte[] getMonochromeImage(bool isGrayscale)
        {
            byte[] monochromeMatrix = new byte[_imageWidth * _imageHeight];
            int i, j;
            if (isGrayscale == false) 
            {
                float luminosity;
                j = 0;
                for (i = 0; i < _imageBytes; i += _colorLayers)
                {
                    luminosity = (float)_imageMatrix[i] * 0.114f + (float)_imageMatrix[i + 1] * 0.587f + (float)_imageMatrix[i + 2] * 0.299f;
                    if (luminosity > 255.0f)
                        luminosity = 255.0f;
                    monochromeMatrix[j] = (byte)luminosity;
                    j++;
                }
            }
            else 
            {
                j = 0;
                for (i = 0; i < _imageBytes; i += _colorLayers)
                {
                    monochromeMatrix[j] = _imageMatrix[i];
                    j++;
                }
            }
            return monochromeMatrix;
        }

        public static unsafe Bitmap getCoocurrenceBitmap()
        {
            if (_cooccurrenceMatrix == null)
                return null;

            byte[] destMatrix = new byte[256 * 256 * _colorLayers];
            int stride = 256*_colorLayers;
            double grayVal;
            int index;
            double factor = 128.0 * (256*256); 
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    grayVal = factor * _cooccurrenceMatrix[i, j];
                    if (grayVal > 255.0)
                        grayVal = 255.0;
                    index = i * stride + j * _colorLayers;
                    destMatrix[index] = destMatrix[index+1] = destMatrix[index+2] = (byte)grayVal;
                    destMatrix[index+3] = 255;
                }
            }
            Bitmap dest = new Bitmap(256, 256);
            BitmapData rawDest = dest.LockBits(new Rectangle(0, 0, 256, 256), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(destMatrix, 0, rawDest.Scan0, destMatrix.Length);
            dest.UnlockBits(rawDest);
            return dest;
        }

        public static double statEnergy = 0.0;
        public static double statInertia = 0.0;
        public static double statHomogeneity = 0.0;
        public static double statCorrelation = 0.0;
        public static void doCooccurrenceStats()
        {
            if (_cooccurrenceMatrix == null)
                return;

            int i, j;
            double tmpVal, tmpOp;
            double average = cooccurrenceTotal / (256 * 256);
            double variance = 0.0;
            for (i = 0; i < 256; i++)
                for (j = 0; j < 256; j++)
                    variance += _cooccurrenceMatrix[i, j] * (i - average) * (i - average);

            statEnergy = 0.0;
            tmpOp = 0.0;
            statInertia = 0.0;
            statHomogeneity = 0.0;
            statCorrelation = 0.0;
            for (i = 0; i < 256; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    tmpVal = _cooccurrenceMatrix[i, j];
                    statEnergy +=  tmpVal * tmpVal;
                    tmpOp = (i - j) * (i - j);
                    statInertia += tmpOp * tmpVal;
                    statHomogeneity += tmpVal / (1.0 + tmpOp);
                    statCorrelation += (tmpVal * ((double)i - average) * ((double)j - average)) / variance;
                }
            }
        }

        public static double[,] getNormalizedMatrix()
        {
            int i, j;
            double max = 0.0;
            for (i = 0; i < 256; i++)
            {
                for (j = 0; j < 256; j++)
                {
                    if (_cooccurrenceMatrix[i,j] > max)
                        max = _cooccurrenceMatrix[i,j];
                }
            }
            if (max == 0.0)
                max = 1.0;
            
            double[,] normMatrix = new double[256,256];
            for (i = 0; i < 256; i++)
                for (j = 0; j < 256; j++)
                    normMatrix[i,j] = _cooccurrenceMatrix[i,j] / max;
            return normMatrix;
        }
    }
}
