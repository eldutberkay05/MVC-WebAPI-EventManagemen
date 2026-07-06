using EventReservationMVC.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace EventReservationMVC.Controllers
{
    public class VenuesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public VenuesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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

            var response = await client.GetAsync($"{GetApiBaseUrl()}/api/VenuesApi");

            var jsonData = await response.Content.ReadAsStringAsync();

            var venues = JsonSerializer.Deserialize<List<VenueDto>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!string.IsNullOrEmpty(search))
            {
                venues = venues
                    .Where(x =>
                        x.VenueName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        x.Location.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(venues);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VenueDto venue)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(venue);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PostAsync($"{GetApiBaseUrl()}/api/VenuesApi", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{GetApiBaseUrl()}/api/VenuesApi/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var venue = JsonSerializer.Deserialize<VenueDto>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(venue);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VenueDto venue)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(venue);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PutAsync($"{GetApiBaseUrl()}/api/VenuesApi/{venue.VenueId}", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{GetApiBaseUrl()}/api/VenuesApi/{id}");

            return RedirectToAction("Index");
        }
    }
}