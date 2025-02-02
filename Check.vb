Imports System.Net
Imports System.IO
Imports System.Management
Imports System.Diagnostics

Public Class Check
    Private Sub Check_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsVirtualMachine() Then
            Debug.WriteLine("Запуск в виртуальной машине обнаружен.")
            Application.Exit()
        End If

        Dim hwid As String = GetHWID()
        hwidLbl.Text = hwid
    End Sub

    Private Sub copyBtn_Click(sender As Object, e As EventArgs)
        Clipboard.SetText(hwidLbl.Text)
    End Sub

    Private Async Sub checkBtn_Click(sender As Object, e As EventArgs) Handles checkBtn.Click
        Dim hwid As String = hwidLbl.Text
        Dim hwidList As List(Of String) = Await GetHWIDListAsync()

        If hwidList.Contains(hwid) Then
            Debug.WriteLine("Доступ подтверждён.")
            Dim anonRatForm As New AnonRAT()
            anonRatForm.Show()
            Me.Close()
        Else
            Debug.WriteLine("Неверный HWID. Доступ запрещён.")
        End If
    End Sub

    Private Function GetHWID() As String
        Dim cpuId As String = GetHardwareId("Win32_Processor", "ProcessorId")
        Dim diskId As String = GetHardwareId("Win32_DiskDrive", "SerialNumber")
        Return cpuId & "-" & diskId
    End Function

    Private Function GetHardwareId(query As String, propertyName As String) As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT " & propertyName & " FROM " & query)
            For Each obj As ManagementObject In searcher.Get()
                Return obj(propertyName).ToString()
            Next
        Catch ex As Exception
            Debug.WriteLine("Ошибка получения HWID: " & ex.Message)
        End Try
        Return "UNKNOWN"
    End Function

    Private Async Function GetHWIDListAsync() As Task(Of List(Of String))
        Dim hwidList As New List(Of String)
        Try
            Using client As New Net.Http.HttpClient()
                Dim response As String = Await client.GetStringAsync("https://pastebin.com/raw/q5nMcidM")
                Using reader As New StringReader(response)
                    While reader.Peek() <> -1
                        hwidList.Add(reader.ReadLine().Trim())
                    End While
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("Ошибка загрузки списка HWID: " & ex.Message)
        End Try
        Return hwidList
    End Function

    Private Function IsVirtualMachine() As Boolean
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem")
            For Each obj As ManagementObject In searcher.Get()
                Dim manufacturer As String = obj("Manufacturer").ToString().ToLower()
                Dim model As String = obj("Model").ToString().ToLower()

                If manufacturer.Contains("microsoft") AndAlso model.Contains("virtual") OrElse
                   manufacturer.Contains("vmware") OrElse
                   manufacturer.Contains("virtualbox") Then
                    Return True
                End If
            Next
        Catch
        End Try
        Return False
    End Function
End Class
