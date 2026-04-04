Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class Form9

    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dr As MySqlDataReader


    Dim WithEvents PrintDoc As New PrintDocument
    Dim rowIndex As Integer = 0

    Sub TampilData()
        Try
            BukaKoneksi()
            ' TANPA kolom id
            da = New MySqlDataAdapter("SELECT nik_nis AS 'NIM/NIK', nama AS 'NAMA', email AS 'EMAIL', telepon AS 'TELEPON', alamat AS 'ALAMAT' FROM anggota", conn)
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



    Sub caridata()
        Try
            BukaKoneksi()
            Dim keyword As String = TextBox1.Text
            ' TANPA kolom id
            da = New MySqlDataAdapter("SELECT nik_nis AS 'NIM/NIK', nama AS 'NAMA', email AS 'EMAIL', telepon AS 'TELEPON', alamat AS 'ALAMAT' FROM anggota WHERE nik_nis LIKE '%" & keyword & "%' OR nama LIKE '%" & keyword & "%' OR email LIKE '%" & keyword & "%' OR telepon LIKE '%" & keyword & "%' OR alamat LIKE '%" & keyword & "%'", conn)
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

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampildata()

        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("NIM/NIK")
        ComboBox1.Items.Add("NAMA")
        ComboBox1.Items.Add("EMAIL")
        ComboBox1.Items.Add("TELEPON")
        ComboBox1.Items.Add("ALAMAT")
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub ComboBoxSortBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            BukaKoneksi()
            Dim kolom As String = ""

            Select Case ComboBox1.SelectedIndex
                Case 0
                    kolom = "nik_nis"
                Case 1
                    kolom = "nama"
                Case 2
                    kolom = "email"
                Case 3
                    kolom = "telepon"
                Case 4
                    kolom = "alamat"
            End Select

            da = New MySqlDataAdapter("SELECT nik_nis AS 'NIM/NIK', nama AS 'NAMA', email AS 'EMAIL', telepon AS 'TELEPON', alamat AS 'ALAMAT' FROM anggota ORDER BY " & kolom, conn)
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

    Private Sub TextBoxCari_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TampilData()
        Else
            caridata()
        End If
    End Sub


    Private Sub Form9_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        tampildata()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Silakan pilih data yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)


            Dim nikNis As String = row.Cells("NIM/NIK").Value.ToString()
            Dim nama As String = row.Cells("NAMA").Value.ToString()
            Dim email As String = row.Cells("EMAIL").Value.ToString()
            Dim telepon As String = row.Cells("TELEPON").Value.ToString()
            Dim alamat As String = row.Cells("ALAMAT").Value.ToString()

            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus data anggota:" & vbCrLf & vbCrLf &
                                                         "NIM/NIK: " & nikNis & vbCrLf &
                                                         "Nama: " & nama & "?",
                                                         "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                BukaKoneksi()


                cmd = New MySqlCommand("DELETE FROM anggota WHERE nik_nis = @nik AND nama = @nama AND email = @email AND telepon = @telepon AND alamat = @alamat LIMIT 1", conn)
                cmd.Parameters.AddWithValue("@nik", nikNis)
                cmd.Parameters.AddWithValue("@nama", nama)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@telepon", telepon)
                cmd.Parameters.AddWithValue("@alamat", alamat)

                Dim affected As Integer = cmd.ExecuteNonQuery()

                If affected > 0 Then
                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TampilData()
                Else
                    MessageBox.Show("Data gagal dihapus atau sudah tidak ada!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Dim nikNis As String = row.Cells("NIM/NIK").Value.ToString()

            Dim form5 As New Form5()
            form5.isEditMode = True
            form5.editNikNis = nikNis
            form5.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDoc.PrintPage
        Dim fontHeader As New Font("Arial", 16, FontStyle.Bold)
        Dim fontSubHeader As New Font("Arial", 10)
        Dim fontTable As New Font("Arial", 8)
        Dim fontTableHeader As New Font("Arial", 9, FontStyle.Bold)

        Dim x As Integer = 30
        Dim y As Integer = 30
        Dim lineHeight As Integer = 20

        ' Header dokumen
        e.Graphics.DrawString("LAPORAN DATA ANGGOTA", fontHeader, Brushes.Black, x, y)
        y += lineHeight + 5
        e.Graphics.DrawString("Tanggal Cetak: " & DateTime.Now.ToString("dd MMMM yyyy HH:mm"), fontSubHeader, Brushes.Black, x, y)
        y += lineHeight
        e.Graphics.DrawString("Total Data: " & DataGridView1.Rows.Count.ToString() & " Anggota", fontSubHeader, Brushes.Black, x, y)
        y += lineHeight + 10

        e.Graphics.DrawLine(New Pen(Color.Black, 2), x, y, e.PageBounds.Width - 30, y)
        y += 15

        ' Header tabel
        Dim colWidths() As Integer = {100, 150, 150, 100, 150} ' NIM/NIK, NAMA, EMAIL, TELEPON, ALAMAT
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
                        cellValue = row.Cells(col.Index).Value.ToString()
                    Else
                        cellValue = "-"
                    End If

                    ' Batasi panjang text
                    If cellValue.Length > 25 Then
                        cellValue = cellValue.Substring(0, 23) & ".."
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
End Class