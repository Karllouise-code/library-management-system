Imports System.Data.OleDb
Public Class frmRegister

    'LOADER
    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblFullname.Hide()
        lblUsername.Hide()
        lblPassword.Hide()
        lblConfirmPass.Hide()
        lblContactNumber.Hide()
        lblPasswordUser.Hide()
        txtFullname.Hide()
        txtUsername.Hide()
        txtPassword.Hide()
        txtConfirmPassword.Hide()
        txtContactNumber.Hide()
        txtPasswordUser.Hide()
        lblDateTime.Text = Today

    End Sub

    'BUTTON BACK
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmStart.Show()
    End Sub

    'BUTTON LOGIN INSTEAD
    Private Sub btnLoginIns_Click(sender As Object, e As EventArgs) Handles btnLoginIns.Click
        Me.Hide()
        frmLogin.Show()
    End Sub

    'BUTTON REGISTER
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If
        If txtFullname.Text = "" Or txtUsername.Text = "" Then
            MessageBox.Show("Please Enter Empty Fields")
        ElseIf ComboBox1.Text = "Employee" Then
            'Validation on database if the account is already registerd
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblEmployees WHERE [Username] = @Username", con)
                cmd.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = txtUsername.Text.Trim
                Dim count = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    MessageBox.Show("Whoops! Username has already taken", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End Using
            'If Username is available
            Using create As New OleDbCommand("INSERT INTO tblEmployees([Fullname], [Username], [Password], [Date_Created]) VALUES(@Fullname, @Username, @Password, @Date_Created)", con)
                If txtPassword.Text = txtConfirmPassword.Text Then
                    create.Parameters.AddWithValue("@Fullname", OleDbType.VarChar).Value = txtFullname.Text.Trim
                    create.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = txtUsername.Text.Trim
                    create.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = txtPassword.Text.Trim
                    create.Parameters.AddWithValue("@Date_Created", OleDbType.VarChar).Value = lblDateTime.Text.Trim

                    If create.ExecuteNonQuery Then
                        MessageBox.Show("Account Created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtFullname.Clear()
                        txtUsername.Clear()
                        txtPassword.Clear()
                        txtConfirmPassword.Clear()
                    End If
                Else
                    MessageBox.Show("Password Mismatch!")
                End If
            End Using

        Else
            'Validation on database if the account is already registerd
            Using cmdUser As New OleDbCommand("SELECT COUNT(*) FROM tblUsers WHERE [Username] = @Username", con)
                cmdUser.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = txtUsername.Text.Trim
                Dim count = Convert.ToInt32(cmdUser.ExecuteScalar())
                If count > 0 Then
                    MessageBox.Show("Whoops! Username has already taken", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtPasswordUser.Clear()
                    txtContactNumber.Clear()
                    Exit Sub
                End If
            End Using
            'If Username is available
            Using createUser As New OleDbCommand("INSERT INTO tblUsers([Fullname], [Username], [Contact_Number], [Password], [Date_Created] , [Verified]) VALUES(@Fullname, @Username, @Contact_Number, @Password, @Date_Created, @Verified)", con)

                createUser.Parameters.AddWithValue("@Fullname", OleDbType.VarChar).Value = txtFullname.Text.Trim
                createUser.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = txtUsername.Text.Trim
                createUser.Parameters.AddWithValue("@Contact_Number", OleDbType.VarChar).Value = txtContactNumber.Text.Trim
                createUser.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = txtPasswordUser.Text.Trim
                createUser.Parameters.AddWithValue("@Date_Created", OleDbType.VarChar).Value = lblDateTime.Text.Trim
                createUser.Parameters.AddWithValue("@Verified", OleDbType.VarChar).Value = "UNVERIFIED"

                If createUser.ExecuteNonQuery Then
                    MessageBox.Show("Account Created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtFullname.Clear()
                    txtUsername.Clear()
                    txtContactNumber.Clear()
                    txtPasswordUser.Clear()
                End If

            End Using
            con.Close()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "User" Then
            lblPassword.Hide()
            lblConfirmPass.Hide()
            txtConfirmPassword.Hide()
            txtPassword.Hide()

            txtFullname.Show()
            txtUsername.Show()
            lblFullname.Show()
            lblUsername.Show()
            lblContactNumber.Show()
            lblPasswordUser.Show()
            txtContactNumber.Show()
            txtPasswordUser.Show()

        ElseIf ComboBox1.Text = "Employee" Then
            lblPasswordUser.Hide()
            lblContactNumber.Hide()
            txtPasswordUser.Hide()
            txtContactNumber.Hide()

            lblFullname.Show()
            lblUsername.Show()
            lblPassword.Show()
            lblConfirmPass.Show()
            txtPassword.Show()
            txtConfirmPassword.Show()

            txtFullname.Show()
            txtUsername.Show()
            txtPassword.Show()
            txtConfirmPassword.Show()
        End If
    End Sub
End Class