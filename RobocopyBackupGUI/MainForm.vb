Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ServiceProcess
Imports System.Reflection
Imports RobocopyBackup.RobocopyBackup

Partial Public Class MainForm
    Inherits Form

    Public Sub New()
        InitializeComponent()
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
        Localize()
        RedrawTasks()
    End Sub

    Private Shared Function DrawTask(task As Task) As ListViewItem
        Dim method__1 As String = Lang.[Get]("IncrementalAbbr")
        If task.Method = Method.Differential Then
            method__1 = Lang.[Get]("DifferentialAbbr")
        ElseIf task.Method = Method.Full Then
            method__1 = Lang.[Get]("FullAbbr")
        End If
        Dim schedule As String = Lang.[Get]("DailyAbbr")
        If task.Period = Period.Weekly Then
            schedule = String.Format("{0}, {1}", Lang.[Get]("WeeklyAbbr"), Lang.[Get](task.DayOfWeek.ToString()))
        ElseIf task.Period = Period.Monthly Then
            schedule = String.Format("{0}, {1}.", Lang.[Get]("MonthlyAbbr"), task.DayOfMonth)
        End If
        schedule = String.Format("{0}, {1} @ {2:D2}:{3:D2}", method__1, schedule, task.Hour, task.Minute)
        Return New ListViewItem(New String() {task.Title, schedule, task.Source, task.Destination, task.NTFS}) With {
              .Tag = task.Guid
        }
    End Function


    Private Shared Sub SaveConfig()
        Config.Save()
        Try
            IpcClient.GetService().ReloadConfig()
        Catch
            MessageBox.Show(Lang.[Get]("UnableToConnectService"), Lang.[Get]("Error"), MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub AboutToolStripButton_Click(sender As Object, e As EventArgs) Handles aboutToolStripButton.Click
        Dim about As New AboutForm()
        about.ShowDialog()
    End Sub

    Private Sub DeleteTaskToolStripButton_Click(sender As Object, e As EventArgs) Handles deleteTaskToolStripButton.Click
        If MessageBox.Show(Lang.[Get]("DeleteConfirmation"), Lang.[Get]("Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim guid As String = DirectCast(taskListView.SelectedItems(0).Tag, String)
            Config.Tasks.Remove(guid)
            SaveConfig()
            RedrawTasks()
        End If
    End Sub

    Private Sub EditTaskToolStripButton_Click(sender As Object, e As EventArgs) Handles editTaskToolStripButton.Click
        Dim guid As String = DirectCast(taskListView.SelectedItems(0).Tag, String)
        Dim editForm As New TaskForm(Config.Tasks(guid))

        If editForm.ShowDialog() = DialogResult.OK Then
            Config.Tasks(guid) = editForm.ResultTask
            SaveConfig()
            RedrawTasks()
        End If
    End Sub

    Private Sub Localize()
        newTaskToolStripButton.Text = Lang.[Get]("NewTask")
        editTaskToolStripButton.Text = Lang.[Get]("EditTask")
        deleteTaskToolStripButton.Text = Lang.[Get]("DeleteTask")
        showTaskLogsToolStripButton.Text = Lang.[Get]("ShowTaskLogs")
        runTaskToolStripButton.Text = Lang.[Get]("RunTask")
        settingsToolStripButton.Text = Lang.[Get]("Settings")
        aboutToolStripButton.Text = Lang.[Get]("About")
        titleColumnHeader.Text = Lang.[Get]("Title")
        scheduleColumnHeader.Text = Lang.[Get]("Schedule")
        sourceColumnHeader.Text = Lang.[Get]("Source")
        destinationColumnHeader.Text = Lang.[Get]("Destination")
    End Sub


    Private Sub NewTaskToolStripButton_Click(sender As Object, e As EventArgs) Handles newTaskToolStripButton.Click
        Dim newForm As New TaskForm()
        If newForm.ShowDialog() = DialogResult.OK Then

            Dim t As Task = newForm.ResultTask
            t.Guid = t.Title
            Config.Tasks(t.Guid) = t
            SaveConfig()
            RedrawTasks()
        End If
    End Sub

    Private Sub RedrawTasks()
        taskListView.Items.Clear()
        For Each task As Task In Config.Tasks.Values
            taskListView.Items.Add(DrawTask(task))
        Next
        taskListView.SelectedIndices.Clear()
        TaskListView_SelectedIndexChanged(Nothing, Nothing)
    End Sub


    Private Sub RunTaskToolStripButton_Click(sender As Object, e As EventArgs) Handles runTaskToolStripButton.Click
        Dim guid As String = DirectCast(taskListView.SelectedItems(0).Tag, String)
        If serviziorunna() = False Then
            Dim result As DialogResult = MessageBox.Show("l'attività verra comunque avviata, ma il servizio risulta inattivo, attivare il servizio prima?", "SERVIZIO INATTIVO", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                serviceokbutton_Click(Nothing, Nothing)
                Dim x As Integer = 0
                While serviziorunna() = False
                    x = x + 1
                    Application.DoEvents()

                    If x > 100 Then
                        MsgBox("Impossibile avviare il servizio...")
                        Exit While
                    End If
                    Threading.Thread.Sleep(10)
                End While

            End If
        End If
        Try
#Disable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza
            IpcClient.GetService().RunTask(guid)
#Enable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza
            MessageBox.Show(Lang.[Get]("BackupStarted"), Lang.[Get]("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
            MessageBox.Show(Lang.[Get]("UnableToConnectService"), Lang.[Get]("Error"), MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub SettingsToolStripButton_Click(sender As Object, e As EventArgs) Handles settingsToolStripButton.Click
        Dim settings As New SettingsForm()
        If settings.ShowDialog() = DialogResult.OK Then
            Dim langChanged As Boolean = settings.Language <> Config.Language
            Config.LogRetention = settings.LogRetention
            Config.Language = settings.Language

            Config.debug = settings.debug
            SaveConfig()
            If langChanged Then
                Lang.SetLang()
                Localize()
                RedrawTasks()
            End If
        End If
    End Sub


    Private Sub ShowTaskLogsToolStripButton_Click(sender As Object, e As EventArgs) Handles showTaskLogsToolStripButton.Click
        Dim guid As String = DirectCast(taskListView.SelectedItems(0).Tag, String)
        Dim logDir As String = Path.Combine(Config.LogRoot, guid)
        If Not Directory.Exists(logDir) Then
            MessageBox.Show(Lang.[Get]("NoTaskLogs"), Lang.[Get]("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Process.Start(logDir)
    End Sub


    Private Sub TaskListView_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles taskListView.MouseDoubleClick
        EditTaskToolStripButton_Click(sender, Nothing)
    End Sub


    Private Sub TaskListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles taskListView.SelectedIndexChanged
        Dim selected As Boolean = taskListView.SelectedItems.Count <> 0
        editTaskToolStripButton.Enabled = selected
        deleteTaskToolStripButton.Enabled = selected
        showTaskLogsToolStripButton.Enabled = selected
        runTaskToolStripButton.Enabled = selected
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkservizio()
    End Sub

    Private Sub settingsToolStripButton_Click_1(sender As Object, e As EventArgs)

    End Sub
    Public Sub RunCMD(command As String, Optional ShowWindow As Boolean = False, Optional WaitForProcessComplete As Boolean = False, Optional permanent As Boolean = False)
        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = " " + If(ShowWindow AndAlso permanent, "/K", "/C") + " " + command
        pi.FileName = "cmd.exe"
        pi.CreateNoWindow = Not ShowWindow
        If ShowWindow Then
            pi.WindowStyle = ProcessWindowStyle.Normal
        Else
            pi.WindowStyle = ProcessWindowStyle.Hidden
        End If
        p.StartInfo = pi
        p.Start()
        If WaitForProcessComplete Then Do Until p.HasExited : Loop
    End Sub


    Sub checkservizio()

        If serviziorunna() Then

            serviceokbutton.Image = My.Resources.serviceok
            serviceokbutton.Text = "killa il Servizio"

        Else

            serviceokbutton.Image = My.Resources.serviceko
            serviceokbutton.Text = "Avvia il Servizio"
        End If

    End Sub

    Function serviziorunna() As Boolean
        Dim service As ServiceController = New ServiceController("RobocopyBackupService")
        Try
            If ((service.Status.Equals(ServiceControllerStatus.Stopped)) Or (service.Status.Equals(ServiceControllerStatus.StopPending))) Then

                Return False

            Else

                Return True

            End If
        Catch ex As Exception

        End Try
        Return False
    End Function

    Sub attendion()
        Dim x As Integer = 0
        While serviziorunna() = False
            x = x + 1
            Application.DoEvents()

            If x > 100 Then
                MsgBox("Impossibile avviare il servizio...")

                Dim ss As String = "SC stop RobocopyBackupService"
                RunCMD(ss,, True)
                Dim s As String = "SC delete RobocopyBackupService"
                RunCMD(s,, True)
                Exit While
            End If
            Threading.Thread.Sleep(10)
        End While

    End Sub
    Sub attendioff()
        Application.DoEvents()

    End Sub

    Private Sub serviceokbutton_Click(sender As Object, e As EventArgs) Handles serviceokbutton.Click
        Dim dir As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        If IO.File.Exists(dir & "\config.xml") = False And serviziorunna() = False Then
            MsgBox("nessun task presente, prima crea un task! esco...")
            Return

        End If
        If IO.File.Exists(Application.StartupPath & "\RobocopyBackupService.exe") = False Then
            MsgBox("servizio (RobocopyBackupService.exe) non presente nella  cartella, impossibile creare/avviare il servizio...")
            Return
        End If

        Dim service As ServiceController = New ServiceController("RobocopyBackupService")

        If serviziorunna() = False Then



            Dim k As String = ""
            Try
                k = service.DisplayName
            Catch ex As Exception
                k = ""
            End Try
            If k = "" Then
                Dim sx As String = "SC create RobocopyBackupService displayname= " & Chr(34) & "RobocopyBackupService" & Chr(34) & " binpath= " & Chr(34) & Application.StartupPath & "\RobocopyBackupService.exe" & Chr(34) & " start= auto"
                RunCMD(sx,, True)
                'riavvia 1/riavvia 2/riavvia sempre > riavvia dopo 1 minuto
                ' sx = "SC failure RobocopyBackupService reset= 86400  actions= restart/60000/restart/60000/restart/60000/" 
                'riavvia 1/riavvia 2/non riavviare piu > riavvia dopo 1 minuto
                sx = "SC failure RobocopyBackupService reset= 86400  actions= restart/60000/restart/60000//"
                RunCMD(sx,, True)

            End If


            Try
                If ((service.Status.Equals(ServiceControllerStatus.Stopped)) Or (service.Status.Equals(ServiceControllerStatus.StopPending))) Then
                    Dim sxs As String = "SC start RobocopyBackupService"
                    RunCMD(sxs,, True)

                End If

            Catch ex As Exception

            End Try




            attendion()

            Application.DoEvents()
            Dim ora As Date = Now.AddSeconds(5)
            Do While ora > Now
                Threading.Thread.Sleep(1)
                Application.DoEvents()
            Loop


            Dim l As Long = 0
            Dim ll As Long = 0
        Else

rifallo:


            attendioff()
            checkservizio()

            Try
                '   IO.File.Delete(apppath & "\eseguiscansioneora")
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try




            Try
                service.Stop()
            Catch ex As Exception

            End Try

            Dim ss As String = "SC stop RobocopyBackupService"
            RunCMD(ss,, True)
            Dim s As String = "SC delete RobocopyBackupService"
            RunCMD(s,, True)
            checkservizio()

        End If

    End Sub

    Private Sub timerservice_Tick(sender As Object, e As EventArgs) Handles timerservice.Tick
        checkservizio()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub
End Class
