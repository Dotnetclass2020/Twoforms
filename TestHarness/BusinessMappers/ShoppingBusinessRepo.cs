using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using TestHarness.Utilitys;

namespace TestHarness
{
    public class ShopingBusinessRepo
    {

        private const string CreateOrUpdateSP = "sp_CreateOrUpdateCustometr";
        private const string spGetCustomers = "[dbo].[sp_getCustomer]";
        private const string cmdCountry = "select CountryID,countryName from [dbo].[countries]";
        private const string getState = "GetStates";
        public int Create(Customer cust)
        {
            ShopingDataContext context = new ShopingDataContext();
            var res = Convert.ToInt32(context.executeScaler<decimal>(CreateOrUpdateSP, customerParamsBuilder(cust)));
            return res;
        }

        public Customer GetCustomer(int Id)
        {
            ShopingDataContext context = new ShopingDataContext();
          var details=  context.executeDataReadar(spGetCustomers, BuildCustomerparams(Id));
           
            details.Read();
            Customer cus = new Customer()
            {
                Id = Convert.ToInt32(details["Id"]),
                FirstName = Convert.ToString(details["FirstName"]),
                LastName = Convert.ToString(details["LastName"]),
                City = Convert.ToString(details["City"]),
                Country = Convert.ToString(details["Country"]),
                Phone = Convert.ToString(details["Phone"])
            };
            details.Close();
            return cus;

        }

        public List<Customer> getAllCustomer()
        {
            ShopingDataContext context = new ShopingDataContext();
            var details = context.ExecuteDataSet(spGetCustomers, BuildCustomerparams(0));
            return  HarnessUtilitys.ConvertDataTable<Customer>(details.Tables[0]);
        }
        public DataSet getCountrys()
        {
            ShopingDataContext context = new ShopingDataContext();
           var datset= context.ExecuteDataSetwithOutParameter(cmdCountry);
            return datset;

        }

        public DataSet getStates(int CountryId)
        {

            ShopingDataContext context = new ShopingDataContext();
            var datset = context.ExecuteDataSet(getState, BuildStatePrams(CountryId));
            return datset;
        }

        private SqlParameter[] BuildStatePrams(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CountryId",Id)
            };
            return param;
        }

        private SqlParameter[] BuildCustomerparams(int Id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CustometId",Id)
            };
            return param;
        }
        private SqlParameter[] customerParamsBuilder(Customer cust)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                

                new SqlParameter("@Id",cust.Id),
                new SqlParameter("@FirstName",cust.FirstName),
                new SqlParameter("@LastName",cust.LastName),
                new SqlParameter("@City",cust.City),
                new SqlParameter("@Country",cust.Country),
                new SqlParameter("@phone",cust.Phone),

            };
            return param;
        }
        

    }
}


