using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LiaoTian_Cup.Helper;

public class DbHelper
{
    private const string ConnectionString = @"Data Source=./Resources/database.db;";

    private static readonly SQLiteConnection Conn = new SQLiteConnection(ConnectionString);

    public static List<string> GetColumnData(string tableName, List<string> list)
    {
        Conn.Open();
        try
        {
            using SQLiteCommand cmd = new SQLiteCommand(Conn);
            cmd.CommandText = $"SELECT * FROM {tableName}";
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var str = Convert.ToString(reader[0]);
                list.Add(str);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        Conn.Close();
        return list;
    }

    public static List<string[]> GetListData(string tableName,int columnCount, List<string[]> list)
    {
        Conn.Open();
        try
        {
            using SQLiteCommand cmd = new SQLiteCommand(Conn);
            cmd.CommandText = $"SELECT * FROM {tableName}";
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string[] rowStrings = new string[columnCount];
                for (int i = 0; i < columnCount; i++)
                {
                    rowStrings[i] = Convert.ToString(reader[i]);
                }
                list.Add(rowStrings);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        Conn.Close();
        return list;
    }
}