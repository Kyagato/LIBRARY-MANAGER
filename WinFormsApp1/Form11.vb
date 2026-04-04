Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Public Class Form11
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable

    ' Variabel untuk printing
    Dim WithEvents PrintDoc As New PrintDocument
    Dim rowIndex As Integer = 0

    Sub TampilData()
        Try
            BukaKoneksi()

            ' Filter berdasarkan tanggal
            Dim tglDari As String = DateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglSampai As String = DateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59")

            da = New MySqlDataAdapter("SELECT id_buku AS 'ID BUKU', judul AS 'JUDUL', penulis AS 'PENULIS', nik_nis AS 'NIM/NIK', nama AS 'NAMA', telepon AS 'TELEPON', tanggal_pinjam AS 'TGL PINJAM', tanggal_pengembalian AS 'TGL KEMBALI', CASE WHEN tanggal_pengembalian IS NULL THEN 'DIPINJAM' ELSE 'DIKEMBALIKAN' END AS 'STATUS' FROM peminjaman WHERE tanggal_pinjam BETWEEN '" & tglDari & "' AND '" & tglSampai & "' ORDER BY tanggal_pinjam DESC", conn)
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
            Dim tglDari As String = DateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglSampai As String = DateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59")

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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form10.Show()
        Me.Close()
    End Sub

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateTimePicker1.Value = DateTime.Now.AddMonths(-1)
        DateTimePicker2.Value = DateTime.Now

        TampilData()


        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("ID BUKU")
        ComboBox1.Items.Add("JUDUL")
        ComboBox1.Items.Add("NIM/NIK")
        ComboBox1.Items.Add("NAMA")
        ComboBox1.Items.Add("TGL PINJAM")
        ComboBox1.Items.Add("STATUS")
        ComboBox1.SelectedIndex = 0

        PrintDoc.DefaultPageSettings.Landscape = True
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        TampilData()
    End Sub
    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        TampilData()
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
                    kolom = "status"
            End Select

            Dim tglDari As String = DateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim tglSampai As String = DateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59")

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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TampilData()
        Else
            CariData()
        End If
    End Sub
    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDoc.PrintPage
        Dim fontHeader As New Font("Arial", 16, FontStyle.Bold)
        Dim fontSubHeader As New Font("Arial", 10)
        Dim fontTable As New Font("Arial", 7)
        Dim fontTableHeader As New Font("Arial", 8, FontStyle.Bold)

        Dim x As Integer = 30
        Dim y As Integer = 30
        Dim lineHeight As Integer = 18

        ' Header dokumen
        e.Graphics.DrawString("LAPORAN PEMINJAMAN BUKU", fontHeader, Brushes.Black, x, y)
        y += lineHeight + 8
        e.Graphics.DrawString("Periode: " & DateTimePicker1.Value.ToString("dd MMMM yyyy") & " s/d " & DateTimePicker2.Value.ToString("dd MMMM yyyy"), fontSubHeader, Brushes.Black, x, y)
        y += lineHeight
        e.Graphics.DrawString("Tanggal Cetak: " & DateTime.Now.ToString("dd MMMM yyyy HH:mm"), fontSubHeader, Brushes.Black, x, y)
        y += lineHeight
        e.Graphics.DrawString("Total Data: " & DataGridView1.Rows.Count.ToString() & " Transaksi", fontSubHeader, Brushes.Black, x, y)
        y += lineHeight + 10

        e.Graphics.DrawLine(New Pen(Color.Black, 2), x, y, e.PageBounds.Width - 30, y)
        y += 15

        ' Header tabel penyesuaian LEBAR DISESUAIKAN UNTUK LANDSCAPE
        Dim colWidths() As Integer = {70, 150, 110, 90, 120, 90, 100, 100, 100, 90}
        Dim startX As Integer = x
        Dim colIndex As Integer = 0

        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Visible Then
                e.Graphics.FillRectangle(Brushes.LightGray, startX, y, colWidths(colIndex), 18)
                e.Graphics.DrawRectangle(Pens.Black, startX, y, colWidths(colIndex), 18)
                e.Graphics.DrawString(col.HeaderText, fontTableHeader, Brushes.Black, startX + 2, y + 2)
                startX += colWidths(colIndex)
                colIndex += 1
            End If
        Next
        y += 18

        ' Data tabel
        Dim hasMorePages As Boolean = False

        While rowIndex < DataGridView1.Rows.Count
            If y > e.PageBounds.Height - 100 Then
                hasMorePages = True
                Exit While
            End If

            startX = x
            colIndex = 0
            Dim row As DataGridViewRow = DataGridView1.Rows(rowIndex)

            For Each col As DataGridViewColumn In DataGridView1.Columns
                If col.Visible Then
                    Dim cellValue As String = ""

                    If row.Cells(col.Index).Value IsNot Nothing AndAlso Not IsDBNull(row.Cells(col.Index).Value) Then
                        If col.HeaderText.Contains("TGL") Then
                            Dim tgl As DateTime
                            If DateTime.TryParse(row.Cells(col.Index).Value.ToString(), tgl) Then
                                cellValue = tgl.ToString("dd/MM/yy")
                            Else
                                cellValue = "-"
                            End If
                        Else
                            cellValue = row.Cells(col.Index).Value.ToString()
                        End If
                    Else
                        cellValue = "-"
                    End If

                    ' Batasi panjang text
                    If col.HeaderText = "JUDUL" AndAlso cellValue.Length > 25 Then
                        cellValue = cellValue.Substring(0, 23) & ".."
                    ElseIf col.HeaderText = "NAMA" AndAlso cellValue.Length > 20 Then
                        cellValue = cellValue.Substring(0, 18) & ".."
                    ElseIf cellValue.Length > 15 Then
                        cellValue = cellValue.Substring(0, 13) & ".."
                    End If

                    e.Graphics.DrawRectangle(Pens.Black, startX, y, colWidths(colIndex), 18)
                    e.Graphics.DrawString(cellValue, fontTable, Brushes.Black, startX + 2, y + 2)
                    startX += colWidths(colIndex)
                    colIndex += 1
                End If
            Next

            y += 18
            rowIndex += 1
        End While

        ' Footer
        If Not hasMorePages Then
            y += 15
            e.Graphics.DrawLine(New Pen(Color.Black, 2), x, y, e.PageBounds.Width - 30, y)
            y += 15

            ' Hitung statistik
            Dim dipinjam As Integer = 0
            Dim dikembalikan As Integer = 0

            For Each row As DataRow In dt.Rows
                If IsDBNull(row("TGL KEMBALI")) OrElse row("TGL KEMBALI").ToString() = "-" Then
                    dipinjam += 1
                Else
                    dikembalikan += 1
                End If
            Next

            e.Graphics.DrawString("Ringkasan Statistik:", fontSubHeader, Brushes.Black, x, y)
            y += lineHeight
            e.Graphics.DrawString("- Total Peminjaman: " & DataGridView1.Rows.Count.ToString(), fontSubHeader, Brushes.Black, x + 20, y)
            y += lineHeight
            e.Graphics.DrawString("- Masih Dipinjam: " & dipinjam.ToString(), fontSubHeader, Brushes.Black, x + 20, y)
            y += lineHeight
            e.Graphics.DrawString("- Sudah Dikembalikan: " & dikembalikan.ToString(), fontSubHeader, Brushes.Black, x + 20, y)
            y += lineHeight + 10
            e.Graphics.DrawString("--- Akhir Laporan ---", fontSubHeader, Brushes.Black, x, y)
        End If

        e.HasMorePages = hasMorePages
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk dicetak!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            rowIndex = 0

            Dim PrintPreview As New PrintPreviewDialog
            PrintPreview.Document = PrintDoc
            PrintPreview.WindowState = FormWindowState.Maximized
            PrintPreview.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Error saat mencetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class