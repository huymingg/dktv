using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Threading.Tasks;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Database=OCRP;Username=postgres;Password=huy2712004vp";

        // API endpoint để tra cứu bằng mã đăng ký
        [HttpGet("ByRegCode/{regCode}")]
        public async Task<IActionResult> GetRegistrationByCode([FromRoute] string regCode)
        {
            if (string.IsNullOrEmpty(regCode))
            {
                return BadRequest(new { message = "Mã đăng ký không được để trống." });
            }

            var sql = "SELECT * FROM public.car_register WHERE registrationcode = @RegistrationCode LIMIT 1";

            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);

                // Sử dụng lại CardRegistrationModel để chứa kết quả
                var result = await connection.QueryFirstOrDefaultAsync<CardRegistrationModel>(sql, new { RegistrationCode = regCode });

                if (result != null)
                {
                    return Ok(result); // Trả về toàn bộ thông tin nếu tìm thấy
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy thông tin cho mã đăng ký này." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ở đây nếu cần
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }
        [HttpPost("ByPersonalInfo")]
        public async Task<IActionResult> GetRegistrationByInfo([FromBody] LookupByInfoModel lookupData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lưu ý: Câu lệnh WHERE cần khớp chính xác. 
            // Trong thực tế có thể cần xử lý chữ hoa/thường hoặc khoảng trắng.
            var sql = @"SELECT * FROM public.car_register 
                        WHERE fullname = @FullName AND dob = @Dob AND tel = @Tel 
                        LIMIT 1";
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                var result = await connection.QueryFirstOrDefaultAsync<CardRegistrationModel>(sql, lookupData);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy thông tin đăng ký phù hợp." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }
    }
}