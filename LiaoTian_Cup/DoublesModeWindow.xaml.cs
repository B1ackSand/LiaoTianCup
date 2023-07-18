using Force.DeepCloner;
using LiaoTian_Cup.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
    /// DoublesModeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DoublesModeWindow : Window, INotifyPropertyChanged
    {
        //路径
        private readonly string baseNegativeFactorFilePath = "./Resources/纯正面因子基础库.csv";
        private readonly string baseMultiFactorFilePath = "./Resources/多线因子基础库.csv";
        private readonly string mutationFactorPath = "./Resources/突变因子列表.csv";
        private readonly string beforeCommanderFilePath = "./Resources/先出指挥官列表.csv";
        private readonly string afterCommanderFilePath = "./Resources/后出指挥官列表.csv";
        private readonly string aIFilePath = "./Resources/电脑AI.csv";
        private readonly string mapsFilePath = "./Resources/所有地图.csv";

        //存放因子库CSV中得到的数据
        private List<string> baseNegativeFactorInfo = new List<string>();
        private List<string> baseMultiFactorInfo = new List<string>();
        private List<string> mutationFactorList = new List<string>();

        //存放先出指挥官CSV中得到的数据
        private List<string> beforeCommanderInfo = new List<string>();
        //存放后出指挥官CSV中得到的数据
        private List<string> afterCommanderInfo = new List<string>();

        //存放所有的人机CSV中得到的数据
        private List<string> botInfo = new List<string>();
        public string botName = "暂未随机AI";

        //存放地图数据
        private List<string> mapsInfo = new List<string>();

        //链表，存放自选因子
        private List<Image> hasSelectBaseNegative = new List<Image>();
        private List<Image> hasSelectBaseMulti = new List<Image>();
        private List<Image> hasSelectFactor = new List<Image>();
        private List<Image> hasSelectCommander = new List<Image>(2);
        private Image hasSelectMap = new Image();


        //初始化
        RandomKit rk = new RandomKit();
        public event PropertyChangedEventHandler PropertyChanged;

        public DoublesModeWindow()
        {
            //初始化窗口时即拿数据
            CSVKit.Csv2Dt(baseNegativeFactorFilePath, baseNegativeFactorInfo);
            CSVKit.Csv2Dt(mutationFactorPath, mutationFactorList);
            CSVKit.Csv2Dt(baseMultiFactorFilePath, baseMultiFactorInfo);
            CSVKit.Csv2Dt(beforeCommanderFilePath, beforeCommanderInfo);
            CSVKit.Csv2Dt(afterCommanderFilePath, afterCommanderInfo);
            CSVKit.Csv2Dt(aIFilePath, botInfo);
            CSVKit.Csv2Dt(mapsFilePath, mapsInfo);
            InitializeComponent();
            this.DataContext = this;
        }

        //是否允许随机AI逻辑
        private bool _isRandAI;
        public bool isRandAI
        {
            get { return _isRandAI; }
            set
            {
                _isRandAI = value;
                RaisePropertyChanged(nameof(isRandAI));
            }
        }

        //玩家名响应相关逻辑
        private string _playName;
        public string playName
        {
            get { return _playName; }
            set
            {
                _playName = value;
                RaisePropertyChanged(nameof(playName));
            }
        }

        //模式选择（5因子模式,8因子模式）数据
        private string _modeName = "5因子模式";
        public string modeName
        {
            get { return _modeName; }
            set
            {
                _modeName = value;
                RaisePropertyChanged(nameof(_modeName));
            }
        }

        //是否随机AI的处理逻辑
        private string IsRandAIFunc()
        {
            if (isRandAI)
            {
                Random rand = new Random();
                int number = rand.Next(0, botInfo.Count);
                return botInfo[number];
            }
            else
            {
                return "暂未随机AI";
            }
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetFunc();
        }

        public void ResetFunc()
        {
            ModeBox.IsEnabled = true;
            SetRandMapEnable(true);
            SetBaseFactorEnable(true);

            hasSelectBaseNegative.Clear();
            ClearBaseNegativeFactor();
            FlashSelectNegativeBase();

            hasSelectBaseMulti.Clear();
            ClearBaseMultiFactor();
            FlashSelectMultiBase();

            hasSelectFactor.Clear();
            ClearRandomFactor();
            FlashHasSelectFactor();

            hasSelectCommander.Clear();
            clearRandomCommander();
            FlashHasSelectCommander();

            hasSelectMap = new Image();
            ClearRandomMaps();
            FlashHasSelectMap();
        }

        //开始随机事件响应
        private void Button_Random_Click(object sender, RoutedEventArgs e)
        {
            ModeBox.IsEnabled = false;
            ShowRandomMaps();
        }

        //随机地图显示清除
        private void ClearRandomMaps()
        {
            MapImg1.Source = new BitmapImage();
            MapImg2.Source = new BitmapImage();
            MapImg3.Source = new BitmapImage();
        }

        //返回主页事件响应
        private void Button_BackMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //随机地图显示
        private void ShowRandomMaps()
        {
            hasSelectMap = new Image();
            FlashHasSelectMap();
            List<int> randNums = rk.GenerateXRandomNum(3, mapsInfo.Count);
            MapImg1.Source = new BitmapImage(new Uri("./Resources/maps/" + mapsInfo[randNums[0]] + ".png", UriKind.Relative));
            MapImg2.Source = new BitmapImage(new Uri("./Resources/maps/" + mapsInfo[randNums[1]] + ".png", UriKind.Relative));
            MapImg3.Source = new BitmapImage(new Uri("./Resources/maps/" + mapsInfo[randNums[2]] + ".png", UriKind.Relative));
        }

        //点击地图图片事件响应
        private void Maps_MouseDown(object sender, MouseEventArgs e)
        {
            CommanderWarn.Text = "";
            Image selectMap = (Image)sender;
            if (selectMap != null)
            {
                hasSelectMap = selectMap;
            }
            FlashHasSelectMap();
        }

        //取消当前选择的地图事件响应
        private void CancelMap_MouseDown(Object sender, RoutedEventArgs e)
        {
            Image cancelMap = (Image)sender;
            if (cancelMap != null)
            {
                hasSelectMap = new Image();
            }
            else { return; }
            FlashHasSelectMap();
        }

        //刷新选择的地图
        private void FlashHasSelectMap()
        {
            if (hasSelectMap != null)
            {
                HasSelectMap.Source = hasSelectMap.Source;
            }
        }

        //确认地图按钮事件响应
        private void Button_MapConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (hasSelectMap == null || hasSelectMap.Source == null || hasSelectMap.Source.Equals(""))
            {
                CommanderWarn.Text = "未选择地图";
                return;
            }
            else
            {
                CommanderWarn.Text = "";
            }
            SetRandMapEnable(false);
            ShowBaseNegativeFactor();
            ShowBaseMultiFactor();
        }



        //正面因子显示
        private void ShowBaseNegativeFactor()
        {
            //相对路径URI指定因子图片来源
            NegativeFactor1.Source = new BitmapImage(new Uri("./Resources/factor/" + baseNegativeFactorInfo[0] + ".png", UriKind.Relative));
            NegativeFactor2.Source = new BitmapImage(new Uri("./Resources/factor/" + baseNegativeFactorInfo[1] + ".png", UriKind.Relative));
            NegativeFactor3.Source = new BitmapImage(new Uri("./Resources/factor/" + baseNegativeFactorInfo[2] + ".png", UriKind.Relative));
            NegativeFactor4.Source = new BitmapImage(new Uri("./Resources/factor/" + baseNegativeFactorInfo[3] + ".png", UriKind.Relative));
            NegativeFactor5.Source = new BitmapImage(new Uri("./Resources/factor/" + baseNegativeFactorInfo[4] + ".png", UriKind.Relative));
            NegativeFactor6.Source = new BitmapImage(new Uri("./Resources/factor/" + baseNegativeFactorInfo[5] + ".png", UriKind.Relative));
        }

        //多线因子显示
        private void ShowBaseMultiFactor()
        {
            MultiFactor1.Source = new BitmapImage(new Uri("./Resources/factor/" + baseMultiFactorInfo[0] + ".png", UriKind.Relative));
            MultiFactor2.Source = new BitmapImage(new Uri("./Resources/factor/" + baseMultiFactorInfo[1] + ".png", UriKind.Relative));
            MultiFactor3.Source = new BitmapImage(new Uri("./Resources/factor/" + baseMultiFactorInfo[2] + ".png", UriKind.Relative));
            MultiFactor4.Source = new BitmapImage(new Uri("./Resources/factor/" + baseMultiFactorInfo[3] + ".png", UriKind.Relative));
            MultiFactor5.Source = new BitmapImage(new Uri("./Resources/factor/" + baseMultiFactorInfo[4] + ".png", UriKind.Relative));
        }

        //点击正面因子图片事件响应
        private void NegativeBase_MouseDown(object sender, MouseEventArgs e)
        {
            FactorWarn.Text = "";
            Image selectBase = (Image)sender;
            if (selectBase != null)
            {
                if (_modeName.Equals("5因子模式") && hasSelectBaseNegative != null && !hasSelectBaseNegative.Contains(selectBase))
                {
                    if (hasSelectBaseNegative.Count + hasSelectBaseMulti.Count < 2)
                    {
                        hasSelectBaseNegative.Add(selectBase);
                    }
                }

                if (_modeName.Equals("8因子模式") && hasSelectBaseNegative != null && !hasSelectBaseNegative.Contains(selectBase))
                {
                    if (hasSelectBaseNegative.Count + hasSelectBaseMulti.Count < 3)
                    {
                        hasSelectBaseNegative.Add(selectBase);
                    }
                }
            }
            FlashSelectNegativeBase();
        }

        //点击多线因子图片事件响应
        private void MultiBase_MouseDown(object sender, MouseEventArgs e)
        {
            FactorWarn.Text = "";
            Image selectBase = (Image)sender;
            if (selectBase != null)
            {
                if (_modeName.Equals("5因子模式") && hasSelectBaseMulti != null && !hasSelectBaseMulti.Contains(selectBase))
                {
                    if (hasSelectBaseNegative.Count + hasSelectBaseMulti.Count < 2)
                    {
                        hasSelectBaseMulti.Add(selectBase);
                    }
                }

                if (_modeName.Equals("8因子模式") && hasSelectBaseMulti != null && !hasSelectBaseMulti.Contains(selectBase))
                {
                    if (hasSelectBaseNegative.Count + hasSelectBaseMulti.Count < 3)
                    {
                        hasSelectBaseMulti.Add(selectBase);
                    }
                }
            }
            FlashSelectMultiBase();
        }

        //取消当前选择的正面基础因子事件响应
        private void CancelNegativeBase_MouseDown(Object sender, RoutedEventArgs e)
        {
            Image cancelBase = (Image)sender;
            if (cancelBase != null)
            {
                for (int i = 0; i < hasSelectBaseNegative.Count; i++)
                {
                    if (hasSelectBaseNegative[i] != null
                        && hasSelectBaseNegative[i].Source.ToString().Equals(cancelBase.Source.ToString()))
                    {
                        hasSelectBaseNegative.RemoveAt(i);
                    }
                }
            }
            else { return; }
            FlashSelectNegativeBase();
        }

        //取消当前选择的多线基础因子事件响应
        private void CancelMultiBase_MouseDown(Object sender, RoutedEventArgs e)
        {
            Image cancelBase = (Image)sender;
            if (cancelBase != null)
            {
                for (int i = 0; i < hasSelectBaseMulti.Count; i++)
                {
                    if (hasSelectBaseMulti[i] != null
                        && hasSelectBaseMulti[i].Source.ToString().Equals(cancelBase.Source.ToString()))
                    {
                        hasSelectBaseMulti.RemoveAt(i);
                    }
                }
            }
            else { return; }
            FlashSelectMultiBase();
        }

        //确认基础因子按钮逻辑响应
        private void Button_BaseConfirm_Click(Object sender, RoutedEventArgs e)
        {
            FactorWarn.Text = "";
            if (_modeName.Equals("5因子模式") && hasSelectBaseNegative.Count + hasSelectBaseMulti.Count < 2)
            {
                FactorWarn.Text = "未选择足够的基础因子";
                return;
            }
            if (_modeName.Equals("8因子模式") && hasSelectBaseNegative.Count + hasSelectBaseMulti.Count < 3)
            {
                FactorWarn.Text = "未选择足够的基础因子";
                return;
            }
            SetBaseFactorEnable(false);
            ShowRandomFactor();
            RandomCommanderInfo();
        }

        //刷新已选基础因子事件
        private void FlashSelectNegativeBase()
        {
            if (hasSelectBaseNegative != null)
            {
                HasSelectNegativeFactor1.Source = hasSelectBaseNegative.Count < 1 ? null : hasSelectBaseNegative[0].Source;
                HasSelectNegativeFactor2.Source = hasSelectBaseNegative.Count < 2 ? null : hasSelectBaseNegative[1].Source;
                HasSelectNegativeFactor3.Source = hasSelectBaseNegative.Count < 3 ? null : hasSelectBaseNegative[2].Source;
            }
        }

        //刷新已选基础因子事件
        private void FlashSelectMultiBase()
        {
            if (hasSelectBaseMulti != null)
            {
                HasSelectMultiFactor1.Source = hasSelectBaseMulti.Count < 1 ? null : hasSelectBaseMulti[0].Source;
                HasSelectMultiFactor2.Source = hasSelectBaseMulti.Count < 2 ? null : hasSelectBaseMulti[1].Source;
                HasSelectMultiFactor3.Source = hasSelectBaseMulti.Count < 3 ? null : hasSelectBaseMulti[2].Source;
            }
        }


        //显示最多8个随机选择因子
        private void ShowRandomFactor()
        {
            var factorListClone = mutationFactorList.DeepClone();
            for (int i = 0; i < hasSelectBaseNegative.Count; i++)
            {

                var currentNegativeFactor = (hasSelectBaseNegative[i].Source as BitmapImage).UriSource.ToString()
                        .Replace("./Resources/factor/", "").Replace(".png", "");
                

                if (currentNegativeFactor.Equals("风暴英雄"))
                {
                    factorListClone.Remove("非同寻常的战役");
                }
                else if (currentNegativeFactor.Equals("非同寻常的战役"))
                {
                    factorListClone.Remove("风暴英雄");
                }

                factorListClone.Remove(currentNegativeFactor);
            }

            for (int i = 0; i < hasSelectBaseMulti.Count; i++)
            {
                string currentMultiFactor = (hasSelectBaseMulti[i].Source as BitmapImage).UriSource.ToString()
                       .Replace("./Resources/factor/", "").Replace(".png", "");
                factorListClone.Remove(currentMultiFactor);
            }

            List<int> randNum = rk.GenerateXRandomNum(8, factorListClone.Count);

            //相对路径URI显示6-8个因子的图片
            SelectFactor1.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[0]] + ".png", UriKind.Relative));
            SelectFactor2.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[1]] + ".png", UriKind.Relative));
            SelectFactor3.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[2]] + ".png", UriKind.Relative));
            SelectFactor4.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[3]] + ".png", UriKind.Relative));
            SelectFactor5.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[4]] + ".png", UriKind.Relative));
            SelectFactor6.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[5]] + ".png", UriKind.Relative));

            if (_modeName.Equals("8因子模式"))
            {
                SelectFactor7.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[6]] + ".png", UriKind.Relative));
                SelectFactor8.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[7]] + ".png", UriKind.Relative));
            }
        }

        //点击自选因子事件响应
        private void Factor_MouseDown(object sender, RoutedEventArgs e)
        {
            FactorWarn.Text = "";
            Image selectFactor = (Image)sender;
            if (selectFactor != null)
            {
                if (_modeName.Equals("5因子模式") && hasSelectFactor != null && !hasSelectFactor.Contains(selectFactor))
                {
                    if (hasSelectFactor.Count < 3)
                    {
                        hasSelectFactor.Add(selectFactor);
                    }

                }

                if (_modeName.Equals("8因子模式") && hasSelectFactor != null && !hasSelectFactor.Contains(selectFactor))
                {
                    if (hasSelectFactor.Count < 5)
                    {
                        hasSelectFactor.Add(selectFactor);
                    }

                }
            }
            FlashHasSelectFactor();
        }

        //点击已选择的自选因子 取消事件响应
        private void CancelFactor_MouseDown(object sender, RoutedEventArgs e)
        {
            Image cancelFactor = (Image)sender;
            if (cancelFactor != null)
            {
                for (int i = 0; i < hasSelectFactor.Count; i++)
                {
                    if (hasSelectFactor[i] != null
                        && hasSelectFactor[i].Source.ToString().Equals(cancelFactor.Source.ToString()))
                    {
                        hasSelectFactor.RemoveAt(i);
                    }
                }
            }
            else { return; }
            FlashHasSelectFactor();
        }

        //刷新已选择的因子
        private void FlashHasSelectFactor()
        {
            if(hasSelectFactor != null)
            {
                HasSelectFactor1.Source = hasSelectFactor.Count < 1 ? null : hasSelectFactor[0].Source;
                HasSelectFactor2.Source = hasSelectFactor.Count < 2 ? null : hasSelectFactor[1].Source;
                HasSelectFactor3.Source = hasSelectFactor.Count < 3 ? null : hasSelectFactor[2].Source;

                if (_modeName.Equals("8因子模式"))
                {
                    HasSelectFactor4.Source = hasSelectFactor.Count < 4 ? null : hasSelectFactor[3].Source;
                    HasSelectFactor5.Source = hasSelectFactor.Count < 5 ? null : hasSelectFactor[4].Source;
                }
            }
        }



        //随机先出和后出指挥官处理逻辑
        private void RandomCommanderInfo()
        {
            List<int> beforeRandNum = rk.GenerateXRandomNum(6, beforeCommanderInfo.Count);
            List<int> afterRandNum = rk.GenerateXRandomNum(4, afterCommanderInfo.Count);

            //相对路径URI指定指挥官图片来源
            BeforeCommander1.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[0]] + ".png", UriKind.Relative));
            BeforeCommander2.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[1]] + ".png", UriKind.Relative));
            BeforeCommander3.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[2]] + ".png", UriKind.Relative));
            BeforeCommander4.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[3]] + ".png", UriKind.Relative));
            BeforeCommander5.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[4]] + ".png", UriKind.Relative));
            BeforeCommander6.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[5]] + ".png", UriKind.Relative));

            AfterCommander1.Source = new BitmapImage(new Uri("./Resources/commander/" + afterCommanderInfo[afterRandNum[0]] + ".png", UriKind.Relative));
            AfterCommander2.Source = new BitmapImage(new Uri("./Resources/commander/" + afterCommanderInfo[afterRandNum[1]] + ".png", UriKind.Relative));
            AfterCommander3.Source = new BitmapImage(new Uri("./Resources/commander/" + afterCommanderInfo[afterRandNum[2]] + ".png", UriKind.Relative));
            AfterCommander4.Source = new BitmapImage(new Uri("./Resources/commander/" + afterCommanderInfo[afterRandNum[3]] + ".png", UriKind.Relative));
        }

        //点击自选指挥官事件响应
        private void Commander_MouseDown(object sender, RoutedEventArgs e)
        {
            CommanderWarn.Text = "";
            Image selectCommander = (Image)sender;
            if (selectCommander != null && hasSelectCommander.Count < 2)
            {
                hasSelectCommander.Add(selectCommander);
            }
            FlashHasSelectCommander();
        }

        //取消当前选择的指挥官事件响应
        private void CancelCommander_MouseDown(Object sender, RoutedEventArgs e)
        {
            Image cancelCommander = (Image)sender;
            if (cancelCommander != null)
            {
                for (int i = 0; i < hasSelectCommander.Count; i++)
                {
                    if (hasSelectCommander[i] != null
                        && hasSelectCommander[i].Source.ToString().Equals(cancelCommander.Source.ToString()))
                    {
                        hasSelectCommander.RemoveAt(i);
                    }
                }
            }
            else { return; }
            FlashHasSelectCommander();
        }

        //刷新选择的指挥官
        private void FlashHasSelectCommander()
        {
            if (hasSelectCommander != null)
            {
                HasSelectCommander1.Source = hasSelectCommander.Count < 1 ? null : hasSelectCommander[0].Source;
                HasSelectCommander2.Source = hasSelectCommander.Count < 2 ? null : hasSelectCommander[1].Source;
            }
        }


        //确认按钮事件响应
        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            botName = IsRandAIFunc();
            if (hasSelectFactor != null)
            {
                if (_modeName.Equals("5因子模式") && hasSelectFactor != null && hasSelectFactor.Count != 3)
                {
                    FactorWarn.Text = "5因子模式需要至少自选3个因子";
                    return;
                }
                else if (_modeName.Equals("8因子模式") && hasSelectFactor != null && hasSelectFactor.Count != 5)
                {
                    FactorWarn.Text = "8因子模式需要至少自选5个因子";
                    return;
                }

            }

            if (hasSelectCommander == null || hasSelectCommander.Count < 2)
            {
                FactorWarn.Text = "双打模式需选择两名指挥官";
                return;
            }
            else
            {
                FactorWarn.Text = "";
            }

            this.Hide();
            ShowDoublesDetail showDoublesDetail = new ShowDoublesDetail(this);
            showDoublesDetail.Show();
        }



        //地图相关控件的可用性设置
        private void SetRandMapEnable(bool enable)
        {
            MapImg1.IsEnabled = enable;
            MapImg2.IsEnabled = enable;
            MapImg3.IsEnabled = enable;
            HasSelectMap.IsEnabled = enable;
            RandStartBtn.IsEnabled = enable;
            MapConfirmBtn.IsEnabled = enable;
            BaseConfirmBtn.IsEnabled = true;
        }

        //基础因子相关控件的可用性设置
        private void SetBaseFactorEnable(bool enable)
        {
            NegativeFactor1.IsEnabled = enable;
            NegativeFactor2.IsEnabled = enable;
            NegativeFactor3.IsEnabled = enable;
            NegativeFactor4.IsEnabled = enable;
            NegativeFactor5.IsEnabled = enable;
            NegativeFactor6.IsEnabled = enable;
            HasSelectNegativeFactor1.IsEnabled = enable;
            HasSelectNegativeFactor2.IsEnabled = enable;
            HasSelectNegativeFactor2.IsEnabled = enable;

            MultiFactor1.IsEnabled = enable;
            MultiFactor2.IsEnabled = enable;
            MultiFactor3.IsEnabled = enable;
            MultiFactor4.IsEnabled = enable;
            MultiFactor5.IsEnabled = enable;
            HasSelectMultiFactor1.IsEnabled = enable;
            HasSelectMultiFactor2.IsEnabled = enable;
            HasSelectMultiFactor3.IsEnabled = enable;

            BaseConfirmBtn.IsEnabled = enable;
        }

        //正面因子清除
        private void ClearBaseNegativeFactor()
        {
            //相对路径URI指定因子图片来源
            NegativeFactor1.Source = new BitmapImage();
            NegativeFactor2.Source = new BitmapImage();
            NegativeFactor3.Source = new BitmapImage();
            NegativeFactor4.Source = new BitmapImage();
            NegativeFactor5.Source = new BitmapImage();
            NegativeFactor6.Source = new BitmapImage();
        }

        //多线因子清除
        private void ClearBaseMultiFactor()
        {
            //相对路径URI指定因子图片来源
            MultiFactor1.Source = new BitmapImage();
            MultiFactor2.Source = new BitmapImage();
            MultiFactor3.Source = new BitmapImage();
            MultiFactor4.Source = new BitmapImage();
            MultiFactor5.Source = new BitmapImage();
        }

        //自选因子清除
        private void ClearRandomFactor()
        {
            //相对路径URI显示5个因子的图片
            SelectFactor1.Source = new BitmapImage();
            SelectFactor2.Source = new BitmapImage();
            SelectFactor3.Source = new BitmapImage();
            SelectFactor4.Source = new BitmapImage();
            SelectFactor5.Source = new BitmapImage();
            SelectFactor6.Source = new BitmapImage();
            SelectFactor7.Source = new BitmapImage();
            SelectFactor8.Source = new BitmapImage();
        }

        //随机先出和后出指挥官清除显示
        private void clearRandomCommander()
        {
            //相对路径URI指定指挥官图片来源
            BeforeCommander1.Source = new BitmapImage();
            BeforeCommander2.Source = new BitmapImage();
            BeforeCommander3.Source = new BitmapImage();
            BeforeCommander4.Source = new BitmapImage();
            BeforeCommander5.Source = new BitmapImage();
            BeforeCommander6.Source = new BitmapImage();

            AfterCommander1.Source = new BitmapImage();
            AfterCommander2.Source = new BitmapImage();
            AfterCommander3.Source = new BitmapImage();
            AfterCommander4.Source = new BitmapImage();
        }





        //重写关闭窗口事件
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.MainWindow.Show();
        }

        //实现绑定响应接口
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
