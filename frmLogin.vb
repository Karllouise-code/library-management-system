Imports System.Data.OleDb
Public Class frmLogin

    'BUTTON BACK
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmStart.Show()
    End Sub

    'BUTTON REGISTER INSTEAD
    Private Sub btnRegisterIns_Click(sender As Object, e As EventArgs) Handles btnRegisterIns.Click
        Me.Hide()
        frmRegister.Show()
    End Sub

    'VERIFY LOADER
    Private Sub verifyLabel(Optional ByVal q As String = "")
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If
        Try
            Dim strsql As String
            strsql = "SELECT * FROM tblUsers WHERE Username = '" & txtUsername.Text & "'"
            Dim cmd As New OleDbCommand(strsql, con)
            Dim myReader As OleDbDataReader
            myReader = cmd.ExecuteReader
            myReader.Read()
            lblVerified.Text = myReader("Verified")

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    'BUTTON LOGIN
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If

        If txtUsername.Text = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Please Enter Empty Fields")
        ElseIf ComboBox1.Text = "Employee" Then

            'CHECKS ON DATABASE IF USERNAME EXIST
            Using login As New OleDbCommand("SELECT COUNT(*) FROM tblEmployees WHERE [Username] = @Username AND [Password] = @Password", con)
                login.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = txtUsername.Text.Trim
                login.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = txtPassword.Text.Trim
                Dim loginCount = Convert.ToInt32(login.ExecuteScalar())
                If loginCount > 0 Then
                    Me.Hide()
                    MessageBox.Show("Login Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    frmEmployee.Show()
                    txtUsername.Clear()
                    txtPassword.Clear()
                    Exit Sub
                Else
                    MessageBox.Show("Invalid Credentials :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtUsername.Clear()
                    txtPassword.Clear()
                End If
            End Using
            con.Close()
        Else
            'CHECKS ON DATABASE IF USERNAME EXIST
            Using login As New OleDbCommand("SELECT COUNT(*) FROM tblUsers WHERE [Username] = @Username AND [Password] = @Password", con)
                login.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = txtUsername.Text.Trim
                login.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = txtPassword.Text.Trim

                Dim loginCount = Convert.ToInt32(login.ExecuteScalar())
                If loginCount > 0 Then
                    Call verifyLabel()
                    If lblVerified.Text = "UNVERIFIED" Then
                        MessageBox.Show("Unverified Account Contact Admin:(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        Me.Hide()
                        MessageBox.Show("Login Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        frmDashboard.Show()
                        txtUsername.Clear()
                        txtPassword.Clear()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Invalid Credentials :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtUsername.Clear()
                    txtPassword.Clear()
                End If
            End Using
            con.Close()
        End If

    End Sub


End Class