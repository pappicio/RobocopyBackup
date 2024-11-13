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
                    If c.Username <> "" And c.Password <> "" Then
                        unc__1.Connect(c)
                    End If
                    c = New Credential
                    c.Username = Credential.Decrypt(task.Guid, task.destuser)
                    c.Password = Credential.Decrypt(task.Guid, task.destpass)
                    If c.Username <> "" And c.Password <> "" Then
                        unc__2.Connect(c)
                    End If

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

    Private Shared Sub timestamp_check(o As String, d As String, conservaper As UShort, logfile As String)


        Dim origine As String = ""
        Dim timestamp As Date = DateTime.Now


        If (d.StartsWith("\\")) And (Not d.StartsWith("\\?\UNC\")) Then
            d = "\\?\UNC\" & d.Substring(2)
        End If


        If (Not d.StartsWith("\\")) And (Not d.StartsWith("\\?\")) Then
            d = "\\?\" & d
        End If

        If (o.StartsWith("\\")) And (Not o.StartsWith("\\?\UNC\")) Then
            o = "\\?\UNC\" & o.Substring(2)
        End If

        If (Not o.StartsWith("\\")) And (Not o.StartsWith("\\?\")) Then
            o = "\\?\" & o
        End If

        Dim myfiles As String() = IO.Directory.GetFiles(d, "*.*", IO.SearchOption.AllDirectories)

        '''Dim yourfiles As String() = IO.Directory.GetFiles(o, "*.*", IO.SearchOption.AllDirectories)


        For Each file As String In myfiles

            origine = file.Replace(d, o)

            Dim originlong As Boolean = False

            ' Se il percorso supera i 255 caratteri, aggiungi il prefisso \\?\UNC\
            ''' If origine.Length > 255 Then



            'in test, se va bene, poi posso anche eliminare tutto qui sotto...............
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If (origine.StartsWith("\\")) And (Not origine.StartsWith("\\?\UNC\")) And (Not origine.StartsWith("\\?\")) Then
                origine = "\\?\UNC\" & origine.Substring(2) ' Rimuovi \\ e aggiungi \\?\UNC\
                originlong = True
            End If
            If (Not origine.StartsWith("\\")) And (Not origine.StartsWith("\\?\UNC\")) And (Not origine.StartsWith("\\?\")) Then
                origine = "\\?\" & origine ' Rimuovi \\ e aggiungi \\?\UNC\
                originlong = True
            End If
            '''End If

            If (file.StartsWith("\\")) And (Not file.StartsWith("\\?\UNC\")) And (Not file.StartsWith("\\?\")) Then
                file = "\\?\UNC\" & file.Substring(2) ' Rimuovi \\ e aggiungi \\?\UNC\
                '''''  originlong = True
            End If
            If (Not file.StartsWith("\\")) And (Not file.StartsWith("\\?\UNC\")) And (Not file.StartsWith("\\?\")) Then
                file = "\\?\" & file ' Rimuovi \\ e aggiungi \\?\UNC\
                ''''' originlong = True
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Dim solofolder As String = epura(Mid(file, 1, file.LastIndexOf("\")))
            If oldfolder <> solofolder Then
                oldfolder = solofolder
                Logger.Log2("")
                Logger.Log("[TASK] analyzing folder: " & oldfolder)
            End If

            Dim origince As Boolean = False

            ''' If originlong Then
            ' Verifica l'esistenza del file
            '''If longfile.FileExists(origine) Then
            '''origince = True
            '''End If
            '''Else
            ''' Dim a As Boolean = IO.File.Exists(origine)
            '''If a Then
            '''origince = True
            ''' End If
            '''End If




            'If origince Then
            If IO.File.Exists(origine) Then
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


            ''''' in testing, se va bene, posso anche eliminare qui sotto
            '''''''''''''''''''''''''''''''''''''''''''
            If (subDir.StartsWith("\\")) And (Not subDir.StartsWith("\\?\UNC\")) And (Not subDir.StartsWith("\\?\")) Then
                subDir = "\\?\UNC\" & subDir.Substring(2) ' Rimuovi \\ e aggiungi \\?\UNC\
            End If

            If (Not subDir.StartsWith("\\")) And (Not subDir.StartsWith("\\?\UNC\")) And (Not subDir.StartsWith("\\?\")) Then
                subDir = "\\?\" & subDir
            End If
            '''''''''''''''''''''''''''''''''''''''''''
            '''


            ' Richiama ricorsivamente la funzione per scansionare eventuali sottodirectory
            DeleteEmptyFolder(subDir)



            ' Controlla se la directory � vuota
            If Directory.GetFiles(subDir).Length = 0 AndAlso Directory.GetDirectories(subDir).Length = 0 Then

                ' Elimina la directory se � vuota

                Try

                    Logger.Log("[DELETE] Deleted empty folder: " & epura(subDir))
                Catch ex As Exception
                    Logger.Log("[DELETE] " & ex.Message)

                End Try

                Try
                    Directory.Delete(subDir)
                Catch ex As Exception
                    Logger.Log("[DELETE] ERROR deleting empty folder: " & epura(subDir))
                End Try


            End If
        Next








    End Sub

    Shared Function epura(s As String) As String
        If s.StartsWith("\\?\UNC\") Then
            Return s.Replace("\\?\UNC\", "\\")
        End If
        Return s.Replace("\\?\", "")
    End Function

End Class
