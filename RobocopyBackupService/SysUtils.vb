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


    Public Shared Sub EseguiComandoNetUseDelete()
        ' Crea un nuovo processo per eseguire il comando nel prompt dei comandi
        Dim process As New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.Arguments = "/c net use * /delete /y" ' /c esegue il comando e poi chiude la finestra use * /delete /y
        process.StartInfo.RedirectStandardOutput = True ' Rende visibile l'output del comando (facoltativo)
        process.StartInfo.UseShellExecute = False ' Per non usare la shell di Windows
        process.StartInfo.CreateNoWindow = True ' Non mostrare la finestra del cmd

        Try
            process.Start() ' Avvia il processo
            process.WaitForExit() ' Attende che il processo termini
            Console.WriteLine("Comando eseguito con successo: net use * /delete")
        Catch ex As Exception
            Console.WriteLine("Errore durante l'esecuzione del comando: " & ex.Message)
        End Try
    End Sub

    Shared Function checkerror(s As String, a As Integer) As String
        Dim ss As String = GetNetUseAddErrorDescription(a)
        If a <> 0 Then
            Return ("[CONNECTION] to: " & s & " ERRORE nr: " & a & " - " & ss)
        Else
            Return "[CONNECTION] to: " & s & "OK"
        End If

    End Function

    Shared Function GetNetUseAddErrorDescription(errorNumber As Integer) As String
        ' ----------------------
        ' Codici generali Win32
        ' ----------------------
        Select Case errorNumber
            Case 0
                Return "Operazione completata con successo."
            Case 1
                Return "Funzione non valida."
            Case 2
                Return "File o risorsa non trovata."
            Case 3
                Return "Percorso non trovato."
            Case 5
                Return "Accesso negato."
            Case 6
                Return "Handle non valido."
            Case 8
                Return "Memoria insufficiente per completare l'operazione."
            Case 14
                Return "Memoria insufficiente disponibile."
            Case 31
                Return "Un dispositivo collegato al sistema non funziona correttamente."
            Case 50
                Return "L'operazione richiesta non è supportata."
            Case 53
                Return "Percorso di rete non trovato."
            Case 64
                Return "Il nome della rete non è più disponibile."
            Case 67
                Return "Nome della risorsa di rete non trovato."
            Case 87
                Return "Parametro non valido."
            Case 120
                Return "Questo dispositivo non accetta l'operazione specificata."
            Case 121
                Return "Timeout durante la connessione."
            Case 123
                Return "Nome non valido."
            Case 126
                Return "Modulo non trovato."
            Case 132
                Return "Nome file o percorso non valido."
            Case 183
                Return "Il file o la risorsa esiste già."
            Case 224
                Return "Il buffer è troppo piccolo."
            Case 234
                Return "I dati restituiti sono troppo grandi per il buffer."
            Case 487
                Return "Tentativo di accesso a un indirizzo non valido."
            Case 995
                Return "Operazione annullata dall'utente."
            Case 1200
                Return "Il dispositivo specificato non è valido."
            Case 1201
                Return "Il dispositivo è già stato ricordato."
            Case 1202
                Return "Il nome della rete è occupato o non valido."
            Case 1219
                Return "Connessioni multiple allo stesso server con credenziali diverse."
            Case 1312
                Return "Nessuna sessione di accesso trovata."
            Case 1314
                Return "Privilegio richiesto non disponibile."
            Case 1326
                Return "Nome utente o password non validi."
            Case 1327
                Return "Restrizioni sull'account utente."
            Case 1330
                Return "Password scaduta."
            Case 1331
                Return "Account disabilitato."
            Case 1355
                Return "Il dominio specificato non esiste o non è disponibile."
            Case 1385
                Return "Accesso negato: il tipo di accesso non è consentito."
            Case 1909
                Return "L'account è bloccato."
            Case 2250
                Return "La risorsa non è connessa."

        ' ----------------------
        ' Codici NetAPI specifici
        ' ----------------------
            Case 2107
                Return "Il nome di rete non è stato trovato."
            Case 2114
                Return "Il servizio di rete non è avviato."
            Case 2115
                Return "Il servizio richiesto è già attivo."
            Case 2123
                Return "Il buffer specificato è troppo piccolo."
            Case 2136
                Return "Password non valida."
            Case 2140
                Return "Errore generico del server remoto."
            Case 2142
                Return "La connessione specificata non è stata trovata."
            Case 2221
                Return "La risorsa condivisa non esiste."
            Case 2242
                Return "L'account utente è bloccato."
            Case 2401
                Return "La connessione al server è stata interrotta."
            Case 2404
                Return "Il server non è disponibile per questa operazione."
            Case 6118
                Return "L'elenco dei server non è disponibile."

        ' ----------------------
        ' Codici supplementari
        ' ----------------------
            Case 1384
                Return "Il client non dispone delle autorizzazioni necessarie per questa operazione."
            Case 2224
                Return "Il nome utente specificato non esiste."
            Case 2251
                Return "Il dispositivo richiesto non è pronto per l'uso."
            Case 317
                Return "Errore sconosciuto nella rete."
            Case 1208
                Return "Un problema di compatibilità del protocollo ha interrotto la connessione."
            Case 1323
                Return "La password specificata non è sufficientemente complessa."

                ' ----------------------
                ' Errore sconosciuto
                ' ----------------------
            Case Else
                Return $"Codice errore sconosciuto: {errorNumber}. Consulta la documentazione ufficiale NETUSEADD di: Netapi32.dll."
        End Select

    End Function


    Public Shared logFile As String = Nothing

    Public Shared Sub RunBackup(task As Task)
        Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
        logFile = GetLogName(task.Guid, timestamp)
        Dim destDir As String = (task.Destination)


        EseguiComandoNetUseDelete()

        Try

            Dim uncPathsource As String = If(Unc.IsUncPath(task.Source), task.Source, Nothing)
            Dim uncPathdest As String = If(Unc.IsUncPath(task.Destination), task.Destination, Nothing)

            Dim unc__1 As New Unc(uncPathsource)
            Dim c As New Credential
            c.Username = Credential.Decrypt(task.Guid, task.originuser)
            c.Password = Credential.Decrypt(task.Guid, task.originpass)
            If uncPathsource IsNot Nothing Then
                Logger.Open(logFile)
                Dim a As Integer = unc__1.Connect(c)
                If a = 0 Then
                    Logger.Log("[CONNECTION] connection to share: " & uncPathsource & ". OK")
                Else
                    Logger.Log(checkerror(uncPathsource, a))
                End If
                Logger.close()
            End If

            Dim unc__2 As New Unc(uncPathdest)
            Dim c2 As New Credential
            c2.Username = Credential.Decrypt(task.Guid, task.destuser)
            c2.Password = Credential.Decrypt(task.Guid, task.destpass)
            If uncPathdest IsNot Nothing Then
                Logger.Open(logFile)
                Dim a As Integer = unc__2.Connect(c2)
                If a = 0 Then
                    Logger.Log("[CONNECTION] Connection to share: " & uncPathdest & ". OK")
                Else
                    Logger.Log(checkerror(uncPathdest, a))
                End If
                Logger.close()
            End If

            RunRobocopy(task, logFile)
            unc__1.Dispose()
            unc__2.Dispose()
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
            args.Add(String.Format("/E /CREATE /DST /COPYALL /XF * /XJ /R:1 /W:1 ""/UNILOG+:{0}""", logFile))
            Logger.Open(logFile)
            Logger.Log("***************************************")
            Logger.Log("[BACKUP] Solo copia alberatura cartelle (con relativi permessi NTFS)")
            Logger.Log2("Se alberatura non viene copiata perche una delle 2 fonti (origine o destinazione)")
            Logger.Log2("non hanno filesystem NTFS!")
            Logger.Log("***************************************")
            Logger.close()
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

        Try
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

            Logger.Log("[TASK] Starting to analyze folders")

            Dim myfiles As String() = IO.Directory.GetFiles(d, "*.*", IO.SearchOption.AllDirectories)

            '''Dim yourfiles As String() = IO.Directory.GetFiles(o, "*.*", IO.SearchOption.AllDirectories)


            For Each file As String In myfiles

                origine = file.Replace(d, o)

                Dim solofolder As String = epura(Mid(file, 1, file.LastIndexOf("\")))
                If oldfolder <> solofolder Then
                    oldfolder = solofolder
                    Logger.Log2("")
                    Logger.Log("[TASK] analyzing folder: " & oldfolder)
                End If

                Dim origince As Boolean = False

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

        Catch ex As Exception
            Logger.Log("[TASK] ERROR Starting to analyze folders: caused by: " & ex.Message)
        End Try

        Try
            DeleteEmptyFolder(d)
        Catch ex As Exception
            Logger.Log("[TASK] ERROR on analyzing files..." & ex.Message)
        End Try

    End Sub

    Private Shared Sub DeleteEmptyFolder(ByVal sDirectoryPath As String)

        ' Ottieni tutte le sottodirectory della directory corrente
        Dim sottodirectory As String() = Directory.GetDirectories(sDirectoryPath)


        For Each subDir As String In sottodirectory

            ' Richiama ricorsivamente la funzione per scansionare eventuali sottodirectory
            DeleteEmptyFolder(subDir)



            ' Controlla se la directory è vuota
            If Directory.GetFiles(subDir).Length = 0 AndAlso Directory.GetDirectories(subDir).Length = 0 Then

                ' Elimina la directory se è vuota

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
