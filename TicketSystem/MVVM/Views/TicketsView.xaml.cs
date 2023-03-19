using System.Windows;
using System.Windows.Controls;
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
        DataContext = new TicketsViewModel();
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


}
