using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;

using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class LibraryFindModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LibraryFindModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public LibraryEntity FoundLibrary;

        public async Task<IActionResult> OnPostSubmit(LibraryEntity library)
        {


            var url = $"http://localhost:5275/api/library?Id={library.Id}";

            var response = await _httpClient.GetAsync(url); // Адрес API

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                FoundLibrary = JsonSerializer.Deserialize<LibraryEntity>(jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Library not found.");
            }


            return Page();
        }

    }
}