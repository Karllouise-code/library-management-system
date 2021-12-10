Imports System.Data.OleDb
Module connection
    Public con As New OleDbConnection
    Public query As New OleDbCommand
    Public adapter As New OleDb.OleDbDataAdapter(query)
    Public dt As New DataTable
    Public myReader As OleDbDataReader

    Sub OpenCon()
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\karll\source\repos\LibraryManagementSystem\bin\Debug\libraryManagementdb.mdb;"
        con.Open()
    End Sub
End Module
