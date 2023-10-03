using LiaoTian_Cup.Dictionary.I18n;
using LiaoTian_Cup.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        // i18n
        /// <summary>
        /// 点击按钮赋值语言的类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I18nBtn(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Content.ToString().Equals("English"))
            {
                LanguageManager.Instance.ChangeLanguage(new CultureInfo("en-US"));
            }
            else
            {
                LanguageManager.Instance.ChangeLanguage(new CultureInfo("zh-CN"));
            }
           
        }

        private void Button_RandomMutation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LiaoTian_Cup;component/Mode/RandomMutationWindow.xaml", UriKind.Relative));
        }

        private void Button_Negative_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LiaoTian_Cup;component/Mode/NegativeFactorWindow.xaml", UriKind.Relative));
        }

        private void Button_Doubles_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LiaoTian_Cup;component/Mode/DoublesModeWindow.xaml", UriKind.Relative));
        }

        private void Button_Single_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LiaoTian_Cup;component/Mode/SingleModeWindow.xaml", UriKind.Relative));
        }

        private void Button_USuck_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LiaoTian_Cup;component/Mode/USuckModeWindow.xaml", UriKind.Relative));
        }

        private void Button_Hub_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("/LiaoTian_Cup;component/Mode/HubModeWindow.xaml", UriKind.Relative));
        }

        private void AboutMeBtn(object sender, RoutedEventArgs e)
        {
            string url = "https://github.com/B1ackSand";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
}
