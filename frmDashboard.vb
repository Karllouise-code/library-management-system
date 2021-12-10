Imports System.Data.OleDb
Public Class frmDashboard

    'FULLNAME OF USER FUNCTION
    Private Sub userName(Optional ByVal q As String = "")
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If
        Try
            Dim strsql As String
            strsql = "SELECT * FROM tblUsers WHERE [Username] = '" & frmLogin.txtUsername.Text & "'"
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
    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call userName()
    End Sub

    'BUTTON LOGOUT
    Private Sub btnLogout_Click(sender As Object, e As EventArgs)
        Me.Close()
        frmLogin.Show()
    End Sub

    'BUTTON LIBRARY
    Private Sub btnLibrary_Click(sender As Object, e As EventArgs) Handles btnLibrary.Click
        Me.Hide()
        frmLibrary.Show()
    End Sub

    'BUTTON BORROW
    Private Sub btnBorrow_Click(sender As Object, e As EventArgs) Handles btnBorrow.Click
        Me.Hide()
        frmBorrow.Show()
    End Sub

    'BUTTON RETURN
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Me.Hide()
        frmReturn.Show()
    End Sub

    'BUTTON LOGOUT
    Private Sub btnLogout_Click_1(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Close()
        frmLogin.Show()
    End Sub
End Class