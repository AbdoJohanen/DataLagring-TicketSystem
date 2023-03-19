using System.Collections.ObjectModel;
using TicketSystem.Core;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

public partial class ListUsersViewModel : ViewModel
{
    private ObservableCollection<UserModel> _users;
    private UserModel _selectedUser;

    public ObservableCollection<UserModel> Users
    {
        get { return _users; }
        set
        {
            if (_users != value)
            {
                _users = value;
                OnPropertyChanged();
            }
        }
    }

    public UserModel SelectedUser
    {
        get { return _selectedUser; }
        set
        {
            if (_selectedUser != value)
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }
    }

    public ListUsersViewModel()
    {
        LoadUsers();
    }

    public async void LoadUsers()
    {
        Users = await UserService.GetAllAsync();
    }
}