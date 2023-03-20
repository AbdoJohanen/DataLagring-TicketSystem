using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using TicketSystem.Models;
using TicketSystem.MVVM.ViewModels;
using TicketSystem.Services;

namespace TicketSystem.MVVM.Views;

public partial class CreateUserView : UserControl
{
    public CreateUserView()
    {
        InitializeComponent();
    }

    private async void Btn_Add_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(tb_FirstName.Text) &&
            !string.IsNullOrEmpty(tb_LastName.Text) &&
            !string.IsNullOrEmpty(tb_Email.Text) &&
            !string.IsNullOrEmpty(tb_StreetAdress.Text) &&
            !string.IsNullOrEmpty(tb_PostalCode.Text) &&
            !string.IsNullOrEmpty(tb_City.Text))
        {
            if (await UserService.GetAsync(tb_Email.Text) != null)
            {
                MessageBox.Show("En användare med denna E-postadress existerar redan.", "Error", MessageBoxButton.OK);
            }
            else
            {
                var user = new UserModel
                {
                    FirstName = tb_FirstName.Text.Truncate(50),
                    LastName = tb_LastName.Text.Truncate(50),
                    Email = tb_Email.Text.Truncate(100),
                    PhoneNumber = MyRegex().Replace(tb_PhoneNumber.Text, "").Truncate(10),
                    StreetName = tb_StreetAdress.Text.Truncate(100),
                    PostalCode = MyRegex().Replace(tb_PostalCode.Text, "").Truncate(5),
                    City = tb_City.Text.Truncate(100)
                };
                await UserService.SaveAsync(user);
                MessageBox.Show("Användaren är skapad.", "Success", MessageBoxButton.OK);
                ((CreateUserViewModel)DataContext).NavigateToListUsersViewCommand.Execute(null!);
                ClearForm();
            }
        }
        else
            MessageBox.Show("Du måste fylla i fälten.", "Error", MessageBoxButton.OK);
    }

    private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        tb_FirstName.Text = "";
        tb_LastName.Text = "";
        tb_Email.Text = "";
        tb_PhoneNumber.Text = "";
        tb_StreetAdress.Text = "";
        tb_PostalCode.Text = "";
        tb_City.Text = "";
    }

    //REGEX
    [GeneratedRegex("[^0-9]")]
    private static partial Regex MyRegex();
}
