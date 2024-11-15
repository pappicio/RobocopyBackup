Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Namespace RobocopyBackup
    Public Class Unc
        Implements IDisposable

        Private _disposed As Boolean = False
        Private disposedValue As Boolean
        Private ReadOnly _path As String

        Public Sub New(ByVal path As String)
            _path = path
        End Sub



        Public Shared Function IsUncPath(ByVal path As String) As Boolean
            Return path.StartsWith("\\")
        End Function

        Public Shared Function TranslatePath(ByVal path As String) As String
            If IsUncPath(path) Then
                Return path
            End If

            Dim path_split As String() = path.Split(New Char() {"\"c}, 2)
            Dim stringBuilder As StringBuilder = New StringBuilder(512)
            Dim size As Integer = stringBuilder.Capacity
            Dim errorx As Integer = WNetGetConnectionW(path_split(0), stringBuilder, size)
            If errorx <> 0 Then Return path
            Return IO.Path.Combine(stringBuilder.ToString(), path_split(1))
        End Function

        Public Function Connect(ByVal credential As Credential) As Integer
            If credential Is Nothing Then
                _disposed = True
                Return -1
            End If

            Dim path_split As String() = _path.Split(New Char() {"\"c})
            Dim domain As String = If(path_split.Length > 2, path_split(2), Nothing)
            Dim useinfo As UseInfo2 = New UseInfo2 With {
                .ui2_remote = _path,
                .ui2_domainname = domain,
                .ui2_username = credential.Username,
                .ui2_password = credential.Password,
                .ui2_asg_type = 0,
                .ui2_usecount = 1
            }
            Dim parmErr As UInteger = Nothing
            Dim a As Integer = Convert.ToInt32(NetUseAdd(Nothing, 2, useinfo, parmErr))
            Return a

        End Function




        Private Declare Function NetUseAdd Lib "Netapi32.dll" (ByVal uncServerName As String, ByVal levelFlags As UInteger, ByRef buf As UseInfo2, <Out> ByRef parmErr As UInteger) As UInteger

        Private Declare Function NetUseDel Lib "Netapi32.dll" (ByVal uncServerName As String, ByVal useName As String, ByVal forceLevelFlags As UInteger) As UInteger

        Private Declare Function WNetGetConnectionW Lib "mpr.dll" (ByVal lpLocalName As String, ByVal lpRemoteName As StringBuilder, ByRef lpnLength As Integer) As Integer

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Private Structure UseInfo2
            Friend ui2_local As String
            Friend ui2_remote As String
            Friend ui2_password As String
            Friend ui2_status As UInteger
            Friend ui2_asg_type As UInteger
            Friend ui2_refcount As UInteger
            Friend ui2_usecount As UInteger
            Friend ui2_username As String
            Friend ui2_domainname As String
        End Structure

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: eliminare lo stato gestito (oggetti gestiti)
                End If

                ' TODO: liberare risorse non gestite (oggetti non gestiti) ed eseguire l'override del finalizzatore
                ' TODO: impostare campi di grandi dimensioni su Null
                disposedValue = True
            End If
        End Sub

        ' ' TODO: eseguire l'override del finalizzatore solo se 'Dispose(disposing As Boolean)' contiene codice per liberare risorse non gestite
        ' Protected Overrides Sub Finalize()
        '     ' Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(disposing As Boolean)'
        '     Dispose(disposing:=False)
        '     MyBase.Finalize()
        ' End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(disposing As Boolean)'
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
