using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using JimBro.Uix.Views;

namespace JimBro.Uix;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) desktop.MainWindow = new LogInForm();
        base.OnFrameworkInitializationCompleted();
    }
}
