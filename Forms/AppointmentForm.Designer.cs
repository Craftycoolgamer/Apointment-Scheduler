namespace Apointment_Scheduler.Forms
{
    partial class AppointmentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblHeader = new Label();
            lblSTime = new Label();
            lblDay = new Label();
            lblLocation = new Label();
            lblConsultant = new Label();
            lblName = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            lblType = new Label();
            lblDescription = new Label();
            txtDescription = new TextBox();
            cbxSTime = new ComboBox();
            cbxLocation = new ComboBox();
            cbxType = new ComboBox();
            cbxConsultant = new ComboBox();
            cbxName = new ComboBox();
            dtpDay = new DateTimePicker();
            cbxETime = new ComboBox();
            lblETime = new Label();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.Anchor = AnchorStyles.Top;
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(28, 23);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(103, 20);
            lblHeader.TabIndex = 29;
            lblHeader.Text = "Appointment";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSTime
            // 
            lblSTime.AutoSize = true;
            lblSTime.Location = new Point(28, 360);
            lblSTime.Name = "lblSTime";
            lblSTime.Size = new Size(60, 15);
            lblSTime.TabIndex = 28;
            lblSTime.Text = "Start Time";
            // 
            // lblDay
            // 
            lblDay.AutoSize = true;
            lblDay.Location = new Point(28, 306);
            lblDay.Name = "lblDay";
            lblDay.Size = new Size(101, 15);
            lblDay.TabIndex = 27;
            lblDay.Text = "Appointment Day";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(28, 248);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(53, 15);
            lblLocation.TabIndex = 25;
            lblLocation.Text = "Location";
            // 
            // lblConsultant
            // 
            lblConsultant.AutoSize = true;
            lblConsultant.Location = new Point(28, 114);
            lblConsultant.Name = "lblConsultant";
            lblConsultant.Size = new Size(65, 15);
            lblConsultant.TabIndex = 24;
            lblConsultant.Text = "Consultant";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(28, 59);
            lblName.Name = "lblName";
            lblName.Size = new Size(94, 15);
            lblName.TabIndex = 23;
            lblName.Text = "Customer Name";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(170, 502);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(93, 46);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(29, 502);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(93, 46);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(148, 248);
            lblType.Name = "lblType";
            lblType.Size = new Size(56, 15);
            lblType.TabIndex = 26;
            lblType.Text = "Visit Type";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(28, 169);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 31;
            lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(28, 187);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(220, 23);
            txtDescription.TabIndex = 30;
            // 
            // cbxSTime
            // 
            cbxSTime.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSTime.FormatString = "g";
            cbxSTime.FormattingEnabled = true;
            cbxSTime.Location = new Point(28, 378);
            cbxSTime.Name = "cbxSTime";
            cbxSTime.Size = new Size(220, 23);
            cbxSTime.TabIndex = 32;
            // 
            // cbxLocation
            // 
            cbxLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxLocation.FormattingEnabled = true;
            cbxLocation.Location = new Point(28, 266);
            cbxLocation.Name = "cbxLocation";
            cbxLocation.Size = new Size(94, 23);
            cbxLocation.TabIndex = 33;
            // 
            // cbxType
            // 
            cbxType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxType.FormattingEnabled = true;
            cbxType.Location = new Point(148, 266);
            cbxType.Name = "cbxType";
            cbxType.Size = new Size(100, 23);
            cbxType.TabIndex = 34;
            // 
            // cbxConsultant
            // 
            cbxConsultant.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxConsultant.FormattingEnabled = true;
            cbxConsultant.Location = new Point(28, 132);
            cbxConsultant.Name = "cbxConsultant";
            cbxConsultant.Size = new Size(220, 23);
            cbxConsultant.TabIndex = 35;
            // 
            // cbxName
            // 
            cbxName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxName.FormattingEnabled = true;
            cbxName.Location = new Point(28, 77);
            cbxName.Name = "cbxName";
            cbxName.Size = new Size(220, 23);
            cbxName.TabIndex = 36;
            // 
            // dtpDay
            // 
            dtpDay.Location = new Point(28, 324);
            dtpDay.Name = "dtpDay";
            dtpDay.Size = new Size(220, 23);
            dtpDay.TabIndex = 37;
            dtpDay.ValueChanged += dtpDay_ValueChanged;
            // 
            // cbxETime
            // 
            cbxETime.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxETime.FormattingEnabled = true;
            cbxETime.Location = new Point(29, 434);
            cbxETime.Name = "cbxETime";
            cbxETime.Size = new Size(219, 23);
            cbxETime.TabIndex = 39;
            // 
            // lblETime
            // 
            lblETime.AutoSize = true;
            lblETime.Location = new Point(29, 416);
            lblETime.Name = "lblETime";
            lblETime.Size = new Size(56, 15);
            lblETime.TabIndex = 38;
            lblETime.Text = "End Time";
            // 
            // AppointmentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 560);
            Controls.Add(cbxETime);
            Controls.Add(lblETime);
            Controls.Add(dtpDay);
            Controls.Add(cbxName);
            Controls.Add(cbxConsultant);
            Controls.Add(cbxType);
            Controls.Add(cbxLocation);
            Controls.Add(cbxSTime);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(lblHeader);
            Controls.Add(lblSTime);
            Controls.Add(lblDay);
            Controls.Add(lblType);
            Controls.Add(lblLocation);
            Controls.Add(lblConsultant);
            Controls.Add(lblName);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "AppointmentForm";
            Text = "NewAppointmentForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeader;
        private Label lblSTime;
        private Label lblDay;
        private Label lblLocation;
        private Label lblConsultant;
        private Label lblName;
        private Button btnSave;
        private Button btnCancel;
        private Label lblType;
        private Label lblDescription;
        private TextBox txtDescription;
        private ComboBox cbxSTime;
        private ComboBox cbxLocation;
        private ComboBox cbxType;
        private ComboBox cbxConsultant;
        private ComboBox cbxName;
        private DateTimePicker dtpDay;
        private ComboBox cbxETime;
        private Label lblETime;
    }
}