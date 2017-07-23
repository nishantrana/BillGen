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
    public class Client
    {
        public int cID { get; set; }
        public string cName { get; set; }
        public string cGSTIN { get; set; }
        public string cAddress { get; set; }
        public string cCountry { get; set; }
        public int cCountryCode { get; set; }
        public string cState { get; set; }
        public int cStateCode { get; set; }
        public string cCity { get; set; }
        public int cCityCode { get; set; }
        public int cPIN { get; set; }
        public string cEmail { get; set; }
        public string cWebsite { get; set; }
        public string cMobileNumer1 { get; set; }
        public string cMobileNumer2 { get; set; }
        public string cMobileNumer3 { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Client() { }

        public void SetClientVariables(DataTable _dtCompany)
        {
            cID = Convert.ToInt32(_dtCompany.Rows[0]["Id"]);
            cName = Convert.ToString(_dtCompany.Rows[0]["Name"]);
            cGSTIN = Convert.ToString(_dtCompany.Rows[0]["GSTIN"]);
            cAddress = Convert.ToString(_dtCompany.Rows[0]["Address"]);
            cCountry = Convert.ToString(CommonClass.GetCountryNameByCode(Convert.ToInt32(_dtCompany.Rows[0]["Country"])).Rows[0]["Name"]);
            cCountryCode = Convert.ToInt32(_dtCompany.Rows[0]["Country"]);
            cState = Convert.ToString(CommonClass.GetStateNameByCode(Convert.ToInt32(_dtCompany.Rows[0]["StateCode"])).Rows[0]["Name"]);
            cStateCode = Convert.ToInt32(_dtCompany.Rows[0]["StateCode"]);
            cCity = Convert.ToString(CommonClass.GetCityNameByCode(Convert.ToInt32(_dtCompany.Rows[0]["City"])).Rows[0]["Name"]);
            cCityCode = Convert.ToInt32(_dtCompany.Rows[0]["City"]);
            cPIN = Convert.ToInt32(_dtCompany.Rows[0]["PIN"]);
            cEmail = Convert.ToString(_dtCompany.Rows[0]["Email"]);
            cWebsite = Convert.ToString(_dtCompany.Rows[0]["Website"]);
            cMobileNumer1 = Convert.ToString(_dtCompany.Rows[0]["MobileNumer1"]);
            cMobileNumer2 = Convert.ToString(_dtCompany.Rows[0]["MobileNumer2"]);
            cMobileNumer3 = Convert.ToString(_dtCompany.Rows[0]["MobileNumer3"]);
        }

        public void SetClientVariables(int ID, string Name,
        string GSTIN, string Address,
        int CountryCode, int StateCode,
        int CityCode, int PIN, string Email,
        string Website, string Mobile1, string Mobile2,
        string Mobile3)
        {
            cID = ID;
            cName = Name;
            cGSTIN = GSTIN;
            cAddress = Address;
            cCountry = Convert.ToString(CommonClass.GetCountryNameByCode(CountryCode).Rows[0]["Name"]);
            cCountryCode = CountryCode;
            cState = Convert.ToString(CommonClass.GetStateNameByCode(StateCode).Rows[0]["Name"]);
            cStateCode = StateCode;
            cCity = Convert.ToString(CommonClass.GetCityNameByCode(CityCode).Rows[0]["Name"]);
            cCityCode = CityCode;
            cPIN = PIN;
            cEmail = Email;
            cWebsite = Website;
            cMobileNumer1 = Mobile1;
            cMobileNumer2 = Mobile2;
            cMobileNumer3 = Mobile3;
        }

        /// <summary>
        /// Method used to select all client entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public static DataTable SelectAllClientInfo()
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Client");
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
        public DataTable SelectClientInfo(int _cId)
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Client Where Id = " + _cId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method used to add company
        /// </summary>
        /// <param name="_cName"></param>
        /// <param name="_cGSTIN"></param>
        /// <param name="_cAddress"></param>
        /// <param name="_cCountry"></param>
        /// <param name="_cState"></param>
        /// <param name="_cPIN"></param>
        /// <param name="_cEmail"></param>
        /// <param name="_cWebsite"></param>
        /// <param name="_cMobile1"></param>
        /// <param name="_cMobile2"></param>
        /// <param name="_cMobile3"></param>
        /// <param name="_cAdditionalInfo"></param>
        public void AddClient(string _cName, string _cGSTIN, string _cAddress,
           int _cCountry, int _cStateCode, int _cCity, int _cPIN,
           string _cEmail, string _cWebsite, string _cMobileNumer1, string _cMobileNumer2,
           string _cMobileNumer3)
        {
            try
            {
                CommonClass.ExecuteNonQuery("Insert into Client values('" + _cName + "','" + _cGSTIN + "','" +
                    _cAddress + "','" + _cCountry + "','" + _cStateCode + "','" + _cCity + "','" +
                    _cPIN + "','" + _cEmail + "','" + _cWebsite + "','" + _cMobileNumer1 + "','" +
                    _cMobileNumer2 + "','" + _cMobileNumer3);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Method used to update company
        /// </summary>
        /// <param name="_cName"></param>
        /// <param name="_cGSTIN"></param>
        /// <param name="_cAddress"></param>
        /// <param name="_cCountry"></param>
        /// <param name="_cState"></param>
        /// <param name="_cPIN"></param>
        /// <param name="_cEmail"></param>
        /// <param name="_cWebsite"></param>
        /// <param name="_cMobile1"></param>
        /// <param name="_cMobile2"></param>
        /// <param name="_cMobile3"></param>
        /// <param name="_cAdditionalInfo"></param>
        public void UpdateClient(int _cId, string _cName, string _cGSTIN,
            string _cAddress, string _cCountry, int _cStateCode, string _cCity, int _cPIN,
            string _cEmail, string _cWebsite, string _cMobile1,
            string _cMobile2, string _cMobile3)
        {
            try
            {
                CommonClass.ExecuteNonQuery("Update Client Set CompanyName = '" + _cName + "', GSTIN = '" + _cGSTIN +
                    "', Address = '" + _cAddress + "', Country = '" + _cCountry + "', StateCode = '" + _cStateCode + "', City = '" + _cCity +
                    "', PIN = '" + _cPIN + "', Email = '" + _cEmail + "', Website = '" + _cWebsite + "', Mobile1 = '" + _cMobile1 +
                    "', Mobile2 = '" + _cMobile2 + "', Mobile3 = '" + _cMobile3 + " Where Id = " + _cId);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Method used to remove company entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public void RemoveClient()
        {
            try
            {
                CommonClass.ExecuteNonQuery("Delete Client where Id = " + this.cID);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
