using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SqlGpsGate
{
    class SqlConnection
    {
         private MySqlConnection connection;
        private string connectionString;

        public SqlConnection()
        {
            Initialize();
        }
        private void Initialize()
        {
           
            connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            connection =  new MySqlConnection(connectionString);
        }

        public MySqlConnection Get(){
            return connection;
        }

        public string MySQLEscape(string str)
        {
            return Regex.Replace(str, @"[\x00'""\b\n\r\t\cZ\\%_]",
            delegate(Match match)
            {
                string v = match.Value;
                switch (v)
                {
                    case "\x00":            // ASCII NUL (0x00) character
                        return "\\0";
                    case "\b":              // BACKSPACE character
                        return "\\b";
                    case "\n":              // NEWLINE (linefeed) character
                        return "\\n";
                    case "\r":              // CARRIAGE RETURN character
                        return "\\r";
                    case "\t":              // TAB
                        return "\\t";
                    case "\u001A":          // Ctrl-Z
                        return "\\Z";
                    default:
                        return "\\" + v;
                }
            });
        }
      
    }
}
