using System.Reflection;
using System.Windows;
using LiaoTian_Cup.Helper;

namespace LiaoTian_Cup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // 初始化数据库读取
            _ = new FileData();
            InitializeComponent();
            this.Height = SystemParameters.PrimaryScreenHeight * (910d / 1080);
            this.Width = SystemParameters.PrimaryScreenWidth * (1180d / 1920);
        }
    }
}
