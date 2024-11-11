
Imports System.Windows.Forms
Imports Microsoft.Win32
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Percorso e nome della chiave di registro
        Dim registryPath As String = "SYSTEM\CurrentControlSet\Control\FileSystem"
        Dim valueName As String = "LongPathsEnabled"


        Dim input As String = Console.ReadLine()
        Dim enableLongPaths As Boolean


        Try
            ' Apri la chiave di registro in modalità scrittura
            Using key As RegistryKey = Registry.LocalMachine.OpenSubKey(registryPath, writable:=True)
                If key IsNot Nothing Then
                    ' Controlla se la chiave esiste
                    Dim currentValue As Object = key.GetValue(valueName)

                    ' Verifica se la chiave è già impostata e sul valore desiderato
                    If currentValue IsNot Nothing AndAlso Convert.ToInt32(currentValue) = 1 Then
                        MsgBox("La chiave è già impostata su 1. Nessuna modifica necessaria.")
                    Else
                        ' Imposta la chiave su 1 per abilitare o 0 per disabilitare
                        key.SetValue(valueName, 1, RegistryValueKind.DWord)
                        MsgBox("La chiave di registro è stata impostata su 1.")
                    End If
                Else
                    'Console.WriteLine("La chiave di registro specificata non esiste.")
                End If
            End Using
        Catch ex As UnauthorizedAccessException
            ' Console.WriteLine("Errore: permessi insufficienti. Esegui il programma come amministratore.")
        Catch ex As Exception
            ' Console.WriteLine($"Errore durante la modifica del registro: {ex.Message}")
        End Try

    End Sub
End Class
