Imports System.Data.OleDb
Public Class frmBurrowLogs

    'LOADER FUNCTION
    Private Sub LoadTables(Optional ByVal q As String = "")
        Try
            If con.State = ConnectionState.Closed Then
                OpenCon()
            End If
            query.Connection = con
            query.CommandText = "SELECT * FROM tblBurrowLog"
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

    'DATA GRID VIEW SELECTOR
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim row As DataGridViewRow = DataGridView1.CurrentRow
        txtSearch.Text = row.Cells(0).Value.ToString()
        txtTitle.Text = row.Cells(1).Value.ToString()
        txtAuthor.Text = row.Cells(2).Value.ToString()
        txtDescrip.Text = row.Cells(3).Value.ToString()
        txtCategory.Text = row.Cells(4).Value.ToString()
        txtType.Text = row.Cells(5).Value.ToString()
        txtDate.Text = row.Cells(6).Value.ToString()
        txtBurrowername.Text = row.Cells(7).Value.ToString()
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

    'BUTTON RETURNED
    Private Sub btnReturned_Click(sender As Object, e As EventArgs) Handles btnReturned.Click
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If

        If txtTitle.Text = "" Or txtAuthor.Text = "" Or txtDescrip.Text = "" Or txtCategory.Text = "" Or txtType.Text = "" Or txtDate.Text = "" Or txtBurrowername.Text = "" Then
            MessageBox.Show("Please input empty fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf txtSearch.Text <> Nothing Then
            MessageBox.Show("Search Field has Value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Try
                query.Connection = con
                query.CommandText = "INSERT INTO tblBooks([Title], [Author], [Description], [Category], [Type], [Date_Published]) VALUES('" & txtTitle.Text & "', '" &
                txtAuthor.Text & "', '" & txtDescrip.Text & "', '" & txtCategory.Text & "', '" & txtType.Text & "', '" & txtDate.Text & "')"
                query.ExecuteNonQuery()
                MessageBox.Show("Successfully Returned!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtTitle.Clear()
                txtAuthor.Clear()
                txtDescrip.Clear()
                txtCategory.Clear()
                txtType.Clear()
                txtDate.Clear()
                txtBurrowername.Clear()
                con.Close()
                Call LoadTables()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
            con.Close()
            Call LoadTables()
        End If
    End Sub

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

    'BUTTON CLEAR AND LOAD TABLES
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Call LoadTables()
        txtSearch.Clear()
    End Sub

    'BUTTON BACK
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmEmployee.Show()
    End Sub
End Class