namespace Apointment_Scheduler.Forms
{
    partial class CustomerForm
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
            components = new System.ComponentModel.Container();
            btnCancel = new Button();
            btnSave = new Button();
            txtName = new TextBox();
            txtAddress = new TextBox();
            txtCity = new TextBox();
            txtZip = new TextBox();
            txtCountry = new TextBox();
            lblName = new Label();
            lblAddress = new Label();
            lblCity = new Label();
            lblZip = new Label();
            lblCountry = new Label();
            lblPhone = new Label();
            lblHeader = new Label();
            errorProvider = new ErrorProvider(components);
            txtPhone = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(34, 373);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(93, 46);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(175, 373);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(93, 46);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(34, 86);
            txtName.Name = "txtName";
            txtName.Size = new Size(220, 23);
            txtName.TabIndex = 2;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(34, 141);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(220, 23);
            txtAddress.TabIndex = 3;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(34, 196);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(100, 23);
            txtCity.TabIndex = 4;
            // 
            // txtZip
            // 
            txtZip.Location = new Point(154, 196);
            txtZip.Name = "txtZip";
            txtZip.Size = new Size(100, 23);
            txtZip.TabIndex = 5;
            // 
            // txtCountry
            // 
            txtCountry.Location = new Point(34, 254);
            txtCountry.Name = "txtCountry";
            txtCountry.Size = new Size(125, 23);
            txtCountry.TabIndex = 6;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(34, 68);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 8;
            lblName.Text = "Name";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(34, 123);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(49, 15);
            lblAddress.TabIndex = 9;
            lblAddress.Text = "Address";
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Location = new Point(34, 178);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(28, 15);
            lblCity.TabIndex = 10;
            lblCity.Text = "City";
            // 
            // lblZip
            // 
            lblZip.AutoSize = true;
            lblZip.Location = new Point(154, 178);
            lblZip.Name = "lblZip";
            lblZip.Size = new Size(55, 15);
            lblZip.TabIndex = 11;
            lblZip.Text = "Zip Code";
            // 
            // lblCountry
            // 
            lblCountry.AutoSize = true;
            lblCountry.Location = new Point(34, 236);
            lblCountry.Name = "lblCountry";
            lblCountry.Size = new Size(50, 15);
            lblCountry.TabIndex = 12;
            lblCountry.Text = "Country";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(34, 290);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(88, 15);
            lblPhone.TabIndex = 13;
            lblPhone.Text = "Phone Number";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(103, 29);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(77, 20);
            lblHeader.TabIndex = 14;
            lblHeader.Text = "Customer";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(34, 308);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(100, 23);
            txtPhone.TabIndex = 15;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 455);
            Controls.Add(txtPhone);
            Controls.Add(lblHeader);
            Controls.Add(lblPhone);
            Controls.Add(lblCountry);
            Controls.Add(lblZip);
            Controls.Add(lblCity);
            Controls.Add(lblAddress);
            Controls.Add(lblName);
            Controls.Add(txtCountry);
            Controls.Add(txtZip);
            Controls.Add(txtCity);
            Controls.Add(txtAddress);
            Controls.Add(txtName);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "CustomerForm";
            Text = "CustomerForm";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnSave;
        private TextBox txtName;
        private TextBox txtAddress;
        private TextBox txtCity;
        private TextBox txtZip;
        private TextBox txtCountry;
        private Label lblName;
        private Label lblAddress;
        private Label lblCity;
        private Label lblZip;
        private Label lblCountry;
        private Label lblPhone;
        private Label lblHeader;
        private ErrorProvider errorProvider;
        private TextBox txtPhone;
        //private TextBox txtPhone;
    }
}