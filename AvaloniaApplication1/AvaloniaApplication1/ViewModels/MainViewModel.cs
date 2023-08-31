namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public MVVMSample MVVMSample { get; } = new MVVMSample();
}
