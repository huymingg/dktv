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
    public class RegisterController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Database=OCRP;Username=postgres;Password=huy2712004vp";
        [HttpPost("register-card")]
        public async Task<IActionResult> CreateRegistration([FromBody] global::CardRegistrationModel registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Quan trọng: Gán một ID duy nhất trước khi insert
            // Bạn có thể dùng Guid hoặc một cơ chế tạo mã khác.
            registrationData.Id = Guid.NewGuid().ToString();

            // Lấy 8 ký tự đầu của Id làm mã đăng ký và viết hoa
            registrationData.RegistrationCode = registrationData.Id.Substring(0, 6);

            // Sửa tên bảng thành "car_register" và thêm cột "id"
            var sql = @"
            INSERT INTO public.car_register (
                id, cardtype, readertype, registertype, fullname, dob, cccd, address, email, tel, job,
                photo, status, cardno, representative, createddate, createdby, deadline, note,
                office, gender, expireddate, receivetype, printcount, ispayment, sentemail,
                level, paymenttype, nation, registrationcode
            )
            VALUES (
                @Id, @CardType, @ReaderType, @RegisterType, @FullName, @Dob, @Cccd, @Address, @Email, @Tel, @Job,
                @Photo, @Status, @CardNo, @Representative, @CreatedDate, @CreatedBy, @Deadline, @Note,
                @Office, @Gender, @ExpiredDate, @ReceiveType, @PrintCount, @IsPayment, @SentEmail,
                @Level, @PaymentType, @Nation, @RegistrationCode
            );";

            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                var affectedRows = await connection.ExecuteAsync(sql, registrationData);

                if (affectedRows > 0)
                {
                    return Ok(new { message = "Đăng ký thẻ thành công.", newId = registrationData.Id });

                }
                else
                {
                    return BadRequest(new { message = "Không thể thêm dữ liệu đăng ký." });
                }
            }
            catch 
            {
                throw;
            }
        }

    }
}
