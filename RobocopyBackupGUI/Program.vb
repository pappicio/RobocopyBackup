Imports System.Windows.Forms
Imports RobocopyBackup.RobocopyBackup

Public NotInheritable Class Program
    Private Sub New()
    End Sub

    <STAThread>
    Public Shared Sub Main()
        Config.Load()
        Lang.SetLang()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New MainForm())
    End Sub
End Class
