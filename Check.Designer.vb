<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Check
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Check))
        Me.checkBtn = New System.Windows.Forms.Button()
        Me.hwidLbl = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'checkBtn
        '
        Me.checkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.checkBtn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.checkBtn.Location = New System.Drawing.Point(57, 139)
        Me.checkBtn.Name = "checkBtn"
        Me.checkBtn.Size = New System.Drawing.Size(336, 90)
        Me.checkBtn.TabIndex = 0
        Me.checkBtn.Text = "Проверить"
        Me.checkBtn.UseVisualStyleBackColor = True
        '
        'hwidLbl
        '
        Me.hwidLbl.AutoSize = True
        Me.hwidLbl.Location = New System.Drawing.Point(57, 24)
        Me.hwidLbl.Name = "hwidLbl"
        Me.hwidLbl.Size = New System.Drawing.Size(48, 16)
        Me.hwidLbl.TabIndex = 2
        Me.hwidLbl.Text = "Label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(441, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Покупка лицензии: c00rupteddata(discord) "
        '
        'Check
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 294)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.hwidLbl)
        Me.Controls.Add(Me.checkBtn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Check"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AnonRAT - Лицензия"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents checkBtn As Button
    Friend WithEvents hwidLbl As Label
    Friend WithEvents Label1 As Label
End Class
