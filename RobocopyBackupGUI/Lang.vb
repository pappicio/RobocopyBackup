Imports System.Collections.Generic

Imports System.Linq

Public NotInheritable Class Lang
    Private Sub New()
    End Sub

    Private Shared _availableLocales As Dictionary(Of String, String)

    Private Shared _translations As New Dictionary(Of String, String)()


    Public Shared Function [Get](key As String, Optional append As String = Nothing) As String
        If _translations.ContainsKey(key) Then
            Dim translation As String = _translations(key)
            If append IsNot Nothing Then
                Return String.Format("{0}{1}", translation, append)
            End If
            Return translation
        End If
        Return key
    End Function


    Public Shared Sub SetLang()

        Dim s As String = "
About=About
AtTime=alle ore
Author=Autore
BackupStarted=La richiesta di esecuzione immediata del backup è stata inviata al servizio.
Daily=ogni giorno
DailyAbbr=ogni g.
DayOfMonth=giorno del mese
DayOfWeek=giorno della settimana
DeleteConfirmation=Vuoi eliminare l'attività selezionata?
DeleteTask=Elimina attività
Destination=Destinazione
Differential=Differenziale
DifferentialAbbr=Diff
Directories=Directory
EditTask=Modifica attività
Friday=Venerdì
Full=Full
FullAbbr=Full
Help=Aiuto
HelpDifferential=Differenziale = Sottodirectory con timestamp nella directory di destinazione per ogni esecuzione. I file vengono collegati in modo permanente dalla sottodirectory del backup precedente (se presente) e quindi le differenze vengono sincronizzate dalla directory di origine.
HelpFull=Completo = Sottodirectory con timestamp nella directory di destinazione per ogni esecuzione. I file vengono sempre copiati completamente dalla directory di origine.
HelpIncremental=Incrementale = Directory di destinazione unica. I file nuovi e modificati nella directory di origine vengono copiati nella directory di destinazione. I file eliminati dalla directory di origine vengono conservati (non eliminati) nella directory di destinazione.
HelpRetention=La conservazione è impostata come numero di sottodirectory con timestamp. Quando viene raggiunto il limite, la directory più vecchia verrà eliminata. Con la conservazione impostata su 1, i metodi differenziale e completo non creano directory con timestamp e sincronizzano solo il contenuto della directory di origine con la directory di destinazione.
IncompleteTaskForm=Per favore inserisci titolo, origine e destinazione.
Incremental=Incrementale
IncrementalAbbr=Inc
Information=Informazione
Language=Linguaggio
LogRetention=Conservazione dei Log (giorni)
Method=Metodo
Monday=Lunedi
MonthlyAbbr=Mensilmente
NetworkCredentials=Credenziali Local Administrator (per accedere alle condivise)
NewTask=Nuova attività
NoTaskLogs=Questa attività non ha ancora log.
Password=Password
Question=Domanda
ReenterCredentials=Reinserire le credenziali prima di salvare
Retention=Conservazione file eliminati per giorni
RunTask=Esegui attività ora
Saturday=Sabato
SaveSettings=Salva impostazioni
SaveTask=Salva attività
Schedule=Schedulazione
Settings=Impostazioni
ShowTaskLogs=mostra logs attivià
Source=Sorgente
SourceCode=codice sorgente
Sunday=Domenica
Task=Attività
Thursday=Giovedì
Title=Nome
Tuesday=Martedì
UnableToConnectService=Impossibile connettersi al servizio.
Username=Username
Version=Versione
Wednesday=Mercoledì
WeeklyAbbr=Settimanalmente
"


        Dim readlines() As String = s.Split(Chr(10))

        _translations = readlines.[Select](Function(line) line.Trim()).Where(Function(line) Not String.IsNullOrEmpty(line)).[Select](Function(line) line.Split(New Char() {"="c}, 2)).ToDictionary(Function(line) line(0), Function(line) line(1))
    End Sub

End Class
