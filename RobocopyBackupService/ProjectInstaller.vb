Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess


<RunInstaller(True)> _
Public Partial Class ProjectInstaller
	Inherits Installer
	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub ServiceInstaller_AfterInstall(sender As Object, e As InstallEventArgs)
		Using serviceController As New ServiceController(serviceInstaller.ServiceName)
			serviceController.Start()
		End Using
	End Sub
End Class
