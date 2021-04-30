using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PyDelivery.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        public Color NameLabelColor { get; set; }
        public string MerchantStaffName { get; set; }
        public string MerchantStaffId { get; set; }
        public string MerchantStaffRole { get; set; }
        public string PhoneNumber { get; set; }
        public Command LogOutBtnCommand { get; set; }
        public ProfileViewModel()
        {
            var app = Application.Current as App;
            MerchantStaffRole = app.UserRole;
            MerchantStaffName = app.UserName;
            PhoneNumber = app.UserPhoneNumber;
            Title = "User Profile";
            try
            {
                if (string.IsNullOrEmpty(MerchantStaffName))
                {
                    NameLabelColor = Color.Transparent;
                }
                else
                {
                    NameLabelColor = Color.White;
                }
            }
            catch
            {
                NameLabelColor = Color.White;
            }

        }

    }
}
