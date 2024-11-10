Imports System.Collections
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Ipc


Public NotInheritable Class IpcServer
	Private Sub New()
	End Sub

	Public Shared Sub Start()

		Dim props As New Hashtable() From {
			{"authorizedGroup", "Everyone"},
			{"exclusiveAddressUse", False},
			{"portName", "RobocopyBackup"}
		}
		Dim channel As New IpcServerChannel(props, Nothing)
		ChannelServices.RegisterChannel(channel, False)
		RemotingConfiguration.RegisterWellKnownServiceType(GetType(IpcService), "ipc", WellKnownObjectMode.Singleton)
	End Sub
End Class
