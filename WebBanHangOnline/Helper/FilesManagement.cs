using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Scripting;

namespace WebBanHangOnline.Helper
{
    public class FilesManagement
    {
        private static string wwwRootPath = Directory.GetCurrentDirectory() +"\\wwwroot";
        // Tạo môi trường luu trữu web
        public static string uploadImage(IFormFile image)
        {
            // Tạo đường dẫn tới thư mục wwroot
            // Chuẩn hóa lại tên tệp thành duy nhất
            // Tạo tên duy nhất cho ảnh, gắn phần mở rộng của file tải lên vào tên ảnh
            // Tạo đường dẫn mới cho file ảnh về wwwroot
            // Tạo FileStream mới tới vị trí muốn luu file
            // Copy file tải lên vào fileStream
            // Tạo địa chỉ hoàn chỉnh cho file
            if(image != null && image.Length>0) {
                string urlPath = "";
                string id = Guid.NewGuid().ToString();

                string filePath = Path.Combine(wwwRootPath, "images", id + "-" + image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                    urlPath = Path.Combine("\\images", id + "-" + image.FileName);
                }
                return urlPath;
            }

            return null;
        }

    }
}
