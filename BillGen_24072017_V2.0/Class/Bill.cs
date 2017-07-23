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
    public class Bill
    {
        public double bBillNumber { get; set; }
        public DateTime bBillDate { get; set; }
        public DateTime bDeliveryDate { get; set; }
        public DateTime bSaudaDate { get; set; }
        public DateTime bDueDate { get; set; }
        public string bBroker { get; set; }

        public Company bCompany;
        public Client bClient;
        public Bank bBank;
        public List<BillParticular> bBillParticulars;

        public double bTotalQuantity { get; set; }
        public double bTotalWeight { get; set; }
        public double bTotalAmount { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Bill()
        {
            bCompany = new Company();
            bClient = new Client();
            bBank = new Bank();
            bBillParticulars = new List<BillParticular>();
        }

        public Bill(double _bBillNumber, DateTime _bBillDate, DateTime _bDeliveryDate, string _bBroker, string _bTruckNumber, Company _bCompany, Client _bClient, Bank _bBank)
        {
            bBillNumber = _bBillNumber;
            bBillDate = _bBillDate;
            bDeliveryDate = _bDeliveryDate;
            bSaudaDate = _bDeliveryDate;
            bDueDate = _bDeliveryDate;
            bBroker = _bBroker;
            bCompany = _bCompany;
            bClient = _bClient;
            bBank = _bBank;

            bBillParticulars = new List<BillParticular>();
            bTotalQuantity = 0;
            bTotalWeight = 0;
            bTotalAmount = 0;
        }

        public void ResetBill()
        {
            bTotalQuantity = 0;
            bTotalWeight = 0;
            bTotalAmount = 0;
            bBillParticulars.Clear();
        }

        public void CalculateTotalQty()
        {
            if (bBillParticulars.Count > 0)
            {
                foreach (BillParticular bp in bBillParticulars)
                {
                    bTotalQuantity += bp.bpQuantity;
                }
            }
        }
        public void CalculateTotalWeight()
        {
            if (bBillParticulars.Count > 0)
            {
                foreach (BillParticular bp in bBillParticulars)
                {
                    bTotalWeight += bp.bpWeight;
                }
            }
        }
        public void CalculateTotalAmount()
        {
            if (bBillParticulars.Count > 0)
            {
                foreach (BillParticular bp in bBillParticulars)
                {
                    if (bp.bpWeightUnit.ToUpper() == "KG")
                        bTotalAmount += ((bp.bpRate * .010) * bp.bpWeight);
                    else if (bp.bpWeightUnit.ToUpper() == "TN")
                        bTotalAmount += ((bp.bpRate * 10) * bp.bpWeight);
                }
                bTotalAmount = Math.Round(bTotalAmount);
            }
        }

        /// <summary>
        /// Gets data from MyData.xml as rows.  
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetRows()
        {
            List<BillParticular> rows = bBillParticulars;

            ////if (File.Exists("MyData.xml"))
            //{
            //    //// Create the query 
            //    //var rowsFromFile = from c in XDocument.Load(
            //    //            "MyData.xml").Elements(
            //    //            "Data").Elements("Rows").Elements("Row")
            //    //                   select c;

            //    // Execute the query 
            //    foreach (var row in rows)
            //    {
            //        rows.Add(new ListViewData(row.Element("col1").Value,
            //                row.Element("col2").Value));
            //    }
            //}
            return rows;
        }
    }

}
