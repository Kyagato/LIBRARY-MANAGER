<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Panel1 = New Panel()
        PictureBox2 = New PictureBox()
        PictureBox1 = New PictureBox()
        Label14 = New Label()
        Panel2 = New Panel()
        Panel3 = New Panel()
        Label15 = New Label()
        Button2 = New Button()
        Label11 = New Label()
        Label12 = New Label()
        Label13 = New Label()
        Label5 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        PictureBox3 = New PictureBox()
        Panel1.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Blue
        Panel1.Controls.Add(PictureBox2)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Label14)
        Panel1.Location = New Point(342, 517)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(637, 141)
        Panel1.TabIndex = 0
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = My.Resources.Resources.pngwing_com__1_
        PictureBox2.Location = New Point(471, 3)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(80, 92)
        PictureBox2.SizeMode = PictureBoxSizeMode.AutoSize
        PictureBox2.TabIndex = 17
        PictureBox2.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.pngwing_com
        PictureBox1.Location = New Point(70, 23)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(73, 80)
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        PictureBox1.TabIndex = 16
        PictureBox1.TabStop = False
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Poor Richard", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label14.ForeColor = Color.White
        Label14.Location = New Point(149, 48)
        Label14.Name = "Label14"
        Label14.Size = New Size(328, 38)
        Label14.TabIndex = 15
        Label14.Text = "LIBRARY MANAGER"
        ' 
        ' Panel2
        ' 
        Panel2.AutoSize = True
        Panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Panel2.BackgroundImage = My.Resources.Resources.istockphoto_139786707_612x612
        Panel2.Location = New Point(342, 16)
        Panel2.Margin = New Padding(3, 4, 3, 4)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(0, 0)
        Panel2.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Blue
        Panel3.Controls.Add(Label15)
        Panel3.Controls.Add(Button2)
        Panel3.Controls.Add(Label11)
        Panel3.Controls.Add(Label12)
        Panel3.Controls.Add(Label13)
        Panel3.Controls.Add(Label5)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label10)
        Panel3.Controls.Add(Label6)
        Panel3.Controls.Add(Label7)
        Panel3.Controls.Add(Label8)
        Panel3.Controls.Add(Label4)
        Panel3.Controls.Add(Label3)
        Panel3.Controls.Add(Label2)
        Panel3.Controls.Add(Label1)
        Panel3.Location = New Point(14, 16)
        Panel3.Margin = New Padding(3, 4, 3, 4)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(311, 643)
        Panel3.TabIndex = 1
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label15.ForeColor = Color.White
        Label15.Location = New Point(58, 524)
        Label15.Name = "Label15"
        Label15.Size = New Size(220, 28)
        Label15.TabIndex = 15
        Label15.Text = "📨LOG PEMINJAMAN" & vbCrLf
        Label15.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Red
        Button2.ForeColor = Color.White
        Button2.Location = New Point(80, 584)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(115, 55)
        Button2.TabIndex = 14
        Button2.Text = "LOGOUT"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label11.ForeColor = Color.White
        Label11.Location = New Point(57, 481)
        Label11.Name = "Label11"
        Label11.Size = New Size(196, 28)
        Label11.TabIndex = 13
        Label11.Text = "👥DATA ANGGOTA"
        Label11.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label12.ForeColor = Color.White
        Label12.Location = New Point(57, 439)
        Label12.Name = "Label12"
        Label12.Size = New Size(153, 28)
        Label12.TabIndex = 12
        Label12.Text = "📚DATA BUKU"
        Label12.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.BackColor = Color.White
        Label13.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label13.ForeColor = Color.Blue
        Label13.Location = New Point(40, 387)
        Label13.Name = "Label13"
        Label13.Size = New Size(80, 35)
        Label13.TabIndex = 11
        Label13.Text = "DATA"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.White
        Label5.Location = New Point(57, 337)
        Label5.Name = "Label5"
        Label5.Size = New Size(196, 28)
        Label5.TabIndex = 10
        Label5.Text = "📔PENGEMBALIAN"
        Label5.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.ForeColor = Color.White
        Label9.Location = New Point(57, 295)
        Label9.Name = "Label9"
        Label9.Size = New Size(175, 28)
        Label9.TabIndex = 9
        Label9.Text = "📕PEMINJAMAN"
        Label9.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.BackColor = Color.White
        Label10.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.ForeColor = Color.Blue
        Label10.Location = New Point(40, 243)
        Label10.Name = "Label10"
        Label10.Size = New Size(154, 35)
        Label10.TabIndex = 8
        Label10.Text = "TRANSAKSI"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.White
        Label6.Location = New Point(58, 336)
        Label6.Name = "Label6"
        Label6.Size = New Size(168, 28)
        Label6.TabIndex = 7
        Label6.Text = "PENGEMBALIAN"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.White
        Label7.Location = New Point(58, 293)
        Label7.Name = "Label7"
        Label7.Size = New Size(147, 28)
        Label7.TabIndex = 6
        Label7.Text = "PEMINJAMAN"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = Color.White
        Label8.Location = New Point(41, 241)
        Label8.Name = "Label8"
        Label8.Size = New Size(154, 35)
        Label8.TabIndex = 5
        Label8.Text = "TRANSAKSI"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.White
        Label4.Location = New Point(58, 188)
        Label4.Name = "Label4"
        Label4.Size = New Size(172, 28)
        Label4.TabIndex = 4
        Label4.Text = "+TAMBAH BUKU"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.White
        Label3.Location = New Point(41, 149)
        Label3.Name = "Label3"
        Label3.Size = New Size(221, 28)
        Label3.TabIndex = 3
        Label3.Text = " +TAMBAH ANGGOTA"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(58, 105)
        Label2.Name = "Label2"
        Label2.Size = New Size(136, 28)
        Label2.TabIndex = 2
        Label2.Text = "🏠BERANDA"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.White
        Label1.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Blue
        Label1.Location = New Point(41, 53)
        Label1.Name = "Label1"
        Label1.Size = New Size(188, 35)
        Label1.TabIndex = 1
        Label1.Text = "MENU UTAMA"
        ' 
        ' PictureBox3
        ' 
        PictureBox3.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox3.Image = My.Resources.Resources.Perpustakaan
        PictureBox3.Location = New Point(342, 15)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(637, 495)
        PictureBox3.TabIndex = 2
        PictureBox3.TabStop = False
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(999, 670)
        Controls.Add(PictureBox3)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form2"
        Text = "Halaman Utama"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label15 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
End Class
