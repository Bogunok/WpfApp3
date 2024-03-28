using System.Windows;
using WpfApp3;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonViewModel pvm;
        public MainWindow()
        {
            InitializeComponent();
            pvm = new PersonViewModel(this);
            DataContext = pvm;
        }
    }
}
