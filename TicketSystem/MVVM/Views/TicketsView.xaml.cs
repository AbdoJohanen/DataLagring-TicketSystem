using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TicketSystem.Models.Entities;
using TicketSystem.MVVM.ViewModels;
using TicketSystem.Services;

namespace TicketSystem.MVVM.Views;

/// <summary>
/// Interaction logic for TicketsView.xaml
/// </summary>
public partial class TicketsView : UserControl
{
    public TicketsView()
    {
        InitializeComponent();
        Loaded += TicketsView_Loaded;
    }

    private async void TicketsView_Loaded(object sender, RoutedEventArgs e)
    {
        await ((TicketsViewModel)DataContext).LoadTicketsAsync();
    }

    private async void Btn_Remove_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var ticket = (TicketEntity)button.DataContext;

        var result = MessageBox.Show("Är du säker på att du vill radera ärendet?", "Radera Ärendet", MessageBoxButton.YesNo);

        if (result == MessageBoxResult.Yes)
        {
            await TicketService.DeleteAsync(ticket.Id);
            ((TicketsViewModel)DataContext).LoadTickets();
        }
    }

    private async void Btn_NotStarted_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var viewModel = button.DataContext as TicketsViewModel;
        TicketEntity ticket = viewModel!.SelectedTicket;
        ticket.Status = "Ej påbörjad";
        await TicketService.UpdateStatusAsync(ticket);
        ((TicketsViewModel)DataContext).LoadTickets();
    }

    private async void Btn_TicketStarted_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var viewModel = button.DataContext as TicketsViewModel;
        TicketEntity ticket = viewModel!.SelectedTicket;
        ticket.Status = "Pågående";
        await TicketService.UpdateStatusAsync(ticket);
        ((TicketsViewModel)DataContext).LoadTickets();
    }

    private async void Btn_TicketClosed_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var viewModel = button.DataContext as TicketsViewModel;
        TicketEntity ticket = viewModel!.SelectedTicket;
        ticket.Status = "Avslutad";
        await TicketService.UpdateStatusAsync(ticket);
        ((TicketsViewModel)DataContext).LoadTickets();
    }

    private void Btn_Comment_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var ticket = (TicketEntity)button.DataContext;
        ((TicketsViewModel)DataContext).NavigateToCommentViewCommand.Execute(ticket);
    }
}
