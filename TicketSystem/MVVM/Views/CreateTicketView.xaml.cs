using System;
using System.Windows;
using System.Windows.Controls;
using TicketSystem.Models.Entities;
using TicketSystem.MVVM.ViewModels;
using TicketSystem.Services;

namespace TicketSystem.MVVM.Views;

public partial class CreateTicketView : UserControl
{
    public CreateTicketView()
    {
        InitializeComponent();
    }

    private async void Btn_Send_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(tb_Email.Text) && !string.IsNullOrEmpty(tb_Subject.Text) && !string.IsNullOrEmpty(tb_Description.Text))
        {
            var _user = await UserService.GetAsync(tb_Email.Text.Trim());
            if (_user != null)
            {
                var ticket = new TicketEntity
                {
                    UserId = _user.Id,
                    Status = "Ej påbörjad",
                    CreatedAt = DateTime.Now,
                    Subject = tb_Subject.Text.Truncate(50),
                    Description = tb_Description.Text
                };
                await TicketService.SaveAsync(ticket);
                MessageBox.Show("Ärendet är skapat.", "Success", MessageBoxButton.OK);
                ((CreateTicketViewModel)DataContext).NavigateToTicketsViewCommand.Execute(null!);
                ClearForm();
            }
            else
                MessageBox.Show("E-postadressen du angav existerar inte.", "Error", MessageBoxButton.OK);
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
        tb_Email.Text = "";
        tb_Subject.Text = "";
        tb_Description.Text = "";
    }
}
