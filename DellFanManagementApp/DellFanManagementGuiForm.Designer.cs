
namespace DellFanManagement.App
{
    partial class DellFanManagementGuiForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DellFanManagementGuiForm));
            this.fansGroupBox = new System.Windows.Forms.GroupBox();
            this.fan2RpmLabel = new System.Windows.Forms.Label();
            this.fan1RpmLabel = new System.Windows.Forms.Label();
            this.thermalSettingGroupBox = new System.Windows.Forms.GroupBox();
            this.thermalSettingRadioButtonPerformance = new System.Windows.Forms.RadioButton();
            this.thermalSettingRadioButtonQuiet = new System.Windows.Forms.RadioButton();
            this.thermalSettingRadioButtonCool = new System.Windows.Forms.RadioButton();
            this.thermalSettingRadioButtonOptimized = new System.Windows.Forms.RadioButton();
            this.temperatureGroupBox = new System.Windows.Forms.GroupBox();
            this.temperatureLabel18 = new System.Windows.Forms.Label();
            this.temperatureLabel17 = new System.Windows.Forms.Label();
            this.temperatureLabel16 = new System.Windows.Forms.Label();
            this.temperatureLabel15 = new System.Windows.Forms.Label();
            this.temperatureLabel14 = new System.Windows.Forms.Label();
            this.temperatureLabel13 = new System.Windows.Forms.Label();
            this.temperatureLabel12 = new System.Windows.Forms.Label();
            this.temperatureLabel11 = new System.Windows.Forms.Label();
            this.temperatureLabel10 = new System.Windows.Forms.Label();
            this.temperatureLabel9 = new System.Windows.Forms.Label();
            this.temperatureLabel8 = new System.Windows.Forms.Label();
            this.temperatureLabel7 = new System.Windows.Forms.Label();
            this.temperatureLabel6 = new System.Windows.Forms.Label();
            this.temperatureLabel5 = new System.Windows.Forms.Label();
            this.temperatureLabel4 = new System.Windows.Forms.Label();
            this.temperatureLabel3 = new System.Windows.Forms.Label();
            this.temperatureLabel2 = new System.Windows.Forms.Label();
            this.temperatureLabel1 = new System.Windows.Forms.Label();
            this.operationModeGroupBox = new System.Windows.Forms.GroupBox();
            this.operationModeRadioButtonConsistency = new System.Windows.Forms.RadioButton();
            this.operationModeRadioButtonManual = new System.Windows.Forms.RadioButton();
            this.operationModeRadioButtonAutomatic = new System.Windows.Forms.RadioButton();
            this.manualGroupBox = new System.Windows.Forms.GroupBox();
            this.manualFan2GroupBox = new System.Windows.Forms.GroupBox();
            this.manualFan2RadioButtonHigh = new System.Windows.Forms.RadioButton();
            this.manualFan2RadioButtonMedium = new System.Windows.Forms.RadioButton();
            this.manualFan2RadioButtonOff = new System.Windows.Forms.RadioButton();
            this.manualFan1GroupBox = new System.Windows.Forms.GroupBox();
            this.manualFan1RadioButtonHigh = new System.Windows.Forms.RadioButton();
            this.manualFan1RadioButtonMedium = new System.Windows.Forms.RadioButton();
            this.manualFan1RadioButtonOff = new System.Windows.Forms.RadioButton();
            this.consistencyModeGroupBox = new System.Windows.Forms.GroupBox();
            this.alertsCheckBox = new System.Windows.Forms.CheckBox();
            this.consistencyModeApplyChangesButton = new System.Windows.Forms.Button();
            this.consistencyModeRpmThresholdTextBox = new System.Windows.Forms.TextBox();
            this.consistencyModeRpmThresholdLabel = new System.Windows.Forms.Label();
            this.consistencyModeUpperTemperatureThresholdTextBox = new System.Windows.Forms.TextBox();
            this.consistencyModeUpperTemperatureThresholdLabel = new System.Windows.Forms.Label();
            this.consistencyModeLowerTemperatureThresholdTextBox = new System.Windows.Forms.TextBox();
            this.consistencyModeLowerTemperatureThresholdLabel = new System.Windows.Forms.Label();
            this.ecFanControlRadioButtonOn = new System.Windows.Forms.RadioButton();
            this.ecFanControlRadioButtonOff = new System.Windows.Forms.RadioButton();
            this.ecFanControlGroupBox = new System.Windows.Forms.GroupBox();
            this.restartBackgroundThreadButton = new System.Windows.Forms.Button();
            this.aboutGroupBox = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.aboutProductLabel = new System.Windows.Forms.Label();
            this.audioKeepAliveGroupBox = new System.Windows.Forms.GroupBox();
            this.audioKeepAliveComboBox = new System.Windows.Forms.ComboBox();
            this.audioKeepAliveCheckbox = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.consistencyModeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.animatedCheckBox = new System.Windows.Forms.CheckBox();
            this.fansGroupBox.SuspendLayout();
            this.thermalSettingGroupBox.SuspendLayout();
            this.temperatureGroupBox.SuspendLayout();
            this.operationModeGroupBox.SuspendLayout();
            this.manualGroupBox.SuspendLayout();
            this.manualFan2GroupBox.SuspendLayout();
            this.manualFan1GroupBox.SuspendLayout();
            this.consistencyModeGroupBox.SuspendLayout();
            this.ecFanControlGroupBox.SuspendLayout();
            this.aboutGroupBox.SuspendLayout();
            this.audioKeepAliveGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fansGroupBox
            // 
            this.fansGroupBox.Controls.Add(this.fan2RpmLabel);
            this.fansGroupBox.Controls.Add(this.fan1RpmLabel);
            this.fansGroupBox.Location = new System.Drawing.Point(12, 12);
            this.fansGroupBox.Name = "fansGroupBox";
            this.fansGroupBox.Size = new System.Drawing.Size(161, 65);
            this.fansGroupBox.TabIndex = 6;
            this.fansGroupBox.TabStop = false;
            this.fansGroupBox.Text = "Fans:";
            // 
            // fan2RpmLabel
            // 
            this.fan2RpmLabel.AutoSize = true;
            this.fan2RpmLabel.Location = new System.Drawing.Point(6, 38);
            this.fan2RpmLabel.Name = "fan2RpmLabel";
            this.fan2RpmLabel.Size = new System.Drawing.Size(152, 15);
            this.fan2RpmLabel.TabIndex = 2;
            this.fan2RpmLabel.Text = "Fan 2 RPM: (Not measured)";
            // 
            // fan1RpmLabel
            // 
            this.fan1RpmLabel.AutoSize = true;
            this.fan1RpmLabel.Location = new System.Drawing.Point(6, 19);
            this.fan1RpmLabel.Name = "fan1RpmLabel";
            this.fan1RpmLabel.Size = new System.Drawing.Size(152, 15);
            this.fan1RpmLabel.TabIndex = 1;
            this.fan1RpmLabel.Text = "Fan 1 RPM: (Not measured)";
            // 
            // thermalSettingGroupBox
            // 
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonPerformance);
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonQuiet);
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonCool);
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonOptimized);
            this.thermalSettingGroupBox.Location = new System.Drawing.Point(288, 117);
            this.thermalSettingGroupBox.Name = "thermalSettingGroupBox";
            this.thermalSettingGroupBox.Size = new System.Drawing.Size(111, 124);
            this.thermalSettingGroupBox.TabIndex = 3;
            this.thermalSettingGroupBox.TabStop = false;
            this.thermalSettingGroupBox.Text = "Thermal setting:";
            // 
            // thermalSettingRadioButtonPerformance
            // 
            this.thermalSettingRadioButtonPerformance.AutoSize = true;
            this.thermalSettingRadioButtonPerformance.Location = new System.Drawing.Point(6, 97);
            this.thermalSettingRadioButtonPerformance.Name = "thermalSettingRadioButtonPerformance";
            this.thermalSettingRadioButtonPerformance.Size = new System.Drawing.Size(93, 19);
            this.thermalSettingRadioButtonPerformance.TabIndex = 3;
            this.thermalSettingRadioButtonPerformance.TabStop = true;
            this.thermalSettingRadioButtonPerformance.Text = "&Performance";
            this.thermalSettingRadioButtonPerformance.UseVisualStyleBackColor = true;
            // 
            // thermalSettingRadioButtonQuiet
            // 
            this.thermalSettingRadioButtonQuiet.AutoSize = true;
            this.thermalSettingRadioButtonQuiet.Location = new System.Drawing.Point(6, 72);
            this.thermalSettingRadioButtonQuiet.Name = "thermalSettingRadioButtonQuiet";
            this.thermalSettingRadioButtonQuiet.Size = new System.Drawing.Size(54, 19);
            this.thermalSettingRadioButtonQuiet.TabIndex = 2;
            this.thermalSettingRadioButtonQuiet.TabStop = true;
            this.thermalSettingRadioButtonQuiet.Text = "&Quiet";
            this.thermalSettingRadioButtonQuiet.UseVisualStyleBackColor = true;
            // 
            // thermalSettingRadioButtonCool
            // 
            this.thermalSettingRadioButtonCool.AutoSize = true;
            this.thermalSettingRadioButtonCool.Location = new System.Drawing.Point(6, 47);
            this.thermalSettingRadioButtonCool.Name = "thermalSettingRadioButtonCool";
            this.thermalSettingRadioButtonCool.Size = new System.Drawing.Size(50, 19);
            this.thermalSettingRadioButtonCool.TabIndex = 1;
            this.thermalSettingRadioButtonCool.TabStop = true;
            this.thermalSettingRadioButtonCool.Text = "&Cool";
            this.thermalSettingRadioButtonCool.UseVisualStyleBackColor = true;
            // 
            // thermalSettingRadioButtonOptimized
            // 
            this.thermalSettingRadioButtonOptimized.AutoSize = true;
            this.thermalSettingRadioButtonOptimized.Location = new System.Drawing.Point(6, 22);
            this.thermalSettingRadioButtonOptimized.Name = "thermalSettingRadioButtonOptimized";
            this.thermalSettingRadioButtonOptimized.Size = new System.Drawing.Size(80, 19);
            this.thermalSettingRadioButtonOptimized.TabIndex = 0;
            this.thermalSettingRadioButtonOptimized.TabStop = true;
            this.thermalSettingRadioButtonOptimized.Text = "&Optimized";
            this.thermalSettingRadioButtonOptimized.UseVisualStyleBackColor = true;
            // 
            // temperatureGroupBox
            // 
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel18);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel17);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel16);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel15);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel14);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel13);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel12);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel11);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel10);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel9);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel8);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel7);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel6);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel5);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel4);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel3);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel2);
            this.temperatureGroupBox.Controls.Add(this.temperatureLabel1);
            this.temperatureGroupBox.Location = new System.Drawing.Point(12, 83);
            this.temperatureGroupBox.Name = "temperatureGroupBox";
            this.temperatureGroupBox.Size = new System.Drawing.Size(270, 199);
            this.temperatureGroupBox.TabIndex = 7;
            this.temperatureGroupBox.TabStop = false;
            this.temperatureGroupBox.Text = "Temperatures:";
            // 
            // temperatureLabel18
            // 
            this.temperatureLabel18.AutoSize = true;
            this.temperatureLabel18.Location = new System.Drawing.Point(138, 175);
            this.temperatureLabel18.Name = "temperatureLabel18";
            this.temperatureLabel18.Size = new System.Drawing.Size(114, 15);
            this.temperatureLabel18.TabIndex = 3;
            this.temperatureLabel18.Text = "Quadro RTX 5000: 50";
            // 
            // temperatureLabel17
            // 
            this.temperatureLabel17.AutoSize = true;
            this.temperatureLabel17.Location = new System.Drawing.Point(138, 156);
            this.temperatureLabel17.Name = "temperatureLabel17";
            this.temperatureLabel17.Size = new System.Drawing.Size(95, 15);
            this.temperatureLabel17.TabIndex = 3;
            this.temperatureLabel17.Text = "CPU Package: 50";
            // 
            // temperatureLabel16
            // 
            this.temperatureLabel16.AutoSize = true;
            this.temperatureLabel16.Location = new System.Drawing.Point(138, 137);
            this.temperatureLabel16.Name = "temperatureLabel16";
            this.temperatureLabel16.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel16.TabIndex = 15;
            this.temperatureLabel16.Text = "CPU Core #16: 50";
            // 
            // temperatureLabel15
            // 
            this.temperatureLabel15.AutoSize = true;
            this.temperatureLabel15.Location = new System.Drawing.Point(138, 118);
            this.temperatureLabel15.Name = "temperatureLabel15";
            this.temperatureLabel15.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel15.TabIndex = 14;
            this.temperatureLabel15.Text = "CPU Core #15: 50";
            // 
            // temperatureLabel14
            // 
            this.temperatureLabel14.AutoSize = true;
            this.temperatureLabel14.Location = new System.Drawing.Point(138, 99);
            this.temperatureLabel14.Name = "temperatureLabel14";
            this.temperatureLabel14.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel14.TabIndex = 13;
            this.temperatureLabel14.Text = "CPU Core #14: 50";
            // 
            // temperatureLabel13
            // 
            this.temperatureLabel13.AutoSize = true;
            this.temperatureLabel13.Location = new System.Drawing.Point(138, 80);
            this.temperatureLabel13.Name = "temperatureLabel13";
            this.temperatureLabel13.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel13.TabIndex = 12;
            this.temperatureLabel13.Text = "CPU Core #13: 50";
            // 
            // temperatureLabel12
            // 
            this.temperatureLabel12.AutoSize = true;
            this.temperatureLabel12.Location = new System.Drawing.Point(138, 61);
            this.temperatureLabel12.Name = "temperatureLabel12";
            this.temperatureLabel12.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel12.TabIndex = 11;
            this.temperatureLabel12.Text = "CPU Core #12: 50";
            // 
            // temperatureLabel11
            // 
            this.temperatureLabel11.AutoSize = true;
            this.temperatureLabel11.Location = new System.Drawing.Point(138, 42);
            this.temperatureLabel11.Name = "temperatureLabel11";
            this.temperatureLabel11.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel11.TabIndex = 10;
            this.temperatureLabel11.Text = "CPU Core #11: 50";
            // 
            // temperatureLabel10
            // 
            this.temperatureLabel10.AutoSize = true;
            this.temperatureLabel10.Location = new System.Drawing.Point(138, 23);
            this.temperatureLabel10.Name = "temperatureLabel10";
            this.temperatureLabel10.Size = new System.Drawing.Size(98, 15);
            this.temperatureLabel10.TabIndex = 9;
            this.temperatureLabel10.Text = "CPU Core #10: 50";
            // 
            // temperatureLabel9
            // 
            this.temperatureLabel9.AutoSize = true;
            this.temperatureLabel9.Location = new System.Drawing.Point(6, 175);
            this.temperatureLabel9.Name = "temperatureLabel9";
            this.temperatureLabel9.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel9.TabIndex = 8;
            this.temperatureLabel9.Text = "CPU Core #9: 50";
            // 
            // temperatureLabel8
            // 
            this.temperatureLabel8.AutoSize = true;
            this.temperatureLabel8.Location = new System.Drawing.Point(6, 156);
            this.temperatureLabel8.Name = "temperatureLabel8";
            this.temperatureLabel8.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel8.TabIndex = 7;
            this.temperatureLabel8.Text = "CPU Core #8: 50";
            // 
            // temperatureLabel7
            // 
            this.temperatureLabel7.AutoSize = true;
            this.temperatureLabel7.Location = new System.Drawing.Point(6, 137);
            this.temperatureLabel7.Name = "temperatureLabel7";
            this.temperatureLabel7.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel7.TabIndex = 6;
            this.temperatureLabel7.Text = "CPU Core #7: 50";
            // 
            // temperatureLabel6
            // 
            this.temperatureLabel6.AutoSize = true;
            this.temperatureLabel6.Location = new System.Drawing.Point(6, 118);
            this.temperatureLabel6.Name = "temperatureLabel6";
            this.temperatureLabel6.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel6.TabIndex = 5;
            this.temperatureLabel6.Text = "CPU Core #6: 50";
            // 
            // temperatureLabel5
            // 
            this.temperatureLabel5.AutoSize = true;
            this.temperatureLabel5.Location = new System.Drawing.Point(6, 99);
            this.temperatureLabel5.Name = "temperatureLabel5";
            this.temperatureLabel5.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel5.TabIndex = 4;
            this.temperatureLabel5.Text = "CPU Core #5: 50";
            // 
            // temperatureLabel4
            // 
            this.temperatureLabel4.AutoSize = true;
            this.temperatureLabel4.Location = new System.Drawing.Point(6, 80);
            this.temperatureLabel4.Name = "temperatureLabel4";
            this.temperatureLabel4.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel4.TabIndex = 3;
            this.temperatureLabel4.Text = "CPU Core #4: 50";
            // 
            // temperatureLabel3
            // 
            this.temperatureLabel3.AutoSize = true;
            this.temperatureLabel3.Location = new System.Drawing.Point(6, 61);
            this.temperatureLabel3.Name = "temperatureLabel3";
            this.temperatureLabel3.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel3.TabIndex = 2;
            this.temperatureLabel3.Text = "CPU Core #3: 50";
            // 
            // temperatureLabel2
            // 
            this.temperatureLabel2.AutoSize = true;
            this.temperatureLabel2.Location = new System.Drawing.Point(6, 42);
            this.temperatureLabel2.Name = "temperatureLabel2";
            this.temperatureLabel2.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel2.TabIndex = 1;
            this.temperatureLabel2.Text = "CPU Core #2: 50";
            // 
            // temperatureLabel1
            // 
            this.temperatureLabel1.AutoSize = true;
            this.temperatureLabel1.Location = new System.Drawing.Point(6, 23);
            this.temperatureLabel1.Name = "temperatureLabel1";
            this.temperatureLabel1.Size = new System.Drawing.Size(92, 15);
            this.temperatureLabel1.TabIndex = 0;
            this.temperatureLabel1.Text = "CPU Core #1: 50";
            // 
            // operationModeGroupBox
            // 
            this.operationModeGroupBox.Controls.Add(this.operationModeRadioButtonConsistency);
            this.operationModeGroupBox.Controls.Add(this.operationModeRadioButtonManual);
            this.operationModeGroupBox.Controls.Add(this.operationModeRadioButtonAutomatic);
            this.operationModeGroupBox.Location = new System.Drawing.Point(288, 12);
            this.operationModeGroupBox.Name = "operationModeGroupBox";
            this.operationModeGroupBox.Size = new System.Drawing.Size(111, 99);
            this.operationModeGroupBox.TabIndex = 2;
            this.operationModeGroupBox.TabStop = false;
            this.operationModeGroupBox.Text = "Operation mode:";
            // 
            // operationModeRadioButtonConsistency
            // 
            this.operationModeRadioButtonConsistency.AutoSize = true;
            this.operationModeRadioButtonConsistency.Location = new System.Drawing.Point(6, 72);
            this.operationModeRadioButtonConsistency.Name = "operationModeRadioButtonConsistency";
            this.operationModeRadioButtonConsistency.Size = new System.Drawing.Size(89, 19);
            this.operationModeRadioButtonConsistency.TabIndex = 2;
            this.operationModeRadioButtonConsistency.TabStop = true;
            this.operationModeRadioButtonConsistency.Text = "Con&sistency";
            this.operationModeRadioButtonConsistency.UseVisualStyleBackColor = true;
            // 
            // operationModeRadioButtonManual
            // 
            this.operationModeRadioButtonManual.AutoSize = true;
            this.operationModeRadioButtonManual.Location = new System.Drawing.Point(6, 47);
            this.operationModeRadioButtonManual.Name = "operationModeRadioButtonManual";
            this.operationModeRadioButtonManual.Size = new System.Drawing.Size(65, 19);
            this.operationModeRadioButtonManual.TabIndex = 1;
            this.operationModeRadioButtonManual.TabStop = true;
            this.operationModeRadioButtonManual.Text = "&Manual";
            this.operationModeRadioButtonManual.UseVisualStyleBackColor = true;
            // 
            // operationModeRadioButtonAutomatic
            // 
            this.operationModeRadioButtonAutomatic.AutoSize = true;
            this.operationModeRadioButtonAutomatic.Location = new System.Drawing.Point(6, 22);
            this.operationModeRadioButtonAutomatic.Name = "operationModeRadioButtonAutomatic";
            this.operationModeRadioButtonAutomatic.Size = new System.Drawing.Size(81, 19);
            this.operationModeRadioButtonAutomatic.TabIndex = 0;
            this.operationModeRadioButtonAutomatic.TabStop = true;
            this.operationModeRadioButtonAutomatic.Text = "&Automatic";
            this.operationModeRadioButtonAutomatic.UseVisualStyleBackColor = true;
            // 
            // manualGroupBox
            // 
            this.manualGroupBox.Controls.Add(this.manualFan2GroupBox);
            this.manualGroupBox.Controls.Add(this.manualFan1GroupBox);
            this.manualGroupBox.Location = new System.Drawing.Point(405, 12);
            this.manualGroupBox.Name = "manualGroupBox";
            this.manualGroupBox.Size = new System.Drawing.Size(194, 126);
            this.manualGroupBox.TabIndex = 4;
            this.manualGroupBox.TabStop = false;
            this.manualGroupBox.Text = "Manual control:";
            // 
            // manualFan2GroupBox
            // 
            this.manualFan2GroupBox.Controls.Add(this.manualFan2RadioButtonHigh);
            this.manualFan2GroupBox.Controls.Add(this.manualFan2RadioButtonMedium);
            this.manualFan2GroupBox.Controls.Add(this.manualFan2RadioButtonOff);
            this.manualFan2GroupBox.Location = new System.Drawing.Point(100, 21);
            this.manualFan2GroupBox.Name = "manualFan2GroupBox";
            this.manualFan2GroupBox.Size = new System.Drawing.Size(88, 99);
            this.manualFan2GroupBox.TabIndex = 1;
            this.manualFan2GroupBox.TabStop = false;
            this.manualFan2GroupBox.Text = "Fan 2:";
            // 
            // manualFan2RadioButtonHigh
            // 
            this.manualFan2RadioButtonHigh.AutoSize = true;
            this.manualFan2RadioButtonHigh.Location = new System.Drawing.Point(6, 72);
            this.manualFan2RadioButtonHigh.Name = "manualFan2RadioButtonHigh";
            this.manualFan2RadioButtonHigh.Size = new System.Drawing.Size(51, 19);
            this.manualFan2RadioButtonHigh.TabIndex = 5;
            this.manualFan2RadioButtonHigh.TabStop = true;
            this.manualFan2RadioButtonHigh.Text = "High";
            this.manualFan2RadioButtonHigh.UseVisualStyleBackColor = true;
            // 
            // manualFan2RadioButtonMedium
            // 
            this.manualFan2RadioButtonMedium.AutoSize = true;
            this.manualFan2RadioButtonMedium.Location = new System.Drawing.Point(6, 47);
            this.manualFan2RadioButtonMedium.Name = "manualFan2RadioButtonMedium";
            this.manualFan2RadioButtonMedium.Size = new System.Drawing.Size(70, 19);
            this.manualFan2RadioButtonMedium.TabIndex = 5;
            this.manualFan2RadioButtonMedium.TabStop = true;
            this.manualFan2RadioButtonMedium.Text = "Medium";
            this.manualFan2RadioButtonMedium.UseVisualStyleBackColor = true;
            // 
            // manualFan2RadioButtonOff
            // 
            this.manualFan2RadioButtonOff.AutoSize = true;
            this.manualFan2RadioButtonOff.Location = new System.Drawing.Point(6, 22);
            this.manualFan2RadioButtonOff.Name = "manualFan2RadioButtonOff";
            this.manualFan2RadioButtonOff.Size = new System.Drawing.Size(42, 19);
            this.manualFan2RadioButtonOff.TabIndex = 5;
            this.manualFan2RadioButtonOff.TabStop = true;
            this.manualFan2RadioButtonOff.Text = "Off";
            this.manualFan2RadioButtonOff.UseVisualStyleBackColor = true;
            // 
            // manualFan1GroupBox
            // 
            this.manualFan1GroupBox.Controls.Add(this.manualFan1RadioButtonHigh);
            this.manualFan1GroupBox.Controls.Add(this.manualFan1RadioButtonMedium);
            this.manualFan1GroupBox.Controls.Add(this.manualFan1RadioButtonOff);
            this.manualFan1GroupBox.Location = new System.Drawing.Point(6, 21);
            this.manualFan1GroupBox.Name = "manualFan1GroupBox";
            this.manualFan1GroupBox.Size = new System.Drawing.Size(88, 99);
            this.manualFan1GroupBox.TabIndex = 0;
            this.manualFan1GroupBox.TabStop = false;
            this.manualFan1GroupBox.Text = "Fan 1:";
            // 
            // manualFan1RadioButtonHigh
            // 
            this.manualFan1RadioButtonHigh.AutoSize = true;
            this.manualFan1RadioButtonHigh.Location = new System.Drawing.Point(6, 72);
            this.manualFan1RadioButtonHigh.Name = "manualFan1RadioButtonHigh";
            this.manualFan1RadioButtonHigh.Size = new System.Drawing.Size(51, 19);
            this.manualFan1RadioButtonHigh.TabIndex = 2;
            this.manualFan1RadioButtonHigh.TabStop = true;
            this.manualFan1RadioButtonHigh.Text = "High";
            this.manualFan1RadioButtonHigh.UseVisualStyleBackColor = true;
            // 
            // manualFan1RadioButtonMedium
            // 
            this.manualFan1RadioButtonMedium.AutoSize = true;
            this.manualFan1RadioButtonMedium.Location = new System.Drawing.Point(6, 47);
            this.manualFan1RadioButtonMedium.Name = "manualFan1RadioButtonMedium";
            this.manualFan1RadioButtonMedium.Size = new System.Drawing.Size(70, 19);
            this.manualFan1RadioButtonMedium.TabIndex = 1;
            this.manualFan1RadioButtonMedium.TabStop = true;
            this.manualFan1RadioButtonMedium.Text = "Medium";
            this.manualFan1RadioButtonMedium.UseVisualStyleBackColor = true;
            // 
            // manualFan1RadioButtonOff
            // 
            this.manualFan1RadioButtonOff.AutoSize = true;
            this.manualFan1RadioButtonOff.Location = new System.Drawing.Point(6, 22);
            this.manualFan1RadioButtonOff.Name = "manualFan1RadioButtonOff";
            this.manualFan1RadioButtonOff.Size = new System.Drawing.Size(42, 19);
            this.manualFan1RadioButtonOff.TabIndex = 0;
            this.manualFan1RadioButtonOff.TabStop = true;
            this.manualFan1RadioButtonOff.Text = "Off";
            this.manualFan1RadioButtonOff.UseVisualStyleBackColor = true;
            // 
            // consistencyModeGroupBox
            // 
            this.consistencyModeGroupBox.Controls.Add(this.alertsCheckBox);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeApplyChangesButton);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeRpmThresholdTextBox);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeRpmThresholdLabel);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeUpperTemperatureThresholdTextBox);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeUpperTemperatureThresholdLabel);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeLowerTemperatureThresholdTextBox);
            this.consistencyModeGroupBox.Controls.Add(this.consistencyModeLowerTemperatureThresholdLabel);
            this.consistencyModeGroupBox.Location = new System.Drawing.Point(405, 144);
            this.consistencyModeGroupBox.Name = "consistencyModeGroupBox";
            this.consistencyModeGroupBox.Size = new System.Drawing.Size(194, 138);
            this.consistencyModeGroupBox.TabIndex = 5;
            this.consistencyModeGroupBox.TabStop = false;
            this.consistencyModeGroupBox.Text = "Consistency mode options:";
            // 
            // alertsCheckBox
            // 
            this.alertsCheckBox.AutoSize = true;
            this.alertsCheckBox.Enabled = false;
            this.alertsCheckBox.Location = new System.Drawing.Point(137, 80);
            this.alertsCheckBox.Name = "alertsCheckBox";
            this.alertsCheckBox.Size = new System.Drawing.Size(56, 19);
            this.alertsCheckBox.TabIndex = 13;
            this.alertsCheckBox.Text = "A&lerts";
            this.alertsCheckBox.UseVisualStyleBackColor = true;
            // 
            // consistencyModeApplyChangesButton
            // 
            this.consistencyModeApplyChangesButton.Location = new System.Drawing.Point(5, 107);
            this.consistencyModeApplyChangesButton.Name = "consistencyModeApplyChangesButton";
            this.consistencyModeApplyChangesButton.Size = new System.Drawing.Size(184, 25);
            this.consistencyModeApplyChangesButton.TabIndex = 6;
            this.consistencyModeApplyChangesButton.Text = "Appl&y changes";
            this.consistencyModeApplyChangesButton.UseVisualStyleBackColor = true;
            // 
            // consistencyModeRpmThresholdTextBox
            // 
            this.consistencyModeRpmThresholdTextBox.Location = new System.Drawing.Point(96, 78);
            this.consistencyModeRpmThresholdTextBox.MaxLength = 4;
            this.consistencyModeRpmThresholdTextBox.Name = "consistencyModeRpmThresholdTextBox";
            this.consistencyModeRpmThresholdTextBox.Size = new System.Drawing.Size(30, 23);
            this.consistencyModeRpmThresholdTextBox.TabIndex = 5;
            this.consistencyModeRpmThresholdTextBox.Text = "2900";
            // 
            // consistencyModeRpmThresholdLabel
            // 
            this.consistencyModeRpmThresholdLabel.AutoSize = true;
            this.consistencyModeRpmThresholdLabel.Location = new System.Drawing.Point(7, 81);
            this.consistencyModeRpmThresholdLabel.Name = "consistencyModeRpmThresholdLabel";
            this.consistencyModeRpmThresholdLabel.Size = new System.Drawing.Size(88, 15);
            this.consistencyModeRpmThresholdLabel.TabIndex = 4;
            this.consistencyModeRpmThresholdLabel.Text = "RPM threshold:";
            // 
            // consistencyModeUpperTemperatureThresholdTextBox
            // 
            this.consistencyModeUpperTemperatureThresholdTextBox.Location = new System.Drawing.Point(170, 49);
            this.consistencyModeUpperTemperatureThresholdTextBox.MaxLength = 2;
            this.consistencyModeUpperTemperatureThresholdTextBox.Name = "consistencyModeUpperTemperatureThresholdTextBox";
            this.consistencyModeUpperTemperatureThresholdTextBox.Size = new System.Drawing.Size(18, 23);
            this.consistencyModeUpperTemperatureThresholdTextBox.TabIndex = 3;
            this.consistencyModeUpperTemperatureThresholdTextBox.Text = "85";
            // 
            // consistencyModeUpperTemperatureThresholdLabel
            // 
            this.consistencyModeUpperTemperatureThresholdLabel.AutoSize = true;
            this.consistencyModeUpperTemperatureThresholdLabel.Location = new System.Drawing.Point(6, 52);
            this.consistencyModeUpperTemperatureThresholdLabel.Name = "consistencyModeUpperTemperatureThresholdLabel";
            this.consistencyModeUpperTemperatureThresholdLabel.Size = new System.Drawing.Size(163, 15);
            this.consistencyModeUpperTemperatureThresholdLabel.TabIndex = 2;
            this.consistencyModeUpperTemperatureThresholdLabel.Text = "Upper temperature threshold:";
            // 
            // consistencyModeLowerTemperatureThresholdTextBox
            // 
            this.consistencyModeLowerTemperatureThresholdTextBox.Location = new System.Drawing.Point(170, 20);
            this.consistencyModeLowerTemperatureThresholdTextBox.MaxLength = 2;
            this.consistencyModeLowerTemperatureThresholdTextBox.Name = "consistencyModeLowerTemperatureThresholdTextBox";
            this.consistencyModeLowerTemperatureThresholdTextBox.Size = new System.Drawing.Size(18, 23);
            this.consistencyModeLowerTemperatureThresholdTextBox.TabIndex = 1;
            this.consistencyModeLowerTemperatureThresholdTextBox.Text = "70";
            // 
            // consistencyModeLowerTemperatureThresholdLabel
            // 
            this.consistencyModeLowerTemperatureThresholdLabel.AutoSize = true;
            this.consistencyModeLowerTemperatureThresholdLabel.Location = new System.Drawing.Point(6, 23);
            this.consistencyModeLowerTemperatureThresholdLabel.Name = "consistencyModeLowerTemperatureThresholdLabel";
            this.consistencyModeLowerTemperatureThresholdLabel.Size = new System.Drawing.Size(163, 15);
            this.consistencyModeLowerTemperatureThresholdLabel.TabIndex = 0;
            this.consistencyModeLowerTemperatureThresholdLabel.Text = "Lower temperature threshold:";
            // 
            // ecFanControlRadioButtonOn
            // 
            this.ecFanControlRadioButtonOn.AutoSize = true;
            this.ecFanControlRadioButtonOn.Location = new System.Drawing.Point(6, 22);
            this.ecFanControlRadioButtonOn.Name = "ecFanControlRadioButtonOn";
            this.ecFanControlRadioButtonOn.Size = new System.Drawing.Size(41, 19);
            this.ecFanControlRadioButtonOn.TabIndex = 2;
            this.ecFanControlRadioButtonOn.TabStop = true;
            this.ecFanControlRadioButtonOn.Text = "O&n";
            this.ecFanControlRadioButtonOn.UseVisualStyleBackColor = true;
            // 
            // ecFanControlRadioButtonOff
            // 
            this.ecFanControlRadioButtonOff.AutoSize = true;
            this.ecFanControlRadioButtonOff.Location = new System.Drawing.Point(53, 22);
            this.ecFanControlRadioButtonOff.Name = "ecFanControlRadioButtonOff";
            this.ecFanControlRadioButtonOff.Size = new System.Drawing.Size(42, 19);
            this.ecFanControlRadioButtonOff.TabIndex = 3;
            this.ecFanControlRadioButtonOff.TabStop = true;
            this.ecFanControlRadioButtonOff.Text = "O&ff";
            this.ecFanControlRadioButtonOff.UseVisualStyleBackColor = true;
            // 
            // ecFanControlGroupBox
            // 
            this.ecFanControlGroupBox.Controls.Add(this.ecFanControlRadioButtonOn);
            this.ecFanControlGroupBox.Controls.Add(this.ecFanControlRadioButtonOff);
            this.ecFanControlGroupBox.Location = new System.Drawing.Point(179, 12);
            this.ecFanControlGroupBox.Name = "ecFanControlGroupBox";
            this.ecFanControlGroupBox.Size = new System.Drawing.Size(103, 48);
            this.ecFanControlGroupBox.TabIndex = 0;
            this.ecFanControlGroupBox.TabStop = false;
            this.ecFanControlGroupBox.Text = "EC fan control:";
            // 
            // restartBackgroundThreadButton
            // 
            this.restartBackgroundThreadButton.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.restartBackgroundThreadButton.Location = new System.Drawing.Point(178, 65);
            this.restartBackgroundThreadButton.Name = "restartBackgroundThreadButton";
            this.restartBackgroundThreadButton.Size = new System.Drawing.Size(105, 21);
            this.restartBackgroundThreadButton.TabIndex = 1;
            this.restartBackgroundThreadButton.Text = "&Restart BG thread";
            this.restartBackgroundThreadButton.UseVisualStyleBackColor = true;
            // 
            // aboutGroupBox
            // 
            this.aboutGroupBox.Controls.Add(this.richTextBox1);
            this.aboutGroupBox.Controls.Add(this.aboutProductLabel);
            this.aboutGroupBox.Location = new System.Drawing.Point(605, 12);
            this.aboutGroupBox.Name = "aboutGroupBox";
            this.aboutGroupBox.Size = new System.Drawing.Size(334, 185);
            this.aboutGroupBox.TabIndex = 8;
            this.aboutGroupBox.TabStop = false;
            this.aboutGroupBox.Text = "About:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(322, 141);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // aboutProductLabel
            // 
            this.aboutProductLabel.AutoSize = true;
            this.aboutProductLabel.Location = new System.Drawing.Point(6, 19);
            this.aboutProductLabel.Name = "aboutProductLabel";
            this.aboutProductLabel.Size = new System.Drawing.Size(191, 15);
            this.aboutProductLabel.TabIndex = 0;
            this.aboutProductLabel.Text = "Dell Fan Management, version DEV";
            // 
            // audioKeepAliveGroupBox
            // 
            this.audioKeepAliveGroupBox.Controls.Add(this.audioKeepAliveComboBox);
            this.audioKeepAliveGroupBox.Controls.Add(this.audioKeepAliveCheckbox);
            this.audioKeepAliveGroupBox.Location = new System.Drawing.Point(605, 203);
            this.audioKeepAliveGroupBox.Name = "audioKeepAliveGroupBox";
            this.audioKeepAliveGroupBox.Size = new System.Drawing.Size(334, 79);
            this.audioKeepAliveGroupBox.TabIndex = 9;
            this.audioKeepAliveGroupBox.TabStop = false;
            this.audioKeepAliveGroupBox.Text = "Audio keep alive:";
            // 
            // audioKeepAliveComboBox
            // 
            this.audioKeepAliveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioKeepAliveComboBox.FormattingEnabled = true;
            this.audioKeepAliveComboBox.Location = new System.Drawing.Point(6, 49);
            this.audioKeepAliveComboBox.Name = "audioKeepAliveComboBox";
            this.audioKeepAliveComboBox.Size = new System.Drawing.Size(322, 23);
            this.audioKeepAliveComboBox.TabIndex = 1;
            // 
            // audioKeepAliveCheckbox
            // 
            this.audioKeepAliveCheckbox.AutoSize = true;
            this.audioKeepAliveCheckbox.Location = new System.Drawing.Point(6, 23);
            this.audioKeepAliveCheckbox.Name = "audioKeepAliveCheckbox";
            this.audioKeepAliveCheckbox.Size = new System.Drawing.Size(322, 19);
            this.audioKeepAliveCheckbox.TabIndex = 0;
            this.audioKeepAliveCheckbox.Text = "&Keep this audio device active to prevent pops and clicks:";
            this.audioKeepAliveCheckbox.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consistencyModeStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 293);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(951, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 11;
            // 
            // consistencyModeStatusLabel
            // 
            this.consistencyModeStatusLabel.Name = "consistencyModeStatusLabel";
            this.consistencyModeStatusLabel.Size = new System.Drawing.Size(109, 17);
            this.consistencyModeStatusLabel.Text = "Fan speed is locked";
            // 
            // trayIcon
            // 
            this.trayIcon.Text = "Dell Fan Management";
            this.trayIcon.Visible = true;
            // 
            // trayIconCheckBox
            // 
            this.trayIconCheckBox.AutoSize = true;
            this.trayIconCheckBox.Checked = true;
            this.trayIconCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.trayIconCheckBox.Location = new System.Drawing.Point(294, 246);
            this.trayIconCheckBox.Name = "trayIconCheckBox";
            this.trayIconCheckBox.Size = new System.Drawing.Size(73, 19);
            this.trayIconCheckBox.TabIndex = 12;
            this.trayIconCheckBox.Text = "&Tray icon";
            this.trayIconCheckBox.UseVisualStyleBackColor = true;
            // 
            // animatedCheckBox
            // 
            this.animatedCheckBox.AutoSize = true;
            this.animatedCheckBox.Checked = true;
            this.animatedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animatedCheckBox.Enabled = false;
            this.animatedCheckBox.Location = new System.Drawing.Point(294, 266);
            this.animatedCheckBox.Name = "animatedCheckBox";
            this.animatedCheckBox.Size = new System.Drawing.Size(78, 19);
            this.animatedCheckBox.TabIndex = 13;
            this.animatedCheckBox.Text = "An&imated";
            this.animatedCheckBox.UseVisualStyleBackColor = true;
            // 
            // DellFanManagementGuiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(951, 315);
            this.Controls.Add(this.animatedCheckBox);
            this.Controls.Add(this.trayIconCheckBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.audioKeepAliveGroupBox);
            this.Controls.Add(this.aboutGroupBox);
            this.Controls.Add(this.restartBackgroundThreadButton);
            this.Controls.Add(this.ecFanControlGroupBox);
            this.Controls.Add(this.consistencyModeGroupBox);
            this.Controls.Add(this.manualGroupBox);
            this.Controls.Add(this.operationModeGroupBox);
            this.Controls.Add(this.temperatureGroupBox);
            this.Controls.Add(this.thermalSettingGroupBox);
            this.Controls.Add(this.fansGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DellFanManagementGuiForm";
            this.Text = "Dell Fan Management";
            this.fansGroupBox.ResumeLayout(false);
            this.fansGroupBox.PerformLayout();
            this.thermalSettingGroupBox.ResumeLayout(false);
            this.thermalSettingGroupBox.PerformLayout();
            this.temperatureGroupBox.ResumeLayout(false);
            this.temperatureGroupBox.PerformLayout();
            this.operationModeGroupBox.ResumeLayout(false);
            this.operationModeGroupBox.PerformLayout();
            this.manualGroupBox.ResumeLayout(false);
            this.manualFan2GroupBox.ResumeLayout(false);
            this.manualFan2GroupBox.PerformLayout();
            this.manualFan1GroupBox.ResumeLayout(false);
            this.manualFan1GroupBox.PerformLayout();
            this.consistencyModeGroupBox.ResumeLayout(false);
            this.consistencyModeGroupBox.PerformLayout();
            this.ecFanControlGroupBox.ResumeLayout(false);
            this.ecFanControlGroupBox.PerformLayout();
            this.aboutGroupBox.ResumeLayout(false);
            this.aboutGroupBox.PerformLayout();
            this.audioKeepAliveGroupBox.ResumeLayout(false);
            this.audioKeepAliveGroupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox fansGroupBox;
        private System.Windows.Forms.Label fan1RpmLabel;
        private System.Windows.Forms.Label fan2RpmLabel;
        private System.Windows.Forms.GroupBox thermalSettingGroupBox;
        private System.Windows.Forms.RadioButton thermalSettingRadioButtonPerformance;
        private System.Windows.Forms.RadioButton thermalSettingRadioButtonQuiet;
        private System.Windows.Forms.RadioButton thermalSettingRadioButtonCool;
        private System.Windows.Forms.RadioButton thermalSettingRadioButtonOptimized;
        private System.Windows.Forms.GroupBox temperatureGroupBox;
        private System.Windows.Forms.Label temperatureLabel18;
        private System.Windows.Forms.Label temperatureLabel17;
        private System.Windows.Forms.Label temperatureLabel16;
        private System.Windows.Forms.Label temperatureLabel15;
        private System.Windows.Forms.Label temperatureLabel14;
        private System.Windows.Forms.Label temperatureLabel13;
        private System.Windows.Forms.Label temperatureLabel12;
        private System.Windows.Forms.Label temperatureLabel11;
        private System.Windows.Forms.Label temperatureLabel10;
        private System.Windows.Forms.Label temperatureLabel9;
        private System.Windows.Forms.Label temperatureLabel8;
        private System.Windows.Forms.Label temperatureLabel7;
        private System.Windows.Forms.Label temperatureLabel6;
        private System.Windows.Forms.Label temperatureLabel5;
        private System.Windows.Forms.Label temperatureLabel4;
        private System.Windows.Forms.Label temperatureLabel3;
        private System.Windows.Forms.Label temperatureLabel2;
        private System.Windows.Forms.Label temperatureLabel1;
        private System.Windows.Forms.GroupBox operationModeGroupBox;
        private System.Windows.Forms.RadioButton operationModeRadioButtonConsistency;
        private System.Windows.Forms.RadioButton operationModeRadioButtonManual;
        private System.Windows.Forms.RadioButton operationModeRadioButtonAutomatic;
        private System.Windows.Forms.GroupBox manualGroupBox;
        private System.Windows.Forms.GroupBox manualFan2GroupBox;
        private System.Windows.Forms.RadioButton manualFan2RadioButtonHigh;
        private System.Windows.Forms.RadioButton manualFan2RadioButtonMedium;
        private System.Windows.Forms.RadioButton manualFan2RadioButtonOff;
        private System.Windows.Forms.GroupBox manualFan1GroupBox;
        private System.Windows.Forms.RadioButton manualFan1RadioButtonHigh;
        private System.Windows.Forms.RadioButton manualFan1RadioButtonMedium;
        private System.Windows.Forms.RadioButton manualFan1RadioButtonOff;
        private System.Windows.Forms.GroupBox consistencyModeGroupBox;
        private System.Windows.Forms.Button consistencyModeApplyChangesButton;
        private System.Windows.Forms.TextBox consistencyModeRpmThresholdTextBox;
        private System.Windows.Forms.Label consistencyModeRpmThresholdLabel;
        private System.Windows.Forms.TextBox consistencyModeUpperTemperatureThresholdTextBox;
        private System.Windows.Forms.Label consistencyModeUpperTemperatureThresholdLabel;
        private System.Windows.Forms.TextBox consistencyModeLowerTemperatureThresholdTextBox;
        private System.Windows.Forms.Label consistencyModeLowerTemperatureThresholdLabel;
        private System.Windows.Forms.RadioButton ecFanControlRadioButtonOn;
        private System.Windows.Forms.RadioButton ecFanControlRadioButtonOff;
        private System.Windows.Forms.GroupBox ecFanControlGroupBox;
        private System.Windows.Forms.Button restartBackgroundThreadButton;
        private System.Windows.Forms.GroupBox aboutGroupBox;
        private System.Windows.Forms.Label aboutProductLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox audioKeepAliveGroupBox;
        private System.Windows.Forms.ComboBox audioKeepAliveComboBox;
        private System.Windows.Forms.CheckBox audioKeepAliveCheckbox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel consistencyModeStatusLabel;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.CheckBox trayIconCheckBox;
        private System.Windows.Forms.CheckBox alertsCheckBox;
        private System.Windows.Forms.CheckBox animatedCheckBox;
    }
}

