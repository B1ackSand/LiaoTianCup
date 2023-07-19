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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_RandomMutation_Click(object sender, RoutedEventArgs e)
        {
            RandomMutationWindow randomMutationWindow = new RandomMutationWindow();
            randomMutationWindow.Show();
            this.Hide();
        }

        private void Button_Negative_Click(object sender, RoutedEventArgs e)
        {
            NegativeFactorWindow negativeFactorWindow = new NegativeFactorWindow();
            negativeFactorWindow.Show();
            this.Hide();
        }

        private void Button_Doubles_Click(object sender, RoutedEventArgs e)
        {
            DoublesModeWindow doublesModeWindow = new DoublesModeWindow();
            doublesModeWindow.Show();
            this.Hide();
        }

        private void Button_Single_Click(object sender, RoutedEventArgs e)
        {
            SingleModeWindow singleModeWindow = new SingleModeWindow();
            singleModeWindow.Show();
            this.Hide();
        }
    }

    
}
