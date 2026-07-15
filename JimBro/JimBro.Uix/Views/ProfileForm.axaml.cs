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

public partial class ProfileForm : Window
{
    private readonly ClientFormViewModel _viewModel;
    private readonly Client _client;
    public ProfileForm(Client client)
    {
        InitializeComponent();
        _client = client;
        _viewModel = new ClientFormViewModel(new ClientService(new ClientDbRepository()));
        DataContext = _viewModel;
        LoadClientData();
    }
    
    private void UpdateButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var client = new Client(
                id: _client.Id,
                name: NameBox.Text ?? _client.Name,
                surname: SurnameBox.Text ?? _client.Surname,
                gender: GenderBox.SelectedIndex == 0 ? Gender.Male : Gender.Female,
                dateOfBirth: DateOnly.FromDateTime(DatePicker.SelectedDate?.DateTime ?? DateTime.Now),
                phoneNumber: PhoneBox.Text ?? _client.PhoneNumber,
                emailAddress: EmailBox.Text ?? _client.EmailAddress,
                password: string.IsNullOrWhiteSpace(PasswordBox.Text) ? _client.Password : PasswordBox.Text, 
                height: Convert.ToDouble(HeightBox.Text),
                weight: Convert.ToDouble(WeightBox.Text),
                goal: GoalBox.Text ?? "",
                trainingLocation: LocationBox.SelectedIndex == 0 ? TrainingLocation.Teratana : TrainingLocation.Kuci,
                healthProblems: HealthProblemsBox.Text ?? ""
            );
            bool success = _viewModel.Update(client);

            if (success)
            {
                ErrorMessageTextBlock.Text = "Profil je uspešno azuriran!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
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
    
    private void LoadClientData()
    {
        NameBox.Text = _client.Name;
        SurnameBox.Text = _client.Surname;
        GenderBox.SelectedIndex = _client.Gender == Gender.Male ? 0 : 1;
        DatePicker.SelectedDate = _client.DateOfBirth.ToDateTime(TimeOnly.MinValue);
        PhoneBox.Text = _client.PhoneNumber;
        EmailBox.Text = _client.EmailAddress;
        PasswordBox.Text = _client.Password;
        HeightBox.Text = _client.Height.ToString();
        WeightBox.Text = _client.Weight.ToString();
        GoalBox.Text = _client.Goal.ToString();
        LocationBox.SelectedIndex = _client.TrainingLocation == TrainingLocation.Teratana ? 0 : 1;
        HealthProblemsBox.Text = _client.HealthProblems;
    }
}