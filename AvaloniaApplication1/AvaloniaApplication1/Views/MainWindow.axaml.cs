using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using AvaloniaApplication1.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //private async Task DoShowDial

}