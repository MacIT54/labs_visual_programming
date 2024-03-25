using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;
using Avalonia.Controls.Templates;
using ReactiveUI;
using System.Collections.ObjectModel;
using Avalonia;
using Lab8visual.Models;

namespace Lab8visual.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private bool _isFlatViewSelected;
        private bool _isTreeViewSelected;
        private object _displayView;

        public bool IsFlatViewSelected
        {
            get => _isFlatViewSelected;
            set
            {
                if (value)
                {
                    _isFlatViewSelected = true;
                    _isTreeViewSelected = false;
                    DisplayFlatView();
                }
            }
        }

        public bool IsTreeViewSelected
        {
            get => _isTreeViewSelected;
            set
            {
                if (value)
                {
                    _isTreeViewSelected = true;
                    _isFlatViewSelected = false;
                    DisplayTreeView();
                }
            }
        }

        public object DisplayView
        {
            get => _displayView;
            set => this.RaiseAndSetIfChanged(ref _displayView, value);
        }

        public MainWindowViewModel()
        {
            IsFlatViewSelected = true;
        }

        private async Task<List<User>> GetUsersAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<User>>(json);
                }
                else
                {
                    return null;
                }
            }
        }

        private async void DisplayFlatView()
        {
            var users = await GetUsersAsync();
            var listBox = new ListBox();

            listBox.MaxHeight = 1000;

            foreach (var user in users)
            {
                var userTemplate = new StackPanel();

                userTemplate.Children.Add(new TextBlock {Text = $"ID: {user.Id}"});
                userTemplate.Children.Add(new TextBlock {Text = $"Name: {user.Name}"});
                userTemplate.Children.Add(new TextBlock {Text = $"Username: {user.Username}"});
                userTemplate.Children.Add(new TextBlock {Text = $"Email: {user.Email}"});

                var addressExpander = new Expander();
                addressExpander.Header = "Address";
                var addressPanel = new StackPanel();
                addressPanel.Children.Add(new TextBlock {Text = $"Street: {user.Address.Street}"});
                addressPanel.Children.Add(new TextBlock {Text = $"Suite: {user.Address.Suite}"});
                addressPanel.Children.Add(new TextBlock {Text = $"City: {user.Address.City}"});
                addressPanel.Children.Add(new TextBlock {Text = $"Zipcode: {user.Address.Zipcode}"});
                addressPanel.Children.Add(new TextBlock {Text = $"Latitude: {user.Address.Geo.Lat}"});
                addressPanel.Children.Add(new TextBlock {Text = $"Longitude: {user.Address.Geo.Lng}"});
                addressExpander.Content = addressPanel;
                userTemplate.Children.Add(addressExpander);

                userTemplate.Children.Add(new TextBlock {Text = $"Phone: {user.Phone}"});
                userTemplate.Children.Add(new TextBlock {Text = $"Website: {user.Website}"});

                var companyExpander = new Expander();
                companyExpander.Header = "Company";
                var companyPanel = new StackPanel();
                companyPanel.Children.Add(new TextBlock {Text = $"Name: {user.Company.Name}"});
                companyPanel.Children.Add(new TextBlock {Text = $"CatchPhrase: {user.Company.CatchPhrase}"});
                companyPanel.Children.Add(new TextBlock {Text = $"BS: {user.Company.Bs}"});
                companyExpander.Content = companyPanel;
                userTemplate.Children.Add(companyExpander);

                listBox.Items.Add(userTemplate);
            }

            DisplayView = listBox;
        }





        private async void DisplayTreeView()
        {
            var users = await GetUsersAsync();
            var treeView = new TreeView();
            foreach (var user in users)
            {
                var userTreeItem = new TreeViewItem {Header = user.Name};

                var idTreeItem = new TreeViewItem {Header = $"Id: {user.Id}"};
                var usernameTreeItem = new TreeViewItem {Header = $"Username: {user.Username}"};
                var emailTreeItem = new TreeViewItem {Header = $"Email: {user.Email}"};

                var addressTreeItem = new TreeViewItem {Header = "Address"};
                addressTreeItem.Items.Add($"Street: {user.Address.Street}");
                addressTreeItem.Items.Add($"Suite: {user.Address.Suite}");
                addressTreeItem.Items.Add($"City: {user.Address.City}");
                addressTreeItem.Items.Add($"Zipcode: {user.Address.Zipcode}");

                var geoTreeItem = new TreeViewItem {Header = "Geo"};
                geoTreeItem.Items.Add($"Lat: {user.Address.Geo.Lat}");
                geoTreeItem.Items.Add($"Lng: {user.Address.Geo.Lng}");

                var phoneTreeItem = new TreeViewItem {Header = $"Phone: {user.Phone}"};
                var websiteTreeItem = new TreeViewItem {Header = $"Website: {user.Website}"};

                var companyTreeItem = new TreeViewItem {Header = "Company"};
                companyTreeItem.Items.Add($"Name: {user.Company.Name}");
                companyTreeItem.Items.Add($"Catch Phrase: {user.Company.CatchPhrase}");
                companyTreeItem.Items.Add($"BS: {user.Company.Bs}");

                userTreeItem.Items.Add(idTreeItem);
                userTreeItem.Items.Add(usernameTreeItem);
                userTreeItem.Items.Add(emailTreeItem);
                userTreeItem.Items.Add(addressTreeItem);
                userTreeItem.Items.Add(geoTreeItem);
                userTreeItem.Items.Add(phoneTreeItem);
                userTreeItem.Items.Add(websiteTreeItem);
                userTreeItem.Items.Add(companyTreeItem);

                treeView.Items.Add(userTreeItem);
            }
            DisplayView = treeView;
        }
    }
}
