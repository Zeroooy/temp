using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;
using System.Text;
using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class BookDeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BookDeleteModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public LibraryEntity[] FoundLibraries;

        public async Task<IActionResult> OnPostSubmit(LibraryEntity library)
        {


            var url = $"http://localhost:5275/api/book?Id={library.Id}";

            var response = await _httpClient.DeleteAsync(url); // Адрес API

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