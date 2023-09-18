using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace AvaloniaApplication1.ViewModels
{
    public class MVVMSample : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private Employee _employee = new Employee();
        private ObservableCollection<Address> _addresses = new ObservableCollection<Address>();

        private Address _selectedAddress = new Address();

        private Address _editedAddress = new Address();
        public Address SelectedAddress { get => _selectedAddress; set { _selectedAddress = value; OnPropertyChanged(nameof(SelectedAddress)); EditedAddress = value; } }
        public Address EditedAddress { get => _editedAddress; set { _editedAddress = value; OnPropertyChanged(nameof(EditedAddress)); } }

        public ObservableCollection<Address> Addresses
        {
            get
            {
                return Employee.Addresses;
            }
            set
            {
                Employee.Addresses = value;
                OnPropertyChanged(nameof(Addresses));
                OnPropertyChanged(nameof(Employee));
            }
        }



        public Employee Employee { get => _employee; set { _employee = value; OnPropertyChanged(nameof(Employee)); } }

        public MVVMSample()
        {
            Employee = new Employee();

            OpenAddressCommand = ReactiveCommand.Create(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);

        }

        public MVVMSample(Employee employee, ObservableCollection<Address> addresses, Address selectedAddress)
        {
            Employee = employee;
            Addresses = addresses;
            SelectedAddress = selectedAddress;
            OpenAddressCommand = ReactiveCommand.Create(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
        }

        public MVVMSample(Employee employee)
        {
            Employee = employee;
            OpenAddressCommand = ReactiveCommand.Create(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
        }


        public ICommand OpenAddressCommand { get; }

        private void OpenAddress()
        {
            //Employee.AddAddress(EditedAddress);
            var addressWindow = new AddressWindow();
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.MainWindow is Window mainWindow)
                {
                    var addressViewModel = new AddressViewModel(SelectedAddress);
                    addressWindow.DataContext = addressViewModel;
                    addressWindow.ShowDialog(mainWindow);
                    if (addressViewModel.ResultAddress is Address resultAddress)
                    {
                        Employee.AddAddress(resultAddress);
                    }
                    
                }
            }
        }


        public ICommand CreateXMLCommand { get; }

        private void CreateXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));

            using (FileStream fs = new FileStream("employer.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, Employee);
               
            }
            using (StreamReader reader = new StreamReader("employer.xml"))
            {
                var a = reader.ReadToEnd();
            }
        }

        public ICommand ReadXMLCommand { get; }

        private async void ReadXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.MainWindow is Window mainWindow)
                {
                    var topLevel = TopLevel.GetTopLevel(mainWindow);

                    var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                    {
                        Title = "Выберете файл",
                        AllowMultiple = false
                    });

                    if(files.Count>=1)
                    {
                        using (FileStream fs = new FileStream(files[0].Name, FileMode.OpenOrCreate))
                        {
                            //    await using var stream = await files[0].OpenReadAsync();
                            //using var streamReader = new StreamReader(stream);
                            //var fileContent = await streamReader.ReadToEndAsync();
                            Employee? employeeFromXML = serializer.Deserialize(fs) as Employee;

                            if (employeeFromXML != null)
                            {
                                Employee = employeeFromXML;
                            }
                        }
                    }
                }
            }
        }

        //public event PropertyChangedEventHandler? PropertyChanged;

        //private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private string resultString;
        //public string ResultString
        //{
        //    get
        //    {
        //        var first = string.Empty;
        //        var second = string.Empty;
        //        var middle = string.Empty;
        //        var address = string.Empty;
        //        if (LastName != null && LastName != string.Empty)
        //            second = LastName;
        //        if (FirstName != null && FirstName != string.Empty)
        //            first = $" {FirstName.FirstOrDefault()}.";
        //        if (MiddleName != null && MiddleName != string.Empty)
        //            middle = $" {MiddleName.FirstOrDefault()}.";
        //        if (Address != null && Address != string.Empty)
        //            address = $" проживает по адресу: {Address}.";

        //        var _result = $"{second}{first}{middle}{address}";

        //        return _result;
        //    }
        //    private set
        //    {
        //        resultString = value; RaisePropertyChanged(nameof(ResultString));
        //    }
        //}

        //private string? firstName;

        //public string? FirstName
        //{
        //    get
        //    {
        //        return firstName;
        //    }
        //    set
        //    {
        //        if (firstName != value)
        //        {
        //            firstName = value; RaisePropertyChanged();
        //            RaisePropertyChanged(nameof(ResultString));
        //        }
        //    }
        //}
        //private string? lastName;

        //public string? LastName
        //{
        //    get
        //    {
        //        return lastName;
        //    }
        //    set
        //    {
        //        if (lastName != value)
        //        {
        //            lastName = value; RaisePropertyChanged();
        //            RaisePropertyChanged(nameof(ResultString));
        //        }
        //    }
        //}


        //private string? middleName;

        //public string? MiddleName
        //{
        //    get
        //    {
        //        return middleName;
        //    }
        //    set
        //    {
        //        if (middleName != value)
        //        {
        //            middleName = value; RaisePropertyChanged();
        //            RaisePropertyChanged(nameof(ResultString));
        //        }
        //    }
        //}


        //private string? address;

        //public string? Address
        //{
        //    get
        //    {
        //        return address;
        //    }
        //    set
        //    {
        //        if (address != value)
        //        {
        //            address = value; RaisePropertyChanged();
        //            RaisePropertyChanged(nameof(ResultString));
        //        }
        //    }
        //}


    }
}
