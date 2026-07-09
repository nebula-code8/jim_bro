using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Domain;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class LogInForm : Window
{
    private readonly LoginViewModel _viewModel;
    public LogInForm()
    {
        InitializeComponent();
        _viewModel = new LoginViewModel(new UserDbRepository());
    }
    
    private void LoginButton_Click(object? sender, RoutedEventArgs e)
    {
        _viewModel.Email = EmailTextBox.Text ?? "";
        _viewModel.Password = PasswordTextBox.Text ?? "";
        if (!_viewModel.Authenticate())
        {
            ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
            ErrorMessageTextBlock.IsVisible = true;
            return;
        }
        var (userId, role) = _viewModel.AuthResult!.Value;
        Window homeWindow = role switch
        {
            Role.Client => new ClientHomeWindow(),
            Role.Trainer => new TrainerHomeWindow(),
            Role.Admin => new AdminHomeWindow(),
            _ => throw new InvalidOperationException($"Unknown role: {role}")
        };
        homeWindow.Show();
        Close();
    }
    
    private async void RegisterButton_Click(object? sender, RoutedEventArgs e)
    {
        RegisterForm registerForm = new RegisterForm();
        await registerForm.ShowDialog(this);
    }
}