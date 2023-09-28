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
        private string _sex = "мужской";
        private string _inn = string.Empty;
        private string _snils = string.Empty;
        private string _email = string.Empty;

        private BirthInfo _birthInfo = new BirthInfo();

        private ObservableCollection<Address> _addresses = new ObservableCollection<Address>();
        private ObservableCollection<Phone> _phones = new ObservableCollection<Phone>();

        public Guid ID { get => _id; set { _id = value; OnPropertyChanged(nameof(ID));  } }
        public string FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(ResultString)); } }
        public string LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(ResultString)); } }
        public string MiddleName { get => _middleName; set { _middleName = value; OnPropertyChanged(nameof(MiddleName)); OnPropertyChanged(nameof(ResultString)); } }
        public string Sex { get => _sex; set { _sex = value; OnPropertyChanged(nameof(Sex)); } }
        public string INN { get => _inn; set { _inn = value; OnPropertyChanged(nameof(INN)); } }
        public string Snils { get => _snils; set { _snils = value; OnPropertyChanged(nameof(Snils)); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }
        
        public ObservableCollection<Address> Addresses { get => _addresses; set { _addresses = value; OnPropertyChanged(nameof(Addresses)); OnPropertyChanged(nameof(ResultString)); } }

        public ObservableCollection<Phone> Phones { get => _phones; set { _phones = value; OnPropertyChanged(nameof(Phones)); } }
        public BirthInfo BirthInfoAttr { get => _birthInfo;set { _birthInfo = value;OnPropertyChanged(nameof(BirthInfoAttr)); } }
        
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
            _id = Guid.NewGuid();
        }

        public void AddAddress(Address address)
        {
            var addresses = Addresses;
            addresses.Add(address);
            Addresses = addresses;
            OnPropertyChanged();
        }
        public void AddPhone(Phone phone)
        {
            var phones = Phones;
            phones.Add(phone);
            Phones = phones;
            OnPropertyChanged();
        }

        public void DeleteAddress(Address address)
        {
            var addresses = Addresses;
            addresses.Remove(address);
            Addresses = addresses;
            OnPropertyChanged();
        }

        public void DeletePhone(Phone phone)
        {
            var phones = Phones;
            phones.Remove(phone);
            Phones = phones;
            OnPropertyChanged();
        }
    }
}
