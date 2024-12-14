using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;
using System.Text;
using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class LibraryEditPatchModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LibraryEditPatchModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IActionResult> OnPostSubmit(LibraryEntity library)
        {


            var url = $"http://localhost:5275/api/library?Id={library.Id}&Title={Uri.EscapeDataString(library.Title)}&Adress={Uri.EscapeDataString(library.Adress)}";

            var response = await _httpClient.PatchAsync(url, null); // Адрес API

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