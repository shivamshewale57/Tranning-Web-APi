using Microsoft.AspNetCore.Mvc;
using WebApi.UI.Models.DTO;

namespace WebApi.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {

             List<RegionDto> response = new List<RegionDto>();
            // Get All Regions from Web Api 
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7251/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                
            }
            catch (Exception )
            {
                // log the exeption
                
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
