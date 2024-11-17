Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace RobocopyBackup
    Public Class OpenFolderDialog
        Inherits Component

        Public Property SelectedPath As String

        Public Function ShowDialog() As DialogResult
            Return ShowDialog(IntPtr.Zero)
        End Function

        Public Function ShowDialog(ByVal hwndOwner As IntPtr) As DialogResult
            Dim dialog As IFileOpenDialog = CType(New FileOpenDialog(), IFileOpenDialog)
            Dim idl As IntPtr = Nothing, path As String = Nothing

            Try
                Dim item As IShellItem = Nothing

                If Not String.IsNullOrEmpty(SelectedPath) Then
                    Dim atts As UInteger = 0

                    If SHILCreateFromPath(SelectedPath, idl, atts) = 0 Then

                        If SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, item) = 0 Then
                            dialog.SetFolder(item)
                        End If

                        Marshal.FreeCoTaskMem(idl)
                    End If
                End If


                dialog.SetOptions(&H20 Or &H40)
                Dim hresult As UInteger = dialog.Show(hwndOwner)
                If hresult = &H800704C7 Then Return DialogResult.Cancel
                If hresult <> 0 Then Return DialogResult.Abort
                dialog.GetResult(item)
                item.GetDisplayName(2147844096, path)

                SelectedPath = path
                Return DialogResult.OK
            Finally
                Marshal.ReleaseComObject(dialog)
            End Try
        End Function


        ' SHILCreateFromPath
        <DllImport("shell32.dll", CharSet:=CharSet.Unicode, PreserveSig:=True)>
        Public Shared Function SHILCreateFromPath(
        ByVal pszPath As String,
        <Out> ByRef ppIdl As IntPtr,
        ByRef rgflnOut As UInteger) As Integer
        End Function
        Private Declare Function SHCreateShellItem Lib "shell32.dll" (ByVal pidlParent As IntPtr, ByVal psfParent As IntPtr, ByVal pidl As IntPtr, <Out> ByRef ppsi As IShellItem) As Integer


        <ComImport, Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7")>
        Private Class FileOpenDialog
        End Class

        <ComImport, Guid("42F85136-DB7E-439C-85F1-E4075D135FC8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
        Private Interface IFileOpenDialog
            <PreserveSig>
            Function Show(
            <[In]> ByVal parent As IntPtr) As UInteger
            Sub SetFileTypes()
            Sub SetFileTypeIndex(
<[In]> ByVal iFileType As UInteger)
            Sub GetFileTypeIndex(<Out> ByRef piFileType As UInteger)
            Sub Advise()
            Sub Unadvise()
            Sub SetOptions(
<[In]> ByVal fos As Integer)
            Sub GetOptions(<Out> ByRef pfos As Integer)
            Sub SetDefaultFolder(ByVal psi As IShellItem)
            Sub SetFolder(ByVal psi As IShellItem)
            Sub GetFolder(<Out> ByRef ppsi As IShellItem)
            Sub GetCurrentSelection(<Out> ByRef ppsi As IShellItem)
            Sub SetFileName(
<[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String)
            Sub GetFileName(<Out>
<MarshalAs(UnmanagedType.LPWStr)> ByRef pszName As String)
            Sub SetTitle(
<[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszTitle As String)
            Sub SetOkButtonLabel(
<[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszText As String)
            Sub SetFileNameLabel(
<[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszLabel As String)
            Sub GetResult(<Out> ByRef ppsi As IShellItem)
            Sub AddPlace(ByVal psi As IShellItem, ByVal alignment As Integer)
            Sub SetDefaultExtension(
<[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pszDefaultExtension As String)
            Sub Close(ByVal hr As Integer)
            Sub SetClientGuid()
            Sub ClearClientData()
            Sub SetFilter(
<MarshalAs(UnmanagedType.[Interface])> ByVal pFilter As IntPtr)
            Sub GetResults(<Out>
<MarshalAs(UnmanagedType.[Interface])> ByRef ppenum As IntPtr)
            Sub GetSelectedItems(<Out>
<MarshalAs(UnmanagedType.[Interface])> ByRef ppsai As IntPtr)
        End Interface

        <ComImport, Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
        Private Interface IShellItem
            Sub BindToHandler()
            Sub GetParent()
            Sub GetDisplayName(
<[In]> ByVal sigdnName As UInteger, <Out>
<MarshalAs(UnmanagedType.LPWStr)> ByRef ppszName As String)
            Sub GetAttributes()
            Sub Compare()
        End Interface
    End Class
End Namespace