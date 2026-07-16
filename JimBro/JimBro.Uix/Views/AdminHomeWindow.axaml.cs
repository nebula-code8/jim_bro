using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Uix.Views;
using JimBro.Services;

namespace JimBro.Uix;

public partial class AdminHomeWindow : Window
{
    public AdminHomeWindow()
    {
        InitializeComponent();
    }

    private async void AccessoriesCRUD_Click(object? sender, RoutedEventArgs e)
    {
        AccessoriesWindow accessoriesWindow = new AccessoriesWindow();
        await accessoriesWindow.ShowDialog(this);
    }
    
    private async void AverageRatings_Click(object? sender, RoutedEventArgs e)
    {
        AverageTrainerRatingsWindow window = new AverageTrainerRatingsWindow();
        await window.ShowDialog(this);
    }

    private void Logout_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();

        loginForm.Show();
        Close();
    }
}