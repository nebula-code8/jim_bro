using System;
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

public partial class CreateExerciseWindow : Window
{
    private readonly ExerciseViewModel _viewModel;
    private readonly Trainer _currentTrainer;
    
    public CreateExerciseWindow(Trainer trainer)
    {
        InitializeComponent();
        _currentTrainer = trainer;
        _viewModel = new ExerciseViewModel(new ExerciseService(new ExerciseDbRepository()), new AccessoryService(new AccessoryDbRepository()), new MachineService(new MachineDbRepository()), _currentTrainer);
        DataContext = _viewModel;
        ExercisesDataGrid.ItemsSource = _viewModel.Exercises;
        AccessoryComboBox.ItemsSource = _viewModel.Accessories;
        MachineComboBox.ItemsSource = _viewModel.Machines;
        
        _viewModel.LoadExercises();
        _viewModel.LoadAccessories();
        _viewModel.LoadMachines();
    }
    
    private void CreateButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            bool success = _viewModel.CreateExercise(NameBox.Text, DescriptionBox.Text ?? "", VideoBox.Text ?? "");
            
            if (success)
            {
                ErrorMessageTextBlock.Text = "Uspešno ste se dodali vezbu!";
                ErrorMessageTextBlock.Foreground = Brushes.Green;
                ErrorMessageTextBlock.IsVisible = true;
                CleanForm();
                _viewModel.LoadExercises();
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
    
    private void CleanForm()
    {
        NameBox.Text = string.Empty;
        DescriptionBox.Text = string.Empty;
        VideoBox.Text = string.Empty;
        _viewModel.SelectedAccessory = null;
        _viewModel.SelectedMachine = null;
        AccessoryComboBox.SelectedItem = null;
        MachineComboBox.SelectedItem = null;
    }
}