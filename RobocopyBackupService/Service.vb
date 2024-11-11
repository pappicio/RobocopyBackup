Imports System.IO
Imports System.Reflection
Imports System.ServiceProcess
Imports System.Threading
Imports RobocopyBackup.RobocopyBackup

Partial Public Class Service
    Inherits ServiceBase

    Private Const _period As Integer = 60000 '1000 '60000

    Private Shared ReadOnly _skewLimit As TimeSpan = TimeSpan.FromSeconds(90)

    Private Shared _lastTick As DateTime

    Private Shared _timer As Timer


    Shared Sub New()
        Service._skewLimit = TimeSpan.FromSeconds(90)
        Service._timer = New Timer(New TimerCallback(AddressOf Service.OnTick), Nothing, -1, -1)
    End Sub

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Public Shared Sub Main(ByVal args As String())
        Dim service As Service = New Service()
#If DEBUG Then

        ' service.OnStart(args)
        'Console.WriteLine("Press any key to stop program")

        'Console.Write("Press any key to continue . . . ")
        'Console.ReadKey(True)
        'service.OnStop()
        ServiceBase.Run(New Service())
#Else
        ServiceBase.Run(New Service())
#End If



    End Sub


    Protected Overrides Sub OnStart(args As String())
        Dim dir As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        'If IO.File.Exists(dir & "\config.xml") = False Then
        'Config.debug = True
        'End If
        Config.Load()

        If (args.Length = 1 AndAlso args(0) = "/log") Or Config.debug Then
            runservicelog()
            serviceLogger.LogServiceStarted()
        End If


        LogCleaner.Start()
        IpcServer.Start()
        SetNextTick(DateTime.Now)

        'da eliminare...........
        '''IpcService.RunTask("aaaaa")
    End Sub
    Shared Sub runservicelog()

        If Config.debug Then
            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd")
            serviceLogger.close()
            serviceLogger.Open(Path.Combine(Config.LogRoot, "service_" & timestamp & ".log"))
        End If

    End Sub

    Protected Overrides Sub OnStop()
        _timer.Change(Timeout.Infinite, Timeout.Infinite)
        _timer = Nothing
        For Each task As Task In Config.Tasks.Values
            task.[Stop]()
        Next
        serviceLogger.LogServiceStopped()
    End Sub


    Private Shared Sub DoTick(now As DateTime)
        For Each task As Task In Config.Tasks.Values
            If task.NextRunDate <= now Then
                runservicelog()
                task.Start()
                task.RefreshNextRunDate()
            End If
        Next
        If LogCleaner.NextRunDate <= now Then
            LogCleaner.Start()
            LogCleaner.RefreshNextRunDate()
        End If
    End Sub

    Private Shared Sub OnTick(state As Object)
        serviceLogger.LogTick()
        Dim now As DateTime = DateTime.Now
        If now - _lastTick > _skewLimit OrElse now < _lastTick Then
            serviceLogger.LogTickSkew()
            For Each task As Task In Config.Tasks.Values
                task.RefreshNextRunDate()
            Next
            LogCleaner.Start()
            LogCleaner.RefreshNextRunDate()
        Else
            DoTick(now)
        End If
        SetNextTick(now)
    End Sub

    Private Shared Sub SetNextTick(ByVal now As DateTime)
        Dim second As Integer = 60000 - now.Second * 1000 - now.Millisecond
        serviceLogger.LogTickDelay(second)
        Service._timer.Change(second, 60000)
        Service._lastTick = now
    End Sub
End Class
