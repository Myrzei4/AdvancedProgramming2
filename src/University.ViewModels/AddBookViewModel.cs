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

public class AddBookViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;

    public string Error
    {
        get { return string.Empty; }
    }

    public string this[string columnTitle]
    {
        get
        {
            if (columnTitle == "Title")
            {
                if (string.IsNullOrEmpty(Title))
                { 
                    return "Title is Required";
                }
            }
            if (columnTitle == "LastTitle")
            {
                if (string.IsNullOrEmpty(LastTitle))
                {
                    return "Last Title is Required";
                }
            }
            if (columnTitle == "Author")
            {
                if (string.IsNullOrEmpty(Author))
                {
                    return "Author is Required";
                }
                
            }
            if (columnTitle == "PublicationDate")
            {
                if (PublicationDate is null)
                {
                    return "Birth Date is Required";
                }
            }
            if (columnTitle == "Isbn")
            {
                if (PublicationDate is null)
                {
                    return "Isbn is Required";
                }
            }
            if (columnTitle == "Genre")
            {
                if (PublicationDate is null)
                {
                    return "Genre is Required";
                }
            }
            if (columnTitle == "PlaceOfResidence")
            {
                if (PublicationDate is null)
                {
                    return "PlaceOfResidence is Required";
                }
            }
            if (columnTitle == "PostalCode")
            {
                if (PublicationDate is null)
                {
                    return "PostalCode is Required";
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

    private ObservableCollection<Subject>? _assignedSubjects = null;
    public ObservableCollection<Subject>? AssignedSubjects
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
    public ICommand? Back
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
    public ICommand? Save
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

        Book book = new Book
        {
            Title = this.Title,
            Author = this.Author,
            PublicationDate = this.PublicationDate,
            Isbn = this.Isbn,
            Genre = this.Genre,
            Description = this.Description,
            //Subjects = AssignedSubjects?.Where(s => s.IsSelected).ToList()
        };

        _context.Books.Add(book);
        _context.SaveChanges();

        Response = "Data Saved";
    }

    public AddBookViewModel(UniversityContext context, IDialogService dialogService)
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
        string[] properties = { "Title", "LastTitle", "Author", "BirthDay", "Isbn", "Genre", "PlaceOfResidence", "PostalCode" };
        foreach (string property in properties)
        {
            if (!string.IsNullOrEmpty(this[property]))
            {
                return false;
            }
        }
        return true;
    }
}
