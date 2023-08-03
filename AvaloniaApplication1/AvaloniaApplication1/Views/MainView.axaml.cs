using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace AvaloniaApplication1.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public void ButtonClicked(object source, RoutedEventArgs args)
    {
        if (double.TryParse(celsius.Text, out double c))
        {
            var f = c*(9d / 5d) + 32;
            fahrenheit.Text = f.ToString("0.0");
        }
        else
        {
            celsius.Text = "0";
            fahrenheit.Text = "0";
        }
        Debug.WriteLine($"Click! Celsius = {celsius.Text}");
    }


}
