Imports MySql.Data.MySqlClient
Public Class Form3
    Private Sub signUp_Click(sender As Object, e As EventArgs) Handles signUp.Click

        MessageBox.Show("Anda akan diarahkan ke halaman Sign up", "Informasi")
        Form1.Show()
        Me.Hide()

    End Sub

    Private Sub login_Click(sender As Object, e As EventArgs) Handles login.Click

        Dim email As String = TextBox2.Text
        Dim password As String = TextBox3.Text
        Dim minKarakter As Integer = 8
        BukaKoneksi()
        Try
            Dim cmd As New MySqlCommand("SELECT * FROM user WHERE email=@email AND password=@pass", conn)
            cmd.Parameters.AddWithValue("@email", TextBox2.Text)
            cmd.Parameters.AddWithValue("@pass", TextBox3.Text)

            Dim rd As MySqlDataReader = cmd.ExecuteReader()
            If rd.HasRows Then
                rd.Read()
                MessageBox.Show("Selamat datang, " & rd("full_name").ToString() & "!", "Login Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Hide()
                Form2.Show()
            Else
                MessageBox.Show("Email atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Kesalahan: " & ex.Message)
        Finally
            conn.Close()
        End Try


        If String.IsNullOrWhiteSpace(email) Or String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Jangan kemana mana dulu, isi dulu ini  sebelum ke halaman utama", "Peringatan")
            Return
        End If

        If password.Length < minKarakter Then
            MessageBox.Show("Password harus diisi minimal 8 karakter", "Peringatan")
            Return
        End If

        MessageBox.Show("Login berhasil, selamat datang di halaman utama", "Informasi")
        Form2.Show()
        Me.Hide()

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox3.PasswordChar = "*"c
        TextBox2.Font = New Font(TextBox2.Font.FontFamily, 14, TextBox2.Font.Style)
        TextBox3.Font = New Font(TextBox3.Font.FontFamily, 14, TextBox3.Font.Style)
    End Sub
End Class