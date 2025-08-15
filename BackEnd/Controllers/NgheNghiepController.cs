using apithethuvien.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using apithethuvien;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgheNghiepController : ControllerBase
    {
        [HttpGet("get-catalog")]
        public virtual async Task<List<GetCatalog>> GetCatalog()
        {
            var connection = new Connection();

            if (!connection.OpenConnection())
            {
                throw new Exception("Không thể mở kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = "SELECT * FROM car_catalog where type='job'";
                var cmd = new NpgsqlCommand(sql, connection.GetConnection());

                var reader = await cmd.ExecuteReaderAsync();

                var cataLog = new List<GetCatalog>();

                while (await reader.ReadAsync())
                {

                    var card = new GetCatalog
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Type = reader.GetString(reader.GetOrdinal("type")),
                        
                    };

                    cataLog.Add(card);
                }
                await reader.CloseAsync();
                return cataLog;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn car_catalog", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
