using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace TMLib
{
    public class TMImage
    {

        public static bool renameUploadFile(HttpPostedFileBase posted_file, string desired_path, string desired_filename,int size)
        {
            var posted_file_name = Path.GetFileName(posted_file.FileName);
            //has file been already uploaded
            if (!System.IO.File.Exists(HttpContext.Current.Request.MapPath(desired_path + desired_filename)))
                //file doesn't exist, upload item but validate first
                return uploadFile(posted_file, desired_path, desired_filename,size);
            else
                return false;
        }

        private static bool uploadFile(HttpPostedFileBase posted_file, string desired_path, string desired_filename,int size)
        {
            var desired_file_plus_path = Path.Combine(HttpContext.Current.Request.MapPath(desired_path), desired_filename);
            string posted_file_extension = Path.GetExtension(posted_file.FileName);

            //make sure the file is valid
            if (!validateExtension(posted_file_extension))
            {
                return false;
            }

            try
            {
                posted_file.SaveAs(desired_file_plus_path);
                Image uploaded_image = Image.FromFile(desired_file_plus_path);
                //pass in whatever value you want 
                Image rescaled_image = ScaleBySize(uploaded_image, size);
                uploaded_image.Dispose();
                rescaled_image.Save(desired_file_plus_path);
                rescaled_image.Dispose();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private static bool validateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        public static Image ScaleBySize(Image imgPhoto, int size)
        {

            Bitmap bmPhoto = null;
            int logoSize = size;
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            if (sourceHeight > size || sourceWidth > size)
            {
                // Resize Image to have the height = logoSize/2 or width = logoSize.
                // Height is greater than width, set Height = logoSize and resize width accordingly
                if (sourceWidth > sourceHeight)
                {
                    destWidth = logoSize;
                    destHeight = (float)(sourceHeight * logoSize / sourceWidth);
                }
                else
                {
                    //int h = logoSize / 2;
                    destHeight = logoSize;
                    destWidth = (float)(sourceWidth * logoSize / sourceHeight);
                }
            }
            else
            {
                destHeight = sourceHeight;
                destWidth = sourceWidth;
            }
            // Width is greater than height, set Width = logoSize and resize height accordingly

            bmPhoto = new Bitmap((int)destWidth, (int)destHeight, PixelFormat.Format16bppRgb555);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
           grPhoto.InterpolationMode = InterpolationMode.Low;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;

        }
    }
}