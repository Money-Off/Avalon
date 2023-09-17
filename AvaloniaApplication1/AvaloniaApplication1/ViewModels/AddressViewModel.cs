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
        public Address Address { get; set; } = null;
        public Address ResultAddress { get; set; } = null;

        public AddressViewModel(Address address)
        {
            Address = address;
            ConfirmAddressCommand = ReactiveCommand.Create(ConfirmAddress);
        }

        public ICommand ConfirmAddressCommand { get; }

        private void ConfirmAddress()
        {
            //Employee.AddAddress(EditedAddress);
            ResultAddress = Address;
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
