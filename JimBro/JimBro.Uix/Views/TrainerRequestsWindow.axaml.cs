using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class TrainerRequestsWindow : Window
{
    private readonly TrainerRequestsViewModel _viewModel;
    private Client? _selectedClient;
    private readonly Trainer _currentTrainer;
    
    public TrainerRequestsWindow(Trainer trainer)
    {
        InitializeComponent();
        _currentTrainer = trainer;
        _viewModel = new TrainerRequestsViewModel(new TrainerRequestService(new TrainerRequestDbRepository()), trainer);
        DataContext = _viewModel;
        RequestsDataGrid.ItemsSource = _viewModel.Clients;
        _viewModel.LoadRequests();
        RequestsDataGrid.SelectionChanged += RequestsDataGrid_SelectionChanged;
    }
    
    private void RequestsDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedClient = RequestsDataGrid.SelectedItem as Client;
        AcceptButton.IsEnabled = _selectedClient != null;
        RejectButton.IsEnabled = _selectedClient != null;
    }

    private void AcceptButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedClient == null) return;
        long requestId = _viewModel.GetRequestIdForClient(_selectedClient.Id);
        
        bool success = _viewModel.AcceptRequest(requestId);
        if (success)
        {
            ErrorMessageTextBlock.Text = "Zahtev je prihvaćen!";
            ErrorMessageTextBlock.Foreground = Brushes.Green;
            ErrorMessageTextBlock.IsVisible = true;
            _viewModel.LoadRequests();
        }
        else
        {
            ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
            ErrorMessageTextBlock.Foreground = Brushes.Red;
            ErrorMessageTextBlock.IsVisible = true;
        }
    }

    private void RejectButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedClient == null) return;
        long requestId = _viewModel.GetRequestIdForClient(_selectedClient.Id);
        
        bool success = _viewModel.RejectRequest(requestId);
        if (success)
        {
            ErrorMessageTextBlock.Text = "Zahtev je odbijen!";
            ErrorMessageTextBlock.Foreground = Brushes.Green;
            ErrorMessageTextBlock.IsVisible = true;
            _viewModel.LoadRequests();
        }
        else
        {
            ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
            ErrorMessageTextBlock.Foreground = Brushes.Red;
            ErrorMessageTextBlock.IsVisible = true;
        }
    }
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();

}