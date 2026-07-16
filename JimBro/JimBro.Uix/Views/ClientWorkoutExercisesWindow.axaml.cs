using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class ClientWorkoutExercisesWindow : Window
{
    private readonly ClientWorkoutExercisesViewModel _viewModel;
    
    public ClientWorkoutExercisesWindow(Workout workout, Client client)
    {
        InitializeComponent();
        _viewModel =
            new ClientWorkoutExercisesViewModel(new WorkoutExerciseService(new WorkoutExerciseDbRepository()), workout, client);
        DataContext = _viewModel;
        WorkoutExerciseDataGrid.ItemsSource = _viewModel.WorkoutExercises;
        _viewModel.LoadWorkoutExercises();
    }
    
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}