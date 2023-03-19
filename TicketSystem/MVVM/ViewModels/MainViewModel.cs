using TicketSystem.Core;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

public partial class MainViewModel : ViewModel
{
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
    public RelayCommand NavigateToCreateTicketsViewCommand { get; set; }
    public RelayCommand NavigateToCreateUserViewCommand { get; set; }
    public RelayCommand NavigateToListUsersViewCommand { get; set; }

    public MainViewModel(INavigationService navService)
    {
        Navigation = navService;
        NavigateToTicketsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<TicketsViewModel>(); }, canExecute: o => true);
        NavigateToCreateTicketsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<CreateTicketViewModel>(); }, canExecute: o => true);
        NavigateToCreateUserViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<CreateUserViewModel>(); }, canExecute: o => true);
        NavigateToListUsersViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<ListUsersViewModel>(); }, canExecute: o => true);
    }
}
