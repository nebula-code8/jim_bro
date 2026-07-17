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

public partial class RateTrainerWindow : Window
{
    private readonly RateTrainerViewModel _viewModel;
    private Trainer? _selectedTrainer;
    private readonly Client _currentClient;
    
    public RateTrainerWindow(Client client)
    {
        InitializeComponent();
        _currentClient = client;
        _viewModel = new RateTrainerViewModel(new TrainerService(new TrainerDbRepository()), new TrainerRatingsService(new TrainerRatingsDbRepository()), client);
        DataContext = _viewModel;
        TrainersDataGrid.ItemsSource = _viewModel.Trainers;
        TrainerRatingsDataGrid.ItemsSource = _viewModel.TrainerRatings;
        _viewModel.LoadTrainers();
        _viewModel.LoadTrainerRatings();
        TrainersDataGrid.SelectionChanged += TrainersDataGrid_SelectionChanged;
    }
    
    private void TrainersDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _selectedTrainer = TrainersDataGrid.SelectedItem as Trainer;
    }
    
    private void RateTrainerButton_Click(object? sender, RoutedEventArgs e)
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
            
            bool success = _viewModel.AddRating(RatingBox.Text ?? "", CommentBox.Text ?? "");

            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste ocenili trenera!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                _viewModel.LoadTrainerRatings();
                RatingBox.Text = "";
                CommentBox.Text = "";
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