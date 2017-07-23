using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using BillGen.Class;
using System.Data;

namespace BillGen
{
    public class BG_DataHandler
    {
        /// <summary>
        /// Key is Company Id
        /// Value is Company Object
        /// </summary>
        public Dictionary<int, Company> CompanyDict;
        
        /// <summary>
        /// Key is Company ID
        /// Value is Bank Object
        /// </summary>
        public Dictionary<int, Bank> BankDict;
        public Dictionary<int, Client> ClientDict;
        /// <summary>
        /// Key : CompanyID
        /// Key : Bill Number
        /// Value : Bill Object
        /// </summary>
        public Dictionary<int ,Dictionary<double, Bill>> BillDict;
        public List<string> QtyUnitList;
        public List<string> WeightUnitList;
        public List<string> BrokerList;
        public List<string> ParticularList;
        //public double BillNumber = 0;
        public Dictionary<int, double> BillNumber;
        static int companySize = 5;

        static string _G1 = "";
        static string _C1 = "";
        static string _G2 = "";
        static string _C2 = "";
        static string _G3 = "";
        static string _C3 = "";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BG_DataHandler()
        {
            BillNumber = new Dictionary<int, double>();

            //_G1 = System.Windows.Forms.Application.StartupPath + "\\Logo\\G1.png";
            //_C1 = System.Windows.Forms.Application.StartupPath + "\\Logo\\C1.png";
            //_G2 = System.Windows.Forms.Application.StartupPath + "\\Logo\\G2.png";
            //_C2 = System.Windows.Forms.Application.StartupPath + "\\Logo\\C2.png";
            //_G3 = System.Windows.Forms.Application.StartupPath + "\\Logo\\G3.png";
            //_C3 = System.Windows.Forms.Application.StartupPath + "\\Logo\\C3.png";
            ReadAllConfig();
        }

