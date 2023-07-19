using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiaoTian_Cup
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_RandomMutation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("RandomMutationWindow.xaml", UriKind.Relative));
        }

        private void Button_Negative_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("NegativeFactorWindow.xaml", UriKind.Relative));
        }

        private void Button_Doubles_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("DoublesModeWindow.xaml", UriKind.Relative));
        }

        private void Button_Single_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SingleModeWindow.xaml", UriKind.Relative));
        }
    }
}
