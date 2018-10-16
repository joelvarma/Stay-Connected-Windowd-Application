using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Devices.Geolocation;
using Bing.Maps;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App15
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            one.Begin();

        }

          public static MobileServiceClient MobileService = new MobileServiceClient(
         "https://stayconnected.azure-mobile.net/",
         "WTMHAfpMgaIZgNjhsktiWryBrvEKGN30"
        );
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var path = ApplicationData.Current.LocalFolder.Path + @"/users1.db";
                var db = new SQLiteConnection(path);
                db.CreateTable<user>();
                rect.Visibility = Visibility.Collapsed;
                head.Visibility = Visibility.Collapsed;

                prhome.Visibility = Visibility.Collapsed;

            }
            catch
            {

            }
        }

        private void login_clicked(object sender, RoutedEventArgs e)
        {
            string a = "";
             try
            {
            //    Class1 u = new Class1();
                var path = ApplicationData.Current.LocalFolder.Path + @"/users1.db";
                var db = new SQLiteConnection(path);
               // string ret_un = "", ret_pwd = "";
                List<user> allusers = db.Query<user>("Select * from users1 where email='"+usrtxt.Text+"' and passwd='"+pwdlogin.Password+"'");
               
                foreach(user un in allusers)
                {
                     a = un.u_name;

                }
                 

                 List<string> send = new List<string>();
                send.Add(usrtxt.Text);
                send.Add(a);

                 if(allusers.Count==1)
                 {
                     this.Frame.Navigate(typeof(BlankPage2),send);

                 }
                 else if (allusers.Count<=0 || allusers.Count>=0)
                 {
                     var m5 = new MessageDialog("invalid login").ShowAsync();
                 }
                
              //  foreach (user u in allusers)
              //    {
                     
              ////        if((u.email==usrtxt.Text)&&(u.passwd==pwdlogin.Password))
              //        {
              //            this.Frame.Navigate(typeof(BlankPage2));

              //        }
                                       
              //    }
                
    

            }
             catch
             {

             }
        }

        private async void logup_clicked1(object sender, RoutedEventArgs e)
        {
            
            Geoposition gp;
            Geolocator l = new Geolocator();
            Location lo = new Location();

            try
            {
                rect.Visibility = Visibility.Visible;
                head.Visibility = Visibility.Visible;

               // prhome.Visibility = Visibility.Collapsed;
                prhome.Visibility = Visibility.Visible;

                var path = ApplicationData.Current.LocalFolder.Path + @"/users1.db";
                var db = new SQLiteConnection(path);
                user u1 = new user
                {
                    u_name = usrnametext.Text,
                    passwd = pwdlogup.Password,
                    email = emailtxt.Text

                };
                
                db.Insert(u1);
                MessageDialog m = new MessageDialog("Successful \nInserted details" + u1.u_name + u1.email);
                await m.ShowAsync();
                gp = await l.GetGeopositionAsync();
                Point p;
                p.X = gp.Coordinate.Latitude;
                p.Y = gp.Coordinate.Longitude;
                detail u2 = new detail
                {

                    email = emailtxt.Text,
                    latitude = p.X.ToString(),
                    longitude = p.Y.ToString(),
                    username=usrnametext.Text
                };

                await MainPage.MobileService.GetTable<detail>().InsertAsync(u2);
                prhome.Visibility = Visibility.Collapsed;
               

                MessageDialog m1 = new MessageDialog("Successfuly located you!!!");
                await m1.ShowAsync();
                this.Frame.Navigate(typeof(MainPage));

            }
            catch (Exception)
            {
                var m2 = new MessageDialog("exception occurred");
            }

        }

        
    }
}
