using ESCO.Reference.Data.Services;
using ESCO.Reference.Data.Model;
using MetroFramework.Forms;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using MetroFramework.Controls;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using System.Diagnostics;

namespace ESCO.Reference.Data.App
{
    public partial class Init : MetroForm
    {
        private ReferenceDataServices _services;
        private KeyWin keyWin;

        private const string _all = "Todos";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

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
                _services.PaginatedMode();
            }
            if (_services != null) { setSchemas(); }
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

        #region Schemas  
        private void setSchemas()
        {
            try
            {
                Dictionary<int, string> schemasKeys = new Dictionary<int, string>()
                {
                    [0] = "schema-000",
                    [1] = "schema-001"
                };

                BindingSource bindingSource = new BindingSource(schemasKeys, null);
                schemaComboInit.DataSource = bindingSource;
                schemaComboInit.DisplayMember = "Value";
                schemaComboInit.ValueMember = "Value";

                schemaComboInit.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getSchemaActive()
        {
            return ((KeyValuePair<int, string>)schemaComboInit.SelectedItem).Value;
        }
        #endregion

        #region ReferenceDatas

        private void disableRDControls()
        {
            typeRDLabel.Visible = false;
            IdRDLabel.Visible = false;
            typeRDCombo.Visible = false;
            IdRDText.Visible = false;

            CurrencyODataLabel.Visible = false;
            MarketODataLabel.Visible = false;
            CountryODataLabel.Visible = false;

            currencyODataCombo.Visible = false;
            marketODataCombo.Visible = false;
            countryODataCombo.Visible = false;
        }

        private void enabledRDGet()
        {
            typeRDLabel.Visible = true;
            IdRDLabel.Visible = false;
            typeRDCombo.Visible = true;
            IdRDText.Visible = false;

            CurrencyODataLabel.Visible = false;
            MarketODataLabel.Visible = false;
            CountryODataLabel.Visible = false;

            currencyODataCombo.Visible = false;
            marketODataCombo.Visible = false;
            countryODataCombo.Visible = false;
        }

        private void enabledRDSearch()
        {
            typeRDLabel.Visible = true;
            IdRDLabel.Visible = true;
            typeRDCombo.Visible = true;
            IdRDText.Visible = true;

            CurrencyODataLabel.Visible = true;
            MarketODataLabel.Visible = true;
            CountryODataLabel.Visible = true;

            currencyODataCombo.Visible = true;
            marketODataCombo.Visible = true;
            countryODataCombo.Visible = true;
        }

        private void enabledSearchId()
        {
            typeRDLabel.Visible = false;
            IdRDLabel.Visible = true;
            typeRDCombo.Visible = false;
            IdRDText.Visible = true;

            CurrencyODataLabel.Visible = false;
            MarketODataLabel.Visible = false;
            CountryODataLabel.Visible = false;

            currencyODataCombo.Visible = false;
            marketODataCombo.Visible = false;
            countryODataCombo.Visible = false;
        }

        private void RefereceDataCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RefereceDataCombo.SelectedIndex)
            {
                case 0:
                    urlReferenceData.Text = Config.ReferenceData + Config.FilterTypeStr;
                    enabledRDGet();
                    break;
                case 1:
                    urlReferenceData.Text = Config.ReferenceData + Config.FilterUpdated;
                    enabledRDGet();
                    break;
                case 2:
                    urlReferenceData.Text = Config.ReferenceData + Config.FilterAdded;
                    enabledRDGet();
                    break;
                case 3:
                    urlReferenceData.Text = Config.ReferenceData + Config.FilterRemoved;
                    enabledRDGet();
                    break;
                case 4:
                    urlReferenceData.Text = Config.ReferenceData + Config.FilterSearch;
                    enabledRDSearch();
                    break;
                case 5:
                    urlReferenceData.Text = Config.ReferenceData + Config.FilterId;
                    enabledSearchId();
                    break;
                case 6:
                    urlReferenceData.Text = Config.Specification;
                    disableRDControls();
                    break;
                default:
                    //
                    break;
            }
        }

