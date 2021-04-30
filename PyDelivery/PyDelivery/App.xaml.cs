using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PyDelivery.Services;
using PyDelivery.Views;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using PyDelivery.Models;
using Plugin.LatestVersion;
using System.Diagnostics;
using PyDelivery.Controls;

namespace PyDelivery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private const string logIn_Status = "logInStatus";
        private const string user_Id = "UserId";
        private const string user_Role = "UserRole";
        private const string user_Name = "UserName";
        private const string user_PhoneNumber = "UserPhoneNumber";
        private const string fireBaseToken = "fireBaseToken";
        private const string user_Key = "UserKey";
        public VersionAppInfo AppInfo { get; set; }
        public string FireBaseToken
        {
            get
            {
                if (Properties.ContainsKey(fireBaseToken))
                    return (string)Properties[fireBaseToken];
                return null;
            }
            set
            {
                Properties[fireBaseToken] = value;
            }
        }
        public bool IsLoggedIn
        {
            get
            {
                if (Properties.ContainsKey(logIn_Status))
                    return (bool)Properties[logIn_Status];
                return false;
            }
            set
            {
                Properties[logIn_Status] = value;
            }
        }
        public string UserId
        {
            get
            {
                if (Properties.ContainsKey(user_Id))
                    return (string)Properties[user_Id];
                return null;
            }
            set
            {
                Properties[user_Id] = value;
            }
        }

        public string UserKey
        {
            get
            {
                if (Properties.ContainsKey(user_Key))
                    return (string)Properties[user_Key];
                return null;
            }
            set
            {
                Properties[user_Key] = value;
            }
        }
        public string UserName
        {
            get
            {
                if (Properties.ContainsKey(user_Name))
                    return (string)Properties[user_Name];
                return null;
            }
            set
            {
                Properties[user_Name] = value;
            }
        }
        public string UserPhoneNumber
        {
            get
            {
                if (Properties.ContainsKey(user_PhoneNumber))
                    return (string)Properties[user_PhoneNumber];
                return null;
            }
            set
            {
                Properties[user_PhoneNumber] = value;
            }
        }
        public string UserRole
        {
            get
            {
                if (Properties.ContainsKey(user_Role))
                    return (string)Properties[user_Role];
                return null;
            }
            set
            {
                Properties[user_Role] = value;
            }
        }
       
        public App()
        {
            InitializeComponent();
            
                if (!IsLoggedIn)
            {
               
                  MainPage = new LogInPage();
                               
            }
            else
            {
                MainPage = new MainPage();
                
            }

            /*DependencyService.Register<MockDataStore>();
            new Task(() => RetrieveToken()).Start();*/
        }

       
        protected override void OnStart()
        {
           

            //app center analytics

            AppCenter.Start("android=92f8578d-a4a3-494c-bade-cf619d7243f0;",
                   typeof(Analytics), typeof(Crashes));

            // check version function called
            CheckVersion();
                       
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        //checking installed version
        async void CheckVersion()
        {
            try
            {
                AppInfo = new VersionAppInfo();
                VersionApiData VData = await VersionService.Instance.CheckVersion("0");
                AppInfo = VData.AppInfo;
                var NewVersion = AppInfo.AppVersion;
                var Message = AppInfo.MessageForUpdateVersion;
                var url = AppInfo.PlayStoreURL;
                var UpdateType = AppInfo.UpdateType;

                string installedVersionNumber = CrossLatestVersion.Current.InstalledVersionNumber;
                Debug.WriteLine("Latest Version Number :", installedVersionNumber);
                double previousversion = Double.Parse(installedVersionNumber);
                Debug.WriteLine("Version Number :", previousversion);

                // update is optional
                if ((previousversion < NewVersion) && (UpdateType == "Optional"))
                {
                    var update = await App.Current.MainPage.DisplayAlert("New Version", Message, "Yes", "No");

                    if (update)
                    {
                        Xamarin.Forms.Device.OpenUri(new Uri(url));

                    }
                }
                else if ((previousversion < NewVersion) && (UpdateType == "Mandatory"))
                {
                    var update = await App.Current.MainPage.DisplayAlert("New Version", Message, "Yes", "No");

                    if (update)
                    {
                        Xamarin.Forms.Device.OpenUri(new Uri(url));
                       
                    }
                    else
                    {
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }

            }
            catch (Exception ex)
            {
            }

        }

    }
}
