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

public partial class ClientHomeWindow : Window
{
    private readonly ClientFormViewModel _viewModel;
    private Client _client;
    
    public ClientHomeWindow(Client client)
    {
        InitializeComponent();
        _client = client;
        _viewModel = new ClientFormViewModel(new ClientService(new ClientDbRepository()));
    }

    private async void ProfileButton_Click(object? sender, RoutedEventArgs e)
    {
        ProfileForm profileForm = new ProfileForm(_client);
        await profileForm.ShowDialog(this);
        _client = new ClientDbRepository().GetById(_client.Id);
    }
    
    private async void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        Close();
    }
    
    private async void RequestTrainerButton_Click(object? sender, RoutedEventArgs e)
    {
        RequestTrainerWindow window = new RequestTrainerWindow(_client);
        await window.ShowDialog(this);
    }
    
    private async void MyRequestsButton_Click(object? sender, RoutedEventArgs e)
        {
            MyRequestsWindow window = new MyRequestsWindow(_client);
            await window.ShowDialog(this);
        }
}