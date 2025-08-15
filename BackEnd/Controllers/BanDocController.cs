using apithethuvien.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using apithethuvien;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanDocController : ControllerBase
    {
        [HttpGet("get-reader-type")]
        public virtual async Task<List<GetReaderType>> GetReaderType()
        {
            var connection = new Connection();

            if (!connection.OpenConnection())
            {
                throw new Exception("Không thể mở kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = "SELECT * FROM car_objecttype";
                var cmd = new NpgsqlCommand(sql, connection.GetConnection());

                var reader = await cmd.ExecuteReaderAsync();

                var readerTypes = new List<GetReaderType>();

                while (await reader.ReadAsync())
                {

                    var card = new GetReaderType
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                    };

                    readerTypes.Add(card);
                }

                await reader.CloseAsync();
                return readerTypes;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn car_objecttype", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
