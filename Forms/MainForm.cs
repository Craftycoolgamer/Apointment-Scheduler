using MySql.Data.MySqlClient;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Apointment_Scheduler.Forms;

namespace Apointment_Scheduler
{
    public partial class MainForm : Form
    {
        private FormState formState = FormState.Customers;

        public MainForm()
        {
            InitializeComponent();

            RefreshTableSettings();
            RefreshTable();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void tabCustomers_Click(object sender, EventArgs e)
        {
            formState = FormState.Customers;
            RefreshTableSettings();
            RefreshTable();
        }
        private void tabAppointments_Click(object sender, EventArgs e)
        {
            formState = FormState.Appointments;
            RefreshTableSettings();
            RefreshTable();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void appointmentByTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Additional Report of Choice
            formState = FormState.Type;
            RefreshTableSettings();
            RefreshTable();
        }
        private void appointmentTypesPerMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formState = FormState.Month;
            RefreshTableSettings();
            RefreshTable();
        }
        private void consultantSchedulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formState = FormState.Consultants;
            RefreshTableSettings();
            RefreshTable();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            RefreshTable();
        }
        private void ckAllAppointments_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTable();
        }
        private void cbxReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (formState == FormState.Customers)
            {

                new CustomerForm().ShowDialog();

            }
            else if (formState == FormState.Appointments)
            {
                new AppointmentForm().ShowDialog();
            }
            else
            {
                return;
            }
            RefreshTable();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (formState == FormState.Customers)
            {
                //Requirement 2B: Update Exeption Handling
                if (dgvMain.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Error: Nothing Selected");
                    return;

                }
                new CustomerForm(dgvMain.SelectedRows[0]).ShowDialog();

            }
            else if (formState == FormState.Appointments)
            {
                //Requirement 3B: Update Exeption Handling
                if (dgvMain.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Error: Nothing Selected");
                }
                new AppointmentForm(dgvMain.SelectedRows[0]).ShowDialog();
            }
            else
            {
                return;
            }

            RefreshTable();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Requirement 2B and 3B: Delete Exeption Handling
            DialogResult result = MessageBox.Show("Do you want to delete? This cannot be undone.", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            if (formState == FormState.Customers)
            {
                //delete customer query
                Data.conn.Open();
                string DeleteCustomerQuery = "DELETE FROM customer WHERE customerId = " +
                                             dgvMain.SelectedRows[0].Cells["customerId"].Value.ToString();
                new MySqlDataAdapter(DeleteCustomerQuery, Data.conn);
                Data.conn.Close();
            }
            else if (formState == FormState.Appointments)
            {
                //delete appointments query
                Data.conn.Open();
                string DeleteAppointmentQuery = "DELETE FROM appointment WHERE appointmentId = " +
                                                dgvMain.SelectedRows[0].Cells["appointmentId"].Value.ToString();
                MySqlDataAdapter deletecustomer = new MySqlDataAdapter(DeleteAppointmentQuery, Data.conn);
                Data.conn.Close();
            }
            else
            {
                return;
            }
            RefreshTable();
        }



        #region Helper Functions
        private void SetupCustomerDGV()
        {
            // Setting column titles
            dgvMain.Columns["customerId"].HeaderText = "Customer ID";
            dgvMain.Columns["customerName"].HeaderText = "Customer Name";
            dgvMain.Columns["address"].HeaderText = "Address";
            dgvMain.Columns["phone"].HeaderText = "Phone";
            dgvMain.Columns["city"].HeaderText = "City";
            dgvMain.Columns["country"].HeaderText = "Country";

            dgvMain.Columns["customerId"].Visible = false;
            dgvMain.Columns["addressId"].Visible = false;
            dgvMain.Columns["cityId"].Visible = false;
            dgvMain.Columns["countryId"].Visible = false;
        }
        private void SetupAppointmentDGV()
        {
            // Setting column titles
            dgvMain.Columns["userName"].HeaderText = "Consultant";
            dgvMain.Columns["customerName"].HeaderText = "Customer Name";
            dgvMain.Columns["description"].HeaderText = "Description";
            dgvMain.Columns["type"].HeaderText = "Visit Type";
            dgvMain.Columns["location"].HeaderText = "Location";
            dgvMain.Columns["start"].HeaderText = "Start";
            dgvMain.Columns["end"].HeaderText = "End";
            dgvMain.Columns["phone"].HeaderText = "Phone";
            dgvMain.Columns["url"].HeaderText = "Visit Link";

            dgvMain.Columns["customerId"].Visible = false;
            dgvMain.Columns["addressId"].Visible = false;
            dgvMain.Columns["cityId"].Visible = false;
            dgvMain.Columns["countryId"].Visible = false;

            dgvMain.Columns["appointmentId"].Visible = false;
            dgvMain.Columns["userId"].Visible = false;

        }
        public void RefreshTableSettings()
        {
            Color mainColor = Color.Red;
            // Tab color
            if (formState == FormState.Customers)
            {
                tabCustomers.ForeColor = mainColor;
                tabAppointments.ForeColor = Color.Black;
                tabReports.ForeColor = Color.Black;

                btnAdd.Show();
                btnUpdate.Show();
                btnDelete.Show();
                btnReport.Hide();
                dateTimePicker.Hide();
                ckAllAppointments.Hide();
                lblReports.Hide();
                cbxReports.Hide();
            }
            else if (formState == FormState.Appointments)
            {
                tabCustomers.ForeColor = Color.Black;
                tabAppointments.ForeColor = mainColor;
                tabReports.ForeColor = Color.Black;

                btnAdd.Show();
                btnUpdate.Show();
                btnDelete.Show();
                btnReport.Hide();
                dateTimePicker.Show();
                ckAllAppointments.Show();
                lblReports.Hide();
                cbxReports.Hide();
            }
            else if (formState == FormState.Month)
            {
                tabCustomers.ForeColor = Color.Black;
                tabAppointments.ForeColor = Color.Black;
                tabReports.ForeColor = mainColor;
                lblReports.Text = "Month";
                cbxReports.DataSource = Enum.GetNames(typeof(Months));

                btnAdd.Hide();
                btnUpdate.Hide();
                btnDelete.Hide();
                btnReport.Show();
                dateTimePicker.Hide();
                ckAllAppointments.Hide();
                lblReports.Show();
                cbxReports.Show();
            }
            else if (formState == FormState.Type)
            {
                tabCustomers.ForeColor = Color.Black;
                tabAppointments.ForeColor = Color.Black;
                tabReports.ForeColor = mainColor;
                lblReports.Text = "Visit Type";
                cbxReports.DataSource = Enum.GetNames(typeof(VisitTypes));

                btnAdd.Hide();
                btnUpdate.Hide();
                btnDelete.Hide();
                btnReport.Show();
                dateTimePicker.Hide();
                ckAllAppointments.Hide();
                lblReports.Show();
                cbxReports.Show();
            }
            else if (formState == FormState.Consultants)
            {
                tabCustomers.ForeColor = Color.Black;
                tabAppointments.ForeColor = Color.Black;
                tabReports.ForeColor = mainColor;
                lblReports.Text = "Consultant";
                cbxReports.DataSource = Data.userNames;
                cbxReports.DisplayMember = "Value";
                cbxReports.ValueMember = "Key";


                btnAdd.Hide();
                btnUpdate.Hide();
                btnDelete.Hide();
                btnReport.Show();
                dateTimePicker.Hide();
                ckAllAppointments.Hide();
                lblReports.Show();
                cbxReports.Show();
            }
        }
        public void RefreshTable()
        {
            TimeZoneInfo.ClearCachedData();
            if (formState == FormState.Appointments)
            {
                if (!ckAllAppointments.Checked)
                {
                    dgvMain.DataSource = Data.GetAppointments(dateTimePicker.Value);
                    dateTimePicker.Enabled = true;
                }
                else
                {
                    dgvMain.DataSource = Data.GetAppointments();
                    dateTimePicker.Enabled = false;
                }
                SetupAppointmentDGV();
            }
            else if (formState == FormState.Customers)
            {

                //get all customer data for gridview
                dgvMain.DataSource = Data.GetCustomers();

                SetupCustomerDGV();
            }
            else if (formState == FormState.Month)
            {
                dgvMain.DataSource = Data.GetAppointmentTypePerMonth(cbxReports.SelectedIndex + 1);
            }
            else if (formState == FormState.Type)
            {
                dgvMain.DataSource = Data.GetAppointmentByType(cbxReports.Text);
                SetupAppointmentDGV();
            }
            else if (formState == FormState.Consultants)
            {
                dgvMain.DataSource = Data.GetConsultantSchedules(((KeyValuePair<int, string>)cbxReports.SelectedItem).Value.ToString(),
                                                                 ((KeyValuePair<int, string>)cbxReports.SelectedItem).Key.ToString());
                SetupAppointmentDGV();
            }
            else
            {
                return;
            }
        }
        #endregion

    }
}
