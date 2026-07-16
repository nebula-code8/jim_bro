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

public partial class RateWorkoutWindow : Window
{
    private readonly RateWorkoutViewModel _viewModel;
    private readonly ClientWorkoutsViewModel _clientWorkoutsViewModel;
    private Trainer? _selectedTrainer;
    private readonly Client _currentClient;
    
    public RateWorkoutWindow(Workout workout, Client client, ClientWorkoutsViewModel clientWorkoutsViewModel)
    {
        InitializeComponent();
        _currentClient = client;
        _viewModel =
            new RateWorkoutViewModel(new WorkoutRatingsService(new WorkoutRatingsDbRepository()), client, workout);
        _clientWorkoutsViewModel = clientWorkoutsViewModel;
        DataContext = _viewModel;
    }
    
    private void RateWorkoutButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.AddRating(RatingBox.Text ?? "", CommentBox.Text ?? "");

            if (success)
            {
                var workoutService = new WorkoutService(new WorkoutDbRepository());
                workoutService.UpdateWorkoutStatus(_clientWorkoutsViewModel.SelectedWorkout.Id, true);
                
                ErrorMessageTextBlock.Text = "Uspešno ste ocenili trening!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                _clientWorkoutsViewModel.LoadWorkoutRatings();
                _clientWorkoutsViewModel.LoadWorkouts();
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