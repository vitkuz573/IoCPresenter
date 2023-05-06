using IoCPresenter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace IoCPresenter;

public partial class IoCPresenterWindow : Window
{
    private IServiceCollection _serviceCollection;
    private List<ServiceDescriptorViewModel> _services;

    public IoCPresenterWindow(IServiceCollection serviceCollection)
    {
        InitializeComponent();
        _serviceCollection = serviceCollection;
        LoadServicesIntoListView(_serviceCollection);
    }

    private void LoadServicesIntoListView(IServiceCollection serviceCollection, bool excludeSystemServices = false)
    {
        _services = new List<ServiceDescriptorViewModel>();

        foreach (var descriptor in serviceCollection)
        {
            var viewModel = new ServiceDescriptorViewModel
            {
                Name = descriptor.ServiceType.Name,
                Type = descriptor.ServiceType.FullName,
                Lifetime = descriptor.Lifetime.ToString(),
                ImplementationType = descriptor.ImplementationType?.Name ?? "N/A",
                Assembly = descriptor.ServiceType.Assembly.FullName
            };

            if (!excludeSystemServices || !viewModel.Assembly.StartsWith("Microsoft.Extensions"))
            {
                _services.Add(viewModel);
            }
        }

        ServicesListView.ItemsSource = _services;
    }

    private void ExcludeSystemServicesCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        LoadServicesIntoListView(_serviceCollection, true);
    }

    private void ExcludeSystemServicesCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        LoadServicesIntoListView(_serviceCollection, false);
    }
}

