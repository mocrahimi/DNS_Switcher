using ControlzEx.Standard;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DNS_Switcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Animations anim = new Animations();
        Network net = new Network();

        //UI page change Animation
        private void transaction(FrameworkElement element)
        {

            FrameworkElement[] mainPages = { Main_Page, SpeedCheck_Page };
            List<FrameworkElement> tabPages = new List<FrameworkElement>(mainPages);

            foreach (FrameworkElement page in tabPages)
            {
                if (page != element)
                {
                    anim.hideAnimation(page, 300);
                }
                else
                    anim.showAnimation(page, 300, 0);
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
           
        }

        private void ExitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            transaction(Main_Page);
        }

        private void SpeedCheck_Button_Click(object sender, RoutedEventArgs e)
        {
            transaction(SpeedCheck_Page);
        }

        private void MainBackground_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUiInfo();
            
        }
        public void GetPing(string IP)
        {
            Thread.Sleep(2000);//for better visual animation
            long totalTime = 0;
            int timeout = 120;
            Ping pingSender = new Ping();

            for (int i = 0; i < 16; i++)
            {
                PingReply reply = pingSender.Send(IP, timeout);
                if (reply.Status == IPStatus.Success)
                {
                    totalTime += reply.RoundtripTime;
                }
            }
            this.Dispatcher.Invoke(() => {
                 Ping_Label.Text = (totalTime / 16).ToString();
                 Ping_Label.Visibility = Visibility.Visible;
                 PingProgress.Visibility = Visibility.Collapsed;
                 MS_Label.Visibility = Visibility.Visible;
            });
            
            
        }
        //change dns to selected dns
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          if(DNS_Lists.SelectedItem == Shecan)
          {
                net.SetDNS(Settings.Default.Shecan);
                RefreshUiInfo();
          }
          else if(DNS_Lists.SelectedItem == Begzar){

                net.SetDNS(Settings.Default.Begzar);
                RefreshUiInfo();
            } 
          else if (DNS_Lists.SelectedItem == Elctro){

                net.SetDNS(Settings.Default.Electro);
                RefreshUiInfo();
            }
          else if (DNS_Lists.SelectedItem == Cloudflare)
            {

                net.SetDNS(Settings.Default.Cloudflare);
                RefreshUiInfo();
            } 
          else if (DNS_Lists.SelectedItem == Google){

                net.SetDNS(Settings.Default.Google);
                RefreshUiInfo();
            }



        }
        //updating the UI with new dns and 
        private void RefreshUiInfo()
        {
            List<string> DNSList = new List<string>();
            DNSList = net.DisplayDnsAddresses();
            if (DNSList.Count > 0)
            {
                DNS1.Text = DNSList[0];
            }
            else
            {
                notification_Label.Text= "شبکه اینترنت یافت نشد";
                notification_Border.Background = new SolidColorBrush(Color.FromArgb(255, 0xFF, 0, 0));
                anim.ShowNotifcation(notification_Border, 500, 4500);
                return;

            }
            if (DNSList.Count > 1)
            {
                DNS2.Text = DNSList[1];
            }else
            {
                DNS2.Text = "";
            }

            this.Dispatcher.Invoke(() => {
                Ping_Label.Visibility = Visibility.Collapsed;
                PingProgress.Visibility = Visibility.Visible;
                MS_Label.Visibility = Visibility.Collapsed;
            });
            Task.Run(() => GetPing(DNSList[0]));
        }

        private void RemoveDNSButton_Click(object sender, RoutedEventArgs e)
        {
            net.RemoveDNS();
            RefreshUiInfo();
        }
        
        private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            
           bool result = net.FlushDNS();
            if (result)
            {
                notification_Label.Text= "حافظه کش پاک شد";
                notification_Border.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 50));
                anim.ShowNotifcation(notification_Border,500,2500);//show a notification
            }
            else
            {
                notification_Label.Text = "عملیات با شکست مواجه شد";
                notification_Border.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0xFF, 50));
                anim.ShowNotifcation(notification_Border, 500, 2500);//show a notification
            }
          
        }
        
    }
    
}
