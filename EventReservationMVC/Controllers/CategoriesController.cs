using EventReservationMVC.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace EventReservationMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoriesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        private string GetApiBaseUrl()
        {
            return _configuration["ApiSettings:BaseUrl"];
        }

        public async Task<IActionResult> Index(string search)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{GetApiBaseUrl()}/api/CategoriesApi");

            var jsonData = await response.Content.ReadAsStringAsync();

            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!string.IsNullOrEmpty(search))
            {
                categories = categories
                    .Where(x => x.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(category);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PostAsync($"{GetApiBaseUrl()}/api/CategoriesApi", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{GetApiBaseUrl()}/api/CategoriesApi/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var category = JsonSerializer.Deserialize<CategoryDto>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto category)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(category);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PutAsync($"{GetApiBaseUrl()}/api/CategoriesApi/{category.CategoryId}", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{GetApiBaseUrl()}/api/CategoriesApi/{id}");

            return RedirectToAction("Index");
        }
    }
}