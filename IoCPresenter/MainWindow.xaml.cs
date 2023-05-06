using IoCPresenter.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace IoCPresenter;

public partial class MainWindow : Window
{
    private readonly IServiceCollection _serviceCollection;
    private readonly IMyService _myService;

    public MainWindow(IServiceCollection serviceCollection, IMyService myService)
    {
        _serviceCollection = serviceCollection;
        _myService = myService;
        InitializeComponent();
    }

    private void ShowIoCServicesButton_Click(object sender, RoutedEventArgs e)
    {
        var ioсPresenterWindow = new IoCPresenterWindow(_serviceCollection);
        ioсPresenterWindow.Show();
    }

    private void GetMessageFromServiceButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(_myService.GetMessage(), "Message from Service");
    }
}
