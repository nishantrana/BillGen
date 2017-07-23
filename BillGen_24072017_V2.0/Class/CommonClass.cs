using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlClient;

namespace BillGen.Class
{
    class CommonClass
    {
        public static SqlCeConnection BillGenConnection()
        {
            string s = Properties.Settings.Default.dbBillGenConnectionString;
            return new SqlCeConnection(Properties.Settings.Default.dbBillGenConnectionString);
        }

        /// <summary>
        /// Common method to execute sql Insert query
        /// </summary>
        /// <param name="_cId"></param>
        public static void ExecuteNonQuery(string _query)
        {
            using (SqlCeConnection con = CommonClass.BillGenConnection())
            {
                if (con.State != ConnectionState.Open) con.Open();

                using (SqlCeCommand com = new SqlCeCommand(_query, con))
                {
                    ///Execute Command
                    com.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Common method to Get execute sql query
        /// </summary>
        /// <param name="_cId"></param>
        public static DataTable ExecuteGetQuery(string _query)
        {
            SqlCeDataAdapter da;
            DataTable dt = new DataTable();
            using (SqlCeConnection con = CommonClass.BillGenConnection())
            {
                if (con.State != ConnectionState.Open) con.Open();

                using (SqlCeCommand com = new SqlCeCommand(_query, con))
                {
                    ///Execute Command
                    da = new SqlCeDataAdapter(com);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Common method to check entry in SQL
        /// </summary>
        /// <param name="_cId"></param>
        public static int ExecuteCheckEntryQuery(string _query)
        {
            int i = 0;
            using (SqlCeConnection con = CommonClass.BillGenConnection())
            {
                if (con.State != ConnectionState.Open) con.Open();

                using (SqlCeCommand com = new SqlCeCommand(_query, con))
                {
                    ///Execute Command
                    i = Convert.ToInt32(com.ExecuteScalar());
                }
            }
            return i;
        }

        #region Dropdown Methods for Country/State/City
        public static DataTable fillCompany()
        {
            return ExecuteGetQuery("Select * from Company");
        }
        
        public static DataTable fillCountry()
        {
            return ExecuteGetQuery("Select * from Country");
        }

        public static DataTable fillState(int countryCode)
        {
            return ExecuteGetQuery("Select * from State Where CountryCode = " + countryCode);
        }
        public static DataTable fillCity(int stateCode)
        {
            return ExecuteGetQuery("Select * from City Where StateCode = " + stateCode);
        }

        public static DataTable GetCountryNameByCode(int code)
        {
            return ExecuteGetQuery("Select Name from Country Where Code = " + code);
        }

        public static DataTable GetStateNameByCode(int code)
        {
            return ExecuteGetQuery("Select Name from State Where Code = " + code);
        }

        public static DataTable GetCityNameByCode(int code)
        {
            return ExecuteGetQuery("Select Name from City Where Code = " + code);
        }

        public static int GetBillNumber(int companyId)
        {
            return Convert.ToInt32( ExecuteGetQuery("Select BillNumber from BillNumbers Where CompanyId = " + companyId).Rows[0][0]);
        }

        public static void SetBillNumber(int companyId, int billNumber)
        {
            ExecuteGetQuery("Update BillNumbers Set BillNumber = " + billNumber + " Where CompanyId = " + companyId);
        }

        public static DataTable GetQuantityUnit()
        {
            return ExecuteGetQuery("Select Unit from QtyUnit");
        }

        public static void SetQuantityUnit(int id, int unit)
        {
            ExecuteGetQuery("Update QtyUnit Set Unit = " + unit + " Where Id = " + id);
        }

        public static DataTable GetWeightUnit()
        {
            return ExecuteGetQuery("Select Unit from WeightUnit");
        }

        public static DataTable GetBrokerList()
        {
            return ExecuteGetQuery("Select Broker from BrokersList");
        }

        public static DataTable GetParticularsList()
        {
            return ExecuteGetQuery("Select Particulars from ParticularsList");
        }

        #endregion

        #region "Bill Operations"
        public static DataTable GetBillDetails()
        {
            return ExecuteGetQuery("Select * from BillDetails");
        }
        
        public static DataTable GetBillParticulars(double billNumber)
        {
            return ExecuteGetQuery("Select * from BillParticulars where BillNumber = " + billNumber);
        }
        
        public static DataTable GetCompany(int Id)
        {
            return ExecuteGetQuery("Select * from Company Where Id = " + Id);
        }

        public static DataTable GetBank(int CompanyId)
        {
            return ExecuteGetQuery("Select * from Bank Where CompanyId = " + CompanyId);
        }

        public static DataTable GetClient(int Id)
        {
            return ExecuteGetQuery("Select * from Client Where Id = " + Id);
        }

        public static void InsertBillDetails(Bill bill)
        {
            ExecuteNonQuery("Insert into BillDetails Values('" + bill.bBillNumber + "','" + bill.bBillDate + "','" + bill.bDeliveryDate + "','" + bill.bSaudaDate + "','" + bill.bDueDate + "','" + bill.bBroker + "','" + bill.bTotalQuantity + "','" + bill.bTotalWeight + "','" + bill.bTotalAmount + "','" + bill.bCompany.cID + "','" + bill.bClient.cID + "')");
        }

        public static void InsertBillParticulars(double billnumber, BillParticular billParticular)
        {
            ExecuteNonQuery("Insert into BillParticular Values('" + billnumber + "','" + billParticular.bpHSNCode + "','" + billParticular.bpTruckNumber + "','" + billParticular.bpQuantity + "','" + billParticular.bpQuantityUnit + "','" + billParticular.bpParticulars + "','" + billParticular.bpWeight + "','" + billParticular.bpWeightUnit + "','" + billParticular.bpRate + "','" + billParticular.bpAmount);
        }

        public static void UpdateQtyUnitDetail(double billnumber, BillParticular billParticular)
        {
            ExecuteNonQuery("Insert into BillParticular Values('" + billnumber + "','" + billParticular.bpHSNCode + "','" + billParticular.bpTruckNumber + "','" + billParticular.bpQuantity + "','" + billParticular.bpQuantityUnit + "','" + billParticular.bpParticulars + "','" + billParticular.bpWeight + "','" + billParticular.bpWeightUnit + "','" + billParticular.bpRate + "','" + billParticular.bpAmount);
        }

        public static void UpdateQtyUnitDetail(string QtyUnit)
        {
            ExecuteNonQuery("Update QtyUnit Set Unit" + QtyUnit);
        }

        public static void UpdateBrokerDetail(string broker)
        {
            ExecuteNonQuery("Update BrokersList Set Broker" + broker);
        }

        public static void UpdateParticularsDetail(string particular)
        {
            ExecuteNonQuery("Update ParticularsList Set Particular" + particular);
        }

        public static void UpdateBillNumber(double billNumber, int companyId)
        {
            ExecuteNonQuery("Update BillNumbers Set BillNumber = " + billNumber + " Where CompanyId = " + companyId);
        }
        #endregion
    }
}
