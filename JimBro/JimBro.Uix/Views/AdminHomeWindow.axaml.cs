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
}