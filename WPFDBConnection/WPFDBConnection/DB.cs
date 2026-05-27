using MySql.Data.MySqlClient;
using System.Windows;

namespace WPFDBConnection
{
    public static class DB
    {
        //this DB class will setup our connection string | You don't have to do it this way, but I wanted to have CRUD inside each "navigation member" class
        public static string Server = "localhost";
        public static string Port = "3306"; //mySQL port (always 3306)
        public static string Database = "student";
        public static string User = "root";
        public static string Password = "";


        private static String connectionString => $"Server={Server};Port={Port};Database={Database};" +
        $"Uid={User};Pwd={Password};";

        /*
         * Alternatively: private static string conn = $"Server={Server};Port={Port};Database={Database};Uid={User};Pwd={Password};";
         */

        public static MySqlConnection Open()
        {
            var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
           
            return conn;
        }
    }
}
