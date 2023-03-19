using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TicketSystem.Core;
using TicketSystem.Models.Entities;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

public partial class TicketsViewModel : ViewModel
{
    private INavigationService _navigation;

    private ObservableCollection<TicketEntity> _tickets;
    private TicketEntity _selectedTicket;

    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToTicketsViewCommand { get; set; }


    public TicketsViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToTicketsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<TicketsViewModel>(); }, canExecute: o => true);
    }


    public ObservableCollection<TicketEntity> Tickets
    {
        get { return _tickets; }
        set
        {
            if (_tickets != value)
            {
                _tickets = value;
                OnPropertyChanged();
            }
        }
    }

    public async void LoadTickets()
    {
        Tickets = await TicketService.GetAllAsync();
    }

    public TicketsViewModel()
    {
        LoadTickets();
    }

    public TicketEntity SelectedTicket
    {
        get { return _selectedTicket; }
        set
        {
            if (_selectedTicket != value)
            {
                _selectedTicket = value;
                OnPropertyChanged();
            }
        }
    }
}
