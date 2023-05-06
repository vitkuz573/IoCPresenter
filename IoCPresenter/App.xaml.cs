using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace IoCPresenter;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = AppHostBuilder.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        AppHostBuilder.RunApplication(_host);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        await _host.StopAsync();
        _host.Dispose();
    }
}
