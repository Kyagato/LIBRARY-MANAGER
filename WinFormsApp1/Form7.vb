Imports MySql.Data.MySqlClient

Public Class Form7
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim selectedIdBuku As String = ""


    ' Auto-fill semua data saat input ID Buku
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Jalankan hanya jika ID Buku sudah lengkap (contoh: 6 digit)
        If TextBox1.TextLength < 6 Then
            KosongkanField()
            Return
        End If

        Try
            BukaKoneksi()
            cmd = New MySqlCommand("SELECT * FROM peminjaman WHERE id_buku = @id_buku AND tanggal_pengembalian IS NULL ORDER BY tanggal_pinjam DESC LIMIT 1", conn)
            cmd.Parameters.AddWithValue("@id_buku", TextBox1.Text)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                selectedIdBuku = dr("id_buku").ToString()
                TextBox2.Text = dr("judul").ToString()
                TextBox3.Text = dr("penulis").ToString()
                TextBox4.Text = dr("penerbit").ToString()
                TextBox5.Text = dr("halaman").ToString()
                TextBox10.Text = dr("nik_nis").ToString()
                TextBox9.Text = dr("nama").ToString()
                TextBox8.Text = dr("email").ToString()
                TextBox7.Text = dr("telepon").ToString()
                TextBox6.Text = dr("alamat").ToString()

                If Not IsDBNull(dr("tanggal_pinjam")) Then
                    DateTimePicker1.Value = Convert.ToDateTime(dr("tanggal_pinjam"))
                End If
            Else
                selectedIdBuku = ""
                KosongkanField()
                MessageBox.Show("Tidak ada data peminjaman aktif untuk ID Buku ini!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If selectedIdBuku = "" Or TextBox1.Text = "" Then
            MessageBox.Show("Silakan masukkan ID Buku yang valid dan sedang dipinjam!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            BukaKoneksi()

            ' Update tanggal_pengembalian 
            cmd = New MySqlCommand("UPDATE peminjaman SET tanggal_pengembalian = @tgl_kembali WHERE id_buku = @id_buku AND tanggal_pengembalian IS NULL", conn)
            cmd.Parameters.Add("@tgl_kembali", MySqlDbType.DateTime).Value = DateTimePicker1.Value
            cmd.Parameters.AddWithValue("@id_buku", selectedIdBuku)

            Dim affected As Integer = cmd.ExecuteNonQuery()

            If affected > 0 Then
                MessageBox.Show("Pengembalian buku berhasil dicatat!" & vbCrLf & "Status: DIKEMBALIKAN" & vbCrLf & "Buku dapat dipinjam kembali.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BersihkanForm()
            Else
                MessageBox.Show("Gagal mencatat pengembalian!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Error menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BersihkanForm()
    End Sub

    Sub KosongkanField()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox10.Clear()
        TextBox9.Clear()
        TextBox8.Clear()
        TextBox7.Clear()
        TextBox6.Clear()
    End Sub

    Sub BersihkanForm()
        TextBox1.Clear()
        KosongkanField()
        selectedIdBuku = ""
        DateTimePicker1.Value = DateTime.Now
        TextBox1.Focus()
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BersihkanForm()
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs)

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

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Dim form As New Form6()
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Form3.Show()
            Me.Close()
        ElseIf result = DialogResult.Cancel Then

        End If
    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click
        Dim form As New Form10
        form.Show()
        Me.Close()
    End Sub
End Class