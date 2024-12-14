using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using models;

using System.Text.Json;

namespace TaskNexus.Web.Pages
{
    public class BooksModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BooksModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public BookEntity[] FoundBooks;

        public async Task<IActionResult> OnGetAsync()
        {

            var jsonRequest = JsonSerializer.Serialize(new {});
            //var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync("http://localhost:5275/api/books"); // Адрес API

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                FoundBooks = JsonSerializer.Deserialize<BookEntity[]>(jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Library not found.");
            }

            //FoundBooks = [new BookEntity { Adress="dsads", Id=1, Title="asdasd" }];

            return Page();
        }

    }
}