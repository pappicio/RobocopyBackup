
Imports RobocopyBackup.RobocopyBackup

Partial Class TaskForm
    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"


    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TaskForm))
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.titleTextBox = New System.Windows.Forms.TextBox()
        Me.sourceLabel = New System.Windows.Forms.Label()
        Me.destinationLabel = New System.Windows.Forms.Label()
        Me.fullRadioButton = New System.Windows.Forms.RadioButton()
        Me.incrementalRadioButton = New System.Windows.Forms.RadioButton()
        Me.sourceTextBox = New System.Windows.Forms.TextBox()
        Me.sourceButton = New System.Windows.Forms.Button()
        Me.destinationTextBox = New System.Windows.Forms.TextBox()
        Me.destinationButton = New System.Windows.Forms.Button()
        Me.differentialRadioButton = New System.Windows.Forms.RadioButton()
        Me.retentionLabel = New System.Windows.Forms.Label()
        Me.retentionNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.directoriesGroupBox = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.onlyfoldercheck = New System.Windows.Forms.CheckBox()
        Me.destpasstext = New System.Windows.Forms.TextBox()
        Me.originusertext = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.originpasstext = New System.Windows.Forms.TextBox()
        Me.destusertext = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.methodGroupBox = New System.Windows.Forms.GroupBox()
        Me.methodHelpButton = New System.Windows.Forms.Button()
        Me.saveTaskButton = New System.Windows.Forms.Button()
        Me.scheduleGroupBox = New System.Windows.Forms.GroupBox()
        Me.timeDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.timeLabel = New System.Windows.Forms.Label()
        Me.monthdayNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.dailyRadioButton = New System.Windows.Forms.RadioButton()
        Me.weeklyRadioButton = New System.Windows.Forms.RadioButton()
        Me.weekdayComboBox = New System.Windows.Forms.ComboBox()
        Me.monthlyRadioButton = New System.Windows.Forms.RadioButton()
        Me.sourceFolderBrowserDialog = New RobocopyBackup.OpenFolderDialog()
        Me.destinationFolderBrowserDialog = New RobocopyBackup.OpenFolderDialog()
        CType(Me.retentionNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.directoriesGroupBox.SuspendLayout()
        Me.methodGroupBox.SuspendLayout()
        Me.scheduleGroupBox.SuspendLayout()
        CType(Me.monthdayNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'titleLabel
        '
        Me.titleLabel.AutoSize = True
        Me.titleLabel.Location = New System.Drawing.Point(36, 15)
        Me.titleLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(60, 25)
        Me.titleLabel.TabIndex = 0
        Me.titleLabel.Text = "Title: "
        '
        'titleTextBox
        '
        Me.titleTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.titleTextBox.Location = New System.Drawing.Point(174, 12)
        Me.titleTextBox.Margin = New System.Windows.Forms.Padding(6)
        Me.titleTextBox.Name = "titleTextBox"
        Me.titleTextBox.Size = New System.Drawing.Size(812, 30)
        Me.titleTextBox.TabIndex = 1
        '
        'sourceLabel
        '
        Me.sourceLabel.AutoSize = True
        Me.sourceLabel.Location = New System.Drawing.Point(12, 175)
        Me.sourceLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.sourceLabel.Name = "sourceLabel"
        Me.sourceLabel.Size = New System.Drawing.Size(81, 25)
        Me.sourceLabel.TabIndex = 0
        Me.sourceLabel.Text = "Source:"
        '
        'destinationLabel
        '
        Me.destinationLabel.AutoSize = True
        Me.destinationLabel.Location = New System.Drawing.Point(12, 347)
        Me.destinationLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.destinationLabel.Name = "destinationLabel"
        Me.destinationLabel.Size = New System.Drawing.Size(115, 25)
        Me.destinationLabel.TabIndex = 4
        Me.destinationLabel.Text = "Destination:"
        '
        'fullRadioButton
        '
        Me.fullRadioButton.AutoSize = True
        Me.fullRadioButton.Location = New System.Drawing.Point(12, 112)
        Me.fullRadioButton.Margin = New System.Windows.Forms.Padding(6)
        Me.fullRadioButton.Name = "fullRadioButton"
        Me.fullRadioButton.Size = New System.Drawing.Size(64, 29)
        Me.fullRadioButton.TabIndex = 2
        Me.fullRadioButton.Text = "Full"
        Me.fullRadioButton.UseVisualStyleBackColor = True
        '
        'incrementalRadioButton
        '
        Me.incrementalRadioButton.AutoSize = True
        Me.incrementalRadioButton.Checked = True
        Me.incrementalRadioButton.Location = New System.Drawing.Point(12, 39)
        Me.incrementalRadioButton.Margin = New System.Windows.Forms.Padding(6)
        Me.incrementalRadioButton.Name = "incrementalRadioButton"
        Me.incrementalRadioButton.Size = New System.Drawing.Size(134, 29)
        Me.incrementalRadioButton.TabIndex = 0
        Me.incrementalRadioButton.TabStop = True
        Me.incrementalRadioButton.Text = "Incremental"
        Me.incrementalRadioButton.UseVisualStyleBackColor = True
        '
        'sourceTextBox
        '
        Me.sourceTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.sourceTextBox.Location = New System.Drawing.Point(211, 172)
        Me.sourceTextBox.Margin = New System.Windows.Forms.Padding(6)
        Me.sourceTextBox.Name = "sourceTextBox"
        Me.sourceTextBox.ReadOnly = True
        Me.sourceTextBox.Size = New System.Drawing.Size(700, 30)
        Me.sourceTextBox.TabIndex = 1
        '
        'sourceButton
        '
        Me.sourceButton.Location = New System.Drawing.Point(922, 172)
        Me.sourceButton.Margin = New System.Windows.Forms.Padding(6)
        Me.sourceButton.Name = "sourceButton"
        Me.sourceButton.Size = New System.Drawing.Size(40, 30)
        Me.sourceButton.TabIndex = 2
        Me.sourceButton.Text = "..."
        Me.sourceButton.UseVisualStyleBackColor = True
        '
        'destinationTextBox
        '
        Me.destinationTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.destinationTextBox.Location = New System.Drawing.Point(211, 344)
        Me.destinationTextBox.Margin = New System.Windows.Forms.Padding(6)
        Me.destinationTextBox.Name = "destinationTextBox"
        Me.destinationTextBox.ReadOnly = True
        Me.destinationTextBox.Size = New System.Drawing.Size(700, 30)
        Me.destinationTextBox.TabIndex = 5
        '
        'destinationButton
        '
        Me.destinationButton.Location = New System.Drawing.Point(922, 344)
        Me.destinationButton.Margin = New System.Windows.Forms.Padding(6)
        Me.destinationButton.Name = "destinationButton"
        Me.destinationButton.Size = New System.Drawing.Size(40, 30)
        Me.destinationButton.TabIndex = 6
        Me.destinationButton.Text = "..."
        Me.destinationButton.UseVisualStyleBackColor = True
        '
        'differentialRadioButton
        '
        Me.differentialRadioButton.AutoSize = True
        Me.differentialRadioButton.Location = New System.Drawing.Point(12, 80)
        Me.differentialRadioButton.Margin = New System.Windows.Forms.Padding(6)
        Me.differentialRadioButton.Name = "differentialRadioButton"
        Me.differentialRadioButton.Size = New System.Drawing.Size(124, 29)
        Me.differentialRadioButton.TabIndex = 1
        Me.differentialRadioButton.Text = "Differential"
        Me.differentialRadioButton.UseVisualStyleBackColor = True
        '
        'retentionLabel
        '
        Me.retentionLabel.AutoSize = True
        Me.retentionLabel.Location = New System.Drawing.Point(389, 78)
        Me.retentionLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.retentionLabel.Name = "retentionLabel"
        Me.retentionLabel.Size = New System.Drawing.Size(100, 25)
        Me.retentionLabel.TabIndex = 3
        Me.retentionLabel.Text = "Retention:"
        '
        'retentionNumericUpDown
        '
        Me.retentionNumericUpDown.BackColor = System.Drawing.SystemColors.Window
        Me.retentionNumericUpDown.Location = New System.Drawing.Point(839, 76)
        Me.retentionNumericUpDown.Margin = New System.Windows.Forms.Padding(6)
        Me.retentionNumericUpDown.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.retentionNumericUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.retentionNumericUpDown.Name = "retentionNumericUpDown"
        Me.retentionNumericUpDown.ReadOnly = True
        Me.retentionNumericUpDown.Size = New System.Drawing.Size(123, 30)
        Me.retentionNumericUpDown.TabIndex = 4
        Me.retentionNumericUpDown.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'directoriesGroupBox
        '
        Me.directoriesGroupBox.Controls.Add(Me.CheckBox1)
        Me.directoriesGroupBox.Controls.Add(Me.Label6)
        Me.directoriesGroupBox.Controls.Add(Me.Label7)
        Me.directoriesGroupBox.Controls.Add(Me.Label5)
        Me.directoriesGroupBox.Controls.Add(Me.Label9)
        Me.directoriesGroupBox.Controls.Add(Me.onlyfoldercheck)
        Me.directoriesGroupBox.Controls.Add(Me.destpasstext)
        Me.directoriesGroupBox.Controls.Add(Me.originusertext)
        Me.directoriesGroupBox.Controls.Add(Me.Label3)
        Me.directoriesGroupBox.Controls.Add(Me.Label4)
        Me.directoriesGroupBox.Controls.Add(Me.originpasstext)
        Me.directoriesGroupBox.Controls.Add(Me.destusertext)
        Me.directoriesGroupBox.Controls.Add(Me.Label1)
        Me.directoriesGroupBox.Controls.Add(Me.Label2)
        Me.directoriesGroupBox.Controls.Add(Me.CheckBox2)
        Me.directoriesGroupBox.Controls.Add(Me.sourceLabel)
        Me.directoriesGroupBox.Controls.Add(Me.destinationLabel)
        Me.directoriesGroupBox.Controls.Add(Me.sourceTextBox)
        Me.directoriesGroupBox.Controls.Add(Me.destinationButton)
        Me.directoriesGroupBox.Controls.Add(Me.sourceButton)
        Me.directoriesGroupBox.Controls.Add(Me.destinationTextBox)
        Me.directoriesGroupBox.Location = New System.Drawing.Point(24, 41)
        Me.directoriesGroupBox.Margin = New System.Windows.Forms.Padding(6)
        Me.directoriesGroupBox.Name = "directoriesGroupBox"
        Me.directoriesGroupBox.Padding = New System.Windows.Forms.Padding(6)
        Me.directoriesGroupBox.Size = New System.Drawing.Size(987, 472)
        Me.directoriesGroupBox.TabIndex = 2
        Me.directoriesGroupBox.TabStop = False
        Me.directoriesGroupBox.Text = "Directories"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(16, 103)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(269, 29)
        Me.CheckBox1.TabIndex = 29
        Me.CheckBox1.Text = "Textbox percorsi read/write"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(206, 313)
        Me.Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(667, 25)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "selezionare un percorso locale o Inserire percorso di rete tipo: \\192.168.1.3 "
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(207, 375)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(692, 45)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "(username e passwoed locali della macchina con condivise per accedere alle condiv" &
    "ise, lasciare vuoti se non necessarie)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(206, 141)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(667, 25)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "selezionare un percorso locale o Inserire percorso di rete tipo: \\192.168.1.3 "
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(207, 209)
        Me.Label9.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(692, 45)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "(username e passwoed locali della macchina con condivise per accedere alle condiv" &
    "ise, lasciare vuoti se non necessarie)"
        '
        'onlyfoldercheck
        '
        Me.onlyfoldercheck.AutoSize = True
        Me.onlyfoldercheck.Location = New System.Drawing.Point(16, 33)
        Me.onlyfoldercheck.Name = "onlyfoldercheck"
        Me.onlyfoldercheck.Size = New System.Drawing.Size(721, 29)
        Me.onlyfoldercheck.TabIndex = 19
        Me.onlyfoldercheck.Text = "Copia solo struttura cartelle (obbligatorio filesystem NTFS  origine/destinazione" &
    ")"
        Me.onlyfoldercheck.UseVisualStyleBackColor = True
        '
        'destpasstext
        '
        Me.destpasstext.Location = New System.Drawing.Point(620, 429)
        Me.destpasstext.Margin = New System.Windows.Forms.Padding(6)
        Me.destpasstext.Name = "destpasstext"
        Me.destpasstext.Size = New System.Drawing.Size(291, 30)
        Me.destpasstext.TabIndex = 8
        Me.destpasstext.UseSystemPasswordChar = True
        '
        'originusertext
        '
        Me.originusertext.Location = New System.Drawing.Point(211, 259)
        Me.originusertext.Margin = New System.Windows.Forms.Padding(6)
        Me.originusertext.Name = "originusertext"
        Me.originusertext.Size = New System.Drawing.Size(288, 30)
        Me.originusertext.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(504, 429)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 25)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 429)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(108, 25)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Username:"
        '
        'originpasstext
        '
        Me.originpasstext.Location = New System.Drawing.Point(620, 259)
        Me.originpasstext.Margin = New System.Windows.Forms.Padding(6)
        Me.originpasstext.Name = "originpasstext"
        Me.originpasstext.Size = New System.Drawing.Size(291, 30)
        Me.originpasstext.TabIndex = 4
        Me.originpasstext.UseSystemPasswordChar = True
        '
        'destusertext
        '
        Me.destusertext.Location = New System.Drawing.Point(211, 426)
        Me.destusertext.Margin = New System.Windows.Forms.Padding(6)
        Me.destusertext.Name = "destusertext"
        Me.destusertext.Size = New System.Drawing.Size(288, 30)
        Me.destusertext.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(504, 262)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 25)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Password:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 262)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Username:"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(16, 68)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(604, 29)
        Me.CheckBox2.TabIndex = 8
        Me.CheckBox2.Text = "Copia permessi NTFS (solo per Filesystem NTFS sulle condivise)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'methodGroupBox
        '
        Me.methodGroupBox.Controls.Add(Me.methodHelpButton)
        Me.methodGroupBox.Controls.Add(Me.differentialRadioButton)
        Me.methodGroupBox.Controls.Add(Me.fullRadioButton)
        Me.methodGroupBox.Controls.Add(Me.incrementalRadioButton)
        Me.methodGroupBox.Location = New System.Drawing.Point(323, 559)
        Me.methodGroupBox.Margin = New System.Windows.Forms.Padding(6)
        Me.methodGroupBox.Name = "methodGroupBox"
        Me.methodGroupBox.Padding = New System.Windows.Forms.Padding(6)
        Me.methodGroupBox.Size = New System.Drawing.Size(38, 12)
        Me.methodGroupBox.TabIndex = 4
        Me.methodGroupBox.TabStop = False
        Me.methodGroupBox.Text = "Method"
        Me.methodGroupBox.Visible = False
        '
        'methodHelpButton
        '
        Me.methodHelpButton.Image = Global.RobocopyBackup.My.Resources.Resources.HelpIcon
        Me.methodHelpButton.Location = New System.Drawing.Point(321, 30)
        Me.methodHelpButton.Margin = New System.Windows.Forms.Padding(6)
        Me.methodHelpButton.Name = "methodHelpButton"
        Me.methodHelpButton.Size = New System.Drawing.Size(48, 47)
        Me.methodHelpButton.TabIndex = 5
        Me.methodHelpButton.UseVisualStyleBackColor = True
        '
        'saveTaskButton
        '
        Me.saveTaskButton.Location = New System.Drawing.Point(24, 648)
        Me.saveTaskButton.Margin = New System.Windows.Forms.Padding(6)
        Me.saveTaskButton.Name = "saveTaskButton"
        Me.saveTaskButton.Size = New System.Drawing.Size(987, 44)
        Me.saveTaskButton.TabIndex = 9
        Me.saveTaskButton.Text = "SaveTask"
        '
        'scheduleGroupBox
        '
        Me.scheduleGroupBox.Controls.Add(Me.timeDateTimePicker)
        Me.scheduleGroupBox.Controls.Add(Me.retentionLabel)
        Me.scheduleGroupBox.Controls.Add(Me.timeLabel)
        Me.scheduleGroupBox.Controls.Add(Me.monthdayNumericUpDown)
        Me.scheduleGroupBox.Controls.Add(Me.dailyRadioButton)
        Me.scheduleGroupBox.Controls.Add(Me.retentionNumericUpDown)
        Me.scheduleGroupBox.Controls.Add(Me.weeklyRadioButton)
        Me.scheduleGroupBox.Controls.Add(Me.weekdayComboBox)
        Me.scheduleGroupBox.Controls.Add(Me.monthlyRadioButton)
        Me.scheduleGroupBox.Location = New System.Drawing.Point(24, 525)
        Me.scheduleGroupBox.Margin = New System.Windows.Forms.Padding(6)
        Me.scheduleGroupBox.Name = "scheduleGroupBox"
        Me.scheduleGroupBox.Padding = New System.Windows.Forms.Padding(6)
        Me.scheduleGroupBox.Size = New System.Drawing.Size(987, 118)
        Me.scheduleGroupBox.TabIndex = 5
        Me.scheduleGroupBox.TabStop = False
        Me.scheduleGroupBox.Text = "Schedule"
        '
        'timeDateTimePicker
        '
        Me.timeDateTimePicker.CustomFormat = "HH:mm"
        Me.timeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timeDateTimePicker.Location = New System.Drawing.Point(211, 73)
        Me.timeDateTimePicker.Margin = New System.Windows.Forms.Padding(6)
        Me.timeDateTimePicker.Name = "timeDateTimePicker"
        Me.timeDateTimePicker.ShowUpDown = True
        Me.timeDateTimePicker.Size = New System.Drawing.Size(133, 30)
        Me.timeDateTimePicker.TabIndex = 3
        Me.timeDateTimePicker.Value = New Date(2018, 1, 1, 0, 0, 0, 0)
        '
        'timeLabel
        '
        Me.timeLabel.AutoSize = True
        Me.timeLabel.Location = New System.Drawing.Point(12, 78)
        Me.timeLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.timeLabel.Name = "timeLabel"
        Me.timeLabel.Size = New System.Drawing.Size(81, 25)
        Me.timeLabel.TabIndex = 5
        Me.timeLabel.Text = "AtTime:"
        '
        'monthdayNumericUpDown
        '
        Me.monthdayNumericUpDown.BackColor = System.Drawing.SystemColors.Window
        Me.monthdayNumericUpDown.Location = New System.Drawing.Point(839, 30)
        Me.monthdayNumericUpDown.Margin = New System.Windows.Forms.Padding(6)
        Me.monthdayNumericUpDown.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.monthdayNumericUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.monthdayNumericUpDown.Name = "monthdayNumericUpDown"
        Me.monthdayNumericUpDown.ReadOnly = True
        Me.monthdayNumericUpDown.Size = New System.Drawing.Size(123, 30)
        Me.monthdayNumericUpDown.TabIndex = 4
        Me.monthdayNumericUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'dailyRadioButton
        '
        Me.dailyRadioButton.AutoSize = True
        Me.dailyRadioButton.Checked = True
        Me.dailyRadioButton.Location = New System.Drawing.Point(17, 30)
        Me.dailyRadioButton.Margin = New System.Windows.Forms.Padding(6)
        Me.dailyRadioButton.Name = "dailyRadioButton"
        Me.dailyRadioButton.Size = New System.Drawing.Size(76, 29)
        Me.dailyRadioButton.TabIndex = 0
        Me.dailyRadioButton.TabStop = True
        Me.dailyRadioButton.Text = "Daily"
        Me.dailyRadioButton.UseVisualStyleBackColor = True
        '
        'weeklyRadioButton
        '
        Me.weeklyRadioButton.AutoSize = True
        Me.weeklyRadioButton.Location = New System.Drawing.Point(211, 30)
        Me.weeklyRadioButton.Margin = New System.Windows.Forms.Padding(6)
        Me.weeklyRadioButton.Name = "weeklyRadioButton"
        Me.weeklyRadioButton.Size = New System.Drawing.Size(147, 29)
        Me.weeklyRadioButton.TabIndex = 1
        Me.weeklyRadioButton.Text = "DayOfWeek:"
        Me.weeklyRadioButton.UseVisualStyleBackColor = True
        '
        'weekdayComboBox
        '
        Me.weekdayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.weekdayComboBox.Location = New System.Drawing.Point(415, 29)
        Me.weekdayComboBox.Margin = New System.Windows.Forms.Padding(6)
        Me.weekdayComboBox.Name = "weekdayComboBox"
        Me.weekdayComboBox.Size = New System.Drawing.Size(206, 33)
        Me.weekdayComboBox.TabIndex = 2
        '
        'monthlyRadioButton
        '
        Me.monthlyRadioButton.AutoSize = True
        Me.monthlyRadioButton.Location = New System.Drawing.Point(655, 30)
        Me.monthlyRadioButton.Margin = New System.Windows.Forms.Padding(6)
        Me.monthlyRadioButton.Name = "monthlyRadioButton"
        Me.monthlyRadioButton.Size = New System.Drawing.Size(150, 29)
        Me.monthlyRadioButton.TabIndex = 2
        Me.monthlyRadioButton.Text = "DayOfMonth:"
        Me.monthlyRadioButton.UseVisualStyleBackColor = True
        '
        'sourceFolderBrowserDialog
        '
        Me.sourceFolderBrowserDialog.SelectedPath = Nothing
        '
        'destinationFolderBrowserDialog
        '
        Me.destinationFolderBrowserDialog.SelectedPath = Nothing
        '
        'TaskForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 702)
        Me.Controls.Add(Me.scheduleGroupBox)
        Me.Controls.Add(Me.saveTaskButton)
        Me.Controls.Add(Me.methodGroupBox)
        Me.Controls.Add(Me.directoriesGroupBox)
        Me.Controls.Add(Me.titleTextBox)
        Me.Controls.Add(Me.titleLabel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TaskForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Attività"
        CType(Me.retentionNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.directoriesGroupBox.ResumeLayout(False)
        Me.directoriesGroupBox.PerformLayout()
        Me.methodGroupBox.ResumeLayout(False)
        Me.methodGroupBox.PerformLayout()
        Me.scheduleGroupBox.ResumeLayout(False)
        Me.scheduleGroupBox.PerformLayout()
        CType(Me.monthdayNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents titleLabel As System.Windows.Forms.Label
    Private WithEvents titleTextBox As System.Windows.Forms.TextBox
    Private WithEvents sourceLabel As System.Windows.Forms.Label
    Private WithEvents destinationLabel As System.Windows.Forms.Label
    Private WithEvents fullRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents incrementalRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents sourceTextBox As System.Windows.Forms.TextBox
    Private WithEvents sourceButton As System.Windows.Forms.Button
    Private WithEvents destinationTextBox As System.Windows.Forms.TextBox
    Private WithEvents destinationButton As System.Windows.Forms.Button
    Private WithEvents differentialRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents retentionLabel As System.Windows.Forms.Label
    Private WithEvents retentionNumericUpDown As System.Windows.Forms.NumericUpDown
    Private WithEvents directoriesGroupBox As System.Windows.Forms.GroupBox
    Private WithEvents methodGroupBox As System.Windows.Forms.GroupBox
    Private WithEvents saveTaskButton As System.Windows.Forms.Button
    Private WithEvents scheduleGroupBox As System.Windows.Forms.GroupBox
    Private WithEvents timeLabel As System.Windows.Forms.Label
    Private WithEvents monthdayNumericUpDown As System.Windows.Forms.NumericUpDown
    Private WithEvents weekdayComboBox As System.Windows.Forms.ComboBox
    Private WithEvents monthlyRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents weeklyRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents dailyRadioButton As System.Windows.Forms.RadioButton
    Private WithEvents timeDateTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents methodHelpButton As System.Windows.Forms.Button
    Private WithEvents sourceFolderBrowserDialog As OpenFolderDialog
    Private WithEvents destinationFolderBrowserDialog As OpenFolderDialog
    Friend WithEvents CheckBox2 As Windows.Forms.CheckBox
    Private WithEvents destpasstext As Windows.Forms.TextBox
    Private WithEvents originusertext As Windows.Forms.TextBox
    Private WithEvents Label3 As Windows.Forms.Label
    Private WithEvents Label4 As Windows.Forms.Label
    Private WithEvents originpasstext As Windows.Forms.TextBox
    Private WithEvents destusertext As Windows.Forms.TextBox
    Private WithEvents Label1 As Windows.Forms.Label
    Private WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents onlyfoldercheck As Windows.Forms.CheckBox
    Private WithEvents Label9 As Windows.Forms.Label
    Private WithEvents Label7 As Windows.Forms.Label
    Private WithEvents Label5 As Windows.Forms.Label
    Private WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
End Class
