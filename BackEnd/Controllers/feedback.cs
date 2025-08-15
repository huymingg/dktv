using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;                      // <--- THÊM CHO ExecuteAsync
using Microsoft.AspNetCore.Mvc;    // <--- THÊM CHO NpgsqlConnection
using System;
using System.Threading.Tasks;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Database=OCRP;Username=postgres;Password=huy2712004vp";
        [HttpPost("feedback-card")]
        public async Task<IActionResult> CreateFeedback([FromBody] CardFeedbackModel feedbackData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Quan trọng: Gán một ID duy nhất trước khi insert
            // Bạn có thể dùng Guid hoặc một cơ chế tạo mã khác.
            feedbackData.Id = Guid.NewGuid().ToString();

            // Sửa tên bảng thành "" và thêm cột "id"
            var sql = @"
            INSERT INTO public.car_feedback (
                id, fullname, address, tel, email, content, createddate, status, title
                )
            VALUES (
                @Id, @FullName, @Address, @Tel, @Email, @Content, @CreatedDate, @Status, @Title
            );";

            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                var affectedRows = await connection.ExecuteAsync(sql, feedbackData);

                if (affectedRows > 0)
                {
                    return Ok(new { message = "Góp ý thành công.", newId = feedbackData.Id });

                }
                else
                {
                    return BadRequest(new { message = "Không thể thêm dữ liệu góp ý." });
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
