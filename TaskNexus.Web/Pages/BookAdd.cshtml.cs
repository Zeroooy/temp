using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;

namespace TaskNexus.Web.Pages
{
    public class BookAddModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BookAddModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IActionResult> OnPostSubmit(BookEntity book)
        {

            var url = $"http://localhost:5275/api/book?Id={book.Id}&Title={Uri.EscapeDataString(book.Title)}&Description={Uri.EscapeDataString(book.Description)}&Author={Uri.EscapeDataString(book.Author)}&LibraryEntityId={book.LibraryEntityId}";

            var response = await _httpClient.PostAsync(url, null); // Адрес API

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Book not found.");
            }


            return Page();
        }

    }
}