Partial Class SettingsForm
	Private components As System.ComponentModel.IContainer = Nothing

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Windows Form Designer generated code"


    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsForm))
        Me.logRetentionLabel = New System.Windows.Forms.Label()
        Me.logRetentionNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.saveSettingsButton = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.logRetentionNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'logRetentionLabel
        '
        Me.logRetentionLabel.AutoSize = True
        Me.logRetentionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.logRetentionLabel.Location = New System.Drawing.Point(21, 34)
        Me.logRetentionLabel.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.logRetentionLabel.Name = "logRetentionLabel"
        Me.logRetentionLabel.Size = New System.Drawing.Size(176, 29)
        Me.logRetentionLabel.TabIndex = 2
        Me.logRetentionLabel.Text = "LogRetention:"
        '
        'logRetentionNumericUpDown
        '
        Me.logRetentionNumericUpDown.Location = New System.Drawing.Point(356, 32)
        Me.logRetentionNumericUpDown.Margin = New System.Windows.Forms.Padding(7)
        Me.logRetentionNumericUpDown.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.logRetentionNumericUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.logRetentionNumericUpDown.Name = "logRetentionNumericUpDown"
        Me.logRetentionNumericUpDown.Size = New System.Drawing.Size(105, 34)
        Me.logRetentionNumericUpDown.TabIndex = 3
        Me.logRetentionNumericUpDown.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'saveSettingsButton
        '
        Me.saveSettingsButton.Location = New System.Drawing.Point(26, 146)
        Me.saveSettingsButton.Margin = New System.Windows.Forms.Padding(7)
        Me.saveSettingsButton.Name = "saveSettingsButton"
        Me.saveSettingsButton.Size = New System.Drawing.Size(435, 51)
        Me.saveSettingsButton.TabIndex = 4
        Me.saveSettingsButton.Text = "SaveSettings"
        Me.saveSettingsButton.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(26, 90)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(204, 33)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Log di DEBUG"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(15.0!, 29.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 213)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.saveSettingsButton)
        Me.Controls.Add(Me.logRetentionNumericUpDown)
        Me.Controls.Add(Me.logRetentionLabel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SettingsForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        CType(Me.logRetentionNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private WithEvents logRetentionLabel As System.Windows.Forms.Label
    Private WithEvents logRetentionNumericUpDown As System.Windows.Forms.NumericUpDown
    Private WithEvents saveSettingsButton As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
End Class
