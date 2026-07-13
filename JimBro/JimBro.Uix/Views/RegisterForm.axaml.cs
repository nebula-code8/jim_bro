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

public partial class RegisterForm : Window
{
    private readonly ClientFormViewModel _viewModel;
    public RegisterForm()
    {
        InitializeComponent();
        _viewModel = new ClientFormViewModel(new ClientService(new ClientDbRepository()));
        DataContext = _viewModel;
    }
    
    private void RegisterButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            Client client = new Client(NameBox.Text ?? "", SurnameBox.Text ?? "", GenderBox.SelectedIndex == 0 ? Gender.Male : Gender.Female , DateOnly.FromDateTime(DatePicker.SelectedDate?.DateTime ?? DateTime.Now), PhoneBox.Text ?? "", EmailBox.Text ?? "", PasswordBox.Text ?? "", Convert.ToDouble(HeightBox.Text), Convert.ToDouble(WeightBox.Text), GoalBox.Text ?? "", HealthProblemsBox.Text ?? "");
            bool success = _viewModel.Register(client);

            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste se registrovali!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                CleanForm();
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

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void CleanForm()
    {
        NameBox.Text = string.Empty;
        SurnameBox.Text = string.Empty;
        GenderBox.SelectedItem = Gender.Male;
        DatePicker.SelectedDate = DateTime.Now;
        PhoneBox.Text = string.Empty;
        EmailBox.Text = string.Empty;
        PasswordBox.Text = string.Empty;
        HeightBox.Text = string.Empty;
        WeightBox.Text = string.Empty;
        GoalBox.Text = string.Empty;
        HealthProblemsBox.Text = string.Empty;
    }
}