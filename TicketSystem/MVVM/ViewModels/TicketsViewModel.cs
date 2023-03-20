using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TicketSystem.Core;
using TicketSystem.Models.Entities;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

public partial class TicketsViewModel : ViewModel
{
    private ObservableCollection<TicketEntity> _tickets;
    private TicketEntity _selectedTicket;

    private INavigationService _navigation;

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
    public RelayCommand NavigateToCommentViewCommand { get; set; }


    public TicketsViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToTicketsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<TicketsViewModel>(); }, canExecute: o => true);
        NavigateToCommentViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<CommentViewModel>(o); }, canExecute: o => true);

        LoadTickets();
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

    public async Task LoadTicketsAsync()
    {
        Tickets = await TicketService.GetAllAsync();
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

