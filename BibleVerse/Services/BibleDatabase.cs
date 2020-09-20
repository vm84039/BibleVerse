using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using BibleVerse.Models;

namespace BibleVerse.Services
{
    public class BibleDatabase
    {
        readonly string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;initial catalog=Benchmark_Bible ;Integrated Security=True;";
        SqlConnection conn = null;

        public void EstablishConnection()
        {
            conn = new SqlConnection(connectionStr);
        }
        public void AddScriptureToDatabase(BibleModel verse)
        {
             string query = "INSERT INTO Bible (TESTAMENT, BOOK, CHAPTER, VERSE, TEXT)VALUES " +
                           "(@TESTAMENT, @BOOK, @CHAPTER, @VERSE, @TEXT)";
            conn = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@TESTAMENT", verse.Testament);
            command.Parameters.AddWithValue("@BOOK", verse.Book);
            command.Parameters.AddWithValue("@CHAPTER", verse.Chapter);
            command.Parameters.AddWithValue("@VERSE", verse.Verse);
            command.Parameters.AddWithValue("@TEXT", verse.Text);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                
                //   MineSweeperLogger.GetInstance().Info(String.Format("Inserted new board state for {0}.", user.UserName));
             }
             catch (SqlException e)
            {
                    Debug.WriteLine("Error generated. Details: " + e);
                //    MineSweeperLogger.GetInstance().Error(e, String.Format("Error occurred when inserting board state for {0}.", user.UserName));
            }
            finally 
            {
                    conn.Close();
            }
            
        }
        public string FindScripture(BibleModel verse)
        {
            string found = "";
            conn = new SqlConnection(connectionStr);
            conn.Open();

            using (SqlCommand command = new SqlCommand("SELECT Testament, Book, Chapter, Verse, Text FROM dbo.Bible",conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string dTestament = reader.GetString(0).Trim();
                    string dBook = reader.GetString(1).Trim();
                    int dChapter = reader.GetInt32(2);
                    int dVerse = reader.GetInt32(3);
                    string dText = reader.GetString(4).Trim();

                    if ((dTestament == verse.Testament) && (dBook == verse.Book) && (dChapter == verse.Chapter) && (dVerse == verse.Verse)) 
                    {
                        found = dText;
                    }
                }
                reader.Close();
            }
            return found;
        }
    }
}
