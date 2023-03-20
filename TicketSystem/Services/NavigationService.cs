using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using TicketSystem.Core;
using TicketSystem.MVVM.ViewModels;

namespace TicketSystem.Services;
public interface IParametrizedViewModel
{
    void SetParameter(object parameter);
}


public interface INavigationService
{
    ViewModel CurrentView { get; }
    void NavigateTo<T>(object parameter = null) where T : ViewModel;
}

public class NavigationService : Core.ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModel> _viewModelFactory;
    private ViewModel _currentView;

    public ViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<T>(object parameter = null) where T : ViewModel
    {
        ViewModel viewModel = _viewModelFactory.Invoke(typeof(T));
        if (viewModel is IParametrizedViewModel parametrizedViewModel)
        {
            parametrizedViewModel.SetParameter(parameter);
        }
        CurrentView = viewModel;
    }
}
