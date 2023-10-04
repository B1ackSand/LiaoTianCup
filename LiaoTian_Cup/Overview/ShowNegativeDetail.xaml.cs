﻿using System.Windows;
using System.Windows.Controls;

namespace LiaoTian_Cup
{
    /// <summary>
    /// ShowNegativeDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ShowNegativeDetail : Page
    {
        //上个窗口的参数，以方便引用窗口
        private NegativeFactorWindow m_parent;
        public ShowNegativeDetail(NegativeFactorWindow parent)
        {
            m_parent = parent;
            InitializeComponent();
            showSelect();

        }
        public ShowNegativeDetail()
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

            HasSelectFactor1.Source = m_parent.HasSelectFactor1.Source;
            HasSelectFactor2.Source = m_parent.HasSelectFactor2.Source;
            HasSelectFactor3.Source = m_parent.HasSelectFactor3.Source;

            HasSelectCommander.Source = m_parent.HasSelectCommander.Source;

            AIBox.Text = m_parent.botName;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
