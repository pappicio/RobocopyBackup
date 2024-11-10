Imports System.Threading

Public Enum Method
	Incremental
	Differential
	Full
End Enum

Public Enum Period
	Daily
	Weekly
	Monthly
End Enum

<Serializable> _
Public Class Task

	Public Property originuser() As String
		Get
			Return m_originuser
		End Get
		Set
			m_originuser = Value
		End Set
	End Property
	Private m_originuser As String

	Public Property originpass() As String
		Get
			Return m_originpass
		End Get
		Set
			m_originpass = Value
		End Set
	End Property
	Private m_originpass As String


	Public Property destuser() As String
		Get
			Return m_destuser
		End Get
		Set
			m_destuser = Value
		End Set
	End Property
	Private m_destuser As String

	Public Property destpass() As String
		Get
			Return m_destpass
		End Get
		Set
			m_destpass = Value
		End Set
	End Property
	Private m_destpass As String

	Public Property DayOfMonth() As Byte
		Get
			Return m_DayOfMonth
		End Get
		Set
			m_DayOfMonth = Value
		End Set
	End Property
	Private m_DayOfMonth As Byte

	Public Property DayOfWeek() As DayOfWeek
		Get
			Return m_DayOfWeek
		End Get
		Set
			m_DayOfWeek = Value
		End Set
	End Property
	Private m_DayOfWeek As DayOfWeek


	Public Property Destination() As String
		Get
			Return m_Destination
		End Get
		Set
			m_Destination = Value
		End Set
	End Property
	Private m_Destination As String


	Public Property NTFS() As String
		Get
			Return m_NTFS
		End Get
		Set
			m_NTFS = Value
		End Set
	End Property
	Private m_NTFS As String



	Public Property ExcludedDirs() As String()
		Get
			Return m_ExcludedDirs
		End Get
		Set
			m_ExcludedDirs = Value
		End Set
	End Property
	Private m_ExcludedDirs As String()


	Public Property ExcludedFiles() As String()
		Get
			Return m_ExcludedFiles
		End Get
		Set
			m_ExcludedFiles = Value
		End Set
	End Property
	Private m_ExcludedFiles As String()


	Public Property Guid() As String
		Get
			Return m_Guid
		End Get
		Set
			m_Guid = Value
		End Set
	End Property
	Private m_Guid As String


	Public Property Hour() As Byte
		Get
			Return m_Hour
		End Get
		Set
			m_Hour = Value
		End Set
	End Property
	Private m_Hour As Byte


	Public Property Method() As Method
		Get
			Return m_Method
		End Get
		Set
			m_Method = Value
		End Set
	End Property
	Private m_Method As Method


	Public Property Minute() As Byte
		Get
			Return m_Minute
		End Get
		Set
			m_Minute = Value
		End Set
	End Property
	Private m_Minute As Byte

	Public ReadOnly Property NextRunDate() As DateTime
		Get
			If _nextRunDate = DateTime.MinValue Then
				RefreshNextRunDate()
			End If
			Return _nextRunDate
		End Get
	End Property


	Public Property Period() As Period
		Get
			Return m_Period
		End Get
		Set
			m_Period = Value
		End Set
	End Property
	Private m_Period As Period


	Public Property Retention() As UShort
		Get
			Return m_Retention
		End Get
		Set
			m_Retention = Value
		End Set
	End Property
	Private m_Retention As UShort


	Public Property Source() As String
		Get
			Return m_Source
		End Get
		Set
			m_Source = Value
		End Set
	End Property
	Private m_Source As String

	Public Property Title() As String
		Get
			Return m_Title
		End Get
		Set
			m_Title = Value
		End Set
	End Property
	Private m_Title As String


	<NonSerialized> _
	Private _nextRunDate As DateTime


	<NonSerialized> _
	Private _thread As Thread


	Public Sub RefreshNextRunDate()
		Dim now As DateTime = DateTime.Now
		Dim nextRun As DateTime = DateTime.Today.AddHours(Hour).AddMinutes(Minute)
		If Period = Period.Daily Then
			If nextRun < now Then
				nextRun = nextRun.AddDays(1)
			End If
		ElseIf Period = Period.Weekly Then
			nextRun = nextRun.AddDays(DayOfWeek - now.DayOfWeek)
			If nextRun < now Then
				nextRun = nextRun.AddDays(7)
			End If
		Else
			While DayOfMonth > DateTime.DaysInMonth(nextRun.Year, nextRun.Month)
				nextRun = nextRun.AddMonths(1)
			End While
			nextRun = nextRun.AddDays(DayOfMonth - now.Day)
			If nextRun < now Then
				nextRun = nextRun.AddMonths(1)
			End If
		End If
		_nextRunDate = nextRun
		serviceLogger.LogTaskNextRunDate(Me)
	End Sub


	Public Sub Start()
		serviceLogger.LogTaskStart(Me)
		Me._thread = New Thread(Sub()
									SysUtils.RunBackup(Me)
								End Sub)
		Me._thread.Start()

	End Sub

	Public Sub [Stop]()
		If _thread IsNot Nothing AndAlso _thread.ThreadState = ThreadState.Running Then
			_thread.Abort()
		End If
	End Sub
End Class
