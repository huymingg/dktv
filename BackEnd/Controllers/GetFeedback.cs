using apithethuvien.Controllers;
using apithethuvien.Dto;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyGopYController : ControllerBase
    {
        
        [HttpGet("get-all-feedback")]
        public virtual async Task<List<CardFeedbackModel>> GetAllFeedback()
        {
            var connection = new Connection();
            if (!connection.OpenConnection())
            {
                throw new System.Exception("Không thể kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = "SELECT id, title, fullname, createddate FROM car_feedback ORDER BY createddate DESC";
                var cmd = new NpgsqlCommand(sql, connection.GetConnection());
                var reader = await cmd.ExecuteReaderAsync();

                var feedbackList = new List<CardFeedbackModel>();
                while (await reader.ReadAsync())
                {
                    var feedback = new CardFeedbackModel
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        FullName = reader.GetString(reader.GetOrdinal("fullname")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("createddate"))
                    };
                    feedbackList.Add(feedback);
                }
                await reader.CloseAsync();
                return feedbackList;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Lỗi khi truy vấn car_feedback", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        
        [HttpGet("get-feedback-detail/{id}")]
        public virtual async Task<IActionResult> GetFeedbackDetail(string id)
        {
            var connection = new Connection();
            if (!connection.OpenConnection())
            {
                return StatusCode(503, "Không thể kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = "SELECT * FROM car_feedback WHERE id = @FeedbackId";
                var cmd = new NpgsqlCommand(sql, connection.GetConnection());
                cmd.Parameters.AddWithValue("FeedbackId", id);

                var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var feedbackDetail = new CardFeedbackModel
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        FullName = reader.GetString(reader.GetOrdinal("fullname")),
                        Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                        Content = reader.GetString(reader.GetOrdinal("content")),
                        Status = reader.GetString(reader.GetOrdinal("status")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("createddate"))
                    };
                    await reader.CloseAsync();
                    return Ok(feedbackDetail);
                }
                else
                {
                    await reader.CloseAsync();
                    return NotFound("Không tìm thấy góp ý.");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        [HttpPut("approve-feedback/{id}")]
        public virtual async Task<IActionResult> ApproveFeedback(string id)
        {
            // Sử dụng chuỗi kết nối trực tiếp và Dapper cho an toàn và ngắn gọn
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=huy2712004vp;Database=OCRP";

            try
            {
                var sql = "UPDATE car_feedback SET status = 'Đã duyệt' WHERE id = @FeedbackId";

                await using var connection = new NpgsqlConnection(connectionString);
                var affectedRows = await connection.ExecuteAsync(sql, new { FeedbackId = id });

                if (affectedRows > 0)
                {
                    return Ok(new { message = "Duyệt góp ý thành công." });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy góp ý để duyệt." });
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("get-feedback-statuses")]
        public virtual async Task<IActionResult> GetFeedbackStatuses()
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=huy2712004vp;Database=OCRP";
            var statuses = new List<string>();

            try
            {
                var sql = "SELECT DISTINCT status FROM car_feedback WHERE status IS NOT NULL AND status != '' ORDER BY status";

                await using var connection = new NpgsqlConnection(connectionString);
                var result = await connection.QueryAsync<string>(sql);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}