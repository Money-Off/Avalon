using AvaloniaApplication1.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public MVVMSample MVVMSample { get; } = new MVVMSample();
    //public Employee Employee { get;} = new Employee();
}
