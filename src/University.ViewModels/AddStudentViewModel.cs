﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using University.Data;
using University.Extensions;
using University.Interfaces;
using University.Models;

namespace University.ViewModels;

public class AddStudentViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly UniversityContext _context;
    private readonly IDialogService _dialogService;

    public string Error
    {
        get { return string.Empty; }
    }

    public string this[string columnName]
    {
        get
        {
            if (columnName == "Name")
            {
                if (string.IsNullOrEmpty(Name))
                { 
                    return "Name is Required";
                }
                if (!Name.IsNameValid())
                {
                    return "Name is Invalid";
                }
            }
            if (columnName == "LastName")
            {
                if (string.IsNullOrEmpty(LastName))
                {
                    return "Last Name is Required";
                }
                if (!LastName.IsNameValid())
                {
                    return "LastName is Invalid";
                }
            }
            if (columnName == "PESEL")
            {
                if (string.IsNullOrEmpty(PESEL))
                {
                    return "PESEL is Required";
                }
                if (!PESEL.IsValidPESEL())
                {
                    return "PESEL is Invalid";
                }
            }
            if (columnName == "BirthDate")
            {
                if (BirthDate is null)
                {
                    return "Birth Date is Required";
                }
                
            }

            if (columnName == "Gender")
            {
                if (string.IsNullOrEmpty(Gender))
                {
                    return "Gender is Required";
                }
            }
            if (columnName == "PlaceOfBirth")
            {
                if (string.IsNullOrEmpty(PlaceOfBirth))
                {
                    return "PlaceOfBirth is Required";
                }
            }
            if (columnName == "PlaceOfResidence")
            {
                if (string.IsNullOrEmpty(PlaceOfResidence))
                {
                    return "PlaceOfResidence is Required";
                }
            }
            if (columnName == "PostalCode")
            {
                if (string.IsNullOrEmpty(PostalCode))
                {
                    return "PostalCode is Required";
                }
            }
            return string.Empty;
            


        }
    }

    private string _name = string.Empty;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get
        {
            return _lastName;
        }
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    private string _pesel = string.Empty;
    public string PESEL
    {
        get
        {
            return _pesel;
        }
        set
        {
            _pesel = value;
            OnPropertyChanged(nameof(PESEL));
        }
    }

    private DateTime? _birthDate = null;
    public DateTime? BirthDate
    {
        get
        {
            return _birthDate;
        }
        set
        {
            _birthDate = value;
            OnPropertyChanged(nameof(BirthDate));
        }
    }
    private string _gender = string.Empty;
    public string Gender
    {
        get
        {
            return _gender;
        }
        set
        {
            _gender = value;
            OnPropertyChanged(nameof(Gender));
        }
    }
    private string _placeOfBirth = string.Empty;
    public string PlaceOfBirth
    {
        get
        {
            return _placeOfBirth;
        }
        set
        {
            _placeOfBirth = value;
            OnPropertyChanged(nameof(PlaceOfBirth));
        }
    }
    private string _placeOfResidence = string.Empty;
    public string PlaceOfResidence
    {
        get
        {
            return _placeOfResidence;
        }
        set
        {
            _placeOfResidence = value;
            OnPropertyChanged(nameof(PlaceOfResidence));
        }
    }
    private string _postalCode = string.Empty;
    public string PostalCode
    {
        get
        {
            return _postalCode;
        }
        set
        {
            _postalCode = value;
            OnPropertyChanged(nameof(PostalCode));
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
            instance.StudentsSubView = new StudentsViewModel(_context, _dialogService);
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

        Student student = new Student
        {
            Name = this.Name,
            LastName = this.LastName,
            PESEL = this.PESEL,
            BirthDate = this.BirthDate,
            Gender = this.Gender,
            PlaceOfBirth = this.PlaceOfBirth,
            PlaceOfResidence = this.PlaceOfResidence,
            PostalCode = this.PostalCode,
            Subjects = AssignedSubjects?.Where(s => s.IsSelected).ToList()
        };

        _context.Students.Add(student);
        _context.SaveChanges();

        Response = "Data Saved";
    }

    public AddStudentViewModel(UniversityContext context, IDialogService dialogService)
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
        string[] properties = { "Name", "LastName", "PESEL", "BirthDay", "Gender", "PlaceOfBirth", "PlaceOfResidence", "PostalCode" };
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
