using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Apointment_Scheduler.Forms
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
            lblHeader.Text = "Add Customer";
            update = false;
        }

        int customerId;
        int addressId;
        int cityId;
        int countryId;
        bool update;

        public CustomerForm(DataGridViewRow selectedRow)
        {
            InitializeComponent();
            lblHeader.Text = "Update Customer";
            update = true;

            //fill in the boxes
            int.TryParse(selectedRow.Cells["customerId"].Value.ToString(), out customerId);
            int.TryParse(selectedRow.Cells["addressId"].Value.ToString(), out addressId);
            int.TryParse(selectedRow.Cells["cityId"].Value.ToString(), out cityId);
            int.TryParse(selectedRow.Cells["countryId"].Value.ToString(), out countryId);

            txtName.Text = selectedRow.Cells["customerName"].Value.ToString();
            txtAddress.Text = selectedRow.Cells["address"].Value.ToString();
            txtCity.Text = selectedRow.Cells["city"].Value.ToString();
            txtZip.Text = selectedRow.Cells["postalCode"].Value.ToString();
            txtCountry.Text = selectedRow.Cells["country"].Value.ToString();
            txtPhone.Text = selectedRow.Cells["phone"].Value.ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Requirement 2A: Validate customer records
            if (!AreAllFieldsValid())
            {
                MessageBox.Show("Please fill out all form fields correctly");
                return;
            }
            var customerData = new Dictionary<string, string>
            {
                { "CustomerName", txtName.Text },
                { "CustomerAddress", txtAddress.Text },
                { "CustomerCity", txtCity.Text },
                { "CustomerCountry", txtCountry.Text },
                { "CustomerPhone", txtPhone.Text },
                { "CustomerPostal", txtZip.Text },
                { "AddressId", addressId.ToString() },
                { "CustomerId", customerId.ToString() },
                { "CityId", cityId.ToString() },
                { "CountryId", countryId.ToString() }
            };

            Data.SaveCustomer(customerData, update);
            this.Close();
        }


        private bool AreAllFieldsValid()
        {
            //Requirement 2A: Validation
            return Data.ValidateTextBox(txtName, "string", errorProvider)
                && Data.ValidateTextBox(txtAddress, "string", errorProvider)
                && Data.ValidateTextBox(txtCity, "string", errorProvider)
                && Data.ValidateTextBox(txtCountry, "string", errorProvider)
                && Data.ValidateTextBox(txtPhone, "phone", errorProvider)
                && Data.ValidateTextBox(txtZip, "string", errorProvider);
        }
    }
}
