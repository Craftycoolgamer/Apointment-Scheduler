using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apointment_Scheduler.Forms
{
    public partial class AppointmentForm : Form
    {

        private int appointmentId = 0;
        bool update;

        public AppointmentForm()
        {
            InitializeComponent();
            TimeZoneInfo.ClearCachedData();
            lblHeader.Text = "New Appointment";
            update = false;
            appointmentId = Data.GetNewId(Data.AppointmentIdxQuery, Data.conn);
            LoadForm();
        }
        public AppointmentForm(DataGridViewRow selectedRow)
        {
            InitializeComponent();
            TimeZoneInfo.ClearCachedData();
            lblHeader.Text = "Update Appointment";
            update = true;
            LoadForm();

            ////fill in boxes
            appointmentId = int.Parse(selectedRow.Cells["appointmentId"].Value.ToString());
            cbxName.Text = selectedRow.Cells["customerName"].Value.ToString();
            cbxConsultant.Text = selectedRow.Cells["userName"].Value.ToString();
            txtDescription.Text = selectedRow.Cells["description"].Value.ToString();
            cbxLocation.Text = selectedRow.Cells["location"].Value.ToString();
            cbxType.Text = selectedRow.Cells["type"].Value.ToString();
            dtpDay.Text = selectedRow.Cells["start"].Value.ToString();
            //cbxSTime.Text = selectedRow.Cells["start"].Value.ToString();
            //cbxETime.Text = selectedRow.Cells["end"].Value.ToString();
            //cbxSTime.SelectedIndex = 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpDay.Value;
            string startTime = ((KeyValuePair<string, string>)cbxSTime.SelectedItem).Key.ToString();
            string endTime = ((KeyValuePair<string, string>)cbxETime.SelectedItem).Value.ToString();
            DateTime startTimeDT = Data.ConvertStringToDateTime(selectedDate, startTime);
            DateTime endTimeDT = Data.ConvertStringToDateTime(selectedDate, endTime);

            //Requirement 3A: Time Validation
            if (!(startTimeDT <= endTimeDT))
            {
                MessageBox.Show("Error: Start Time is before End Time");
                return;
            }

            var appointmentData = new Dictionary<string, string>
            {
                { "CustomerId", ((KeyValuePair<int, string>)cbxName.SelectedItem).Key.ToString() },
                { "UserId", ((KeyValuePair<int, string>)cbxConsultant.SelectedItem).Key.ToString() },
                { "CustomerName", cbxName.Text },
                { "ConsultantName", cbxConsultant.Text },
                { "Description", txtDescription.Text },
                { "Location", cbxLocation.Text },
                { "VisitType", cbxType.Text },
                { "AppointmentId", appointmentId.ToString() }
            };

            // Requirement 3: Add/Update appointments
            Data.SaveAppointment(appointmentData, startTimeDT, endTimeDT, update);
            this.Close();
        }
        private void dtpDay_ValueChanged(object sender, EventArgs e)
        {
            var avalable = Data.GetAvailableSlots(dtpDay.Value); //List<KeyValuePair<string, string>>
            cbxSTime.DataSource = avalable;
            cbxETime.DataSource = avalable;
            cbxSTime.DisplayMember = "Key";
            cbxETime.DisplayMember = "Value";
        }

        private void LoadForm()
        {
            //Requirement 3B: Using combo box to prevent invalid user input
            //set all combo boxes datasoures
            cbxName.DataSource = Data.customerNames;
            cbxConsultant.DataSource = Data.userNames;

            cbxConsultant.DisplayMember = "Value";
            cbxName.DisplayMember = "Value";
            cbxConsultant.ValueMember = "Key";
            cbxName.ValueMember = "Key";
            cbxLocation.DataSource = Enum.GetNames(typeof(Locations));
            cbxType.DataSource = Enum.GetNames(typeof(VisitTypes));

            var avalable = Data.GetAvailableSlots(dtpDay.Value); //List<KeyValuePair<string, string>>
            cbxSTime.DataSource = avalable;
            cbxETime.DataSource = avalable;
            cbxSTime.DisplayMember = "Key";
            cbxETime.DisplayMember = "Value";

        }
    }
}






