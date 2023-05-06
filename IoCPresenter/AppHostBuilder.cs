using IoCPresenter.Abstractions;
using IoCPresenter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IoCPresenter;

public static class AppHostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);

    private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        // Здесь зарегистрируйте свои сервисы
        // services.AddTransient<IMyService, MyServiceImplementation>();

        // Зарегистрировать MainWindow и IoCPresenterWindow
        services.AddSingleton(services); // передача IServiceCollection как Singleton
        services.AddTransient<MainWindow>();
        services.AddTransient<IoCPresenterWindow>();

        services.AddTransient<IMyService, MyService>();
    }

    public static void RunApplication(IHost host)
    {
        var mainWindow = host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
