using Dapper;
using EventReservationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EventsApiController(IConfiguration configuration)
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
                    e.EventId,
                    e.EventName,
                    e.EventDate,
                    e.TicketPrice,
                    e.CategoryId,
                    e.VenueId,
                    c.CategoryName,
                    v.VenueName
                FROM Events e
                INNER JOIN Categories c ON e.CategoryId = c.CategoryId
                INNER JOIN Venues v ON e.VenueId = v.VenueId";

            var values = connection.Query<EventDto>(query).ToList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using var connection = GetConnection();

            string query = @"
                SELECT 
                    e.EventId,
                    e.EventName,
                    e.EventDate,
                    e.TicketPrice,
                    e.CategoryId,
                    e.VenueId,
                    c.CategoryName,
                    v.VenueName
                FROM Events e
                INNER JOIN Categories c ON e.CategoryId = c.CategoryId
                INNER JOIN Venues v ON e.VenueId = v.VenueId
                WHERE e.EventId = @EventId";

            var value = connection.QueryFirstOrDefault<EventDto>(query, new { EventId = id });

            if (value == null)
            {
                return NotFound("Etkinlik bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPost]
        public IActionResult Create(EventDto model)
        {
            using var connection = GetConnection();

            string query = @"
                INSERT INTO Events
                (
                    EventName,
                    EventDate,
                    TicketPrice,
                    CategoryId,
                    VenueId
                )
                VALUES
                (
                    @EventName,
                    @EventDate,
                    @TicketPrice,
                    @CategoryId,
                    @VenueId
                )";

            connection.Execute(query, model);

            return Ok("Etkinlik başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EventDto model)
        {
            using var connection = GetConnection();

            string query = @"
                UPDATE Events SET
                    EventName = @EventName,
                    EventDate = @EventDate,
                    TicketPrice = @TicketPrice,
                    CategoryId = @CategoryId,
                    VenueId = @VenueId
                WHERE EventId = @EventId";

            model.EventId = id;

            connection.Execute(query, model);

            return Ok("Etkinlik başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var connection = GetConnection();

            string query = "DELETE FROM Events WHERE EventId = @EventId";

            connection.Execute(query, new { EventId = id });

            return Ok("Etkinlik başarıyla silindi.");
        }
    }
}