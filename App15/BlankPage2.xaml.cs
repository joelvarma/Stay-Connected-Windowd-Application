
using Bing.Maps;
using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class BlankPage2 : Page
    {
        public BlankPage2()
        {
            this.InitializeComponent();
        }
        Geoposition gp;
        Geolocator gl = new Geolocator();
        Location l2 = new Location();
        public string u_name = "",glofname;
        Button b;

        private async void t_c(object sender, RoutedEventArgs e)
        {

            try
            {



                gp = await gl.GetGeopositionAsync();
                pr.Visibility = Visibility.Visible;
                //  Point p;
                //  List<detail> rt=new List<detail>();
                List<detail> rt = await MainPage.MobileService.GetTable<detail>().Where(op => op.username==b.Content.ToString()).ToListAsync();
                Point p;
                
                if(rt.Count==1)
                {
                    var nomore = new MessageDialog("Displaying past one location of this contact.....").ShowAsync();

                }
                else
                {
                    var moree = new MessageDialog("Displaying past locations of this contact.....").ShowAsync();

                }
                foreach (detail d in rt)
                {

                    p.X = Convert.ToDouble(d.latitude);


                    p.Y = Convert.ToDouble(d.longitude);




                    l2.Longitude = p.Y;
                    l2.Latitude = p.X;

                    map1.SetView(l2, 15);
                    pr.Visibility = Visibility.Collapsed;


                    // Tb1.Text= gp.CivicAddress.City; 
                    //Children.Add(pp1);//pp1 is push pin name	
                    MapLayer.SetPosition(pp1, l2);


                    //MessageDialog a = new MessageDialog("Your Family member" + b.Content + " is here......");
                    //await a.ShowAsync();
                }

            }
            catch (Exception ex)
            {
              //  var m1 = new MessageDialog("Sorry"+ex).ShowAsync();

            }


        }
        Location loc = new Location();
        Geoposition geo;
        Geolocator geolo = new Geolocator();
        List<string> rec;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pr.Visibility = Visibility.Collapsed;
            rect1.Visibility = Visibility.Collapsed;
            // go.Visibility = Visibility.Collapsed;
            go_Copy.Visibility = Visibility.Collapsed;
            useremailtxt.Visibility = Visibility.Collapsed;
            addfrnd.Visibility = Visibility.Collapsed;
            sv.Visibility = Visibility.Collapsed;
            sp.Visibility = Visibility.Collapsed;
            cl.Visibility = Visibility.Collapsed;
            rec = e.Parameter as List<string>;
           // var m11 = new MessageDialog(rec[0] + "" + rec[1]).ShowAsync();

            try
            {
                var path = ApplicationData.Current.LocalFolder.Path + @"/frnd.db";
                var db = new SQLiteConnection(path);
                db.CreateTable<fname>();

            }
            catch (Exception)
            {

            }



        }

        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // var app = ;

                geo = await geolo.GetGeopositionAsync();
                //loc.Latitude = geo.Coordinate.Latitude;
                //loc.Longitude = geo.Coordinate.Longitude;
                // map1.SetView(loc, 10);
                Point p;
                p.X = geo.Coordinate.Latitude;
                p.Y = geo.Coordinate.Longitude;
                // IMobileServiceTable<detail>
                detail u2 = new detail
                {
                    latitude = p.X.ToString(),
                    longitude = p.Y.ToString()

                };

                MainPage.MobileService.GetTable<detail>().Where(upp => upp.email == rec[0]).IncludeDeleted();




                // MapLayer.StPosition(pp1, l1);
            }
            catch (Exception)
            {

            }



        }
        List<detail> rt = new List<detail>();
        private async void gocopy_onclicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = useremailtxt.Text;
                int i = 0;
                var path = ApplicationData.Current.LocalFolder.Path + @"/frnd.db";
                var db = new SQLiteConnection(path);
                // List<users2> names=db1.Query<users2>("Select * from users2");


                if (email != null)
                {

                    rt = await MainPage.MobileService.GetTable<detail>().Where(opp => opp.email == useremailtxt.Text).ToListAsync();
                    foreach (detail d in rt)
                    {
                        u_name = d.username;
                        //var n = new MessageDialog("This user is available to connect"+ u_name + "").ShowAsync();
                        i = 1;

                    }
                    fname u2 = new fname
                    {
                        friendname = u_name

                    };
                    db.Insert(u2);


                    if (i == 1)
                    {
                        if (u_name != null)
                        {

                            Button b1 = new Button();
                            b1.Content = u_name;

                            b1.Click += b1_Click;
                            b1.MaxWidth = 100;


                            sp.Children.Add(b1);
                        }
                    }
                    else
                    {
                        var no = new MessageDialog("Please enter the valid email id").ShowAsync();
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }




        }
        TextBlock tb1;
        private void tb1_GotFocus(object sender, RoutedEventArgs q)
        {
            tb1.Visibility = Visibility.Collapsed;

        }
        private void l_clicked(object sender, RoutedEventArgs e)
        {
            rect1.Visibility = Visibility.Collapsed;
            // go.Visibility = Visibility.Collapsed;
            go_Copy.Visibility = Visibility.Collapsed;
            useremailtxt.Visibility = Visibility.Collapsed;
            addfrnd.Visibility = Visibility.Collapsed;
            sv.Visibility = Visibility.Collapsed;
            sp.Visibility = Visibility.Collapsed;


        }

        private async void u_c(object sender, RoutedEventArgs e)
        {
            Geoposition gp;
            Geolocator l = new Geolocator();
            Location lo = new Location();
            pr.Visibility = Visibility.Visible;

            //DispatcherTimer t = TimeSpan.FromSeconds(3);
            gp = await l.GetGeopositionAsync();
            Point p;
            p.X = gp.Coordinate.Latitude;
            p.Y = gp.Coordinate.Longitude;
            detail u2 = new detail
            {
                email = rec[0],
                latitude = p.X.ToString(),
                longitude = p.Y.ToString(),
                username = rec[1]
            };

            await MainPage.MobileService.GetTable<detail>().InsertAsync(u2);
            pr.Visibility = Visibility.Collapsed;
            var update = new MessageDialog("Your current Location Updated").ShowAsync();
            

        }
        int jj;
        string a;
        int k;

       // int visi = 0;
        private void vi_c(object sender, RoutedEventArgs e)
        {
            rect1.Visibility = Visibility.Visible;
            // go.Visibility = Visibility.Visible;
            go_Copy.Visibility = Visibility.Visible;
            useremailtxt.Visibility = Visibility.Visible;
            addfrnd.Visibility = Visibility.Visible;
            sv.Visibility = Visibility.Visible;
            cl.Visibility = Visibility.Visible;

            sp.Visibility = Visibility.Visible;

            try
            {
                var path = ApplicationData.Current.LocalFolder.Path + @"/frnd.db";
                var db = new SQLiteConnection(path);
                List<fname> names = db.Query<fname>("Select * from frnd");
                foreach (fname pfn in names)
                {
                    Button b2 = new Button();
                    if (pfn.friendname != null)
                    {
                        b2.Content = pfn.friendname;
                        if (jj==0)
                        {
                            sp.Children.Add(b2);
                        }
                        
                    }
                    b2.Click += b2_Click;
                    k++;
                }
                jj = 1;
            }
            catch (Exception)
            {

            }


        }
        
        void b2_Click(object sender, RoutedEventArgs e)
        {
            b = sender as Button;
            var v = new MessageDialog(b.Content+"").ShowAsync();

            rect1.Visibility = Visibility.Collapsed;
            // go.Visibility = Visibility.Collapsed;
            go_Copy.Visibility = Visibility.Collapsed;
            useremailtxt.Visibility = Visibility.Collapsed;
            addfrnd.Visibility = Visibility.Collapsed;
            sv.Visibility = Visibility.Collapsed;
            sp.Visibility = Visibility.Collapsed;
            cl.Visibility = Visibility.Collapsed;

            t_c(sender, e);

        }

        void b1_Click(object sender, RoutedEventArgs e)
        {
            b = sender as Button;
           
            rect1.Visibility = Visibility.Collapsed;
            // go.Visibility = Visibility.Collapsed;
            go_Copy.Visibility = Visibility.Collapsed;
            useremailtxt.Visibility = Visibility.Collapsed;
            addfrnd.Visibility = Visibility.Collapsed;
            sv.Visibility = Visibility.Collapsed;
            sp.Visibility = Visibility.Collapsed;
            cl.Visibility = Visibility.Collapsed;
            t_c(sender, e);


        }

        private void back_c(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));

        }

        private void lc_c(object sender, RoutedEventArgs e)
        {
            rect1.Visibility = Visibility.Collapsed;
            // go.Visibility = Visibility.Collapsed;
            go_Copy.Visibility = Visibility.Collapsed;
            useremailtxt.Visibility = Visibility.Collapsed;
            addfrnd.Visibility = Visibility.Collapsed;
            sv.Visibility = Visibility.Collapsed;
            sp.Visibility = Visibility.Collapsed;
            cl.Visibility = Visibility.Collapsed;


        }


        string u;
        double[] dis=new double[10];

        List<detail> r;
        private async void everyone_c(object sender, RoutedEventArgs e)
        {
            

             var path = ApplicationData.Current.LocalFolder.Path + @"/frnd.db";
              var db = new SQLiteConnection(path);
               
            pr.Visibility = Visibility.Visible;
             r = await MainPage.MobileService.GetTable<detail>().ToListAsync();
            Geoposition gp1;
            Geolocator l1 = new Geolocator();
            Location lo1 = new Location();

            gp1 = await l1.GetGeopositionAsync();
            Point p;
            p.X = gp1.Coordinate.Latitude;
            p.Y = gp1.Coordinate.Longitude;
           // fname loc = new fname();
           calcdist(p);
           int j = 0;
           foreach (detail w in r)
           {
               List<fname> st = db.Query<fname>("Select * from frnd");
               foreach (fname ff in st)
               {

                   if (ff.friendname == w.username)
                   {
                       if (dis[j] < 100)
                       {
                           if (dis[j] / 2 < 50)
                           {
                               if (dis[j] / 3 < 40)
                               {  
                                   u += w.username + "\n";
                               }
                           }
                       }
                   }
                  

               }
               j++;

           }
               //foreach (detail w in r)
               //{
               //    Point p1, p2;
               //    p1.X = Convert.ToDouble(w.latitude);
               //    p1.Y = Convert.ToDouble(w.longitude);
               //    p2.X = p1.X - 0.30;
               //    p2.Y = p1.Y - 0.30;

               //    p1.X = p1.X + 0.30;
               //    p1.Y = p1.Y + 0.30;
               //    List<fname> st = db.Query<fname>("Select * from frnd");
               //    foreach (fname ff in st)
               //    {
               //        if (ff.friendname == w.username)
               //        {
               //            if (((p.X <= p1.X) && (p.X >= p2.X)) && ((p.Y <= p1.Y) && (p.Y >= p2.Y)))
               //            {
               //                u += w.username + "\n";
               //            }
               //        }
               //    }
               //}
            pr.Visibility = Visibility.Collapsed;
            string i = "No people around you!!!";
            string ii = "Your family members near you:";

            var ms = new MessageDialog((u==null)?i:ii + u).ShowAsync();




        }
        int i = 0;
        public void calcdist(Point p)
        {
             i = 0;
            double ddistance=0;
            foreach (detail w in r)
            {
                Point p1, p2;
                p1.X = Convert.ToDouble(w.latitude);
                p1.Y = Convert.ToDouble(w.longitude);
                double dLat1InRad = p.X * (Math.PI / 180.0);
                double dLong1InRad = p.Y* (Math.PI / 180.0);
                double dLat2InRad = p1.X * (Math.PI / 180.0);
                double dLong2InRad =p1.Y * (Math.PI / 180.0);
                double dLongitude = dLong2InRad - dLong1InRad;
                double dLatitude = dLat2InRad - dLat1InRad;
                double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                   Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                   Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

                // Intermediate result c (great circle distance in Radians).
                double c = 2.0 * Math.Asin(Math.Sqrt(a));

                // Distance.
                // const Double kEarthRadiusMiles = 3956.0;
                const Double kEarthRadiusKms = 6376.5;
                ddistance = kEarthRadiusKms * c;
                dis[i++] = ddistance;

                
                //double theta = p.Y - 78.4760;
               // dist = Math.Sin(deg2rad(p.X)) * Math.Sin(deg2rad(17.3660)) + Math.Cos(deg2rad(p.X)) * Math.Cos(deg2rad(17.3660)) * Math.Cos(deg2rad(theta));
                //dist = Math.Acos(dist);
                //dist = RadianToDegree(dist);
                //dist = dist * 60 * 1.1515;
                //dist = dist * 1.609344;



            }
           // return ddistance;
            //return dist;



        }
        private double deg2rad(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private async void h_c(object sender, RoutedEventArgs e)
        {
            MessageDialog help = new MessageDialog("\nLook who's around you will prompt you to enter your friends email\nTrack button will locate all your friends in your vicinity ranging 30 kilometres\nUpdate button will update your current location ,mostly when you travel to cities it will be useful to locate your old friends");
            await help.ShowAsync();
        }




    }
}