        void ReadAllConfig()
        {
            Company _company;
            Bank _bank;
            Client _client;

            #region"Company"

            CompanyDict = new Dictionary<int, Company>();
            int size = 0;
            //Company ID | Company Name|TIN Number| CST Number|Address|Mobile1,Mobile2,Mobile3

            try
            {
                _company = null;
                //string[] compArr = MyCrypto.decode(sr.ReadLine()).Split('|');

                DataTable dtCompanys = Company.SelectAllCompanyInfo();
                size = dtCompanys.Rows.Count;
                if (size > companySize) return;

                foreach (DataRow dr in dtCompanys.Rows)
                {
                    _company = new Company();
                    _company.SetCompanyVariables(Convert.ToInt32(dr["Id"]), Convert.ToString(dr["Name"]),
                            Convert.ToString(dr["GSTIN"]), Convert.ToString(dr["Address"]),
                            Convert.ToInt32(dr["Country"]), Convert.ToInt32(dr["StateCode"]),
                            Convert.ToInt32(dr["City"]), Convert.ToInt32(dr["PIN"]),
                            Convert.ToString(dr["Email"]), Convert.ToString(dr["Website"]),
                            Convert.ToString(dr["MobileNumer1"]), Convert.ToString(dr["MobileNumer2"]),
                            Convert.ToString(dr["MobileNumer3"]), Convert.ToString(dr["AdditionalInfo"]),
                            Convert.ToString(dr["GLogo"]), Convert.ToString(dr["LogoPath"]));

                    if (!CompanyDict.ContainsKey(_company.cID)) CompanyDict.Add(_company.cID, _company);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Company DB Error"); }
            #endregion

            #region"Bank"
            BankDict = new Dictionary<int, Bank>();
            //Bank Name|Address|A/C Number| IFSC Code

            try
            {
                DataTable dtBanks = Bank.SelectAllBankInfo();

                foreach (DataRow dr in dtBanks.Rows)
                {
                    _bank = new Bank();
                    _bank.SetBankVariables(Convert.ToInt32(dr["Id"]), Convert.ToString(dr["Name"]),
                            Convert.ToString(dr["Address"]),
                            Convert.ToInt32(dr["Country"]), Convert.ToInt32(dr["StateCode"]),
                            Convert.ToInt32(dr["City"]), Convert.ToString(dr["Account"]),
                            Convert.ToString(dr["IFSC"]), Convert.ToInt32(dr["CompanyId"]));

                    if (!BankDict.ContainsKey(_bank.bCompanyID)) BankDict.Add(_bank.bCompanyID, _bank);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Bank DB Error"); }
            #endregion

            #region"Client"
            ClientDict = new Dictionary<int, Client>();
            //Client Name|Address|TIN Number

            try
            {
                //string[] ClArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                DataTable dtClients = Client.SelectAllClientInfo();

                foreach (DataRow dr in dtClients.Rows)
                {
                    _client = new Client();
                    _client.SetClientVariables(Convert.ToInt32(dr["Id"]), Convert.ToString(dr["Name"]),
                            Convert.ToString(dr["GSTIN"]), Convert.ToString(dr["Address"]),
                            Convert.ToInt32(dr["Country"]), Convert.ToInt32(dr["StateCode"]),
                            Convert.ToInt32(dr["City"]), Convert.ToInt32(dr["PIN"]),
                            Convert.ToString(dr["Email"]), Convert.ToString(dr["Website"]),
                            Convert.ToString(dr["MobileNumer1"]), Convert.ToString(dr["MobileNumer2"]),
                            Convert.ToString(dr["MobileNumer3"]));

                    if (!ClientDict.ContainsKey(_client.cID)) ClientDict.Add(_client.cID, _client);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Client DB Error"); }
            #endregion

            #region"Bill Number"
            //Bill Number For Company 1
            try
            {
                //string[] billArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                int billArr = CommonClass.GetBillNumber(1);

                if (!BillNumber.ContainsKey(1))
                    BillNumber.Add(1, billArr + 1);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Bill Number Error in Company 1"); }

            //Bill Number For Company 2
            try
            {
                //string[] billArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                int billArr = CommonClass.GetBillNumber(2);

                if (!BillNumber.ContainsKey(2))
                    BillNumber.Add(2, billArr + 1);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Bill Number Error in Company 2"); }

            //Bill Number For Company 3
            try
            {
                //string[] billArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                int billArr = CommonClass.GetBillNumber(3);

                if (!BillNumber.ContainsKey(3))
                    BillNumber.Add(3, billArr + 1);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Bill Number Error in Company 3"); }

            #endregion

            #region"Qty Unit"
            // sr = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\DBase\QUnit.txt");
            QtyUnitList = new List<string>();
            //sr.ReadLine();
            DataTable dtQtyUnit = CommonClass.GetQuantityUnit();
            //Unit
            foreach (DataRow dr in dtQtyUnit.Rows)
            {
                try
                {
                    //string[] QUnitArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                    QtyUnitList.Add(dr[0].ToString().ToUpper());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Qty Unit DB Error"); }
            }
            #endregion

            #region"Weight Unit"
            //sr = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\DBase\WUnit.txt");
            WeightUnitList = new List<string>();
            DataTable dtWeightUnit = CommonClass.GetWeightUnit();
            //WUnit
            foreach (DataRow dr in dtWeightUnit.Rows)
            {
                try
                {
                    //string[] WUnitArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                    WeightUnitList.Add(dr[0].ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Weight Unit DB Error"); }
            }
            #endregion

            #region"Broker"
            //sr = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\DBase\Brokers.txt");
            BrokerList = new List<string>();
            DataTable dtBrokerList = CommonClass.GetBrokerList();
            //Broker
            foreach (DataRow dr in dtBrokerList.Rows)
            {
                try
                {
                    // string[] BrokerArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                    BrokerList.Add(dr[0].ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Broker DB Error"); }
            }
            #endregion

            #region"Particulars"
            //sr = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\DBase\Particulars.txt");
            ParticularList = new List<string>();
            DataTable dtParticularsList = CommonClass.GetParticularsList();
            //Particulars
            foreach (DataRow dr in dtParticularsList.Rows)
            {
                try
                {
                    //string[] ParticularArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                    ParticularList.Add(dr[0].ToString());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Particular DB Error"); }
            }
            #endregion

            #region"Bills"
            BillDict = new Dictionary<int, Dictionary<double, Bill>>();
            DataTable dtBill = CommonClass.GetBillDetails();
            foreach (DataRow Arr in dtBill.Rows)
            {
                Bill bill = new Bill();
                //string[] Arr = MyCrypto.decode(sr.ReadLine()).Split('|');
                bill.bBillNumber = Convert.ToDouble(Arr["BillNumber"]);
                bill.bBillDate = Convert.ToDateTime(Arr["BillDate"]);
                bill.bDeliveryDate = Convert.ToDateTime(Arr["DeliveryDate"]);
                bill.bSaudaDate = Convert.ToDateTime(Arr["SaudaDate"]);
                bill.bDueDate = Convert.ToDateTime(Arr["DueDate"]);
                bill.bBroker = Convert.ToString(Arr["Broker"]);
                bill.bTotalQuantity = Convert.ToDouble(Arr["TotalQty"]);
                bill.bTotalWeight = Convert.ToDouble(Arr["TotalWeight"]);
                bill.bTotalAmount = Convert.ToDouble(Arr["TotalAmount"]);

                DataTable dtCompany = CommonClass.GetCompany(Convert.ToInt32(Arr["CompanyId"]));

                bill.bCompany.cID = Convert.ToInt32(Arr["CompanyId"]);
                bill.bCompany.cName = Convert.ToString(dtCompany.Rows[0]["Name"]);
                bill.bCompany.cGLogo = Convert.ToString(dtCompany.Rows[0]["GLogo"]);
                bill.bCompany.cLogoPath = Convert.ToString(dtCompany.Rows[0]["LogoPath"]);
                bill.bCompany.cAddress = Convert.ToString(dtCompany.Rows[0]["Address"]);
                bill.bCompany.cGSTIN = Convert.ToString(dtCompany.Rows[0]["GSTIN"]);
                bill.bCompany.cMobile1 = Convert.ToString(dtCompany.Rows[0]["MobileNumer1"]);
                bill.bCompany.cMobile2 = Convert.ToString(dtCompany.Rows[0]["MobileNumer2"]);
                bill.bCompany.cMobile3 = Convert.ToString(dtCompany.Rows[0]["MobileNumer3"]);

                DataTable dtClient = CommonClass.GetClient(Convert.ToInt32(Arr["ClientID"]));

                bill.bClient.cName = Convert.ToString(dtCompany.Rows[0]["Name"]);
                bill.bClient.cAddress = Convert.ToString(dtCompany.Rows[0]["Address"]);
                bill.bClient.cGSTIN = Convert.ToString(dtCompany.Rows[0]["GSTIN"]);

                DataTable dtBank = CommonClass.GetBank(Convert.ToInt32(Arr["CompanyId"]));

                bill.bBank.bCompanyID = Convert.ToInt32(Arr["CompanyId"]);
                bill.bBank.bName = Convert.ToString(dtBank.Rows[0]["Name"]);
                bill.bBank.bAddress = Convert.ToString(dtBank.Rows[0]["Address"]);
                bill.bBank.bAccount = Convert.ToString(dtBank.Rows[0]["Account"]);
                bill.bBank.bIFSC = Convert.ToString(dtBank.Rows[0]["IFSC"]);

                DataTable dtBillParticulars = CommonClass.GetBillParticulars(bill.bBillNumber);
                foreach (DataRow sr in dtBillParticulars.Rows)
                {
                    try
                    {
                        BillParticular bp = new BillParticular();
                        //string[] fieldArr = MyCrypto.decode(sr.ReadLine()).Split('|');
                        bp.bpTruckNumber = Convert.ToString(sr["TruckNumber"]);
                        bp.bpQuantity = Convert.ToDouble(sr["Qty"]);
                        bp.bpQuantityUnit = Convert.ToString(sr["QtyUnit"]);
                        bp.bpParticulars = Convert.ToString(sr["Particulars"]);
                        bp.bpWeight = Convert.ToDouble(sr["Weight"]);
                        bp.bpWeightUnit = Convert.ToString(sr["WeightUnit"]);
                        bp.bpRate = Convert.ToDouble(sr["Rate"]);
                        bp.bpAmount = Convert.ToDouble(sr["Amount"]);

                        bill.bBillParticulars.Add(bp);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Raw Bill DB Error"); }
                }

                if (!BillDict.ContainsKey(bill.bCompany.cID))
                {
                    BillDict.Add(bill.bCompany.cID, new Dictionary<double, Bill>());
                }

                if (!BillDict[bill.bCompany.cID].ContainsKey(bill.bBillNumber))
                    BillDict[bill.bCompany.cID].Add(bill.bBillNumber, bill);
            }
            #endregion
        }

        public void AddOrUpdateRawBill(Bill _bill)
        {
            try
            {
                string fileName = _bill.bBillNumber + ".txt";
                //sw.WriteLine(MyCrypto.encode(s));
                CommonClass.InsertBillDetails(_bill);

                foreach (BillParticular bp in _bill.bBillParticulars)
                {
                    //sw.WriteLine(MyCrypto.encode(s));
                    CommonClass.InsertBillParticulars(_bill.bBillNumber, bp);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }

        public void _AddOrUpdateRawBill(Bill _bill)
        {
            try
            {
                string fileName = _bill.bBillNumber + ".txt";
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Bills\\RAW")) Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Bills\\RAW");
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Bills\\RAW\\" + _bill.bCompany.cName.ToUpper().ToString())) Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Bills\\RAW\\" + _bill.bCompany.cName.ToUpper().ToString());

                string path = System.Windows.Forms.Application.StartupPath + "\\Bills\\RAW\\" + _bill.bCompany.cName.ToUpper().ToString() + "\\" + fileName;
                try
                {
                    FileInfo fi = new FileInfo(path);
                    if (fi.Exists) fi.Delete();
                }
                catch (Exception ex) { MessageBox.Show("Bill Update Error : " + ex.Message); }
                FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                string s = _bill.ToString();
                sw.WriteLine(MyCrypto.encode(s));
                
                foreach (BillParticular bp in _bill.bBillParticulars)
                {
                    s = bp.ToString();
                    sw.WriteLine(MyCrypto.encode(s));
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }
               
        public void UpdateBillNumber(double BillNumber, int CompanyID)
        {
            try
            {
                //sw.WriteLine(MyCrypto.encode(BillNumber));
                CommonClass.UpdateBillNumber(BillNumber, CompanyID);
            }
            catch (Exception ex) { MessageBox.Show("Update Bill Number Error : " + ex.Message); }
        }
    }
          
    public static class MyCrypto
    {
        public static string encode(string text)
        {
            byte[] mybyte = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(mybyte);
        }

        public static string decode(string text)
        {
            byte[] mybyte = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(mybyte);
        }
    }
}
