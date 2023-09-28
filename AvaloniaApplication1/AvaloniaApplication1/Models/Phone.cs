using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvaloniaApplication1.Models
{
    public class Phone : BaseClass
    {
        private string _id = string.Empty;
        private string _typeOfPhone = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _model = string.Empty;
        private string _provider = string.Empty;
                
        private ObservableCollection<string> _typesOfPhone = new ObservableCollection<string>() { "рабочий", "мобильный", "домашний" };
        [XmlIgnore]
        public string PhoneSring {  get => GetFullPhone(); }
        public string ID { get => _id; set { _id = value; OnPropertyChanged(nameof(ID)); } }
        public string Type { get => _typeOfPhone; set { _typeOfPhone = value; OnPropertyChanged(nameof(Type)); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); } }
        public string Model { get => _model; set { _model = value; OnPropertyChanged(nameof(Model)); } }
        public string Provider { get => _provider; set { _provider = value; OnPropertyChanged( nameof(Provider)); } }
        [XmlIgnore]
        public ObservableCollection<string> TypesOfPhone { get => _typesOfPhone; set { _typesOfPhone = value; } }

        public Phone() 
        { 
            _id = Guid.NewGuid().ToString();
        }

        public Phone( string id, string type, string phoneNumber, string model, string provider)
        {            
            ID = id;
            PhoneNumber = phoneNumber;
            Model = model;
            Provider = provider;
            Type = type;
        }

        public Phone( Phone phone )
        {
            ID = phone.ID;
            PhoneNumber = phone.PhoneNumber;
            Model = phone.Model;
            Provider = phone.Provider;
            Type = phone.Type;
        }

        private string GetFullPhone()
        {
            var fullPhone = string.Empty;

            if (!string.IsNullOrWhiteSpace(Type))
            {
                fullPhone = $"{Type}";
            }
            if (!string.IsNullOrWhiteSpace(PhoneNumber))
            {
                fullPhone += $", {PhoneNumber}";
                fullPhone = fullPhone.TrimStart(' ', ',');
            }
            if (!string.IsNullOrWhiteSpace(Model))
            {
                fullPhone += $", {Model}";
                fullPhone = fullPhone.TrimStart(' ', ',');
            }
            if (!string.IsNullOrWhiteSpace(Provider))
            {
                fullPhone += $", {Provider}";
                fullPhone = fullPhone.TrimStart(' ', ',');
            }

            return fullPhone;
        }
    }
}
