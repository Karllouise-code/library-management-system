Imports System.Data.OleDb
Public Class frmBorrow

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

    'LOADER
    Private Sub frmBorrow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadTables()
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

    'BUTTON BURROW
    Private Sub btnBurrow_Click(sender As Object, e As EventArgs) Handles btnBurrow.Click
        If con.State = ConnectionState.Closed Then
            OpenCon()
        End If

        If txtSearch.Text = "" Then
            MessageBox.Show("Please input a Book ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Try
                ''CHECK IF BOOKID EXIST ON DATABASE
                Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblBooks WHERE [Book_ID] = @Book_ID", con)
                    cmd.Parameters.AddWithValue("@Book_ID", OleDbType.VarChar).Value = txtSearch.Text.Trim
                    Dim count = Convert.ToInt32(cmd.ExecuteScalar())
                    If count > 0 Then
                        query.Connection = con
                        query.CommandText = "DELETE * FROM tblBooks WHERE [Book_ID] = " & txtSearch.Text & ""
                        query.ExecuteNonQuery()
                        MessageBox.Show("Book Borrowed Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtSearch.Clear()
                        con.Close()
                        Call LoadTables()
                        Exit Sub
                    Else
                        MessageBox.Show("Book ID is not Registered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
            con.Close()
            Call LoadTables()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        frmDashboard.Show()
    End Sub
End Class