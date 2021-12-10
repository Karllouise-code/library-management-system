Imports System.Data.OleDb
Public Class frmReturn

    'LOADER FUNCTION
    Private Sub LoadTables(Optional ByVal q As String = "")
        Try
            If con.State = ConnectionState.Closed Then
                OpenCon()
            End If
            query.Connection = con
            query.CommandText = "SELECT * FROM tblBooks"
            adapter.SelectCommand = query
            dt.Clear()
            adapter.Fill(dt)
            DataGridView1.DataSource = dt
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con.Close()
    End Sub

    'BUTTON CLEAR
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call LoadTables()
    End Sub

    'BUTTON RETURN
    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If

        If txtTitle.Text = "" Or txtAuthor.Text = "" Or txtDescrip.Text = "" Or txtCategory.Text = "" Or txtType.Text = "" Or txtDate.Text = "" Then
            MessageBox.Show("Please input empty fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf txtSearch.Text <> Nothing Then
            MessageBox.Show("Search Field has Value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Try
                query.Connection = con
                query.CommandText = "INSERT INTO tblBooks([Title], [Author], [Description], [Category], [Type], [Date_Published]) VALUES('" & txtTitle.Text & "', '" & txtAuthor.Text & "', '" & txtDescrip.Text & "', '" & txtCategory.Text & "', '" & txtType.Text & "', '" & txtDate.Text & "')"
                query.ExecuteNonQuery()
                MessageBox.Show("Book Returned Succesfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtTitle.Clear()
                txtAuthor.Clear()
                txtDescrip.Clear()
                txtCategory.Clear()
                txtType.Clear()
                txtDate.Clear()
                con.Close()
                Call LoadTables()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
            con.Close()
            Call LoadTables()
        End If
    End Sub

    'BUTTON BACK
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmDashboard.Show()
    End Sub
End Class