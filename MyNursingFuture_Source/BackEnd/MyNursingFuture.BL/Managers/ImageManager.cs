using System;
using System.IO;
using System.Drawing;
using MyNursingFuture.Util;
using System.Web;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using System.Configuration;

namespace MyNursingFuture.BL.Managers
{
    public interface IImageManager
    {
        Result GetImages(int page, int count = 6);
    }
    public class ImageManager:IImageManager
    {
        public Result GetImages(int page, int count = 6)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new {page, count},
                Query = @"DECLARE @PageNumber AS INT, @RowspPage AS INT
                        SET @PageNumber = @page
                        SET @RowspPage = @count
                        SELECT *
                        FROM Images
                        ORDER BY Date
                        OFFSET ((@PageNumber - 1) * @RowspPage) ROWS
                        FETCH NEXT @RowspPage ROWS ONLY;"
            };
            return con.ExecuteQuery<ImageEntity>(query);
        }

        private void SaveImageData(string name)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"INSERT into Images (Name, Date) VALUES(@Name, @Date)",
                Entity = new {  Name = name, Date = DateTime.Now }
            };
            con.ExecuteQuery(query);
        }

        internal Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        internal Result IsImage(HttpPostedFileBase file)
        {
            var result = new Result();


            if (file == null || file.ContentType.Contains("image"))
            {
                result.Success = true;
                return result;
            }

            result.Success = false;
            result.Message = "Invalid image file";
            return result;
        }

        internal Result CheckFileImageAndSave(string mapPath, HttpPostedFileBase file, int minWidth, int mintHeight, int maxWidth, int maxHeight)
        {
            Result result = new Result(true);

            if (file == null || !file.ContentType.Contains("image"))
            {
                result.Message = "Invalid image file";
                result.Success = false;
                return result;
            }

            Stream fileStream = file.InputStream;
            fileStream.Position = 0;
            byte[] fileContents = new byte[file.ContentLength];
            fileStream.Read(fileContents, 0, file.ContentLength);
            Image image = Image.FromStream(new MemoryStream(fileContents));
            if (image.Height > maxHeight)
            {
                result.Success = false;
                result.Message = "Image height is larger than the maximum of " + maxHeight;
            }
            if (image.Width > maxWidth)
            {
                result.Success = false;
                result.Message += "\nImage width is larger than the maximum of " + maxWidth;
            }
            if (image.Height < mintHeight)
            {
                result.Success = false;
                result.Message += "\nImage height is lesser than the minimum of " + mintHeight;
            }
            if (image.Width < minWidth)
            {
                result.Success = false;
                result.Message += "\nImage width is lesser than the minimum of " + minWidth;
            }

            try
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == "")
                    extension = ".jpg";
                string fileName = Path.ChangeExtension(
                    Path.GetRandomFileName(),
                    extension
                );

                //var path = Path.Combine(mapPath, fileName);
                var path = Path.Combine(mapPath + fileName);
                file.SaveAs(path);

                //saving for webapi
                var pathUploadsApi = ConfigurationManager.AppSettings["UploadsWebAPI"];
                path = Path.Combine(pathUploadsApi + fileName);
                file.SaveAs(path);

                result.Entity = fileName;
                SaveImageData(fileName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Message = ex.Message;
                result.Success = false;
            }

            return result;
        }
        internal void DeleteImage(string fileName, string imgPath, bool deleteThumbnail = false)
        {
            try
            {
                var imagePath = Path.Combine(imgPath, fileName);
                File.Delete(imagePath);
            }
            catch (Exception)
            {
            }

        }
    }
}
