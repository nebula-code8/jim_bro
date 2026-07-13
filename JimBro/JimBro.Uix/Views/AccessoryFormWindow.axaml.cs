using Avalonia.Controls;
using Avalonia.Interactivity;
using JimBro.Domain;
using JimBro.Uix.ViewModels;

namespace JimBro.Uix.Views;

public partial class AccessoryFormWindow : Window
{
    private readonly AccessoriesViewModel _viewModel;
    private readonly Accessory? _accessoryToEdit;

    public AccessoryFormWindow(AccessoriesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _accessoryToEdit = null;
    }

    public AccessoryFormWindow(AccessoriesViewModel viewModel, Accessory accessoryToEdit)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _accessoryToEdit = accessoryToEdit;
        Title = "Update accessory";
        SaveButton.Content = "Update";
        NameTextBox.Text = accessoryToEdit.Name;
        DescriptionTextBox.Text = accessoryToEdit.Description;
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text ?? string.Empty;
        string description = DescriptionTextBox.Text ?? string.Empty;
        bool successful;
        if (_accessoryToEdit is null)
        {
            successful = _viewModel.AddAccessory(name, description);
        }
        else
        {
            successful = _viewModel.UpdateAccessory(_accessoryToEdit.Id, name, description);
        }
        if (successful)
        {
            Close();
            return;
        }
        ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
        ErrorMessageTextBlock.IsVisible = true;
    }
    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
