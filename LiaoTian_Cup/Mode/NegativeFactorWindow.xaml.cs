using Force.DeepCloner;
using LiaoTian_Cup.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LiaoTian_Cup
{
    /// <summary>
    /// NegativeFactorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NegativeFactorWindow : Page, INotifyPropertyChanged
    {
        public string botName = Dictionary.I18n.Lang.ResourceManager.GetString("RandomBotStrTip");

        //链表，存放自选因子
        private List<Image> hasSelectBase = new List<Image>(2);
        private List<Image> hasSelectFactor = new List<Image>(5);
        private Image hasSelectCommander = new Image();
        private Image hasSelectMap = new Image();

        //初始化工具
        readonly RandomKit rk = new RandomKit();
        string factorDir = Dictionary.FilePath.factorDir;
        string mapDir = Dictionary.FilePath.mapDir;
        string commanderDir = Dictionary.FilePath.commanderDir;
        public event PropertyChangedEventHandler PropertyChanged;

        public NegativeFactorWindow()
        {
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
        private string _modeName = Dictionary.I18n.Lang.ResourceManager.GetString("ThreeMutatorsMode");
        public string modeName
        {
            get { return _modeName; }
            set
            {
                _modeName = value;
                RaisePropertyChanged(nameof(_modeName));
            }
        }

        //随机先出和后出指挥官处理逻辑
        private void RandomCommanderInfo()
        {
            List<int> beforeRandNum = rk.GenerateXRandomNum(4, FileData.beforeCommanderInfo.Count);
            List<int> afterRandNum = rk.GenerateXRandomNum(4, FileData.afterCommanderInfo.Count);

            //相对路径URI指定指挥官图片来源
            BeforeCommander1.Source = new BitmapImage(new Uri( $"{commanderDir}{FileData.beforeCommanderInfo[beforeRandNum[0]]}.png", UriKind.Relative));
            BeforeCommander2.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.beforeCommanderInfo[beforeRandNum[1]]}.png", UriKind.Relative));
            BeforeCommander3.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.beforeCommanderInfo[beforeRandNum[2]]}.png", UriKind.Relative));
            BeforeCommander4.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.beforeCommanderInfo[beforeRandNum[3]]}.png", UriKind.Relative));

            AfterCommander1.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.afterCommanderInfo[afterRandNum[0]]}.png", UriKind.Relative));
            AfterCommander2.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.afterCommanderInfo[afterRandNum[1]]}.png", UriKind.Relative));
            AfterCommander3.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.afterCommanderInfo[afterRandNum[2]]}.png", UriKind.Relative));
            AfterCommander4.Source = new BitmapImage(new Uri($"{commanderDir}{FileData.afterCommanderInfo[afterRandNum[3]]}.png", UriKind.Relative));
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
            AfterCommander3.Source = new BitmapImage();
            AfterCommander4.Source = new BitmapImage();
        }

        //是否随机AI的处理逻辑
        private string IsRandAIFunc()
        {
            if (isRandAI)
            {
                Random rand = new Random();
                int number = rand.Next(0, FileData.botInfo.Count);
                return Dictionary.I18n.Lang.ResourceManager.GetString(FileData.botInfo[number]);
            }
            else
            {
                return Dictionary.I18n.Lang.ResourceManager.GetString("RandomBotStrTip");
            }
        }

        //随机地图显示
        private void ShowRandomMaps()
        {
            hasSelectMap = new Image();
            FlashHasSelectMap();
            List<int> randNums = rk.GenerateXRandomNum(3, FileData.mapsInfo.Count);
            MapImg1.Source = new BitmapImage(new Uri($"{mapDir}{FileData.mapsInfo[randNums[0]]}.png", UriKind.Relative));
            MapImg2.Source = new BitmapImage(new Uri($"{mapDir}{FileData.mapsInfo[randNums[1]]}.png", UriKind.Relative));
            MapImg3.Source = new BitmapImage(new Uri($"{mapDir}{FileData.mapsInfo[randNums[2]]}.png", UriKind.Relative));
            MapName1.Text = Dictionary.I18n.Lang.ResourceManager.GetString(FileData.mapsInfo[randNums[0]]);
            MapName2.Text = Dictionary.I18n.Lang.ResourceManager.GetString(FileData.mapsInfo[randNums[1]]);
            MapName3.Text = Dictionary.I18n.Lang.ResourceManager.GetString(FileData.mapsInfo[randNums[2]]);

            MapLabel.Foreground = Brushes.Red;
        }

        //随机地图显示清除
        private void ClearRandomMaps()
        {
            MapImg1.Source = new BitmapImage();
            MapImg2.Source = new BitmapImage();
            MapImg3.Source = new BitmapImage();
            MapName1.Text = "";
            MapName2.Text = "";
            MapName3.Text = "";
        }

        //返回主页事件响应
        private void Button_BackMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //开始随机事件响应
        private void Button_Random_Click(object sender, RoutedEventArgs e)
        {
            ModeBox.IsEnabled = false;
            ShowRandomMaps();
        }

        //确认地图按钮事件响应
        private void Button_MapConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (hasSelectMap == null || hasSelectMap.Source == null || hasSelectMap.Source.Equals(""))
            {
                MapTip.Text = Dictionary.I18n.Lang.ResourceManager.GetString("MapWarn");
                return;
            }
            MapLabel.Foreground = Brushes.Gray;
            SetRandMapEnable(false);
            ShowBaseFactor();
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

        //取消当前选择的地图事件响应(Unused)
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
            HasSelectMap.Source = hasSelectMap.Source;
            MapTip.Text = "";
            if (hasSelectMap.Source != null)
            {
                var currentMapName = (hasSelectMap.Source as BitmapImage).UriSource.ToString()
                        .Replace("/LiaoTian_Cup;component/Resources/maps/", "").Replace(".png", "");
                if (!string.IsNullOrEmpty(currentMapName))
                {
                    MapTip.Text = Dictionary.I18n.Lang.ResourceManager.GetString(currentMapName);
                }
                else
                {
                    MapTip.Text = "";
                }
            }
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

        //基础因子显示
        private void ShowBaseFactor()
        {
            BaseLabel.Foreground = Brushes.Red;
            //相对路径URI指定因子图片来源
            Factor1.Source = new BitmapImage(new Uri(factorDir + FileData.baseNegativeFactorInfo[0] + ".png", UriKind.Relative));
            Factor2.Source = new BitmapImage(new Uri(factorDir + FileData.baseNegativeFactorInfo[1] + ".png", UriKind.Relative));
            Factor3.Source = new BitmapImage(new Uri(factorDir + FileData.baseNegativeFactorInfo[2] + ".png", UriKind.Relative));
            Factor4.Source = new BitmapImage(new Uri(factorDir + FileData.baseNegativeFactorInfo[3] + ".png", UriKind.Relative));
            Factor5.Source = new BitmapImage(new Uri(factorDir + FileData.baseNegativeFactorInfo[4] + ".png", UriKind.Relative));
            Factor6.Source = new BitmapImage(new Uri(factorDir + FileData.baseNegativeFactorInfo[5] + ".png", UriKind.Relative));
        }

        //基础因子清除
        private void ClearRandomBaseFactor()
        {
            //相对路径URI指定因子图片来源
            Factor1.Source = new BitmapImage();
            Factor2.Source = new BitmapImage();
            Factor3.Source = new BitmapImage();
            Factor4.Source = new BitmapImage();
            Factor5.Source = new BitmapImage();
            Factor6.Source = new BitmapImage();
        }

        //点击基础因子图片事件响应
        private void Base_MouseDown(object sender, MouseEventArgs e)
        {
            Warn.Text = "";
            Image selectBase = (Image)sender;
            if (selectBase != null)
            {
                if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("ThreeMutatorsMode")) && hasSelectBase != null && !hasSelectBase.Contains(selectBase))
                {
                    if (hasSelectBase.Count < 1)
                    {
                        hasSelectBase.Add(selectBase);
                    }
                }

                if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("FiveMutatorsMode")) && hasSelectBase != null && !hasSelectBase.Contains(selectBase))
                {
                    if (hasSelectBase.Count < 2)
                    {
                        hasSelectBase.Add(selectBase);
                    }
                }
            }
            FlashSelectBase();
        }

        //取消当前选择的基础因子事件响应
        private void CancelBase_MouseDown(Object sender, RoutedEventArgs e)
        {
            Image cancelBase = (Image)sender;
            if (cancelBase != null)
            {
                for (int i = 0; i < hasSelectBase.Count; i++)
                {
                    if (hasSelectBase[i] != null
                        && hasSelectBase[i].Source.ToString().Equals(cancelBase.Source.ToString()))
                    {
                        hasSelectBase.RemoveAt(i);
                    }
                }
            }
            else { return; }
            FlashSelectBase();
        }

        // 基础因子确认按钮
        private void Button_BaseConfirm_Click(Object sender, RoutedEventArgs e)
        {
            Warn.Text = "";
            if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("ThreeMutatorsMode")) && hasSelectBase.Count < 1)
            {
                Warn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("BaseMutatorsWarn");
                return;
            }
            if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("FiveMutatorsMode")) && hasSelectBase.Count < 2)
            {
                Warn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("BaseMutatorsWarn");
                return;
            }
            MapLabel.Foreground = Brushes.Gray;
            SetBaseFactorEnable(false);
            ShowRandomFactor();
            RandomCommanderInfo();

            BaseLabel.Foreground = Brushes.Gray;
            FreeLabel.Foreground = Brushes.Red;
            OldCmdrLabel.Foreground = Brushes.Red;
            NewCmdrLabel.Foreground = Brushes.Red;
        }

        //刷新已选基础因子事件
        private void FlashSelectBase()
        {
            if (hasSelectBase != null)
            {
                HasSelectBaseFactor1.Source = hasSelectBase.Count < 1 ? null : hasSelectBase[0].Source;
                HasSelectBaseFactor2.Source = hasSelectBase.Count < 2 ? null : hasSelectBase[1].Source;
            }
        }

        //基础因子相关控件的可用性设置
        private void SetBaseFactorEnable(bool enable)
        {
            Factor1.IsEnabled = enable;
            Factor2.IsEnabled = enable;
            Factor3.IsEnabled = enable;
            Factor4.IsEnabled = enable;
            Factor5.IsEnabled = enable;
            Factor6.IsEnabled = enable;
            HasSelectBaseFactor1.IsEnabled = enable;
            HasSelectBaseFactor2.IsEnabled = enable;
            BaseConfirmBtn.IsEnabled = enable;
        }

        //显示最多8个随机选择因子
        private void ShowRandomFactor()
        {
            var factorListClone = FileData.negativeFactorInfo.DeepClone();
            for (int i = 0; i < hasSelectBase.Count; i++)
            {
                var currentFactor = (hasSelectBase[i].Source as BitmapImage).UriSource.ToString()
                        .Replace("/LiaoTian_Cup;component/Resources/factor/", "").Replace(".png", "");
                if (currentFactor.Equals("风暴英雄"))
                {
                    factorListClone.Remove("非同寻常的战役");
                }
                else if (currentFactor.Equals("非同寻常的战役"))
                {
                    factorListClone.Remove("风暴英雄");
                }

                factorListClone.Remove(currentFactor);
            }
            List<int> randNum = rk.GenerateXRandomNum(8, factorListClone.Count);

            //相对路径URI显示8个因子的图片
            SelectFactor1.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[0]]}.png", UriKind.Relative));
            SelectFactor2.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[1]]}.png", UriKind.Relative));
            SelectFactor3.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[2]]}.png", UriKind.Relative));
            SelectFactor4.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[3]]}.png", UriKind.Relative));
            SelectFactor5.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[4]]}.png", UriKind.Relative));
            SelectFactor6.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[5]]}.png", UriKind.Relative));
            SelectFactor7.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[6]]}.png", UriKind.Relative));
            SelectFactor8.Source = new BitmapImage(new Uri($"{factorDir}{factorListClone[randNum[7]]}.png", UriKind.Relative));
        }

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

        //点击自选因子事件响应
        private void Factor_MouseDown(object sender, RoutedEventArgs e)
        {
            Warn.Text = "";
            Image selectFactor = (Image)sender;
            if (selectFactor != null)
            {
                if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("ThreeMutatorsMode")) && hasSelectFactor != null && !hasSelectFactor.Contains(selectFactor))
                {
                    if (hasSelectFactor.Count < 2)
                    {
                        hasSelectFactor.Add(selectFactor);
                    }

                }

                if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("FiveMutatorsMode")) && hasSelectFactor != null && !hasSelectFactor.Contains(selectFactor))
                {
                    if (hasSelectFactor.Count < 3)
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
            HasSelectFactor1.Source = hasSelectFactor.Count < 1 ? null : hasSelectFactor[0].Source;
            HasSelectFactor2.Source = hasSelectFactor.Count < 2 ? null : hasSelectFactor[1].Source;
            HasSelectFactor3.Source = hasSelectFactor.Count < 3 ? null : hasSelectFactor[2].Source;
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
                if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("ThreeMutatorsMode")) && hasSelectFactor != null && hasSelectFactor.Count != 2)
                {
                    Warn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("FreeMutatorsWarn1");
                    return;
                }
                else if (_modeName.Equals(Dictionary.I18n.Lang.ResourceManager.GetString("FiveMutatorsMode")) && hasSelectFactor != null && hasSelectFactor.Count != 3)
                {
                    Warn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("FreeMutatorsWarn2");
                    return;
                }

            }

            if (hasSelectCommander == null || hasSelectCommander.Source == null || hasSelectCommander.Source.Equals(""))
            {
                Warn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("CommanderWarn2");
                return;
            }
            else
            {
                Warn.Text = "";
            }
            this.NavigationService.Navigate(new ShowNegativeDetail(this));
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

            hasSelectBase.Clear();
            BaseConfirmBtn.IsEnabled = false;
            ClearRandomBaseFactor();
            FlashSelectBase();

            hasSelectFactor.Clear();
            ClearRandomFactor();
            FlashHasSelectFactor();

            hasSelectCommander = new Image();
            clearRandomCommander();
            FlashHasSelectCommander();

            hasSelectMap = new Image();
            ClearRandomMaps();
            FlashHasSelectMap();

            MapLabel.Foreground = Brushes.Black;
            FreeLabel.Foreground = Brushes.Black;
            BaseLabel.Foreground = Brushes.Black;
            OldCmdrLabel.Foreground = Brushes.Black;
            NewCmdrLabel.Foreground = Brushes.Black;
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
