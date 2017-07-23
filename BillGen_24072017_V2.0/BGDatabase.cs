using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using System.Data;
using System.Windows.Forms;

namespace BillGen
{
    class BGDatabase
    {
        //MySqlConnection con;
        //MySqlCommand com;
        //MySqlDataAdapter da;
        //DataSet ds;
        //MySqlDataReader dr;

        Dictionary<string, string> CompanyDetailsCol;
        Dictionary<string, string> BankDetailsCol;
        Dictionary<string, string> ClientDetailsCol;

        public BGDatabase()
        {
        
        }

        public BGDatabase(string Host, string User, string Password, string Database)
        {
            //con = new MySqlConnection("server = localhost; Database = billgen; User ID = root; Password = root;");
        }

        #region "Company Details"
        public bool AddCompany(string Name, byte[] Logo, string TIN, string CST, string Address, string MobileNo)
        {
            bool flag = false;
            try
            {
                com = new MySqlCommand("Insert into bg_companydetail (name, tin, cst, address, mobile, logo) values('" + Name + "','" + TIN + "','" + CST + "','" + Address + "','" + MobileNo + "'," + Logo + ")", con);
                int i = com.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            return flag;
        }

        public bool UpdateCompany(int id, string Name, byte[] Logo, string TIN, string CST, string Address, string MobileNo)
        {
            bool flag = false;
            try
            {
                com = new MySqlCommand("update bg_companydetail set name = '" + Name + "', tin = '" + TIN + "', cst = '" + CST + "', address = '" + Address + "', mobile = '" + MobileNo + "', logo = " + Logo + " where id = " + id + ")", con);
                int i = com.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }
        
        public bool DeleteCompany()
        {
            bool flag=false;
        return flag;}
        #endregion

        #region "Bank Details"
        public bool AddBankDetail(int CompanyID, string BankName, string Address, int AccountNumber, string IFSC)
        {
            bool flag = false;
            try
            {
                com = new MySqlCommand("Insert into bg_bankdetail (company_id, name, address, account_number, ifsc_code) values('" + CompanyID + "','" + BankName + "','" + Address + "','" + AccountNumber + "'," + IFSC + ")", con);
                int i = com.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }

        public bool UpdateBankDetail(int Company_Id, string BankName, string Address, string Account_Number, string IFSC)
        {
            bool flag = false;
            try
            {
                com = new MySqlCommand("update bg_bankdetail set name = '" + BankName + "', address = '" + Address + "', account_number = '" + Account_Number + "', ifsc_code = '" + IFSC + " where company_id = " + Company_Id + ")", con);
                int i = com.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }
        
        
        public bool DeleteBankDetail()
        {
            bool flag=false;
        return flag;}
        #endregion

        #region "Client Details"
        public bool AddClientDetail(int CompanyID, string ClientName, string Address, string TIN, string CST)
        {
            bool flag = false;
            try
            {
                com = new MySqlCommand("Insert into bg_clientdetail (company_id, name, address, tin, cst) values('" + CompanyID + "','" + ClientName + "','" + Address + "','" + TIN + "'," + CST + ")", con);
                int i = com.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }

        public bool UpdateBankDetail(int CompanyID, int ClientID, string ClientName, string Address, string TIN, string CST)
        {
            bool flag = false;
            try
            {
                com = new MySqlCommand("update bg_clientdetail set name = '" + ClientName + "', address = '" + Address + "', tin = '" + TIN + "', cst = '" + CST + " where client_id = " + ClientID + ")", con);
                int i = com.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return flag;
        }
        
        public bool DeleteClient()
        {
            bool flag=false;
        return flag;}
        #endregion
    }
}
