using EventReservationMVC.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace EventReservationMVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EventsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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

            var response = await client.GetAsync($"{GetApiBaseUrl()}/api/EventsApi");

            var jsonData = await response.Content.ReadAsStringAsync();

            var events = JsonSerializer.Deserialize<List<EventDto>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!string.IsNullOrEmpty(search))
            {
                events = events
                    .Where(x =>
                        x.EventName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        x.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        x.VenueName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(events);
        }

        public async Task<IActionResult> Create()
        {
            await FillDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventDto eventDto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(eventDto);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PostAsync($"{GetApiBaseUrl()}/api/EventsApi", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{GetApiBaseUrl()}/api/EventsApi/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var eventDto = JsonSerializer.Deserialize<EventDto>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            await FillDropdowns(eventDto.CategoryId, eventDto.VenueId);

            return View(eventDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventDto eventDto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonSerializer.Serialize(eventDto);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PutAsync($"{GetApiBaseUrl()}/api/EventsApi/{eventDto.EventId}", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{GetApiBaseUrl()}/api/EventsApi/{id}");

            return RedirectToAction("Index");
        }

        private async Task FillDropdowns(int? selectedCategoryId = null, int? selectedVenueId = null)
        {
            var client = _httpClientFactory.CreateClient();

            var categoryResponse = await client.GetAsync($"{GetApiBaseUrl()}/api/CategoriesApi");
            var categoryJson = await categoryResponse.Content.ReadAsStringAsync();

            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoryJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var venueResponse = await client.GetAsync($"{GetApiBaseUrl()}/api/VenuesApi");
            var venueJson = await venueResponse.Content.ReadAsStringAsync();

            var venues = JsonSerializer.Deserialize<List<VenueDto>>(venueJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", selectedCategoryId);
            ViewBag.Venues = new SelectList(venues, "VenueId", "VenueName", selectedVenueId);
        }
    }
}