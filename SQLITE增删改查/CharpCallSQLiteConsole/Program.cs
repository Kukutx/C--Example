using System;
using System.Data.SQLite;

namespace SQLiteSamples
{
    class Program
    {
        //数据库连接
        SQLiteConnection m_dbConnection;

        static void Main(string[] args)
        {
            Program p = new Program();
        }

        public Program()
        {
            createNewDatabase();
            connectToDatabase();
            createTable();
            fillTable();
            printHighscores();
        }

        //创建一个空的数据库
        void createNewDatabase()
        {
            SQLiteConnection.CreateFile("MyDatabase.db");//可以不要此句
        }

        //创建一个连接到指定数据库
        void connectToDatabase()
        {
            //基本方式：Data Source=c:\mydb.db;Version=3;
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.db;Version=3;");//没有数据库则自动创建
            m_dbConnection.Open();
        }

        //在指定数据库中创建一个table
        void createTable()
        {
            string sql = "create table  if not exists highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //插入一些数据
        void fillTable()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //使用sql查询语句，并显示结果
        void printHighscores()
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            Console.ReadLine();
        }

        //增
        bool insertdatatosqlite(string exsql)
        {
            bool flag = true;
            using (SQLiteConnection SQLiteConn = new SQLiteConnection("data source=MyDatabase.db"))
            {
                SQLiteConn.Open();
                using (SQLiteCommand SQLiteCmd = new SQLiteCommand())
                {
                    try
                    {
                        SQLiteCmd.Connection = SQLiteConn;
                        SQLiteCmd.CommandText = exsql;
                        SQLiteCmd.ExecuteNonQuery();
                        SQLiteCmd.Dispose();//如果忘记这句话，会出现一个叫做database is locked的错误
                    }
                    catch (Exception exdbe)
                    {
                        Console.WriteLine(exdbe);
                        flag = false;
                    }
                }
                SQLiteConn.Close();//每次使用结束记得释放连接.
            }
            return flag;
        }

        //查
        bool QuerydataExist(string strsql, string sname, string sarea)
        {
            SQLiteDataReader qereader;
            bool flag = false;
            using (SQLiteConnection SQLiteConn = new SQLiteConnection("data source=MyDatabase.db"))
            {
                SQLiteConn.Open();
                using (SQLiteCommand SQLiteCmd = new SQLiteCommand())
                {
                    try
                    {
                        SQLiteCmd.Connection = SQLiteConn;
                        SQLiteCmd.CommandText = strsql;
                        qereader = SQLiteCmd.ExecuteReader();
                        while (qereader.Read())
                        {
                            if (qereader["modelname"].ToString() == sname && qereader["modelarea"].ToString() == sarea)
                            {
                                SQLiteConn.Close();
                                flag = true;
                                break;
                            }
                        }
                        SQLiteCmd.Dispose();
                    }
                    catch (Exception exdbe)
                    {
                        Console.WriteLine(exdbe);
                    }
                }
                SQLiteConn.Close();
            }
            return flag;
        }
    }
}