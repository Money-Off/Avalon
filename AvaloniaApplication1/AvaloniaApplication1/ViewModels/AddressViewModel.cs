using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
    public class AddressViewModel : ViewModelBase
    {
        public Address Address { get; set; }
        //public Address? ResultAddress { get; set; } = null;

        public Employee Employee { get; set; }

        public AddressViewModel(Address address, Employee employee)
        {
            Address = address;
            Employee = employee;
            ConfirmAddressCommand = ReactiveCommand.Create(ConfirmAddress);
        }

        public ICommand ConfirmAddressCommand { get; }

        private void ConfirmAddress()
        {
            //Employee.AddAddress(EditedAddress);
            //ResultAddress = Address;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is AddressViewModel) is Window addressWindow)
                {
                    var id = Address.ID;
                    if(Employee.Addresses.FirstOrDefault(o=>o.ID == id) is Address addressExisted)
                    {
                        addressExisted = Address;
                    }
                    else
                    {
                        Employee.AddAddress(Address);
                    }
                    addressWindow.Close();
                }
            }
        }
    }
}
