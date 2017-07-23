using BillGen.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BillGen.BankInfo
{
    /// <summary>
    /// Interaction logic for AddBank.xaml
    /// </summary>
    public partial class AddBank : Window
    {
        Bank _bank;
        BG_DataHandler _bgDHandler;

        public AddBank()
        {
            InitializeComponent();
        }

        public AddBank(BG_DataHandler _bgDHandler)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._bgDHandler = _bgDHandler;
            FillCBCountry();
            FillCBCompany();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtBoxBankName.Text))
                {
                    System.Windows.MessageBox.Show("Bank Name Is Empty");
                    txtBoxBankName.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(txtBoxAddress.Text))
                {
                    System.Windows.MessageBox.Show("Address is Empty");
                    txtBoxAddress.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(txtBoxAccount.Text))
                {
                    System.Windows.MessageBox.Show("Account Number is Empty");
                    txtBoxAccount.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(txtBoxIFSC.Text))
                {
                    System.Windows.MessageBox.Show("IFSC is Empty.");
                    txtBoxIFSC.Focus();
                    return;
                }

                if (_bgDHandler.BankDict.ContainsKey(Convert.ToInt32(CmbBoxCompany.SelectedItem)))
                {
                    _bank = _bgDHandler.BankDict[Convert.ToInt32(CmbBoxCompany.SelectedItem)];
                    _bank.bName = txtBoxBankName.Text;
                    _bank.bAddress = txtBoxAddress.Text;
                    _bank.bCountry = Convert.ToString(CmbBoxCountry.SelectedItem);
                    _bank.bCountryCode = Convert.ToInt32(CmbBoxCountry.SelectedValue);
                    _bank.bState = Convert.ToString(CmbBoxState.SelectedItem);
                    _bank.bStateCode = Convert.ToInt32(CmbBoxState.SelectedValue);
                    _bank.bCity = Convert.ToString(CmbBoxCity.SelectedItem);
                    _bank.bCityCode = Convert.ToInt32(CmbBoxCity.SelectedValue);
                    _bank.bAccount = txtBoxAccount.Text;
                    _bank.bIFSC = txtBoxIFSC.Text;
                    _bank.bCompanyID = Convert.ToInt32(CmbBoxCompany.SelectedItem);

                    _bank.AddOrUpdateBankInfo();
                }
                this.DialogResult = true;
                this.Close(); 
                System.Windows.MessageBox.Show("Bank Details Updated Successully");
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }
        
        void FillCBCompany()
        {
            DataTable dtCompany = CommonClass.fillCompany();
            CmbBoxCompany.ItemsSource = dtCompany.DefaultView;
            CmbBoxCompany.DisplayMemberPath = dtCompany.Columns["Name"].ToString();
            CmbBoxCompany.SelectedValuePath = dtCompany.Columns["Id"].ToString();
        }

        void FillCBCountry()
        {
            DataTable dtCountry = CommonClass.fillCountry();
            CmbBoxCountry.ItemsSource = dtCountry.DefaultView;
            CmbBoxCountry.DisplayMemberPath = dtCountry.Columns["Name"].ToString();
            CmbBoxCountry.SelectedValuePath = dtCountry.Columns["Code"].ToString();
        }

        private void CmbBoxCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dtBankDetail = CommonClass.ExecuteGetQuery("Select * from Bank Where CompanyId = " + Convert.ToInt32(CmbBoxCompany.SelectedValue));

            #region "Fill Bank Details"
            txtBoxBankName.Text = dtBankDetail.Columns["Name"].ToString();
            txtBoxAddress.Text = dtBankDetail.Columns["Address"].ToString();

            CmbBoxCountry.SelectedItem = CommonClass.GetCountryNameByCode(Convert.ToInt32(dtBankDetail.Columns["Country"]));
            CmbBoxState.SelectedItem = CommonClass.GetStateNameByCode(Convert.ToInt32(dtBankDetail.Columns["StateCode"]));
            CmbBoxCity.SelectedItem = CommonClass.GetCityNameByCode(Convert.ToInt32(dtBankDetail.Columns["City"]));
            
            txtBoxAccount.Text = dtBankDetail.Columns["Account"].ToString();
            txtBoxIFSC.Text = dtBankDetail.Columns["IFSC"].ToString();
            #endregion
        }

        private void CmbBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dtState = CommonClass.fillState(Convert.ToInt32(CmbBoxCountry.SelectedValue));
            CmbBoxState.ItemsSource = dtState.DefaultView;
            CmbBoxState.DisplayMemberPath = dtState.Columns["Name"].ToString();
            CmbBoxState.SelectedValuePath = dtState.Columns["Code"].ToString();
        }

        private void CmbBoxState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dtCity = CommonClass.fillCity(Convert.ToInt32(CmbBoxState.SelectedValue));
            CmbBoxCity.ItemsSource = dtCity.DefaultView;
            CmbBoxCity.DisplayMemberPath = dtCity.Columns["Name"].ToString();
            CmbBoxCity.SelectedValuePath = dtCity.Columns["Code"].ToString();
        }

        private void CmbBoxCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
