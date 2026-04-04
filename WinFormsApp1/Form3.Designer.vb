<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        GroupBox1 = New GroupBox()
        login = New Button()
        Label4 = New Label()
        TextBox3 = New TextBox()
        Label3 = New Label()
        signUp = New Button()
        TextBox2 = New TextBox()
        Panel1 = New Panel()
        Label1 = New Label()
        GroupBox1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.BackColor = Color.White
        GroupBox1.Controls.Add(login)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(signUp)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Panel1)
        GroupBox1.Location = New Point(131, 67)
        GroupBox1.Margin = New Padding(3, 4, 3, 4)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(3, 4, 3, 4)
        GroupBox1.Size = New Size(653, 467)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        ' 
        ' login
        ' 
        login.BackColor = Color.Blue
        login.FlatStyle = FlatStyle.Popup
        login.Font = New Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        login.ForeColor = SystemColors.ButtonHighlight
        login.Location = New Point(499, 380)
        login.Name = "login"
        login.Size = New Size(120, 52)
        login.TabIndex = 8
        login.Text = "LOGIN"
        login.UseVisualStyleBackColor = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.Blue
        Label4.Location = New Point(71, 235)
        Label4.Name = "Label4"
        Label4.Size = New Size(124, 35)
        Label4.TabIndex = 7
        Label4.Text = "Password"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(215, 224)
        TextBox3.Margin = New Padding(3, 4, 3, 4)
        TextBox3.Multiline = True
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(316, 59)
        TextBox3.TabIndex = 6
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Blue
        Label3.Location = New Point(117, 165)
        Label3.Name = "Label3"
        Label3.Size = New Size(78, 35)
        Label3.TabIndex = 5
        Label3.Text = "Email"
        ' 
        ' signUp
        ' 
        signUp.BackColor = Color.Red
        signUp.FlatStyle = FlatStyle.Popup
        signUp.ForeColor = Color.White
        signUp.Location = New Point(26, 377)
        signUp.Margin = New Padding(3, 4, 3, 4)
        signUp.Name = "signUp"
        signUp.Size = New Size(115, 55)
        signUp.TabIndex = 4
        signUp.Text = "SIGN UP"
        signUp.UseVisualStyleBackColor = False
        ' 
        ' TextBox2
        ' 
        TextBox2.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TextBox2.Location = New Point(215, 157)
        TextBox2.Margin = New Padding(3, 4, 3, 4)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(316, 59)
        TextBox2.TabIndex = 2
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Blue
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(653, 72)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(289, 16)
        Label1.Name = "Label1"
        Label1.Size = New Size(92, 35)
        Label1.TabIndex = 0
        Label1.Text = "LOGIN"
        ' 
        ' Form3
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Navy
        ClientSize = New Size(914, 600)
        Controls.Add(GroupBox1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "Form3"
        Text = "Halaman login"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents signUp As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents login As Button
End Class
