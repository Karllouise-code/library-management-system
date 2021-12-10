Imports System.Data.OleDb

Public Class frmLibrary
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

    'SEARCH BOOK ID FUNCTION
    Private Sub RefreshTables(Optional ByVal q As String = "")
        Try
            OpenCon()
            query.Connection = con
            query.CommandText = "SELECT * FROM tblBooks WHERE [Book_ID] = " & txtSearch.Text & ""
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

    'SEARCH AUTHOR FUNCTION
    Private Sub searchAuthor(Optional ByVal q As String = "")
        Try
            OpenCon()
            query.Connection = con
            query.CommandText = "SELECT * FROM tblBooks WHERE [Author] = '" & txtSearch.Text & "'"
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

    'BUTTON SEARCH
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If txtSearch.Text = "" Then
                MessageBox.Show("Please input value on Search Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf Integer.TryParse(txtSearch.Text, vbNull) Then
                Call RefreshTables()
            Else
                Call searchAuthor()
            End If
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con.Close()
    End Sub

    'BUTTON CLEAR
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call LoadTables()
        txtSearch.Clear()
    End Sub

    'BUTTON BACK
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmDashboard.Show()
    End Sub

End Class