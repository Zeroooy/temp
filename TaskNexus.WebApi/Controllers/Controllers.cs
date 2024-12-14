using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using models;

using NetworkOS;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/library")]
    public class ControllerLibrary : ControllerBase
    {

        [HttpPost]
        public LibraryEntity Post(int Id, string Title, string Adress)
        {
            using ApplicationContext db = new();
            LibraryEntity newLibrary = new LibraryEntity()
            {
                Id = Id,
                Title = Title,
                Adress = Adress,
                Books = []
            };
            db.Libraries.Add(newLibrary);
            db.SaveChanges();

            return db.Libraries.Where(x => x.Id == newLibrary.Id).ToList().First();
        }


        [HttpGet]
        public LibraryEntity Get(int id)
        {
            try
            {
                using ApplicationContext db = new();

                return db.Libraries.Include(x => x.Books).FirstOrDefault(x =>x.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPut]
        public LibraryEntity Put(int Id, string? Title, string? Adress)
        {
            using ApplicationContext db = new();
            if (Title != null) db.Libraries.Where(x => x.Id == Id).ToList().First().Title = Title;
            else db.Libraries.Where(x => x.Id == Id).ToList().First().Title = "";
            if (Adress != null) db.Libraries.Where(x => x.Id == Id).ToList().First().Adress = Adress;
            else db.Libraries.Where(x => x.Id == Id).ToList().First().Adress = "";
            db.SaveChanges();

            return db.Libraries.Where(x => x.Id == Id).ToList().First();
        }

        [HttpPatch]
        public LibraryEntity Patch(int Id, string? Title, string? Adress)
        {
            using ApplicationContext db = new();
            if (Title != null) db.Libraries.Where(x => x.Id == Id).ToList().First().Title = Title;
            if (Adress != null) db.Libraries.Where(x => x.Id == Id).ToList().First().Adress = Adress;
            db.SaveChanges();

            return db.Libraries.Where(x => x.Id == Id).ToList().First();
        }

        [HttpDelete]
        public String Delete(int id)
        {
            try
            {
                using ApplicationContext db = new();

                db.Libraries.Remove(db.Libraries.Where(x => x.Id == id).ToList().First());
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return "false";
            }
            return "true";
        }






    }


    [ApiController]
    [Route("api/book")]
    public class ControllerBook : ControllerBase
    {

        [HttpPost]
        [HttpPost]
        public IActionResult Post(int Id, string Title, string Description, string Author, int LibraryEntityId)
        {
            using ApplicationContext db = new();

            try
            {
                // Проверяем, существует ли библиотека
                var library = db.Libraries.Include(l => l.Books).FirstOrDefault(x => x.Id == LibraryEntityId);

                if (library == null)
                {
                    return BadRequest("Library with the specified ID does not exist.");
                }

                // Создаем новую книгу
                BookEntity newBook = new BookEntity
                {
                    Id = Id, // Используйте автогенерацию ID, если возможно
                    Title = Title,
                    Description = Description,
                    Author = Author,
                    LibraryEntityId = LibraryEntityId
                };

                // Добавляем книгу в контекст
                db.Books.Add(newBook);

                // Связываем книгу с библиотекой
                library.Books.Add(newBook);

                // Сохраняем изменения
                db.SaveChanges();

                // Возвращаем созданную книгу
                return Ok(newBook);
            }
            catch (Exception ex)
            {
                // Логирование исключения для диагностики
                Console.WriteLine($"Error adding book: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again.");
            }
        }


        [HttpGet]
        public BookEntity Get(int id)
        {
            try
            {
                using ApplicationContext db = new();

                return db.Books.Where(x => x.Id == id).ToList().First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public BookEntity Put(int Id, string? Title, string? Description, string? Author, int? LibraryEntityId)
        {
            using ApplicationContext db = new();
            if (Title != null) db.Books.Where(x => x.Id == Id).ToList().First().Title = Title;
            else db.Books.Where(x => x.Id == Id).ToList().First().Title = "";
            if (Author != null) db.Books.Where(x => x.Id == Id).ToList().First().Author = Author;
            else db.Books.Where(x => x.Id == Id).ToList().First().Author = "";
            if (Description != null) db.Books.Where(x => x.Id == Id).ToList().First().Description = Description;
            else db.Books.Where(x => x.Id == Id).ToList().First().Description = "";
            if (LibraryEntityId != null) db.Books.Where(x => x.Id == Id).ToList().First().LibraryEntityId = LibraryEntityId;
            else db.Books.Where(x => x.Id == Id).ToList().First().LibraryEntityId = -1;
            db.SaveChanges();

            return db.Books.Where(x => x.Id == Id).ToList().First();
        }

        [HttpPatch]
        public BookEntity Patch(int Id, string? Title, string? Description, string? Author, int? LibraryEntityId)
        {
            using ApplicationContext db = new();
            if (Title != null) db.Books.Where(x => x.Id == Id).ToList().First().Title = Title;
            if (Author != null) db.Books.Where(x => x.Id == Id).ToList().First().Author = Author;
            if (Description != null) db.Books.Where(x => x.Id == Id).ToList().First().Description = Description;
            if (LibraryEntityId != null) db.Books.Where(x => x.Id == Id).ToList().First().LibraryEntityId = LibraryEntityId;
            db.SaveChanges();

            return db.Books.Where(x => x.Id == Id).ToList().First();
        }

        [HttpDelete]
        public String Delete(int id)
        {
            try
            {
                using ApplicationContext db = new();

                db.Books.Remove(db.Books.Where(x => x.Id == id).ToList().First());
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return "false";
            }
            return "true";
        }

    }




























    [ApiController]
    [Route("api/libraries")]
    public class ControllerLibraries : ControllerBase
    {
        [HttpGet]
        public List<LibraryEntity> Get()
        {
            try
            {
                using ApplicationContext db = new();

                return db.Libraries.Include(x => x.Books).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }



    [ApiController]
    [Route("api/books")]
    public class ControllerBooks : ControllerBase
    {
        [HttpGet]
        public List<BookEntity> Get()
        {
            try
            {
                using ApplicationContext db = new();

                return db.Books.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
