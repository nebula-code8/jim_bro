using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Services;
using JimBro.Uix.ViewModels;
using JimBro.Uix.Views;

namespace JimBro.Uix;

public partial class TrainerHomeWindow : Window
{
    private Trainer _trainer;
    
    public TrainerHomeWindow(Trainer trainer)
    {
        InitializeComponent();
        _trainer = trainer;
    }
    
    private async void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        Close();
    }
    
    private async void TrainerRequestsButton_Click(object? sender, RoutedEventArgs e)
    {
        TrainerRequestsWindow window = new TrainerRequestsWindow(_trainer);
        await window.ShowDialog(this);
    }
    private async void CreateExerciseButton_Click(object? sender, RoutedEventArgs e)
    {
        CreateExerciseWindow window = new  CreateExerciseWindow(_trainer);
        await window.ShowDialog(this);
    }
    
    private async void CreateWorkoutButton_Click(object? sender, RoutedEventArgs e)
    {
        ClientsForTrainerWindow window = new ClientsForTrainerWindow(_trainer);
        await window.ShowDialog(this);
    }
}