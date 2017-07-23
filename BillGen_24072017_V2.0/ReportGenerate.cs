using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Web.UI;
using BillGen.Class;

namespace BillGen
{
    public partial class ReportGenerate : System.Windows.Forms.UserControl
    {
        Bill _bill;
        bool PdfComplete;
        string TempLocation;
        string format = "dd-MMM-y"; 
        public ReportGenerate()
        {
            InitializeComponent();
        }

        public ReportGenerate(Bill bill)
        {
            InitializeComponent();

            PdfComplete = false;
            _bill = bill;
        }

        # region //html pages

        public string OriginalBill()
        {
            string BillPageHtml = "";
            StringWriter writer = new StringWriter();
            try
            {
                HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);  //HTML
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "background-color:pink; width:100%; height:100%; margin:0; font-family: Times New Roman");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Body);  //Body

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; height:100%; padding-bottom: 15px; ");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Container Table
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);    //Tr 1
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);    //Td 1

                #region"Main Bill Layout (Outer Table)"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "background-color:pink;width:90%");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Sub Container Table

                #region"Row 1 [TOP HEAD - GSTIN & MOBILE No.]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-top:0px; padding-left:20px; width: 105px; font-size:26px; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("GSTIN No. ");
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("CST No. ");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 of 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-top:0px; font-size:26px; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(": " + _bill.bCompany.cGSTIN);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine(": " + _bill.bCompany.cCST);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding:5px 0px 0px 120px; font-size:18.5px; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                {
                    htmlWriter.WriteLine("Sri Ganeshaya Namaha");
                    htmlWriter.WriteBreak();
                    htmlWriter.WriteBreak();
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cGLogo + "' width='200px' height='110px' />");
                }
                else
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cGLogo + "' width='300px' height='160px' />");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding:43px 5px 0px 125px; vertical-align: top; font-size:26px; text-align:right; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Mob. ");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 of 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding:42px 0px 0px 0px; vertical-align: top; font-size:26px; width:155px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(": " + _bill.bCompany.cMobile1);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine(": " + _bill.bCompany.cMobile2);
                if (!String.IsNullOrEmpty(Convert.ToString(_bill.bCompany.cMobile3)))
                {
                    htmlWriter.WriteBreak();
                    htmlWriter.WriteLine(": " + _bill.bCompany.cMobile3);
                }
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                
                #region"Row 2 [LOGO HEAD]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-align: center; font:15px;padding-top:10px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                {
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cLogoPath + "' width='1200px' height='200px' />");
                }
                else
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cLogoPath + "' width='1200px' height='240px' />");
                
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                                
                #region"Row 5 [MAIN]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 5
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table 1 [CLIENT AND BILL NUMBER DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; border-color:black;margin-top: 15px; font-size:28px;  border-style:solid; border-width:1px");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1

                #region"Row 1"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 65%; border-color:black; border-right-style:solid; border-right-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [Client Detail Entry]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; font-size:27px;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 55px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1 [Client Name]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-decoration:underline");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("M/s  ");
                htmlWriter.WriteLine(_bill.bClient.cName.ToUpper());
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bClient.cAddress);
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-decoration:underline");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("GSTIN No.: ");
                htmlWriter.WriteLine(_bill.bClient.cGSTIN);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 (Newly Added For State Name/Code)"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 20%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [State Name/Code Entry]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; font-size:28px;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 55px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1 [State Name]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("State: ");
                htmlWriter.WriteLine(_bill.bClient.cState);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("Code: ");
                //htmlWriter.WriteLine(_bill.bDeliveryDate.ToShortDateString());
                htmlWriter.WriteLine(Convert.ToString(_bill.bClient.cStateCode));
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.Write("Truck No. : ");
                //htmlWriter.WriteLine(_bill.bTruckNumber);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 20%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [BILL NUMBER ENTRY]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; font-size:28px;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 55px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1 [Bill Number]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("BILL No. : ");
                htmlWriter.WriteLine(_bill.bBillNumber);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("DATE : ");
                //htmlWriter.WriteLine(_bill.bDeliveryDate.ToShortDateString());
                htmlWriter.WriteLine(_bill.bBillDate.ToShortDateString());
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.Write("Truck No. : ");
                //htmlWriter.WriteLine(_bill.bTruckNumber);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1 [Delivery Date]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "border-top-color:black; border-top-style:solid; border-top-width:1px; padding-left:20px; width: 50%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("Delivery Date : ");
                htmlWriter.WriteLine(_bill.bDeliveryDate.ToShortDateString());
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 [Broker Name]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "border-top-color:black; border-top-style:solid; border-top-width:1px; padding-left:20px; width: 50%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("Broker : ");
                htmlWriter.WriteLine(_bill.bBroker);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 40%; padding-left:30px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [PARTICULARS]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:1212px; height:920px; border-color:black; border-top-style: solid;border-top-width: 1px;");
                }
                else
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:1212px; height:880px; border-color:black; border-top-style: solid;border-top-width: 1px;");
                }
                
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1 [HEADING]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-align: center;height:47px; font-size:26px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 7 [HSN Code (Newly Added)] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 120px;max-width: 120px;display: inline-block;border-width:1px; background-color: #F8A2C4; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("HSN Code");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 [TruckNumber] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 120px;max-width: 120px;display: inline-block;border-width:1px; background-color: #F8A2C4; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("TRUCK NO");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2 [QTY] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 145px;max-width: 145px;display: inline-block;border-width:1px; background-color: #F8A2C4; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("QTY");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 [PARTICULARS] width: 300px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 275px;max-width: 275px;border-width:1px;display: inline-block; background-color: #F8A2C4; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("PARTICULARS");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 4 [WEIGHT] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 120px; max-width: 310px;display: inline-block;background-color: #F8A2C4;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("WEIGHT");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 5 [RATE] width: 125px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 125px; max-width: 125px;display: inline-block;background-color: #F8A2C4;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("RATE / QT");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 6 [AMOUNT] width: 125px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 165px; max-width: 165px; background-color: #F8A2C4;border-width:1px; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("AMOUNT");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"ITEMS [PARTICULARS]"

                #region"Row 1 [All PARTICULARS IN FOR LOOP]"
                for (int i = 0; i < _bill.bBillParticulars.Count; i++)
                {
                    if (_bill.bBillParticulars[i].bpQuantity != 0)
                    {
                        if(i == _bill.bBillParticulars.Count - 1)//-2)
                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;text-align: center;height:auto; font-size:26px;");
                        else
                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;text-align: center;height:30px; font-size:26px;");
                        
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                        #region"Cell 7 [HSN Code]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 110px;max-width: 110px;display: inline-block;padding-top: 5px;padding-left:10px; border-width:1px; border-right-style:solid;text-align: left;");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpHSNCode);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 1 [TRUCK No]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 110px;max-width: 110px;display: inline-block;padding-top: 5px;padding-left:10px; border-width:1px; border-right-style:solid;text-align: left;");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpTruckNumber);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 2 [QTY]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 140px;max-width: 140px;display: inline-block;padding-top: 5px;padding-right:25px; border-width:1px; border-right-style:solid;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpQuantity + " " + _bill.bBillParticulars[i].bpQuantityUnit);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 3 [PARTICULARS]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 275px;max-width: 275px;display: inline-block;padding-top: 5px;padding-left:10px; border-width:1px; border-right-style:solid;text-align:left");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpParticulars);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 4 [WEIGHT]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 130px;max-width: 130px;display: inline-block;padding-top: 5px;padding-right:25px; border-width:1px; border-right-style:solid;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpWeight + " " + _bill.bBillParticulars[i].bpWeightUnit);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 5 [RATE]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 125px;max-width: 125px;display: inline-block;padding-top: 5px;padding-right:25px; border-width:1px; border-right-style:solid;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(string.Format("{0:0.00}", _bill.bBillParticulars[i].bpRate));
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 6 [AMOUNT]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 165px;max-width: 165px;display: inline-block;padding-top: 5px;padding-right:25px; text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(string.Format("{0:0.00}", _bill.bBillParticulars[i].bpAmount));
                        htmlWriter.RenderEndTag();
                        #endregion
                        htmlWriter.RenderEndTag();  //End Row 2
                    }
                }
                #endregion
                #endregion
                #region"Row 3 [FOOTER - TOTAL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "font-size:24px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1 [ToTal]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "6");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%;height: 2px; background-color: #F8A2C4;padding-left:10px; border-width:1px; border-top-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("TOTAL");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                #region"Row 3 [FOOTER - TOTAL DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%;height: 55px; font-size:25px; font-weight:bold; padding-left:10px; border-width:1px;");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 40px; font-size:23px; font-weight:bold;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1 [TOTAL TAG]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 120px;padding-top: 5px;padding-left:10px;background-color: #F8A2C4; border-width:1px; border-top-style:solid; border-right-style:solid; border-bottom-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine(_bill.bTotalQuantity + " " + _bill.bBillParticulars.First().bpQuantityUnit);
                htmlWriter.WriteLine("TOTAL");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2 [ToTal Qty]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 110px;padding-left:10px; border-width:1px; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 145px;max-width: 145px;background-color: #F8A2C4;display: inline-block;padding-top: 5px; padding-right:10px; border-width:1px;border-top-style:solid; border-right-style:solid;border-bottom-style:solid;text-align:right");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bTotalQuantity + " UNITS");// + _bill.bBillParticulars.First().bpQuantityUnit);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 [No BANK DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 275px;padding-top: 15px;padding-left:10px;background-color: #F8A2C4; border-width:1px;border-top-style:solid; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("Kotak mahindra Bank Ltd.");
                //htmlWriter.WriteLine(_bill.bBank.bName);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("Naya Bazar, Delhi-06");
                //htmlWriter.WriteLine(_bill.bBank.bAddress);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("A/c No.:4411260022");
                //htmlWriter.WriteLine("A/c No.: " + _bill.bBank.bAccount);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("IFSC Code : KKBK0000210");
                //htmlWriter.WriteLine("IFSC Code : " + _bill.bBank.bIFSC);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 4 [ToTal Weight]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 125px;padding-left:10px; border-width:1px; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 130px;max-width: 130px;display: inline-block;background-color: #F8A2C4;padding-top: 5px;padding-right:10px; border-width:1px;border-top-style:solid; border-right-style:solid;border-bottom-style:solid;text-align:right");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bTotalWeight + " " + _bill.bBillParticulars.First().bpWeightUnit);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 5 [ToTal Head]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 125px;padding-left:10px;background-color: #F8A2C4; border-width:1px;border-top-style:solid; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("TOTAL");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 6 [ToTal Amount]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 165px;max-width: 165px;display: inline-block;background-color: #F8A2C4;padding-top: 5px;padding-right:10px; border-width:1px;border-top-style:solid; border-bottom-style:solid;text-align:right");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Rs " + string.Format("{0:0.00}", _bill.bTotalAmount));
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                #region"Row 3 [FOOTER - TOTAL/BANK DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 130px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1 [BANK DETAILS HEADING]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 110px;font-size: 17px; font-weight:bold; background-color: #F8A2C4;padding-left:10px; border-width:1px; border-right-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("BANK DETAILS");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2 [BANK DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 315px; font-size:27px; padding-top: 15px; padding-left:10px; border-width:1px; border-right-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bBank.bName);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine(_bill.bBank.bAddress);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("A/c No.: " + _bill.bBank.bAccount);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("IFSC Code : " + _bill.bBank.bIFSC);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 [Sauda Date]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "4");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 115px; font-size:27px; padding-top: 15px;padding-left:10px;border-width:1px; border-right-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Sauda Date : ");
                htmlWriter.WriteBreak();
                if (_bill.bSaudaDate.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    htmlWriter.WriteLine("-");
                else
                    htmlWriter.WriteLine(_bill.bSaudaDate.ToShortDateString());
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 4 [Due Date]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "4");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 115px; font-size:27px; padding-top: 15px;padding-left:10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Due Date : ");
                htmlWriter.WriteBreak();
                if (_bill.bDueDate.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    htmlWriter.WriteLine("-");
                else
                    htmlWriter.WriteLine(_bill.bDueDate.ToShortDateString());
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag(); // End Table 1
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 5
                #endregion

                #region"Row 6 [MAIN FOOTER]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1

                #region"Row 1"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 4
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top; padding-left:10px;padding-top:5px; font-size:26px; width:45%");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("1. Subject to Chennai Jurisdiction.");
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("2. Goods once sold will not be taken back.");
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("3. Interest will be charged @ 24%p.a. after 8 days");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;padding-top:5px; font-size:26px; width:11%;padding-left: 10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("E.&.O.E");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3"
                if (_bill.bCompany.cName.ToLower().Contains("agro"))
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;padding-top:5px; font-size:31px; width:44%; text-align: right; padding-right: 10px;");
                }
                else
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;padding-top:5px; font-size:35px; width:44%; text-align: right; padding-right: 10px;");
                }
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("FOR <b>ANKIT IMPEX CHENNAI</b>");
                htmlWriter.WriteLine("FOR <b>"+_bill.bCompany.cName.ToUpper()+"</b>");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 4
                #endregion
                #region"Row 2 EMPTY"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 5
                #region"Cell 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 5
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height:130px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 6
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:10px; font-size: 28px; vertical-align: bottom; padding-left: 10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Signature of Buyer/Broker ...............");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:10px; font-size:28px;vertical-align: bottom;text-align: right;padding-right: 10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Auth. Signatory");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 6
                #endregion
                #region"Row 4"
                ////htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-top: 32px;");
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 4
                //#region"Cell 1"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top; padding-left:10px; padding-top:25px; font-size:13px;");
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                ////htmlWriter.WriteLine("1. Subject to Chennai Jurisdiction.");
                ////htmlWriter.WriteBreak();
                ////htmlWriter.WriteLine("2. Goods Once sold will not be taken back.");
                ////htmlWriter.WriteBreak();
                ////htmlWriter.WriteLine("3. Interest will be charged @ 24%p.a. after 8 days");
                ////htmlWriter.WriteBreak();
                //htmlWriter.RenderEndTag();
                //#endregion
                //htmlWriter.RenderEndTag();  //End Row 4
                #endregion
                #region"Row 5 EMPTY"
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 7
                //#region"Cell 1"
                ////htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height:5px");
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.RenderEndTag();
                //#endregion
                //htmlWriter.RenderEndTag();  //End Row 7
                #endregion

                htmlWriter.RenderEndTag(); // End Table 1
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion

                htmlWriter.RenderEndTag();  // End Sub Container Table
                #endregion
                htmlWriter.RenderEndTag();  // End Td 1
                htmlWriter.RenderEndTag();  // End Tr 1
                htmlWriter.RenderEndTag();  // End Container Table
                htmlWriter.RenderEndTag();  // Body
                htmlWriter.RenderEndTag();  // End HTML
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            BillPageHtml = writer.ToString();
            writer.Flush();
            writer.Close();
            return BillPageHtml;
        }

        public string DuplicateBill()
        {
            string BillPageHtml = "";
            StringWriter writer = new StringWriter();
            try
            {
                HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);  //HTML
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "background-color:white; width:100%; height:100%; margin:0; font-family: Times New Roman");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Body);  //Body

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; height:100%; padding-bottom: 15px; ");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Container Table
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);    //Tr 1
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);    //Td 1

                #region"Main Bill Layout (Outer Table)"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "background-color:white;width:90%");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Sub Container Table

                #region"Row 1 [TOP HEAD - TIN & MOBILE No.]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-top:0px; padding-left:20px; width: 100px; font-size:26px; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("GSTIN No. ");
                htmlWriter.WriteBreak();
                
                //if (String.IsNullOrWhiteSpace( _bill.bCompany.cCST))
                //    htmlWriter.WriteLine("");
                //else
                //    htmlWriter.WriteLine("CST No. ");

                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 of 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-top:0px; font-size:26px; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(": " + _bill.bCompany.cGSTIN);
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding:5px 0px 0px 120px; font-size:18.5px; ");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                {
                    htmlWriter.WriteLine("Sri Ganeshaya Namaha");
                    htmlWriter.WriteBreak();
                    htmlWriter.WriteBreak();
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cGLogo + "' width= '200px' height= '110px' />");
                }
                else
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cGLogo + "' width= '300px' height= '160px' />");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding:43px 5px 0px 125px; vertical-align: top; font-size:26px; text-align:right;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Mob. ");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 of 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding:42px 0px 0px 0px; vertical-align: top; font-size:26px; width:155px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(": " + _bill.bCompany.cMobile1);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine(": " + _bill.bCompany.cMobile2);
                if (!String.IsNullOrEmpty(Convert.ToString(_bill.bCompany.cMobile3)))
                {
                    htmlWriter.WriteBreak();
                    htmlWriter.WriteLine(": " + _bill.bCompany.cMobile3);
                }
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion

                #region"Row 2 [LOGO HEAD]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-align: center; font:15px;padding-top:10px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                {
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cLogoPath + "' width='1200px' height='200px' />");
                }
                else
                {
                    htmlWriter.WriteLine("<img src='" + _bill.bCompany.cLogoPath + "' width='1200px' height='240px' />");
                } 
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion

                #region"Row 5 [MAIN]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 5
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table 1 [CLIENT AND BILL NUMBER DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; border-color:black;margin-top: 15px; font-size:28px; border-style:solid; border-width:1px");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1

                #region"Row 1"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 65%; border-color:black; border-right-style:solid; border-right-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [Client Detail Entry]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; font-size:27px;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 55px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1 [Client Name]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-decoration:underline");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("M/s  ");
                htmlWriter.WriteLine(_bill.bClient.cName.ToUpper());
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bClient.cAddress);
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-decoration:underline");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("TIN/CST No.: ");
                htmlWriter.WriteLine(_bill.bClient.cGSTIN);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 (Newly Added For State Name/Code)"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 20%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [State Name/Code Entry]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; font-size:28px;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 55px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1 [State Name]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("State: ");
                htmlWriter.WriteLine(_bill.bClient.cState);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("Code: ");
                //htmlWriter.WriteLine(_bill.bDeliveryDate.ToShortDateString());
                htmlWriter.WriteLine(Convert.ToString(_bill.bClient.cStateCode));
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.Write("Truck No. : ");
                //htmlWriter.WriteLine(_bill.bTruckNumber);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 35%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [BILL NUMBER ENTRY]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:100%; font-size:28px;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1
                #region"Row 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 55px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 1 [Bill Number]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("BILL No. : ");
                htmlWriter.WriteLine(_bill.bBillNumber);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px; font-size:34px;border-color:black; border-bottom-style:solid; border-bottom-width:1px");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("DATE : ");
                //htmlWriter.WriteLine(_bill.bDeliveryDate.ToShortDateString());
                //htmlWriter.WriteLine(_bill.bBillDate.ToShortDateString());
                htmlWriter.WriteLine(_bill.bBillDate.ToString(format));
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:20px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.Write("Truck No. : ");
                //htmlWriter.WriteLine(_bill.bTruckNumber);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"Row 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 50px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 2
                #region"Cell 1 [Delivery Date]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "border-top-color:black; border-top-style:solid; border-top-width:1px; padding-left:20px; width: 50%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("Delivery Date : ");
                //htmlWriter.WriteLine(_bill.bDeliveryDate.ToShortDateString());
                htmlWriter.WriteLine(_bill.bDeliveryDate.ToString(format));
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 [Broker Name]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "border-top-color:black; border-top-style:solid; border-top-width:1px; padding-left:20px; width: 50%;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("Broker : ");
                htmlWriter.WriteLine(_bill.bBroker);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 2
                #endregion
                #region"Row 3"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 40%; padding-left:30px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table [PARTICULARS]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:1212px;height:920px; border-color:black; border-top-style: solid;border-top-width: 1px;");
                }
                else
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width:1212px;height:880px; border-color:black; border-top-style: solid;border-top-width: 1px;");
                }
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1

                #region"Row [DUPLICATE STAMP]"
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                #region"Cell 1 [DUPLICATE STAMP CELL]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 1212px;font-size: 85px;font-weight: bold;opacity: 0.2;position: absolute;text-align: center;margin: 250 auto;transform: rotate(-30deg); ");
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div); //Container DIV FOR DUPLICATE LABEL
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Label); //Container Label FOR DUPLICATE LABEL
                //htmlWriter.WriteLine("DUPLICATE");
                //htmlWriter.RenderEndTag();  // End Label
                //htmlWriter.RenderEndTag();  // End DIV
                // htmlWriter.RenderEndTag();
                #endregion
                //htmlWriter.RenderEndTag();  //End Row 1
                #endregion

                #region"Row 1 [HEADING]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "text-align: center;height:47px; font-size:26px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                #region"Cell 7 [HSN Code (Newly Added)] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 120px;max-width: 120px;display: inline-block;border-width:1px; background-color: #F8A2C4; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("HSN Code");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 [TRUCK NO] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 120px;max-width: 120px;display: inline-block; background-color: #E0D9E0;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("TRUCK NO");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2 [QTY] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 145px;max-width: 145px;display: inline-block; background-color: #E0D9E0;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("QTY");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 [PARTICULARS] width: 275px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 275px;max-width: 275px;border-width:1px;display: inline-block; background-color: #E0D9E0;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("PARTICULARS");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 4 [WEIGHT] width: 120px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 120px; max-width: 310px;display: inline-block;background-color: #E0D9E0;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("WEIGHT");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 5 [RATE] width: 125px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 125px; max-width: 125px;display: inline-block;background-color: #E0D9E0;border-width:1px; border-right-style:solid; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("RATE / QT");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 6 [AMOUNT] width: 125px"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 165px; max-width: 165px; background-color: #E0D9E0;border-width:1px; border-bottom-style: solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("AMOUNT");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 1
                #endregion
                #region"ITEMS [PARTICULARS]"

                #region"Row 1 [All PARTICULARS IN FOR LOOP]"
                for (int i = 0; i < _bill.bBillParticulars.Count; i++)
                {
                    if (_bill.bBillParticulars[i].bpQuantity != 0)
                    {
                        if (i == _bill.bBillParticulars.Count - 1)
                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;text-align: center;height:auto; font-size:26px;");
                        else
                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;text-align: center;height:30px; font-size:26px;");
                        
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 1
                        #region"Cell 7 [HSN Code]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 110px;max-width: 110px;display: inline-block;padding-top: 5px;padding-left:10px; border-width:1px; border-right-style:solid;text-align: left;");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpHSNCode);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 1 [TRUCK NO.]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 110px;max-width: 110px;display: inline-block;padding-top: 5px;padding-left:10px; border-width:1px; border-right-style:solid;text-align: left;");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpTruckNumber);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 2 [QTY]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 140px;max-width: 140px;display: inline-block;padding-top: 5px;padding-right:25px; border-width:1px; border-right-style:solid;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpQuantity + " " + _bill.bBillParticulars[i].bpQuantityUnit);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 3 [PARTICULARS]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 275px;max-width: 275px;display: inline-block;padding-top: 5px;padding-left:10px; border-width:1px; border-right-style:solid;text-align:left");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpParticulars);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 4 [WEIGHT]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 130px;max-width: 130px;display: inline-block;padding-top: 5px;padding-right:25px; border-width:1px; border-right-style:solid;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(_bill.bBillParticulars[i].bpWeight + " " + _bill.bBillParticulars[i].bpWeightUnit);
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 5 [RATE]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 125px;max-width: 125px;display: inline-block;padding-top: 5px;padding-right:25px; border-width:1px; border-right-style:solid;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(string.Format("{0:0.00}", _bill.bBillParticulars[i].bpRate));
                        htmlWriter.RenderEndTag();
                        #endregion
                        #region"Cell 6 [AMOUNT]"
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 165px;max-width: 165px;display: inline-block;padding-top: 5px;padding-right:25px;text-align:right");
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.WriteLine(string.Format("{0:0.00}", _bill.bBillParticulars[i].bpAmount));
                        htmlWriter.RenderEndTag();
                        #endregion
                        htmlWriter.RenderEndTag();  //End Row 2
                    }
                }
                #endregion
                #endregion
                #region"Row 3 [FOOTER - TOTAL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "font-size:24px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1 [ToTal head]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "6");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%;height: 2px; background-color: #E0D9E0;padding-left:10px; border-width:1px; border-top-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("TOTAL");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                #region"Row 3 [FOOTER - TOTAL DETAILS]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%;font-size:25px; font-weight:bold;height: 55px; background-color: #E0D9E0;padding-left:10px; border-width:1px; ");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 40px; font-size:23px; font-weight:bold;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1 [Truck Column Empty]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 120px;padding-top: 5px;padding-left:10px; border-width:1px; border-top-style:solid; border-right-style:solid; border-bottom-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("TOTAL");
                //htmlWriter.WriteLine(_bill.bTotalQuantity + " " + _bill.bBillParticulars.First().bpQuantityUnit);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 1 [ToTal Qty]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 110px;padding-left:10px; border-width:1px; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 145px;max-width: 145px;display: inline-block;padding-top: 5px;padding-right:10px; border-width:1px; border-top-style:solid; border-right-style:solid;border-bottom-style:solid;text-align:right");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bTotalQuantity + " UNITS");// + _bill.bBillParticulars.First().bpQuantityUnit
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2 [BANK DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 275px;padding-top: 15px;padding-left:10px; border-width:1px; border-top-style:solid; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("Kotak mahindra Bank Ltd.");
                //htmlWriter.WriteLine(_bill.bBank.bName);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("Naya Bazar, Delhi-06");
                //htmlWriter.WriteLine(_bill.bBank.bAddress);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("A/c No.:4411260022");
                //htmlWriter.WriteLine("A/c No.: " + _bill.bBank.bAccount);
                //htmlWriter.WriteBreak();
                //htmlWriter.WriteLine("IFSC Code : KKBK0000210");
                //htmlWriter.WriteLine("IFSC Code : " + _bill.bBank.bIFSC);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 [ToTal Weight]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 125px;padding-left:10px; border-width:1px; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 130px;max-width: 130px;display: inline-block;padding-top: 5px;padding-right:10px; border-width:1px; border-top-style:solid; border-right-style:solid;border-bottom-style:solid;text-align:right");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bTotalWeight + " " + _bill.bBillParticulars.First().bpWeightUnit);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 4 [ToTal Head]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 125px;padding-left:10px; border-width:1px; border-top-style:solid; border-right-style:solid;border-bottom-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("TOTAL");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 5 [ToTal Amount]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 125px;padding-right:25px; border-width:1px; border-bottom-style:solid;text-align:right");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 165px;max-width: 165px;display: inline-block;padding-top: 5px;padding-right:10px; border-width:1px; border-top-style:solid; border-bottom-style:solid;text-align:right");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Rs " + string.Format("{0:0.00}", _bill.bTotalAmount));
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                #region"Row 3 [FOOTER - TOTAL/BANK DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height: 130px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1 [BANK DETAILS HEADING]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 110px;font-size: 17px; font-weight:bold; background-color: #E0D9E0;padding-left:10px; border-width:1px; border-right-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("BANK DETAILS");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2 [BANK DETAIL]"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 315px; font-size:27px; padding-top: 15px;padding-left:10px;border-width:1px; border-right-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine(_bill.bBank.bName);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine(_bill.bBank.bAddress);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("A/c No.: " + _bill.bBank.bAccount);
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("IFSC Code : " + _bill.bBank.bIFSC);
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3 [Sauda Date]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "4");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 115px;padding-top: 15px; font-size:27px; padding-left:10px;border-width:1px; border-right-style:solid;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Sauda Date : ");
                htmlWriter.WriteBreak();
                if (_bill.bSaudaDate.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    htmlWriter.WriteLine("-");
                else
                    htmlWriter.WriteLine(_bill.bSaudaDate.ToString(format));
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 4 [Due Date]"
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "4");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 115px; font-size:27px; padding-top: 15px;padding-left:10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Due Date : ");
                htmlWriter.WriteBreak();
                if (_bill.bDueDate.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    htmlWriter.WriteLine("-");
                else
                    htmlWriter.WriteLine(_bill.bDueDate.ToString(format));
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion

                htmlWriter.RenderEndTag(); // End Table 1
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 5
                #endregion

                #region"Row 6 [MAIN FOOTER]"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 3
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "5");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                #region"Table 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table); //Table 1

                #region"Row 1"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 4
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top; padding-left:10px; padding-top:5px; font-size:26px; width:45%");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);

                if (_bill.bCompany.cName.ToLower().Contains("chennai"))
                    htmlWriter.WriteLine("1. Subject to chennai jurisdiction.");
                else
                    htmlWriter.WriteLine("1. Subject to delhi jurisdiction.");
                
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("2. Goods once sold will not be taken back.");
                htmlWriter.WriteBreak();
                htmlWriter.WriteLine("3. Interest will be charged @ 24%p.a. after 8 days");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;padding-top:5px; font-size:26px; width:11%;padding-left: 10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("E.&.O.E");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 3"
                if (_bill.bCompany.cName.ToLower().Contains("agro"))
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;padding-top:5px; font-size:31px; width:44%; text-align: right; padding-right: 10px;");
                }
                else
                {
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "vertical-align: top;padding-top:5px; font-size:35px; width:44%; text-align: right; padding-right: 10px;");
                }
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.WriteLine("FOR <b>ANKIT IMPEX CHENNAI</b>");
                htmlWriter.WriteLine("FOR <b>" + _bill.bCompany.cName.ToUpper() + "</b>");
                htmlWriter.WriteBreak();
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 4
                #endregion
                #region"Row 2 EMPTY"
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 5
                #region"Cell 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 5
                #endregion
                #region"Row 3"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height:130px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 6
                #region"Cell 1"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:10px; font-size: 28px; vertical-align: bottom; padding-left: 10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Signature of Buyer/Broker ...............");
                htmlWriter.RenderEndTag();
                #endregion
                #region"Cell 2"
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "padding-left:10px; font-size:28px; vertical-align: bottom; text-align: right; padding-right: 10px;");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.WriteLine("Auth. Signatory");
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 6
                #endregion                
                #region"Row 4 EMPTY"
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr); //Row 7
                //#region"Cell 1"
                ////htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
                //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Style, "height:5px");
                //htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                //htmlWriter.RenderEndTag();
                //#endregion
                //htmlWriter.RenderEndTag();  //End Row 7
                #endregion

                htmlWriter.RenderEndTag(); // End Table 1
                #endregion
                htmlWriter.RenderEndTag();
                #endregion
                htmlWriter.RenderEndTag();  //End Row 3
                #endregion

                htmlWriter.RenderEndTag();  // End Sub Container Table
                #endregion
                htmlWriter.RenderEndTag();  // End Td 1
                htmlWriter.RenderEndTag();  // End Tr 1
                htmlWriter.RenderEndTag();  // End Container Table
                htmlWriter.RenderEndTag();  // Body
                htmlWriter.RenderEndTag();  // End HTML
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            BillPageHtml = writer.ToString();
            writer.Flush();
            writer.Close();
            return BillPageHtml;
        }

        #endregion

        public void PdfReport(string Dispath)
        {
            string str = Dispath;
            System.Threading.Thread.Sleep(1500);
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(savePDF));
            th.Start(str);
        }

        string path = null;
        void savePDF(object obj)
        {
            try
            {
                string[] str = obj.ToString().Split('|');
                string tempLoc = "";
                string arguments = "";
                try
                {
                    if (!Directory.Exists(Application.StartupPath + "\\Bills\\Temp"))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\Bills\\Temp");
                    }

                    //tempLoc = Path.GetTempPath() + "BGTemp" + _bill.bBillNumber + DateTime.Now.Ticks;
                    tempLoc = Application.StartupPath + "\\Bills\\Temp\\BGTemp" + _bill.bBillNumber + DateTime.Now.Ticks;
                    
                    if (Directory.Exists(tempLoc))
                    {
                        //tempLoc = Path.GetTempPath() + "BGTemp" + _bill.bBillNumber + DateTime.Now.Ticks + 5;
                        tempLoc = Application.StartupPath + "\\Bills\\Temp\\BGTemp" + _bill.bBillNumber + DateTime.Now.Ticks + 5;
                        Directory.CreateDirectory(tempLoc);
                    }
                    else
                        Directory.CreateDirectory(tempLoc);


                    //arguments = "--footer-center [page] --margin-bottom 7mm";

                    #region ReportsPages
                    
                    ///Comment On Clients requirement
                    
                    //string OrgBill = OriginalBill();
                    //File.WriteAllText(tempLoc + "\\Bill.html", OrgBill.ToString());
                    //arguments += tempLoc + "\\" + "Bill.html ";

                    string DupBill = DuplicateBill();
                    File.WriteAllText(tempLoc + "\\DupBill.html", DupBill.ToString());
                    arguments += tempLoc + "\\" + "DupBill.html ";
                    
                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
                try
                {

                    string[] s = str[0].Split(',');
                    path = s[0] + "\\" + s[1];// +"\\" + s[2];
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = Application.StartupPath + "\\wkhtmltopdf\\wkhtmltopdf.exe";
                    p.StartInfo.Arguments = arguments +s[1];
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.EnableRaisingEvents = true;
                    p.StartInfo.WorkingDirectory = s[0];// +"\\" + s[1];
                    PdfComplete = p.Start();
                    TempLocation = tempLoc;

                    System.Threading.Thread.Sleep(1000);

                    //Send Mail Process
                    //if (PdfComplete)
                    //{

                    //    SendMail(path, s[1], s[2]);

                    //}

                    p.Exited += new EventHandler(p_Exited);
                    
                    MessageBox.Show("Bill Number " + s[1].ToLower().Trim('p','d','f'), "Bill Generated Successfully.");
                    //_bill.ResetBill();

                    try
                    {
                        if (Directory.Exists(TempLocation))
                        {
                            Directory.Delete(TempLocation, true);
                        }
                        TempLocation = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(ex);
                        MessageBox.Show(ex.Message + " || " + ex.StackTrace + " || " + arguments + str[0], "Bill Generation Failed.");
                    }

                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message + " || " + ex.StackTrace + " || " + arguments + str[0], "Bill Generation Failed.");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                MessageBox.Show(ex.Message + " || " + ex.StackTrace , "Bill Generation Failed.");
            }
        }

        //when save as pdf process exits   
        void p_Exited(object sender, EventArgs e)
        {
            try
            {
                //System.Threading.Thread.Sleep(1000);
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                MessageBox.Show(ex.Message + " || " + ex.StackTrace, "PDF Not Open.");
            }    
        }
    }
}
