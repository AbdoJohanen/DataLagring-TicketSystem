using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TicketSystem.Models;
using TicketSystem.Models.Entities;
using TicketSystem.MVVM.ViewModels;
using TicketSystem.Services;

namespace TicketSystem.MVVM.Views;

/// <summary>
/// Interaction logic for CommentView.xaml
/// </summary>
public partial class CommentView : UserControl
{
    public CommentView()
    {
        InitializeComponent();
    }

    private async void Btn_Add_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var viewModel = button.DataContext as CommentViewModel;
        TicketEntity ticket = viewModel!.SelectedTicket;

        if (!string.IsNullOrEmpty(tb_Email.Text) && !string.IsNullOrEmpty(tb_Comment.Text))
        {
            var _user = await UserService.GetAsync(tb_Email.Text.Trim());
            if (_user != null)
            {
                var comment = new CommentEntity
                {
                    UserId = _user.Id,
                    TicketId = ticket.Id,
                    CreatedAt = DateTime.Now,
                    Comment = tb_Comment.Text
                };
                await CommentService.SaveAsync(comment);
                MessageBox.Show("Kommentaren är skapad.", "Success", MessageBoxButton.OK);
                ((CommentViewModel)DataContext).NavigateToTicketsViewCommand.Execute(null!);
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
        ((CommentViewModel)DataContext).NavigateToTicketsViewCommand.Execute(null!);
        ClearForm();
    }


    private void ClearForm()
    {
        tb_Email.Text = "";
        tb_Comment.Text = "";
    }
}
