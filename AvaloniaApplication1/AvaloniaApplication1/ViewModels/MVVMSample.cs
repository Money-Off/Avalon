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
