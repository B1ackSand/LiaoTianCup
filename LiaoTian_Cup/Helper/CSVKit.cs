using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace LiaoTian_Cup.Helper
{
    class CSVKit
    {
        // TODO 读写锁的问题存在
        //两个实现
        public static List<string[]> Csv2Dt(string filePath, List<string[]> list)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false);
                while (reader.Peek() > 0)
                {
                    string str = reader.ReadLine();
                    string[] split = str.Split(',');
                    list.Add(split);
                }
                reader.Close();
                reader.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<string> Csv2Dt(string filePath, List<string> list)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false);
                int count = 0;
                while (reader.Peek() > 0)
                {
                    string str = reader.ReadLine();
                    list.Add(str);
                    count++;
                }
                reader.Close();
                reader.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