        private void ReferenceDataSendBtn_Click(object sender, EventArgs e)
        {
            ReferenceDataSendBtn.Enabled = false;
            ReportsTab.Enabled = false;
            ReferenceDataView.DataSource = null;
            ReferenceDataTextBox.Text = String.Empty;
            cantLabel.Text = String.Empty;
            Cursor.Current = Cursors.WaitCursor;

            var schema = getSchemaActive();
            var type = (typeRDCombo.SelectedValue == null || typeRDCombo.Text == _all) ? null
                : typeRDCombo.SelectedValue.ToString();
            var id = (IdRDText.Text == null || IdRDText.Text == String.Empty) ? null
                : IdRDText.Text;
            var currency = (currencyODataCombo.SelectedValue == null || currencyODataCombo.Text == _all) ? null
                : currencyODataCombo.SelectedValue.ToString();
            var market = (marketODataCombo.SelectedValue == null || marketODataCombo.Text == _all) ? null
                : marketODataCombo.SelectedValue.ToString();
            var country = (countryODataCombo.SelectedValue == null || countryODataCombo.Text == _all) ? null
                : countryODataCombo.SelectedValue.ToString();

            _ = SelectOption(type, id, currency, market, country, schema);
        }

        private async Task SelectOption(string type, string id, string currency, string market, string country, string schema)
        {
            switch (RefereceDataCombo.SelectedIndex)
            {
                case 0:
                    getReferenceDatas(await _services.GetReferenceData(type, schema));
                    break;
                case 1:
                    getReferenceDatas(await _services.GetReferenceDataTodayUpdated(type, schema));
                    break;
                case 2:
                    getReferenceDatas(await _services.GetReferenceDataTodayAdded(type, schema));
                    break;
                case 3:
                    getReferenceDatas(await _services.GetReferenceDataTodayRemoved(type, schema));
                    break;
                case 4:
                    getReferenceDatas(await _services.SearchReferenceData(type, id, currency, market, country, schema));
                    break;
                case 5:
                    getReferenceDatas(await _services.SearchReferenceDataById(id, schema));
                    break;
                case 6:
                    _ = getReferenceDataSpecification();
                    break;
                default:
                    break;
            }
        }

        private void getReferenceDatas(ReferenceDatas rd)
        {
            try
            {
                string result = JsonSerializer.Serialize(rd, options);

                ReferenceDataView.AutoGenerateColumns = true;
                ReferenceDataView.AutoResizeColumns();
                ReferenceDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReferenceDataView.DataSource = rd.data;
                ReferenceDataTextBox.Text = result;
                cantLabel.Text = rd.totalCount.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                ReportsTab.Enabled = true;
            }
        }

        private async Task getReferenceDataSpecification()
        {
            try
            {
                Specification result = await _services.GetReferenceDataSpecification(getSchemaActive());
                ReferenceDataTextBox.Text = JsonSerializer.Serialize(result, options);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReferenceDataSendBtn.Enabled = true;
                ReportsTab.Enabled = true;
            }
        }

        #endregion

        #region OData  
        private void disableODataAll()
        {
            QueryODataLabel.Visible = false;
            QueryODataTxt.Visible = false;
        }

        private void enabledODataGet()
        {
            QueryODataLabel.Visible = true;
            QueryODataTxt.Visible = true;
            QueryODataTxt.Text = "?$filter=type eq 'FUT'";
        }

