using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;

namespace OX.Wallets.Flash.Chat
{
    public static class ImageCompressHelper
    {
        public static bool CompressImage(string sourcePath, long desiredSize, out byte[] bs)
        {
            bs = default;
            try
            {
                using (Image image = Image.FromFile(sourcePath))
                {
                    var w = image.Size.Width;
                    var h = image.Size.Height;
                    long quality = 100;
                    float rate = 1F;
                    int newW = 0;
                    int newH = 0;
                    MemoryStream ms = new MemoryStream();
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    Image bitmap;
                    do
                    {
                        ms.SetLength(0);
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        image.Save(ms, jpgEncoder, myEncoderParameters);
                        newW = (int)(w * rate);
                        newH = (int)(h * rate);
                        var bp = Bitmap.FromStream(ms);
                        bitmap = new System.Drawing.Bitmap(newW, newH);
                        Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.Clear(Color.White);
                        g.DrawImage(bp, new Rectangle(0, 0, newW, newH),
                            new Rectangle(0, 0, w, h),
                            GraphicsUnit.Pixel);
                        ms.SetLength(0);
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        if (quality > 10)
                            quality -= 10;
                        else
                            quality -= 1;
                        if (rate > 0.1)
                            rate -= 0.1F;
                        else
                            rate -= 0.01F;
                    } while (ms.Length > desiredSize && quality > 3 && rate > 0.001);
                    bs = ms.ToArray();
                    return bs.Length <= desiredSize;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool CompressImage(Stream sourceStream, long desiredSize, out byte[] bs)
        {
            bs = default;
            try
            {
                using (Image image = Image.FromStream(sourceStream))
                {
                    var w = image.Size.Width;
                    var h = image.Size.Height;
                    long quality = 100;
                    float rate = 1F;
                    int newW = 0;
                    int newH = 0;
                    MemoryStream ms = new MemoryStream();
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    Image bitmap;
                    do
                    {
                        ms.SetLength(0);
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        image.Save(ms, jpgEncoder, myEncoderParameters);
                        newW = (int)(w * rate);
                        newH = (int)(h * rate);
                        var bp = Bitmap.FromStream(ms);
                        bitmap = new System.Drawing.Bitmap(newW, newH);
                        Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.Clear(Color.White);
                        g.DrawImage(bp, new Rectangle(0, 0, newW, newH),
                            new Rectangle(0, 0, w, h),
                            GraphicsUnit.Pixel);
                        ms.SetLength(0);
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        if (quality > 10)
                            quality -= 10;
                        else
                            quality -= 1;
                        if (rate > 0.1)
                            rate -= 0.1F;
                        else
                            rate -= 0.01F;
                    } while (ms.Length > desiredSize && quality > 3 && rate > 0.001);
                    bs = ms.ToArray();
                    return bs.Length <= desiredSize;
                }
            }
            catch
            {
                return false;
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
