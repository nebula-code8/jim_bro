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

public partial class RateExerciseWindow : Window
{
    private readonly RateExerciseViewModel _viewModel;
    private readonly ClientWorkoutExercisesViewModel _clientWorkoutExercisesViewModel;
    private readonly Client _currentClient;
    
    public RateExerciseWindow(WorkoutExercise workoutExercise, Client client, ClientWorkoutExercisesViewModel clientWorkoutExercisesViewModel)
    {
        InitializeComponent();
        _currentClient = client;
        _viewModel = new RateExerciseViewModel(new ExerciseRatingsService(new ExerciseRatingsDbRepository()), client,
            workoutExercise);
        _clientWorkoutExercisesViewModel = clientWorkoutExercisesViewModel;
        DataContext = _viewModel;
    }
    
    private void RateExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.AddRating(RatingBox.Text ?? "", CommentBox.Text ?? "");

            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste ocenili vezbu!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                _clientWorkoutExercisesViewModel.LoadWorkoutExercises();
                _clientWorkoutExercisesViewModel.LoadWorkoutExerciseRatings();
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