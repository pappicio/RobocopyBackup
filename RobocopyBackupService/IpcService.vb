


Imports RobocopyBackup.RobocopyBackup

Public Class IpcService
    Inherits MarshalByRefObject

    Public Function Ping() As String
        Return "pong"
    End Function


    Public Sub ReloadConfig()
        serviceLogger.LogIpcReloadMessage()
        Config.Load()
        LogCleaner.Start()
    End Sub

    Public Shared Sub RunTask(guid As String)
        serviceLogger.LogIpcRunTaskMessage(guid)
        If Config.Tasks.ContainsKey(guid) Then
            Dim task As Task = Config.Tasks(guid)
            task.Start()
        End If
    End Sub
End Class
