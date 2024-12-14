using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;

using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class LibrariesModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LibrariesModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public LibraryEntity[] FoundLibraries;

        public async Task<IActionResult> OnGetAsync()
        {

            var jsonRequest = JsonSerializer.Serialize(new {});
            //var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync("http://localhost:5275/api/libraries"); // Адрес API

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                FoundLibraries = JsonSerializer.Deserialize<LibraryEntity[]>(jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Library not found.");
            }


            return Page();
        }

    }
}