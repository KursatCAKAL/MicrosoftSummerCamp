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
        private static byte[] _imageMatrix;
        private static int _imageBytes;    
        private static int _colorLayers;   
        private static int _imageWidth;                                        
        private static int _imageHeight;


        public unsafe static void setImageBytes(Bitmap source)
        {
            if (source == null)
            {
                _imageBytes = 0;
                _imageMatrix = null;
                return;
            }

            Rectangle srcSize = new Rectangle(0, 0, source.Width, source.Height);
            _imageWidth = source.Width;
            _imageHeight = source.Height;
            _colorLayers = 4; // 32 bits

            BitmapData rawSrc = source.LockBits(srcSize, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb); // 32 bits
            _imageBytes = rawSrc.Width * rawSrc.Height * _colorLayers;
            _imageMatrix = new Byte[_imageBytes];
            System.Runtime.InteropServices.Marshal.Copy(rawSrc.Scan0, _imageMatrix, 0, _imageBytes);
            source.UnlockBits(rawSrc);
        }


        public unsafe static Bitmap resizeCanvas(int destWidth, int destHeight)
        {
            if (_imageMatrix == null)
                return null;
            
            int destBytes = destWidth * destHeight * _colorLayers;
            byte[] destMatrix = new byte[destBytes];

            int srcI = 0, destI = 0;
            int srcStride = _imageWidth * _colorLayers;
            int destStride = destWidth * _colorLayers;
            for (int i = 0; i < destHeight; i++)
            {
                for (int j = 0; j < destStride; j += _colorLayers)
                {
                    srcI = i * srcStride + j; 
                    destI = i * destStride + j; 

                    if (i < _imageHeight && j < srcStride)
                    {
                        destMatrix[destI] = _imageMatrix[srcI];
                        destMatrix[destI + 1] = _imageMatrix[srcI + 1];
                        destMatrix[destI + 2] = _imageMatrix[srcI + 2];
                        destMatrix[destI + 3] = _imageMatrix[srcI + 3];
                    }
                    else
                    {
                        destMatrix[destI] = 60;
                        destMatrix[destI+1] = 60;
                        destMatrix[destI+2] = 60;
                        destMatrix[destI + 3] = 255;
                    }
                }
            }

            Bitmap dest = new Bitmap(destWidth, destHeight);
            BitmapData rawDest = dest.LockBits(new Rectangle(0, 0, destWidth, destHeight), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(destMatrix, 0, rawDest.Scan0, destMatrix.Length);
            dest.UnlockBits(rawDest);
            return dest;
        }


        public unsafe static Bitmap setCanvasPosition(Rectangle position)
        {
            if (_imageMatrix == null)
                return null;

            int destBytes = (position.Width+1) * (position.Height+1) * _colorLayers;
            byte[] destMatrix = new Byte[destBytes];
            int destStride = (position.Width + 1) * _colorLayers;
           
            int srcStride = _imageWidth * _colorLayers;
            int maxX = position.X + position.Width + 1;
            if (maxX > _imageWidth)
                maxX = _imageWidth;
            maxX *= _colorLayers;
            int maxY = position.Y + position.Height + 1;
            if (maxY > _imageHeight)
                maxY = _imageHeight;

            int destI = 0, destJ = 0, srcIndex = 0, destIndex = 0;
            for (int i = position.Y; i < maxY; i++)
            {
                destJ = 0;
                for (int j = position.X*_colorLayers; j < maxX; j += _colorLayers)
                {
                    srcIndex = i * srcStride + j; 
                    destIndex = destI*destStride + destJ; 
                    destMatrix[destIndex] = _imageMatrix[srcIndex];
                    destMatrix[destIndex + 1] = _imageMatrix[srcIndex + 1];
                    destMatrix[destIndex + 2] = _imageMatrix[srcIndex + 2];
                    destMatrix[destIndex + 3] = _imageMatrix[srcIndex + 3];
                    destJ += _colorLayers;
                }
                destI++;
            }

            Bitmap dest = new Bitmap(position.Width+1, position.Height+1);
            BitmapData rawDest = dest.LockBits(new Rectangle(0, 0, position.Width+1, position.Height+1), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(destMatrix, 0, rawDest.Scan0, destMatrix.Length);
            dest.UnlockBits(rawDest);
            return dest;
        }


        public unsafe static Bitmap setPaletteColor(Color origColor, Color newColor, int acceptance)
        {
            if (_imageMatrix == null)
                return null;

            byte[] destMatrix = new byte[_imageBytes];
            int stride = _imageWidth * _colorLayers;

            int index = 0;
            for (int i = 0; i < _imageHeight; i++)
            {
                for (int j = 0; j < stride; j += _colorLayers)
                {
                    index = i * stride + j; 

                    
                    if ((int)_imageMatrix[index + 2] >= (int)origColor.R - acceptance && (int)_imageMatrix[index + 2] <= (int)origColor.R + acceptance
                      && (int)_imageMatrix[index + 1] >= (int)origColor.G - acceptance && (int)_imageMatrix[index + 1] <= (int)origColor.G + acceptance
                      && (int)_imageMatrix[index] >= (int)origColor.B -acceptance && (int)_imageMatrix[index] <= (int)origColor.B + acceptance)
                    {
                        destMatrix[index] = newColor.B;
                        destMatrix[index + 1] = newColor.G;
                        destMatrix[index + 2] = newColor.R;
                    }
                    else
                    {
                        destMatrix[index] = _imageMatrix[index];
                        destMatrix[index + 1] = _imageMatrix[index + 1];
                        destMatrix[index + 2] = _imageMatrix[index + 2];
                    }
                    destMatrix[index + 3] = _imageMatrix[index + 3]; 
                }
            }

            Bitmap dest = new Bitmap(_imageWidth, _imageHeight);
            BitmapData rawDest = dest.LockBits(new Rectangle(0, 0, _imageWidth, _imageHeight), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(destMatrix, 0, rawDest.Scan0, destMatrix.Length);
            dest.UnlockBits(rawDest);
            return dest;
        }


        public unsafe static Bitmap setOppositeColors()
        {
            if (_imageMatrix == null)
                return null;

            byte[] destMatrix = new byte[_imageBytes];
            int stride = _imageWidth * _colorLayers;

            int index = 0;
            for (int i = 0; i < _imageHeight; i++)
            {
                for (int j = 0; j < stride; j += _colorLayers)
                {
                    index = i * stride + j; 
                    destMatrix[index] = (byte)(255 - _imageMatrix[index]);
                    destMatrix[index + 1] = (byte)(255 - _imageMatrix[index + 1]);
                    destMatrix[index + 2] = (byte)(255 - _imageMatrix[index + 2]);
                    destMatrix[index + 3] = _imageMatrix[index + 3]; 
                }
            }

            Bitmap dest = new Bitmap(_imageWidth, _imageHeight);
            BitmapData rawDest = dest.LockBits(new Rectangle(0, 0, _imageWidth, _imageHeight), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(destMatrix, 0, rawDest.Scan0, destMatrix.Length);
            dest.UnlockBits(rawDest);
            return dest;
        }


        public unsafe static Bitmap extractPlane(Color colorPlane)
        {
            if (_imageMatrix == null)
                return null;

            byte[] destMatrix = new byte[_imageBytes];
            int stride = _imageWidth * _colorLayers;
            int index = 0;

            if(colorPlane == Color.Blue)
            {
                for (int i = 0; i < _imageHeight; i++)
                {
                    for (int j = 0; j < stride; j += _colorLayers)
                    {
                        index = i * stride + j; 
                        destMatrix[index] = _imageMatrix[index];
                        destMatrix[index + 1] = _imageMatrix[index];
                        destMatrix[index + 2] = _imageMatrix[index];
                        destMatrix[index + 3] = _imageMatrix[index + 3]; 
                    }
                }
            }
            else if(colorPlane == Color.Green)
            {
                for (int i = 0; i < _imageHeight; i++)
                {
                    for (int j = 0; j < stride; j += _colorLayers)
                    {
                        index = i * stride + j; 
                        destMatrix[index] = _imageMatrix[index + 1];
                        destMatrix[index + 1] = _imageMatrix[index + 1];
                        destMatrix[index + 2] = _imageMatrix[index + 1];
                        destMatrix[index + 3] = _imageMatrix[index + 3]; 
                    }
                }
            }
            
            else if (colorPlane == Color.Red)
            {
                for (int i = 0; i < _imageHeight; i++)
                {
                    for (int j = 0; j < stride; j += _colorLayers)
                    {
                        index = i * stride + j; 
                        destMatrix[index] = _imageMatrix[index + 2];
                        destMatrix[index + 1] = _imageMatrix[index + 2];
                        destMatrix[index + 2] = _imageMatrix[index + 2];
                        destMatrix[index + 3] = _imageMatrix[index + 3];
                    }
                }
            }
            else
            {
                double luminosity = 0.0;
                for (int i = 0; i < _imageHeight; i++)
                {
                    for (int j = 0; j < stride; j += _colorLayers)
                    {
                        index = i * stride + j; 
                        luminosity = (double)_imageMatrix[index] * 0.114 + (double)_imageMatrix[index + 1] * 0.587 + (double)_imageMatrix[index + 2] * 0.299;
                        if (luminosity > 255.0)
                            luminosity = 255.0;
                        destMatrix[index] = (byte)luminosity;
                        destMatrix[index + 1] = (byte)luminosity;
                        destMatrix[index + 2] = (byte)luminosity;
                        destMatrix[index + 3] = _imageMatrix[index + 3]; 
                    }
                }
            }

            Bitmap dest = new Bitmap(_imageWidth, _imageHeight);
            BitmapData rawDest = dest.LockBits(new Rectangle(0, 0, _imageWidth, _imageHeight), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(destMatrix, 0, rawDest.Scan0, destMatrix.Length);
            dest.UnlockBits(rawDest);
            return dest;
        }
    }
}
