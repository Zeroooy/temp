using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;

namespace TaskNexus.Web.Pages
{
    public class LibraryAddModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LibraryAddModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public LibraryEntity[] FoundLibraries;

        public async Task<IActionResult> OnPostSubmit(LibraryEntity library)
        {


            var url = $"http://localhost:5275/api/library?Id={library.Id}&Title={Uri.EscapeDataString(library.Title)}&Adress={Uri.EscapeDataString(library.Adress)}";

            var response = await _httpClient.PostAsync(url, null); // Адрес API

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Library not found.");
            }


            return Page();
        }

    }
}