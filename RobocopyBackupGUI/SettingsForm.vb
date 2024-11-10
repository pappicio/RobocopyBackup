
Imports System.Windows.Forms
Imports RobocopyBackup.RobocopyBackup

Partial Public Class SettingsForm
    Inherits Form

    Public Property Language() As String
        Get
            Return m_Language
        End Get
        Private Set
            m_Language = Value
        End Set
    End Property
    Private m_Language As String

    Public Property LogRetention() As UShort
        Get
            Return m_LogRetention
        End Get
        Private Set
            m_LogRetention = Value
        End Set
    End Property
    Private m_LogRetention As UShort

    Public Property debug As Boolean
        Get
            Return m_debug
        End Get
        Private Set
            m_debug = Value
        End Set
    End Property
    Private m_debug As Boolean

    Public Sub New()
        InitializeComponent()
        Localize()
        'languageComboBox.Items.AddRange(Lang.AvailableLocales.Keys.ToArray())
        'languageComboBox.Text = Lang.[Get]("")
        ' Name of the language is stored in the empty string key
        logRetentionNumericUpDown.Value = Config.LogRetention
        CheckBox1.Checked = Config.debug
    End Sub

    Private Sub Localize()
        Text = Lang.[Get]("Settings")
        'languageLabel.Text = Lang.[Get]("Language", ":")
        logRetentionLabel.Text = Lang.[Get]("LogRetention", ":")
        saveSettingsButton.Text = Lang.[Get]("SaveSettings")
    End Sub


    Private Sub SaveSettingsButton_Click(sender As Object, e As System.EventArgs) Handles saveSettingsButton.Click

        LogRetention = CUShort(Math.Truncate(logRetentionNumericUpDown.Value))
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then


            debug = True

        Else

            debug = False

        End If
    End Sub

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
