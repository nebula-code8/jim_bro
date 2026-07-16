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
        _viewModel = new ClientWorkoutsViewModel(new WorkoutService(new WorkoutDbRepository()), client);
        DataContext = _viewModel;
        WorkoutsDataGrid.ItemsSource = _viewModel.Workouts;
        _viewModel.LoadWorkouts();
    }

    private async void ShowWorkoutExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        ClientWorkoutExercisesWindow window = new ClientWorkoutExercisesWindow(_viewModel.SelectedWorkout, _client);
        await window.ShowDialog(this);
    }
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}