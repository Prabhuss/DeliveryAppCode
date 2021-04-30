using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            if (CrossConnectivity.Current.IsConnected)
            {

                menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="New Order",IconImg="home.png" },
                new HomeMenuItem {Id = MenuItemType.InProgress, Title="On The Way Orders",IconImg="inprogress.png" },
                new HomeMenuItem {Id = MenuItemType.Delevered, Title="Delivered Orders", IconImg="delivered.png" },
                new HomeMenuItem {Id = MenuItemType.Today, Title="Today's Summary", IconImg="today.png" },
                new HomeMenuItem {Id = MenuItemType.Setting, Title="About Us", IconImg= "about.png" },
                new HomeMenuItem {Id = MenuItemType.Profile, Title="Logout", IconImg="logout.png" },
            };

                ListViewMenu.ItemsSource = menuItems;

                ListViewMenu.SelectedItem = menuItems[0];
                ListViewMenu.ItemSelected += async (sender, e) =>
                {
                    if (e.SelectedItem == null)
                        return;

                    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                    await RootPage.NavigateFromMenu(id);
                };

            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");

            }
        }
     }
}