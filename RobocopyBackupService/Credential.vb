Imports System.IO
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text

<Serializable> _
Public Class Credential

	Public Property Username() As String
		Get
			Return m_Username
		End Get
		Set
			m_Username = Value
		End Set
	End Property
	Private m_Username As String

	Public Property Password() As String
		Get
			Return m_Password
		End Get
		Set
			m_Password = Value
		End Set
	End Property
	Private m_Password As String


	Shared Function Decrypt(guid As String, encrypted As String) As String


		Dim EncryptionKey As String = guid
		Dim cipherBytes As Byte() = Convert.FromBase64String(encrypted)
		Using encryptor As Aes = Aes.Create()
			Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
			 &H65, &H64, &H76, &H65, &H64, &H65,
			 &H76})
			encryptor.Key = pdb.GetBytes(32)
			encryptor.IV = pdb.GetBytes(16)
			Using ms As New MemoryStream()
				Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
					cs.Write(cipherBytes, 0, cipherBytes.Length)
					cs.Close()
				End Using

				encrypted = Encoding.Unicode.GetString(ms.ToArray())



			End Using
		End Using

		Return encrypted
	End Function
	Shared Function Encrypt(guid As String, decrypted As String) As String

		If decrypted.Trim = "" Then
			Return ""
		End If

		Dim EncryptionKey As String = guid
		Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(decrypted)
		Using encryptor As Aes = Aes.Create()
			Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
		 &H65, &H64, &H76, &H65, &H64, &H65,
		 &H76})
			encryptor.Key = pdb.GetBytes(32)
			encryptor.IV = pdb.GetBytes(16)
			Using ms As New MemoryStream()
				Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
					cs.Write(clearBytes, 0, clearBytes.Length)
					cs.Close()
				End Using

				decrypted = Convert.ToBase64String(ms.ToArray())


			End Using
		End Using

		Return decrypted
	End Function



End Class
