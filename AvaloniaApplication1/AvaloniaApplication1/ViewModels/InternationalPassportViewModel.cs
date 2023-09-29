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
    public class InternationalPassportViewModel : ViewModelBase
    {
        public InternationalPassport Passport { get; set; }
        public InternationalPassport ResultPassport { get; set; }

        public Employee Employee { get; set; }

        public InternationalPassportViewModel(InternationalPassport passport, Employee employee)
        {
            Passport = passport;
            ResultPassport = new InternationalPassport(Passport);

            Employee = employee;
            ConfirmInternationalPassportCommand = ReactiveCommand.Create(ConfirmInternationalPassport);
            CancelInternationalPassportCommand = ReactiveCommand.Create(CancelInternationalPassport);
        }

        public ICommand ConfirmInternationalPassportCommand { get; }

        private void ConfirmInternationalPassport()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {

                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is InternationalPassportViewModel) is Window internationalPassportWindow)
                {
                    var id = Passport.ID;
                    if (Employee.InternationalPassports.FirstOrDefault(o => o.ID == id) is InternationalPassport internationalPassportExisted)
                    {
                        Employee.DeleteElement(internationalPassportExisted, Employee.InternationalPassports);
                    }
                    Employee.AddElement(ResultPassport, Employee.InternationalPassports);
                    internationalPassportWindow.Close();
                }
            }
        }

        public ICommand CancelInternationalPassportCommand { get; }

        private void CancelInternationalPassport()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime deckstop)
            {
                if (deckstop.Windows.FirstOrDefault(o => o.DataContext is InternationalPassportViewModel) is Window internationalPassportWindow)
                {
                    internationalPassportWindow.Close();
                }
            }

        }
    }
}
