using AvaloniaApplication1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModels
{
    public class AddressMVVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private string _addressString = string.Empty;
        private Guid _id;
        private string _city = string.Empty;
        private string _country = string.Empty;
        private string _street = string.Empty;
        private string _house = string.Empty;
        private string _room = string.Empty;

        public string AddressString { get => GetFullAddress(); private set { _addressString = value; OnPropertyChanged(); } }
        public string City { get => _city; set { _city = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string Country { get => _country; set { _country = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string Street { get => _street; set { _street = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string House { get => _house; set { _house = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string Room { get => _room; set { _room = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public Guid ID { get => _id; set { _id = value; OnPropertyChanged(); } }

        public AddressMVVM(string country, string city, string street, string house, string room)
        {
            Country = country;
            City = city;
            Street = street;
            House = house;
            Room = room;
            ID = Guid.NewGuid();
            AddressString = GetFullAddress();
        }
        public AddressMVVM()
        {
            ID = Guid.NewGuid();
        }

        public AddressMVVM(Address address)
        {
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            House = address.House;
            Room = address.Room;
            ID = address.ID;
            AddressString = address.AddressString;
        }


        private string GetFullAddress()
        {
            var fullAddress = string.Empty;

            if (!string.IsNullOrWhiteSpace(Country))
            {
                fullAddress = $"{Country}";
            }
            if (!string.IsNullOrWhiteSpace(City))
            {
                fullAddress += $", {City}".Trim(' ', ',');
            }
            if (string.IsNullOrWhiteSpace(Street))
            {
                fullAddress += $", {Street}".Trim(' ', ',');
            }
            if (string.IsNullOrWhiteSpace(House))
            {
                fullAddress += $", {House}".Trim(' ', ',');
            }
            if (string.IsNullOrWhiteSpace(Room))
            {
                fullAddress += $", {Room}".Trim(' ', ',');
            }

            return fullAddress;
        }
    }
}
