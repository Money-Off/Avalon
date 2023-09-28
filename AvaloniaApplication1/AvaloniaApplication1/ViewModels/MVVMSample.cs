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
using System.Reactive;
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

        private ObservableCollection<string> _sexes = new ObservableCollection<string>() { "мужской","женский"};

        private Address? _selectedAddress = null;
        public Address? SelectedAddress { get => _selectedAddress; set { _selectedAddress = value; OnPropertyChanged(nameof(SelectedAddress)); OnPropertyChanged(nameof(IsAddressSelected)); } }
        
        private Phone? _selectedPhone = null;
        public Phone? SelectedPhone { get => _selectedPhone; set { _selectedPhone = value; OnPropertyChanged(nameof(SelectedPhone)); OnPropertyChanged(nameof(IsPhoneSelected)); } }
        public bool IsAddressSelected { get => SelectedAddress is Address; }
        public bool IsPhoneSelected { get => SelectedPhone is Phone; }
        public string SelectedSex { get => Employee.Sex; set { Employee.Sex = value; OnPropertyChanged(nameof(SelectedSex)); } }

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
        public ObservableCollection<Phone> Phones
        {
            get
            {
                return Employee.Phones;
            }
            set
            {
                Employee.Phones = value;
                OnPropertyChanged(nameof(Phones));
                OnPropertyChanged(nameof(Employee));
            }
        }
        public ObservableCollection<string> Sexes
        {
            get
            {
                return _sexes;
            }
            set
            {                    
                _sexes = value;
            }
        }



        public Employee Employee 
        { 
            get => _employee; 
            set 
            { 
                _employee = value; 
                OnPropertyChanged(nameof(Employee)); 
                OnPropertyChanged(nameof(Addresses));
                OnPropertyChanged(nameof(Phones));
                OnPropertyChanged(nameof(SelectedSex)); } 
            }

        public MVVMSample()
        {
            Employee = new Employee();

            OpenAddressCommand = ReactiveCommand.Create<bool>(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
            DeleteAddressCommand = ReactiveCommand.Create(DeleteAddress);
            OpenPhoneCommand = ReactiveCommand.Create<bool>(OpenPhone);
            DeletePhoneCommand = ReactiveCommand.Create(DeletePhone);

        }

        public MVVMSample(Employee employee, ObservableCollection<Address> addresses, Address selectedAddress,ObservableCollection<Phone> phones, Phone selectedPhone)
        {
            Employee = employee;
            Addresses = addresses;
            SelectedAddress = selectedAddress;
            Phones = phones;
            SelectedPhone = selectedPhone;
            OpenAddressCommand = ReactiveCommand.Create<bool>(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
            DeleteAddressCommand = ReactiveCommand.Create(DeleteAddress);
            OpenPhoneCommand = ReactiveCommand.Create<bool>(OpenPhone);
            DeletePhoneCommand = ReactiveCommand.Create(DeletePhone);
        }

        public MVVMSample(Employee employee)
        {
            Employee = employee;
            OpenAddressCommand = ReactiveCommand.Create<bool>(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
            DeleteAddressCommand = ReactiveCommand.Create(DeleteAddress);
            OpenPhoneCommand = ReactiveCommand.Create<bool>(OpenPhone);
            DeletePhoneCommand = ReactiveCommand.Create(DeletePhone);
        }

       

        public ReactiveCommand<bool,Unit> OpenAddressCommand { get; }

        private void OpenAddress(bool IsSelected = true)
        {
            var addressWindow = new AddressWindow();
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.MainWindow is Window mainWindow)
                {
                    var selectedAddress = new Address();
                    if(IsSelected)
                    {
                        selectedAddress = SelectedAddress;
                    }
                    var addressViewModel = new AddressViewModel(selectedAddress, Employee);
                    addressWindow.DataContext = addressViewModel;
                    addressWindow.ShowDialog(mainWindow);
                    
                    
                }
            }
        }
        public ReactiveCommand<bool,Unit> OpenPhoneCommand { get; }

        private void OpenPhone(bool IsSelected = true)
        {
            var phoneWindow = new PhoneWindow();
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.MainWindow is Window mainWindow)
                {
                    var selectedPhone = new Phone();
                    if(IsSelected)
                    {
                        selectedPhone = SelectedPhone;
                    }
                    var phoneViewModel = new PhoneViewModel(selectedPhone, Employee);
                    phoneWindow.DataContext = phoneViewModel;
                    phoneWindow.ShowDialog(mainWindow);
                    
                    
                }
            }
        }
        public ICommand DeleteAddressCommand { get; }

        private void DeleteAddress()
        {
            if(SelectedAddress != null)
            {
                Employee.DeleteAddress(SelectedAddress);
            }
            SelectedAddress = null;
        }
        public ICommand DeletePhoneCommand { get; }

        private void DeletePhone()
        {
            if(SelectedPhone != null)
            {
                Employee.DeletePhone(SelectedPhone);
            }
            SelectedPhone = null;
        }

        public ICommand CreateXMLCommand { get; }

        private void CreateXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));

            using (FileStream fs = new FileStream("employer.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, Employee);
               
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


    }
}
