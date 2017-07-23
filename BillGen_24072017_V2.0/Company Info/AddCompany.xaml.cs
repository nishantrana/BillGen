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
using Microsoft.Win32;

namespace BillGen.CompanyInfo
{
    /// <summary>
    /// Interaction logic for AddCountry.xaml
    /// </summary>
    public partial class AddCompany : Window
    {
        Company _company;

        public AddCompany()
        {
            InitializeComponent();
            FillCBCountry();
            txtBoxCompanyName.Focus();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxCompanyName.Text))
            {
                System.Windows.MessageBox.Show("Company Name Is Empty");
                txtBoxCompanyName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxGSTIN.Text))
            {
                System.Windows.MessageBox.Show("GSTIN Is Empty");
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
                System.Windows.MessageBox.Show("PIN is Empty.");
                txtBoxPIN.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxEmail.Text))
            {
                System.Windows.MessageBox.Show("Email is Empty.");
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
                System.Windows.MessageBox.Show("Mobile is Empty. Atleast Fill One");
                txtBoxMobileNo1.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxAdditionalInfo.Text))
            {
                System.Windows.MessageBox.Show("Additional Info is Empty.");
                txtBoxAdditionalInfo.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxGLogo.Text))
            {
                System.Windows.MessageBox.Show("GLogo Info is Empty.");
                txtBoxGLogo.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBoxCompanyLogo.Text))
            {
                System.Windows.MessageBox.Show("Company Logo Info is Empty.");
                txtBoxCompanyLogo.Focus();
                return;
            } 

            _company = new Company();
            _company.AddCompany(txtBoxCompanyName.Text, txtBoxGSTIN.Text, txtBoxAddress.Text,
              Convert.ToInt32(CmbBoxCountry.SelectedValue), Convert.ToInt32(CmbBoxState.SelectedValue.ToString()),
              Convert.ToInt32(CmbBoxCity.SelectedValue), Convert.ToInt32(txtBoxPIN.Text), txtBoxEmail.Text,
              txtBoxWebsite.Text, txtBoxMobileNo1.Text, txtBoxMobileNo2.Text, txtBoxMobileNo3.Text,
              txtBoxAdditionalInfo.Text,txtBoxGLogo.Text,txtBoxCompanyLogo.Text);

            this.DialogResult = true;
            this.Close(); 
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            txtBoxCompanyName.Text = "";
            txtBoxGSTIN.Text = "";
            txtBoxAddress.Text = "";
            CmbBoxCountry.SelectedIndex = 0;
            CmbBoxState.SelectedIndex = 0;
            CmbBoxCity.SelectedIndex = 0;
            //txtBoxCity.Text = "";
            txtBoxPIN.Text = "";
            txtBoxEmail.Text = "";
            txtBoxWebsite.Text = "";
            txtBoxMobileNo1.Text = "";
            txtBoxMobileNo2.Text = "";
            txtBoxMobileNo3.Text = "";
            txtBoxAdditionalInfo.Text = "";
            txtBoxGLogo.Text = "";
            txtBoxCompanyLogo.Text = "";
            txtBoxCompanyName.Focus();
        }
        
        void FillCBCountry()
        {
            DataTable dtCountry = CommonClass.fillCountry();
            CmbBoxCountry.ItemsSource = dtCountry.DefaultView;
            CmbBoxCountry.DisplayMemberPath = dtCountry.Columns["Name"].ToString();
            CmbBoxCountry.SelectedValuePath = dtCountry.Columns["Code"].ToString();
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

        private void BtnUploadGLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                txtBoxGLogo.Text = openFileDialog.FileName;
            }
        }

        private void BtnUploadCompanyLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                txtBoxCompanyLogo.Text = openFileDialog.FileName;
            }
        }

    }
}
