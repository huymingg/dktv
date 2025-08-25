using apithethuvien.Controllers;
using apithethuvien.Dto;
using BackEnd.Dto;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuyetDangKyController : ControllerBase
    {

        [HttpGet("get-sign-up")]
        public virtual async Task<List<GetRegistration>> GetRegistations()
        {
            var connection = new Connection();
            if (!connection.OpenConnection())
            {
                throw new System.Exception("Không thể kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = @"
                    SELECT id, registrationcode, fullname, cardtype, cccd, createddate, status 
                    FROM car_register
                    ORDER BY createddate DESC";

                using (var conn = connection.GetConnection())
                {
                    var feedbackList = await conn.QueryAsync<GetRegistration>(sql);
                    return feedbackList.ToList();
                }
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



        [HttpGet("get-sign-details/{id}")]
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
                    var feedbackDetail = new GetRegistration
                    {
                        id = reader.GetString(reader.GetOrdinal("id")),
                        fullName = reader.GetString(reader.GetOrdinal("fullname")),
                        status = reader.GetString(reader.GetOrdinal("status")),
                        createddate = reader.GetDateTime(reader.GetOrdinal("createddate")),
                        registrationcode = reader.GetString(reader.GetOrdinal("registrationcode")),
                        cardtype = reader.GetString(reader.GetOrdinal("cardtype"))
                    };
                    await reader.CloseAsync();
                    return Ok(feedbackDetail);

                }
                else
                {
                    await reader.CloseAsync();
                    return NotFound("Không tìm thấy thẻ.");
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

        [HttpPut("approve-sign/{id}")]
        public virtual async Task<IActionResult> ApproveFeedback(string id)
        {
            // Sử dụng chuỗi kết nối trực tiếp và Dapper cho an toàn và ngắn gọn
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=OCRP";

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
        [HttpGet("get-sign-statuses")]
        public virtual async Task<IActionResult> GetFeedbackStatuses()
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=OCRP";
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