using TicketSystem.Core;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

public partial class CreateTicketViewModel : ViewModel
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

    public CreateTicketViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToTicketsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<TicketsViewModel>(); }, canExecute: o => true);
    }
}