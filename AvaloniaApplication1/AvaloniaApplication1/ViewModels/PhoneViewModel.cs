using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using AvaloniaApplication1.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;

namespace AvaloniaApplication1.ViewModels
{
    public class PhoneViewModel : ViewModelBase
    {
        public Phone Phone { get; set; }
        public Phone ResultPhone { get; set; }

        public Employee Employee { get; set; }

        public PhoneViewModel(Phone phone, Employee employee)
        {
            Phone = phone;
            ResultPhone = new Phone(Phone);

            Employee = employee;
            ConfirmPhoneCommand = ReactiveCommand.Create(ConfirmPhone);
            CancelPhoneCommand = ReactiveCommand.Create(CancelPhone);
        }

        public ICommand ConfirmPhoneCommand { get; }

        private void ConfirmPhone()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {

                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is PhoneViewModel) is Window phoneWindow)
                {
                    var id = Phone.ID;
                    if (Employee.Phones.FirstOrDefault(o => o.ID == id) is Phone phoneExisted)
                    {
                        Employee.DeletePhone(phoneExisted);
                    }
                    Employee.AddPhone(ResultPhone);
                    phoneWindow.Close();
                }
            }
        }

        public ICommand CancelPhoneCommand { get; }

        private void CancelPhone()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is PhoneViewModel) is Window phoneWindow)
                {
                    phoneWindow.Close();
                }
            }
        }
    }
}

