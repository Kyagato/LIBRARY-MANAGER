Imports MySql.Data.MySqlClient


Public Class Form5
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        ' Variabel untuk mode edit
        Public isEditMode As Boolean = False
        Public editNikNis As String = ""

        Sub BersihkanForm()
            TextBox1.Clear() ' NIM/NIK
            TextBox2.Clear() ' NAMA
            TextBox3.Clear() ' EMAIL
            TextBox4.Clear() ' TELEPON
            TextBox5.Clear() ' ALAMAT
            TextBox1.Focus()

            ' Reset mode
            isEditMode = False
            editNikNis = ""
        daftar.Text = "DAFTAR"
    End Sub

        Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Jika mode edit, tampilkan data
        If isEditMode Then
            daftar.Text = "UPDATE"
            TampilkanDataEdit()
        Else
            BersihkanForm()
        End If

    End Sub

    Sub TampilkanDataEdit()
        Try
            BukaKoneksi()
            cmd = New MySqlCommand("SELECT * FROM anggota WHERE nik_nis = @nik", conn)
            cmd.Parameters.AddWithValue("@nik", editNikNis)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                TextBox1.Text = dr("nik_nis").ToString()
                TextBox1.ReadOnly = True ' NIK tidak bisa diubah saat edit
                TextBox2.Text = dr("nama").ToString()
                TextBox3.Text = dr("email").ToString()
                TextBox4.Text = dr("telepon").ToString()
                TextBox5.Text = dr("alamat").ToString()
            End If

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If isEditMode Then
            ' Jika mode edit, kembali ke Form9 tanpa update
            Dim result As DialogResult = MessageBox.Show("Batalkan edit data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Dim form9 As New Form9()
                form9.Show()
                Me.Close()
            End If
        Else
            ' Jika mode tambah, bersihkan form
            BersihkanForm()
        End If

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim form As New Form2()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim form As New Form4()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Dim form As New Form6()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim form As New Form7()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim form As New Form8()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim form As New Form9
        form.Show()
        Close()
    End Sub

    Private Sub daftar_Click(sender As Object, e As EventArgs) Handles daftar.Click
        Dim nimNik As String = TextBox1.Text
        Dim namaLengkap As String = TextBox2.Text
        Dim alamat As String = TextBox5.Text
        Dim telepon As String = TextBox4.Text
        Dim email As String = TextBox3.Text


        ' Validasi input
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MessageBox.Show("Semua field harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            BukaKoneksi()

            If isEditMode Then
                ' MODE UPDATE
                cmd = New MySqlCommand("UPDATE anggota SET nama = @nama, email = @email, telepon = @telepon, alamat = @alamat WHERE nik_nis = @nik", conn)
                cmd.Parameters.AddWithValue("@nik", editNikNis)
                cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
                cmd.Parameters.AddWithValue("@email", TextBox3.Text)
                cmd.Parameters.AddWithValue("@telepon", TextBox4.Text)
                cmd.Parameters.AddWithValue("@alamat", TextBox5.Text)

                Dim affected As Integer = cmd.ExecuteNonQuery()

                If affected > 0 Then
                    MessageBox.Show("Data anggota berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    conn.Close()

                    ' Kembali ke Form9
                    Dim form9 As New Form9()
                    form9.Show()
                    Me.Close()
                Else
                    MessageBox.Show("Gagal mengupdate data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                ' MODE INSERT (Tambah Baru)

                ' Cek duplikasi NIM/NIK
                cmd = New MySqlCommand("SELECT * FROM anggota WHERE nik_nis = @nik", conn)
                cmd.Parameters.AddWithValue("@nik", TextBox1.Text)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    dr.Close()
                    MessageBox.Show("NIM/NIK sudah terdaftar! Gunakan NIM/NIK yang berbeda.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    conn.Close()
                    Return
                End If
                dr.Close()

                ' Insert data baru
                cmd = New MySqlCommand("INSERT INTO anggota (nik_nis, nama, email, telepon, alamat) VALUES (@nik, @nama, @email, @telepon, @alamat)", conn)
                cmd.Parameters.AddWithValue("@nik", TextBox1.Text)
                cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
                cmd.Parameters.AddWithValue("@email", TextBox3.Text)
                cmd.Parameters.AddWithValue("@telepon", TextBox4.Text)
                cmd.Parameters.AddWithValue("@alamat", TextBox5.Text)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data anggota berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                BersihkanForm()
            End If

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin kembali ke halaman login?", "konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Form3.Show()
            Me.Close()
        ElseIf result = DialogResult.Cancel Then

        End If


    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Dim form As New Form10
        form.Show()
        Me.Close()
    End Sub
End Class