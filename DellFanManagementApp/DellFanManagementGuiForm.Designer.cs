
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DellFanManagementGuiForm));
            this.fansGroupBox = new System.Windows.Forms.GroupBox();
            this.fan2RpmValue = new System.Windows.Forms.Label();
            this.fan1RpmValue = new System.Windows.Forms.Label();
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
            this.configurationGroupBox = new System.Windows.Forms.GroupBox();
            this.configurationRadioButtonKeepAlive = new System.Windows.Forms.RadioButton();
            this.configurationRadioButtonManual = new System.Windows.Forms.RadioButton();
            this.configurationRadioButtonAutomatic = new System.Windows.Forms.RadioButton();
            this.manualGroupBox = new System.Windows.Forms.GroupBox();
            this.manualFan2GroupBox = new System.Windows.Forms.GroupBox();
            this.manualFan2RadioButtonHigh = new System.Windows.Forms.RadioButton();
            this.manualFan2RadioButtonMedium = new System.Windows.Forms.RadioButton();
            this.manualFan2RadioButtonOff = new System.Windows.Forms.RadioButton();
            this.manualFan1GroupBox = new System.Windows.Forms.GroupBox();
            this.manualFan1RadioButtonHigh = new System.Windows.Forms.RadioButton();
            this.manualFan1RadioButtonMedium = new System.Windows.Forms.RadioButton();
            this.manualFan1RadioButtonOff = new System.Windows.Forms.RadioButton();
            this.keepAliveGroupBox = new System.Windows.Forms.GroupBox();
            this.keepAliveApplyChangesButton = new System.Windows.Forms.Button();
            this.keepAliveRpmThresholdTextBox = new System.Windows.Forms.TextBox();
            this.keepAliveRpmThresholdLabel = new System.Windows.Forms.Label();
            this.keepAliveUpperTemperatureThresholdTextBox = new System.Windows.Forms.TextBox();
            this.keepAliveUpperTemperatureThresholdLabel = new System.Windows.Forms.Label();
            this.keepAliveLowerTemperatureThresholdTextBox = new System.Windows.Forms.TextBox();
            this.keepAliveLowerTemperatureThresholdLabel = new System.Windows.Forms.Label();
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
            this.keepAliveStatusLabel = new System.Windows.Forms.Label();
            this.fansGroupBox.SuspendLayout();
            this.thermalSettingGroupBox.SuspendLayout();
            this.temperatureGroupBox.SuspendLayout();
            this.configurationGroupBox.SuspendLayout();
            this.manualGroupBox.SuspendLayout();
            this.manualFan2GroupBox.SuspendLayout();
            this.manualFan1GroupBox.SuspendLayout();
            this.keepAliveGroupBox.SuspendLayout();
            this.ecFanControlGroupBox.SuspendLayout();
            this.aboutGroupBox.SuspendLayout();
            this.audioKeepAliveGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // fansGroupBox
            // 
            this.fansGroupBox.Controls.Add(this.fan2RpmValue);
            this.fansGroupBox.Controls.Add(this.fan1RpmValue);
            this.fansGroupBox.Controls.Add(this.fan2RpmLabel);
            this.fansGroupBox.Controls.Add(this.fan1RpmLabel);
            this.fansGroupBox.Location = new System.Drawing.Point(12, 12);
            this.fansGroupBox.Name = "fansGroupBox";
            this.fansGroupBox.Size = new System.Drawing.Size(161, 65);
            this.fansGroupBox.TabIndex = 6;
            this.fansGroupBox.TabStop = false;
            this.fansGroupBox.Text = "Fans:";
            // 
            // fan2RpmValue
            // 
            this.fan2RpmValue.AutoSize = true;
            this.fan2RpmValue.Location = new System.Drawing.Point(69, 38);
            this.fan2RpmValue.Name = "fan2RpmValue";
            this.fan2RpmValue.Size = new System.Drawing.Size(90, 15);
            this.fan2RpmValue.TabIndex = 4;
            this.fan2RpmValue.Text = "(Not measured)";
            // 
            // fan1RpmValue
            // 
            this.fan1RpmValue.AutoSize = true;
            this.fan1RpmValue.Location = new System.Drawing.Point(69, 19);
            this.fan1RpmValue.Name = "fan1RpmValue";
            this.fan1RpmValue.Size = new System.Drawing.Size(90, 15);
            this.fan1RpmValue.TabIndex = 3;
            this.fan1RpmValue.Text = "(Not measured)";
            // 
            // fan2RpmLabel
            // 
            this.fan2RpmLabel.AutoSize = true;
            this.fan2RpmLabel.Location = new System.Drawing.Point(6, 38);
            this.fan2RpmLabel.Name = "fan2RpmLabel";
            this.fan2RpmLabel.Size = new System.Drawing.Size(66, 15);
            this.fan2RpmLabel.TabIndex = 2;
            this.fan2RpmLabel.Text = "Fan 2 RPM:";
            // 
            // fan1RpmLabel
            // 
            this.fan1RpmLabel.AutoSize = true;
            this.fan1RpmLabel.Location = new System.Drawing.Point(6, 19);
            this.fan1RpmLabel.Name = "fan1RpmLabel";
            this.fan1RpmLabel.Size = new System.Drawing.Size(66, 15);
            this.fan1RpmLabel.TabIndex = 1;
            this.fan1RpmLabel.Text = "Fan 1 RPM:";
            // 
            // thermalSettingGroupBox
            // 
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonPerformance);
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonQuiet);
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonCool);
            this.thermalSettingGroupBox.Controls.Add(this.thermalSettingRadioButtonOptimized);
            this.thermalSettingGroupBox.Location = new System.Drawing.Point(288, 117);
            this.thermalSettingGroupBox.Name = "thermalSettingGroupBox";
            this.thermalSettingGroupBox.Size = new System.Drawing.Size(107, 128);
            this.thermalSettingGroupBox.TabIndex = 3;
            this.thermalSettingGroupBox.TabStop = false;
            this.thermalSettingGroupBox.Text = "Thermal setting:";
            // 
            // thermalSettingRadioButtonPerformance
            // 
            this.thermalSettingRadioButtonPerformance.AutoSize = true;
            this.thermalSettingRadioButtonPerformance.Location = new System.Drawing.Point(6, 99);
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
            this.thermalSettingRadioButtonQuiet.Location = new System.Drawing.Point(6, 73);
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
            this.temperatureGroupBox.Location = new System.Drawing.Point(12, 84);
            this.temperatureGroupBox.Name = "temperatureGroupBox";
            this.temperatureGroupBox.Size = new System.Drawing.Size(270, 198);
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
            // configurationGroupBox
            // 
            this.configurationGroupBox.Controls.Add(this.configurationRadioButtonKeepAlive);
            this.configurationGroupBox.Controls.Add(this.configurationRadioButtonManual);
            this.configurationGroupBox.Controls.Add(this.configurationRadioButtonAutomatic);
            this.configurationGroupBox.Location = new System.Drawing.Point(288, 12);
            this.configurationGroupBox.Name = "configurationGroupBox";
            this.configurationGroupBox.Size = new System.Drawing.Size(107, 99);
            this.configurationGroupBox.TabIndex = 2;
            this.configurationGroupBox.TabStop = false;
            this.configurationGroupBox.Text = "Configuration:";
            // 
            // configurationRadioButtonKeepAlive
            // 
            this.configurationRadioButtonKeepAlive.AutoSize = true;
            this.configurationRadioButtonKeepAlive.Location = new System.Drawing.Point(6, 72);
            this.configurationRadioButtonKeepAlive.Name = "configurationRadioButtonKeepAlive";
            this.configurationRadioButtonKeepAlive.Size = new System.Drawing.Size(78, 19);
            this.configurationRadioButtonKeepAlive.TabIndex = 2;
            this.configurationRadioButtonKeepAlive.TabStop = true;
            this.configurationRadioButtonKeepAlive.Text = "&Keep alive";
            this.configurationRadioButtonKeepAlive.UseVisualStyleBackColor = true;
            // 
            // configurationRadioButtonManual
            // 
            this.configurationRadioButtonManual.AutoSize = true;
            this.configurationRadioButtonManual.Location = new System.Drawing.Point(6, 47);
            this.configurationRadioButtonManual.Name = "configurationRadioButtonManual";
            this.configurationRadioButtonManual.Size = new System.Drawing.Size(65, 19);
            this.configurationRadioButtonManual.TabIndex = 1;
            this.configurationRadioButtonManual.TabStop = true;
            this.configurationRadioButtonManual.Text = "&Manual";
            this.configurationRadioButtonManual.UseVisualStyleBackColor = true;
            // 
            // configurationRadioButtonAutomatic
            // 
            this.configurationRadioButtonAutomatic.AutoSize = true;
            this.configurationRadioButtonAutomatic.Location = new System.Drawing.Point(6, 22);
            this.configurationRadioButtonAutomatic.Name = "configurationRadioButtonAutomatic";
            this.configurationRadioButtonAutomatic.Size = new System.Drawing.Size(81, 19);
            this.configurationRadioButtonAutomatic.TabIndex = 0;
            this.configurationRadioButtonAutomatic.TabStop = true;
            this.configurationRadioButtonAutomatic.Text = "&Automatic";
            this.configurationRadioButtonAutomatic.UseVisualStyleBackColor = true;
            // 
            // manualGroupBox
            // 
            this.manualGroupBox.Controls.Add(this.manualFan2GroupBox);
            this.manualGroupBox.Controls.Add(this.manualFan1GroupBox);
            this.manualGroupBox.Location = new System.Drawing.Point(401, 12);
            this.manualGroupBox.Name = "manualGroupBox";
            this.manualGroupBox.Size = new System.Drawing.Size(213, 128);
            this.manualGroupBox.TabIndex = 4;
            this.manualGroupBox.TabStop = false;
            this.manualGroupBox.Text = "Manual control:";
            // 
            // manualFan2GroupBox
            // 
            this.manualFan2GroupBox.Controls.Add(this.manualFan2RadioButtonHigh);
            this.manualFan2GroupBox.Controls.Add(this.manualFan2RadioButtonMedium);
            this.manualFan2GroupBox.Controls.Add(this.manualFan2RadioButtonOff);
            this.manualFan2GroupBox.Location = new System.Drawing.Point(96, 22);
            this.manualFan2GroupBox.Name = "manualFan2GroupBox";
            this.manualFan2GroupBox.Size = new System.Drawing.Size(84, 100);
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
            this.manualFan1GroupBox.Location = new System.Drawing.Point(6, 22);
            this.manualFan1GroupBox.Name = "manualFan1GroupBox";
            this.manualFan1GroupBox.Size = new System.Drawing.Size(84, 100);
            this.manualFan1GroupBox.TabIndex = 0;
            this.manualFan1GroupBox.TabStop = false;
            this.manualFan1GroupBox.Text = "Fan 1:";
            // 
            // manualFan1RadioButtonHigh
            // 
            this.manualFan1RadioButtonHigh.AutoSize = true;
            this.manualFan1RadioButtonHigh.Location = new System.Drawing.Point(7, 74);
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
            this.manualFan1RadioButtonMedium.Location = new System.Drawing.Point(6, 48);
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
            this.manualFan1RadioButtonOff.Location = new System.Drawing.Point(7, 23);
            this.manualFan1RadioButtonOff.Name = "manualFan1RadioButtonOff";
            this.manualFan1RadioButtonOff.Size = new System.Drawing.Size(42, 19);
            this.manualFan1RadioButtonOff.TabIndex = 0;
            this.manualFan1RadioButtonOff.TabStop = true;
            this.manualFan1RadioButtonOff.Text = "Off";
            this.manualFan1RadioButtonOff.UseVisualStyleBackColor = true;
            // 
            // keepAliveGroupBox
            // 
            this.keepAliveGroupBox.Controls.Add(this.keepAliveApplyChangesButton);
            this.keepAliveGroupBox.Controls.Add(this.keepAliveRpmThresholdTextBox);
            this.keepAliveGroupBox.Controls.Add(this.keepAliveRpmThresholdLabel);
            this.keepAliveGroupBox.Controls.Add(this.keepAliveUpperTemperatureThresholdTextBox);
            this.keepAliveGroupBox.Controls.Add(this.keepAliveUpperTemperatureThresholdLabel);
            this.keepAliveGroupBox.Controls.Add(this.keepAliveLowerTemperatureThresholdTextBox);
            this.keepAliveGroupBox.Controls.Add(this.keepAliveLowerTemperatureThresholdLabel);
            this.keepAliveGroupBox.Location = new System.Drawing.Point(401, 146);
            this.keepAliveGroupBox.Name = "keepAliveGroupBox";
            this.keepAliveGroupBox.Size = new System.Drawing.Size(213, 136);
            this.keepAliveGroupBox.TabIndex = 5;
            this.keepAliveGroupBox.TabStop = false;
            this.keepAliveGroupBox.Text = "Keep alive options:";
            // 
            // keepAliveApplyChangesButton
            // 
            this.keepAliveApplyChangesButton.Location = new System.Drawing.Point(7, 105);
            this.keepAliveApplyChangesButton.Name = "keepAliveApplyChangesButton";
            this.keepAliveApplyChangesButton.Size = new System.Drawing.Size(194, 23);
            this.keepAliveApplyChangesButton.TabIndex = 6;
            this.keepAliveApplyChangesButton.Text = "Appl&y changes";
            this.keepAliveApplyChangesButton.UseVisualStyleBackColor = true;
            // 
            // keepAliveRpmThresholdTextBox
            // 
            this.keepAliveRpmThresholdTextBox.Location = new System.Drawing.Point(166, 78);
            this.keepAliveRpmThresholdTextBox.MaxLength = 4;
            this.keepAliveRpmThresholdTextBox.Name = "keepAliveRpmThresholdTextBox";
            this.keepAliveRpmThresholdTextBox.Size = new System.Drawing.Size(35, 23);
            this.keepAliveRpmThresholdTextBox.TabIndex = 5;
            this.keepAliveRpmThresholdTextBox.Text = "2900";
            // 
            // keepAliveRpmThresholdLabel
            // 
            this.keepAliveRpmThresholdLabel.AutoSize = true;
            this.keepAliveRpmThresholdLabel.Location = new System.Drawing.Point(7, 81);
            this.keepAliveRpmThresholdLabel.Name = "keepAliveRpmThresholdLabel";
            this.keepAliveRpmThresholdLabel.Size = new System.Drawing.Size(88, 15);
            this.keepAliveRpmThresholdLabel.TabIndex = 4;
            this.keepAliveRpmThresholdLabel.Text = "RPM threshold:";
            // 
            // keepAliveUpperTemperatureThresholdTextBox
            // 
            this.keepAliveUpperTemperatureThresholdTextBox.Location = new System.Drawing.Point(176, 49);
            this.keepAliveUpperTemperatureThresholdTextBox.MaxLength = 3;
            this.keepAliveUpperTemperatureThresholdTextBox.Name = "keepAliveUpperTemperatureThresholdTextBox";
            this.keepAliveUpperTemperatureThresholdTextBox.Size = new System.Drawing.Size(25, 23);
            this.keepAliveUpperTemperatureThresholdTextBox.TabIndex = 3;
            this.keepAliveUpperTemperatureThresholdTextBox.Text = "85";
            // 
            // keepAliveUpperTemperatureThresholdLabel
            // 
            this.keepAliveUpperTemperatureThresholdLabel.AutoSize = true;
            this.keepAliveUpperTemperatureThresholdLabel.Location = new System.Drawing.Point(6, 52);
            this.keepAliveUpperTemperatureThresholdLabel.Name = "keepAliveUpperTemperatureThresholdLabel";
            this.keepAliveUpperTemperatureThresholdLabel.Size = new System.Drawing.Size(163, 15);
            this.keepAliveUpperTemperatureThresholdLabel.TabIndex = 2;
            this.keepAliveUpperTemperatureThresholdLabel.Text = "Upper temperature threshold:";
            // 
            // keepAliveLowerTemperatureThresholdTextBox
            // 
            this.keepAliveLowerTemperatureThresholdTextBox.Location = new System.Drawing.Point(176, 20);
            this.keepAliveLowerTemperatureThresholdTextBox.MaxLength = 3;
            this.keepAliveLowerTemperatureThresholdTextBox.Name = "keepAliveLowerTemperatureThresholdTextBox";
            this.keepAliveLowerTemperatureThresholdTextBox.Size = new System.Drawing.Size(25, 23);
            this.keepAliveLowerTemperatureThresholdTextBox.TabIndex = 1;
            this.keepAliveLowerTemperatureThresholdTextBox.Text = "70";
            // 
            // keepAliveLowerTemperatureThresholdLabel
            // 
            this.keepAliveLowerTemperatureThresholdLabel.AutoSize = true;
            this.keepAliveLowerTemperatureThresholdLabel.Location = new System.Drawing.Point(7, 23);
            this.keepAliveLowerTemperatureThresholdLabel.Name = "keepAliveLowerTemperatureThresholdLabel";
            this.keepAliveLowerTemperatureThresholdLabel.Size = new System.Drawing.Size(163, 15);
            this.keepAliveLowerTemperatureThresholdLabel.TabIndex = 0;
            this.keepAliveLowerTemperatureThresholdLabel.Text = "Lower temperature threshold:";
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
            this.restartBackgroundThreadButton.Location = new System.Drawing.Point(181, 65);
            this.restartBackgroundThreadButton.Name = "restartBackgroundThreadButton";
            this.restartBackgroundThreadButton.Size = new System.Drawing.Size(99, 22);
            this.restartBackgroundThreadButton.TabIndex = 1;
            this.restartBackgroundThreadButton.Text = "&Restart BG thread";
            this.restartBackgroundThreadButton.UseVisualStyleBackColor = true;
            // 
            // aboutGroupBox
            // 
            this.aboutGroupBox.Controls.Add(this.richTextBox1);
            this.aboutGroupBox.Controls.Add(this.aboutProductLabel);
            this.aboutGroupBox.Location = new System.Drawing.Point(620, 12);
            this.aboutGroupBox.Name = "aboutGroupBox";
            this.aboutGroupBox.Size = new System.Drawing.Size(348, 186);
            this.aboutGroupBox.TabIndex = 8;
            this.aboutGroupBox.TabStop = false;
            this.aboutGroupBox.Text = "About:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(336, 140);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // aboutProductLabel
            // 
            this.aboutProductLabel.AutoSize = true;
            this.aboutProductLabel.Location = new System.Drawing.Point(6, 19);
            this.aboutProductLabel.Name = "aboutProductLabel";
            this.aboutProductLabel.Size = new System.Drawing.Size(167, 15);
            this.aboutProductLabel.TabIndex = 0;
            this.aboutProductLabel.Text = "Dell Fan Management, version";
            // 
            // audioKeepAliveGroupBox
            // 
            this.audioKeepAliveGroupBox.Controls.Add(this.audioKeepAliveComboBox);
            this.audioKeepAliveGroupBox.Controls.Add(this.audioKeepAliveCheckbox);
            this.audioKeepAliveGroupBox.Location = new System.Drawing.Point(620, 202);
            this.audioKeepAliveGroupBox.Name = "audioKeepAliveGroupBox";
            this.audioKeepAliveGroupBox.Size = new System.Drawing.Size(348, 80);
            this.audioKeepAliveGroupBox.TabIndex = 9;
            this.audioKeepAliveGroupBox.TabStop = false;
            this.audioKeepAliveGroupBox.Text = "Audio keep alive:";
            // 
            // audioKeepAliveComboBox
            // 
            this.audioKeepAliveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioKeepAliveComboBox.FormattingEnabled = true;
            this.audioKeepAliveComboBox.Location = new System.Drawing.Point(6, 48);
            this.audioKeepAliveComboBox.Name = "audioKeepAliveComboBox";
            this.audioKeepAliveComboBox.Size = new System.Drawing.Size(336, 23);
            this.audioKeepAliveComboBox.TabIndex = 1;
            // 
            // audioKeepAliveCheckbox
            // 
            this.audioKeepAliveCheckbox.AutoSize = true;
            this.audioKeepAliveCheckbox.Location = new System.Drawing.Point(6, 22);
            this.audioKeepAliveCheckbox.Name = "audioKeepAliveCheckbox";
            this.audioKeepAliveCheckbox.Size = new System.Drawing.Size(322, 19);
            this.audioKeepAliveCheckbox.TabIndex = 0;
            this.audioKeepAliveCheckbox.Text = "K&eep this audio device active to prevent pops and clicks:";
            this.audioKeepAliveCheckbox.UseVisualStyleBackColor = true;
            // 
            // keepAliveStatusLabel
            // 
            this.keepAliveStatusLabel.AutoSize = true;
            this.keepAliveStatusLabel.Location = new System.Drawing.Point(288, 248);
            this.keepAliveStatusLabel.MaximumSize = new System.Drawing.Size(107, 0);
            this.keepAliveStatusLabel.Name = "keepAliveStatusLabel";
            this.keepAliveStatusLabel.Size = new System.Drawing.Size(74, 30);
            this.keepAliveStatusLabel.TabIndex = 10;
            this.keepAliveStatusLabel.Text = "Fan speed is locked";
            // 
            // DellFanManagementGuiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 295);
            this.Controls.Add(this.keepAliveStatusLabel);
            this.Controls.Add(this.audioKeepAliveGroupBox);
            this.Controls.Add(this.aboutGroupBox);
            this.Controls.Add(this.restartBackgroundThreadButton);
            this.Controls.Add(this.ecFanControlGroupBox);
            this.Controls.Add(this.keepAliveGroupBox);
            this.Controls.Add(this.manualGroupBox);
            this.Controls.Add(this.configurationGroupBox);
            this.Controls.Add(this.temperatureGroupBox);
            this.Controls.Add(this.thermalSettingGroupBox);
            this.Controls.Add(this.fansGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DellFanManagementGuiForm";
            this.Text = "Dell Fan Management";
            this.fansGroupBox.ResumeLayout(false);
            this.fansGroupBox.PerformLayout();
            this.thermalSettingGroupBox.ResumeLayout(false);
            this.thermalSettingGroupBox.PerformLayout();
            this.temperatureGroupBox.ResumeLayout(false);
            this.temperatureGroupBox.PerformLayout();
            this.configurationGroupBox.ResumeLayout(false);
            this.configurationGroupBox.PerformLayout();
            this.manualGroupBox.ResumeLayout(false);
            this.manualFan2GroupBox.ResumeLayout(false);
            this.manualFan2GroupBox.PerformLayout();
            this.manualFan1GroupBox.ResumeLayout(false);
            this.manualFan1GroupBox.PerformLayout();
            this.keepAliveGroupBox.ResumeLayout(false);
            this.keepAliveGroupBox.PerformLayout();
            this.ecFanControlGroupBox.ResumeLayout(false);
            this.ecFanControlGroupBox.PerformLayout();
            this.aboutGroupBox.ResumeLayout(false);
            this.aboutGroupBox.PerformLayout();
            this.audioKeepAliveGroupBox.ResumeLayout(false);
            this.audioKeepAliveGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox fansGroupBox;
        private System.Windows.Forms.Label fan1RpmLabel;
        private System.Windows.Forms.Label fan2RpmLabel;
        private System.Windows.Forms.Label fan2RpmValue;
        private System.Windows.Forms.Label fan1RpmValue;
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
        private System.Windows.Forms.GroupBox configurationGroupBox;
        private System.Windows.Forms.RadioButton configurationRadioButtonKeepAlive;
        private System.Windows.Forms.RadioButton configurationRadioButtonManual;
        private System.Windows.Forms.RadioButton configurationRadioButtonAutomatic;
        private System.Windows.Forms.GroupBox manualGroupBox;
        private System.Windows.Forms.GroupBox manualFan2GroupBox;
        private System.Windows.Forms.RadioButton manualFan2RadioButtonHigh;
        private System.Windows.Forms.RadioButton manualFan2RadioButtonMedium;
        private System.Windows.Forms.RadioButton manualFan2RadioButtonOff;
        private System.Windows.Forms.GroupBox manualFan1GroupBox;
        private System.Windows.Forms.RadioButton manualFan1RadioButtonHigh;
        private System.Windows.Forms.RadioButton manualFan1RadioButtonMedium;
        private System.Windows.Forms.RadioButton manualFan1RadioButtonOff;
        private System.Windows.Forms.GroupBox keepAliveGroupBox;
        private System.Windows.Forms.Button keepAliveApplyChangesButton;
        private System.Windows.Forms.TextBox keepAliveRpmThresholdTextBox;
        private System.Windows.Forms.Label keepAliveRpmThresholdLabel;
        private System.Windows.Forms.TextBox keepAliveUpperTemperatureThresholdTextBox;
        private System.Windows.Forms.Label keepAliveUpperTemperatureThresholdLabel;
        private System.Windows.Forms.TextBox keepAliveLowerTemperatureThresholdTextBox;
        private System.Windows.Forms.Label keepAliveLowerTemperatureThresholdLabel;
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
        private System.Windows.Forms.Label keepAliveStatusLabel;
    }
}

