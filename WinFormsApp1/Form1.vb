Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Login_Click(sender As Object, e As EventArgs) Handles Login.Click
        Dim username As String = TextBox1.Text
        Dim email As String = TextBox2.Text
        Dim password As String = TextBox3.Text
        Dim minKarakter As Integer = 8
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MessageBox.Show("Semua kolom wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        BukaKoneksi()
        Try
            Dim cmd As New MySqlCommand("Insert into `user`(`full_name`, `email`, `password`) VALUES (@nama,@email,@pass)", conn)
            cmd.Parameters.AddWithValue("@nama", TextBox1.Text)
            cmd.Parameters.AddWithValue("@email", TextBox2.Text)
            cmd.Parameters.AddWithValue("@pass", TextBox3.Text)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Akun berhasil dibuat!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            conn.Close()
            Me.Hide()
            Form3.Show()
        Catch ex As MySqlException
            If ex.Number = 1062 Then
                MessageBox.Show("Email sudah terdaftar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Terjadi kesalahan: " & ex.Message)
            End If
        Finally
            conn.Close()
        End Try
    End Sub


    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
