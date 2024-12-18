using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using Mysqlx.Crud;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Policy;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Data.Common;

#region Requirements
//  Requirements
//  1.   !Create a login form that has the ability to do the following:

//        a.Determine a user’s location.

//        b.Translate login and error control messages (e.g., “The username and password do not match.”) into English and one additional language.

//        c.Verify the correct username and password.

//  2.   !Provide the ability to add, update, and delete customer records.

//        a.Validate each of the following requirements for customer records:

//            •    that a customer record includes name, address, and phone number fields

//            •    that fields are trimmed and non-empty

//            •    that the phone number field allows only digits and dashes

//        b.Add exception handling that can be used when performing each of the following operations for customer records:

//            •    “add” operations

//            •    “update” operations

//            •    “delete database” operations

//  3.   !Provide the ability to add, update, and delete appointments, capture the type of appointment, and link to a specific customer record in the database.

//        a.Validate each of the following requirements for appointments:

//            •    ##Require appointments to be scheduled during the business hours of 9:00 a.m.to 5:00 p.m., Monday–Friday, eastern standard time.

//            •    Prevent the scheduling of overlapping appointments.

//        b.Add exception handling that can be used when performing each of the following operations for appointments:

//            •    “add” operations

//            •    “update” operations

//            •    “delete database” operations

//  4.   !Create a calendar view feature, including the ability to view appointments on a specific day by selecting a day of the month from a calendar of the months of the year.

//  5.   Provide the ability to automatically adjust appointment times based on user time zones and daylight saving time.
//          (works but you have to relaunch the app if you change timezones)

//  6.   Create a function that generates an alert whenever a user who has an appointment within 15 minutes logs in to their account.

//  7.   !Create a function that allows users to generate the three reports listed using collection classes, incorporating a lambda expression into the code for each of the following reports:

//            •    the number of appointment types by month

//            •    the schedule for each user

//            •    one additional report of your choice

//  8.   !Record the timestamp and the username of each login in a text file named “Login_History.txt,” ensuring that each new record is appended to the log file.
#endregion


namespace Apointment_Scheduler
{
    public partial class LoginForm : Form
    {
        static string ErrorMessage = "Username and Password do not match";


        public LoginForm()
        {
            InitializeComponent();
            CheckLanguage();

            Data.InitializeDatabase();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
           
            try
            {
                //Requirment 1C: Verify the correct username and password
                Data.conn.Open();
                string LoginQuery = "SELECT userId, userName, password FROM user WHERE userName = '" + username + "' AND password = '" + password + "'";
                MySqlDataAdapter sda = new MySqlDataAdapter(LoginQuery, Data.conn);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Data.User = dt.Rows[0].Field<string>("userName");
                    Data.UserId = dt.Rows[0].Field<int>("userId").ToString();

                    Data.GetCustomerNames();
                    Data.GetUserNames();

                    //Requirement 6: Generates an alert whenever a user who has an appointment within 15 minutes logs in to their account.
                    CheckAppointments();

                    //Requirement 8: Record the timestamp and the username of each login in a text file 
                    LoginLogger();

                    //load Main Form
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(ErrorMessage, "Error2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Clear();

                    txtUsername.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                //close connection
                conn.Close();
            }


        }

        private void LoginLogger() 
        {
            // Requirement 8: UserActivityLog should be in bin/debug/UserActivityLog.txt
            try
            {
                using (StreamWriter sw = new StreamWriter("Login_History.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - User '{Data.User}' logged in.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error logging user activity: {ex.Message}");
            }
        }
        private void CheckLanguage()
        {
            //Requirement 1A: Determine a user’s location.
            CultureInfo culture = CultureInfo.CurrentCulture;
            CultureInfo main = new CultureInfo("en-US");
            //CultureInfo sp = new CultureInfo("es-ES");

            //Requirement 1B: Translate login and error control messages (e.g., “The username and password do not match.”) into English and one additional language.

            if (culture.EnglishName != main.EnglishName)
            {
                //lblHeader.Text = "Por favor Iniciar sesión";
                lblUsername.Text = "Nombre de usuario";
                lblPassword.Text = "Contraseña";
                btnLogin.Text = "Iniciar sesión";
                btnExit.Text = "Salida";
                ErrorMessage = "El nombre de usuario y la contraseña no coinciden";
            }
        }
        private void CheckAppointments()
        {
            bool result;
            try
            {
                Data.conn.Open();
                using (var upcomingAppointmentCMD = new MySqlCommand(Data.UpcomingAppointmentQuery, Data.conn))
                {
                    var currentTime = DateTime.UtcNow;
                    upcomingAppointmentCMD.Parameters.AddWithValue("@userId", Data.UserId);
                    upcomingAppointmentCMD.Parameters.AddWithValue("@currentTime", currentTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    int upcomingAppointmentCount = Convert.ToInt32(upcomingAppointmentCMD.ExecuteScalar());
                    result = upcomingAppointmentCount > 0;
                }
            }
            catch
            {
                throw;
            }

            if (result)
            {
                MessageBox.Show("You have an upcoming appointment.");
            }
        }
        
    }
}
