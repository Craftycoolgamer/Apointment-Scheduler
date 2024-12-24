using MySql.Data.MySqlClient;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Apointment_Scheduler.Forms;
using System.Xml.Linq;

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
        private void ckMonth_CheckedChanged(object sender, EventArgs e)
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
                    return;
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
            if (dgvMain.SelectedRows.Count == 0)
            {
                MessageBox.Show("Error: Nothing Selected");
                return;
            }
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
                int customerId = Convert.ToInt32(dgvMain.SelectedRows[0].Cells["customerId"].Value);
                Data.DeleteCustomer(customerId);
                Data.conn.Close();
            }
            else if (formState == FormState.Appointments)
            {
                //delete appointments query
                Data.conn.Open();
                int appointmentId = Convert.ToInt32(dgvMain.SelectedRows[0].Cells["appointmentId"].Value);
                Data.DeleteAppointment(appointmentId);
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

            dgvMain.Columns["customerName"].DisplayIndex = 0;
            dgvMain.Columns["description"].DisplayIndex = 1;
            dgvMain.Columns["userName"].DisplayIndex = 2;
            dgvMain.Columns["type"].DisplayIndex = 3;
            dgvMain.Columns["location"].DisplayIndex = 4;
            dgvMain.Columns["start"].DisplayIndex = 5;
            dgvMain.Columns["end"].DisplayIndex = 6;
            dgvMain.Columns["phone"].DisplayIndex = 7;
            dgvMain.Columns["url"].DisplayIndex = 8;

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
            dgvMain.Refresh();

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
                ckMonth.Hide();
            }
            else if (formState == FormState.Appointments)
            {
                tabCustomers.ForeColor = Color.Black;
                tabAppointments.ForeColor = mainColor;
                tabReports.ForeColor = Color.Black;
                lblReports.Text = "Month";
                cbxReports.DataSource = Enum.GetNames(typeof(Months));

                btnAdd.Show();
                btnUpdate.Show();
                btnDelete.Show();
                btnReport.Hide();
                dateTimePicker.Show();
                ckAllAppointments.Show();
                lblReports.Show();
                cbxReports.Show();
                ckMonth.Show();
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
                ckMonth.Hide();
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
                ckMonth.Hide();
            }
            else if (formState == FormState.Consultants)
            {

                tabCustomers.ForeColor = Color.Black;
                tabAppointments.ForeColor = Color.Black;
                tabReports.ForeColor = mainColor;
                lblReports.Text = "Consultant";
                cbxReports.DataSource = Data.GetUserNames();
                cbxReports.DisplayMember = "userName";
                cbxReports.ValueMember = "userId";


                btnAdd.Hide();
                btnUpdate.Hide();
                btnDelete.Hide();
                btnReport.Show();
                dateTimePicker.Hide();
                ckAllAppointments.Hide();
                lblReports.Show();
                cbxReports.Show();
                ckMonth.Hide();
            }
        }
        public void RefreshTable()
        {
            TimeZoneInfo.ClearCachedData();
            dgvMain.Refresh();
            if (formState == FormState.Appointments)
            {
                if (!ckAllAppointments.Checked)
                {
                    dateTimePicker.Enabled = true;
                    ckMonth.Enabled = true;
                    cbxReports.Enabled = true;
                    if (ckMonth.Checked)
                    {
                        dateTimePicker.Enabled = false;
                        dgvMain.DataSource = Data.GetAppointments(cbxReports.Text);
                        return;
                    }
                    dgvMain.DataSource = Data.GetAppointments(dateTimePicker.Value);
                }
                else
                {
                    dgvMain.DataSource = Data.GetAppointments();
                    dateTimePicker.Enabled = false;
                    cbxReports.Enabled = false;
                    ckMonth.Enabled = false;
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
                cbxReports.Enabled = true;
                dgvMain.DataSource = Data.GetAppointmentTypePerMonth(cbxReports.SelectedIndex + 1);
            }
            else if (formState == FormState.Type)
            {
                cbxReports.Enabled = true;
                dgvMain.DataSource = Data.GetAppointmentByType(cbxReports.Text);
                SetupAppointmentDGV();
            }
            else if (formState == FormState.Consultants)
            {
                cbxReports.Enabled = true;
                //GetConsultantSchedules(string userName, string userId)
                dgvMain.DataSource = Data.GetConsultantSchedules(cbxReports.Text, cbxReports.SelectedValue.ToString());
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
