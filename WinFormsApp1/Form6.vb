Imports MySql.Data.MySqlClient
Public Class Form6
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text <> "" Then
            Try
                BukaKoneksi()
                cmd = New MySqlCommand("SELECT * FROM buku WHERE id_buku = @id_buku", conn)
                cmd.Parameters.AddWithValue("@id_buku", TextBox1.Text)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    TextBox2.Text = dr("judul").ToString()
                    TextBox3.Text = dr("penulis").ToString()
                    TextBox4.Text = dr("penerbit").ToString()
                    TextBox5.Text = dr("halaman").ToString()
                Else
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                End If

                dr.Close()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        Else
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
        End If
    End Sub
    Private Sub TextBoxNimNik_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        If TextBox10.Text <> "" Then
            Try
                BukaKoneksi()
                cmd = New MySqlCommand("SELECT * FROM anggota WHERE nik_nis = @nik", conn)
                cmd.Parameters.AddWithValue("@nik", TextBox10.Text)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    TextBox9.Text = dr("nama").ToString()
                    TextBox8.Text = dr("email").ToString()
                    TextBox7.Text = dr("telepon").ToString()
                    TextBox6.Text = dr("alamat").ToString()
                Else
                    TextBox9.Clear()
                    TextBox8.Clear()
                    TextBox7.Clear()
                    TextBox6.Clear()
                End If

                dr.Close()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        Else
            TextBox9.Clear()
            TextBox8.Clear()
            TextBox7.Clear()
            TextBox6.Clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox10.Text = "" Or TextBox2.Text = "" Or TextBox9.Text = "" Then
            MessageBox.Show("Pastikan ID Buku dan NIM/NIK valid dan sudah terisi lengkap!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' ==== CEK APAKAH BUKU MASIH DIPINJAM ====
            BukaKoneksi()
            cmd = New MySqlCommand("SELECT * FROM peminjaman WHERE id_buku = @id_buku AND tanggal_pengembalian IS NULL", conn)
            cmd.Parameters.AddWithValue("@id_buku", TextBox1.Text)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                dr.Close()
                conn.Close()
                MessageBox.Show("Buku ini sedang dipinjam dan belum dikembalikan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            dr.Close()
            conn.Close()

            ' ==== SIMPAN DATA PEMINJAMAN ====
            BukaKoneksi()
            cmd = New MySqlCommand("INSERT INTO peminjaman (id_buku, judul, penulis, penerbit, halaman, nik_nis, nama, email, telepon, alamat, tanggal_pinjam, tanggal_batas_kembali)
                                VALUES (@id_buku, @judul, @penulis, @penerbit, @halaman, @nik, @nama, @email, @telepon, @alamat, @tgl_pinjam, @tgl_batas)", conn)

            cmd.Parameters.AddWithValue("@id_buku", TextBox1.Text)
            cmd.Parameters.AddWithValue("@judul", TextBox2.Text)
            cmd.Parameters.AddWithValue("@penulis", TextBox3.Text)
            cmd.Parameters.AddWithValue("@penerbit", TextBox4.Text)
            cmd.Parameters.AddWithValue("@halaman", TextBox5.Text)
            cmd.Parameters.AddWithValue("@nik", TextBox10.Text)
            cmd.Parameters.AddWithValue("@nama", TextBox9.Text)
            cmd.Parameters.AddWithValue("@email", TextBox8.Text)
            cmd.Parameters.AddWithValue("@telepon", TextBox7.Text)
            cmd.Parameters.AddWithValue("@alamat", TextBox6.Text)
            cmd.Parameters.Add("@tgl_pinjam", MySqlDbType.DateTime).Value = DateTimePicker1.Value
            cmd.Parameters.Add("@tgl_batas", MySqlDbType.DateTime).Value = DateTimePicker2.Value

            cmd.ExecuteNonQuery()
            conn.Close()

            Dim pesan As String = "Data peminjaman berhasil disimpan!" & vbCrLf & vbCrLf &
                              "Status: DIPINJAM" & vbCrLf &
                              "Batas Kembali: " & DateTimePicker2.Value.ToString("dd MMMM yyyy")
            MessageBox.Show(pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            BersihkanForm()

        Catch ex As Exception
            MessageBox.Show("Error menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BersihkanForm()
    End Sub

    Sub BersihkanForm()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox10.Clear()
        TextBox9.Clear()
        TextBox8.Clear()
        TextBox7.Clear()
        TextBox6.Clear()
        DateTimePicker1.Value = DateTime.Now
        DateTimePicker2.Value = DateTime.Now.AddDays(7)

        TextBox1.Focus()
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim form As New Form2()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim form As New Form5()
        form.Show()
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim form As New Form4()
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

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Form3.Show()
            Me.Close()
        ElseIf result = DialogResult.Cancel Then

        End If
    End Sub

    Private Sub Label27_Click(sender As Object, e As EventArgs) Handles Label27.Click
        Dim form As New Form10
        form.Show()
        Me.Close()
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BersihkanForm()
    End Sub
End Class