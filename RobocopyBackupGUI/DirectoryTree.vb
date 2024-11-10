Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms


Public NotInheritable Class DirectoryTree
	Private Sub New()
	End Sub



	Public Shared Function GetExcludedNodes(nodes As TreeNodeCollection) As List(Of TreeNode)
		Dim list As New List(Of TreeNode)()
		For Each child As TreeNode In nodes
			If Not child.Checked Then
				list.Add(child)
			Else
				list.AddRange(GetExcludedNodes(child.Nodes))
			End If
		Next
		Return list
	End Function

	''' 
	Public Shared tf As Long = 0
	Public Shared tc As Long = 0



End Class
