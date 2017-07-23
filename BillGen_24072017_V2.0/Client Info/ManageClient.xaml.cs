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

namespace BillGen.Client_Info
{
    /// <summary>
    /// Interaction logic for ManageClient.xaml
    /// </summary>
    public partial class ManageClient : Window
    {
        Client selectedClient;
        BG_DataHandler _bgDHandler;

        public ManageClient()
        {
            InitializeComponent();
        }

        public ManageClient(BG_DataHandler bgDHandler)
        {
            InitializeComponent();

            this._bgDHandler = bgDHandler;
            FillClientDetails();
        }

        private void btnClientAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClient _addClient = new AddClient();
            _addClient.ShowDialog();
            if (_addClient.DialogResult == true)
            {
                FillClientDetails();
                try
                {
                    //GetClientNameList();
                }
                catch { }
            }
        }
        
        private void btnClientDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedClient = (Client)lstViewClientNames.SelectedItems[0];

                if (_bgDHandler.ClientDict.ContainsKey(selectedClient.cID))
                {
                    _bgDHandler.ClientDict.Remove(selectedClient.cID);
                }
                else
                {
                    MessageBox.Show("Client Does Not Exist.");
                }

                selectedClient.RemoveClient();
                FillClientDetails();

                try
                {
                    //GetClientNameList();
                }
                catch { }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        void FillClientDetails()
        {
            lstViewClientNames.ItemsSource = Client.SelectAllClientInfo().DefaultView;
            lstViewClientNames.Items.Refresh();
        }

    }
}
