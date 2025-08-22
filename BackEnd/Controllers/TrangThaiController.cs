using apithethuvien.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using apithethuvien;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiController : ControllerBase
    {
        [HttpGet("get-status")]
        public virtual async Task<List<GetStatus>> GetStatus()
        {
            var connection = new Connection();

            if (!connection.OpenConnection())
            {
                throw new Exception("Không thể mở kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = "SELECT * FROM car_status";
                var cmd = new NpgsqlCommand(sql, connection.GetConnection());

                var reader = await cmd.ExecuteReaderAsync();

                var Status = new List<GetStatus>();

                while (await reader.ReadAsync())
                {

                    var card = new GetStatus
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                    };

                    Status.Add(card);
                }

                await reader.CloseAsync();
                return Status;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn car_status", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
