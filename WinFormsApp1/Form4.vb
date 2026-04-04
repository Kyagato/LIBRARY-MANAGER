Imports System.Xml
Imports MySql.Data.MySqlClient
Public Class Form4
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader


    Public isEditMode As Boolean = False
    Public editIdBuku As String = ""

    Sub BersihkanForm()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox1.Focus()


        isEditMode = False
        editIdBuku = ""
        daftar.Text = "DAFTAR"
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
        Dim idBuku As String = TextBox1.Text
        Dim judulBuku As String = TextBox2.Text
        Dim penulis As String = TextBox3.Text
        Dim penerbit As String = TextBox4.Text
        Dim halaman As Integer = TextBox5.Text

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MessageBox.Show("Semua field harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            BukaKoneksi()

            If isEditMode Then

                cmd = New MySqlCommand("UPDATE buku SET judul = @judul, penulis = @penulis, penerbit = @penerbit, halaman = @halaman WHERE id_buku = @id_buku", conn)
                cmd.Parameters.AddWithValue("@id_buku", editIdBuku)
                cmd.Parameters.AddWithValue("@judul", TextBox2.Text)
                cmd.Parameters.AddWithValue("@penulis", TextBox3.Text)
                cmd.Parameters.AddWithValue("@penerbit", TextBox4.Text)
                cmd.Parameters.AddWithValue("@halaman", TextBox5.Text)

                Dim affected As Integer = cmd.ExecuteNonQuery()

                If affected > 0 Then
                    MessageBox.Show("Data buku berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    conn.Close()


                    Dim form8 As New Form8()
                    form8.Show()
                    Me.Close()
                Else
                    MessageBox.Show("Gagal mengupdate data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else



                cmd = New MySqlCommand("SELECT * FROM buku WHERE id_buku = @id_buku", conn)
                cmd.Parameters.AddWithValue("@id_buku", TextBox1.Text)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    dr.Close()
                    MessageBox.Show("ID Buku sudah ada! Gunakan ID yang berbeda.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    conn.Close()
                    Return
                End If
                dr.Close()

                ' Insert data baru
                cmd = New MySqlCommand("INSERT INTO buku (id_buku, judul, penulis, penerbit, halaman) VALUES (@id_buku, @judul, @penulis, @penerbit, @halaman)", conn)
                cmd.Parameters.AddWithValue("@id_buku", TextBox1.Text)
                cmd.Parameters.AddWithValue("@judul", TextBox2.Text)
                cmd.Parameters.AddWithValue("@penulis", TextBox3.Text)
                cmd.Parameters.AddWithValue("@penerbit", TextBox4.Text)
                cmd.Parameters.AddWithValue("@halaman", TextBox5.Text)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data buku berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin kembali ke login?", "konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
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

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            cmd = New MySqlCommand("SELECT * FROM buku WHERE id_buku = @id_buku", conn)
            cmd.Parameters.AddWithValue("@id_buku", editIdBuku)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                TextBox1.Text = dr("id_buku").ToString()
                TextBox1.ReadOnly = True
                TextBox2.Text = dr("judul").ToString()
                TextBox3.Text = dr("penulis").ToString()
                TextBox4.Text = dr("penerbit").ToString()
                TextBox5.Text = dr("halaman").ToString()
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

            Dim result As DialogResult = MessageBox.Show("Batalkan edit data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Dim form8 As New Form8()
                form8.Show()
                Me.Close()
            End If
        Else

            BersihkanForm()
        End If
    End Sub

End Class