using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace BillGen.Class
{
    public class Bank
    {
        public int bID { get; set; }
        public string bName { get; set; }
        public string bAddress { get; set; }
        public string bCountry { get; set; }
        public int bCountryCode { get; set; }
        public string bState { get; set; }
        public int bStateCode { get; set; }
        public string bCity { get; set; }
        public int bCityCode { get; set; }
        public string bAccount { get; set; }
        public string bIFSC { get; set; }
        public int bCompanyID { get; set; }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Bank() { }

        public void SetBankVariables(DataTable _dtBank)
        {
            bID = Convert.ToInt32(_dtBank.Rows[0]["Id"]);
            bName = Convert.ToString(_dtBank.Rows[0]["Name"]);
            bAddress = Convert.ToString(_dtBank.Rows[0]["Address"]);
            bCountry = Convert.ToString(CommonClass.GetCountryNameByCode(Convert.ToInt32(_dtBank.Rows[0]["Country"])).Rows[0]["Name"]);
            bCountryCode = Convert.ToInt32(_dtBank.Rows[0]["Country"]);
            bState = Convert.ToString(CommonClass.GetStateNameByCode(Convert.ToInt32(_dtBank.Rows[0]["StateCode"])).Rows[0]["Name"]);
            bStateCode = Convert.ToInt32(_dtBank.Rows[0]["StateCode"]);
            bCity = Convert.ToString(CommonClass.GetCityNameByCode(Convert.ToInt32(_dtBank.Rows[0]["City"])).Rows[0]["Name"]);
            bCityCode = Convert.ToInt32(_dtBank.Rows[0]["City"]);
            bAccount = Convert.ToString(_dtBank.Rows[0]["Account"]);
            bIFSC = Convert.ToString(_dtBank.Rows[0]["IFSC"]);
            bCompanyID = Convert.ToInt32(_dtBank.Rows[0]["CompanyId"]);
        }

        public void SetBankVariables(int ID, string Name,
        string Address, int CountryCode, int StateCode, int CityCode, string Account,
        string IFSC, int CompanyID)
        {
            bID = ID;
            bName = Name;
            bAddress = Address;
            bCountry = Convert.ToString(CommonClass.GetCountryNameByCode(CountryCode).Rows[0]["Name"]);
            bCountryCode = CountryCode;
            bState = Convert.ToString(CommonClass.GetStateNameByCode(StateCode).Rows[0]["Name"]);
            bStateCode = StateCode;
            bCity = Convert.ToString(CommonClass.GetCityNameByCode(CityCode).Rows[0]["Name"]);
            bCityCode = CityCode;
            bAccount = Account;
            bIFSC = IFSC;
            bCompanyID = CompanyID;
        }

        /// <summary>
        /// Method used to select all Bank entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public static DataTable SelectAllBankInfo()
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Bank");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method used to select particular client entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public DataTable SelectBankInfo(int _bId)
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Bank Where Id = " + _bId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Method used to select particular client entry on basis of Company Id from DB
        /// </summary>
        /// <param name="_cId"></param>
        public DataTable SelectBankOfCompany(int _bCompanyId)
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Bank Where CompanyId = " + _bCompanyId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public void AddOrUpdateBankInfo()
        {
            try
            {
                if (CommonClass.ExecuteCheckEntryQuery("Select * from Bank Where CountryId = " + bCompanyID) == 0)
                {
                    CommonClass.ExecuteNonQuery("Insert into Bank values('" + bName + "','" + bAddress +
                        "','" + bCountry + "','" + bStateCode + "','" + bCity + "','" +
                        bAccount + "','" + bIFSC + "','" + bCompanyID);
                }
                else
                {
                    CommonClass.ExecuteNonQuery("Update Bank Set CompanyName = '" + bName + "', Address = '" + bAddress +
                    "', Country = '" + bCountry + "', StateCode = '" + bStateCode + "', City = '" + bCity +
                    "', Account = '" + bAccount + "', IFSC = " + bIFSC + " Where Id = " + bCompanyID);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Method used to remove company entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public void RemoveClient(int _cId)
        {
            try
            {
                CommonClass.ExecuteNonQuery("Delete Client where Id = " + _cId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
