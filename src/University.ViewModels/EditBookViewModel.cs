using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Extensions;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class EditBookViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;
    private Book? _book = new Book();

    public string Error
    {
        get { return string.Empty; }
    }

    public string this[string columnName]
    {
        get
        {
            if (columnName == "Title")
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return "Title is Required";
                }
            }
            if (columnName == "LastTitle")
            {
                if (string.IsNullOrEmpty(LastTitle))
                {
                    return "Last Title is Required";
                }
            }
            if (columnName == "Author")
            {
                if (string.IsNullOrEmpty(Author))
                {
                    return "Author is Required";
                }

            }
            if (columnName == "PublicationDate")
            {
                if (PublicationDate is null)
                {
                    return "Birth Date is Required";
                }
            }
            if (columnName == "Isbn")
            {
                if (Isbn is null)
                {
                    return "Isbn is Required";
                }
            }
            if (columnName == "Genre")
            {
                if (Genre is null)
                {
                    return "Genre is Required";
                }
            }
            if (columnName == "Publisher")
            {
                if (Genre is null)
                {
                    return "Publisher is Required";
                }
            }
            return string.Empty;
        }
    }

    private string _Title = string.Empty;
    public string Title
    {
        get
        {
            return _Title;
        }
        set
        {
            _Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _lastTitle = string.Empty;
    public string LastTitle
    {
        get
        {
            return _lastTitle;
        }
        set
        {
            _lastTitle = value;
            OnPropertyChanged(nameof(LastTitle));
        }
    }

    private string _Author = string.Empty;
    public string Author
    {
        get
        {
            return _Author;
        }
        set
        {
            _Author = value;
            OnPropertyChanged(nameof(Author));
        }
    }

    private DateTime? _PublicationDate = null;
    public DateTime? PublicationDate
    {
        get
        {
            return _PublicationDate;
        }
        set
        {
            _PublicationDate = value;
            OnPropertyChanged(nameof(PublicationDate));
        }
    }
    private string _isbn = string.Empty;
    public string Isbn
    {
        get
        {
            return _isbn;
        }
        set
        {
            _isbn = value;
            OnPropertyChanged(nameof(Isbn));
        }
    }
    private string _genre = string.Empty;
    public string Genre
    {
        get
        {
            return _genre;
        }
        set
        {
            _genre = value;
            OnPropertyChanged(nameof(Genre));
        }
    }
    private string _description = string.Empty;
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    private string _publisher = string.Empty;
    public string Publisher
    {
        get
        {
            return _publisher;
        }
        set
        {
            _publisher = value;
            OnPropertyChanged(nameof(Publisher));
        }
    }
    private string _response = string.Empty;
    public string Response
    {
        get
        {
            return _response;
        }
        set
        {
            _response = value;
            OnPropertyChanged(nameof(Response));
        }
    }

    private string _bookId = string.Empty;
    public string BookId
    {
        get
        {
            return _bookId;
        }
        set
        {
            _bookId = value;
            OnPropertyChanged(nameof(BookId));
            LoadBookData();
        }
    }

    private ObservableCollection<Subject>? _assignedSubjects = null;
    public ObservableCollection<Subject> AssignedSubjects
    {
        get
        {
            if (_assignedSubjects is null)
            {
                _assignedSubjects = LoadSubjects();
                return _assignedSubjects;
            }
            return _assignedSubjects;
        }
        set
        {
            _assignedSubjects = value;
            OnPropertyChanged(nameof(AssignedSubjects));
        }
    }

    private ICommand? _back = null;
    public ICommand Back
    {
        get
        {
            if (_back is null)
            {
                _back = new RelayCommand<object>(NavigateBack);
            }
            return _back;
        }
    }

    private void NavigateBack(object? obj)
    {
        var instance = MainWindowViewModel.Instance();
        if (instance is not null)
        {
            instance.BooksSubView = new BooksViewModel(_context, _dialogService);
        }
    }

    private ICommand? _save = null;
    public ICommand Save
    {
        get
        {
            if (_save is null)
            {
                _save = new RelayCommand<object>(SaveData);
            }
            return _save;
        }
    }

    private void SaveData(object? obj)
    {
        if (!IsValid())
        {
            Response = "Please complete all required fields";
            return;
        }

        if (_book is null)
        {
            return;
        }
       

        _book.Title = Title;
        _book.Author = Author;
        _book.PublicationDate = PublicationDate;
        _book.Isbn = Isbn;
        _book.Genre = Genre;
        _book.Description = Description;
        _book.Publisher = Publisher;

        _context.Entry(_book).State = EntityState.Modified;
        _context.SaveChanges();

        Response = "Data Updated";
    }

    public EditBookViewModel(UniversityContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    private ObservableCollection<Subject> LoadSubjects()
    {
        _context.Database.EnsureCreated();
        _context.Subjects.Load();
        return _context.Subjects.Local.ToObservableCollection();
    }

    private bool IsValid()
    {
        string[] properties = { "Name", "LastName", "PESEL", "BirthDay", "Gender", "PlaceOfBirth", "PlaceOfResidence", "PostalCode"};
        foreach (string property in properties)
        {
            if (!string.IsNullOrEmpty(this[property]))
            {
                return false;
            }
        }
        return true;
    }

    private void LoadBookData()
    {
        if (_context?.Books is null)
        {
            return;
        }
        _book = _context.Books.Find(BookId);
        if (_book is null)
        {
            return;
        }
        this.Title = _book.Title;
        this.Author = _book.Author;
        this.PublicationDate = _book.PublicationDate;
        this.Isbn = _book.Isbn;
        this.Genre = _book.Genre;
        this.Description = _book.Description;
        this.Publisher = _book.Publisher;
        /*
        if (_book.Subjects is null)
        {
            return;
        }
        foreach (Subject subject in _book.Subjects)
        {
            if (subject is not null && AssignedSubjects is not null)
            {
                var assignedSubject = AssignedSubjects
                    .FirstOrDefault(s => s.SubjectId == subject.SubjectId);
                if (assignedSubject is not null)
                { 
                    assignedSubject.IsSelected = true;
                }
            }
        }
        */
    }
}
