Imports System.Threading


Public NotInheritable Class LogCleaner
	Private Sub New()
	End Sub

	Public Shared ReadOnly Property NextRunDate() As DateTime
		Get
			If _nextRunDate = DateTime.MinValue Then
				RefreshNextRunDate()
			End If
			Return _nextRunDate
		End Get
	End Property


	Private Shared _cleanupThread As Thread

	Private Shared _nextRunDate As DateTime


	Public Shared Sub RefreshNextRunDate()
		_nextRunDate = DateTime.Today.AddDays(1)
		serviceLogger.LogCleanupNextRunDate(_nextRunDate)
	End Sub


	Public Shared Sub Start()
		serviceLogger.LogCleanupStart()
		_cleanupThread = New Thread(New ThreadStart(AddressOf SysUtils.DeleteOldLogs))
		_cleanupThread.Start()
	End Sub
End Class
