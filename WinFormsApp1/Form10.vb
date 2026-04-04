Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient

Public Class Form10

    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim WithEvents PrintDocBukti As New PrintDocument
    Dim selectedIdBuku As String = ""
    Dim selectedData As DataRow = Nothing


    Sub TampilData()
        Try
            BukaKoneksi()
            da = New MySqlDataAdapter("SELECT id_buku AS 'ID BUKU', judul AS 'JUDUL', penulis AS 'PENULIS', nik_nis AS 'NIM/NIK', nama AS 'NAMA', email AS 'EMAIL', DATE_FORMAT(tanggal_pinjam, '%d/%m/%Y') AS 'TGL PINJAM', DATE_FORMAT(tanggal_batas_kembali, '%d/%m/%Y') AS 'BATAS KEMBALI', IF(tanggal_pengembalian IS NULL, '-', DATE_FORMAT(tanggal_pengembalian, '%d/%m/%Y')) AS 'TGL KEMBALI', CASE WHEN tanggal_pengembalian IS NULL THEN 'DIPINJAM' ELSE 'DIKEMBALIKAN' END AS 'STATUS' FROM peminjaman ORDER BY tanggal_pinjam DESC", conn)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            With DataGridView1
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .RowHeadersVisible = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .AllowUserToAddRows = False
                .ReadOnly = True
            End With

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error menampilkan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    Sub CariData()
        Try
            BukaKoneksi()
            Dim keyword As String = TextBox1.Text
            da = New MySqlDataAdapter("SELECT id_buku AS 'ID BUKU', judul AS 'JUDUL', penulis AS 'PENULIS', nik_nis AS 'NIM/NIK', nama AS 'NAMA', email AS 'EMAIL', DATE_FORMAT(tanggal_pinjam, '%d/%m/%Y') AS 'TGL PINJAM', DATE_FORMAT(tanggal_batas_kembali, '%d/%m/%Y') AS 'BATAS KEMBALI', IF(tanggal_pengembalian IS NULL, '-', DATE_FORMAT(tanggal_pengembalian, '%d/%m/%Y')) AS 'TGL KEMBALI', CASE WHEN tanggal_pengembalian IS NULL THEN 'DIPINJAM' ELSE 'DIKEMBALIKAN' END AS 'STATUS' FROM peminjaman WHERE id_buku LIKE '%" & keyword & "%' OR judul LIKE '%" & keyword & "%' OR nik_nis LIKE '%" & keyword & "%' OR nama LIKE '%" & keyword & "%' ORDER BY tanggal_pinjam DESC", conn)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error pencarian: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("ID BUKU")
        ComboBox1.Items.Add("JUDUL")
        ComboBox1.Items.Add("NIM/NIK")
        ComboBox1.Items.Add("NAMA")
        ComboBox1.Items.Add("TGL PINJAM")
        ComboBox1.Items.Add("STATUS")
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Silakan pilih data yang ingin dicetak buktinya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If selectedData Is Nothing Then
            MessageBox.Show("Data tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim PrintPreview As New PrintPreviewDialog
            PrintPreview.Document = PrintDocBukti
            PrintPreview.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error saat mencetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub PrintDocBukti_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocBukti.PrintPage
        If selectedData Is Nothing Then Return

        Dim fontTitle As New Font("Arial", 18, FontStyle.Bold)
        Dim fontHeader As New Font("Arial", 12, FontStyle.Bold)
        Dim fontNormal As New Font("Arial", 10)
        Dim fontSmall As New Font("Arial", 8)

        Dim x As Integer = 50
        Dim y As Integer = 50
        Dim lineHeight As Integer = 25
        Dim cardWidth As Integer = 700

        ' Border card
        e.Graphics.DrawRectangle(New Pen(Color.Black, 3), x, y, cardWidth, 600)

        ' Header
        y += 20
        Dim titleText As String = "BUKTI PEMINJAMAN BUKU"
        Dim titleSize As SizeF = e.Graphics.MeasureString(titleText, fontTitle)
        e.Graphics.DrawString(titleText, fontTitle, Brushes.Black, x + (cardWidth - titleSize.Width) / 2, y)

        y += 40
        e.Graphics.DrawLine(New Pen(Color.Black, 2), x + 20, y, x + cardWidth - 20, y)

        ' Data Buku
        y += 30
        e.Graphics.DrawString("INFORMASI BUKU", fontHeader, Brushes.Black, x + 30, y)
        y += lineHeight + 5

        e.Graphics.DrawString("ID Buku", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("id_buku").ToString(), fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Judul", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("judul").ToString(), fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Penulis", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("penulis").ToString(), fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Penerbit", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("penerbit").ToString(), fontNormal, Brushes.Black, x + 180, y)

        ' Data Peminjam
        y += lineHeight + 20
        e.Graphics.DrawLine(Pens.Gray, x + 20, y, x + cardWidth - 20, y)
        y += 20
        e.Graphics.DrawString("INFORMASI PEMINJAM", fontHeader, Brushes.Black, x + 30, y)
        y += lineHeight + 5

        e.Graphics.DrawString("NIM/NIK", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("nik_nis").ToString(), fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Nama", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("nama").ToString(), fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Telepon", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("telepon").ToString(), fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Alamat", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & selectedData("alamat").ToString(), fontNormal, Brushes.Black, x + 180, y)

        ' Tanggal Peminjaman
        y += lineHeight + 20
        e.Graphics.DrawLine(Pens.Gray, x + 20, y, x + cardWidth - 20, y)
        y += 20
        e.Graphics.DrawString("INFORMASI PEMINJAMAN", fontHeader, Brushes.Black, x + 30, y)
        y += lineHeight + 5

        Dim tglPinjam As String = Convert.ToDateTime(selectedData("tanggal_pinjam")).ToString("dd MMMM yyyy")
        e.Graphics.DrawString("Tanggal Pinjam", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & tglPinjam, fontNormal, Brushes.Black, x + 180, y)
        y += lineHeight

        Dim tglBatas As String = Convert.ToDateTime(selectedData("tanggal_batas_kembali")).ToString("dd MMMM yyyy")
        e.Graphics.DrawString("Batas Kembali", fontNormal, Brushes.Black, x + 30, y)
        e.Graphics.DrawString(": " & tglBatas, New Font("Arial", 10, FontStyle.Bold), Brushes.Red, x + 180, y)
        y += lineHeight

        e.Graphics.DrawString("Tanggal Kembali", fontNormal, Brushes.Black, x + 30, y)
        If IsDBNull(selectedData("tanggal_pengembalian")) Then
            e.Graphics.DrawString(": -", fontNormal, Brushes.Gray, x + 180, y)
        Else
            Dim tglKembali As String = Convert.ToDateTime(selectedData("tanggal_pengembalian")).ToString("dd MMMM yyyy")
            e.Graphics.DrawString(": " & tglKembali, fontNormal, Brushes.Green, x + 180, y)
        End If
        y += lineHeight

        Dim status As String = If(IsDBNull(selectedData("tanggal_pengembalian")), "DIPINJAM", "DIKEMBALIKAN")
        e.Graphics.DrawString("Status", fontNormal, Brushes.Black, x + 30, y)
        Dim statusBrush As Brush = If(status = "DIPINJAM", Brushes.Red, Brushes.Green)
        e.Graphics.DrawString(": " & status, New Font("Arial", 10, FontStyle.Bold), statusBrush, x + 180, y)

        ' Footer
        y += lineHeight + 40
        e.Graphics.DrawLine(New Pen(Color.Black, 2), x + 20, y, x + cardWidth - 20, y)
        y += 15
        e.Graphics.DrawString("Dicetak pada: " & DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"), fontSmall, Brushes.Gray, x + 30, y)
        y += 15
        e.Graphics.DrawString("Harap kembalikan buku sebelum batas waktu yang ditentukan", fontSmall, Brushes.Gray, x + 30, y)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin kembali ke halaman login?", "konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Form3.Show()
            Me.Close()
        ElseIf result = DialogResult.Cancel Then

        End If
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
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MessageBox.Show("anda akan diarahkan ke halaman laporan peminjaman buku", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Form11.Show()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            BukaKoneksi()
            Dim kolom As String = ""

            Select Case ComboBox1.SelectedIndex
                Case 0
                    kolom = "id_buku"
                Case 1
                    kolom = "judul"
                Case 2
                    kolom = "nik_nis"
                Case 3
                    kolom = "nama"
                Case 4
                    kolom = "tanggal_pinjam"
                Case 5
                    kolom = "tanggal_pengembalian"
            End Select

            da = New MySqlDataAdapter("Select id_buku As 'ID BUKU', judul AS 'JUDUL', penulis AS 'PENULIS', nik_nis AS 'NIM/NIK', nama AS 'NAMA', telepon AS 'TELEPON', DATE_FORMAT(tanggal_pinjam, '%d/%m/%Y') AS 'TGL PINJAM', DATE_FORMAT(tanggal_batas_kembali, '%d/%m/%Y') AS 'BATAS KEMBALI', IF(tanggal_pengembalian IS NULL, '-', DATE_FORMAT(tanggal_pengembalian, '%d/%m/%Y')) AS 'TGL KEMBALI', CASE WHEN tanggal_pengembalian IS NULL THEN 'DIPINJAM' ELSE 'DIKEMBALIKAN' END AS 'STATUS' FROM peminjaman ORDER BY " & kolom & " DESC", conn)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error sorting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    Private Sub Form10_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        TampilData()
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                selectedIdBuku = row.Cells("ID BUKU").Value.ToString()


                BukaKoneksi()
                cmd = New MySqlCommand("SELECT * FROM peminjaman WHERE id_buku = @id_buku ORDER BY tanggal_pinjam DESC LIMIT 1", conn)
                cmd.Parameters.AddWithValue("@id_buku", selectedIdBuku)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim tempDt As New DataTable
                adapter.Fill(tempDt)
                If tempDt.Rows.Count > 0 Then
                    selectedData = tempDt.Rows(0)
                End If
                conn.Close()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Silakan pilih data yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim idBuku As String = row.Cells("ID BUKU").Value.ToString()
            Dim judul As String = row.Cells("JUDUL").Value.ToString()
            Dim nama As String = row.Cells("NAMA").Value.ToString()

            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus riwayat peminjaman:" & vbCrLf & vbCrLf &
                                                         "ID Buku: " & idBuku & vbCrLf &
                                                         "Judul: " & judul & vbCrLf &
                                                         "Peminjam: " & nama & "?",
                                                         "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                BukaKoneksi()
                cmd = New MySqlCommand("DELETE FROM peminjaman WHERE id_buku = @id_buku LIMIT 1", conn)
                cmd.Parameters.AddWithValue("@id_buku", idBuku)

                Dim affected As Integer = cmd.ExecuteNonQuery()

                If affected > 0 Then
                    MessageBox.Show("Data riwayat berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TampilData()
                Else
                    MessageBox.Show("Data gagal dihapus!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error menghapus data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TampilData()
        Else
            CariData()
        End If
    End Sub
End Class