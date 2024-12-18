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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Mysqlx.Crud;
using System.Data.Common;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;



namespace Apointment_Scheduler
{
    public enum FormState
    {
        Customers,
        Appointments,
        Month,
        Type,
        Consultants,
    }
    public enum Locations
    {
        Arizona,
        New_York,
        London,
        Remote,
    }
    public enum VisitTypes
    {
        Consultation,
        Presentation,
        Assesment,
        Business_Analysis,
        Quality_Assurance,
        Scrum,
        Lunch,
    }
    public enum Months
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
    }


    public class Data
    {
        public static string connectionString = "server=localhost;user=sqlUser;database=client_schedule;port=3306;password=Passw0rd!";
        public static MySqlConnection conn = new MySqlConnection(connectionString);
        
        //public static List<KeyValuePair<int, string>> Months = new List<KeyValuePair<int, string>>();


        public static string? User { get; set; }
        public static string? UserId { get; set; }
        public static string? ErrorMessage { get; set; }


        #region Getters
        public static DataTable GetCustomers()
        {
            conn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(GetCustomerTableQuery, conn);
            conn.Close();
            DataTable customerDataTable = new DataTable();
            sda.Fill(customerDataTable);
            return customerDataTable;
        }
        public static DataTable GetAppointments(DateTime date)
        {
            //This function gets all appointments from the database that corispond to value
            
            DateTime start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

            // Execute SQL Query
            DataTable FilterdAppointments = new DataTable();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(GetFilteredAppointmentsQuery, conn))
            {
                cmd.Parameters.AddWithValue("@StartDate", start);
                cmd.Parameters.AddWithValue("@EndDate", end);
                cmd.ExecuteNonQuery();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(FilterdAppointments);
                }
            }
            conn.Close();
            return ConvertToLocal(FilterdAppointments);
        }
        public static DataTable GetAppointments()
        {
            //This function gets all appointments
            conn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(GetAppointmentTableQuery, conn);
            conn.Close();
            DataTable appointmentsDataTable = new DataTable();
            sda.Fill(appointmentsDataTable);
            return ConvertToLocal(appointmentsDataTable);
        }
        public static int GetNewId(string query, MySqlConnection conn) => Convert.ToInt32(new MySqlCommand(query, conn).ExecuteScalar()) + 1;
        public static DataTable GetUserNames()
        {
            MySqlDataAdapter sda = new MySqlDataAdapter(GetUsersQuery, conn);
            DataTable Users = new DataTable();
            sda.Fill(Users);
            return Users;
        }
        public static DataTable GetCustomerNames()
        {
            MySqlDataAdapter sda = new MySqlDataAdapter(GetCustomersQuery, conn);
            DataTable Customers = new DataTable();
            sda.Fill(Customers);
            return Customers;
        }


        public static DataTable GetAppointmentTypePerMonth(int month)
        {
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(AppointmentTypeByMonthQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally 
            { 
                conn.Close(); 
            }
            return dataTable;
        }
        public static DataTable GetConsultantSchedules(string userName, string userId)
        {
            DataTable consultantDataTable = new DataTable();
            try
            {
                conn.Open();
                using (MySqlCommand consultantCMD = new MySqlCommand(GetUserScheduleQuery, conn))
                {
                    consultantCMD.Parameters.AddWithValue("@UserName", userName);
                    consultantCMD.Parameters.AddWithValue("@UserId", userId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(consultantCMD))
                    {
                        adapter.Fill(consultantDataTable);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return ConvertToLocal(consultantDataTable);
        }
        public static DataTable GetAppointmentByType(string type)
        {
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(AppointmentTypeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@type", type);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }

        #endregion


        #region Time slots
        public static DataTable ConvertToLocal(DataTable table)
        {
            //This converts start and end dates to local timezone accounting for Daylight Savings Time
            foreach (DataRow row in table.Rows)
            {
                if (row["start"] is DateTime startUtc)
                {
                    row["start"] = TimeZoneInfo.ConvertTimeFromUtc(startUtc, TimeZoneInfo.Local);
                }

                if (row["end"] is DateTime endUtc)
                {
                    row["end"] = TimeZoneInfo.ConvertTimeFromUtc(endUtc, TimeZoneInfo.Local);
                }
            }
            return table;
        }
        public static List<KeyValuePair<string, string>> GetAvailableSlots(DateTime date)
        {
            //KeyValuePair<List<string>, List<string>>;
            
            var allSlots = GenerateAllSlots(date);
            var bookedSlots = GetBookedSlots(date);
            if (bookedSlots == null)
            {
                // Create an empty list instead of null, if all appointment times are open
                bookedSlots = new List<Tuple<DateTime, DateTime>>();
            }
            
            var availableSlots = allSlots.Where(slot => !IsSlotBooked(slot, bookedSlots)).ToList();
            var availableSlotsString = ConvertSlotsToString(availableSlots);

            return availableSlotsString;
        }
        private static List<Tuple<DateTime, DateTime>> GenerateAllSlots(DateTime date)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            // Requirement 3A: Time slots will be from business hours 9-5 EST
            var allSlots = new List<Tuple<DateTime, DateTime>>();

            // Create date with EST time zone
            DateTime startHourEST = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            DateTime endHourEST = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0);

            // Convert EST time slots to UTC for use in available slot calculations
            DateTime startHour = TimeZoneInfo.ConvertTimeToUtc(startHourEST, estZone);
            DateTime endHour = TimeZoneInfo.ConvertTimeToUtc(endHourEST, estZone);

            while (startHour < endHour)
            {
                allSlots.Add(new Tuple<DateTime, DateTime>(startHour, startHour.AddMinutes(30)));
                startHour = startHour.AddMinutes(30);
            }


            return allSlots;
        }
        public static List<Tuple<DateTime, DateTime>> GetBookedSlots(DateTime date)
        {
            var bookedSlots = new List<Tuple<DateTime, DateTime>>();
            try
            {
                conn.Open();
                using (var cmd = new MySqlCommand(GetAppointmentStartEndQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var start = reader.GetDateTime("start");
                            var end = reader.GetDateTime("end");
                            bookedSlots.Add(new Tuple<DateTime, DateTime>(start, end));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return bookedSlots;
        }
        private static bool IsSlotBooked(Tuple<DateTime, DateTime> slot, List<Tuple<DateTime, DateTime>> bookedSlots)
        {
            // Streamline the filtering calculation using lambda
            return bookedSlots.Any(bookedSlot => bookedSlot.Item1 < slot.Item2 && bookedSlot.Item2 > slot.Item1);
        }
        private static List<KeyValuePair<string, string>> ConvertSlotsToString(List<Tuple<DateTime, DateTime>> availableSlots)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            List<KeyValuePair<string, string>> stringSlots = new List<KeyValuePair<string, string>>();

            // This converts UTC slots to user's local time zone and then to string for display in Add/Update form
            foreach ( var slot in availableSlots)
            {
                string itm1 = $"{TimeZoneInfo.ConvertTimeFromUtc(slot.Item1, localZone):hh:mm tt}";
                string itm2 = $"{TimeZoneInfo.ConvertTimeFromUtc(slot.Item2, localZone):hh:mm tt}";
                stringSlots.Add(new KeyValuePair<string, string>(itm1, itm2));
            }
            return stringSlots;

        }
        public static DateTime ConvertStringToDateTime(DateTime selectedDate, string selectedTimeStr)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;

            // Split string and Parse
            DateTime time = DateTime.ParseExact(selectedTimeStr, "hh:mm tt", null);

            // Combine the date string from selectedDate and the time strings from startTime and endTime
            DateTime timeDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, time.Hour, time.Minute, 0);

            // Convert to UTC for database saving/updating
            DateTime startDateTimeUTC = TimeZoneInfo.ConvertTimeToUtc(timeDate, userTimeZone);

            return startDateTimeUTC;
        }
        #endregion

        #region Save Customer Data
        public static void SaveCustomer(Dictionary<string, string> customerData, bool update) 
        {
            //Requirement 2B: Add/Update Exeption Handling
            try
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int countryId = SaveCountryData(customerData, conn, update);
                        int cityId = SaveCityData(customerData, conn, countryId, update);
                        int addressId = SaveAddressData(customerData, conn, cityId, update);
                        SaveCustomerNameData(customerData, conn, addressId, update);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "test");
            }
            finally
            {
                conn.Close();
            }
        }
        private static int SaveCountryData(Dictionary<string, string> customerData, MySqlConnection conn, bool update)
        {
            int countryId;
            string query;
            if (update)
            {
                countryId = int.Parse(customerData["CountryId"]);
                query = CountryUpdateQuery;
            }
            else
            {
                using (var countryIndexCmd = new MySqlCommand(CountryIdxQuery, conn))
                {
                    countryId = GetNewId(CountryIdxQuery, conn);
                    query = CountryInsertQuery;
                }
            }

            using (var countryInsertCMD = new MySqlCommand(query, conn))
            {
                countryInsertCMD.Parameters.AddWithValue("@CountryId", countryId);
                countryInsertCMD.Parameters.AddWithValue("@Country", customerData["CustomerCountry"]);
                countryInsertCMD.Parameters.AddWithValue("@LastUpdateBy", User);
                if (!update) { countryInsertCMD.Parameters.AddWithValue("@CreatedBy", User); }

                countryInsertCMD.Prepare();
                countryInsertCMD.ExecuteNonQuery();
            }
            return countryId;
        }
        private static int SaveCityData(Dictionary<string, string> customerData, MySqlConnection conn, int latestCountryId, bool update)
        {
            int cityId;
            string query;

            if (update)
            {
                cityId = int.Parse(customerData["CityId"]);
                query = CityUpdateQuery;
            }
            else
            {
                using (var cityIndexCmd = new MySqlCommand(CityIdxQuery, conn))
                {
                    cityId = GetNewId(CityIdxQuery, conn);
                    query = CityInsertQuery;
                }
            }
            using (var cityInsertCMD = new MySqlCommand(query, conn))
            {
                cityInsertCMD.Parameters.AddWithValue("@CityId", cityId);
                cityInsertCMD.Parameters.AddWithValue("@City", customerData["CustomerCity"]);
                cityInsertCMD.Parameters.AddWithValue("@CountryId", latestCountryId);
                cityInsertCMD.Parameters.AddWithValue("@LastUpdateBy", User);

                if (!update) { cityInsertCMD.Parameters.AddWithValue("@CreatedBy", User); }

                cityInsertCMD.Prepare();
                cityInsertCMD.ExecuteNonQuery();
            }

            return cityId;
        }
        private static int SaveAddressData(Dictionary<string, string> customerData, MySqlConnection conn, int latestCityId, bool update)
        {
            int addressId;
            string query;

            if (update)
            {
                addressId = int.Parse(customerData["AddressId"]);
                query = AddressUpdateQuery;
            }
            else
            {
                using (var addressIndexCmd = new MySqlCommand(AddressIdxQuery, conn))
                {
                    addressId = GetNewId(AddressIdxQuery, conn);
                    query = AddressInsertQuery;
                }
            }
            using (var addressInsertCommand = new MySqlCommand(query, conn))
            {
                addressInsertCommand.Parameters.AddWithValue("@AddressId", addressId);
                addressInsertCommand.Parameters.AddWithValue("@Address", customerData["CustomerAddress"]);
                addressInsertCommand.Parameters.AddWithValue("@PostalCode", customerData["CustomerPostal"]);
                addressInsertCommand.Parameters.AddWithValue("@PhoneNumber", customerData["CustomerPhone"]);
                addressInsertCommand.Parameters.AddWithValue("@CityId", latestCityId);
                addressInsertCommand.Parameters.AddWithValue("@LastUpdateBy", User);
                if (!update) { addressInsertCommand.Parameters.AddWithValue("@CreatedBy", User); };

                addressInsertCommand.Prepare();
                addressInsertCommand.ExecuteNonQuery();
            }

            return addressId;
        }
        private static void SaveCustomerNameData(Dictionary<string, string> customerData, MySqlConnection conn, int latestAddressId, bool update)
        {
            int customerId;
            string query;

            if (update)
            {
                customerId = int.Parse(customerData["CustomerId"]);
                query = CustomerUpdateQuery;
            }
            else
            {
                using (var customerIndexCmd = new MySqlCommand(CustomerIdxQuery, conn))
                {
                    customerId = GetNewId(CountryIdxQuery, conn);
                    query = CustomerInsertQuery;
                }
            }
            using (var customerInsertCommand = new MySqlCommand(query, conn))
            {
                //customerNames.Add(new KeyValuePair<int, string>(customerId, customerData["CustomerName"]));
                customerInsertCommand.Parameters.AddWithValue("@CustomerId", customerId);
                customerInsertCommand.Parameters.AddWithValue("@CustomerName", customerData["CustomerName"]);
                customerInsertCommand.Parameters.AddWithValue("@AddressId", latestAddressId);
                customerInsertCommand.Parameters.AddWithValue("@Active", 1);
                customerInsertCommand.Parameters.AddWithValue("@LastUpdateBy", User);
                if (!update) { customerInsertCommand.Parameters.AddWithValue("@CreatedBy", User); };

                customerInsertCommand.Prepare();
                customerInsertCommand.ExecuteNonQuery();
            }
        }
        public static void DeleteCustomer(int customerId)
        {
            try
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var deleteAppointmentsCMD = new MySqlCommand(DeleteCustomerAppointmentsQuery, conn))
                        {
                            deleteAppointmentsCMD.Parameters.AddWithValue("@CustomerId", customerId);
                            deleteAppointmentsCMD.ExecuteNonQuery();
                        }

                        using (var deleteCustomerCMD = new MySqlCommand(DeleteCustomerQuery, conn))
                        {
                            deleteCustomerCMD.Parameters.AddWithValue("@CustomerId", customerId);
                            deleteCustomerCMD.Prepare();
                            deleteCustomerCMD.ExecuteNonQuery();
                        }
                        transaction.Commit(); // Success? Commit
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Save Appointment Data
        public static void SaveAppointment(Dictionary<string, string> appointmentData, DateTime startTime, DateTime endTime, int CustomerId, int UserId, bool isUpdate)
        {
            //Requirement 3B: Add/Update Exeption Handling
            try
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query;
                        if (isUpdate)
                        {
                            query = appointmentUpdateQuery;
                        }
                        else
                        {
                            using (var countryIndexCmd = new MySqlCommand(CountryIdxQuery, conn))
                            {
                                query = appointmentInsertQuery;
                            }
                        }
                        SaveAppointmentData(appointmentData, startTime, endTime, isUpdate, CustomerId, UserId, conn, query);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private static void SaveAppointmentData(Dictionary<string, string> appointmentData, DateTime startTime, DateTime endTime, bool isUpdate, int CustomerId, int UserId, MySqlConnection conn, string query)
        {
            using (var appointmentInsertCMD = new MySqlCommand(query, conn))
            {
                appointmentInsertCMD.Parameters.AddWithValue("@AppointmentId", appointmentData["AppointmentId"]);
                appointmentInsertCMD.Parameters.AddWithValue("@CustomerId", CustomerId);
                appointmentInsertCMD.Parameters.AddWithValue("@UserId", UserId);
                appointmentInsertCMD.Parameters.AddWithValue("@Title", "not needed");
                appointmentInsertCMD.Parameters.AddWithValue("@Description", appointmentData["Description"]);
                appointmentInsertCMD.Parameters.AddWithValue("@Location", appointmentData["Location"]);
                appointmentInsertCMD.Parameters.AddWithValue("@Contact", appointmentData["ConsultantName"]);
                appointmentInsertCMD.Parameters.AddWithValue("@Type", appointmentData["VisitType"]);
                appointmentInsertCMD.Parameters.AddWithValue("@URL", "not needed");
                appointmentInsertCMD.Parameters.AddWithValue("@Start", startTime);
                appointmentInsertCMD.Parameters.AddWithValue("@End", endTime);
                appointmentInsertCMD.Parameters.AddWithValue("@LastUpdateBy", User);
                if (!isUpdate) { appointmentInsertCMD.Parameters.AddWithValue("@CreatedBy", User); }
                appointmentInsertCMD.Prepare();
                appointmentInsertCMD.ExecuteNonQuery();
            }
        }
        #endregion

        #region Validation
        //Requirement 2A: Validation
        public static bool ValidateTextBox(TextBox textBox, string type, ErrorProvider errorProvider)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, "This field cannot be empty");
                ErrorMessage = "This field cannot be empty";
                isValid = false;
            }
            else if (!ValidateType(textBox, type, errorProvider))
            {
                isValid = false;
            }
            else
            {
                errorProvider.SetError(textBox, "");
                ErrorMessage = "";
            }

            textBox.BackColor = isValid ? Color.White : Color.Salmon;
            return isValid;
        }
        private static bool ValidateType(TextBox textBox, string type, ErrorProvider errorProvider)
        {
            bool isValid = true;

            switch (type)
            {
                case "int":
                    if (!int.TryParse(textBox.Text, out int intNumber))
                    {
                        errorProvider.SetError(textBox, "Invalid integer format");
                        ErrorMessage = "Invalid integer format";
                        isValid = false;
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "");
                        ErrorMessage = "";
                    }
                    break;

                case "decimal":
                    if (!decimal.TryParse(textBox.Text, out decimal decimalNumber))
                    {
                        errorProvider.SetError(textBox, "Invalid decimal format");
                        ErrorMessage = "Invalid decimal format";
                        isValid = false;
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "");
                        ErrorMessage = "";
                    }
                    break;

                case "string":
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "This field cannot be empty");
                        ErrorMessage = "This field cannot be empty";
                        isValid = false;
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "");
                        ErrorMessage = "";
                    }
                    break;
                case "phone":
                    Regex phoneRegex = new Regex(@"^(\+?\d{1,2}\s?)?((\(\d{1,3}\))|\d{1,3})[-.\s]?\d{1,4}[-.\s]?\d{1,4}([-.\s]?\d{1,9})?$");
                    if (phoneRegex.IsMatch(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "");
                        ErrorMessage = "";
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "Invalid phone number format");
                        ErrorMessage = "Invalid phone number format";
                        isValid = false;
                    }
                    break;
                default:
                    errorProvider.SetError(textBox, "Unknown type");
                    ErrorMessage = "Unkown Type";
                    isValid = false;
                    break;
            }
            textBox.BackColor = isValid ? Color.White : Color.Salmon;
            return isValid;
        }
        public static void DeleteAppointment(int appointmentId)
        {
            try
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var deleteAppointmentCMD = new MySqlCommand(DeleteAppointmentQuery, conn))
                        {
                            deleteAppointmentCMD.Parameters.AddWithValue("@AppointmentId", appointmentId);
                            deleteAppointmentCMD.Prepare();
                            deleteAppointmentCMD.ExecuteNonQuery();
                        }
                        transaction.Commit(); // Success? Commit
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        #endregion


        #region Queries
        #region Get Table Queries
        public static string GetCustomerTableQuery => "SELECT " +
                 "c.customerId, c.customerName, " +
                 "a.address, a.addressId, a.postalCode, a.phone, " +
                 "ci.city, ci.cityId, " +
                 "co.country, co.countryId " +
                 "FROM customer c " +
                 "JOIN address a ON c.addressId = a.addressId " +
                 "JOIN city ci ON a.cityId = ci.cityId " +
                 "JOIN country co ON ci.countryId = co.countryId";
        #endregion

        #region Country Queries
        public static string CountryIdxQuery => "SELECT " +
                 "countryId FROM country " +
                 "ORDER BY countryId DESC LIMIT 1";
        public static string CountryInsertQuery => "INSERT INTO country " +
                 "(countryId, country, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                 "VALUES (@CountryId, @Country, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";

        public static string CountryUpdateQuery => "UPDATE country SET " +
                 "country = @Country, " +
                 "lastUpdate = NOW(), " +
                 "lastUpdateBy = @LastUpdateBy " +
                 "WHERE countryId = @CountryId";


        #endregion

        #region City Queries
        public static string CityIdxQuery => "SELECT " +
                 "cityId FROM city " +
                 "ORDER BY cityId DESC LIMIT 1";
        public static string CityInsertQuery => "INSERT INTO city " +
                 "(cityId, city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                 "VALUES (@CityId, @City, @CountryId, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string CityUpdateQuery => "UPDATE city SET " +
                "city = @City, " +
                "countryId = @CountryId, " +
                "lastUpdate = NOW(), " +
                "lastUpdateBy = @LastUpdateBy " +
                "WHERE cityId = @CityId";

        #endregion

        #region Address Queries
        public static string AddressIdxQuery => "SELECT " +
                "addressId FROM address " +
                "ORDER BY addressId DESC LIMIT 1";
        public static string AddressInsertQuery => "INSERT INTO address " +
                "(addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                "VALUES (@AddressId, @Address, '', @CityId, @PostalCode, @PhoneNumber, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string AddressUpdateQuery => "UPDATE address SET " +
                "address = @Address, " +
                "address2 = '', " +
                "cityId = @CityId, " +
                "postalCode = @PostalCode, " +
                "phone = @PhoneNumber, " +
                "lastUpdate = NOW(), " +
                "lastUpdateBy = @LastUpdateBy " +
                "WHERE addressId = @AddressId";
        #endregion

        #region Customer Queries
        public static string CustomerIdxQuery => "SELECT " +
                "customerId FROM customer " +
                "ORDER BY customerId DESC LIMIT 1";
        public static string CustomerInsertQuery => "INSERT INTO customer " +
                "(customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                "VALUES (@CustomerId, @CustomerName, @AddressId, @Active, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string CustomerUpdateQuery => "UPDATE customer SET " +
                "customerName = @CustomerName, " +
                "addressId = @AddressId, " +
                "active = @Active, " +
                "lastUpdate = NOW(), " +
                "lastUpdateBy = @LastUpdateBy " +
                "WHERE customerId = @CustomerId";
        public static string GetCustomersQuery => "SELECT customerId, customerName FROM customer";
        public static string DeleteCustomerAppointmentsQuery => "DELETE FROM appointment WHERE customerId = @CustomerId";
        public static string DeleteCustomerQuery => "DELETE FROM customer WHERE customerId = @CustomerId";
        #endregion

        #region Appointment Queries
        public static string appointmentInsertQuery = "INSERT INTO appointment " + "" +
                "(appointmentId, customerId, userId, title, description, location, contact, type, url, " +
                "start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                "VALUES (@AppointmentId, @CustomerId, @UserId, @Title, @Description, @Location, @Contact, " +
                "@Type, @URL, @Start, @End, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string appointmentUpdateQuery = "UPDATE appointment SET " +
            "customerId = @CustomerId, " +
            "userId = @UserId, title = @Title, " +
            "description = @Description, location = @Location, contact = @Contact, type = @Type, " +
            "url = @URL, start = @Start, end = @End, " +
            "lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy " +
            "WHERE appointmentId = @AppointmentId;";
        public static string GetAppointmentTableQuery =>
                "SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, " +
                "ap.type, ap.url, ap.start, ap.end, u.userName, " +
                "c.customerName, a.phone, a.addressId, a.cityId, ci.countryId " +
                "FROM appointment ap " +
                "JOIN customer c ON ap.customerId = c.customerId " +
                "JOIN address a ON c.addressId = a.addressId " +
                "JOIN city ci ON a.cityId = ci.cityId " +
                "JOIN country co ON ci.countryId = co.countryId " +
                "JOIN user u ON ap.userId = u.userId " +
                "ORDER BY ap.start";
        public static string GetAppointmentStartEndQuery => "SELECT start, end FROM appointment WHERE DATE(start) = @Date";
        public static string GetFilteredAppointmentsQuery =>
                "SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, " +
                "ap.type, ap.url, ap.start, ap.end, u.userName, " +
                "c.customerName, a.phone, a.addressId, a.cityId, ci.countryId " +
                "FROM appointment ap " +
                "JOIN customer c ON ap.customerId = c.customerId " +
                "JOIN address a ON c.addressId = a.addressId " +
                "JOIN city ci ON a.cityId = ci.cityId " +
                "JOIN country co ON ci.countryId = co.countryId " +
                "JOIN user u ON ap.userId = u.userId " +
                "WHERE start BETWEEN @StartDate AND @EndDate " +
                "ORDER BY ap.start";

        public static string AppointmentIdxQuery => "SELECT appointmentId FROM appointment ORDER BY appointmentId DESC LIMIT 1";
        public static string DeleteAppointmentQuery => "DELETE FROM appointment WHERE appointmentId = @AppointmentId";
        public static string UpcomingAppointmentQuery =>
                "SELECT ap.start, ap.end, c.customerName " +
                "FROM appointment ap " +
                "JOIN customer c ON ap.customerId = c.customerId " +
                "WHERE start BETWEEN @currentTime AND DATE_ADD(@currentTime, INTERVAL 15 MINUTE) AND userId=@userId " +
                "ORDER BY start ASC";

        #endregion

        #region User Queries
        public static string GetUsersQuery => "SELECT userId, userName FROM user";
        #endregion

        #region Report Queries
        public static string AppointmentTypeByMonthQuery => @"SELECT type AS 'Appointment Type', 
                COUNT(type) AS 'Number of Appointments'
                FROM  appointment
                WHERE  MONTH(start) = @month
                GROUP BY type
                HAVING COUNT(type) > 0";

        public static string GetUserScheduleQuery =>
                "SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, " +
                "ap.type, ap.url, ap.start, ap.end, u.userName, " +
                "c.customerName, a.phone, a.addressId, a.cityId, ci.countryId " +
                "FROM appointment ap " +
                "JOIN customer c ON ap.customerId = c.customerId " +
                "JOIN address a ON c.addressId = a.addressId " +
                "JOIN city ci ON a.cityId = ci.cityId " +
                "JOIN country co ON ci.countryId = co.countryId " +
                "JOIN user u ON ap.userId = u.userId " +
                "WHERE u.userName = @UserName AND u.userId = @UserId " +
                "ORDER BY ap.start";

        //TODO: Not Done yet
        public static string AppointmentTypeQuery => "SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, " +
                "ap.type, ap.url, ap.start, ap.end, u.userName, " +
                "c.customerName, a.phone, a.addressId, a.cityId, ci.countryId " +
                "FROM appointment ap " +
                "JOIN customer c ON ap.customerId = c.customerId " +
                "JOIN address a ON c.addressId = a.addressId " +
                "JOIN city ci ON a.cityId = ci.cityId " +
                "JOIN country co ON ci.countryId = co.countryId " +
                "JOIN user u ON ap.userId = u.userId " +
                "WHERE ap.type = @type "+
                "ORDER BY ap.start";

        #endregion
        #endregion

        #region Initialize Database
        public static bool InitializeDatabase()
        {
            bool isNewDb = false;
            try
            {
                conn.Open();

                MySqlCommand tableCMD = new MySqlCommand(CheckTablesExistQuery, conn);
                int tableCount = Convert.ToInt32(tableCMD.ExecuteScalar());

                MySqlCommand userCMD = new MySqlCommand(GetUserCount, conn);
                int userCount = Convert.ToInt32(userCMD.ExecuteScalar());
                if (tableCount < 6 || userCount == 0)  // If not all tables exist or no users exist, initialize the database
                {
                    MySqlCommand initCmd = new MySqlCommand(InitializeDatabaseQuery, conn);
                    initCmd.ExecuteNonQuery();
                    isNewDb = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isNewDb;
        }
        public static string GetUserCount => "SELECT COUNT(*) FROM user;";
        public static string InitializeDatabaseQuery => @"
                USE `client_schedule`;

                -- populate table `country`
                INSERT INTO `country` VALUES 
                (1,'US','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (2,'Canada','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (3,'Norway','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test');

                -- populate table `city`
                INSERT INTO `city` VALUES 
                (1,'New York',1,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (2,'Los Angeles',1,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (3,'Toronto',2,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (4,'Vancouver',2,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (5,'Oslo',3,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test');

                -- populate table `address`
                INSERT INTO `address` VALUES 
                (1,'123 Main','',1,'11111','555-1212','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (2,'123 Elm','',3,'11112','555-1213','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (3,'123 Oak','',5,'11113','555-1214','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test');

                -- populate table `customer`
                INSERT INTO `customer` VALUES 
                (1,'John Doe',1,1,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (2,'Alfred E Newman',2,1,'2023-09-17 00:00:00','test2','2023-09-17 00:00:00','test2'),
                (3,'Ina Prufung',3,1,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test');

                -- populate table `user`
                INSERT INTO `user` VALUES 
                (1,'test','test',1,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (2,'test2','test2',1,'2023-09-17 00:00:00','test','2023-09-17 00:00:00','test2');

                -- populate table `appointment`
                INSERT INTO `appointment` VALUES 
                (1,1,1,'not needed','not needed','not needed','not needed','Presentation','not needed','2023-09-17 00:00:00','2023-09-17 00:00:00','2023-09-17 00:00:00','test','2023-09-17 00:00:00','test'),
                (2,2,1,'not needed','not needed','not needed','not needed','Scrum','not needed','2023-09-17 00:00:00','2023-09-17 00:00:00','2023-09-17 00:00:00','test2','2023-09-17 00:00:00','test2');
                ";
        public static string CheckTablesExistQuery => @"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = 'client_schedule' 
                  AND table_name IN ('country', 'city', 'address', 'customer', 'user', 'appointment');
                ";
        #endregion
    }
}