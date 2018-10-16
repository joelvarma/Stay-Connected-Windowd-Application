using Bing.Maps;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App15
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        Geoposition gp ;
        Geolocator l = new Geolocator();
        Location lo = new Location();
        private async void log_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
               
                var path = ApplicationData.Current.LocalFolder.Path + @"/users1.db";
                var db = new SQLiteConnection(path);
                gp = await l.GetGeopositionAsync();
                user u1 = new user
                {
                    u_name = usrnametext.Text,
                    passwd = pwdlogup.Password,
                    email  =emailtxt.Text

                };
                db.Insert(u1);
                MessageDialog m = new MessageDialog("Successfully inserted details"+u1.u_name+u1.email);
                await m.ShowAsync();
                
                Point p;
                p.X = gp.Coordinate.Latitude;
                p.Y = gp.Coordinate.Longitude;
                detail u2 = new detail
                {

                    email=emailtxt.Text,
                    latitude=p.X.ToString(),
                    longitude=p.Y.ToString()

                };

                await MainPage.MobileService.GetTable<detail>().InsertAsync(u2);
               
                
                MessageDialog m1 = new MessageDialog("Successfuly inserted your coordinates"+gp.Coordinate.Latitude+","+gp.Coordinate.Longitude);
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
