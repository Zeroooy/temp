using System.Text.Json.Serialization;

namespace models
{
    public class LibraryEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("adress")]
        public string Adress { get; set; }

        [JsonPropertyName("books")]
        public List<BookEntity>? Books { get; set; } = new List<BookEntity>();

    }

    public class BookEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }


        public int? LibraryEntityId { get; set; }

        [JsonIgnore]
        public LibraryEntity? Library { get; set; }

    }
}
