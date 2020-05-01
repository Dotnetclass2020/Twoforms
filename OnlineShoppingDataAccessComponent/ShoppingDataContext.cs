using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineShopingDataAccessComponent
{

    public class ShopingDataContext
    {

        private readonly SqlConnection _con;

        public ShopingDataContext()
        {
            _con = GetConnection();
        }



        private bool checkConnectionState(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void insProduct()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-L3LE0A6\SQLEXPRESS;Initial Catalog=SHOPPING;Integrated Security=True");


            con.Open();

            //Do DataBase Operations



            con.Close();
        }


        private void maintainedConStatus(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else
            {
                conn.Close();
            }
        }
        private SqlConnection GetConnection()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["OnlineShoping"].ConnectionString;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-L3LE0A6\SQLEXPRESS;Initial Catalog=SHOPPING;Integrated Security=True");

            maintainedConStatus(con);
            return con;
        }
    }
}

