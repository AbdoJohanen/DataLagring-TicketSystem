using TicketSystem.Core;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

internal class CreateUserViewModel : ViewModel
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

    public RelayCommand NavigateToListUsersViewCommand { get; set; }

    public CreateUserViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToListUsersViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<ListUsersViewModel>(); }, canExecute: o => true);
    }
}
