﻿using System.Windows;
using System.Windows.Controls;

namespace LiaoTian_Cup
{
    /// <summary>
    /// ShowDoublesDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDoublesDetail : Page
    {
        private DoublesModeWindow m_parent;
        public ShowDoublesDetail(DoublesModeWindow parent)
        {
            m_parent = parent;
            InitializeComponent();
            showSelect();
        }

        public ShowDoublesDetail()
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
            HasSelectBaseFactor1.Source = m_parent.HasSelectBaseFactor1.Source;
            HasSelectBaseFactor2.Source = m_parent.HasSelectBaseFactor2.Source;
            HasSelectBaseFactor3.Source = m_parent.HasSelectBaseFactor3.Source;

            HasSelectFactor1.Source = m_parent.HasSelectFactor1.Source;
            HasSelectFactor2.Source = m_parent.HasSelectFactor2.Source;
            HasSelectFactor3.Source = m_parent.HasSelectFactor3.Source;
            HasSelectFactor4.Source = m_parent.HasSelectFactor4.Source;
            HasSelectFactor5.Source = m_parent.HasSelectFactor5.Source;

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
