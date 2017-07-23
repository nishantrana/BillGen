using System;
using System.Collections.Generic;
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
using BillGen.Class;
using System.Data;

namespace BillGen.CompanyInfo
{
    /// <summary>
    /// Interaction logic for AddCountry.xaml
    /// </summary>
    public partial class UpdateCompany : Window
    {
        Company _company;
        BG_DataHandler _bgDHandler;

        public UpdateCompany ()
        {
            InitializeComponent();
        }

        public UpdateCompany(BG_DataHandler bgDHandler)
        {
            InitializeComponent();

            this._bgDHandler = bgDHandler;
            FillCBCompany();
            FillCBCountry();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxGSTIN.Text))
            {
                System.Windows.MessageBox.Show("GSTIN No. is Empty");
                txtBoxGSTIN.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxAddress.Text))
            {
                System.Windows.MessageBox.Show("Address is Empty");
                txtBoxAddress.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxPIN.Text))
            {
                System.Windows.MessageBox.Show("PIN No. is Empty.");
                txtBoxPIN.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxEmail.Text))
            {
                System.Windows.MessageBox.Show("Email Id is Empty.");
                txtBoxEmail.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxWebsite.Text))
            {
                System.Windows.MessageBox.Show("Website is Empty.");
                txtBoxWebsite.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxMobileNo1.Text))
            {
                System.Windows.MessageBox.Show("Mobile No. is Empty. Fill Atleast One");
                txtBoxMobileNo1.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxGLogo.Text))
            {
                System.Windows.MessageBox.Show("GLogo is Empty. Fill Atleast One");
                txtBoxGLogo.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxCompanyLogo.Text))
            {
                System.Windows.MessageBox.Show("Company Logo is Empty. Fill Atleast One");
                txtBoxCompanyLogo.Focus();
                return;
            }

            if (_bgDHandler.CompanyDict.ContainsKey(Convert.ToInt32(CmbBoxCompanyName.SelectedValue.ToString())))
            {
                Company _company = _bgDHandler.CompanyDict[Convert.ToInt32(CmbBoxCompanyName.SelectedValue.ToString())];
                _company.cName = Name;
                _company.cGSTIN = txtBoxGSTIN.Text;
                _company.cAddress = txtBoxAddress.Text;
                _company.cCountry = Convert.ToString(CmbBoxCountry.SelectedItem);
                _company.cCountryCode = Convert.ToInt32(CmbBoxCountry.SelectedValue);
                _company.cState = Convert.ToString(CmbBoxState.SelectedItem);
                _company.cStateCode = Convert.ToInt32(CmbBoxState.SelectedValue.ToString());
                _company.cCity = Convert.ToString(CmbBoxCity.SelectedItem);
                _company.cCityCode = Convert.ToInt32(CmbBoxCity.SelectedValue);
                _company.cPIN = Convert.ToInt32(txtBoxPIN.Text);
                _company.cEmail = txtBoxEmail.Text;
                _company.cWebsite = txtBoxWebsite.Text;
                _company.cMobile1 = txtBoxMobileNo1.Text;
                _company.cMobile2 = txtBoxMobileNo2.Text;
                _company.cMobile3 = txtBoxMobileNo3.Text;
                _company.cAdditionalInfo = txtBoxAdditionalInfo.Text;
                _company.cGLogo = txtBoxGLogo.Text;
                _company.cLogoPath = txtBoxCompanyLogo.Text;

                _company.UpdateCompany();
            }
            System.Windows.MessageBox.Show("Company Details Updated Successully");
        }
        
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            CmbBoxCompanyName.SelectedIndex = 0;
            txtBoxGSTIN.Text = "";
            txtBoxAddress.Text = "";
            CmbBoxCountry.SelectedIndex = 0;
            CmbBoxState.SelectedIndex = 0;
            CmbBoxCity.SelectedIndex = 0;
            txtBoxPIN.Text = "";
            txtBoxEmail.Text = "";
            txtBoxWebsite.Text = "";
            txtBoxMobileNo1.Text = "";
            txtBoxMobileNo2.Text = "";
            txtBoxMobileNo3.Text = "";
            txtBoxAdditionalInfo.Text = "";
            txtBoxGLogo.Text = "";
            txtBoxCompanyLogo.Text = "";
        }

        private void CmbBoxCompanyName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        void FillCBCompany()
        {
            DataTable dtCompany = CommonClass.fillCompany();
            CmbBoxCompanyName.ItemsSource = dtCompany.DefaultView;
            CmbBoxCompanyName.DisplayMemberPath = dtCompany.Columns["Name"].ToString();
            CmbBoxCompanyName.SelectedValuePath = dtCompany.Columns["Id"].ToString();
        }

        void FillCBCountry()
        {
            DataTable dtCountry = CommonClass.fillCountry();
            CmbBoxCountry.ItemsSource = dtCountry.DefaultView;
            CmbBoxCountry.DisplayMemberPath = dtCountry.Columns["Name"].ToString();
            CmbBoxCountry.SelectedValuePath = dtCountry.Columns["Code"].ToString();
        }
    }
}
