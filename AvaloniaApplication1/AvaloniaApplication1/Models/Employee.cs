using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    [Serializable]
    public class Employee : BaseClass
    {
        private Guid _id;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _middleName = string.Empty;

        private ObservableCollection<Address> _addresses = new ObservableCollection<Address>();

        public Guid ID { get => _id; set { _id = value; OnPropertyChanged(nameof(ID));  } }
        public string FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(ResultString)); } }
        public string LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(ResultString)); } }
        public string MiddleName { get => _middleName; set { _middleName = value; OnPropertyChanged(nameof(MiddleName)); OnPropertyChanged(nameof(ResultString)); } }
        public ObservableCollection<Address> Addresses { get => _addresses; set { _addresses = value; OnPropertyChanged(nameof(Addresses)); OnPropertyChanged(nameof(ResultString)); } }

        public string ResultString
        {
            get
            {
                var first = string.Empty;
                var second = string.Empty;
                var middle = string.Empty;
                var address = string.Empty;
                if (LastName != null && LastName != string.Empty)
                    second = LastName;
                if (FirstName != null && FirstName != string.Empty)
                    first = $" {FirstName.FirstOrDefault()}.";
                if (MiddleName != null && MiddleName != string.Empty)
                    middle = $" {MiddleName.FirstOrDefault()}.";
                if (Addresses != null && Addresses.Count != 0)
                    address = $" проживает по адресу: {Addresses.FirstOrDefault().AddressString}.";

                var _result = $"{second}{first}{middle}{address}";

                return _result;
            }
        }

        public Employee(string lastName, string firstName, string middleName, ObservableCollection<Address> addresses = null) 
        { 
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Addresses = addresses;
            _id = Guid.NewGuid();
        }
        public Employee() 
        { 
            _addresses = new ObservableCollection<Address>() { new Address("HH","drgdg","drgyydry","drgdgdrg",""),new Address("dfgdg","dfgdg","dfgdg","dfgdg","dfgdg")};
            _id = Guid.NewGuid();
        }

        public void AddAddress(Address address)
        {
            var addresses = Addresses;
            addresses.Add(address);
            Addresses = addresses;
            OnPropertyChanged();
        }

        public void DeleteAddress(Address address)
        {
            var addresses = Addresses;
            addresses.Remove(address);
            Addresses = addresses;
            OnPropertyChanged();
        }
    }
}
