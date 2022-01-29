﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeAwalk.dbSource
{
   public class dbHelper
    {
        public static string Getconnectionstring()
        {
            string val = ConfigurationManager.ConnectionStrings["DefauitConnectionString"].ConnectionString;
            return val;
        }

        public static DataRow ReadDataRow(string connectionstring, string dbCommandstring, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    if (dt.Rows.Count == 0)
                        return null;
                    DataRow dr = dt.Rows[0];
                    return dr;
                }
            }
        }
    }
}
