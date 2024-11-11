Imports System.IO

Imports RobocopyBackup.RobocopyBackup

Public NotInheritable Class Logger
    Private Sub New()
    End Sub

    Public Shared Property Writer() As StreamWriter
        Get
            Return m_Writer
        End Get
        Private Set
            m_Writer = Value
        End Set
    End Property
    Private Shared m_Writer As StreamWriter

    Private Const _dateFormat As String = "yyyy-MM-dd HH:mm:ss.fff"


    Public Shared Sub LogCleanupNextRunDate(nextCleanupDate As DateTime)
        If Writer IsNot Nothing Then
            Log(String.Format("[Cleanup] Next log cleanup scheduled at {0}", nextCleanupDate.ToString(_dateFormat)))
        End If
    End Sub

    Public Shared Sub LogCleanupStart()
        If Writer IsNot Nothing Then
            Log("[Cleanup] Log cleanup started")
        End If
    End Sub


    Public Shared Sub LogConfig()
        If Writer IsNot Nothing Then
            Log(String.Format("[Config] Got log retention period {0} days", Config.LogRetention))
            LogCleaner.RefreshNextRunDate()

            For Each task As Task In Config.Tasks.Values

                task.RefreshNextRunDate()
            Next
        End If
    End Sub

    Public Shared Sub LogConfigLoad()
        If Writer IsNot Nothing Then
            Log("[Config] Loading config")
        End If
    End Sub


    Public Shared Sub LogFileDeleted(file As String)
        If Writer IsNot Nothing Then

            Log(String.Format("[DELETE] Deleted old file {0}", file))
        End If
    End Sub

    Public Shared Sub updatefile(file As String)
        If Writer IsNot Nothing Then
            Log(String.Format("[Task] update timestamp on file {0}", file))
        End If
    End Sub
    Public Shared Sub LogFolderDeleted(file As String)
        If Writer IsNot Nothing Then
            Log(String.Format("[DELETE] Deleted empty Folder {0}", file))
        End If
    End Sub

    Public Shared Sub LogerrorFileDeleted(file As String)
        If Writer IsNot Nothing Then
            Log(String.Format("[Task] ERROR deleting file {0}", file))
        End If
    End Sub

    Public Shared Sub LogerrorfolderDeleted(file As String)
        If Writer IsNot Nothing Then
            Log(String.Format("[Task] ERROR deleting empty Folder {0}", file))
        End If
    End Sub

    Public Shared Sub LogIpcReloadMessage()
        If Writer IsNot Nothing Then
            Log("[Service] Received IPC request for config reload")
        End If
    End Sub

    Public Shared Sub LogIpcRunTaskMessage(guid As String)
        If Writer IsNot Nothing Then
            Log(String.Format("[Service] Received IPC request for task GUID {0} run", guid))
        End If
    End Sub


    Public Shared Sub LogLogFileDeleted(logFile As String)
        If Writer IsNot Nothing Then
            Log(String.Format("[Cleanup] Deleted old log file {0}", logFile))
        End If
    End Sub

    Public Shared Sub LogServiceStarted()
        If Writer IsNot Nothing Then
            Log("[Service] Service started")
        End If
    End Sub


    Public Shared Sub LogServiceStopped()
        If Writer IsNot Nothing Then
            Log("[Service] Service stopped")
        End If
    End Sub


    Public Shared Sub LogTaskNextRunDate(task As Task)
        If Writer IsNot Nothing Then
            Log(String.Format("[Task] Next task GUID {0} run scheduled at {1}", task.Guid, task.NextRunDate.ToString(_dateFormat)))
        End If
    End Sub

    Public Shared Sub LogTaskStart(task As Task)
        If Writer IsNot Nothing Then
            Log(String.Format("[Task] Task GUID {0} started", task.Guid))
        End If
    End Sub


    Public Shared Sub LogTickDelay(delay As Integer)
        If Writer IsNot Nothing Then
            Log(String.Format("[Timer] Next tick in {0} s", (delay / 1000)))
        End If
    End Sub

    Public Shared Sub LogTick()
        If Writer IsNot Nothing Then
            Log("[Timer] Tick")
        End If
    End Sub

    Public Shared Sub LogTickSkew()
        If Writer IsNot Nothing Then
            Log("[Timer] Last tick more than 90 seconds ago or in the future, refreshing schedules")
        End If
    End Sub

    Public Shared Sub Open(path As String)
        Writer = New StreamWriter(path, True) With {
             .AutoFlush = True
        }
    End Sub

    Public Shared Sub close()
        If Writer IsNot Nothing Then
            Log("***************************************")
            Writer.Close()
            Writer.Dispose()
        End If
    End Sub

    Public Shared Sub Open(stream As Stream)
        Writer = New StreamWriter(stream) With {
            .AutoFlush = True
        }
    End Sub

    Public Shared Sub Log(message As String)
        If Writer IsNot Nothing Then
            If message <> "" Then
                Writer.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString(_dateFormat), message))
            Else
                Writer.WriteLine("")
            End If

        End If

    End Sub
    Public Shared Sub Log2(message As String)
        If Writer IsNot Nothing Then
            If message <> "" Then
                Writer.WriteLine("    " & message)
            Else
                Writer.WriteLine("")
            End If

        End If

    End Sub
End Class
