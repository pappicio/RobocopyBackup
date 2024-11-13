Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports Microsoft.Win32.SafeHandles
Imports RobocopyBackup.RobocopyBackup

Public Enum EntryType
    File
    Directory
End Enum

Public NotInheritable Class SysUtils
    Private Sub New()
    End Sub

    Public Shared Sub DeleteOldLogs()
        Dim threshold As DateTime = DateTime.Now.AddDays(-Config.LogRetention)
        Dim logRoot As New DirectoryInfo(Config.LogRoot)
        For Each directory As DirectoryInfo In logRoot.EnumerateDirectories()
            For Each file As FileInfo In directory.EnumerateFiles().Where(Function(f) f.LastWriteTime < threshold)
                Try
                    file.Delete()
                    serviceLogger.LogLogFileDeleted(file.FullName)
                    ' Ignore if the old log can't be deleted now 
                Catch
                End Try
            Next
        Next
    End Sub

    Public Shared Sub RunBackup(task As Task)
        Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
        Dim logFile As String = GetLogName(task.Guid, timestamp)
        Dim destDir As String = (task.Destination)

        Try

            Dim uncPath As String = If(Unc.IsUncPath(task.Source), task.Source, If(Unc.IsUncPath(task.Destination), task.Destination, Nothing))
            Using unc__1 As New Unc(uncPath)
                Using unc__2 As New Unc(uncPath)
                    Dim c As New Credential
                    c.Username = Credential.Decrypt(task.Guid, task.originuser)
                    c.Password = Credential.Decrypt(task.Guid, task.originpass)
                    unc__1.Connect(c)
                    c.Username = Credential.Decrypt(task.Guid, task.destuser)
                    c.Password = Credential.Decrypt(task.Guid, task.destpass)
                    unc__2.Connect(c)
                    RunRobocopy(task, logFile)
                End Using
            End Using
        Catch e As Exception
            Using streamWriter As New StreamWriter(logFile, True)
                streamWriter.Write(e.Message)
                streamWriter.Write(e.StackTrace)
            End Using
        End Try
    End Sub




    Private Shared Sub deletefiles(f As String)
        Dim psi As New ProcessStartInfo("cmd", String.Format("/C rmdir /s /q {0}", EscapeArg(f))) With {
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Dim rmdir As Process = Process.Start(psi)
        rmdir.WaitForExit()
        Logger.LogFileDeleted(f)
    End Sub

    Private Shared Sub DeleteOldfiles(origin As String, dir As String, retention As UShort)
        Dim files As String() = Directory.GetFiles(dir)
        If files.Length > System.Threading.Interlocked.Increment(CLng(retention)) Then
            For Each file As String In files.Take(files.Length - retention)
                deletefiles(file)
            Next
        End If
    End Sub

    Private Shared Function EscapeArg(arg As String) As String
        Return """" & Regex.Replace(arg, "(\\+)$", "$1$1") & """"
    End Function


    Private Shared Function GetLogName(guid As String, timestamp As String) As String
        Dim logDir As String = Path.Combine(Config.LogRoot, guid)
        If Not Directory.Exists(logDir) Then
            Directory.CreateDirectory(logDir)
        End If
        Return Path.Combine(logDir, String.Format("{0}.log", timestamp))
    End Function

    Private Shared Sub RunRobocopy(task As Task, logFile As String)
        Dim args As New List(Of String)() From {
            EscapeArg(task.Source)
        }
        args.Add(EscapeArg(task.Destination))

        Dim only As Boolean = False

        If task.onlyfolder <> "" Then
            only = True
            args.Add(String.Format("/E /CREATE /DST /DCOPY:DAT /XF * /XJ /R:1 /W:1 ""/UNILOG+:{0}""", logFile))
        Else
            If task.NTFS <> "" Then
                'args.Add(String.Format("/e /sec /COPYALL /R:3 /W:3 /NP ""/UNILOG+:{0}""", logFile))
                args.Add(String.Format("/e /sec /COPYALL /R:1 /W:1 /NP ""/UNILOG+:{0}""", logFile))
            Else
                'args.Add(String.Format("/e /sec /DCOPY:DAT /COPY:DAT /R:3 /W:3 /NP ""/UNILOG+:{0}""", logFile))
                args.Add(String.Format("/e /sec /DCOPY:DAT /COPY:DAT /R:1 /W:1 /NP ""/UNILOG+:{0}""", logFile))
            End If
        End If



        Dim psi As New ProcessStartInfo("robocopy", String.Join(" ", args)) With {
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Dim robocopy As Process = Process.Start(psi)
        robocopy.WaitForExit()

        If only = False Then
            Logger.Open(logFile)
            Logger.Log("***************************************")
            Try
                timestamp_check(task.Source, task.Destination, task.Retention, logFile)
            Catch ex As Exception
                Logger.Log("[TASK] ERROR: " & ex.Message)
            End Try
            Logger.close()
        End If
        only = False
    End Sub

    Shared oldfolder As String = ""



    ' Dichiarazione dell'API FindFirstFile di Windows
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Shared Function FindFirstFile(lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATA) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function FindClose(hFindFile As IntPtr) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure WIN32_FIND_DATA
        Public dwFileAttributes As UInteger
        Public ftCreationTime As Long
        Public ftLastAccessTime As Long
        Public ftLastWriteTime As Long
        Public nFileSizeHigh As UInteger
        Public nFileSizeLow As UInteger
        Public dwReserved0 As UInteger
        Public dwReserved1 As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public cFileName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
    End Structure
    Shared Function FileExists(ByVal filePath As String) As Boolean
        ' Rimuove il prefisso \\?\ se presente, poiché FindFirstFile lo gestisce direttamente
        If filePath.StartsWith("\\?\") Then
            filePath = filePath.Substring(4)
        End If

        ' Struttura per ricevere le informazioni del file
        Dim findData As WIN32_FIND_DATA

        ' Chiama FindFirstFile per verificare l'esistenza del file
#Disable Warning BC42108 ' La variabile è stata passata per riferimento prima dell'assegnazione di un valore
        Dim hFindFile As IntPtr = FindFirstFile(filePath, findData)
#Enable Warning BC42108 ' La variabile è stata passata per riferimento prima dell'assegnazione di un valore
        If hFindFile <> IntPtr.Zero Then
            ' Chiudi l'handle se troviamo il file
            FindClose(hFindFile)
            Return True
        Else
            ' Nessun file trovato o errore nell'accesso
            Return False
        End If
    End Function




    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Shared Sub timestamp_check(o As String, d As String, conservaper As UShort, logfile As String)


        Dim origine As String = ""
        Dim timestamp As Date = DateTime.Now

        If (d.StartsWith("\\")) And (Not d.StartsWith("\\?\UNC\")) Then
            d = "\\?\UNC\" & d.Substring(2)
        End If

        Dim myfiles As String() = IO.Directory.GetFiles(d, "*.*", IO.SearchOption.AllDirectories)

        For Each file As String In myfiles

            origine = file.Replace(d, o)

            Dim originlong As Boolean = False

            ' Se il percorso supera i 255 caratteri, aggiungi il prefisso \\?\UNC\
            If origine.Length > 255 Then
                If (origine.StartsWith("\\")) And (Not origine.StartsWith("\\?\UNC\")) Then
                    origine = "\\?\UNC\" & origine.Substring(2) ' Rimuovi \\ e aggiungi \\?\UNC\
                    originlong = True
                End If
                If (Not origine.StartsWith("\\")) And (Not origine.StartsWith("\\?\UNC\")) And (Not origine.StartsWith("\\?\")) Then
                    origine = "\\?\" & origine ' Rimuovi \\ e aggiungi \\?\UNC\
                    originlong = True
                End If
            End If


            If (file.StartsWith("\\")) And (Not file.StartsWith("\\?\UNC\")) Then
                file = "\\?\UNC\" & file.Substring(2) ' Rimuovi \\ e aggiungi \\?\UNC\
            End If


            Dim solofolder As String = Mid(file, 1, file.LastIndexOf("\"))
            If oldfolder <> solofolder Then
                oldfolder = solofolder
                Logger.Log2("")
                Logger.Log("[TASK] analyzing folder: " & oldfolder)
            End If

            Dim origince As Boolean = False

            If originlong Then
                ' Verifica l'esistenza del file
                If FileExists(origine) Then
                    origince = True
                End If
            Else
                Dim a As Boolean = IO.File.Exists(origine)
                If a Then
                    origince = True
                End If
            End If




            If origince Then

                Dim fi As New FileInfo(file)

                With fi
                    If (.Attributes And FileAttributes.Archive) = FileAttributes.Archive Then
                        'File has the Archive attribute set
                    End If
                    If (.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                        'File is Hidden
                    End If
                    Try
                        If (.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                            .Attributes = .Attributes And Not FileAttributes.ReadOnly
                            '''Logger.Log2("[TASK] readonly attribute deleted on: " & fi.Name)
                        End If
                    Catch ex As Exception
                        Logger.Log2("[TASK] ERROR deleting readonly flag on: " & fi.Name)
                    End Try

                End With


                'fi.CreationTime = DateTime.Now
                Try

                    fi.LastWriteTime = timestamp
                    '''Logger.Log2("[TIMESTAMP] new timestamp ok on: " & fi.Name)
                Catch ex As Exception
                    Logger.Log2("[TIMESTAMP] ERROR setting timestamp on: " & fi.Name)
                End Try

                'Logger.updatefile(file)
            Else
                Dim fi As New FileInfo(file)

                Dim x As Integer = Convert.ToInt32(DateDiff(DateInterval.Day, fi.LastWriteTime, Now.Date))
                If x > conservaper Then
                    '   Dim f As String = Mid(file, file.LastIndexOf("\") + 2, file.Length)
                    Try

                        fi.Delete()

                        '''Logger.Log2("[DELETE] deleted: " & fi.Name)
                    Catch ex As Exception
                        Logger.Log2("[DELETE] ERROR Deleting:" & fi.Name)
                    End Try
                Else
                    Logger.Log2("[TIMESTAMP] orphan file: " & fi.Name & " is old: " & x & " days of " & conservaper)
                End If
            End If
            '''''Logger.Log("")
        Next

        Try
            DeleteEmptyFolder(d)
        Catch ex As Exception
            Logger.Log("[TASK] ERROR on analyzing files..." & ex.Message)
        End Try

    End Sub

    Private Shared Sub DeleteEmptyFolder(ByVal sDirectoryPath As String)

        ' Se il percorso supera i 255 caratteri, aggiungi il prefisso \\?\UNC\


        ' Ottieni tutte le sottodirectory della directory corrente
        Dim sottodirectory As String() = Directory.GetDirectories(sDirectoryPath)


        For Each subDir As String In sottodirectory

            If (subDir.StartsWith("\\")) And (Not subDir.StartsWith("\\?\UNC\")) Then
                subDir = "\\?\UNC\" & subDir.Substring(2) ' Rimuovi \\ e aggiungi \\?\UNC\
            End If

            ' Richiama ricorsivamente la funzione per scansionare eventuali sottodirectory
            DeleteEmptyFolder(subDir)



            ' Controlla se la directory è vuota
            If Directory.GetFiles(subDir).Length = 0 AndAlso Directory.GetDirectories(subDir).Length = 0 Then

                ' Elimina la directory se è vuota

                Try

                    Logger.Log("[DELETE] Deleted empty folder: " & subDir)

                Catch ex As Exception
                    Logger.Log("[DELETE] " & ex.Message)

                End Try

                Try
                    Directory.Delete(subDir)
                Catch ex As Exception
                    Logger.Log("[DELETE] ERROR deleting empty folder: " & subDir)
                End Try


            End If
        Next








    End Sub


End Class
