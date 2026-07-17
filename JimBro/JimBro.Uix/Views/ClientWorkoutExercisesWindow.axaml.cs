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
    private readonly Client _client;
    
    public ClientWorkoutExercisesWindow(Workout workout, Client client)
    {
        InitializeComponent();
        _client = client; 
        _viewModel =
            new ClientWorkoutExercisesViewModel(new WorkoutExerciseService(new WorkoutExerciseDbRepository()), new ExerciseRatingsService(new ExerciseRatingsDbRepository()), workout, client);
        DataContext = _viewModel;
        WorkoutExerciseDataGrid.ItemsSource = _viewModel.WorkoutExercises;
        WorkoutExerciseRatingDataGrid.ItemsSource = _viewModel.WorkoutExerciseRatings;
        _viewModel.LoadWorkoutExercises();
        _viewModel.LoadWorkoutExerciseRatings();
        WorkoutExerciseDataGrid.SelectionChanged += WorkoutExerciseDataGrid_SelectionChanged;
    }

    private void WorkoutExerciseDataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _viewModel.SelectedWorkoutExercise = WorkoutExerciseDataGrid.SelectedItem as WorkoutExercise;
    }

    private async void RateButton_Click(object? sender, RoutedEventArgs e)
    {
        if (_viewModel.SelectedWorkoutExercise == null)
        {
            ErrorMessageTextBlock.Text = "Izaberite vezbu!";
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
        RateExerciseWindow window = new RateExerciseWindow(_viewModel.SelectedWorkoutExercise, _client, _viewModel);
        await window.ShowDialog(this);
    }
    
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}