<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Builder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Builder))
        Me.autoCb = New System.Windows.Forms.ComboBox()
        Me.autoCheck = New System.Windows.Forms.CheckBox()
        Me.ipTxt = New System.Windows.Forms.TextBox()
        Me.portTxt = New System.Windows.Forms.TextBox()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'autoCb
        '
        Me.autoCb.BackColor = System.Drawing.Color.Black
        Me.autoCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.autoCb.ForeColor = System.Drawing.Color.White
        Me.autoCb.FormattingEnabled = True
        Me.autoCb.Location = New System.Drawing.Point(21, 81)
        Me.autoCb.Name = "autoCb"
        Me.autoCb.Size = New System.Drawing.Size(121, 24)
        Me.autoCb.TabIndex = 0
        '
        'autoCheck
        '
        Me.autoCheck.AutoSize = True
        Me.autoCheck.Location = New System.Drawing.Point(21, 33)
        Me.autoCheck.Name = "autoCheck"
        Me.autoCheck.Size = New System.Drawing.Size(122, 20)
        Me.autoCheck.TabIndex = 1
        Me.autoCheck.Text = "Автозагрузка"
        Me.autoCheck.UseVisualStyleBackColor = True
        '
        'ipTxt
        '
        Me.ipTxt.Location = New System.Drawing.Point(170, 33)
        Me.ipTxt.Name = "ipTxt"
        Me.ipTxt.Size = New System.Drawing.Size(100, 22)
        Me.ipTxt.TabIndex = 2
        Me.ipTxt.Text = "Хост"
        '
        'portTxt
        '
        Me.portTxt.Location = New System.Drawing.Point(170, 81)
        Me.portTxt.Name = "portTxt"
        Me.portTxt.Size = New System.Drawing.Size(100, 22)
        Me.portTxt.TabIndex = 3
        Me.portTxt.Text = "Порт"
        '
        'btnBuild
        '
        Me.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuild.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnBuild.Location = New System.Drawing.Point(698, 428)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(195, 59)
        Me.btnBuild.TabIndex = 4
        Me.btnBuild.Text = "Сбилдить"
        Me.btnBuild.UseVisualStyleBackColor = True
        '
        'Builder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.AnonRAT.My.Resources.Resources.backk
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(959, 499)
        Me.Controls.Add(Me.btnBuild)
        Me.Controls.Add(Me.portTxt)
        Me.Controls.Add(Me.ipTxt)
        Me.Controls.Add(Me.autoCheck)
        Me.Controls.Add(Me.autoCb)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Builder"
        Me.Text = "AnonRAT - Builder"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents autoCb As ComboBox
    Friend WithEvents autoCheck As CheckBox
    Friend WithEvents ipTxt As TextBox
    Friend WithEvents portTxt As TextBox
    Friend WithEvents btnBuild As Button
End Class
