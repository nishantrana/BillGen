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
    public class Company
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
        public string cMobile1 { get; set; }
        public string cMobile2 { get; set; }
        public string cMobile3 { get; set; }
        public string cAdditionalInfo { get; set; }
        public string cGLogo { get; set; }
        public string cLogoPath { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Company() { }
                
        public void SetCompanyVariables(DataTable _dtCompany)
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
            cMobile1 = Convert.ToString(_dtCompany.Rows[0]["MobileNumer1"]);
            cMobile2 = Convert.ToString(_dtCompany.Rows[0]["MobileNumer2"]);
            cMobile3 = Convert.ToString(_dtCompany.Rows[0]["MobileNumer3"]);
            cAdditionalInfo = Convert.ToString(_dtCompany.Rows[0]["AdditionalInfo"]);
            cGLogo = Convert.ToString(_dtCompany.Rows[0]["GLogo"]);
            cLogoPath = Convert.ToString(_dtCompany.Rows[0]["LogoPath"]);
        }

        public void SetCompanyVariables(int ID, string Name,
        string GSTIN, string Address, 
        int CountryCode, int StateCode,
        int CityCode, int PIN, string Email,
        string Website, string Mobile1, string Mobile2,
        string Mobile3, string AdditionalInfo, string GLogo,
        string LogoPath)
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
            cMobile1 = Mobile1;
            cMobile2 = Mobile2;
            cMobile3 = Mobile3;
            cAdditionalInfo = AdditionalInfo;
            cGLogo = GLogo;
            cLogoPath = LogoPath;
        }

        /// <summary>
        /// Method used to select all company entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public static DataTable SelectAllCompanyInfo()
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Company");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method used to select particular company entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public DataTable SelectCompanyInfo(int _cId)
        {
            try
            {
                return CommonClass.ExecuteGetQuery("Select * from Company Where Id = " + _cId);
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
        /// <param name="_cStateCode"></param>
        /// <param name="_cPIN"></param>
        /// <param name="_cEmail"></param>
        /// <param name="_cWebsite"></param>
        /// <param name="_cMobile1"></param>
        /// <param name="_cMobile2"></param>
        /// <param name="_cMobile3"></param>
        /// <param name="_cAdditionalInfo"></param>
        /// <param name="_cGLogo"></param>
        /// <param name="_cLogoPath"></param>
        public void AddCompany(string _cName, string _cGSTIN,
            string _cAddress, int _cCountry, int _cStateCode, int _cCity, int _cPIN,
            string _cEmail, string _cWebsite, string _cMobile1,
            string _cMobile2, string _cMobile3, string _cAdditionalInfo,
            string _cGLogo, string _cLogoPath)
        {
            try
            {
                CommonClass.ExecuteNonQuery("Insert into Company values('" + _cName + "','" + _cGSTIN + "','" +
                    _cAddress + "','" + _cCountry + "','" + _cStateCode + "','" + _cCity + "','" +
                    _cPIN + "','" + _cEmail + "','" + _cWebsite + "','" + _cMobile1 + "','" +
                    _cMobile2 + "','" + _cMobile3 + "','" + _cAdditionalInfo + "','" + _cGLogo + "','" + _cLogoPath);
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
        /// <param name="_cLogo"></param>
        public void UpdateCompany(int _cId, string _cGSTIN,
            string _cAddress, int _cCountry, int _cStateCode, int _cCity, int _cPIN,
            string _cEmail, string _cWebsite, string _cMobile1,
            string _cMobile2, string _cMobile3, string _cAdditionalInfo,
            string _cGLogo, string _cLogoPath)
        {
            try
            {
                CommonClass.ExecuteNonQuery("Update Company Set GSTIN = '" + _cGSTIN +
                    "', Address = '" + _cAddress + "', Country = '" + _cCountry + "', StateCode = '" + _cStateCode + "', City = '" + _cCity + "', PIN = '" +
                    _cPIN + "', Email = '" + _cEmail + "', Website = '" + _cWebsite + "', Mobile1 = '" + _cMobile1 +
                    "', Mobile2 = '" + _cMobile2 + "', Mobile3 = '" + _cMobile3 + "', AdditionalInfo = '" + _cAdditionalInfo +
                    "', GLogo = '" + _cGLogo + "', LogoPath = '" + _cLogoPath + " Where Id = " + _cId);
            }
            catch(Exception ex)
            {
                
            }
        }

        public void UpdateCompany()
        {
            try
            {
                CommonClass.ExecuteNonQuery("Update Company Set GSTIN = '" + this.cGSTIN +
                    "', Address = '" + this.cAddress + "', Country = '" + this.cCountry +
                    "', StateCode = '" + this.cStateCode + "', City = '" + this.cCity + "', PIN = '" +
                    this.cPIN + "', Email = '" + this.cEmail + "', Website = '" + this.cWebsite +
                    "', Mobile1 = '" + this.cMobile1 +
                    "', Mobile2 = '" + this.cMobile2 + "', Mobile3 = '" + this.cMobile3 +
                    "', AdditionalInfo = '" + this.cAdditionalInfo +
                    "', GLogo = '" + this.cGLogo + "', LogoPath = '" + this.cLogoPath + " Where Id = " + this.cID);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Method used to remove company entry from DB
        /// </summary>
        /// <param name="_cId"></param>
        public void RemoveCompany(int _cId)
        {
            try
            {
                CommonClass.ExecuteNonQuery("Delete Company where Id = " + _cId);
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}
