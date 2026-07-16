using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JimBro.Database.Repositories;
using JimBro.Services;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class AverageTrainerRatingsWindow : Window
{
    private readonly AverageTrainerRatingsViewModel _viewModel;
    
    public AverageTrainerRatingsWindow()
    {
        InitializeComponent();
        _viewModel = new AverageTrainerRatingsViewModel(new TrainerService(new TrainerDbRepository()));
        DataContext = _viewModel;
        TrainersDataGrid.ItemsSource = _viewModel.Trainers;
        _viewModel.LoadTrainers();
    }
}