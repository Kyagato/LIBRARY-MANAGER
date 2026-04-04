Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class Form8

    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable


    Dim WithEvents PrintDoc As New PrintDocument
    Dim rowIndex As Integer = 0

    Public Sub TampilData(Optional filter As String = "")
        Try
            BukaKoneksi()
            Dim query As String = "SELECT id_buku AS 'ID BUKU', judul AS 'JUDUL', penulis AS 'PENULIS', penerbit AS 'PENERBIT', halaman AS 'HALAMAN' FROM buku"
            If filter <> "" Then
                query &= " WHERE judul LIKE @filter OR penulis LIKE @filter OR penerbit LIKE @filter OR id_buku LIKE @filter"
            End If

            cmd = New MySqlCommand(query, conn)
            If filter <> "" Then cmd.Parameters.AddWithValue("@filter", "%" & filter & "%")

            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
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
            MessageBox.Show("Gagal menampilkan data: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()

        ' Isi ComboBox Sort By
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("ID BUKU")
        ComboBox1.Items.Add("JUDUL")
        ComboBox1.Items.Add("PENULIS")
        ComboBox1.Items.Add("PENERBIT")
        ComboBox1.Items.Add("HALAMAN")
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TampilData(TextBox1.Text)
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

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim form As New Form9
        form.Show()
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin kembali ke halaman login?", "konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If result = DialogResult.OK Then
            Form3.Show()
            Me.Close()
        ElseIf result = DialogResult.Cancel Then

        End If

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        Dim form As New Form10
        form.Show()
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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

            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus buku:" & vbCrLf & vbCrLf & "ID: " & idBuku & vbCrLf & "Judul: " & judul & "?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                BukaKoneksi()
                cmd = New MySqlCommand("DELETE FROM buku WHERE id_buku = @id_buku LIMIT 1", conn)
                cmd.Parameters.AddWithValue("@id_buku", idBuku)

                Dim affected As Integer = cmd.ExecuteNonQuery()

                If affected > 0 Then
                    MessageBox.Show("Data buku berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Silakan pilih data yang ingin diedit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim idBuku As String = row.Cells("ID BUKU").Value.ToString()


            Dim form4 As New Form4()
            form4.isEditMode = True
            form4.editIdBuku = idBuku
            form4.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDoc.PrintPage
        Dim fontHeader As New Font("Arial", 16, FontStyle.Bold)
        Dim fontSubHeader As New Font("Arial", 10)
        Dim fontTable As New Font("Arial", 8)
        Dim fontTableHeader As New Font("Arial", 9, FontStyle.Bold)

        Dim x As Integer = 30
        Dim y As Integer = 30
        Dim lineHeight As Integer = 20

        ' Header dokumen
        e.Graphics.DrawString("LAPORAN DATA BUKU", fontHeader, Brushes.Black, x, y)
        y += lineHeight + 5
        e.Graphics.DrawString("Tanggal Cetak: " & DateTime.Now.ToString("dd MMMM yyyy HH:mm"), fontSubHeader, Brushes.Black, x, y)
        y += lineHeight
        e.Graphics.DrawString("Total Data: " & DataGridView1.Rows.Count.ToString() & " Buku", fontSubHeader, Brushes.Black, x, y)
        y += lineHeight + 10

        e.Graphics.DrawLine(New Pen(Color.Black, 2), x, y, e.PageBounds.Width - 30, y)
        y += 15

        ' Header tabel
        Dim colWidths() As Integer = {80, 180, 130, 130, 80} ' Sesuaikan lebar kolom
        Dim startX As Integer = x
        Dim colIndex As Integer = 0

        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Visible Then
                e.Graphics.FillRectangle(Brushes.LightGray, startX, y, colWidths(colIndex), 20)
                e.Graphics.DrawRectangle(Pens.Black, startX, y, colWidths(colIndex), 20)
                e.Graphics.DrawString(col.HeaderText, fontTableHeader, Brushes.Black, startX + 2, y + 3)
                startX += colWidths(colIndex)
                colIndex += 1
            End If
        Next
        y += 20


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
                        cellValue = row.Cells(col.Index).Value.ToString()
                    Else
                        cellValue = "-"
                    End If

                    ' Batasi panjang text untuk JUDUL
                    If col.HeaderText = "JUDUL" AndAlso cellValue.Length > 30 Then
                        cellValue = cellValue.Substring(0, 28) & ".."
                    ElseIf cellValue.Length > 20 Then
                        cellValue = cellValue.Substring(0, 18) & ".."
                    End If

                    e.Graphics.DrawRectangle(Pens.Black, startX, y, colWidths(colIndex), 20)
                    e.Graphics.DrawString(cellValue, fontTable, Brushes.Black, startX + 2, y + 3)
                    startX += colWidths(colIndex)
                    colIndex += 1
                End If
            Next

            y += 20
            rowIndex += 1
        End While

        ' Footer
        If Not hasMorePages Then
            y += 20
            e.Graphics.DrawLine(New Pen(Color.Black, 2), x, y, e.PageBounds.Width - 30, y)
            y += 15
            e.Graphics.DrawString("--- Akhir Laporan ---", fontSubHeader, Brushes.Black, x, y)
        End If

        e.HasMorePages = hasMorePages
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
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
                    kolom = "penulis"
                Case 3
                    kolom = "penerbit"
                Case 4
                    kolom = "halaman"
            End Select

            Dim query As String = "SELECT id_buku AS 'ID BUKU', judul AS 'JUDUL', penulis AS 'PENULIS', penerbit AS 'PENERBIT', halaman AS 'HALAMAN' FROM buku ORDER BY " & kolom

            da = New MySqlDataAdapter(query, conn)
            dt = New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error sorting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

    End Sub
End Class