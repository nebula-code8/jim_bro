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

public partial class RequestTrainerWindow : Window
{
    private readonly RequestTrainerViewModel _viewModel;
    private Trainer? _selectedTrainer;
    private readonly Client _currentClient;
    
    public RequestTrainerWindow(Client client)
    {
        InitializeComponent();
        _currentClient = client;
        _viewModel = new RequestTrainerViewModel(new TrainerService(new TrainerDbRepository()), new TrainerRequestService(new TrainerRequestDbRepository()), _currentClient);
        DataContext = _viewModel;
        TrainersDataGrid.ItemsSource = _viewModel.Trainers;
        _viewModel.LoadTrainers();
        TrainersDataGrid.SelectionChanged += TrainersDataGrid_SelectionChanged;
    }
    
    private void TrainersDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedTrainer = TrainersDataGrid.SelectedItem as Trainer;
        
        if (_selectedTrainer != null)
        {
            RequestButton.IsEnabled = true;
        }
        else
        {
            RequestButton.IsEnabled = false;
        }
    }

    private void SendRequestButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (_selectedTrainer == null)
            {
                ErrorMessageTextBlock.Text = "Izaberite trenera!";
                ErrorMessageTextBlock.Foreground = Brushes.Red;
                ErrorMessageTextBlock.IsVisible = true;
                return;
            }
            
            bool success = _viewModel.SendRequest(_currentClient.Id, _selectedTrainer.Id);

            if (success)
            {
                ErrorMessageTextBlock.Text = "Zahtev je uspešno poslat!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                _viewModel.LoadTrainers();
            } else{
                ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
                ErrorMessageTextBlock.Foreground = Brushes.Red;
                ErrorMessageTextBlock.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessageTextBlock.Text = ex.Message;
            ErrorMessageTextBlock.Foreground = Brushes.Red;
            ErrorMessageTextBlock.IsVisible = true;
        }
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}