        public async void _saveFile(Stream stream)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "Save As",
                FileName = "report.zip",
                DefaultExt = ".zip",
                Filter = "(*.zip)|*.zip",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            
            var result = saveFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (var fileStream = File.Create(saveFile.FileName))
                {
                    await stream.CopyToAsync(fileStream);
                    Process.Start(saveFile.FileName);
                }
            }
            ODataText.Text = new StreamReader(stream).ReadToEnd();
        }

        private void ODataCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ODataCombo.SelectedIndex)
            {
                case 0:
                    //Retorna la lista de instrumentos financieros filtrados con Query en formato OData
                    urlOData.Text = Config.ReferenceData;
                    enabledODataGet();
                    break;
                case 1:
                    //Retorna la lista de instrumentos financieros consolidados filtrados con Query en formato OData.
                    urlOData.Text = Config.ReferenceData;
                    enabledODataGet();
                    break;
                case 2:
                    //Retorna la lista de instrumentos financieros filtrados en un CSV con Query en formato OData.
                    urlOData.Text = Config.ReferenceData;
                    enabledODataGet();
                    break;
                case 3:
                    //Retorna la lista de Sociedades Depositarias o Custodia de Fondos
                    urlOData.Text = Config.ReferenceData + Config.Custodian;
                    disableODataAll();
                    break;
                case 4:
                    //Retorna la lista de Sociedades Administradoras de Fondos
                    urlOData.Text = Config.ReferenceData + Config.Managment;
                    disableODataAll();
                    break;
                case 5:
                    //Retorna la lista de Tipos de Rentas
                    urlOData.Text = Config.ReferenceData + Config.RentType;
                    disableODataAll();
                    break;
                case 6:
                    //Retorna la lista de Regiones
                    urlOData.Text = Config.ReferenceData + Config.Region;
                    disableODataAll();
                    break;
                case 7:
                    //Retorna la lista de Monedas
                    urlOData.Text = Config.ReferenceData + Config.Currency;
                    disableODataAll();
                    break;
                case 8:
                    //Retorna la lista de Países
                    urlOData.Text = Config.ReferenceData + Config.Country;
                    disableODataAll();
                    break;
                case 9:
                    //Retorna la lista de Issuers
                    urlOData.Text = Config.ReferenceData + Config.Issuer;
                    disableODataAll();
                    break;
                case 10:
                    //Retorna la lista de Horizons
                    urlOData.Text = Config.ReferenceData + Config.Horizon;
                    disableODataAll();
                    break;
                case 11:
                    //Retorna la lista de Tipos de Fondos
                    urlOData.Text = Config.ReferenceData + Config.FundType;
                    disableODataAll();
                    break;
                case 12:
                    //Retorna la lista de Benchmarks
                    urlOData.Text = Config.ReferenceData + Config.Benchmark;
                    disableODataAll();
                    break;
                case 13:
                    //Retorna la lista de Tipos de Instrumentos financieros
                    urlOData.Text = Config.ReferenceData;
                    disableODataAll();
                    break;
                case 14:
                    //Retorna la lista de Mercados para los Instrumentos financieros
                    urlOData.Text = Config.ReferenceData + Config.Markets;
                    disableODataAll();
                    break;
                default:
                    break;
            }
        }

        private void ODataBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ODataBtn.Enabled = false;
            ReportsTab.Enabled = false;
            ODataView.DataSource = null;
            ODataText.Text = String.Empty;
            cantLabel.Text = String.Empty;
            _ = SendOption();
        }

        private async Task SendOption()
        {
            var schema = getSchemaActive();
            var query = (QueryODataTxt.Text == String.Empty) ? null : QueryODataTxt.Text;
            try
            {
                switch (ODataCombo.SelectedIndex)
                {
                    case 0:
                        ReferenceDatas rdbyodata = await _services.GetReferenceDataByOData(query, schema);
                        _showData(rdbyodata.data);
                        break;
                    case 1:
                        ReferenceDatas consolidate = await _services.GetConsolidatedByOData(query, schema);
                        _showData(consolidate.data);
                        break;
                    case 2:
                        ODataView.DataSource = null;
                        Stream stream = await _services.GetCSVByOData(query, schema); 
                        _saveFile(stream);                        
                        break;
                    case 3:
                        _showData(await _services.GetCustodians(schema));
                        break;
                    case 4:
                        _showData(await _services.GetManagements(schema));
                        break;
                    case 5:
                        _showData(await _services.GetRentTypes(schema));
                        break;
                    case 6:
                        _showData(await _services.GetRegions(schema));
                        break;
                    case 7:
                        _showData(await _services.GetCurrencys(schema));
                        break;
                    case 8:
                        _showData(await _services.GetCountrys(schema));
                        break;
                    case 9:
                        _showData(await _services.GetIssuers(schema));
                        break;
                    case 10:
                        _showData(await _services.GetHorizons(schema));
                        break;
                    case 11:
                        _showData(await _services.GetFundTypes(schema));
                        break;
                    case 12:
                        _showData(await _services.GetBenchmarks(schema));
                        break;
                    case 13:
                        _showData(_services.GetReferenceDataTypes(schema));
                        break;
                    case 14:
                        _showData(await _services.GetMarkets(schema));
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
                ReportsTab.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void _showData(object spec)
        {
            string result = JsonSerializer.Serialize(spec, options);
            ODataView.AutoGenerateColumns = true;
            ODataView.AutoResizeColumns();
            ODataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ODataView.DataSource = spec;
            ODataText.Text = result;
        }

        private void typeRDCombo_MouseClick(object sender, MouseEventArgs e)
        {
            if (typeRDCombo.DataSource == null)
            {
                SetRDTypes(typeRDCombo);
            }
        }

        private void SetRDTypes(MetroComboBox combo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ReferenceDataTypes values = _services.GetReferenceDataTypes();

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

        private void currencyODataCombo_MouseClick(object sender, EventArgs e)
        {
            if (currencyODataCombo.DataSource == null)
            {
                _ = SetCurrencys(currencyODataCombo);
            }
        }

        private async Task SetCurrencys(MetroComboBox combo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Currencys values = await _services.GetCurrencys();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i];
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

        private void marketODataCombo_Click(object sender, EventArgs e)
        {
            if (marketODataCombo.DataSource == null)
            {
                _ = SetRDMarket();
            }
        }

        private async Task SetRDMarket()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Markets values = await _services.GetMarkets();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i];
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

        private void countryODataCombo_Click(object sender, EventArgs e)
        {
            if (countryODataCombo.DataSource == null)
            {
                _ = SetRDCountry();
            }
        }

        private async Task SetRDCountry()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Countrys values = await _services.GetCountrys();

                Dictionary<int, string> keys = new Dictionary<int, string>();
                for (int i = 0; i < values.Count; i++)
                {
                    int index = i;
                    keys[index] = values[i];
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

        #region Reports       

        private void ReportsSendBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ReportsSendBtn.Enabled = false;
            ReportsTab.Enabled = false;
            ReportsDataView.DataSource = null;
            ReportsTextBox.Text = String.Empty;
            cantLabel.Text = String.Empty;
            _ = SelectReport();
        }

        private async Task SelectReport()
        {
            string schema = getSchemaActive();
            switch (ReportsCombo.SelectedIndex)
            {                
                case 0:
                    Reports reports = await _services.GetFieldsReports(schema);
                    getFields(reports.fields);
                    break;
                case 1:
                    Reports fields = await _services.GetFieldsReports(schema);
                    getFields(fields.fields);
                    break;
                case 2:
                    Mappings mapping = await _services.GetMapping(schema);
                    getFields(mapping.fieldsMapping);
                    break;
                case 3:
                    Fondos fondos = await _services.GetFondos(schema);
                    getReports(fondos.data, fondos.totalCount, JsonSerializer.Serialize(fondos.data, options));
                    break;
                case 4:
                    Cedears cd = await _services.GetCedears(schema);
                    getReports(cd.data, cd.totalCount, JsonSerializer.Serialize(cd.data, options));
                    break;
                case 5:
                    Acciones acc = await _services.GetAcciones(schema);
                    getReports(acc.data, acc.totalCount, JsonSerializer.Serialize(acc.data, options));
                    break;
                case 6:
                    Acciones adrs = await _services.GetAccionesADRS(schema);
                    getReports(adrs.data, adrs.totalCount, JsonSerializer.Serialize(adrs.data, options));
                    break;
                case 7:
                    Acciones priv = await _services.GetAccionesPrivadas(schema);
                    getReports(priv.data, priv.totalCount, JsonSerializer.Serialize(priv.data, options));
                    break;
                case 8:
                    Acciones pymes = await _services.GetAccionesPYMES(schema);
                    getReports(pymes.data, pymes.totalCount, JsonSerializer.Serialize(pymes.data, options));
                    break;
                case 9:
                    Obligaciones oblg = await _services.GetObligaciones(schema);
                    getReports(oblg.data, oblg.totalCount, JsonSerializer.Serialize(oblg.data, options));
                    break;
                case 10:
                    Titulos titulos = await _services.GetTitulos(schema);
                    getReports(titulos.data, titulos.totalCount, JsonSerializer.Serialize(titulos.data, options));
                    break;
                case 11:
                    Futuros futuros = await _services.GetFuturos(schema);
                    getReports(futuros.data, futuros.totalCount, JsonSerializer.Serialize(futuros.data, options));
                    break;
                case 12:
                    ReferenceDatas opts = await _services.GetOpciones(schema);
                    getReports(opts.data, opts.totalCount, JsonSerializer.Serialize(opts.data, options));
                    break;
                case 13:
                    Pases pases = await _services.GetPases(schema);
                    getReports(pases.data, pases.totalCount, JsonSerializer.Serialize(pases.data, options));
                    break;
                case 14:
                    Cauciones caucc = await _services.GetCauciones(schema);
                    getReports(caucc.data, caucc.totalCount, JsonSerializer.Serialize(caucc.data, options));
                    break;
                case 15:
                    Plazos plazos = await _services.GetPlazos(schema);
                    getReports(plazos.data, plazos.totalCount, JsonSerializer.Serialize(plazos.data, options));
                    break;
                case 16:
                    Prestamos prestamos = await _services.GetPrestamosValores(schema);
                    getReports(prestamos.data, prestamos.totalCount, JsonSerializer.Serialize(prestamos.data, options));
                    break;
                case 17:
                    Indices indices = await _services.GetIndices(schema);
                    getReports(indices.data, indices.totalCount, JsonSerializer.Serialize(indices.data, options));
                    break;
                default:
                    break;
            }
        }

        private void getFields(object result)
        {
            try
            {
                ReportsDataView.AutoGenerateColumns = true;
                ReportsDataView.AutoResizeColumns();
                ReportsDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReportsDataView.DataSource = result;
                ReportsTextBox.Text = JsonSerializer.Serialize(result, options);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReportsSendBtn.Enabled = true;
                ReportsTab.Enabled = true;
            }
        }

        private void getReports(object rd, int? totalCount, string result)
        {
            try
            {
                ReportsDataView.AutoGenerateColumns = true;
                ReportsDataView.AutoResizeColumns();
                ReportsDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ReportsDataView.DataSource = rd;
                ReportsTextBox.Text = result;
                cantLabel.Text = totalCount.ToString() + " records found.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ReportsSendBtn.Enabled = true;
                ReportsTab.Enabled = true;
            }
        }

        private void ReportsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ReportsCombo.SelectedIndex)
            {
                case 0:
                    //Devuelve la lista completa de campos para los reportes
                    urlReports.Text = Config.FieldsReports;
                    break;
                case 1:
                    //Devuelve la lista completa de campos
                    urlReports.Text = Config.Fields;
                    break;
                case 2:
                    //Devuelve el mapping que tiene un schema
                    urlReports.Text = Config.Mapping;
                    break;
                case 3:
                    //Retorna la lista de instrumentos financieros de tipo Fondos Comunes de Inversion (MF)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Fondos);
                    break;
                case 4:
                    //Retorna la lista de instrumentos financieros de tipo Cedears (CD)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Cedears);
                    break;
                case 5:
                    //Retorna la lista de instrumentos financieros de tipo Acciones (CS)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Acciones);
                    break;
                case 6:
                    //Retorna la lista de instrumentos financieros de tipo Acciones A.D.R.S.
                    urlReports.Text = Config.ReferenceData + Config.FilterADRS;
                    break;
                case 7:
                    //Retorna la lista de instrumentos financieros de tipo Acciones Privadas
                    urlReports.Text = Config.ReferenceData + Config.FilterPrivadas;
                    break;
                case 8:
                    //Retorna la lista de instrumentos financieros de tipo Acciones PYMES
                    urlReports.Text = Config.ReferenceData + Config.FilterPymes;
                    break;
                case 9:
                    //Retorna la lista de instrumentos financieros de tipo Obligaciones (CORP)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Obligaciones);
                    break;
                case 10:
                    //Retorna la lista de instrumentos financieros de tipo Títulos Públicos (GO)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Titulos);
                    break;
                case 11:
                    //Retorna la lista de instrumentos financieros de tipo Futuros (FUT)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Futuros);
                    break;
                case 12:
                    //Retorna la lista de instrumentos financieros de tipo Opciones (OPT-OOF)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Opciones);
                    break;
                case 13:
                    //Retorna la lista de instrumentos financieros de tipo Pases (BUYSELL)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Pases);
                    break;
                case 14:
                    //Retorna la lista de instrumentos financieros de tipo Cauciones (REPO)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Cauciones);
                    break;
                case 15:
                    //Retorna la lista de instrumentos financieros de tipo Plazos por Lotes
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.T);
                    break;
                case 16:
                    //Retorna la lista de instrumentos financieros de tipo Préstamos de Valores
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.TERM);
                    break;
                case 17:
                    //Retorna la lista de instrumentos financieros de tipo Indice (XLINKD)
                    urlReports.Text = Config.ReferenceData + Config.SetUrl(Types.Indices);
                    break;
                default:
                    break;
            }
        }

        #endregion        
    }
}
