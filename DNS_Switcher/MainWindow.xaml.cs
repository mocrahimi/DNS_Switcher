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
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
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
                net.SetDNS("178.22.122.100", "185.51.200.2");
                RefreshUiInfo();
          }
          else if(DNS_Lists.SelectedItem == Begzar){

                net.SetDNS("185.55.226.26", "185.55.225.25");
                RefreshUiInfo();
            } 
          else if (DNS_Lists.SelectedItem == Elctro){

                net.SetDNS("78.157.42.100", "78.157.42.101");
                RefreshUiInfo();
            }
          else if (DNS_Lists.SelectedItem == Cloudflare)
            {

                net.SetDNS("1.1.1.1", "1.0.0.1");
                RefreshUiInfo();
            } 
          else if (DNS_Lists.SelectedItem == Google){

                net.SetDNS("8.8.8.8", "8.8.4.4");
                RefreshUiInfo();
            }



        }
        //updating the UI
        private void RefreshUiInfo()
        {
            List<string> result = new List<string>();
            result = net.DisplayDnsAddresses();
            DNS1.Text = result[0];
            DNS2.Text = result[1];
            this.Dispatcher.Invoke(() => {
                Ping_Label.Visibility = Visibility.Collapsed;
                PingProgress.Visibility = Visibility.Visible;
                MS_Label.Visibility = Visibility.Collapsed;
            });
            Task.Run(() => GetPing(result[0]));
        }

        private void RemoveDNSButton_Click(object sender, RoutedEventArgs e)
        {
            net.RemoveDNS();
            RefreshUiInfo();
        }
    }
    
}
