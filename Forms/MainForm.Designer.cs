


namespace Apointment_Scheduler
{
    partial class MainForm
    {
        


        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            tabCustomers = new ToolStripMenuItem();
            tabAppointments = new ToolStripMenuItem();
            tabReports = new ToolStripMenuItem();
            appointmentTypesPerMonthToolStripMenuItem = new ToolStripMenuItem();
            appointmentByTypeToolStripMenuItem = new ToolStripMenuItem();
            consultantSchedulesToolStripMenuItem = new ToolStripMenuItem();
            btnExit = new Button();
            dgvMain = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnReport = new Button();
            dateTimePicker = new DateTimePicker();
            ckAllAppointments = new CheckBox();
            cbxReports = new ComboBox();
            lblReports = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMain).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { tabCustomers, tabAppointments, tabReports });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1210, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // tabCustomers
            // 
            tabCustomers.Name = "tabCustomers";
            tabCustomers.Size = new Size(76, 20);
            tabCustomers.Text = "Customers";
            tabCustomers.Click += tabCustomers_Click;
            // 
            // tabAppointments
            // 
            tabAppointments.Name = "tabAppointments";
            tabAppointments.Size = new Size(95, 20);
            tabAppointments.Text = "Appointments";
            tabAppointments.Click += tabAppointments_Click;
            // 
            // tabReports
            // 
            tabReports.DropDownItems.AddRange(new ToolStripItem[] { appointmentTypesPerMonthToolStripMenuItem, appointmentByTypeToolStripMenuItem, consultantSchedulesToolStripMenuItem });
            tabReports.Name = "tabReports";
            tabReports.Size = new Size(59, 20);
            tabReports.Text = "Reports";
            // 
            // appointmentTypesPerMonthToolStripMenuItem
            // 
            appointmentTypesPerMonthToolStripMenuItem.Name = "appointmentTypesPerMonthToolStripMenuItem";
            appointmentTypesPerMonthToolStripMenuItem.Size = new Size(236, 22);
            appointmentTypesPerMonthToolStripMenuItem.Text = "Appointment Types Per Month";
            appointmentTypesPerMonthToolStripMenuItem.Click += appointmentTypesPerMonthToolStripMenuItem_Click;
            // 
            // appointmentByTypeToolStripMenuItem
            // 
            appointmentByTypeToolStripMenuItem.Name = "appointmentByTypeToolStripMenuItem";
            appointmentByTypeToolStripMenuItem.Size = new Size(236, 22);
            appointmentByTypeToolStripMenuItem.Text = "Appointment by Visit Type";
            appointmentByTypeToolStripMenuItem.Click += appointmentByTypeToolStripMenuItem_Click;
            // 
            // consultantSchedulesToolStripMenuItem
            // 
            consultantSchedulesToolStripMenuItem.Name = "consultantSchedulesToolStripMenuItem";
            consultantSchedulesToolStripMenuItem.Size = new Size(236, 22);
            consultantSchedulesToolStripMenuItem.Text = "Consultant Schedules";
            consultantSchedulesToolStripMenuItem.Click += consultantSchedulesToolStripMenuItem_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(1083, 682);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(83, 29);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // dgvMain
            // 
            dgvMain.AllowUserToAddRows = false;
            dgvMain.AllowUserToDeleteRows = false;
            dgvMain.AllowUserToResizeRows = false;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMain.Location = new Point(41, 27);
            dgvMain.MultiSelect = false;
            dgvMain.Name = "dgvMain";
            dgvMain.ReadOnly = true;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.Size = new Size(1125, 568);
            dgvMain.TabIndex = 7;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(41, 634);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(104, 46);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(184, 634);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(104, 46);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(335, 634);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(104, 46);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnReport
            // 
            btnReport.Location = new Point(41, 634);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(104, 46);
            btnReport.TabIndex = 11;
            btnReport.Text = "View Report";
            btnReport.UseVisualStyleBackColor = true;
            btnReport.Click += btnReport_Click;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Location = new Point(481, 634);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 12;
            dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
            // 
            // ckAllAppointments
            // 
            ckAllAppointments.AutoSize = true;
            ckAllAppointments.Location = new Point(481, 663);
            ckAllAppointments.Name = "ckAllAppointments";
            ckAllAppointments.Size = new Size(151, 19);
            ckAllAppointments.TabIndex = 13;
            ckAllAppointments.Text = "Show All Appointments";
            ckAllAppointments.UseVisualStyleBackColor = true;
            ckAllAppointments.CheckedChanged += ckAllAppointments_CheckedChanged;
            // 
            // cbxReports
            // 
            cbxReports.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxReports.FormattingEnabled = true;
            cbxReports.Location = new Point(839, 647);
            cbxReports.Name = "cbxReports";
            cbxReports.Size = new Size(141, 23);
            cbxReports.TabIndex = 14;
            cbxReports.SelectedIndexChanged += cbxReports_SelectedIndexChanged;
            // 
            // lblReports
            // 
            lblReports.AutoSize = true;
            lblReports.Location = new Point(839, 629);
            lblReports.Name = "lblReports";
            lblReports.Size = new Size(65, 15);
            lblReports.TabIndex = 15;
            lblReports.Text = "Consultant";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(1210, 723);
            Controls.Add(lblReports);
            Controls.Add(cbxReports);
            Controls.Add(ckAllAppointments);
            Controls.Add(dateTimePicker);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dgvMain);
            Controls.Add(btnExit);
            Controls.Add(menuStrip1);
            Controls.Add(btnReport);
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(1226, 762);
            MinimumSize = new Size(1226, 762);
            Name = "MainForm";
            Text = "Scheduler";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem tabCustomers;
        private ToolStripMenuItem tabAppointments;
        private ToolStripMenuItem tabReports;
        private ToolStripMenuItem appointmentTypesPerMonthToolStripMenuItem;
        private ToolStripMenuItem appointmentByTypeToolStripMenuItem;
        private ToolStripMenuItem consultantSchedulesToolStripMenuItem;
        private Button btnExit;
        private DataGridView dgvMain;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnReport;
        private DateTimePicker dateTimePicker;
        private CheckBox ckAllAppointments;
        private ComboBox cbxReports;
        private Label lblReports;
    }
}
