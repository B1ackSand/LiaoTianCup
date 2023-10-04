using System.Windows;
using System.Windows.Controls;

namespace LiaoTian_Cup
{
    /// <summary>
    /// ShowRMDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ShowRMDetail : Page
    {
        public ShowRMDetail()
        {
            InitializeComponent();
        }

        //上个窗口的参数，以方便引用窗口
        private RandomMutationWindow m_parent;
        public ShowRMDetail(RandomMutationWindow parent)
        {
            m_parent = parent;
            InitializeComponent();
            showSelect();

            //返回时重置显示
            parent.MutationBox.Text = "";
            parent.MapBox.Text = "";
        }


        //展示已选择的因子
        private void showSelect()
        {
            //上个窗口的数据传递至本窗口
            PlayerName.Text = m_parent.PlayerName.Text;

            MutationBox.Text = m_parent.MutationBox.Text;
            MapBox.Text = m_parent.MapBox.Text;
            ChkDoubles.IsChecked = m_parent.ChkDoubles.IsChecked;

            MapImg.Source = m_parent.MapImg.Source;
            Factor1.Source = m_parent.Factor1.Source;
            Factor2.Source = m_parent.Factor2.Source;
            Factor3.Source = m_parent.Factor3.Source;

            HasSelectFactor1.Source = m_parent.HasSelectFactor1.Source;
            HasSelectFactor2.Source = m_parent.HasSelectFactor2.Source;
            HasSelectFactor3.Source = m_parent.HasSelectFactor3.Source;
            HasSelectFactor4.Source = m_parent.HasSelectFactor4.Source;
            HasSelectFactor5.Source = m_parent.HasSelectFactor5.Source;
            HasSelectFactor6.Source = m_parent.HasSelectFactor6.Source;
            HasSelectFactor7.Source = m_parent.HasSelectFactor7.Source;
            HasSelectFactor8.Source = m_parent.HasSelectFactor8.Source;

            HasSelectCommander1.Source = m_parent.HasSelectCommander1.Source;
            HasSelectCommander2.Source = m_parent.HasSelectCommander2.Source;

            AIBox.Text = m_parent.botName;

            
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
