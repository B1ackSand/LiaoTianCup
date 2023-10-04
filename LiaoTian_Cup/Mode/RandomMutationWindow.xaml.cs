using Force.DeepCloner;
using LiaoTian_Cup.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LiaoTian_Cup
{
    /// <summary>
    /// RandomMutationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RandomMutationWindow : Page, INotifyPropertyChanged
    {
        public string botName = Dictionary.I18n.Lang.ResourceManager.GetString("RandomBotStrTip");

        //链表，存放自选因子
        private List<Image> hasSelectFactor = new List<Image>(8);
        private List<Image> hasSelectCommander = new List<Image>(2);

        //初始化工具
        readonly RandomKit rk = new RandomKit();
        string factorDir = Dictionary.FilePath.factorDir;
        string mapDir = Dictionary.FilePath.mapDir;
        string commanderDir = Dictionary.FilePath.commanderDir;
        public event PropertyChangedEventHandler PropertyChanged;

        //是否允许随机AI逻辑
        private bool _isRandAI;
        public bool isRandAI {
            get { return _isRandAI; }
            set
            {
                _isRandAI = value;
                RaisePropertyChanged(nameof(isRandAI));
            }
        }

        //是否为双打模式
        private bool _isDoubles;
        public bool isDoubles
        {
            get { return _isDoubles; }
            set
            {
                _isDoubles = value;
                RaisePropertyChanged(nameof(isDoubles));
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
        

        public RandomMutationWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        //随机得到一条官突数据stirng[],8因子和指挥官也会在此进行随机
        private void RandomMutationFunc()
        {
            //TODO 有重复出现同一个突变的情况
            Random rand = new Random();
            int number = rand.Next(0, FileData.mutationList.Count);

            string[] randMutationInfo = FileData.mutationList[number];
            MutationBox.Text = randMutationInfo[0];//突变名称
            MapBox.Text = Dictionary.I18n.Lang.ResourceManager.GetString(randMutationInfo[1]);//地图名称

            //相对路径URI指定随机突变地图和因子图片来源
            MapImg.Source = new BitmapImage(new Uri(mapDir + randMutationInfo[1] + ".png", UriKind.Relative));
            Factor1.Source = new BitmapImage(new Uri(factorDir + randMutationInfo[2] + ".png", UriKind.Relative));
            Factor2.Source = new BitmapImage(new Uri(factorDir + randMutationInfo[3] + ".png", UriKind.Relative));
            Factor3.Source = new BitmapImage(new Uri(factorDir + randMutationInfo[4] + ".png", UriKind.Relative));
            //随机8因子选择
            Random8Factor(randMutationInfo);
            //随机先出和后出指挥官
            RandomCommanderInfo();
        }

        //随机8因子选择处理逻辑
        private void Random8Factor(string[] randMutationInfo)
        {
            var factorListClone = FileData.mutationFactorList.DeepClone();
            for (int i = 2; i < randMutationInfo.Length; i++)
            {
                factorListClone.Remove(randMutationInfo[i]);
            }
            List<int> rand8Num = rk.GenerateXRandomNum(8, factorListClone.Count);

            //相对路径URI显示8个因子的图片
            SelectFactor1.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[0]] + ".png", UriKind.Relative));
            SelectFactor2.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[1]] + ".png", UriKind.Relative));
            SelectFactor3.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[2]] + ".png", UriKind.Relative));
            SelectFactor4.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[3]] + ".png", UriKind.Relative));
            SelectFactor5.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[4]] + ".png", UriKind.Relative));
            SelectFactor6.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[5]] + ".png", UriKind.Relative));
            SelectFactor7.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[6]] + ".png", UriKind.Relative));
            SelectFactor8.Source = new BitmapImage(new Uri(factorDir + factorListClone[rand8Num[7]] + ".png", UriKind.Relative));
        }

        //随机先出和后出指挥官处理逻辑
        private void RandomCommanderInfo()
        {
            List<int> beforeRandNum = rk.GenerateXRandomNum(5, FileData.beforeCommanderInfo.Count);
            List<int> afterRandNum = rk.GenerateXRandomNum(3, FileData.afterCommanderInfo.Count);

            //相对路径URI指定指挥官图片来源
            BeforeCommander1.Source = new BitmapImage(new Uri(commanderDir + FileData.beforeCommanderInfo[beforeRandNum[0]] + ".png", UriKind.Relative));
            BeforeCommander2.Source = new BitmapImage(new Uri(commanderDir + FileData.beforeCommanderInfo[beforeRandNum[1]] + ".png", UriKind.Relative));
            BeforeCommander3.Source = new BitmapImage(new Uri(commanderDir + FileData.beforeCommanderInfo[beforeRandNum[2]] + ".png", UriKind.Relative));
            BeforeCommander4.Source = new BitmapImage(new Uri(commanderDir + FileData.beforeCommanderInfo[beforeRandNum[3]] + ".png", UriKind.Relative));

            AfterCommander1.Source = new BitmapImage(new Uri(commanderDir + FileData.afterCommanderInfo[afterRandNum[0]] + ".png", UriKind.Relative));
            AfterCommander2.Source = new BitmapImage(new Uri(commanderDir + FileData.afterCommanderInfo[afterRandNum[1]] + ".png", UriKind.Relative));

            //双打模式先后各多显示一名指挥官
            if(_isDoubles)
            {
                BeforeCommander5.Source = new BitmapImage(new Uri(commanderDir + FileData.beforeCommanderInfo[beforeRandNum[4]] + ".png", UriKind.Relative));
                AfterCommander3.Source = new BitmapImage(new Uri(commanderDir + FileData.afterCommanderInfo[afterRandNum[2]] + ".png", UriKind.Relative));
            }
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
            return Dictionary.I18n.Lang.ResourceManager.GetString("RandomBotStrTip");
        }

        

        //开始随机事件响应
        private void Button_Random_Click(object sender, RoutedEventArgs e)
        {
            ChkDoubles.IsEnabled = false;
            reflashSelectItem();
            RandomMutationFunc();
        }

        //重置当前界面响应
        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            reflashSelectItem();
            ResetFunc();
        }


        //返回主页事件响应
        private void Button_BackMain_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        //点击自选因子事件响应
        private void Factor_MouseDown(object sender, RoutedEventArgs e)
        {
            Image selectFactor = (Image)sender;
            if (selectFactor != null)
            {
                if (!hasSelectFactor.Contains(selectFactor))
                {
                    hasSelectFactor.Add(selectFactor);
                }
                else
                {
                    return;
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

        //点击自选指挥官事件响应
        private void Commander_MouseDown(object sender, RoutedEventArgs e)
        {
            CommanderWarn.Text = "";
            Image selectCommander = (Image)sender;
            if(selectCommander != null && !_isDoubles)
            {
                if(hasSelectCommander.Count < 1)
                {
                    hasSelectCommander.Add(selectCommander);
                }
            }


            else if (selectCommander != null && _isDoubles)
            {
                if(hasSelectCommander.Count < 2 && !hasSelectCommander.Contains(selectCommander))
                {
                    hasSelectCommander.Add(selectCommander);
                }   
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

        //确认按钮事件响应
        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            botName = IsRandAIFunc();
            if (hasSelectCommander == null ||(_isDoubles && hasSelectCommander.Count < 2))
            {
                CommanderWarn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("CommanderWarn1");
                return;
            }
            else if(hasSelectCommander == null || (!_isDoubles && hasSelectCommander.Count < 1))
            {
                CommanderWarn.Text = Dictionary.I18n.Lang.ResourceManager.GetString("CommanderWarn2");
                return;
            }
            else
            {
                CommanderWarn.Text = "";
            }

            this.NavigationService.Navigate(new ShowRMDetail(this));
        }

        //刷新已选择的因子
        private void FlashHasSelectFactor()
        {
            HasSelectFactor1.Source = hasSelectFactor.Count < 1 ? null : hasSelectFactor[0].Source;
            HasSelectFactor2.Source = hasSelectFactor.Count < 2 ? null : hasSelectFactor[1].Source;
            HasSelectFactor3.Source = hasSelectFactor.Count < 3 ? null : hasSelectFactor[2].Source;
            HasSelectFactor4.Source = hasSelectFactor.Count < 4 ? null : hasSelectFactor[3].Source;
            HasSelectFactor5.Source = hasSelectFactor.Count < 5 ? null : hasSelectFactor[4].Source;
            HasSelectFactor6.Source = hasSelectFactor.Count < 6 ? null : hasSelectFactor[5].Source;
            HasSelectFactor7.Source = hasSelectFactor.Count < 7 ? null : hasSelectFactor[6].Source;
            HasSelectFactor8.Source = hasSelectFactor.Count < 8 ? null : hasSelectFactor[7].Source;
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

        //刷新窗口的方法
        internal void reflashSelectItem()
        {
            hasSelectFactor.Clear();
            hasSelectCommander.Clear();
            //刷新
            FlashHasSelectFactor();
            FlashHasSelectCommander();
        }

        public void ResetFunc()
        {
            CommanderWarn.Text = "";
            ChkDoubles.IsEnabled = true;
            isDoubles = false;

            MutationBox.Text = string.Empty;
            MapBox.Text = string.Empty;

            Factor1.Source = new BitmapImage();
            Factor2.Source = new BitmapImage();
            Factor3.Source = new BitmapImage();
            MapImg.Source = new BitmapImage();

            SelectFactor1.Source = new BitmapImage();
            SelectFactor2.Source = new BitmapImage();
            SelectFactor3.Source = new BitmapImage();
            SelectFactor4.Source = new BitmapImage();
            SelectFactor5.Source = new BitmapImage();
            SelectFactor6.Source = new BitmapImage();
            SelectFactor7.Source = new BitmapImage();
            SelectFactor8.Source = new BitmapImage();

            BeforeCommander1.Source = new BitmapImage();
            BeforeCommander2.Source = new BitmapImage();
            BeforeCommander3.Source = new BitmapImage();
            BeforeCommander4.Source = new BitmapImage();
            BeforeCommander5.Source = new BitmapImage();

            AfterCommander1.Source = new BitmapImage();
            AfterCommander2.Source = new BitmapImage();
            AfterCommander3.Source = new BitmapImage();
        }

        //实现绑定响应接口
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
