Imports MySql.Data.MySqlClient

Module ModuleKoneksi

    Public conn As MySqlConnection

    Public Sub BukaKoneksi()
        Try
            Dim str As String = "server=localhost;user id=root;password=;database=vb_user_database"
            conn = New MySqlConnection(str)
            conn.Open()
        Catch ex As Exception
            MessageBox.Show("Koneksi gagal: " & ex.Message)
        End Try
    End Sub
End Module