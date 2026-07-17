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

public partial class CreateWorkoutWindow : Window
{
    private readonly CreateWorkoutViewModel _viewModel;
    private readonly Trainer _currentTrainer;
    private readonly Client _selectedClient;
    
    public CreateWorkoutWindow(Trainer trainer, Client client)
    {
        InitializeComponent();
        _currentTrainer = trainer;
        _selectedClient = client;
        _viewModel = new CreateWorkoutViewModel(new ExerciseService(new ExerciseDbRepository()),
            new WorkoutService(new WorkoutDbRepository()),
            new WorkoutExerciseService(new WorkoutExerciseDbRepository()), trainer, client);
        DataContext = _viewModel;
        AllExercisesDataGrid.ItemsSource = _viewModel.AllExercises;
        WorkoutExercisesDataGrid.ItemsSource = _viewModel.WorkoutExercises;
        _viewModel.LoadExercises();
    }
    
    private void AddWorkoutExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.Sets = SetsBox.Text ?? "";
            _viewModel.Reps = RepsBox.Text ?? "";
            _viewModel.Duration = DurationBox.Text ?? "";
            
            bool success = _viewModel.AddExerciseToWorkout();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste dodali vezbu!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                SetsBox.Text = "";
                RepsBox.Text = "";
                DurationBox.Text = "";
            }
            else
            {
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
    
    private void RemoveWorkoutExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.RemoveExerciseFromWorkout();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste uklonili vezbu!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
            }
            else
            {
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
    
    private void AddWorkoutButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            _viewModel.WorkoutNote = NoteBox.Text ?? "";

            bool success = _viewModel.AddWorkout();
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste dodali trening!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
            }
            else
            {
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