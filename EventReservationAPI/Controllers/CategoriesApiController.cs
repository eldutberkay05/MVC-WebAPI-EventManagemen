using Dapper;
using EventReservationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CategoriesApiController(IConfiguration configuration)
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

            string query = "SELECT CategoryId, CategoryName FROM Categories";

            var values = connection.Query<CategoryDto>(query).ToList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using var connection = GetConnection();

            string query = "SELECT CategoryId, CategoryName FROM Categories WHERE CategoryId = @CategoryId";

            var value = connection.QueryFirstOrDefault<CategoryDto>(query, new { CategoryId = id });

            if (value == null)
            {
                return NotFound("Kategori bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPost]
        public IActionResult Create(CategoryDto model)
        {
            using var connection = GetConnection();

            string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";

            connection.Execute(query, model);

            return Ok("Kategori başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryDto model)
        {
            using var connection = GetConnection();

            string query = @"
                UPDATE Categories 
                SET CategoryName = @CategoryName 
                WHERE CategoryId = @CategoryId";

            model.CategoryId = id;

            connection.Execute(query, model);

            return Ok("Kategori başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var connection = GetConnection();

            string query = "DELETE FROM Categories WHERE CategoryId = @CategoryId";

            connection.Execute(query, new { CategoryId = id });

            return Ok("Kategori başarıyla silindi.");
        }
    }
}