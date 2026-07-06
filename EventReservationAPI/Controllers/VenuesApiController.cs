using Dapper;
using EventReservationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VenuesApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            using var connection = GetConnection();

            string query = @"
                SELECT 
                    VenueId,
                    VenueName,
                    Location,
                    Capacity
                FROM Venues";

            var values = connection.Query<VenueDto>(query).ToList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using var connection = GetConnection();

            string query = @"
                SELECT 
                    VenueId,
                    VenueName,
                    Location,
                    Capacity
                FROM Venues
                WHERE VenueId = @VenueId";

            var value = connection.QueryFirstOrDefault<VenueDto>(query, new { VenueId = id });

            if (value == null)
            {
                return NotFound("Mekan bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPost]
        public IActionResult Create(VenueDto model)
        {
            using var connection = GetConnection();

            string query = @"
                INSERT INTO Venues
                (
                    VenueName,
                    Location,
                    Capacity
                )
                VALUES
                (
                    @VenueName,
                    @Location,
                    @Capacity
                )";

            connection.Execute(query, model);

            return Ok("Mekan başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, VenueDto model)
        {
            using var connection = GetConnection();

            string query = @"
                UPDATE Venues SET
                    VenueName = @VenueName,
                    Location = @Location,
                    Capacity = @Capacity
                WHERE VenueId = @VenueId";

            model.VenueId = id;

            connection.Execute(query, model);

            return Ok("Mekan başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var connection = GetConnection();

            string query = "DELETE FROM Venues WHERE VenueId = @VenueId";

            connection.Execute(query, new { VenueId = id });

            return Ok("Mekan başarıyla silindi.");
        }
    }
}