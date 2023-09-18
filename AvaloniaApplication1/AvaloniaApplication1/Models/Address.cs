using AvaloniaApplication1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvaloniaApplication1.Models
{
    [Serializable]
    public class Address : BaseClass
    {
        private string _addressString = string.Empty;
        private Guid _id;
        private string _city = string.Empty;
        private string _country = string.Empty;
        private string _street = string.Empty;
        private string _house = string.Empty;
        private string _room = string.Empty;

        [XmlIgnore]
        public string AddressString { get => GetFullAddress(); private set { _addressString = value; OnPropertyChanged();  } }
        public string City { get => _city; set { _city = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string Country { get => _country; set { _country = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); Console.WriteLine(value); } }
        public string Street { get => _street; set { _street = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string House { get => _house; set { _house = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public string Room { get => _room; set { _room = value; OnPropertyChanged(); OnPropertyChanged(nameof(AddressString)); } }
        public Guid ID { get => _id; set { _id = value; OnPropertyChanged(); } }


        public Address(string country, string city, string street, string house, string room)
        { 
            Country = country;
            City = city;
            Street = street;
            House = house;
            Room = room;
            ID = Guid.NewGuid();
            AddressString = GetFullAddress();
        }
        public Address()
        { 
            ID = Guid.NewGuid(); 
        }

        public Address(AddressMVVM addressMVVM)
        {
            Country = addressMVVM.Country;
            City = addressMVVM.City;
            Street = addressMVVM.Street;
            House = addressMVVM.House;
            Room = addressMVVM.Room;
            ID = Guid.NewGuid();
            AddressString = GetFullAddress();

        }
        public Address(Address address)
        {
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            House = address.House;
            Room = address.Room;
            ID = address.ID;
            AddressString = GetFullAddress();

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
                fullAddress += $", {City}";//.TrimStart(' ',',');
            }
            if (!string.IsNullOrWhiteSpace(Street))
            {
                fullAddress += $", {Street}";//.TrimStart(' ', ',');
            }
            if (!string.IsNullOrWhiteSpace(House))
            {
                fullAddress += $", {House}";//.TrimStart(' ', ',');
            }
            if (!string.IsNullOrWhiteSpace(Room))
            {
                fullAddress += $", {Room}";//.TrimStart(' ', ',');
            }

            return fullAddress;
        }
    }
}
