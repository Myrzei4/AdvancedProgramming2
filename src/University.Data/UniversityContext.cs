using University.Models;
using Microsoft.EntityFrameworkCore;

namespace University.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("UniversityDb");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().Ignore(s => s.IsSelected);

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "John", LastName = "Doe", PESEL = "1234567890", BirthDate = new DateTime(1990, 1, 1), Gender = "Male", PlaceOfBirth = "New York", PlaceOfResidence = "Los Angeles", PostalCode = "12345" },
                new Student { StudentId = 2, Name = "Jane", LastName = "Smith", PESEL = "0987654321", BirthDate = new DateTime(1995, 5, 10), Gender = "Female", PlaceOfBirth = "Chicago", PlaceOfResidence = "San Francisco", PostalCode = "54321" },
                new Student { StudentId = 3, Name = "Michael", LastName = "Johnson", PESEL = "9876543210", BirthDate = new DateTime(1988, 12, 25), Gender = "Male", PlaceOfBirth = "Houston", PlaceOfResidence = "Dallas", PostalCode = "67890" }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, Name = "Matematyka", Semester = "1", Lecturer = "Michalina Warszawa" },
                new Subject { SubjectId = 2, Name = "Biologia", Semester = "2", Lecturer = "Halina Katowice" },
                new Subject { SubjectId = 3, Name = "Chemia", Semester = "3", Lecturer = "Jan Nowak" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = "1", Title = "Book 1", Author = "Author 1", Publisher = "Publisher 1", PublicationDate = new DateTime(2022, 01, 01), Isbn = "ISBN 1", Genre = "Genre 1", Description = "Description 1" },
                new Book { BookId = "2", Title = "Book 2", Author = "Author 2", Publisher = "Publisher 2", PublicationDate = new DateTime(2022, 02, 02), Isbn = "ISBN 2", Genre = "Genre 2", Description = "Description 2" },
                new Book { BookId = "3", Title = "Book 3", Author = "Author 3", Publisher = "Publisher 3", PublicationDate = new DateTime(2022, 03, 03), Isbn = "ISBN 3", Genre = "Genre 3", Description = "Description 3" }
            );

            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { ClassroomID = "1", Location = "Building A, Room 101", Capacity = 30, AvailableSeats = 30, Projector = true, Whiteboard = true, Microphone = false, Description = "Standard classroom" },
                new Classroom { ClassroomID = "2", Location = "Building B, Room 201", Capacity = 20, AvailableSeats = 20, Projector = true, Whiteboard = false, Microphone = false, Description = "Small classroom" },
                new Classroom { ClassroomID = "3", Location = "Building C, Room 301", Capacity = 50, AvailableSeats = 50, Projector = true, Whiteboard = true, Microphone = true, Description = "Large classroom" }
            );

        }
    }
}
