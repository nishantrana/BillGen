using BillGen.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BillGen.Invoice
{
    /// <summary>
    /// Interaction logic for AddInvoice.xaml
    /// </summary>
    public partial class AddInvoice : Window
    {
        private static bool isModifyBill = false;
        private bool dataChanged = false;
        private bool stopRefreshControls = false;

        BG_DataHandler _bgDHandler;
        AddClient _addClient;
        List<string> _clientNameList; 

        Company _selectedCompany;
        Client _selectedClient;
        Bank _selectedBank;
        Bill _selectedBill;

        public AddInvoice()
        {
            InitializeComponent();
        }

        public AddInvoice(BG_DataHandler bgDHandler)
        {
            InitializeComponent();

            this._bgDHandler = bgDHandler;
            GetClientNameList();
            FillCompany();
            FillWeightUnit();
            Win_Loaded();
        }

        #region "Methods"
        void FillCompany()
        {
            try
            {
                DataTable dtCompany = CommonClass.fillCompany();
                cmbBoxNBCompany.ItemsSource = dtCompany.DefaultView;
                cmbBoxNBCompany.DisplayMemberPath = dtCompany.Columns["Name"].ToString();
                cmbBoxNBCompany.SelectedValuePath = dtCompany.Columns["Id"].ToString();

                cmbBoxNBCompany.Items.Add("Select");
                cmbBoxNBCompany.SelectedIndex = 0;
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        void FillWeightUnit()
        {
            //cbNBWUnit.Items.Add("Select");

            foreach (string wu in _bgDHandler.WeightUnitList)
            {
                cbNBWUnit.Items.Add(wu.ToUpper());
            }
            cbNBWUnit.SelectedIndex = 0;
        }

        /// <summary>
        /// Window Loaded event handler
        /// Loads data into ListView, and selecta a row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Win_Loaded()
        {
            ShowData();

            if (lstVwNewBill.Items.Count == 0)
            {
                AddRow();
            }
            else
            {
                lstVwNewBill.SelectedIndex = 0;
            }
            setDataChanged(false);
        }

        /// <summary>
        /// Shows(Loads) data into the ListView
        /// </summary>
        private void ShowData()
        {
            lstVwNewBill.Items.Clear();
        }

        /// <summary>
        /// Sets the window into a DataChanged status.
        /// </summary>
        /// <param name="value"></param>
        private void setDataChanged(bool value)
        {
            dataChanged = value;
            //saveButton.IsEnabled = value;
        }
        
        /// <summary>
        /// Saves the current value to the Tag property of textbox.
        /// </summary>
        /// <param name="sender"></param>
        private void StoreCurrentValue(object sender)
        {
            System.Windows.Controls.TextBox myText = (System.Windows.Controls.TextBox)sender;
            myText.Tag = myText.Text;
        }

        /// <summary>
        /// Restores old value, saved in Tag property, to textbox.
        /// </summary>
        /// <param name="sender"></param>
        private void RestoreOldValue(object sender)
        {
            System.Windows.Controls.TextBox myText = (System.Windows.Controls.TextBox)sender;

            if (myText.Text != myText.Tag.ToString())
            {
                myText.Text = myText.Tag.ToString();
                myText.SelectAll();
            }
        }
        
        void GetClientNameList()
        {
            _clientNameList = _bgDHandler.ClientDict.Select(n => n.Value.cName.ToUpper()).ToList();
        }

        int GetClientIdByName(string name)
        {
            return Convert.ToInt32(_bgDHandler.ClientDict.Where(n => n.Value.cName.ToUpper() == name.ToUpper()).Select(x => x.Key).ToList());
        }
        #endregion

        private void cmbBoxNBCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedCompany = new Company();
                _selectedCompany.SetCompanyVariables(_selectedCompany.SelectCompanyInfo(Convert.ToInt32(cmbBoxNBCompany.SelectedValue)));

                _selectedBank = new Bank();
                _selectedBank.SetBankVariables(_selectedBank.SelectBankOfCompany(Convert.ToInt32(_selectedCompany.cID)));

                hdrNewBill.Content = "GENERATE NEW BILL FOR " + _selectedCompany.cName.ToUpper();
                txtBoxNBClientName.Focus();
            }
            catch { }
        }

        #region TextBox-TextChanged-txtBoxNBClientName
        private void txtBoxNBClientName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string typedString = txtBoxNBClientName.Text;
                List<string> autoList = new List<string>();
                autoList.Clear();

                if (string.IsNullOrEmpty(txtBoxNBClientName.Text))
                {
                    lbSuggestion.Visibility = Visibility.Collapsed;
                    lbSuggestion.ItemsSource = null;
                    return;
                }

                if (_clientNameList.Count == 0)
                {
                    System.Windows.MessageBox.Show("Client Database IS Empty. Please Add Clients Detail First");
                    txtBoxNBClientName.Text = "";
                    txtBoxNBClientName.Focus();
                    return;
                }

                foreach (string item in _clientNameList)
                {
                    if (!string.IsNullOrEmpty(txtBoxNBClientName.Text))
                    {
                        if (item.StartsWith(typedString.ToUpper()))
                        {
                            autoList.Add(item);
                        }
                    }
                }

                if (autoList.Count > 0)
                {
                    lbSuggestion.ItemsSource = autoList;
                    lbSuggestion.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }
        #endregion

        private void txtBoxNBBillNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxNBBillNumber.Text))
            {
                ClearBillForm(); return;
            }

            Bill tempBill = null;
            if (_bgDHandler.BillDict.ContainsKey(_selectedCompany.cID))
                if (_bgDHandler.BillDict[_selectedCompany.cID].ContainsKey(Convert.ToDouble(txtBoxNBBillNumber.Text)))
                    tempBill = _bgDHandler.BillDict[_selectedCompany.cID][Convert.ToDouble(txtBoxNBBillNumber.Text)];

            if (tempBill != null)
            {
                try
                {
                    //_selectedBill = tempBill;
                    _selectedBill = new Bill();
                    _selectedBill = tempBill;
                    _selectedClient = tempBill.bClient;
                    cmbBoxNBCompany.Text = tempBill.bCompany.cName.ToUpper();
                    txtBoxNBClientName.Text = tempBill.bClient.cName.ToUpper();
                    lbSuggestion.Visibility = Visibility.Collapsed;
                    txtBoxNBBillNumber.Text = tempBill.bBillNumber.ToString();
                    dpNBDeliveryDate.Text = tempBill.bDeliveryDate.ToString();
                    dpNBBillDate.Text = tempBill.bBillDate.ToString();
                    dpNBSaudaDate.Text = tempBill.bSaudaDate.ToString();
                    dpNBDueDate.Text = tempBill.bDueDate.ToString();
                    txtBoxNBBroker.Text = tempBill.bBroker;
                    lblBrokerSuggestion.Visibility = Visibility.Collapsed;
                    lstVwNewBill.Items.Clear();

                    foreach (BillParticular bp in tempBill.bBillParticulars)
                        lstVwNewBill.Items.Add(bp);

                    AddRow();
                    lstVwNewBill.Items.Refresh();
                    setDataChanged(true);

                    btnNBGenerateModifyBill.IsEnabled = true;
                    btnNBGenerateNewBill.IsEnabled = false;
                }
                catch (Exception ex) { Logger.WriteErrorLog(ex); }
            }
            else
            {
                ClearBillForm();

                btnNBGenerateModifyBill.IsEnabled = false;
                btnNBGenerateNewBill.IsEnabled = true;
            }
        }

        private void cbNBWUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string wUnit = cbNBWUnit.SelectedItem.ToString();
        }

        private void lblQUnitSuggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lblQUnitSuggestion.SelectedIndex == -1) return;

                if (lblQUnitSuggestion.ItemsSource != null)
                {
                    txtBoxNBQtyUnit.TextChanged -= new TextChangedEventHandler(txtBoxNBQtyUnit_TextChanged);
                    if (lblQUnitSuggestion.SelectedIndex != -1)
                    {
                        txtBoxNBQtyUnit.Text = lblQUnitSuggestion.SelectedItem.ToString();
                    }
                    lblQUnitSuggestion.SelectedIndex = -1;
                    lblQUnitSuggestion.Visibility = Visibility.Collapsed;
                    txtBoxNBQtyUnit.TextChanged += new TextChangedEventHandler(txtBoxNBQtyUnit_TextChanged);
                }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        #region TextBox-TextChanged-txtBoxNBQtyUnit_TextChanged
        private void txtBoxNBQtyUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = txtBoxNBQtyUnit.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in _bgDHandler.QtyUnitList)
            {
                if (!string.IsNullOrEmpty(txtBoxNBQtyUnit.Text))
                {
                    if (item.StartsWith(typedString.ToUpper()))
                    {
                        autoList.Add(item);
                    }
                }
            }

            if (autoList.Count > 0)
            {
                lblQUnitSuggestion.ItemsSource = autoList;
                lblQUnitSuggestion.Visibility = Visibility.Visible;
            }
            else if (txtBoxNBQtyUnit.Text.Equals(""))
            {
                lblQUnitSuggestion.Visibility = Visibility.Collapsed;
                lblQUnitSuggestion.ItemsSource = null;
            }
            else
            {
                lblQUnitSuggestion.Visibility = Visibility.Collapsed;
                lblQUnitSuggestion.ItemsSource = null;
            }
        }

        private void txtBoxNBTruckNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string TruckNumber = txtBoxNBTruckNumber.Text.ToString();
                double Qty = 0;
                string QtyUnit = txtBoxNBQtyUnit.Text.ToString();
                string Particular = txtBoxNBParticulars.Text;
                double Weight = 0;
                if (cbNBWUnit.SelectedIndex < 0) cbNBWUnit.SelectedIndex = 0;
                string WeightUnit = cbNBWUnit.SelectedItem.ToString();
                double Rate = 0;

                try { if (txtBoxNBQty.Text == "")Qty = 0; else Qty = Convert.ToDouble(txtBoxNBQty.Text); }
                catch { Qty = 0; }
                try { if (txtBoxNBWeight.Text == "")Weight = 0; else Weight = Convert.ToDouble(txtBoxNBWeight.Text); }
                catch { Weight = 0; }
                try { if (txtBoxNBRate.Text == "")Rate = 0; else Rate = Convert.ToDouble(txtBoxNBRate.Text); }
                catch { Rate = 0; }
                RefreshListView(TruckNumber, Qty, QtyUnit, Particular, Weight, WeightUnit, Rate);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void txtBoxNBQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double Qty = 0;
                double Weight = 0;
                double Rate = 0;
                string TruckNumber = txtBoxNBTruckNumber.Text.ToString();
                string QtyUnit = txtBoxNBQtyUnit.Text.ToString();
                string Particular = txtBoxNBParticulars.Text;
                string WeightUnit = cbNBWUnit.SelectedItem.ToString();

                var QtyCheck = txtBoxNBQty.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(QtyCheck) && !Regex.IsMatch(QtyCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Quantity should be in numbers.", "Alert");
                        txtBoxNBQty.Text = "0";
                        return;
                    }

                    if (txtBoxNBQty.Text == "") Qty = 0; else Qty = Convert.ToDouble(txtBoxNBQty.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Quantity should be in numbers.");
                    Qty = 0; txtBoxNBQty.Text = "0";
                }


                var WtCheck = txtBoxNBWeight.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(WtCheck) && !Regex.IsMatch(WtCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Weight should be in numbers.", "Alert");
                        txtBoxNBWeight.Text = "0";
                        return;
                    }

                    if (txtBoxNBWeight.Text == "") Weight = 0; else Weight = Convert.ToDouble(txtBoxNBWeight.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Weight should be in numbers.");
                    Weight = 0; txtBoxNBWeight.Text = "0";
                }

                var RateCheck = txtBoxNBRate.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(RateCheck) && !Regex.IsMatch(RateCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Rate should be in numbers.", "Alert");
                        txtBoxNBRate.Text = "0";
                        return;
                    }

                    if (txtBoxNBRate.Text == "") Rate = 0; else Rate = Convert.ToDouble(txtBoxNBRate.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Rate should be in numbers.");
                    Rate = 0; txtBoxNBRate.Text = "0";
                }

                RefreshListView(TruckNumber, Qty, QtyUnit, Particular, Weight, WeightUnit, Rate);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void txtBoxNBParticulars_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string TruckNumber = txtBoxNBTruckNumber.Text.ToString();
                double Qty = 0;
                string QtyUnit = txtBoxNBQtyUnit.Text.ToString();
                string Particular = txtBoxNBParticulars.Text;
                double Weight = 0;
                string WeightUnit = cbNBWUnit.SelectedItem.ToString();
                double Rate = 0;

                try { if (txtBoxNBQty.Text == "")Qty = 0; else Qty = Convert.ToDouble(txtBoxNBQty.Text); }
                catch { Qty = 0; }
                try { if (txtBoxNBWeight.Text == "")Weight = 0; else Weight = Convert.ToDouble(txtBoxNBWeight.Text); }
                catch { Weight = 0; }
                try { if (txtBoxNBRate.Text == "")Rate = 0; else Rate = Convert.ToDouble(txtBoxNBRate.Text); }
                catch { Rate = 0; }
                RefreshListView(TruckNumber, Qty, QtyUnit, Particular, Weight, WeightUnit, Rate);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void txtBoxNBWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double Qty = 0;
                double Weight = 0;
                double Rate = 0;
                string TruckNumber = txtBoxNBTruckNumber.Text.ToString();
                string QtyUnit = txtBoxNBQtyUnit.Text.ToString();
                string Particular = txtBoxNBParticulars.Text;
                string WeightUnit = cbNBWUnit.SelectedItem.ToString();

                var QtyCheck = txtBoxNBQty.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(QtyCheck) && !Regex.IsMatch(QtyCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Quantity should be in numbers.", "Alert");
                        txtBoxNBQty.Text = "0";
                        return;
                    }

                    if (txtBoxNBQty.Text == "") Qty = 0; else Qty = Convert.ToDouble(txtBoxNBQty.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Quantity should be in numbers.");
                    Qty = 0; txtBoxNBQty.Text = "0";
                }

                var WtCheck = txtBoxNBWeight.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(WtCheck) && !Regex.IsMatch(WtCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Weight should be in numbers.", "Alert");
                        txtBoxNBWeight.Text = "0";
                        return;
                    }

                    if (txtBoxNBWeight.Text == "") Weight = 0; else Weight = Convert.ToDouble(txtBoxNBWeight.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Weight should be in numbers.");
                    Weight = 0; txtBoxNBWeight.Text = "0";
                    //Logger.WriteErrorLog(ex);
                }

                var RateCheck = txtBoxNBRate.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(RateCheck) && !Regex.IsMatch(RateCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Rate should be in numbers.", "Alert");
                        txtBoxNBRate.Text = "0";
                        return;
                    }

                    if (txtBoxNBRate.Text == "") Rate = 0; else Rate = Convert.ToDouble(txtBoxNBRate.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Rate should be in numbers.");
                    Rate = 0; txtBoxNBRate.Text = "0";
                }

                RefreshListView(TruckNumber, Qty, QtyUnit, Particular, Weight, WeightUnit, Rate);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void txtBoxNBRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double Qty = 0;
                double Weight = 0;
                double Rate = 0;
                string TruckNumber = txtBoxNBTruckNumber.Text.ToString();
                string QtyUnit = txtBoxNBQtyUnit.Text.ToString();
                string Particular = txtBoxNBParticulars.Text;
                string WeightUnit = cbNBWUnit.SelectedItem.ToString();

                var QtyCheck = txtBoxNBQty.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(QtyCheck) && !Regex.IsMatch(QtyCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Quantity should be in numbers.", "Alert");
                        txtBoxNBQty.Text = "0";
                        return;
                    }

                    if (txtBoxNBQty.Text == "") Qty = 0; else Qty = Convert.ToDouble(txtBoxNBQty.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Quantity should be in numbers.");
                    Qty = 0; txtBoxNBQty.Text = "0";
                }

                var WtCheck = txtBoxNBWeight.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(WtCheck) && !Regex.IsMatch(WtCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Weight should be in numbers.", "Alert");
                        txtBoxNBWeight.Text = "0";
                        return;
                    }

                    if (txtBoxNBWeight.Text == "") Weight = 0; else Weight = Convert.ToDouble(txtBoxNBWeight.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Weight should be in numbers.");
                    Weight = 0; txtBoxNBWeight.Text = "0";
                }

                var RateCheck = txtBoxNBRate.Text.Trim();
                try
                {
                    if (!String.IsNullOrEmpty(RateCheck) && !Regex.IsMatch(RateCheck, "\\d+"))
                    {
                        System.Windows.MessageBox.Show("Rate should be in numbers.", "Alert");
                        txtBoxNBRate.Text = "0";
                        return;
                    }

                    if (txtBoxNBRate.Text == "") Rate = 0; else Rate = Convert.ToDouble(txtBoxNBRate.Text);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Rate should be in numbers.");
                    Rate = 0; txtBoxNBRate.Text = "0";
                }

                RefreshListView(TruckNumber, Qty, QtyUnit, Particular, Weight, WeightUnit, Rate);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        /// <summary>
        /// Refreshses the ListView row with given values
        /// </summary>
        /// <param name="value1">Value for column 1</param>
        /// <param name="value2">Value for column 2</param>
        private void RefreshListView(string TruckNumber, double Qty, string QtyUnit, string Particular, double Weight, string WeightUnit, double Rate)
        {
            try
            {
                BillParticular lvc = (BillParticular)lstVwNewBill.SelectedItem; //new ListViewClass(value1, value2);
                if (lvc != null && !stopRefreshControls)
                {
                    setDataChanged(true);

                    lvc.bpTruckNumber = TruckNumber;
                    lvc.bpQuantity = Qty;
                    lvc.bpQuantityUnit = QtyUnit;
                    lvc.bpParticulars = Particular;
                    lvc.bpWeight = Weight;
                    lvc.bpWeightUnit = WeightUnit;
                    lvc.bpRate = Rate;
                    //lvc.bpAmount = (Weight * Rate);
                    //KG or TN
                    if (WeightUnit.ToUpper() == "KG")
                    {
                        //lvc.bpAmount = ((Rate * Qty * Weight) / 100);
                        //lvc.bpAmount = ((Rate * .010) * Qty * Weight);
                        lvc.bpAmount = ((Rate * .010) * Weight);
                    }
                    else if (WeightUnit.ToUpper() == "TN")
                    {
                        //lvc.bpAmount = ((Rate * 10) * Qty * Weight);
                        lvc.bpAmount = ((Rate * 10) * Weight);
                    }
                    lstVwNewBill.Items.Refresh();
                }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        #region ListBox-SelectionChanged-lbSuggestion
        private void lbSuggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSuggestion.SelectedIndex == -1) return;

            if (lbSuggestion.ItemsSource != null)
            {
                //double left = 130, top = -80, right = 0, bottom = 0;
                //dpNBDeliveryDate.Margin = new Thickness(left, top, right, bottom);                

                txtBoxNBClientName.TextChanged -= new TextChangedEventHandler(txtBoxNBClientName_TextChanged);
                if (lbSuggestion.SelectedIndex != -1)
                {
                    txtBoxNBClientName.Text = lbSuggestion.SelectedItem.ToString();
                    _selectedClient = _bgDHandler.ClientDict[GetClientIdByName(txtBoxNBClientName.Text.ToString().ToUpper())];
                }
                lbSuggestion.SelectedIndex = -1;
                lbSuggestion.Visibility = Visibility.Collapsed;
                txtBoxNBClientName.TextChanged += new TextChangedEventHandler(txtBoxNBClientName_TextChanged);
            }
        }
        #endregion

        private void lstVwNewBill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BillParticular lvc = (BillParticular)lstVwNewBill.SelectedItem;
            if (lvc != null)
            {
                stopRefreshControls = true;
                txtBoxNBTruckNumber.Text = lvc.bpTruckNumber;
                txtBoxNBQty.Text = Convert.ToString(lvc.bpQuantity);
                txtBoxNBQtyUnit.Text = lvc.bpQuantityUnit;
                txtBoxNBParticulars.Text = lvc.bpParticulars;
                txtBoxNBWeight.Text = Convert.ToString(lvc.bpWeight);
                txtBoxNBRate.Text = Convert.ToString(lvc.bpRate);
                stopRefreshControls = false;
            }
        }

        #endregion

        private void txtBoxNBBroker_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = txtBoxNBBroker.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in _bgDHandler.BrokerList)
            {
                if (!string.IsNullOrEmpty(txtBoxNBBroker.Text))
                {
                    if (item.StartsWith(typedString.ToUpper()))
                    {
                        autoList.Add(item);
                    }
                }
            }

            if (autoList.Count > 0)
            {
                lblBrokerSuggestion.ItemsSource = autoList;
                lblBrokerSuggestion.Visibility = Visibility.Visible;
                //lblBrokerSuggestion.SelectedIndex = 0;
            }
            else if (txtBoxNBBroker.Text.Equals(""))
            {
                lblBrokerSuggestion.Visibility = Visibility.Collapsed;
                lblBrokerSuggestion.ItemsSource = null;
            }
            else
            {
                lblBrokerSuggestion.Visibility = Visibility.Collapsed;
                lblBrokerSuggestion.ItemsSource = null;
            }
        }

        private void lblBroker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lblBrokerSuggestion.SelectedIndex == -1) return;

                if (lblBrokerSuggestion.ItemsSource != null)
                {
                    txtBoxNBBroker.TextChanged -= new TextChangedEventHandler(txtBoxNBBroker_TextChanged);
                    if (lblBrokerSuggestion.SelectedIndex != -1)
                    {
                        txtBoxNBBroker.Text = lblBrokerSuggestion.SelectedItem.ToString();
                    }
                    lblBrokerSuggestion.SelectedIndex = -1;
                    lblBrokerSuggestion.Visibility = Visibility.Collapsed;
                    txtBoxNBBroker.TextChanged += new TextChangedEventHandler(txtBoxNBBroker_TextChanged);
                }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void txtBoxNBParticular_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = txtBoxNBParticulars.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in _bgDHandler.ParticularList)
            {
                if (!string.IsNullOrEmpty(txtBoxNBParticulars.Text))
                {
                    if (item.StartsWith(typedString.ToUpper()))
                    {
                        autoList.Add(item);
                    }
                }
            }

            if (autoList.Count > 0)
            {
                lblParticularsSuggestion.ItemsSource = autoList;
                lblParticularsSuggestion.Visibility = Visibility.Visible;
            }
            else if (txtBoxNBParticulars.Text.Equals(""))
            {
                lblParticularsSuggestion.Visibility = Visibility.Collapsed;
                lblParticularsSuggestion.ItemsSource = null;
            }
            else
            {
                lblParticularsSuggestion.Visibility = Visibility.Collapsed;
                lblParticularsSuggestion.ItemsSource = null;
            }
        }

        private void lblParticulars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lblParticularsSuggestion.SelectedIndex == -1) return;

                if (lblParticularsSuggestion.ItemsSource != null)
                {
                    txtBoxNBParticulars.TextChanged -= new TextChangedEventHandler(txtBoxNBParticular_TextChanged);
                    if (lblParticularsSuggestion.SelectedIndex != -1)
                    {
                        txtBoxNBParticulars.Text = lblParticularsSuggestion.SelectedItem.ToString();
                    }
                    lblParticularsSuggestion.SelectedIndex = -1;
                    lblParticularsSuggestion.Visibility = Visibility.Collapsed;
                    txtBoxNBParticulars.TextChanged += new TextChangedEventHandler(txtBoxNBParticular_TextChanged);
                }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void btnNBModifyBill_Click(object sender, RoutedEventArgs e)
        {
            //_bgDHandler.AddOrUpdateRawBill(_selectedBill);
            try
            {
                if (!_bgDHandler.QtyUnitList.Contains(txtBoxNBQtyUnit.Text.ToUpper()))
                {
                    _bgDHandler.QtyUnitList.Add(txtBoxNBQtyUnit.Text.ToUpper());
                    CommonClass.UpdateQtyUnitDetail(txtBoxNBQtyUnit.Text.ToUpper());
                }
                if (!_bgDHandler.BrokerList.Contains(txtBoxNBBroker.Text.ToUpper()))
                {
                    _bgDHandler.BrokerList.Add(txtBoxNBBroker.Text.ToUpper());
                    CommonClass.UpdateBrokerDetail(txtBoxNBBroker.Text.ToUpper());
                }
                if (!_bgDHandler.ParticularList.Contains(txtBoxNBParticulars.Text.ToUpper()))
                {
                    _bgDHandler.ParticularList.Add(txtBoxNBParticulars.Text.ToUpper());
                    CommonClass.UpdateParticularsDetail(txtBoxNBParticulars.Text.ToUpper());
                }

                setDataChanged(true);
                BillParticular lvc = (BillParticular)lstVwNewBill.SelectedItem;
                if (lvc != null)
                {
                    stopRefreshControls = true;
                    lvc.bpTruckNumber = txtBoxNBTruckNumber.Text;
                    lvc.bpQuantity = Convert.ToDouble(txtBoxNBQty.Text);
                    lvc.bpQuantityUnit = txtBoxNBQtyUnit.Text;
                    lvc.bpParticulars = txtBoxNBParticulars.Text;
                    lvc.bpWeight = Convert.ToDouble(txtBoxNBWeight.Text);
                    // cbNBWUnit.SelectedItem = lvc.bpWeightUnit;
                    lvc.bpRate = Convert.ToDouble(txtBoxNBRate.Text);
                    isModifyBill = true;
                    stopRefreshControls = false;
                }
                lstVwNewBill.SelectedIndex = lstVwNewBill.SelectedIndex + 1;
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void btnNBGenerateModifyBill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBoxNBClientName.Text.ToString()))
                {
                    System.Windows.MessageBox.Show("Enter Client Name.");
                    txtBoxNBClientName.Focus();
                    return;
                }
                if (dpNBDeliveryDate.SelectedDate == null)
                {
                    System.Windows.MessageBox.Show("Select Delivery Date.");
                    dpNBDeliveryDate.Focus();
                    return;
                }
                if (dpNBBillDate.SelectedDate == null)
                {
                    System.Windows.MessageBox.Show("Select Bill Date.");
                    dpNBBillDate.Focus();
                    return;
                }
                //if (dpNBSaudaDate.SelectedDate == null)
                //{
                //    System.Windows.MessageBox.Show("Select Sauda Date.");
                //    dpNBSaudaDate.Focus();
                //    return;
                //}
                //if (dpNBDueDate.SelectedDate == null)
                //{
                //    System.Windows.MessageBox.Show("Select Due Date.");
                //    dpNBDueDate.Focus();
                //    return;
                //}
                if (string.IsNullOrEmpty(txtBoxNBBillNumber.Text.ToString()))
                {
                    System.Windows.MessageBox.Show("Enter Bill Number");
                    txtBoxNBBillNumber.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBoxNBBroker.Text.ToString()))
                {
                    System.Windows.MessageBox.Show("Enter Broker Name.");
                    txtBoxNBBroker.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(txtBoxNBTruckNumber.Text.ToString()))
                //{
                //    System.Windows.MessageBox.Show("Enter Truck Number.");
                //    txtBoxNBTruckNumber.Focus();
                //    return;
                //}

                if (lstVwNewBill.Items.Count <= 1)
                {
                    System.Windows.MessageBox.Show("Please Click on ADD atleast once to Add Item Particulars Before Generate Bill.");
                    txtBoxNBQty.Focus();
                    return;
                }

                BillGenerate(true);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        private void txtBoxNBClientName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lbSuggestion.Items.Count != 0)
            {
                lbSuggestion.SelectedIndex = 0;
            }
            lbSuggestion.Visibility = Visibility.Collapsed;
        }

        private void txtBoxNBBroker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lblBrokerSuggestion.Items.Count != 0)
            {
                lblBrokerSuggestion.SelectedIndex = 0;
            }
            lblBrokerSuggestion.Visibility = Visibility.Collapsed;
        }

        private void txtBoxNBQtyUnit_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (lblQUnitSuggestion.Items.Count != 0)
            {
                lblQUnitSuggestion.SelectedIndex = 0;
            }
            lblQUnitSuggestion.Visibility = Visibility.Collapsed;
        }

        private void txtBoxNBParticulars_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lblParticularsSuggestion.Items.Count != 0)
            {
                lblParticularsSuggestion.SelectedIndex = 0;
            }
            lblParticularsSuggestion.Visibility = Visibility.Collapsed;
        }

        private void txtBoxNBQty_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                RestoreOldValue(sender);
            }
        }

        private void txtBoxNBParticulars_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                RestoreOldValue(sender);
            }
        }

        private void txtBoxNBWeight_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                RestoreOldValue(sender);
            }
        }

        private void txtBoxNBRate_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                RestoreOldValue(sender);
            }
        }

        private void txtBoxNBQty_GotFocus(object sender, RoutedEventArgs e)
        {
            StoreCurrentValue(sender);
            txtBoxNBQty.SelectAll();
        }

        private void txtBoxNBQtyUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            //lblQUnitSuggestion.Visibility = Visibility.Collapsed;
            //lblQUnitSuggestion.ItemsSource = null;            
        }

        private void txtBoxNBParticulars_GotFocus(object sender, RoutedEventArgs e)
        {
            StoreCurrentValue(sender);
            txtBoxNBParticulars.SelectAll();
        }

        private void txtBoxNBWeight_GotFocus(object sender, RoutedEventArgs e)
        {
            StoreCurrentValue(sender);
            txtBoxNBWeight.SelectAll();
        }

        private void txtBoxNBRate_GotFocus(object sender, RoutedEventArgs e)
        {
            StoreCurrentValue(sender);
            txtBoxNBRate.SelectAll();
        }

        private void btnNBAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_bgDHandler.QtyUnitList.Contains(txtBoxNBQtyUnit.Text.ToUpper()))
                {
                    _bgDHandler.QtyUnitList.Add(txtBoxNBQtyUnit.Text.ToUpper());
                    CommonClass.UpdateQtyUnitDetail(txtBoxNBQtyUnit.Text.ToUpper());
                }
                if (!_bgDHandler.BrokerList.Contains(txtBoxNBBroker.Text.ToUpper()))
                {
                    _bgDHandler.BrokerList.Add(txtBoxNBBroker.Text.ToUpper());
                    CommonClass.UpdateBrokerDetail(txtBoxNBBroker.Text.ToUpper());
                }
                if (!_bgDHandler.ParticularList.Contains(txtBoxNBParticulars.Text.ToUpper()))
                {
                    _bgDHandler.ParticularList.Add(txtBoxNBParticulars.Text.ToUpper());
                    CommonClass.UpdateParticularsDetail(txtBoxNBParticulars.Text.ToUpper());
                }

                setDataChanged(true);
                AddRow();
                txtBoxNBTruckNumber.Focus();
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }
        private void btnNBRemove_Click(object sender, RoutedEventArgs e)
        {
            setDataChanged(true);
            int selectedIndex = lstVwNewBill.SelectedIndex;

            lstVwNewBill.Items.Remove(lstVwNewBill.SelectedItem);

            // if no rows left, add a blank row
            if (lstVwNewBill.Items.Count == 0)
            {
                AddRow();
                txtBoxNBTruckNumber.Focus();
            }
            else if (selectedIndex <= lstVwNewBill.Items.Count - 1) // otherwise select next row
            {
                lstVwNewBill.SelectedIndex = selectedIndex;
            }
            else // not above cases? Select last row
            {
                lstVwNewBill.SelectedIndex = lstVwNewBill.Items.Count - 1;
            }
        }

        private void btnNBGenerateNewBill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBoxNBClientName.Text.ToString()))
                {
                    System.Windows.MessageBox.Show("Enter Client Name.");
                    txtBoxNBClientName.Focus();
                    return;
                }
                if (dpNBDeliveryDate.SelectedDate == null)
                {
                    System.Windows.MessageBox.Show("Select Delivery Date.");
                    dpNBDeliveryDate.Focus();
                    return;
                }
                if (dpNBBillDate.SelectedDate == null)
                {
                    System.Windows.MessageBox.Show("Select Bill Date.");
                    dpNBBillDate.Focus();
                    return;
                }
                //if (dpNBSaudaDate.SelectedDate == null)
                //{
                //    System.Windows.MessageBox.Show("Select Sauda Date.");
                //    dpNBSaudaDate.Focus();
                //    return;
                //}
                //if (dpNBDueDate.SelectedDate == null)
                //{
                //    System.Windows.MessageBox.Show("Select Due Date.");
                //    dpNBDueDate.Focus();
                //    return;
                //}
                if (string.IsNullOrEmpty(txtBoxNBBillNumber.Text.ToString()))
                {
                    System.Windows.MessageBox.Show("Enter Bill Number");
                    txtBoxNBBillNumber.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBoxNBBroker.Text.ToString()))
                {
                    System.Windows.MessageBox.Show("Enter Broker Name.");
                    txtBoxNBBroker.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(txtBoxNBTruckNumber.Text.ToString()))
                //{
                //    System.Windows.MessageBox.Show("Enter Truck Number.");
                //    txtBoxNBTruckNumber.Focus();
                //    return;
                //}

                if (lstVwNewBill.Items.Count <= 1)
                {
                    System.Windows.MessageBox.Show("Please Click on ADD atleast once to Add Item Particulars Before Generate Bill.");
                    txtBoxNBQty.Focus();
                    return;
                }

                BillGenerate(false);
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        void BillGenerate(bool modifyBill)
        {
            if (dataChanged)
            {
                //_bgDHandler.BillNumber++;
                //_selectedBill.bNumber = _bgDHandler.BillNumber;
                if (!modifyBill)
                {
                    _selectedBill = new Bill();
                    _selectedBill.bClient = _selectedClient;
                    _selectedBill.bCompany = _selectedCompany;
                    _selectedBill.bBank = _selectedBank;
                }
                if (modifyBill)
                {
                    //_selectedBill.bBillParticulars.Clear();
                    if (_selectedBill == null)
                    {
                        _selectedBill = new Bill();
                    }
                    else
                        _selectedBill.ResetBill();

                    if (_selectedClient == null) _selectedClient = _bgDHandler.ClientDict[GetClientIdByName(txtBoxNBClientName.Text.ToString().ToUpper())];
                    if (_selectedBank == null) _selectedBank = _bgDHandler.BankDict[_selectedCompany.cID];
                    _selectedBill.bClient = _selectedClient;
                    _selectedBill.bCompany = _selectedCompany;
                    _selectedBill.bBank = _selectedBank;
                    isModifyBill = false;
                }
                //_selectedBill = tempBill; 
                _selectedBill.bBillNumber = Convert.ToDouble(txtBoxNBBillNumber.Text);
                //_selectedBill.bCompany = _selectedCompany;
                _selectedBill.bBroker = txtBoxNBBroker.Text;
                _selectedBill.bBillDate = dpNBBillDate.SelectedDate.Value.Date;
                _selectedBill.bDeliveryDate = dpNBDeliveryDate.SelectedDate.Value.Date;
                if (dpNBSaudaDate.SelectedDate == null)
                    _selectedBill.bSaudaDate = DateTime.MinValue;
                else
                    _selectedBill.bSaudaDate = dpNBSaudaDate.SelectedDate.Value.Date;

                if (dpNBDueDate.SelectedDate == null)
                    _selectedBill.bDueDate = DateTime.MinValue;
                else
                    _selectedBill.bDueDate = dpNBDueDate.SelectedDate.Value.Date;
                //_selectedBill.bTruckNumber = txtBoxNBTruckNumber.Text.ToString();

                foreach (BillParticular bp in lstVwNewBill.Items)
                {
                    if (bp.bpQuantity > 0)
                        _selectedBill.bBillParticulars.Add(bp);
                }

                _selectedBill.CalculateTotalQty();
                _selectedBill.CalculateTotalWeight();
                _selectedBill.CalculateTotalAmount();

                setDataChanged(false);

                try
                {
                    //if (Convert.ToDouble(txtBoxNBBillNumber.Text) > (_bgDHandler.BillNumber[_selectedCompany.cID] - 1))
                    //    _bgDHandler.UpdateBillNumber(Convert.ToString(txtBoxNBBillNumber.Text), _selectedCompany.cID);

                    //if (!modifyBill)
                    //    _bgDHandler.BillNumber[_selectedCompany.cID] += 1;
                    if (_selectedBill.bBillNumber > (_bgDHandler.BillNumber[_selectedCompany.cID] - 1))
                        _bgDHandler.UpdateBillNumber(_selectedBill.bBillNumber, _selectedCompany.cID);

                    if (!modifyBill)
                        _bgDHandler.BillNumber[_selectedCompany.cID] = _selectedBill.bBillNumber + 1;
                    txtBoxNBBillNumber.Text = Convert.ToString(_bgDHandler.BillNumber[_selectedCompany.cID]);
                }
                catch { }

                try
                {
                    _bgDHandler.AddOrUpdateRawBill(_selectedBill);

                }
                catch { }

                if (!_bgDHandler.BillDict.ContainsKey(_selectedCompany.cID))
                    _bgDHandler.BillDict.Add(_selectedCompany.cID, new Dictionary<double, Bill>());


                if (!_bgDHandler.BillDict[_selectedCompany.cID].ContainsKey(_selectedBill.bBillNumber))
                    _bgDHandler.BillDict[_selectedCompany.cID].Add(_selectedBill.bBillNumber, _selectedBill);

                ReportGenerate rg = new ReportGenerate(_selectedBill);
                rg.Dock = DockStyle.Fill;
                
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF")) Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF");
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF\\" + _selectedCompany.cName.ToUpper().ToString())) Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF\\" + _selectedCompany.cName.ToUpper().ToString());
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF\\" + _selectedCompany.cName.ToUpper().ToString() + "\\" + DateTime.Now.Year.ToString())) Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF\\" + _selectedCompany.cName.ToUpper().ToString() + "\\" + DateTime.Now.Year.ToString());
                string path = System.Windows.Forms.Application.StartupPath + "\\Bills\\PDF\\" + _selectedCompany.cName.ToUpper().ToString() + "\\" + DateTime.Now.Year.ToString() + "," + _selectedBill.bBillNumber + ".PDF";
                rg.PdfReport(path);

                try
                {
                    System.Threading.Thread.Sleep(1500);
                    ClearBillForm();
                    txtBoxNBClientName.Focus();
                }
                catch { }
            }
        }

        private void ClearBillForm()
        {
            txtBoxNBClientName.Text = "";
            txtBoxNBBroker.Text = "";
            dpNBBillDate.Text = "";
            dpNBDeliveryDate.Text = "";
            dpNBSaudaDate.Text = "";
            dpNBDueDate.Text = "";
            //txtBoxNBTruckNumber.Text = "";
            lstVwNewBill.Items.Clear();

            if (lstVwNewBill.Items.Count == 0)
            {
                AddRow();
            }
            else
            {
                lstVwNewBill.SelectedIndex = 0;
            }
            //_selectedBill.ResetBill(); 
            //txtBoxNBClientName.Focus();
        }
        
        /// <summary>
        /// ComboBox For Client Names   [COMMENTED]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxNBClientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_selectedClient = _bgDHandler.ClientDict[txtBoxNBClientName.SelectedItem.ToString().ToLower()];
        }

        #region"New Bill Events"

        void AddRow()
        {
            try
            {
                lstVwNewBill.Items.Add(new BillParticular());
                lstVwNewBill.SelectedIndex = lstVwNewBill.Items.Count - 1;

                txtBoxNBTruckNumber.Text = "";
                txtBoxNBQty.Text = "";
                txtBoxNBQtyUnit.Text = "";
                txtBoxNBParticulars.Text = "";
                txtBoxNBWeight.Text = "";
                txtBoxNBRate.Text = "";
                //txtBoxNBTruckNumber.Focus();

                //try
                //{
                //    _bgDHandler.UpdateQtyUnitDetail();
                //}catch{}
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        /// <summary>
        /// removeButton click event handler
        /// Removes the selected row from the ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setDataChanged(true);
                int selectedIndex = lstVwNewBill.SelectedIndex;

                lstVwNewBill.Items.Remove(lstVwNewBill.SelectedItem);

                // if no rows left, add a blank row
                if (lstVwNewBill.Items.Count == 0)
                {
                    AddRow();
                    txtBoxNBTruckNumber.Focus();
                }
                else if (selectedIndex <= lstVwNewBill.Items.Count - 1) // otherwise select next row
                {
                    lstVwNewBill.SelectedIndex = selectedIndex;
                }
                else // not above cases? Select last row
                {
                    lstVwNewBill.SelectedIndex = lstVwNewBill.Items.Count - 1;
                }
            }
            catch (Exception ex) { Logger.WriteErrorLog(ex); }
        }

        #endregion

    }
}
