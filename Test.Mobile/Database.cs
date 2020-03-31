using System;
using System.Collections.Generic;
using System.IO;
using Android.Util;
using SQLite;

namespace Test.Mobile
{
    public class Database
    {
        static string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static string connectionString= Path.Combine(folder, "Choices.db");

        public bool Create()
        {
                try
                {
                    if (!File.Exists(connectionString))
                    {
                        using (var connection = new SQLiteConnection(connectionString))
                        {
                            connection.CreateTable<Choice>();
                        }
                    }
                    return true;
                }
                catch(SQLiteException ex)
                {
                    Log.Info("SQLiteEx", ex.Message);
                    return false;
                }
        }

        public bool Add(Choice choice)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Insert(choice);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Choice> GetChoices()
        {
            try
            {
                using (var connection = new SQLiteConnection (connectionString))
                {
                    return connection.Table<Choice>().ToList();
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
    }
}