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



        Dim readlines() As String = My.Resources.it.Split(Chr(10))

        _translations = readlines.[Select](Function(line) line.Trim()).Where(Function(line) Not String.IsNullOrEmpty(line)).[Select](Function(line) line.Split(New Char() {"="c}, 2)).ToDictionary(Function(line) line(0), Function(line) line(1))
    End Sub

End Class
