using Dapper;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.IO;
using System.Threading.Tasks;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Database=OCRP;Username=postgres;Password=huy2712004vp";
        private readonly IWebHostEnvironment _environment; 

        // Sửa lại constructor để nhận IWebHostEnvironment
        public PhotoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload-image/{id}")]
        public virtual async Task<IActionResult> UploadImage(IFormFile file, [FromRoute] string id)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new { message = "File ảnh không hợp lệ" });

                // 1. Lấy đường dẫn tuyệt đối đến thư mục wwwroot/uploads
                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolderPath))
                    Directory.CreateDirectory(uploadsFolderPath);

                // 2. Tạo tên file và đường dẫn tuyệt đối để lưu file
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var fullPath = Path.Combine(uploadsFolderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 3. TẠO ĐƯỜNG DẪN WEB TƯƠNG ĐỐI ĐỂ LƯU VÀO DATABASE
                var relativePath = $"/uploads/{fileName}";

                // 4. Cập nhật database với đường dẫn web tương đối
                var sql = "update car_register set photo = @Photo where id=@id";
                await using var connection = new NpgsqlConnection(_connectionString);
                var affectedRows = await connection.ExecuteAsync(sql, new { Photo = relativePath, id = id });

                if (affectedRows > 0)
                {
                    return Ok(new { message = "Cập nhật ảnh thành công." });
                }
                else
                {
                    return BadRequest(new { message = "Không thể cập nhật ảnh vào database." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi upload ảnh: " + ex.Message);
            }
        }
    }
}