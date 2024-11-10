Partial Class ProjectInstaller
	Private components As System.ComponentModel.IContainer = Nothing

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Component Designer generated code"


	Private Sub InitializeComponent()
		Me.serviceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
		Me.serviceInstaller = New System.ServiceProcess.ServiceInstaller()
		' 
		' serviceProcessInstaller
		' 
		Me.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
		Me.serviceProcessInstaller.Password = Nothing
		Me.serviceProcessInstaller.Username = Nothing
		' 
		' serviceInstaller
		' 
		Me.serviceInstaller.Description = "RobocopyBackup robocopy backup service"
		Me.serviceInstaller.DisplayName = "RobocopyBackup"
		Me.serviceInstaller.ServiceName = "RobocopyBackup"
		Me.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic
		AddHandler Me.serviceInstaller.AfterInstall, New System.Configuration.Install.InstallEventHandler(AddressOf Me.ServiceInstaller_AfterInstall)
		' 
		' ProjectInstaller
		' 
		Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.serviceProcessInstaller, Me.serviceInstaller})

	End Sub

	#End Region

	Private serviceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
	Private serviceInstaller As System.ServiceProcess.ServiceInstaller
End Class
