using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using University.Data;
using University.Models;
using University.ViewModels;
using University.Services;

namespace University.Tests;

[TestClass]
public class BooksTest
{
    private DbContextOptions<UniversityContext> _options;

    [TestInitialize()]
    public void Initialize()
    {
        _options = new DbContextOptionsBuilder<UniversityContext>()
            .UseInMemoryDatabase(databaseName: "UniversityTestDB_Books")
            .Options;
        SeedTestDB();
    }

    private void SeedTestDB()
    {
        using var context = new UniversityContext(_options);
        {
            context.Database.EnsureDeleted();
            var books = new List<Book>
                {
                    new Book { BookId = "1", Title = "Mathematics for Machine Learning", Author = "Marc Peter Deisenroth", Publisher = "Cambridge University Press", PublicationDate = new DateTime(2020, 1, 1) },
                    new Book { BookId = "2", Title = "Deep Learning", Author = "Ian Goodfellow", Publisher = "MIT Press", PublicationDate = new DateTime(2016, 1, 1) },
                    new Book { BookId = "3", Title = "Pattern Recognition and Machine Learning", Author = "Christopher M. Bishop", Publisher = "Springer", PublicationDate = new DateTime(2006, 1, 1) }
                };
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Show_all_books()
    {
        using var context = new UniversityContext(_options);
        {
            var booksViewModel = new BooksViewModel(context, new DialogService());
            bool hasData = booksViewModel.Books.Any();
            Assert.IsTrue(hasData);
        }
    }

  
   

    [TestMethod]
    public void Add_book_without_publisher()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Title = "New Book Title",
                Author = "New Author",
                PublicationDate = new DateTime(2024, 1, 1),
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Title == "New Book Title" && b.Author == "New Author");
            Assert.IsFalse(newBookExists);
        }
    }

    [TestMethod]
    public void Update_book()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "1",
                Title = "Updated Book Title",
                Author = "Updated Author",
                Publisher = "Updated Publisher",
                PublicationDate = new DateTime(2022, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "1");
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("Updated Book Title", updatedBook.Title);
            Assert.AreEqual("Updated Author", updatedBook.Author);
            Assert.AreEqual("Updated Publisher", updatedBook.Publisher);
            Assert.AreEqual(new DateTime(2022, 1, 1), updatedBook.PublicationDate);
        }
    }

   

    [TestMethod]
    public void Update_book_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "4", // Non-existent book ID
                Title = "Updated Book Title",
                Author = "Updated Author",
                Publisher = "Updated Publisher",
                PublicationDate = new DateTime(2022, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "4");
            Assert.IsNull(updatedBook);
        }
    }
    [TestMethod]
    public void Add_book_without_title()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Author = "New Author",
                Publisher = "New Publisher",
                PublicationDate = new DateTime(2024, 1, 1),
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Author == "New Author" && b.Publisher == "New Publisher");
            Assert.IsFalse(newBookExists);
        }
    }

    [TestMethod]
    public void Add_book_without_author()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Title = "New Book Title",
                Publisher = "New Publisher",
                PublicationDate = new DateTime(2024, 1, 1),
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Title == "New Book Title" && b.Publisher == "New Publisher");
            Assert.IsFalse(newBookExists);
        }
    }

    [TestMethod]
    public void Add_book_without_publication_date()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Title = "New Book Title",
                Author = "New Author",
                Publisher = "New Publisher",
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Title == "New Book Title" && b.Author == "New Author" && b.Publisher == "New Publisher");
            Assert.IsFalse(newBookExists);
        }
    }
    [TestMethod]
    public void Update_book_title()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "1",
                Title = "Updated Book Title",
                Author = "Marc Peter Deisenroth",
                Publisher = "Cambridge University Press",
                PublicationDate = new DateTime(2020, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "1");
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("Updated Book Title", updatedBook.Title);
            Assert.AreEqual("Marc Peter Deisenroth", updatedBook.Author);
            Assert.AreEqual("Cambridge University Press", updatedBook.Publisher);
            Assert.AreEqual(new DateTime(2020, 1, 1), updatedBook.PublicationDate);
        }
    }

    [TestMethod]
    public void Update_book_author()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "1",
                Title = "Mathematics for Machine Learning",
                Author = "Updated Author",
                Publisher = "Cambridge University Press",
                PublicationDate = new DateTime(2020, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "1");
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("Mathematics for Machine Learning", updatedBook.Title);
            Assert.AreEqual("Updated Author", updatedBook.Author);
            Assert.AreEqual("Cambridge University Press", updatedBook.Publisher);
            Assert.AreEqual(new DateTime(2020, 1, 1), updatedBook.PublicationDate);
        }
    }

    [TestMethod]
    public void Update_book_publisher()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "1",
                Title = "Mathematics for Machine Learning",
                Author = "Marc Peter Deisenroth",
                Publisher = "Updated Publisher",
                PublicationDate = new DateTime(2020, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "1");
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("Mathematics for Machine Learning", updatedBook.Title);
            Assert.AreEqual("Marc Peter Deisenroth", updatedBook.Author);
            Assert.AreEqual("Updated Publisher", updatedBook.Publisher);
            Assert.AreEqual(new DateTime(2020, 1, 1), updatedBook.PublicationDate);
        }
    }

    [TestMethod]
    public void Update_book_publication_date()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "1",
                Title = "Mathematics for Machine Learning",
                Author = "Marc Peter Deisenroth",
                Publisher = "Cambridge University Press",
                PublicationDate = new DateTime(2022, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "1");
            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("Mathematics for Machine Learning", updatedBook.Title);
            Assert.AreEqual("Marc Peter Deisenroth", updatedBook.Author);
            Assert.AreEqual("Cambridge University Press", updatedBook.Publisher);
            Assert.AreEqual(new DateTime(2022, 1, 1), updatedBook.PublicationDate);
        }
    }


    [TestMethod]
    public void Add_book_without_title_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Author = "New Author",
                Publisher = "New Publisher",
                PublicationDate = new DateTime(2024, 1, 1),
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Author == "New Author" && b.Publisher == "New Publisher");
            Assert.IsFalse(newBookExists);
        }
    }

    [TestMethod]
    public void Add_book_without_author_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Title = "New Book Title",
                Publisher = "New Publisher",
                PublicationDate = new DateTime(2024, 1, 1),
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Title == "New Book Title" && b.Publisher == "New Publisher");
            Assert.IsFalse(newBookExists);
        }
    }

    [TestMethod]
    public void Add_book_without_publication_date_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var addBookViewModel = new AddBookViewModel(context, new DialogService())
            {
                Title = "New Book Title",
                Author = "New Author",
                Publisher = "New Publisher",
            };
            addBookViewModel.Save.Execute(null);

            bool newBookExists = context.Books.Any(b => b.Title == "New Book Title" && b.Author == "New Author" && b.Publisher == "New Publisher");
            Assert.IsFalse(newBookExists);
        }
    }

   

    [TestMethod]
    public void Update_book_title_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "4", // Non-existent book ID
                Title = "Updated Book Title",
                Author = "Marc Peter Deisenroth",
                Publisher = "Cambridge University Press",
                PublicationDate = new DateTime(2020, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "4");
            Assert.IsNull(updatedBook);
        }
    }

    [TestMethod]
    public void Update_book_author_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "4", // Non-existent book ID
                Title = "Mathematics for Machine Learning",
                Author = "Updated Author",
                Publisher = "Cambridge University Press",
                PublicationDate = new DateTime(2020, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "4");
            Assert.IsNull(updatedBook);
        }
    }

    [TestMethod]
    public void Update_book_publisher_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "4", // Non-existent book ID
                Title = "Mathematics for Machine Learning",
                Author = "Marc Peter Deisenroth",
                Publisher = "Updated Publisher",
                PublicationDate = new DateTime(2020, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "4");
            Assert.IsNull(updatedBook);
        }
    }

    [TestMethod]
    public void Update_book_publication_date_negative()
    {
        using var context = new UniversityContext(_options);
        {
            var updateBookViewModel = new EditBookViewModel(context, new DialogService())
            {
                BookId = "4", // Non-existent book ID
                Title = "Mathematics for Machine Learning",
                Author = "Marc Peter Deisenroth",
                Publisher = "Cambridge University Press",
                PublicationDate = new DateTime(2022, 1, 1),
            };
            updateBookViewModel.Save.Execute(null);

            var updatedBook = context.Books.FirstOrDefault(b => b.BookId == "4");
            Assert.IsNull(updatedBook);
        }
    }
}
