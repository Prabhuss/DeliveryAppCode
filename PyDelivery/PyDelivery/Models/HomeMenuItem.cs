using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Models
{
    public enum MenuItemType
    {
        Browse,
        InProgress,
        Delevered,
        Today, 
        Setting,
        Profile
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string IconImg { get; set; }
    }
}
