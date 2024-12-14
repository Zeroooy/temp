using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;
using System.Text;
using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class BookEditPutModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BookEditPutModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IActionResult> OnPostSubmit(BookEntity book)
        {


            var url = $"http://localhost:5275/api/book?Id={book.Id}&Title={Uri.EscapeDataString(book.Title)}&Description={Uri.EscapeDataString(book.Description)}&Author={Uri.EscapeDataString(book.Author)}&LibraryEntityId={book.LibraryEntityId}";

            var response = await _httpClient.PutAsync(url, null); // Адрес API

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