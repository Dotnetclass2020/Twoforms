using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness
{
    public class ShopingDataContext
    {
        private string strConnection;

        public ShopingDataContext()
        {
            strConnection = ConfigurationManager.ConnectionStrings["OnlineShoping"].ConnectionString;
        }

        public int executeNonQueary(string procedureName, SqlParameter[] parem)
        {
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parem);
            int results = cmd.ExecuteNonQuery();
            con.Close();
            return results;
        }

        public SqlDataReader executeDataReadar(string procedureName, SqlParameter[] parem)
        {
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parem);
            var dataReader = cmd.ExecuteReader();
            return dataReader;
            
        }

        public t executeScaler<t>(string procedureName, SqlParameter[] parem)
        {
            var result = typeof(t);
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parem);
            dynamic results = (decimal)cmd.ExecuteScalar();
            con.Close();
            return results;
        }

        public DataSet ExecuteDataSet(string procedureName, SqlParameter[] parem)
        {

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand(procedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parem);

            //SqlDataAdpater
            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            adpter.Fill(ds);
            con.Close();
            return ds;

        }

        public DataSet ExecuteDataSetwithOutParameter(string command)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand(command, con);
            SqlDataAdapter adpter = new SqlDataAdapter(cmd);
            adpter.Fill(ds);

            con.Close();
            return ds;
        }

    }
}
