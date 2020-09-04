using ESCO.Reference.Data.Model;
using ESCO.Reference.Data.Services;
using MetroFramework.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESCO.Reference.Data.App
{
    public partial class KeyWin : MetroForm
    {
        public string key { get; set; }
        public ReferenceDataServices services { get; set; }

        public KeyWin(string _key, bool _modal)
        {
            InitializeComponent();
            keyText.Text = key = _key;
            cancelBtn.Visible = !_modal;
            closeBtn.Visible = _modal;
        }

        private void keyBtn_Click(object sender, EventArgs e)
        {
            _ = validateKey();
        }

        private async Task validateKey()
        {
            try
            {
                services = new ReferenceDataServices(keyText.Text);
                Schemas schemas = await services.getSchemas();
                key = keyText.Text;
                Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            key = String.Empty;
            Close();
        }

        private void keyText_TextChanged(object sender, EventArgs e)
        {
            cancelBtn.Enabled = (keyText.Text != String.Empty);
            keyBtn.Enabled = (keyText.Text != String.Empty);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
