using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class ClientWorkoutsWindow : Window
{
    private readonly ClientWorkoutsViewModel _viewModel;
    private readonly Client _client;
    
    public ClientWorkoutsWindow(Client client)
    {
        InitializeComponent();
        _client = client;
        _viewModel = new ClientWorkoutsViewModel(new WorkoutService(new WorkoutDbRepository()), new WorkoutRatingsService(new WorkoutRatingsDbRepository()), client);
        DataContext = _viewModel;
        WorkoutsDataGrid.ItemsSource = _viewModel.Workouts;
        WorkoutRatingsDataGrid.ItemsSource = _viewModel.WorkoutRatings;
        _viewModel.LoadWorkouts();
        _viewModel.LoadWorkoutRatings();
    }

    private async void ShowWorkoutExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        ClientWorkoutExercisesWindow window = new ClientWorkoutExercisesWindow(_viewModel.SelectedWorkout, _client);
        await window.ShowDialog(this);
    }
    
    private async void RateWorkoutButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_viewModel.SelectedWorkout == null)
        {
            ErrorMessageTextBlock.Text = "Izaberite trening!";
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
        RateWorkoutWindow window = new RateWorkoutWindow(_viewModel.SelectedWorkout, _client, _viewModel);
        await window.ShowDialog(this);
    }
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}