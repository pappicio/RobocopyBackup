Partial Class Service
	Private components As System.ComponentModel.IContainer = Nothing

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Component Designer generated code"


	Private Sub InitializeComponent()
		' 
		' Service
		' 
		Me.ServiceName = "RobocopyBackup"

	End Sub

	#End Region
End Class
