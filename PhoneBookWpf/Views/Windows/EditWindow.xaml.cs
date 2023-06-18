using PhoneBookWpf.Models;
using PhoneBookWpf.ViewModels;
using System.Windows;

namespace PhoneBookWpf.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow(Contact contact)
        {
            InitializeComponent();
            DataContext = new EditWindowViewModel(contact);
        }
    }
}
