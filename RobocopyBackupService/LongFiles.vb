Imports System.Runtime.InteropServices

Public Class longfile

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
    Public Shared Function FileExists(ByVal filePath As String) As Boolean
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


End Class
