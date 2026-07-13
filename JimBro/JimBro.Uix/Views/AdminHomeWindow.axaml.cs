using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Uix.Views;

namespace JimBro.Uix;

public partial class AdminHomeWindow : Window
{
    public AdminHomeWindow()
    {
        InitializeComponent();
    }
    
    private async void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
            LogInForm loginForm = new LogInForm();
            loginForm.Show();
            Close();
    }
}