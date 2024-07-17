using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using University.Data;
using University.Interfaces;
using University.Models;
using University.Services;
using University.ViewModels;

namespace University.Tests;

[TestClass]
public class StudentsTest
{
    private IDialogService _dialogService;
    private DbContextOptions<UniversityContext> _options;

    [TestInitialize()]
    public void Initialize()
    {
        _options = new DbContextOptionsBuilder<UniversityContext>()
            .UseInMemoryDatabase(databaseName: "UniversityTestDB")
            .Options;
        SeedTestDB();
        _dialogService = new DialogService();
    }

    private void SeedTestDB()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            context.Database.EnsureDeleted();
            List<Student> students = new List<Student>
            {
                new Student { StudentId = 1, Name = "Wie�czys�aw", LastName = "Nowakowicz", PESEL="PESEL1", BirthDate = new DateTime(1987, 05, 22) },
                new Student { StudentId = 2, Name = "Stanis�aw", LastName = "Nowakowicz", PESEL = "PESEL2", BirthDate = new DateTime(2019, 06, 25) },
                new Student { StudentId = 3, Name = "Eugenia", LastName = "Nowakowicz", PESEL = "PESEL3", BirthDate = new DateTime(2021, 06, 08) }
            };
            List<Subject> subjects = new List<Subject>
            {
                new Subject { SubjectId = 1, Name = "Matematyka", Semester = "1", Lecturer = "Michalina Beldzik"},
                new Subject { SubjectId = 2, Name = "Biologia", Semester = "2", Lecturer = "Halina Kope�" },
                new Subject { SubjectId = 3, Name = "Chemia", Semester = "3", Lecturer = "Jan Nowak" }
            };
            context.Students.AddRange(students);
            context.Subjects.AddRange(subjects);
            context.SaveChanges();
        }
    }

    [TestMethod]
    public void Show_all_students()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            StudentsViewModel studentsViewModel = new StudentsViewModel(context, _dialogService);
            bool hasData = studentsViewModel.Students.Any();
            Assert.IsTrue(hasData);
        }
    }
    /*

    [TestMethod]
    public void Add_studend_without_subjects()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116");
            Assert.IsTrue(newStudentExists);
        }
    }
        
    [TestMethod]
    public void Add_studend_with_subjects()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            Random random = new Random();
            int toSkip = random.Next(0, context.Subjects.Count());
            Subject subject = context.Subjects.OrderBy(x => x.SubjectId).Skip(toSkip).Take(1).FirstOrDefault();
            subject.IsSelected = true;

            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe II",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06),
                AssignedSubjects = new ObservableCollection<Subject>
            {
                subject
            }
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe II" && s.PESEL == "67111994116" && s.Subjects.Any());
            Assert.IsTrue(newStudentExists);
        }
    }
    */

    [TestMethod]
    public void Add_Studend_without_name()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                LastName = "Doe  III",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.LastName == "Doe III" && s.PESEL == "67111994116");
            Assert.IsFalse(newStudentExists);
        }
    }

    [TestMethod]
    public void Add_Studend_without_last_name()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John IV",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John IV" && s.PESEL == "67111994116");
            Assert.IsFalse(newStudentExists);
        }
    }

    [TestMethod]
    public void Add_Studend_without_PESEL()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe V",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe V");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_Studend_without_Gender()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "Doe" && s.PESEL == "67111994116");
            Assert.IsFalse(newStudentExists);
        }
    }
  
    [TestMethod]
    public void Update_student()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            // Arrange
            int studentId = 1;
            string newName = "John";
            string newLastName = "Doe";
            string newPESEL = "1234567890";
            DateTime newBirthDate = new DateTime(1990, 01, 01);

            // Act
            Student student = context.Students.FirstOrDefault(s => s.StudentId == studentId);
            student.Name = newName;
            student.LastName = newLastName;
            student.PESEL = newPESEL;
            student.BirthDate = newBirthDate;
            context.SaveChanges();

            // Assert
            Student updatedStudent = context.Students.FirstOrDefault(s => s.StudentId == studentId);
            Assert.AreEqual(newName, updatedStudent.Name);
            Assert.AreEqual(newLastName, updatedStudent.LastName);
            Assert.AreEqual(newPESEL, updatedStudent.PESEL);
            Assert.AreEqual(newBirthDate, updatedStudent.BirthDate);
        }
    }
   
    [TestMethod]
    public void Test_PESEL_and_ID_Uniqueness()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            // Arrange
            string newPESEL = "1234567891"; // Updated PESEL to a valid value
            int newStudentId = 4;

            // Act
            bool isPESELUsed = context.Students.Any(s => s.PESEL == newPESEL);
            bool isStudentIdUsed = context.Students.Any(s => s.StudentId == newStudentId);

            // Assert
            Assert.IsFalse(isPESELUsed);
            Assert.IsFalse(isStudentIdUsed);
        }
    }
    [TestMethod]
    public void Test_PESEL_Uniqueness()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            // Arrange
            string existingPESEL = "PESEL1"; // Existing PESEL value

            // Act
            bool isPESELUsed = context.Students.Any(s => s.PESEL == existingPESEL);

            // Assert
            Assert.IsTrue(isPESELUsed);
        }
    }
    [TestMethod]
    public void Add_student_with_invalid_name()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "1234",
                LastName = "Doe",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "1234" && s.LastName == "Doe" && s.PESEL == "67111994116");
            Assert.IsFalse(newStudentExists);
        }
    }
    [TestMethod]
    public void Add_student_with_invalid_last_name()
    {
        using UniversityContext context = new UniversityContext(_options);
        {
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(context, _dialogService)
            {
                Name = "John",
                LastName = "1234",
                PESEL = "67111994116",
                BirthDate = new DateTime(1967, 12, 06)
            };
            addStudentViewModel.Save.Execute(null);

            bool newStudentExists = context.Students.Any(s => s.Name == "John" && s.LastName == "1234" && s.PESEL == "67111994116");
            Assert.IsFalse(newStudentExists);
        }
    }

    
    



}
