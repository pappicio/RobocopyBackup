Imports System.Diagnostics
Imports System.Reflection
Imports System.Windows.Forms

Public Partial Class AboutForm
	Inherits Form

	Public Sub New()
		InitializeComponent()
		Localize()
		'''versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString(2)
	End Sub

	''' <summary>
	''' Translates the form strings to the language set by <see cref="Config.Language"/>
	''' </summary>
	Private Sub Localize()
		Text = Lang.[Get]("About")
		'''aboutVersionLabel.Text = Lang.[Get]("Version", ":")
		'aboutAuthorLabel.Text = Lang.[Get]("Author", ":")
		'aboutSourceCodeLabel.Text = Lang.[Get]("SourceCode", ":")
	End Sub

	''' <summary>
	''' <see cref="sourceCodeLinkLabel.Click"/> event handler. Opens a GitHub link in default web browser.
	''' </summary>
	Private Sub SourceCodeLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
		'Process.Start(sourceCodeLinkLabel.Text)
	End Sub

	Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
		'''Process.Start(LinkLabel1.Text)
	End Sub

	Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim Version As Version = Assembly.GetExecutingAssembly().GetName().Version
		Label3.Text = ("Versione: " + Version.Major.ToString + "." + Version.Minor.ToString)
		Label1.Focus()
		TextBox1.Select(0, 0)
	End Sub

	Private Sub Label2_Click(sender As Object, e As EventArgs)

	End Sub
End Class
