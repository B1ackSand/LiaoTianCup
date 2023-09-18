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
        //路径
        private readonly string mutationFilePath = "./Resources/自选突变列表.csv";
        private readonly string mutationFactorPath = "./Resources/突变因子列表.csv";
        private readonly string beforeCommanderFilePath = "./Resources/先出指挥官列表.csv";
        private readonly string afterCommanderFilePath = "./Resources/后出指挥官列表.csv";
        private readonly string aIFilePath = "./Resources/电脑AI.csv";

        //公共信息提取
        //存放从自选突变CSV中得到的数据
        public List<string[]> mutationList = new List<string[]>();

        //存放从突变因子CSV中得到的数据
        public List<string> mutationFactorList = new List<string>();

        //存放先出指挥官CSV中得到的数据
        public List<string> beforeCommanderInfo = new List<string>();
        //存放后出指挥官CSV中得到的数据
        public List<string> afterCommanderInfo = new List<string>();

        //存放所有的人机CSV中得到的数据
        public List<string> botInfo = new List<string>();

        public void ReadCsv()
        {
            //初始化窗口时即拿数据
            CSVKit.Csv2Dt(mutationFilePath, mutationList);
            CSVKit.Csv2Dt(mutationFactorPath, mutationFactorList);
            CSVKit.Csv2Dt(beforeCommanderFilePath, beforeCommanderInfo);
            CSVKit.Csv2Dt(afterCommanderFilePath, afterCommanderInfo);
            CSVKit.Csv2Dt(aIFilePath, botInfo);
        }
    }
}
