using ESCO.Reference.Data.Services;
using ESCO.Reference.Data.Model;
using MetroFramework.Forms;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using MetroFramework.Controls;

namespace ESCO.Reference.Data.App
{
    public partial class Init : MetroForm
    {
        private ReferenceDataServices _services;
        private string _schemaActive;
        private KeyWin keyWin;

        private const string _all = "Todos";

        public Init()
        {
            InitializeComponent();
        }

        private void Init_Shown(object sender, EventArgs e)
        {
            string subscriptionKey = ConfigurationManager.AppSettings["subscriptionKey"];
            if (subscriptionKey == String.Empty || subscriptionKey == null)
            {
                showKeyWin(true);
            }
            else
            {
                keyText.Text = subscriptionKey;
                _services = new ReferenceDataServices(subscriptionKey);
            }
            if (_services != null) { _ = setSchemas(); }
            else { Close(); }
        }

        private void changeKeyBtn_Click(object sender, EventArgs e)
        {
            showKeyWin(false);
        }

        private void showKeyWin(bool modal)
        {
            if (keyWin == null)
            {
                keyWin = new KeyWin(keyText.Text, modal);
                keyWin.FormClosed += keyWin_WindowClosed;
                keyWin.ShowDialog();
            }
        }

        private void keyWin_WindowClosed(object sender, FormClosedEventArgs e)
        {            
            if (keyWin.key != String.Empty && keyWin.ActiveControl.Text == "Accept")
            {
                keyText.Text = keyWin.key;
                _services = new ReferenceDataServices(keyWin.key);                
            }
            if (keyWin.ActiveControl.Text == "Close")
            {
                _services = null;               
            }
            keyWin = null;
        }

