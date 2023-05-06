using IoCPresenter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IoCPresenter;

public partial class IoCPresenterWindow : Window
{
    private IServiceCollection _serviceCollection;
    private List<ServiceDescriptorViewModel> _services;
    private List<ServiceDescriptorViewModel> _filteredServices;

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

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = SearchTextBox.Text.ToLower();
        _filteredServices = _services.Where(s => s.Name.ToLower().Contains(searchText) || s.Type.ToLower().Contains(searchText)).ToList();
        ServicesListView.ItemsSource = _filteredServices;
    }

    private void ServicesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedService = ServicesListView.SelectedItem as ServiceDescriptorViewModel;

        if (selectedService != null)
        {
            SelectedServiceDetailsGrid.Children.Clear();
            SelectedServiceDetailsGrid.RowDefinitions.Clear();

            // Add grid rows
            for (int i = 0; i < 5; i++)
            {
                SelectedServiceDetailsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            // Add Name label and value
            AddDetailRow(SelectedServiceDetailsGrid, "Name:", selectedService.Name, 0);

            // Add Type label and value
            AddDetailRow(SelectedServiceDetailsGrid, "Type:", selectedService.Type, 1);

            // Add Lifetime label and value
            AddDetailRow(SelectedServiceDetailsGrid, "Lifetime:", selectedService.Lifetime, 2);

            // Add Implementation label and value
            AddDetailRow(SelectedServiceDetailsGrid, "Implementation:", selectedService.ImplementationType, 3);

            // Add Assembly label and value
            AddDetailRow(SelectedServiceDetailsGrid, "Assembly:", selectedService.Assembly, 4);
        }
    }

    private static void AddDetailRow(Grid grid, string label, string value, int rowIndex)
    {
        var labelControl = new TextBlock { Text = label, FontWeight = FontWeights.Bold, Margin = new Thickness(0, 5, 5, 5) };
        Grid.SetRow(labelControl, rowIndex);
        Grid.SetColumn(labelControl, 0);
        grid.Children.Add(labelControl);

        var valueControl = new TextBlock { Text = value, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(5) };
        Grid.SetRow(valueControl, rowIndex);
        Grid.SetColumn(valueControl, 1);
        grid.Children.Add(valueControl);

        // Add ColumnDefinitions to grid
        if (grid.ColumnDefinitions.Count == 0)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
    }
}