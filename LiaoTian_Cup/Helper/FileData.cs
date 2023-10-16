using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LiaoTian_Cup.Helper
{

    public class FileData
    {
        //公共信息提取
        //存放从自选突变CSV中得到的数据
        public static List<string[]> mutationList = new List<string[]>();

        //存放从突变因子CSV中得到的数据
        public static List<string> mutationFactorList = new List<string>();

        //存放先出指挥官CSV中得到的数据
        public static List<string> beforeCommanderInfo = new List<string>();
        //存放后出指挥官CSV中得到的数据
        public static List<string> afterCommanderInfo = new List<string>();

        //存放所有的人机CSV中得到的数据
        public static List<string> botInfo = new List<string>();

        //存放因子库CSV中得到的数据
        public static List<string> baseNegativeFactorInfo = new List<string>();
        public static List<string> baseMultiFactorInfo = new List<string>();
        public static List<string> negativeFactorInfo = new List<string>();
        public static List<string[]> scoreFactorList = new List<string[]>();

        //存放USuck模式的CSV数据
        public static List<string> usuckNegativeFactorInfo = new List<string>();
        public static List<string> usuckMultiFactorInfo = new List<string>();
        public static List<string> usuckFactorList = new List<string>();

        //Hub
        public static List<string> hubNegativeFactorInfo = new List<string>();
        public static List<string> hubMultiFactorInfo = new List<string>();
        public static List<string> hubFactorList = new List<string>();
        public static List<string> hubBeforeCommanderInfo = new List<string>();
        public static List<string> hubAfterCommanderInfo = new List<string>();
        public static List<string> braveMapsInfo = new List<string>();

        //存放地图数据
        public static List<string> mapsInfo = new List<string>();

        public FileData()
        {
            ReadDataBase();
        }

        //读取数据库
        private void ReadDataBase()
        {
            DbHelper.GetListData("global_weeklymutations", 5, mutationList);
            DbHelper.GetListData("group_MutatorList_Cost", 3, scoreFactorList);

            DbHelper.GetColumnData("global_mutatorlist", mutationFactorList);
            DbHelper.GetColumnData("global_cmdroldlist", beforeCommanderInfo);
            DbHelper.GetColumnData("global_cmdrnewlist", afterCommanderInfo);
            DbHelper.GetColumnData("ai_table", botInfo);
            DbHelper.GetColumnData("doubles_negativelist", baseNegativeFactorInfo);
            DbHelper.GetColumnData("doubles_multilist", baseMultiFactorInfo);
            DbHelper.GetColumnData("global_maplist", mapsInfo);
            DbHelper.GetColumnData("global_negativelist", negativeFactorInfo);

            //USuck
            DbHelper.GetColumnData("usuck_mutatorlist", usuckFactorList);
            DbHelper.GetColumnData("usuck_multilist", usuckMultiFactorInfo);
            DbHelper.GetColumnData("usuck_negativelist", usuckNegativeFactorInfo);

            //Hub
            DbHelper.GetColumnData("hub_mutatorlist", hubFactorList);
            DbHelper.GetColumnData("hub_multilist", hubMultiFactorInfo);
            DbHelper.GetColumnData("hub_negativelist", hubNegativeFactorInfo);
            DbHelper.GetColumnData("hub_cmdroldlist", hubBeforeCommanderInfo);
            DbHelper.GetColumnData("hub_cmdrnewlist", hubAfterCommanderInfo);
            DbHelper.GetColumnData("hub_bravemap", braveMapsInfo);
        }
    }
}
