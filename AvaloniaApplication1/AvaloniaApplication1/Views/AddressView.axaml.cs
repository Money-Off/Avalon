using Avalonia.Controls;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1.Views
{
    public partial class AddressView : UserControl
    {
        
        public AddressView()
        {
            InitializeComponent();
        }

        public AddressView(Address address)
        {
            
            InitializeComponent();
        }
    }
}
