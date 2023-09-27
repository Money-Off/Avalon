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

        private Address? _editedAddress = null;
        public Address? SelectedAddress { get => _selectedAddress; set { _selectedAddress = value; OnPropertyChanged(nameof(SelectedAddress)); OnPropertyChanged(nameof(IsAddressSelected)); EditedAddress = value; } }
        public Address? EditedAddress { get => _editedAddress; set { _editedAddress = value; OnPropertyChanged(nameof(EditedAddress)); } }
        public bool IsAddressSelected { get => SelectedAddress is Address; }
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



        public Employee Employee { get => _employee; set { _employee = value; OnPropertyChanged(nameof(Employee)); OnPropertyChanged(nameof(Addresses));OnPropertyChanged(nameof(SelectedSex)); } }

        public MVVMSample()
        {
            Employee = new Employee();

            OpenAddressCommand = ReactiveCommand.Create<bool>(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
            DeleteAddressCommand = ReactiveCommand.Create(DeleteAddress);

        }

        public MVVMSample(Employee employee, ObservableCollection<Address> addresses, Address selectedAddress)
        {
            Employee = employee;
            Addresses = addresses;
            SelectedAddress = selectedAddress;
            OpenAddressCommand = ReactiveCommand.Create<bool>(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
            DeleteAddressCommand = ReactiveCommand.Create(DeleteAddress);
        }

        public MVVMSample(Employee employee)
        {
            Employee = employee;
            OpenAddressCommand = ReactiveCommand.Create<bool>(OpenAddress);
            CreateXMLCommand = ReactiveCommand.Create(CreateXML);
            ReadXMLCommand = ReactiveCommand.Create(ReadXML);
            DeleteAddressCommand = ReactiveCommand.Create(DeleteAddress);
        }

       

        public ReactiveCommand<bool,Unit> OpenAddressCommand { get; }

        private void OpenAddress(bool IsSelected = true)
        {
            //Employee.AddAddress(EditedAddress);
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
        public ICommand DeleteAddressCommand { get; }

        private void DeleteAddress()
        {
            if(SelectedAddress != null)
            {
                Employee.DeleteAddress(SelectedAddress);
            }
            SelectedAddress = null;
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
