using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiaoTian_Cup.Helper
{
    internal class RandomKit
    {
        //不重复的X个随机数
        public List<int> GenerateXRandomNum(int count, int range)
        {
            Random rand = new Random();
            List<int> result = new List<int>();
            int temp;
            while (result.Count < count)
            {
                temp = rand.Next(0, range);
                if (!result.Contains(temp))
                {
                    result.Add(temp);
                }
            }
            return result;
        }
    }
}