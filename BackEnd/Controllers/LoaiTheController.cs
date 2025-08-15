using apithethuvien;
using apithethuvien.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Npgsql;
using System.Reflection;
using System.Text;

namespace apithethuvien.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class LoaiTheController : ControllerBase
    {
        [HttpGet("get-card-type")]
        public virtual async Task<List<GetCardType>> GetCardType()
        {
            var connection = new Connection();

            if (!connection.OpenConnection())
            {
                throw new Exception("Không thể mở kết nối đến cơ sở dữ liệu.");
            }

            try
            {
                var sql = "SELECT * FROM car_cardtype";
                var cmd = new NpgsqlCommand(sql, connection.GetConnection());

                var reader = await cmd.ExecuteReaderAsync();

                var cardTypes = new List<GetCardType>();

                while (await reader.ReadAsync())
                {
                    var expireDateOrdinal = reader.GetOrdinal("expiredate"); // Get column index once

                    var card = new GetCardType
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Price = reader.GetString(reader.GetOrdinal("price")),
                        Status = reader.GetBoolean(reader.GetOrdinal("status")),
                        // Corrected line:
                        ExpireDate = reader.IsDBNull(expireDateOrdinal) ? (DateTime?)null : reader.GetDateTime(expireDateOrdinal)
                    };

                    cardTypes.Add(card);
                }

                await reader.CloseAsync();
                return cardTypes;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn car_cardtype", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
