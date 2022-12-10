using ControlzEx.Standard;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
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
    }
}
