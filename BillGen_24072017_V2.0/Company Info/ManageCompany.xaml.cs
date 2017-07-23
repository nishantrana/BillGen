using BillGen.Class;
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
using BillGen.Client_Info;

namespace BillGen.CompanyInfo
{
    /// <summary>
    /// Interaction logic for ManageCompany.xaml
    /// </summary>
    public partial class ManageCompany : Window
    {
        Company selectedCompany;
        BG_DataHandler _bgDHandler;

        public ManageCompany()
        {
            InitializeComponent();
        }

        public ManageCompany(BG_DataHandler bgDHandler)
        {
            InitializeComponent();

            this._bgDHandler = bgDHandler;
            FillCompanyDetails();
        }

        private void btnCompanyAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCompany _addCompany = new AddCompany();
            _addCompany.ShowDialog();
            if (_addCompany.DialogResult == true)
            {
                FillCompanyDetails();
            }
        }

        private void btnCompanyDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedCompany = (Company)lstViewCompanyNames.SelectedItems[0];

                if (_bgDHandler.CompanyDict.ContainsKey(selectedCompany.cID))
                {
                    _bgDHandler.CompanyDict.Remove(selectedCompany.cID);
                }
                else
                {
                    MessageBox.Show("Company Does Not Exist.");
                }

                selectedCompany.RemoveCompany(selectedCompany.cID);
                FillCompanyDetails();

            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        void FillCompanyDetails()
        {
            lstViewCompanyNames.ItemsSource = Company.SelectAllCompanyInfo().DefaultView;
            lstViewCompanyNames.Items.Refresh();
        }

        private void btnCompanyUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateCompany _updateCompany = new UpdateCompany();
            _updateCompany.ShowDialog();
            if (_updateCompany.DialogResult == true)
            {
                FillCompanyDetails();
            }
        }
    }
}