        private async Task setSchemas()
        {
            try
            {
                Schemas schemas = await _services.getSchemas();
                Schema active = await _services.getSchema();
                string schemaActive = active.id;

                Dictionary<int, string> schemasKeys = new Dictionary<int, string>();
                for (int i = 0; i < schemas.Count; i++) {
                    int index = Int32.Parse(schemas[i].id); 
                    string strActive = (schemas[i].id == schemaActive) ? " (Activo)" : String.Empty;
                    schemasKeys[index] = schemas[i].name + strActive;
                }               

                BindingSource bindingSource = new BindingSource(schemasKeys, null);
                schemaComboInit.DataSource = bindingSource;
                schemaComboInit.DisplayMember = "Value";
                schemaComboInit.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private async Task setSchemaActive()
        {
            try
            {                
                Schema spec = await _services.getSchema();
                _schemaActive = spec.id;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> getSchemaActive()
        {
            Schema spec = await _services.getSchema();
            return spec.id;
        }   

        #region ReferenceDatas

        private void disableRDControls()
        {
            typeRDLabel.Visible = false;
            IdRDLabel.Visible = false;
            typeRDCombo.Visible = false;
            IdRDText.Visible = false;
        }

        private void enabledRDGet()
        {
            typeRDLabel.Visible = true;
            IdRDLabel.Visible = false;
            typeRDCombo.Visible = true;
            IdRDText.Visible = false;
        }

        private void enabledRDSearch()
        {           
            typeRDLabel.Visible = false;
            IdRDLabel.Visible = true;
            typeRDCombo.Visible = false;
            IdRDText.Visible = true;
        }

        private void RefereceDataCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RefereceDataCombo.SelectedIndex)
            {
                case 0:
                    urlReferenceData.Text = Config.TodayUpdated + Config.FilterTypeStr;
                    enabledRDGet();
                    break;
                case 1:
                    urlReferenceData.Text = Config.TodayUpdated + Config.FilterId;
                    enabledRDSearch();
                    break;
                case 2:
                    urlReferenceData.Text = Config.TodayAdded + Config.FilterTypeStr;
                    enabledRDGet();
                    break;
                case 3:
                    urlReferenceData.Text = Config.TodayAdded + Config.FilterId;
                    enabledRDSearch();
                    break;
                case 4:
                    urlReferenceData.Text = Config.TodayRemoved + Config.FilterTypeStr;
                    enabledRDGet();
                    break;
                case 5:
                    urlReferenceData.Text = Config.TodayRemoved + Config.FilterId;
                    enabledRDSearch();
                    break;
                case 6:
                    urlReferenceData.Text = Config.ReferenceDatas + Config.FilterTypeStr;
                    enabledRDGet();
                    break;
                case 7:
                    urlReferenceData.Text = Config.ReferenceDatas + Config.FilterId;
                    enabledRDSearch();
                    break;
                case 8:
                    urlReferenceData.Text = Config.Specification;
                    disableRDControls();
                    break;
                default:
                    //
                    break;
            }            
        }

        private void typeRDCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (typeRDCombo.DataSource == null)
            {
                _ = setRDTypes(typeRDCombo);
            }
        }

        private void ReferenceDataSendBtn_Click(object sender, EventArgs e)
        {
            ReferenceDataSendBtn.Enabled = false;
            SectionsTab.Enabled = false;
            ReferenceDataView.DataSource = null;
            ReferenceDataTextBox.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var type = (typeRDCombo.SelectedValue == null || typeRDCombo.Text == _all) ? null
                : typeRDCombo.SelectedValue.ToString();
            var id = IdRDText.Text;

            type = (type == String.Empty) ? null : type;

            switch (RefereceDataCombo.SelectedIndex)
            {
                case 0:
                    _ = getReferenceDataTodayUpdated(type, false);
                    break;
                case 1:
                    _ = getReferenceDataTodayUpdated(id, true);
                    break;
                case 2:
                    _ = getReferenceDataTodayAdded(type, false);
                    break;
                case 3:
                    _ = getReferenceDataTodayAdded(id, true);
                    break;
                case 4:
                    _ = getReferenceDataTodayRemoved(type, false);
                    break;
                case 5:
                    _ = getReferenceDataTodayRemoved(id, true);
                    break;
                case 6:
                    _ = getReferenceDatas(type, false);
                    break;
                case 7:
                    _ = getReferenceDatas(id, true);
                    break;
                case 8:
                    _ = getReferenceDataSpecification();
                    break;
            }
        }

        private async Task getReferenceDataTodayUpdated(string val, bool search)
        {
            try
            {
                if (search && val == String.Empty)
                {
                    throw new Exception("Valor Id requerido");
                }

                ReferenceDatas spec = (search)? 
                    await _services.searchReferenceDataTodayUpdated(val, await getSchemaActive()) :
                    await _services.getReferenceDataTodayUpdated(val, await getSchemaActive());
                string result = JsonConvert.SerializeObject(spec, Formatting.Indented);

                ReferenceDataView.AutoGenerateColumns = true;
                ReferenceDataView.AutoResizeColumns();
                ReferenceDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReferenceDataView.DataSource = spec;
                ReferenceDataTextBox.Text = result;
                cantLabel.Text = spec.Count.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getReferenceDataTodayAdded(string val, bool search)
        {
            try
            {
                if (search && val == String.Empty)
                {
                    throw new Exception("Valor Id requerido");
                }

                ReferenceDatas spec = (search) ?
                    await _services.searchReferenceDataTodayAdded(val, await getSchemaActive()) :
                    await _services.getReferenceDataTodayAdded(val, await getSchemaActive());
                string result = JsonConvert.SerializeObject(spec, Formatting.Indented);

                ReferenceDataView.AutoGenerateColumns = true;
                ReferenceDataView.AutoResizeColumns();
                ReferenceDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReferenceDataView.DataSource = spec;
                ReferenceDataTextBox.Text = result;
                cantLabel.Text = spec.Count.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getReferenceDataTodayRemoved(string val, bool search)
        {
            try
            {
                if (search && val == String.Empty)
                {
                    throw new Exception("Valor Id requerido");
                }

                ReferenceDatas spec = (search) ?
                    await _services.searchReferenceDataTodayRemoved(val, await getSchemaActive()) :
                    await _services.getReferenceDataTodayRemoved(val, await getSchemaActive());               
                string result = JsonConvert.SerializeObject(spec, Formatting.Indented);

                ReferenceDataView.AutoGenerateColumns = true;
                ReferenceDataView.AutoResizeColumns();
                ReferenceDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReferenceDataView.DataSource = spec;
                ReferenceDataTextBox.Text = result;
                cantLabel.Text = spec.Count.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getReferenceDatas(string val, bool search)
        {
            try
            {
                if (search && val == String.Empty)
                {
                    throw new Exception("Valor Id requerido");
                }

                ReferenceDatas spec = (search) ?
                    await _services.searchReferenceData(val, await getSchemaActive()) :
                    await _services.getReferenceData(val, await getSchemaActive());
                string result = JsonConvert.SerializeObject(spec, Formatting.Indented);

                ReferenceDataView.AutoGenerateColumns = true;
                ReferenceDataView.AutoResizeColumns();
                ReferenceDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReferenceDataView.DataSource = spec;
                ReferenceDataTextBox.Text = result;
                cantLabel.Text = spec.Count.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getReferenceDataSpecification()
        {
            try
            {
                Specification result = await _services.getReferenceDataSpecification(await getSchemaActive());
                string spec = JsonConvert.SerializeObject(result, Formatting.Indented);

                ReferenceDataView.AutoGenerateColumns = true;
                ReferenceDataView.AutoResizeColumns();
                ReferenceDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReferenceDataView.DataSource = result;
                ReferenceDataTextBox.Text = spec;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        #endregion

        #region Instruments
        private void disableInstrumentsControls()
        {
            sourceInstrumentsLabel.Visible = false;
            typeInstrumentsLabel.Visible = false;
            IdInstrumentsLabel.Visible = false;
            sourceInstrumentsCombo.Visible = false;
            typeInstrumentsCombo.Visible = false;
            IdInstrumentsText.Visible = false;
        }

        private void enabledInstrumentsGet()
        {
            sourceInstrumentsLabel.Visible = true;
            typeInstrumentsLabel.Visible = true;
            IdInstrumentsLabel.Visible = false;
            sourceInstrumentsCombo.Visible = true;
            typeInstrumentsCombo.Visible = true;
            IdInstrumentsText.Visible = false;
        }

        private void enabledInstrumentsSearch()
        {
            sourceInstrumentsLabel.Visible = false;
            typeInstrumentsLabel.Visible = false;
            IdInstrumentsLabel.Visible = true;
            sourceInstrumentsCombo.Visible = false;
            typeInstrumentsCombo.Visible = false;
            IdInstrumentsText.Visible = true;
        }

        private void InstrumentsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (InstrumentsCombo.SelectedIndex)
            {
                case 0:
                    urlInstruments.Text = Config.InstrumentsSuggestedFields;
                    disableInstrumentsControls();
                    break;
                case 1:
                    urlInstruments.Text = Config.InstrumentsTodayUpdated;
                    enabledInstrumentsGet();
                    break;
                case 2:
                    urlInstruments.Text = Config.InstrumentsTodayUpdated;
                    enabledInstrumentsSearch();
                    break;
                case 3:
                    urlInstruments.Text = Config.InstrumentsTodayAdded;
                    enabledInstrumentsGet();
                    break;
                case 4:
                    urlInstruments.Text = Config.InstrumentsTodayAdded;
                    enabledInstrumentsSearch();
                    break;
                case 5:
                    urlInstruments.Text = Config.InstrumentsTodayRemoved;
                    enabledInstrumentsGet();
                    break;
                case 6:
                    urlInstruments.Text = Config.InstrumentsTodayRemoved;
                    enabledInstrumentsSearch();
                    break;
                case 7:
                    urlInstruments.Text = Config.InstrumentsReport;
                    enabledInstrumentsGet();
                    break;
                case 8:
                    urlInstruments.Text = Config.InstrumentsReport;
                    enabledInstrumentsSearch();
                    break;
                case 9:
                    urlInstruments.Text = Config.Instrument;
                    enabledInstrumentsSearch();
                    break;
                case 10:
                    urlInstruments.Text = Config.Instruments;
                    enabledInstrumentsGet();
                    break;
                case 11:
                    urlInstruments.Text = Config.Instruments;
                    enabledInstrumentsSearch();
                    break;
                default:
                    //
                    break;
            }
        }

        private void InstrumentsBtn_Click(object sender, EventArgs e)
        {
            InstrumentsBtn.Enabled = false;
            SectionsTab.Enabled = false;
            InstrumentsDataView.DataSource = null;
            InstrumentsDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var source = (sourceInstrumentsCombo.SelectedValue == null || sourceInstrumentsCombo.Text == _all) ? null
                : ((KeyValuePair<int, string>)sourceInstrumentsCombo.SelectedItem).Value.ToString();

            var type = (typeInstrumentsCombo.SelectedValue == null || typeInstrumentsCombo.Text == _all) ? null
                : ((KeyValuePair<int, string>)typeInstrumentsCombo.SelectedItem).Key.ToString();

            var id = IdInstrumentsText.Text;

            switch (InstrumentsCombo.SelectedIndex)
            {
                case 0:
                    _ = getInstrumentsSuggestedFields();
                    break;
                case 1:
                    _ = getInstrumentsTodayUpdated(type, false, source);
                    break;
                case 2:
                    _ = getInstrumentsTodayUpdated(id, true);
                    break;
                case 3:
                    _ = getInstrumentsTodayAdded(type, false, source);
                    break;
                case 4:
                    _ = getInstrumentsTodayAdded(id, true);
                    break;
                case 5:
                    _ = getInstrumentsTodayRemoved(type, false, source);
                    break;
                case 6:
                    _ = getInstrumentsTodayRemoved(id, true);
                    break;
                case 7:
                    _ = getInstrumentsReport(source, false);
                    break;
                case 8:
                    _ = getInstrumentsReport(id, true);
                    break;
                case 9:
                    _ = getInstrument(id);
                    break;
                case 10:
                    _ = getInstruments(type, false, source);
                    break;
                case 11:
                    _ = getInstruments(id, true);
                    break;
                default:
                    InstrumentsBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getInstrumentsSuggestedFields()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getInstrumentsTodayUpdated(string val, bool search, string source = null)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getInstrumentsTodayAdded(string val, bool search, string source = null)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getInstrumentsTodayRemoved(string val, bool search, string source = null)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getInstrumentsReport(string val, bool search)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getInstrument(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getInstruments(string val, bool search, string source = null)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                InstrumentsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private void typeInstrumentsCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (typeInstrumentsCombo.DataSource == null)
            {
                _ = setInstrumentsTypes(typeInstrumentsCombo);
            }
        }

        private void sourceInstrumentsCombo_Click(object sender, EventArgs e)
        {
            if (sourceInstrumentsCombo.DataSource == null)
            {
                _ = setInstrumentsSources(sourceInstrumentsCombo);
            }
        }

        private async Task setInstrumentsTypes(MetroComboBox combo)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async Task setInstrumentsSources(MetroComboBox combo)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region Fondos

        private void disableFundsControls()
        {
            IdFundsLabel.Visible = false;
            ManagmentFundsLabel.Visible = false;
            DepositaryFundsLabel.Visible = false;
            CurrencyFundsLabel.Visible = false;
            RentFundsLabel.Visible = false;

            IdFundsTxt.Visible = false;
            mangmentFundsCombo.Visible = false;
            depositaryFundsCombo.Visible = false;
            currencyFundsCombo.Visible = false;
            rentFundsCombo.Visible = false;
        }

        private void enabledFundsGet()
        {
            IdFundsLabel.Visible = true;
            ManagmentFundsLabel.Visible = false;
            DepositaryFundsLabel.Visible = false;
            CurrencyFundsLabel.Visible = false;
            RentFundsLabel.Visible = false;

            IdFundsTxt.Visible = true;
            mangmentFundsCombo.Visible = false;
            depositaryFundsCombo.Visible = false;
            currencyFundsCombo.Visible = false;
            rentFundsCombo.Visible = false;
        }

        private void enabledFundsSearch()
        {
            IdFundsLabel.Visible = false;
            ManagmentFundsLabel.Visible = true;
            DepositaryFundsLabel.Visible = true;
            CurrencyFundsLabel.Visible = true;
            RentFundsLabel.Visible = true;

            IdFundsTxt.Visible = false;
            mangmentFundsCombo.Visible = true;
            depositaryFundsCombo.Visible = true;
            currencyFundsCombo.Visible = true;
            rentFundsCombo.Visible = true;
        }

        private void FondosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FondosCombo.SelectedIndex)
            {
                case 0:
                    urlFunds.Text = Config.Fund;
                    enabledFundsGet();
                    break;
                case 1:
                    urlFunds.Text = Config.Funds;
                    enabledFundsSearch();
                    break;
                case 2:
                    urlFunds.Text = Config.Funds;
                    enabledFundsGet();
                    break;
                default:
                    //
                    break;
            }
        } 
        
        private void FundsBtn_Click(object sender, EventArgs e)
        {
            FundsBtn.Enabled = false;
            SectionsTab.Enabled = false;
            FundsDataView.DataSource = null;
            FundsDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = IdFundsTxt.Text;

            var managment = (mangmentFundsCombo.SelectedValue == null || mangmentFundsCombo.Text == _all) ? null 
                : ((KeyValuePair<int, string>)mangmentFundsCombo.SelectedItem).Key.ToString();

            var depositary = (depositaryFundsCombo.SelectedValue == null || depositaryFundsCombo.Text == _all) ? null 
                : ((KeyValuePair<int, string>)depositaryFundsCombo.SelectedItem).Key.ToString();

            var currency = (currencyFundsCombo.SelectedValue == null || currencyFundsCombo.Text == _all) ? null 
                : currencyFundsCombo.SelectedValue.ToString();

            var rent = (rentFundsCombo.SelectedValue == null || rentFundsCombo.Text == _all) ? null 
                : ((KeyValuePair<int, string>)rentFundsCombo.SelectedItem).Key.ToString();

            switch (FondosCombo.SelectedIndex)
            {
                case 0:
                    _ = getFund(id);
                    break;
                case 1:
                    _ = getFunds(managment, depositary, currency, rent);
                    break;
                case 2:
                    _ = searchFunds(id);
                    break;
            }
        }

        private async Task getFund(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                FundsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getFunds(string managment, string depositary, string currency, string rent)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                FundsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task searchFunds(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                FundsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private void mangmentFundsCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (mangmentFundsCombo.DataSource == null)
            {
                _ = setRDMangment();
            }
        }

        private async Task setRDMangment()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Managments values = await _services.getManagements();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    try { index = Int32.Parse(values[i].FundManagerId); }
                    catch { index = i; }
                    keys[index] = values[i].FundManagerName;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                mangmentFundsCombo.DataSource = bindingSource;
                mangmentFundsCombo.DisplayMember = "Value";
                mangmentFundsCombo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void depositaryFundsCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (depositaryFundsCombo.DataSource == null)
            {
                _ = setDepositaryFunds();
            }
        }

        private async Task setDepositaryFunds()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Custodians values = await _services.getCustodians();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    try { index = Int32.Parse(values[i].FundCustodianId); }
                    catch { index = i; }
                    keys[index] = values[i].FundCustodianName;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                depositaryFundsCombo.DataSource = bindingSource;
                depositaryFundsCombo.DisplayMember = "Value";
                depositaryFundsCombo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void currencyFundsCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (currencyFundsCombo.DataSource == null)
            {
                _ = setCurrencys(currencyFundsCombo);
            }
        }

        private void rentFundsCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (rentFundsCombo.DataSource == null)
            {
                _ = setRentFunds();
            }
        }

        private async Task setRentFunds()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Rents values = await _services.getRentTypes();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    try { index = Int32.Parse(values[i].RentTypeId); }
                    catch { index = i; }
                    keys[index] = values[i].RentTypeName;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                rentFundsCombo.DataSource = bindingSource;
                rentFundsCombo.DisplayMember = "Value";
                rentFundsCombo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region Securities
        private void SecuritiesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SecuritiesCombo.SelectedIndex)
            {
                case 0:
                    urlSecurities.Text = Config.Securitie;
                    IdSecuritiesLabel.Visible = true;
                    IdSecuritiesText.Visible = true;
                    break;
                case 1:
                    urlSecurities.Text = Config.Securities;
                    IdSecuritiesLabel.Visible = true;
                    IdSecuritiesText.Visible = true;
                    break;
                default:
                    //
                    break;
            }
        }

        private void SecuritiesBtn_Click(object sender, EventArgs e)
        {
            SecuritiesBtn.Enabled = false;
            SectionsTab.Enabled = false;
            SecuritiesDataView.DataSource = null;
            SecuritiesDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = IdSecuritiesText.Text;

            switch (SecuritiesCombo.SelectedIndex)
            {
                case 0:
                    _ = getSecuritie(id);
                    break;
                case 1:
                    _ = getSecurities(id);
                    break;
                default:
                    SecuritiesBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getSecuritie(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SecuritiesBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getSecurities(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SecuritiesBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        #endregion

        #region Types
        private void TypesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TypesCombo.SelectedIndex)
            {
                case 0:
                    urlTypes.Text = Config.SourceFieldTypes;
                    break;
                case 1:
                    urlTypes.Text = Config.PropertyControlTypes;
                    break;
                case 2:
                    urlTypes.Text = Config.StateControlTypes;
                    break;
                case 3:
                    urlTypes.Text = Config.InstrumentTypes;
                    break;
                case 4:
                    urlTypes.Text = Config.PropertyOriginTypes;
                    break;
                case 5:
                    urlTypes.Text = Config.SourceTypes;
                    break;            
                default:
                    //
                    break;
            }                      
        }

        private void TypesBtn_Click(object sender, EventArgs e)
        {
            TypesBtn.Enabled = false;
            SectionsTab.Enabled = false;
            TypesDataView.DataSource = null;
            TypesDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            _ = getTypes(TypesCombo.SelectedIndex);           
        }

        private async Task getTypes(int index)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
                //Types result;
                //switch (index)
                //{
                //    case 0:
                //        result = await _services.getSourceFieldTypes();
                //        break;
                //    case 1:
                //        result = await _services.getPropertyControlTypes();
                //        break;
                //    case 2:
                //        result = await _services.getStateControlTypes();
                //        break;
                //    case 3:
                //        result = await _services.getInstrumentTypes();
                //        break;
                //    case 4:
                //        result = await _services.getPropertyOriginTypes();
                //        break;
                //    case 5:
                //        result = await _services.getSourceTypes();
                //        break;
                //    default:
                //        TypesBtn.Enabled = true;
                //        SectionsTab.Enabled = true;
                //        throw new Exception("Seleccionar opción");     
                //}

                //string spec = JsonConvert.SerializeObject(result, Formatting.Indented);
                //TypesDataView.AutoGenerateColumns = true;
                //TypesDataView.AutoResizeColumns();
                //TypesDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //TypesDataView.DataSource = result;
                //TypesDataText.Text = spec;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                TypesBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }
        #endregion

        #region Schema        

        private void schemaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SchemaCombo.SelectedIndex)
            {
                case 0:
                    urlSchema.Text = Config.WorkingSchema;
                    SchemaIdLabel.Visible = false;
                    SchemaIdText.Visible = false;                   
                    break;
                case 1:
                    urlSchema.Text = Config.Schemas;
                    SchemaIdLabel.Visible = false;
                    SchemaIdText.Visible = false;
                    break;
                case 2:
                    urlSchema.Text = Config.SchemasId;
                    SchemaIdLabel.Visible = true;
                    SchemaIdText.Visible = true;
                    break;
                case 3:
                    urlSchema.Text = Config.PromoteSchema;
                    SchemaIdLabel.Visible = false;
                    SchemaIdText.Visible = false;
                    break;               
                default:
                    //
                    break;
            }
        }

        private void SchemaBtn_Click(object sender, EventArgs e)
        {
            SchemaBtn.Enabled = false;
            SectionsTab.Enabled = false;
            SchemaDataView.DataSource = null;
            SchemaDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = SchemaIdText.Text;

            switch (SchemaCombo.SelectedIndex)
            {
                case 0:
                    _ = getWorkingSchema();
                    break;
                case 1:
                    _ = getSchemas();
                    break;
                case 2:
                    _ = getSchemasId(id);
                    break;
                case 3:
                    _ = getPromoteSchema();
                    break;                
                default:
                    SchemaBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getWorkingSchema()
        {
            try
            {
                Schema spec = await _services.getSchema();
                string result = JsonConvert.SerializeObject(spec, Formatting.Indented);

                SchemaDataView.AutoGenerateColumns = true;
                SchemaDataView.AutoResizeColumns();
                SchemaDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                SchemaDataView.DataSource = spec;
                SchemaDataText.Text = result;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SchemaBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getSchemas()
        {
            try
            {
                Schemas spec = await _services.getSchemas();
                string result = JsonConvert.SerializeObject(spec, Formatting.Indented);

                SchemaDataView.AutoGenerateColumns = true;
                SchemaDataView.AutoResizeColumns();
                SchemaDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                SchemaDataView.DataSource = spec;
                SchemaDataText.Text = result;
                cantLabel.Text = spec.Count.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SchemaBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getPromoteSchema()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SchemaBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getSchemasId(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SchemaBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        #endregion

        #region Fields        

        private void FieldsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FieldsCombo.SelectedIndex)
            {
                case 0:
                    urlFields.Text = Config.Field;
                    FieldsIdLabel.Visible = true;
                    FieldsIdText.Visible = true;
                    break;
                case 1:
                    urlFields.Text = Config.Fields;
                    FieldsIdLabel.Visible = false;
                    FieldsIdText.Visible = false;
                    break;
                default:
                    //
                    break;
            }
        }

        private void FieldsBtn_Click(object sender, EventArgs e)
        {
            FieldsBtn.Enabled = false;
            SectionsTab.Enabled = false;
            FieldsDataView.DataSource = null;
            FieldsDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = FieldsIdText.Text;

            switch (FieldsCombo.SelectedIndex)
            {
                case 0:
                    _ = getField(id);
                    break;
                case 1:
                    _ = getFields();
                    break;
                default:
                    FieldsBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getField(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                FieldsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getFields()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                FieldsBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        #endregion

        #region Mapping       
        private void MappingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (MappingCombo.SelectedIndex)
            {
                case 0:
                    urlMapping.Text = Config.Mapping;
                    MappingIdLabel.Visible = true;
                    MappingIdText.Visible = true;
                    break;
                case 1:
                    urlMapping.Text = Config.Mappings;
                    MappingIdLabel.Visible = false;
                    MappingIdText.Visible = false;
                    break;
                default:
                    //
                    break;
            }
        }

        private void MappingBtn_Click(object sender, EventArgs e)
        {
            MappingBtn.Enabled = false;
            SectionsTab.Enabled = false;
            MappingDataView.DataSource = null;
            MappingDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = MappingIdText.Text;

            switch (MappingCombo.SelectedIndex)
            {
                case 0:
                    _ = getMapping(id);
                    break;
                case 1:
                    _ = getMappings();
                    break;
                default:
                    MappingBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getMapping(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                MappingBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getMappings()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                MappingBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        #endregion

        #region SourceFields       
        private void SFCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SFCombo.SelectedIndex)
            {
                case 0:
                    urlSF.Text = Config.SourceField;
                    SFIdLabel.Visible = true;
                    SFIdText.Visible = true;
                    break;
                case 1:
                    urlSF.Text = Config.SourceFields;
                    SFIdLabel.Visible = false;
                    SFIdText.Visible = false;
                    break;
                default:
                    //
                    break;
            }
        }

        private void SFBtn_Click(object sender, EventArgs e)
        {
            SFBtn.Enabled = false;
            SectionsTab.Enabled = false;
            SFDataView.DataSource = null;
            SFDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = SFIdText.Text;

            switch (SFCombo.SelectedIndex)
            {
                case 0:
                    _ = getSourceField(id);
                    break;
                case 1:
                    _ = getSourceFields();
                    break;
                default:
                    SFBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getSourceField(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SFBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task getSourceFields()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                SFBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }
        #endregion

        #region Derivatives               

        private void enabledDerivativesGet()
        {
            marketDerivLabel.Visible = true;
            symbolDerivLabel.Visible = true;
            IdDerivLabel.Visible = false;
            marketDerivativesCombo.Visible = true;
            symbolDerivativesCombo.Visible = true;
            IdDerivTxt.Visible = false;
        }

        private void enabledDerivativesSearch()
        {
            marketDerivLabel.Visible = false;
            symbolDerivLabel.Visible = false;
            IdDerivLabel.Visible = true;
            marketDerivativesCombo.Visible = false;
            symbolDerivativesCombo.Visible = false;
            IdDerivTxt.Visible = true;
        }
        private void DerivativesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DerivativesCombo.SelectedIndex)
            {
                case 0:
                    urlDerivatives.Text = Config.Derivatives;
                    enabledDerivativesGet();
                    break;
                case 1:
                    urlDerivatives.Text = Config.Derivatives;
                    enabledDerivativesSearch();
                    break;
                default:
                    urlDerivatives.Text = Config.Derivatives;
                    break;
            }
        }

        private void DerivativesBtn_Click(object sender, EventArgs e)
        {
            DerivativesBtn.Enabled = false;
            SectionsTab.Enabled = false;
            DerivativesDataView.DataSource = null;
            DerivativesDataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var id = IdDerivTxt.Text;

            var market = (marketDerivativesCombo.SelectedValue == null || marketDerivativesCombo.Text == _all) ? null
               : marketDerivativesCombo.SelectedValue.ToString();

            var symbol = (symbolDerivativesCombo.SelectedValue == null || symbolDerivativesCombo.Text == _all) ? null
                : symbolDerivativesCombo.SelectedValue.ToString();

            switch (DerivativesCombo.SelectedIndex)
            {
                case 0:
                    _ = getDerivatives(market, symbol);
                    break;
                case 1:
                    _ = searchDerivatives(id);
                    break;
                default:
                    DerivativesBtn.Enabled = true;
                    SectionsTab.Enabled = true;
                    break;
            }
        }

        private async Task getDerivatives(string market, string symbol)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                DerivativesBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private async Task searchDerivatives(string id)
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                DerivativesBtn.Enabled = true;
                SectionsTab.Enabled = true;
            }
        }

        private void marketDerivativesCombo_Click(object sender, EventArgs e)
        {
            if (marketDerivativesCombo.DataSource == null)
            {
                _ = setmarketDerivatives();
            }
        }

        private async Task setmarketDerivatives()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void symbolDerivativesCombo_Click(object sender, EventArgs e)
        {
            if (symbolDerivativesCombo.DataSource == null)
            {
                _ = setSymbolDerivatives();
            }
        }

        private async Task setSymbolDerivatives()
        {
            try
            {
                MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region OData  

        private void disableODataAll()
        {
            IdODataLabel.Visible = false;
            TypeODataLabel.Visible = false;

            CurrencyODataLabel.Visible = false;
            SymbolODataLabel.Visible = false;
            MarketODataLabel.Visible = false;
            CountryODataLabel.Visible = false;
            QueryODataLabel.Visible = false;

            IdODataTxt.Visible = false;
            typeODataCombo.Visible = false;
            currencyODataCombo.Visible = false;
            symbolODataCombo.Visible = false;
            marketODataCombo.Visible = false;
            countryODataCombo.Visible = false;
            QueryODataTxt.Visible = false;
        }

        private void enabledODataGet()
        {
            IdODataLabel.Visible = false;
            TypeODataLabel.Visible = false;
            CurrencyODataLabel.Visible = false;
            SymbolODataLabel.Visible = false;
            MarketODataLabel.Visible = false;
            CountryODataLabel.Visible = false;
            QueryODataLabel.Visible = true;

            IdODataTxt.Visible = false;
            typeODataCombo.Visible = false;
            currencyODataCombo.Visible = false;
            symbolODataCombo.Visible = false;
            marketODataCombo.Visible = false;
            countryODataCombo.Visible = false;
            QueryODataTxt.Visible = true;
        }

        private void enabledODataGetId()
        {
            IdODataLabel.Visible = true;
            TypeODataLabel.Visible = false;
            CurrencyODataLabel.Visible = false;
            SymbolODataLabel.Visible = false;
            MarketODataLabel.Visible = false;
            CountryODataLabel.Visible = false;
            QueryODataLabel.Visible = false;

            IdODataTxt.Visible = true;
            typeODataCombo.Visible = false;
            currencyODataCombo.Visible = false;
            symbolODataCombo.Visible = false;
            marketODataCombo.Visible = false;
            countryODataCombo.Visible = false;
            QueryODataTxt.Visible = false;
        }

        private void enabledODataSearch()
        {
            IdODataLabel.Visible = false;
            TypeODataLabel.Visible = true;
            CurrencyODataLabel.Visible = true;
            SymbolODataLabel.Visible = true;
            MarketODataLabel.Visible = true;
            CountryODataLabel.Visible = true;
            QueryODataLabel.Visible = false;

            IdODataTxt.Visible = false;
            typeODataCombo.Visible = true;
            currencyODataCombo.Visible = true;
            symbolODataCombo.Visible = true;
            marketODataCombo.Visible = true;
            countryODataCombo.Visible = true;
            QueryODataTxt.Visible = false;
        }

        private void ODataCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ODataCombo.SelectedIndex)
            {
                case 0:
                    //Retorna la lista de instrumentos financieros filtrados con Query en formato OData
                    urlOData.Text = Config.OData;
                    enabledODataGet();
                    break;
                case 1:
                    //Retorna la lista de instrumentos financieros filtrados por Id
                    urlOData.Text = Config.OData;
                    enabledODataGetId();
                    break;
                case 2:
                    //Retorna la lista de instrumentos financieros filtrados por campos específicos
                    urlOData.Text = Config.OData;
                    enabledODataSearch();
                    break;
                case 3:
                    //Retorna la lista de Sociedades Depositarias o Custodia de Fondos
                    urlOData.Text = Config.OData + Config.Depositary;
                    disableODataAll();
                    break;
                case 4:
                    //Retorna la lista de Sociedades Administradoras de Fondos
                    urlOData.Text = Config.OData + Config.Managment;
                    disableODataAll();
                    break;
                case 5:
                    //Retorna la lista de Tipos de Rentas
                    urlOData.Text = Config.OData + Config.RentType;
                    disableODataAll();
                    break;
                case 6:
                    //Retorna la lista de Regiones
                    urlOData.Text = Config.OData + Config.Region;
                    disableODataAll();
                    break;
                case 7:
                    //Retorna la lista de Monedas
                    urlOData.Text = Config.OData + Config.Currency;
                    disableODataAll();
                    break;
                case 8:
                    //Retorna la lista de Países
                    urlOData.Text = Config.OData + Config.Country;
                    disableODataAll();
                    break;
                case 9:
                    //Retorna la lista de Issuers
                    urlOData.Text = Config.OData + Config.Issuer;
                    disableODataAll();
                    break;
                case 10:
                    //Retorna la lista de Horizons
                    urlOData.Text = Config.OData + Config.Horizon;
                    disableODataAll();
                    break;
                case 11:
                    //Retorna la lista de Tipos de Fondos
                    urlOData.Text = Config.OData + Config.FundType;
                    disableODataAll();
                    break;
                case 12:
                    //Retorna la lista de Benchmarks
                    urlOData.Text = Config.OData + Config.Benchmark;
                    disableODataAll();
                    break;
                case 13:
                    //Retorna la lista de Benchmarks
                    urlOData.Text = Config.OData + Config.RDTypes;
                    disableODataAll();
                    break;
                case 14:
                    //Retorna la lista de Benchmarks
                    urlOData.Text = Config.OData + Config.Markets;
                    disableODataAll();
                    break;
                default:
                    //
                    break;
            }
        }
        
        private void ODataBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ODataBtn.Enabled = false;
            SectionsTab.Enabled = false;
            ODataView.DataSource = null;
            ODataText.Text = String.Empty;
            cantLabel.Text = String.Empty;            
            _ = sendOption();
        }

        private async Task sendOption()
        {
            var id = IdODataTxt.Text;

            var type = (typeODataCombo.SelectedValue == null || typeODataCombo.Text == _all) ? null 
                : typeODataCombo.SelectedValue.ToString();

            var currency = (currencyODataCombo.SelectedValue == null || currencyODataCombo.Text == _all) ? null 
                : currencyODataCombo.SelectedValue.ToString();

            var symbol = (symbolODataCombo.SelectedValue == null || symbolODataCombo.Text == _all) ? null
                : symbolODataCombo.SelectedValue.ToString();

            var market = (marketODataCombo.SelectedValue == null || marketODataCombo.Text == _all) ? null 
                : marketODataCombo.SelectedValue.ToString();

            var country = (countryODataCombo.SelectedValue == null || countryODataCombo.Text == _all) ? null 
                : countryODataCombo.SelectedValue.ToString();

            var query = (QueryODataTxt.Text == String.Empty) ? null 
                : QueryODataTxt.Text;

            try
            {
                switch (ODataCombo.SelectedIndex)
                {
                    case 0:
                        MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
                        //ODataObject getReferenceDatas = await _services.getODataReferenceDatas(query);
                        //_showData(getReferenceDatas);
                        break;
                    case 1:
                        MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
                        //if (id == String.Empty)
                        //{
                        //    throw new Exception("Valor Id requerido");
                        //}
                        //_showData(await _services.getODataReferenceDatasById(id));
                        break;
                    case 2:
                        MessageBox.Show("Opcion no disponible en esta versión", "Info:", MessageBoxButtons.OK);
                        //_showData(await _services.searchODataReferenceDatas(type, currency, symbol, market, country));
                        break;
                    case 3:
                        _showData(await _services.getCustodians());
                        break;
                    case 4:
                        _showData(await _services.getManagements());
                        break;
                    case 5:
                        _showData(await _services.getRentTypes());
                        break;
                    case 6:
                        _showData(await _services.getRegions());
                        break;
                    case 7:
                        _showData(await _services.getCurrencys());
                        break;
                    case 8:
                        _showData(await _services.getCountrys());
                        break;
                    case 9:
                        _showData(await _services.getIssuers());
                        break;
                    case 10:
                        _showData(await _services.getHorizons());
                        break;
                    case 11:
                        _showData(await _services.getFundTypes());
                        break;
                    case 12:
                        _showData(await _services.getBenchmarks());
                        break;
                    case 13:
                        _showData(await _services.getReferenceDataTypes());
                        break;
                    case 14:
                        _showData(await _services.getMarkets());
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ODataBtn.Enabled = true;
                SectionsTab.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void _showData(dynamic spec)
        {
            string result = JsonConvert.SerializeObject(spec, Formatting.Indented);
            ODataView.AutoGenerateColumns = true;
            ODataView.AutoResizeColumns();
            ODataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ODataView.DataSource = spec;
            ODataText.Text = result;
            cantLabel.Text = spec.Count.ToString() + " records found.";
        }

        private void typeODataCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (typeODataCombo.DataSource == null)
            {
                _ = setRDTypes(typeODataCombo);
            }
        }

        private void symbolODataCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (symbolODataCombo.DataSource == null)
            {
                _ = setRDSymbols(symbolODataCombo);
            }
        }

        private async Task setRDSymbols(MetroComboBox combo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ReferenceDataSymbols values = await _services.getReferenceDataSymbols();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i].UnderlyingSymbol;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                combo.DataSource = bindingSource;
                combo.DisplayMember = "Value";
                combo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async Task setRDTypes(MetroComboBox combo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ReferenceDataTypes values = await _services.getReferenceDataTypes();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i].type;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                combo.DataSource = bindingSource;
                combo.DisplayMember = "Value";
                combo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void currencyODataCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (currencyODataCombo.DataSource == null)
            {
                _ = setCurrencys(currencyODataCombo);
            }
        }

        private async Task setCurrencys(MetroComboBox combo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Currencys values = await _services.getCurrencys();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i].Currency;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                combo.DataSource = bindingSource;
                combo.DisplayMember = "Value";
                combo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void marketODataCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (marketODataCombo.DataSource == null)
            {
                _ = setRDMarket();
            }
        }

        private async Task setRDMarket()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Markets values = await _services.getMarkets();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i].MarketId;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                marketODataCombo.DataSource = bindingSource;
                marketODataCombo.DisplayMember = "Value";
                marketODataCombo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void countryODataCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (countryODataCombo.DataSource == null)
            {
                _ = setRDCountry();
            }
        }

        private async Task setRDCountry()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Countrys values = await _services.getCountrys();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i].Country;
                }
                keys[keys.Keys.LastOrDefault() + 1] = _all;

                BindingSource bindingSource = new BindingSource(keys, null);
                countryODataCombo.DataSource = bindingSource;
                countryODataCombo.DisplayMember = "Value";
                countryODataCombo.ValueMember = "Value";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion
    }
}
