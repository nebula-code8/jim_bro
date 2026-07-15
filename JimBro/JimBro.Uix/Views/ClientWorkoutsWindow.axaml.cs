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
    ClientWorkoutsViewModel _viewModel;
    
    public ClientWorkoutsWindow(Client client)
    {
        InitializeComponent();
        _viewModel = new ClientWorkoutsViewModel(new WorkoutService(new WorkoutDbRepository()), client);
        DataContext = _viewModel;
        WorkoutsDataGrid.ItemsSource = _viewModel.Workouts;
        _viewModel.LoadWorkouts();
    }

    private void ShowWorkoutExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        
    }
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}