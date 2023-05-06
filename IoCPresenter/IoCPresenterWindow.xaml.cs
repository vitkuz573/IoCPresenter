using IoCPresenter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace IoCPresenter;

public partial class IoCPresenterWindow : Window
{
    public IoCPresenterWindow(IServiceCollection serviceCollection)
    {
        InitializeComponent();
        LoadServicesIntoListView(serviceCollection);
    }

    private void LoadServicesIntoListView(IServiceCollection serviceCollection)
    {
        var services = new List<ServiceDescriptorViewModel>();

        foreach (var descriptor in serviceCollection)
        {
            services.Add(new ServiceDescriptorViewModel
            {
                Name = descriptor.ServiceType.Name,
                Type = descriptor.ServiceType.FullName,
                Lifetime = descriptor.Lifetime.ToString(),
                ImplementationType = descriptor.ImplementationType?.Name ?? "N/A",
                Assembly = descriptor.ServiceType.Assembly.FullName
            });
        }

        ServicesListView.ItemsSource = services;
    }
}
