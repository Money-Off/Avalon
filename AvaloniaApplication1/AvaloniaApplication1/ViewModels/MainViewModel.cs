using AvaloniaApplication1.Models;
using AvaloniaApplication1.Views;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Linq;
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

    //public MainViewModel()
    //{
    //    ShowDialog = new Interaction<MVVMSample, AddressViewModel?>();
    //    OpenAddressCommand = ReactiveCommand.CreateFromTask(async () =>
    //    {
    //        var newMVVM = new MVVMSample();
    //        var result = await ShowDialog.Handle(newMVVM)
    //        });
    //    OpenAddressCommand = ReactiveCommand.Create(OpenAddress);
    //}
    //public Interaction<MVVMSample, AddressViewModel?> ShowDialog { get; }
}
