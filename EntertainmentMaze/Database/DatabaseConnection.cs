using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace EntertainmentMaze.Database
{
    class DatabaseConnection
    {
        public SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=Database/TriviaDatabase.db");

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception)
            {

            }
            return sqlite_conn;
        }

        public void ReadData()
        {
            SQLiteConnection connection = CreateConnection();
            SQLiteCommand sqliteCmd = connection.CreateCommand();
            sqliteCmd = new SQLiteCommand("SELECT * FROM QUESTION", connection);
            SQLiteDataReader sqliteDatareader = sqliteCmd.ExecuteReader();

            string[] questionCollection = new string[25];
            int i = 0;

            while (sqliteDatareader.Read())
            {

                int questionID = sqliteDatareader.GetInt32(0);
                int answerID = sqliteDatareader.GetInt32(1);
                string question = sqliteDatareader.GetString(2);

                questionCollection[i] = question;

                i++;
            }

            connection.Close();
        }
    }
}
