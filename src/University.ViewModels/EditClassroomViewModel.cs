using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class EditClassroomViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;
    private Classroom? _classroom = new Classroom();

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            if (columnName == "Location")
            {
                if (string.IsNullOrEmpty(Location))
                {
                    return "Location is Required";
                }
            }
            if (columnName == "Description")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "Description is Required";
                }
            }
            if (columnName == "Capacity")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "Capacity is Required";
                }
            }
            if (columnName == "AvailableSeats")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "AvailableSeats is Required";
                }
            }
            if (columnName == "Projector")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "Projector is Required";
                }
            }
            if (columnName == "Whiteboard")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "WhiteBoard is Required";
                }
            }
            if (columnName == "Microphone")
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return "Microphone is Required";
                }
            }
            return string.Empty;
        }
    }

    private string _location = string.Empty;
    public string Location
    {
        get
        {
            return _location;
        }
        set
        {
            _location = value;
            OnPropertyChanged(nameof(Location));
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
    private int _capacity = 0;
    public int Capacity
    {
        get
        {
            return _capacity;
        }
        set
        {
            _capacity = value;
            OnPropertyChanged(nameof(Capacity));
        }
    }
    private int _availableSeats = 0;
    public int AvailableSeats
    {
        get
        {
            return _availableSeats;
        }
        set
        {
            _availableSeats = value;
            OnPropertyChanged(nameof(AvailableSeats));
        }
    }
    private bool _projector = false;
    public bool Projector
    {
        get
        {
            return _projector;
        }
        set
        {
            _projector = value;
            OnPropertyChanged(nameof(Projector));
        }
    }
    private bool _whiteboard = false;
    public bool Whiteboard
    {
        get
        {
            return _whiteboard;
        }
        set
        {
            _whiteboard = value;
            OnPropertyChanged(nameof(Whiteboard));
        }
    }
    private bool _microphone = false;
    public bool Microphone
    {
        get
        {
            return _microphone;
        }
        set
        {
            _microphone = value;
            OnPropertyChanged(nameof(Microphone));
        }
    }
    private string _classroomId = string.Empty;
    public string ClassroomId
    {
        get
        {
            return _classroomId;
        }
        set
        {
            _classroomId = value;
            LoadClassroomData();
            OnPropertyChanged(nameof(ClassroomId));
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

    private ObservableCollection<Student>? _availableStudents = null;
    public ObservableCollection<Student> AvailableStudents
    {
        get
        {
            if (_availableStudents is null)
            {
                _availableStudents = LoadStudents();
                return _availableStudents;
            }
            return _availableStudents;
        }
        set
        {
            _availableStudents = value;
            OnPropertyChanged(nameof(AvailableStudents));
        }
    }

    private ObservableCollection<Student>? _assignedStudents = null;
    public ObservableCollection<Student>? AssignedStudents
    {
        get
        {
            if (_assignedStudents is null)
            {
                _assignedStudents = new ObservableCollection<Student>();
                return _assignedStudents;
            }
            return _assignedStudents;
        }
        set
        {
            _assignedStudents = value;
            OnPropertyChanged(nameof(AssignedStudents));
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
            instance.ClassroomsSubView = new ClassroomsViewModel(_context, _dialogService);
        }
    }

    private ICommand? _add;
    public ICommand Add
    {
        get
        {
            if (_add is null)
            {
                _add = new RelayCommand<object>(AddStudent);
            }
            return _add;
        }
    }

    private void AddStudent(object? obj)
    {
        if (obj is Student student)
        {
            if (AssignedStudents is not null && !AssignedStudents.Contains(student))
            {
                AssignedStudents.Add(student);
            }
        }
    }

    private ICommand? _remove = null;
    public ICommand? Remove
    {
        get
        {
            if (_remove is null)
            {
                _remove = new RelayCommand<object>(RemoveStudent);
            }
            return _remove;
        }
    }

    private void RemoveStudent(object? obj)
    {
        if (obj is Student student)
        {
            if (AssignedStudents is not null)
            {
                AssignedStudents.Remove(student);
            }
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

        if (_classroom is null)
        {
            return;
        }

        _classroom.Location = Location;
        _classroom.Capacity = Capacity;
        _classroom.Description = Description;
        _classroom.AvailableSeats = AvailableSeats;
        _classroom.Projector = Projector;
        _classroom.Whiteboard = Whiteboard;
        _classroom.Microphone = Microphone;


        _context.Entry(_classroom).State = EntityState.Modified;
        _context.SaveChanges();

        Response = "Data Saved";
    }

    public EditClassroomViewModel(UniversityContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    private ObservableCollection<Student> LoadStudents()
    {
        _context.Database.EnsureCreated();
        _context.Students.Load();
        return _context.Students.Local.ToObservableCollection();
    }

    private bool IsValid()
    {
        string[] properties = { "Name", "Semester", "Lecturer" };
        foreach (string property in properties)
        {
            if (!string.IsNullOrEmpty(this[property]))
            {
                return false;
            }
        }
        return true;
    }

    private void LoadClassroomData()
    {
        var classrooms = _context.Classrooms;
        if (classrooms is not null)
        {
            _classroom = classrooms.Find(ClassroomId);
            if (_classroom is null)
            {
                return;
            }
            this.Location = _classroom.Location;
            this.Capacity = _classroom.Capacity;
            this.Description = _classroom.Description;
            this.AvailableSeats = _classroom.AvailableSeats;
            this.Projector = _classroom.Projector;
            this.Whiteboard = _classroom.Whiteboard;
            this.Microphone = _classroom.Microphone;

            /*
            if (_classroom.Students is not null)
            {
                this.AssignedStudents =
                    new ObservableCollection<Student>(_classroom.Students);
            }
            */
        }
    }
}
