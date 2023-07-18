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
using System.Windows.Shapes;

namespace LiaoTian_Cup
{
    /// <summary>
    /// ShowDoublesDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDoublesDetail : Window
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

        private void showSelect()
        {

        }
    }
}
