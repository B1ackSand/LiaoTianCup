using Force.DeepCloner;
using LiaoTian_Cup.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LiaoTian_Cup
{
    /// <summary>
    /// SingleModeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SingleModeWindow : Page
    {
        //路径
        private readonly string scoreFactorPath = "./Resources/group_FactorList_Score.csv";
        private readonly string beforeCommanderFilePath = "./Resources/先出指挥官列表.csv";
        private readonly string afterCommanderFilePath = "./Resources/后出指挥官列表.csv";
        private readonly string aIFilePath = "./Resources/电脑AI.csv";
        private readonly string mapsFilePath = "./Resources/所有地图.csv";

        //存放因子库CSV中得到的数据
        private List<string[]> scoreFactorList = new List<string[]>();

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
        private Image hasSelectMap = new Image();
        private List<Image> hasSelectBanFactor = new List<Image>(5);
        private List<Image> hasSelectFactor = new List<Image>();
        private Image hasSelectCommander = new Image();

        //显示当前选择的因子总分

        //初始化
        RandomKit rk = new RandomKit();
        public event PropertyChangedEventHandler PropertyChanged;

        public SingleModeWindow()
        {
            //初始化窗口时即拿数据
            CSVKit.Csv2Dt(scoreFactorPath, scoreFactorList);

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

        //模式选择（3因子模式,5因子模式）数据
        private string _modeName = "3因子模式";
        public string modeName
        {
            get { return _modeName; }
            set
            {
                _modeName = value;
                RaisePropertyChanged(nameof(_modeName));
            }
        }

        //分数响应相关逻辑
        private int _score = 0;
        public int score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged(nameof(score));
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
            Warn.Text = "";
            ModeBox.IsEnabled = true;
            SetRandMapEnable(true);

            hasSelectFactor.Clear();
            ClearRandomFactor();
            FlashHasSelectFactor();
            Score.Text = "0";
            _score = 0;

            hasSelectBanFactor.Clear();
            FlashHasSelectBanFactor();

            hasSelectCommander = new Image();
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
            this.NavigationService.GoBack();
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
            Warn.Text = "";
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
                Warn.Text = "未选择地图";
                return;
            }
            else
            {
                Warn.Text = "";
            }
            SetRandMapEnable(false);
            ShowRandomFactor();
            RandomCommanderInfo();
        }




        //显示最多10个随机选择因子
        private void ShowRandomFactor()
        {
            var factorListClone = scoreFactorList.DeepClone();

            if (_modeName.Equals("3因子模式"))
            {
                for (int i = 0; i < factorListClone.Count; i++)
                {

                    if (Convert.ToInt32(factorListClone[i][2]) > 6)
                    {
                        factorListClone.RemoveAt(i);
                        i--;
                    }
                }
                List<int> randNum = rk.GenerateXRandomNum(6, factorListClone.Count);

                //相对路径URI显示6个因子的图片
                SelectFactor1.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[0]][1] + ".png", UriKind.Relative));
                SelectFactor2.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[1]][1] + ".png", UriKind.Relative));
                SelectFactor3.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[2]][1] + ".png", UriKind.Relative));
                SelectFactor4.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[3]][1] + ".png", UriKind.Relative));
                SelectFactor5.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[4]][1] + ".png", UriKind.Relative));
                SelectFactor6.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum[5]][1] + ".png", UriKind.Relative));
            }
            else if (_modeName.Equals("5因子模式"))
            {
                //3% 18% 79%
                SortedSet<int> randNum = new SortedSet<int>();
                while (randNum.Count < 10)
                {
                    int num = rk.GenerateRandomFromNumToNum(1, 101);
                    if (num <= 3)
                    {
                        randNum.Add(rk.GenerateRandomFromNumToNum(0, 3));
                        continue;
                    }
                    if (num > 3 && num <= 21)
                    {
                        randNum.Add(rk.GenerateRandomFromNumToNum(3, 15));
                        continue;
                    }
                    if (num > 21)
                    {
                        randNum.Add(rk.GenerateRandomFromNumToNum(15, 48));
                        continue;
                    }
                }

                SelectFactor1.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(0)][1] + ".png", UriKind.Relative));
                SelectFactor2.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(1)][1] + ".png", UriKind.Relative));
                SelectFactor3.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(2)][1] + ".png", UriKind.Relative));
                SelectFactor4.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(3)][1] + ".png", UriKind.Relative));
                SelectFactor5.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(4)][1] + ".png", UriKind.Relative));
                SelectFactor6.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(5)][1] + ".png", UriKind.Relative));
                SelectFactor7.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(6)][1] + ".png", UriKind.Relative));
                SelectFactor8.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(7)][1] + ".png", UriKind.Relative));
                SelectFactor9.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(8)][1] + ".png", UriKind.Relative));
                SelectFactor10.Source = new BitmapImage(new Uri("./Resources/factor/" + factorListClone[randNum.ElementAt(9)][1] + ".png", UriKind.Relative));
            }
        }


        //因子总分数计算
        private void CalFactorScore(Image img)
        {
            
            var selectName = (img.Source as BitmapImage).UriSource.ToString()
                        .Replace("./Resources/factor/", "").Replace(".png", "");

            for (int i = 0; i < scoreFactorList.Count; i++)
            {
                if (scoreFactorList[i][1].Equals(selectName))
                {
                    _score += Convert.ToInt32(scoreFactorList[i][2]);
                    Score.Text = _score.ToString();
                    break;
                }
            }
        }

        //左键点击自选因子事件响应
        private void Factor_MouseDown(object sender, RoutedEventArgs e)
        {
            Warn.Text = "";
            Image selectFactor = (Image)sender;
            if (selectFactor != null)
            {
                if (_modeName.Equals("3因子模式") && hasSelectFactor != null && !hasSelectFactor.Contains(selectFactor) && !hasSelectBanFactor.Contains(selectFactor))
                {
                    if (hasSelectFactor.Count < 3)
                    {
                        hasSelectFactor.Add(selectFactor);
                        CalFactorScore(selectFactor);
                    }

                }

                if (_modeName.Equals("5因子模式") && hasSelectFactor != null && !hasSelectFactor.Contains(selectFactor) && !hasSelectBanFactor.Contains(selectFactor))
                {
                    if (hasSelectFactor.Count < 5)
                    {
                        hasSelectFactor.Add(selectFactor);
                        CalFactorScore(selectFactor);
                    }

                }
            }
            FlashHasSelectFactor();
        }

        //右键点击自选因子以ban掉
        private void Ban_MouseDown(object sender, RoutedEventArgs e)
        {
            Warn.Text = "";
            Image selectFactor = (Image)sender;
            if (selectFactor != null)
            {
                if (_modeName.Equals("3因子模式") && hasSelectBanFactor != null && !hasSelectBanFactor.Contains(selectFactor) && !hasSelectFactor.Contains(selectFactor))
                {
                    if (hasSelectBanFactor.Count < 3)
                    {
                        hasSelectBanFactor.Add(selectFactor);
                    }

                }

                if (_modeName.Equals("5因子模式") && hasSelectBanFactor != null && !hasSelectBanFactor.Contains(selectFactor) && !hasSelectFactor.Contains(selectFactor))
                {
                    if (hasSelectBanFactor.Count < 5)
                    {
                        hasSelectBanFactor.Add(selectFactor);
                    }

                }
            }
            FlashHasSelectBanFactor();
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

                //因子总分数计算
                var selectName = (cancelFactor.Source as BitmapImage).UriSource.ToString()
                            .Replace("./Resources/factor/", "").Replace(".png", "");

                for (int i = 0; i < scoreFactorList.Count; i++)
                {
                    if (scoreFactorList[i][1].Equals(selectName))
                    {
                        _score -= Convert.ToInt32(scoreFactorList[i][2]);
                        Score.Text = _score.ToString();
                        break;
                    }
                }
            }
            else { return; }
            FlashHasSelectFactor();
        }

        //点击已选择的Ban选因子 取消事件响应
        private void CancelBan_MouseDown(object sender, RoutedEventArgs e)
        {
            Image cancelFactor = (Image)sender;
            if (cancelFactor != null)
            {
                for (int i = 0; i < hasSelectBanFactor.Count; i++)
                {
                    if (hasSelectBanFactor[i] != null
                        && hasSelectBanFactor[i].Source.ToString().Equals(cancelFactor.Source.ToString()))
                    {
                        hasSelectBanFactor.RemoveAt(i);
                    }
                }
            }
            else { return; }
            FlashHasSelectBanFactor();
        }


        //刷新已选择的因子
        private void FlashHasSelectFactor()
        {
            if (hasSelectFactor != null)
            {
                HasSelectFactor1.Source = hasSelectFactor.Count < 1 ? null : hasSelectFactor[0].Source;
                HasSelectFactor2.Source = hasSelectFactor.Count < 2 ? null : hasSelectFactor[1].Source;
                HasSelectFactor3.Source = hasSelectFactor.Count < 3 ? null : hasSelectFactor[2].Source;

                if (_modeName.Equals("5因子模式"))
                {
                    HasSelectFactor4.Source = hasSelectFactor.Count < 4 ? null : hasSelectFactor[3].Source;
                    HasSelectFactor5.Source = hasSelectFactor.Count < 5 ? null : hasSelectFactor[4].Source;
                }
            }
        }

        //刷新已选择的BAN因子
        private void FlashHasSelectBanFactor()
        {
            if (hasSelectBanFactor != null)
            {
                HasSelectBanFactor1.Source = hasSelectBanFactor.Count < 1 ? null : hasSelectBanFactor[0].Source;
                HasSelectBanFactor2.Source = hasSelectBanFactor.Count < 2 ? null : hasSelectBanFactor[1].Source;
                HasSelectBanFactor3.Source = hasSelectBanFactor.Count < 3 ? null : hasSelectBanFactor[2].Source;

                if (_modeName.Equals("5因子模式"))
                {
                    HasSelectBanFactor4.Source = hasSelectBanFactor.Count < 4 ? null : hasSelectBanFactor[3].Source;
                    HasSelectBanFactor5.Source = hasSelectBanFactor.Count < 5 ? null : hasSelectBanFactor[4].Source;
                }
            }
        }



        //随机先出和后出指挥官处理逻辑
        private void RandomCommanderInfo()
        {
            List<int> beforeRandNum = rk.GenerateXRandomNum(4, beforeCommanderInfo.Count);
            List<int> afterRandNum = rk.GenerateXRandomNum(2, afterCommanderInfo.Count);

            //相对路径URI指定指挥官图片来源
            BeforeCommander1.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[0]] + ".png", UriKind.Relative));
            BeforeCommander2.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[1]] + ".png", UriKind.Relative));
            BeforeCommander3.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[2]] + ".png", UriKind.Relative));
            BeforeCommander4.Source = new BitmapImage(new Uri("./Resources/commander/" + beforeCommanderInfo[beforeRandNum[3]] + ".png", UriKind.Relative));

            AfterCommander1.Source = new BitmapImage(new Uri("./Resources/commander/" + afterCommanderInfo[afterRandNum[0]] + ".png", UriKind.Relative));
            AfterCommander2.Source = new BitmapImage(new Uri("./Resources/commander/" + afterCommanderInfo[afterRandNum[1]] + ".png", UriKind.Relative));
        }

        //点击自选指挥官事件响应
        private void Commander_MouseDown(object sender, RoutedEventArgs e)
        {
            Warn.Text = "";
            Image selectCommander = (Image)sender;
            if (selectCommander != null)
            {
                hasSelectCommander = selectCommander;
            }
            FlashHasSelectCommander();
        }

        //取消当前选择的指挥官事件响应
        private void CancelCommander_MouseDown(Object sender, RoutedEventArgs e)
        {
            Image cancelCommander = (Image)sender;
            if (cancelCommander != null)
            {
                hasSelectCommander = new Image();
            }
            else { return; }
            FlashHasSelectCommander();
        }

        //刷新选择的指挥官
        private void FlashHasSelectCommander()
        {
            if (hasSelectCommander != null)
            {
                HasSelectCommander.Source = hasSelectCommander.Source;
            }
        }



        //确认按钮事件响应
        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            botName = IsRandAIFunc();
            if (hasSelectFactor != null)
            {
                if (_modeName.Equals("3因子模式") && hasSelectFactor != null && hasSelectFactor.Count != 3)
                {
                    Warn.Text = "3因子模式需要至少选择3个因子";
                    return;
                }
                else if (_modeName.Equals("5因子模式") && hasSelectFactor != null && hasSelectFactor.Count != 5)
                {
                    Warn.Text = "5因子模式需要至少选择5个因子";
                    return;
                }

            }

            if (hasSelectCommander == null || hasSelectCommander.Source == null || hasSelectCommander.Source.Equals(""))
            {
                Warn.Text = "未选择指挥官";
                return;
            }
            else
            {
                Warn.Text = "";
            }
            this.NavigationService.Navigate(new ShowSingleDetail(this));
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
        }

        //自选因子清除
        private void ClearRandomFactor()
        {
            //相对路径URI显示10个因子的图片
            SelectFactor1.Source = new BitmapImage();
            SelectFactor2.Source = new BitmapImage();
            SelectFactor3.Source = new BitmapImage();
            SelectFactor4.Source = new BitmapImage();
            SelectFactor5.Source = new BitmapImage();
            SelectFactor6.Source = new BitmapImage();
            SelectFactor7.Source = new BitmapImage();
            SelectFactor8.Source = new BitmapImage();
            SelectFactor9.Source = new BitmapImage();
            SelectFactor10.Source = new BitmapImage();
        }

        //随机先出和后出指挥官清除显示
        private void clearRandomCommander()
        {
            //相对路径URI指定指挥官图片来源
            BeforeCommander1.Source = new BitmapImage();
            BeforeCommander2.Source = new BitmapImage();
            BeforeCommander3.Source = new BitmapImage();
            BeforeCommander4.Source = new BitmapImage();

            AfterCommander1.Source = new BitmapImage();
            AfterCommander2.Source = new BitmapImage();
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
