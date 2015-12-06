namespace CRM.Common.Extensions
{
    using System.IO;
    using System.Web;
    using System.Linq;
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class ImageExtensions
    {
        public static bool IsImage(this HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            var formats = new[]
            {
                ImageFormat.Jpeg, 
                ImageFormat.Png, 
                ImageFormat.Gif, 
                ImageFormat.Jpeg,
                ImageFormat.Bmp,
                ImageFormat.Tiff
            };

            using (var img = Image.FromStream(file.InputStream))
            {
                return formats.Contains(img.RawFormat);
            }

            // Or can use:

            //string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".Bmp", ".Tiff" };

            //return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static Image HttpPostedFileBaseToImage(this HttpPostedFileBase file)
        {
            return Image.FromStream(file.InputStream);
        }

        public static byte[] ImageToByteArray(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
