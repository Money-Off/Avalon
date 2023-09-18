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
        public Address ResultAddress { get; set; }

        public Employee Employee { get; set; }

        public AddressViewModel(Address address, Employee employee)
        {
            Address = address;
            ResultAddress = new Address(Address);

            Employee = employee;
            ConfirmAddressCommand = ReactiveCommand.Create(ConfirmAddress);
            CancelAddressCommand = ReactiveCommand.Create(CancelAddress);
        }

        public ICommand ConfirmAddressCommand { get; }

        private void ConfirmAddress()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {

                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is AddressViewModel) is Window addressWindow)
                {
                    var id = Address.ID;
                    if (Employee.Addresses.FirstOrDefault(o => o.ID == id) is Address addressExisted)
                    {
                        Employee.DeleteAddress(addressExisted);
                    }                    
                        Employee.AddAddress(ResultAddress);
                    addressWindow.Close();
                }
            }
        }

        public ICommand CancelAddressCommand { get; }

        private void CancelAddress()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is AddressViewModel) is Window addressWindow)
                {
                    addressWindow.Close();
                }
            }
        }
    }
}
