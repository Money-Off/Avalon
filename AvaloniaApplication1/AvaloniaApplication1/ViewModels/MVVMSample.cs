using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace AvaloniaApplication1.ViewModels
{
    public class MVVMSample : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string resultString;
        public string ResultString
        {
            get
            {
                var first = string.Empty;
                var second = string.Empty;
                var middle = string.Empty;
                var address = string.Empty;
                if(LastName != null && LastName != string.Empty)
                    second = LastName;
                if(FirstName!= null && FirstName != string.Empty)
                    first = $" {FirstName.FirstOrDefault()}.";
                if(MiddleName != null && MiddleName != string.Empty)
                    middle = $" {MiddleName.FirstOrDefault()}.";
                if(Address != null && Address != string.Empty)
                    address = $" проживает по адресу: {Address}.";

                var _result = $"{second}{first}{middle}{address}";

                return _result; 
            }
            //private set
            //{
            //    resultString = value; RaisePropertyChanged(nameof(ResultString));
            //}
        }

        private string? firstName;

        public string? FirstName { 
            get
            {
                return firstName;
            }
            set
            {
                if(firstName != value)
                {
                    firstName = value; RaisePropertyChanged();
                    RaisePropertyChanged(nameof(ResultString));
                }
            }
        }
        private string? lastName;

        public string? LastName { 
            get
            {
                return lastName;
            }
            set
            {
                if(lastName != value)
                {
                    lastName = value; RaisePropertyChanged();
                    RaisePropertyChanged(nameof(ResultString));
                }
            }
        }

        
        private string? middleName;

        public string? MiddleName { 
            get
            {
                return middleName;
            }
            set
            {
                if(middleName != value)
                {
                    middleName = value; RaisePropertyChanged();
                    RaisePropertyChanged(nameof(ResultString));
                }
            }
        }
        
        
        private string? address;

        public string? Address { 
            get
            {
                return address;
            }
            set
            {
                if(address != value)
                {
                    address = value; RaisePropertyChanged();
                    RaisePropertyChanged(nameof(ResultString));
                }
            }
        }


    }
}
