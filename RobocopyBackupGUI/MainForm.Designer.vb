Partial Class MainForm
	Private components As System.ComponentModel.IContainer = Nothing

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Windows Form Designer generated code"

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.toolStrip = New System.Windows.Forms.ToolStrip()
        Me.newTaskToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.editTaskToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.deleteTaskToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.aboutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.showTaskLogsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.runTaskToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.settingsToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.serviceokbutton = New System.Windows.Forms.ToolStripButton()
        Me.taskListView = New System.Windows.Forms.ListView()
        Me.titleColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.scheduleColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.sourceColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.destinationColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ntfscolumnheader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.timerservice = New System.Windows.Forms.Timer(Me.components)
        Me.toolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'toolStrip
        '
        Me.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.toolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newTaskToolStripButton, Me.toolStripSeparator1, Me.editTaskToolStripButton, Me.deleteTaskToolStripButton, Me.aboutToolStripButton, Me.toolStripSeparator2, Me.showTaskLogsToolStripButton, Me.runTaskToolStripButton, Me.settingsToolStripButton, Me.ToolStripSeparator3, Me.serviceokbutton})
        Me.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.toolStrip.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip.Name = "toolStrip"
        Me.toolStrip.Size = New System.Drawing.Size(1555, 27)
        Me.toolStrip.TabIndex = 1
        '
        'newTaskToolStripButton
        '
        Me.newTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.newTaskToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.NewIcon
        Me.newTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.newTaskToolStripButton.Name = "newTaskToolStripButton"
        Me.newTaskToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.newTaskToolStripButton.Text = "NewTask"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'editTaskToolStripButton
        '
        Me.editTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.editTaskToolStripButton.Enabled = False
        Me.editTaskToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.EditIcon
        Me.editTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.editTaskToolStripButton.Name = "editTaskToolStripButton"
        Me.editTaskToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.editTaskToolStripButton.Text = "EditTask"
        '
        'deleteTaskToolStripButton
        '
        Me.deleteTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.deleteTaskToolStripButton.Enabled = False
        Me.deleteTaskToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.DeleteIcon
        Me.deleteTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.deleteTaskToolStripButton.Name = "deleteTaskToolStripButton"
        Me.deleteTaskToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.deleteTaskToolStripButton.Text = "DeleteTask"
        '
        'aboutToolStripButton
        '
        Me.aboutToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.aboutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.aboutToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.HelpIcon
        Me.aboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.aboutToolStripButton.Name = "aboutToolStripButton"
        Me.aboutToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.aboutToolStripButton.Text = "About"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'showTaskLogsToolStripButton
        '
        Me.showTaskLogsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.showTaskLogsToolStripButton.Enabled = False
        Me.showTaskLogsToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.LogsIcon
        Me.showTaskLogsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.showTaskLogsToolStripButton.Name = "showTaskLogsToolStripButton"
        Me.showTaskLogsToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.showTaskLogsToolStripButton.Text = "ShowTaskLogs"
        '
        'runTaskToolStripButton
        '
        Me.runTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.runTaskToolStripButton.Enabled = False
        Me.runTaskToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.RunIcon
        Me.runTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.runTaskToolStripButton.Name = "runTaskToolStripButton"
        Me.runTaskToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.runTaskToolStripButton.Text = "RunTask"
        '
        'settingsToolStripButton
        '
        Me.settingsToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.settingsToolStripButton.Image = Global.RobocopyBackup.My.Resources.Resources.SettingsIcon
        Me.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.settingsToolStripButton.Name = "settingsToolStripButton"
        Me.settingsToolStripButton.Size = New System.Drawing.Size(29, 24)
        Me.settingsToolStripButton.Text = "Settings"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 27)
        '
        'serviceokbutton
        '
        Me.serviceokbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.serviceokbutton.Image = Global.RobocopyBackup.My.Resources.Resources.serviceok
        Me.serviceokbutton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.serviceokbutton.Name = "serviceokbutton"
        Me.serviceokbutton.Size = New System.Drawing.Size(29, 24)
        Me.serviceokbutton.Text = "Avvia servizio"
        '
        'taskListView
        '
        Me.taskListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.titleColumnHeader, Me.scheduleColumnHeader, Me.sourceColumnHeader, Me.destinationColumnHeader, Me.ntfscolumnheader})
        Me.taskListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.taskListView.FullRowSelect = True
        Me.taskListView.GridLines = True
        Me.taskListView.HideSelection = False
        Me.taskListView.Location = New System.Drawing.Point(0, 27)
        Me.taskListView.Margin = New System.Windows.Forms.Padding(8)
        Me.taskListView.Name = "taskListView"
        Me.taskListView.Size = New System.Drawing.Size(1555, 912)
        Me.taskListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.taskListView.TabIndex = 2
        Me.taskListView.UseCompatibleStateImageBehavior = False
        Me.taskListView.View = System.Windows.Forms.View.Details
        '
        'titleColumnHeader
        '
        Me.titleColumnHeader.Text = "Title"
        Me.titleColumnHeader.Width = 304
        '
        'scheduleColumnHeader
        '
        Me.scheduleColumnHeader.Text = "Schedule"
        Me.scheduleColumnHeader.Width = 251
        '
        'sourceColumnHeader
        '
        Me.sourceColumnHeader.Text = "Source"
        Me.sourceColumnHeader.Width = 464
        '
        'destinationColumnHeader
        '
        Me.destinationColumnHeader.Text = "Destination"
        Me.destinationColumnHeader.Width = 461
        '
        'ntfscolumnheader
        '
        Me.ntfscolumnheader.Text = "NTFS"
        Me.ntfscolumnheader.Width = 180
        '
        'timerservice
        '
        Me.timerservice.Enabled = True
        Me.timerservice.Interval = 1000
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(15.0!, 29.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1555, 939)
        Me.Controls.Add(Me.taskListView)
        Me.Controls.Add(Me.toolStrip)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(8)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RoboCopyBackup"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.toolStrip.ResumeLayout(False)
        Me.toolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents toolStrip As System.Windows.Forms.ToolStrip
    Private WithEvents newTaskToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents editTaskToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents deleteTaskToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents aboutToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents taskListView As System.Windows.Forms.ListView
    Private WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents showTaskLogsToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents runTaskToolStripButton As System.Windows.Forms.ToolStripButton
    Private WithEvents titleColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents scheduleColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents sourceColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents destinationColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents settingsToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator

    Friend WithEvents timerservice As Windows.Forms.Timer
    Friend WithEvents ntfscolumnheader As Windows.Forms.ColumnHeader
    Private WithEvents serviceokbutton As Windows.Forms.ToolStripButton
End Class
