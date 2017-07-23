using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Diagnostics;
using BillGen.Class;
using BillGen.Client_Info;
using BillGen.CompanyInfo;
using BillGen.BankInfo;
using BillGen.Invoice;

namespace BillGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BG_DataHandler _bgDHandler;
        
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Logger.Location = System.Windows.Forms.Application.StartupPath + "\\Logger\\" + DateTime.Now.ToString("MMM dd yyyy");
                Logger.FileName = Properties.Settings.Default.LogFileName;
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
            try
            {
                ///Create Database Class
                _bgDHandler = new BG_DataHandler();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Main Window Exception");
                Logger.WriteErrorLog(ex);
            }
        }
        
        #region"Interface Button Click Events"

        private void btnManageCompany_Click(object sender, RoutedEventArgs e)
        {
            ManageCompany _manageCompany = new ManageCompany(_bgDHandler);
            _manageCompany.ShowDialog();
        }

        private void btnBankDetail_Click(object sender, RoutedEventArgs e)
        {
            AddBank _addBank = new AddBank(_bgDHandler);
            _addBank.ShowDialog();
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            ManageClient _manageClient = new ManageClient(_bgDHandler);
            _manageClient.ShowDialog();
        }

        private void btnBill_Click(object sender, RoutedEventArgs e)
        {
            AddInvoice _addInvoice = new AddInvoice(_bgDHandler);
            _addInvoice.ShowDialog();
        }

        private void btnViewBill_Click(object sender, RoutedEventArgs e)
        {
            try { System.Diagnostics.Process.Start("explorer.exe", System.Windows.Forms.Application.StartupPath + @"\Bills\PDF\"); }
            catch (Exception ex) { System.Windows.MessageBox.Show("View Bills : " + ex.Message); }
        }
        #endregion

        private void mnExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
