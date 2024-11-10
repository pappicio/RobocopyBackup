Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Ipc


Public NotInheritable Class IpcClient
	Private Sub New()
	End Sub

	Private Shared _channel As IpcClientChannel

	Private Shared _service As IpcService

	Public Shared Function GetService() As IpcService
		Try
			_service.Ping()
		Catch
			RegisterIpcClient()
		End Try
		Return _service
	End Function

	Private Shared Sub RegisterIpcClient()
		If _channel IsNot Nothing Then
			ChannelServices.UnregisterChannel(_channel)
		End If
		_channel = New IpcClientChannel()
		ChannelServices.RegisterChannel(_channel, False)
		If _service Is Nothing Then
			Dim remoteType As New WellKnownClientTypeEntry(GetType(IpcService), "ipc://RobocopyBackup/ipc")
			RemotingConfiguration.RegisterWellKnownClientType(remoteType)
			_service = New IpcService()
		End If
	End Sub
End Class
