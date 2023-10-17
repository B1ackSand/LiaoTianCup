using System.Windows;
using System.Windows.Controls;

namespace LiaoTian_Cup
{
    /// <summary>
    /// ShowSingleDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ShowHubSoloDetail : Page
    {
        private HubSoloModeWindow m_parent;
        public ShowHubSoloDetail(HubSoloModeWindow parent)
        {
            m_parent = parent;
            InitializeComponent();
            showSelect();
        }

        public ShowHubSoloDetail()
        {
            InitializeComponent();
        }

        //展示已选择的因子
        private void showSelect()
        {
            //上个窗口的数据传递至本窗口
            PlayerName.Text = m_parent.PlayerName.Text;
            modeName.Text = m_parent.modeName;

            HasSelectMap.Source = m_parent.HasSelectMap.Source;
            MapTip.Text = m_parent.MapTip.Text;

            HasSelectFactor1.Source = m_parent.HasSelectFactor1.Source;
            HasSelectFactor2.Source = m_parent.HasSelectFactor2.Source;
            HasSelectFactor3.Source = m_parent.HasSelectFactor3.Source;
            HasSelectFactor4.Source = m_parent.HasSelectFactor4.Source;
            HasSelectFactor5.Source = m_parent.HasSelectFactor5.Source;
            Score.Text = m_parent.Score.Text;

            HasSelectCommander.Source = m_parent.HasSelectCommander.Source;

            AIBox.Text = m_parent.botName;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
