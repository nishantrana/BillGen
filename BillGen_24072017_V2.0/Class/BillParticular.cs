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
    public class BillParticular
    {
        public string bpHSNCode { get; set; }
        public string bpTruckNumber { get; set; }
        public double bpQuantity { get; set; }
        public string bpQuantityUnit { get; set; }
        public string bpParticulars { get; set; }
        public double bpWeight { get; set; }
        public string bpWeightUnit { get; set; }
        public double bpRate { get; set; }
        public double bpAmount { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BillParticular() { }

        public BillParticular(int _bpQuantity, string _bpParticulars, double _bpWeight, double _bpRate)
        {
            bpQuantity = _bpQuantity;
            bpParticulars = _bpParticulars;
            bpWeight = _bpWeight;
            bpRate = _bpRate;
            //bpAmount = (_bpWeight * _bpRate);
            bpAmount = ((_bpRate * _bpQuantity * _bpWeight) / 100);
        }
        public BillParticular(int _bpQuantity, string _bpQuantityUnit, string _bpParticulars, double _bpWeight, string _bpWeightUnit, double _bpRate)
        {
            bpQuantity = _bpQuantity;
            bpQuantityUnit = _bpQuantityUnit;
            bpParticulars = _bpParticulars;
            bpWeight = _bpWeight;
            bpWeightUnit = _bpWeightUnit;
            bpRate = _bpRate;

            if (_bpWeightUnit.ToUpper() == "KG")
            {
                //lvc.bpAmount = ((Rate * Qty * Weight) / 100);
                //lvc.bpAmount = ((Rate * .010) * Qty * Weight);
                //bpAmount = ((_bpRate * .010) * _bpQuantity * _bpWeight);
                bpAmount = ((_bpRate * .010) * _bpWeight);
            }
            else if (_bpWeightUnit.ToUpper() == "TN")
            {
                //lvc.bpAmount = ((Rate * 10) * Qty * Weight);
                bpAmount = ((_bpRate * 10) * _bpWeight);
            }

        }
    }
}
