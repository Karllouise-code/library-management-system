Imports System.Data.OleDb
Public Class frmEmployee

    'FULLNAME OF EMPLOYEE FUNCTION
    Private Sub employeeName(Optional ByVal q As String = "")
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If
        Try
            Dim strsql As String
            strsql = "SELECT * FROM tblEmployees WHERE Username = '" & frmLogin.txtUsername.Text & "'"
            Dim cmd As New OleDbCommand(strsql, con)
            Dim myReader As OleDbDataReader
            myReader = cmd.ExecuteReader
            myReader.Read()
            lblFullname.Text = myReader("Fullname")
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con.Close()
    End Sub

    'LOADER
    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call employeeName()
    End Sub

    'BUTTON MANAGE ACCOUNTS
    Private Sub btnManageAccounts_Click(sender As Object, e As EventArgs) Handles btnManageAccounts.Click
        Me.Hide()
        frmAccounts.Show()
    End Sub

    'BUTTON LOGOUT
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Close()
        frmLogin.Show()
    End Sub

    'BUTTON BOOKS
    Private Sub btnBooks_Click(sender As Object, e As EventArgs) Handles btnBooks.Click
        Me.Hide()
        frmBooks.Show()
    End Sub
End Class