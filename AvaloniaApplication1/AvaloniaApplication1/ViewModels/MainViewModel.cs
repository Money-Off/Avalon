using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using System.Collections.Generic;
using System.Windows.Input;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public MVVMSample MVVMSample { get; } = new MVVMSample();
    //public Employee Employee { get;} = new Employee();


    public ICommand OpenAddressCommand { get; }

    private void OpenAddress()
    {
        var addressWindow = new AddressWindow();
        addressWindow.Show();
    }

    public MainViewModel()
    {
        OpenAddressCommand = ReactiveCommand.Create(OpenAddress);
    }
}
