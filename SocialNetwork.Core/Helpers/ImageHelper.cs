#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#endregion

namespace SocialNetwork.Core.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ToByteArray(this Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}