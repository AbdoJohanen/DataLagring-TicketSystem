using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using TicketSystem.Core;
using TicketSystem.Models.Entities;
using TicketSystem.Services;

namespace TicketSystem.MVVM.ViewModels;

public class CommentViewModel : ViewModel, IParametrizedViewModel
{
    private TicketEntity _ticket;

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

    public CommentViewModel(INavigationService navigation, TicketEntity ticket)
    {
        Navigation = navigation;
        NavigateToTicketsViewCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<TicketsViewModel>(); }, canExecute: o => true);
        _ticket = ticket;
    }


    public void SetParameter(object parameter)
    {
        if(parameter is TicketEntity ticket)
        {
            SelectedTicket = ticket;
        }
    }

    public TicketEntity SelectedTicket
    {
        get { return _ticket; }
        set
        {
            if (_ticket != value)
            {
                _ticket = value;
                OnPropertyChanged();
            }
        }
    }
}
