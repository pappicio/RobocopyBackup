
Imports System.Linq
Imports System.Windows.Forms
Imports RobocopyBackup.RobocopyBackup

Partial Public Class TaskForm
    Inherits Form

    Shared titolo As String = ""
    Public Property ResultTask() As Task
        Get
            Return m_ResultTask
        End Get
        Private Set
            m_ResultTask = Value
        End Set
    End Property
    Private m_ResultTask As Task


    Private _guid As String


    Public Sub New()
        taskx = Nothing
        InitializeComponent()
        Localize()
    End Sub
    Dim taskx As Task



    Public Sub New(task As Task)
        Me.New()
        _guid = task.Guid
        taskx = task
        titleTextBox.Text = task.Title
        If titleTextBox.Text <> "" Then
            titleTextBox.ReadOnly = True
        End If
        Dim c As New Credential
        If task.originuser <> "" And task.originpass <> "" Then
            c.Username = Credential.Decrypt(_guid, task.originuser)
            c.Password = Credential.Decrypt(_guid, task.originpass)
        End If
        If Not c Is Nothing Then
            originusertext.Text = c.Username
            originpasstext.Text = c.Password
        End If
        Dim c2 As New Credential
        If task.destuser <> "" And task.destpass <> "" Then
            c2.Username = Credential.Decrypt(_guid, task.destuser)
            c2.Password = Credential.Decrypt(_guid, task.destpass)
        End If
        If Not c2 Is Nothing Then
            destusertext.Text = c2.Username
            destpasstext.Text = c2.Password
        End If
        c = Nothing
        sourceFolderBrowserDialog.SelectedPath = InlineAssignHelper(sourceTextBox.Text, task.Source)

        destinationFolderBrowserDialog.SelectedPath = InlineAssignHelper(destinationTextBox.Text, task.Destination)

        If task.Method = Method.Incremental Then
            incrementalRadioButton.Checked = True
            retentionNumericUpDown.Value = task.Retention
        End If
        If task.Period = Period.Weekly Then
            weekdayComboBox.SelectedIndex = CInt(task.DayOfWeek)
        ElseIf task.Period = Period.Monthly Then
            monthdayNumericUpDown.Value = task.DayOfMonth
        End If
        timeDateTimePicker.Value = DateTime.Today.AddHours(task.Hour).AddMinutes(task.Minute)
        Dim s As String = task.NTFS
        If s <> "" Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If

    End Sub


    Private Sub CheckUncPath()
        Dim unc__1 As Boolean = Unc.IsUncPath(sourceTextBox.Text)
        Dim unc__2 As Boolean = Unc.IsUncPath(destinationTextBox.Text)
        originusertext.Enabled = unc__1
        originpasstext.Enabled = unc__1
        Button2.Enabled = unc__1
        If Not taskx Is Nothing Then
            If unc__1 And taskx.originuser <> "" Then
                originusertext.Text = Credential.Decrypt(taskx.Guid, taskx.originuser)
                originusertext.Text = Credential.Decrypt(taskx.Guid, taskx.originpass)
            End If
        End If


        destusertext.Enabled = unc__2
        destpasstext.Enabled = unc__2
        Button1.Enabled = unc__2
        If Not taskx Is Nothing Then
            If unc__2 And taskx.destuser <> "" Then
                destusertext.Text = Credential.Decrypt(taskx.Guid, taskx.destuser)
                destpasstext.Text = Credential.Decrypt(taskx.Guid, taskx.destpass)
            End If
        End If

    End Sub

    Private Sub CreateTask()
        taskx = Nothing

        Dim task As New Task() With {
             .Guid = titleTextBox.Text, '_guid
             .Title = titleTextBox.Text,
             .Source = sourceTextBox.Text,
             .Destination = destinationTextBox.Text
        }
        _guid = task.Title

        task.originuser = Credential.Encrypt(_guid, originusertext.Text)
        task.originpass = Credential.Encrypt(_guid, originpasstext.Text)


        task.destuser = Credential.Encrypt(_guid, destusertext.Text)
        task.destpass = Credential.Encrypt(_guid, destpasstext.Text)



        task.Method = Method.Incremental
        task.Retention = CUShort(Math.Truncate(retentionNumericUpDown.Value))
        If CheckBox2.Checked Then
            task.NTFS = "SI"
        Else
            task.NTFS = ""
        End If

        If dailyRadioButton.Checked Then
            task.Period = Period.Daily
        ElseIf weeklyRadioButton.Checked Then
            task.Period = Period.Weekly
            task.DayOfWeek = CType(weekdayComboBox.SelectedIndex, DayOfWeek)
        Else
            task.Period = Period.Monthly
            task.DayOfMonth = CByte(Math.Truncate(monthdayNumericUpDown.Value))
        End If
        task.Hour = CByte(timeDateTimePicker.Value.Hour)
        task.Minute = CByte(timeDateTimePicker.Value.Minute)
        ResultTask = task
    End Sub


    Private Sub DestinationButton_Click(sender As Object, e As EventArgs) Handles destinationButton.Click
        If Me.destinationFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            Dim control As Control = Me.destinationTextBox
            Dim openFolderDialog As OpenFolderDialog = Me.destinationFolderBrowserDialog
            Dim text As String = Unc.TranslatePath(Me.destinationFolderBrowserDialog.SelectedPath)
            Dim text2 As String = text
            openFolderDialog.SelectedPath = text
            control.Text = text2
        End If
    End Sub

    Private Sub DifferentialRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles differentialRadioButton.CheckedChanged
        retentionNumericUpDown.Enabled = True
    End Sub

    Private Sub DestinationTextBox_TextChanged(sender As Object, e As EventArgs) Handles destinationTextBox.TextChanged
        CheckUncPath()
    End Sub

    Private Sub FullRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles fullRadioButton.CheckedChanged
        retentionNumericUpDown.Enabled = True
    End Sub


    Private Sub IncrementalRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles incrementalRadioButton.CheckedChanged
        'retentionNumericUpDown.Enabled = False
    End Sub


    Private Sub Localize()
        Text = Lang.[Get]("Task")
        titleLabel.Text = Lang.[Get]("Title", ":")
        directoriesGroupBox.Text = Lang.[Get]("Directories")
        sourceLabel.Text = Lang.[Get]("Source", ":")
        destinationLabel.Text = Lang.[Get]("Destination", ":")

        methodGroupBox.Text = Lang.[Get]("Method")
        incrementalRadioButton.Text = Lang.[Get]("Incremental")
        differentialRadioButton.Text = Lang.[Get]("Differential")
        fullRadioButton.Text = Lang.[Get]("Full")
        retentionLabel.Text = Lang.[Get]("Retention", ":")
        scheduleGroupBox.Text = Lang.[Get]("Schedule")
        dailyRadioButton.Text = Lang.[Get]("Daily")
        weeklyRadioButton.Text = Lang.[Get]("DayOfWeek", ":")
        monthlyRadioButton.Text = Lang.[Get]("DayOfMonth", ":")
        timeLabel.Text = Lang.[Get]("AtTime", ":")
        saveTaskButton.Text = Lang.[Get]("SaveTask")

        For Each dow As DayOfWeek In [Enum].GetValues(GetType(DayOfWeek))
            weekdayComboBox.Items.Add(Lang.[Get](dow.ToString()))
        Next
        weekdayComboBox.Text = Lang.[Get](DayOfWeek.Sunday.ToString())
        dailyRadioButton.Checked = True
    End Sub


    Private Sub MethodHelpButton_Click(sender As Object, e As EventArgs) Handles methodHelpButton.Click
        Dim message As String = String.Format("{0}" & vbLf & vbLf & "{1}" & vbLf & vbLf & "{2}" & vbLf & vbLf & "{3}", Lang.[Get]("HelpIncremental"), Lang.[Get]("HelpDifferential"), Lang.[Get]("HelpFull"), Lang.[Get]("HelpRetention"))
        MessageBox.Show(message, Lang.[Get]("Help"), MessageBoxButtons.OK, MessageBoxIcon.Question)
    End Sub


    Private Sub MonthdayNumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles monthdayNumericUpDown.ValueChanged
        monthlyRadioButton.Checked = True
    End Sub


    Private Sub SaveTaskButton_Click(sender As Object, e As EventArgs) Handles saveTaskButton.Click
        If String.IsNullOrEmpty(titleTextBox.Text) OrElse String.IsNullOrEmpty(sourceTextBox.Text) OrElse String.IsNullOrEmpty(destinationTextBox.Text) Then
            MessageBox.Show(Lang.[Get]("IncompleteTaskForm"), Lang.[Get]("Error"), MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return
        End If
        If (String.IsNullOrEmpty(originusertext.Text) OrElse String.IsNullOrEmpty(originpasstext.Text)) And originusertext.Enabled Then
            MessageBox.Show("Mancano username e password per accesso percorso remoto 'origine'...", "Inserire credenziali", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            originusertext.Focus()
            Return
        End If
        If (String.IsNullOrEmpty(destusertext.Text) OrElse String.IsNullOrEmpty(destpasstext.Text)) And destusertext.Enabled Then
            MessageBox.Show("Mancano username e password per accesso percorso remoto 'destinazione'...", "Inserire credenziali", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            originusertext.Focus()
            Return
        End If
        If Config.Tasks.ContainsKey(titleTextBox.Text.Trim) And titleTextBox.ReadOnly = False Then

            MsgBox("una attività con lo stesso nome è gia esistente, cambiare 'titolo' attivita!")

            Return
        End If

        CreateTask()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub SourceButton_Click(sender As Object, e As EventArgs) Handles sourceButton.Click

        'If Me.sourceTextBox.Text.Trim <> "" And IO.Directory.Exists(Me.sourceTextBox.Text.Trim) Then
        ' Me.sourceFolderBrowserDialog.SelectedPath = Me.sourceTextBox.Text.Trim
        'End If
        If Me.sourceFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            Dim control As Control = Me.sourceTextBox
            Dim openFolderDialog As OpenFolderDialog = Me.sourceFolderBrowserDialog
            Dim text As String = Unc.TranslatePath(Me.sourceFolderBrowserDialog.SelectedPath)
            Dim text2 As String = text
            openFolderDialog.SelectedPath = text
            control.Text = text2



        End If
    End Sub


    Private Sub SourceTextBox_TextChanged(sender As Object, e As EventArgs) Handles sourceTextBox.TextChanged
        CheckUncPath()
    End Sub




    Private Sub WeekdayComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles weekdayComboBox.SelectedIndexChanged
        weeklyRadioButton.Checked = True
    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function

    Private Sub TaskForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub retentionNumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles retentionNumericUpDown.ValueChanged

    End Sub

    Private Sub titleTextBox_TextChanged(sender As Object, e As EventArgs) Handles titleTextBox.TextChanged

    End Sub

    Private Sub titleTextBox_LostFocus(sender As Object, e As EventArgs) Handles titleTextBox.LostFocus
        If Config.Tasks.ContainsKey(titleTextBox.Text.Trim) And titleTextBox.ReadOnly = False Then

            MsgBox("una attività con lo stesso nome è gia esistente, cambiare 'nome' attivita!")
            titleTextBox.Focus()
            Return
        End If
    End Sub

    ReadOnly AllowedKeys As String =
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.- "

    Private Sub titleTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles titleTextBox.KeyPress
        Select Case e.KeyChar

            Case Convert.ToChar(Keys.Enter) ' Enter is pressed
            ' Call method here...

            Case Convert.ToChar(Keys.Back) ' Backspace is pressed
                e.Handled = False ' Delete the character

            Case Convert.ToChar(Keys.Capital Or Keys.RButton) ' CTRL+V is pressed
                ' Paste clipboard content only if contains allowed keys
                e.Handled = Not Clipboard.GetText().All(Function(c) AllowedKeys.Contains(c))

            Case Else ' Other key is pressed
                e.Handled = Not AllowedKeys.Contains(e.KeyChar)

        End Select
    End Sub

    Private Sub titleTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles titleTextBox.KeyDown


    End Sub


    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub usernameTextBox_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_MouseDown(sender As Object, e As MouseEventArgs) Handles Button2.MouseDown
        originpasstext.UseSystemPasswordChar = False
    End Sub

    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        destpasstext.UseSystemPasswordChar = False
    End Sub

    Private Sub Button2_MouseUp(sender As Object, e As MouseEventArgs) Handles Button2.MouseUp
        originpasstext.UseSystemPasswordChar = True
    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As MouseEventArgs) Handles Button1.MouseUp
        destpasstext.UseSystemPasswordChar = True
    End Sub

    Private Sub originpasstext_TextChanged(sender As Object, e As EventArgs) Handles originpasstext.TextChanged

    End Sub

    Private Sub weeklyRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles weeklyRadioButton.CheckedChanged
        If weeklyRadioButton.Checked Then

            retentionNumericUpDown.Increment = 7
            retentionNumericUpDown.Value = 8
        End If
    End Sub

    Private Sub monthlyRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles monthlyRadioButton.CheckedChanged
        retentionNumericUpDown.Increment = 30
        retentionNumericUpDown.Value = 31
    End Sub

    Private Sub dailyRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles dailyRadioButton.CheckedChanged
        retentionNumericUpDown.Increment = 1
        retentionNumericUpDown.Value = 15
    End Sub
End Class
