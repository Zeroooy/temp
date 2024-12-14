using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;

using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class BookFindModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BookFindModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public BookEntity FoundBook;

        public async Task<IActionResult> OnPostSubmit(BookEntity book)
        {


            var url = $"http://localhost:5275/api/book?Id={book.Id}";

            var response = await _httpClient.GetAsync(url); // Адрес API

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                FoundBook = JsonSerializer.Deserialize<BookEntity>(jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Library not found.");
            }


            return Page();
        }

    }
}