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

namespace BillGen
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class UpdateClient : Window
    {
        Client _client;
        public UpdateClient()
        {
            InitializeComponent();
        }


        private void btnClientAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            _client = new Client();
            _client.UpdateClient(Convert.ToInt32(CmbBoxClientName.SelectedValue.ToString()), CmbBoxClientName.SelectedItem.ToString(), txtBoxGSTIN.Text, txtBoxAddress.Text,
                CmbBoxCountry.SelectedItem.ToString(), Convert.ToInt32(CmbBoxState.SelectedValue.ToString()),
                CmbBoxCity.SelectedItem.ToString(), Convert.ToInt32(txtBoxPIN.Text), txtBoxEmail.Text,
                txtBoxWebsite.Text, txtBoxMobileNo1.Text, txtBoxMobileNo2.Text, txtBoxMobileNo3.Text);
                        
            Clear();
            this.DialogResult = true;
            this.Close();
        }

        private void btnClientClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            CmbBoxClientName.SelectedIndex = 0;
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
        }
    }
}
