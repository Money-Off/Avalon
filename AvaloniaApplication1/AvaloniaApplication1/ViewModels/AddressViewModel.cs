using AvaloniaApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModels
{
    internal class AddressViewModel : ViewModelBase
    {
        public Address Address { get; set; } = null;

        public AddressViewModel(Address address)
        {
            Address = address;
        }
    }
}
