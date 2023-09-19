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
        public List<string[]> mutationList = new List<string[]>();

        //存放从突变因子CSV中得到的数据
        public List<string> mutationFactorList = new List<string>();

        //存放先出指挥官CSV中得到的数据
        public List<string> beforeCommanderInfo = new List<string>();
        //存放后出指挥官CSV中得到的数据
        public List<string> afterCommanderInfo = new List<string>();

        //存放所有的人机CSV中得到的数据
        public List<string> botInfo = new List<string>();

        //存放因子库CSV中得到的数据
        public List<string> baseNegativeFactorInfo = new List<string>();
        public List<string> baseMultiFactorInfo = new List<string>();
        public List<string> negativeFactorInfo = new List<string>();
        public List<string[]> scoreFactorList = new List<string[]>();

        //存放地图数据
        public List<string> mapsInfo = new List<string>();

        public FileData()
        {
            ReadCsv();
        }

        //读取csv
        private void ReadCsv()
        {
            //初始化窗口时即拿数据
            CSVKit.Csv2Dt(Dictionary.FilePath.mutationFilePath, mutationList);
            CSVKit.Csv2Dt(Dictionary.FilePath.mutationFactorPath, mutationFactorList);
            CSVKit.Csv2Dt(Dictionary.FilePath.beforeCommanderFilePath, beforeCommanderInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.afterCommanderFilePath, afterCommanderInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.aIFilePath, botInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.baseNegativeFactorFilePath, baseNegativeFactorInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.baseMultiFactorFilePath, baseMultiFactorInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.mapsFilePath, mapsInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.negativeFactorFilePath, negativeFactorInfo);
            CSVKit.Csv2Dt(Dictionary.FilePath.scoreFactorPath, scoreFactorList);
        }
    }
}
