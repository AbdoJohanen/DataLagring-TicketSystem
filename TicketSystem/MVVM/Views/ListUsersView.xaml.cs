using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TicketSystem.Models;
using TicketSystem.MVVM.ViewModels;
using TicketSystem.Services;

namespace TicketSystem.MVVM.Views;

/// <summary>
/// Interaction logic for ListUsersView.xaml
/// </summary>
public partial class ListUsersView : UserControl
{
    public ListUsersView()
    {
        InitializeComponent();
        Loaded += ListUsersView_Loaded;
    }

    private async void ListUsersView_Loaded(object sender, RoutedEventArgs e)
    {
        await ((ListUsersViewModel)DataContext).LoadUsersAsync();
    }

    private async void Btn_Remove_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var button = (Button)sender;
        var user = (UserModel)button.DataContext;

        var result = MessageBox.Show("Är du säker på att du vill radera kontakten?", "Radera Kontakten", MessageBoxButton.YesNo);

        if (result == MessageBoxResult.Yes)
        {
            await UserService.DeleteAsync(user.Email);
            ((ListUsersViewModel)DataContext).LoadUsers();
        }
    }
}
