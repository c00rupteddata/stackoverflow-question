Imports System.Net.Sockets
Imports System.Threading
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Net

Public Class ScreenViewerForm
    Private client As TcpClient
    Private stream As NetworkStream
    Private receiveThread As Thread
    Private stopRequested As Boolean = False
    Private fpsCounter As Integer = 0
    Private WithEvents fpsTimer As New System.Windows.Forms.Timer()
    Private WithEvents PictureBox1 As New PictureBox()

    Public Sub New(ByVal client As TcpClient)
        InitializeComponent()
        Me.client = client
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Me.Controls.Add(PictureBox1)
        fpsTimer.Interval = 1000
        Me.Text = "Экран (Unknown) ФПС: 0"
    End Sub

    Private Sub ScreenViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            stream = client.GetStream()
        Catch ex As Exception
            MessageBox.Show("Ошибка получения потока: " & ex.Message)
            Me.Close()
        End Try
        fpsTimer.Start()
        receiveThread = New Thread(AddressOf ReceiveImages)
        receiveThread.IsBackground = True
        receiveThread.Start()
    End Sub

    Private Sub ReceiveImages()
        While Not stopRequested
            Try
                Dim lengthBuffer(3) As Byte
                Dim bytesRead As Integer = stream.Read(lengthBuffer, 0, 4)
                If bytesRead < 4 Then Exit While
                Dim imageLength As Integer = BitConverter.ToInt32(lengthBuffer, 0)
                If imageLength <= 0 Then Continue While
                Dim imageBuffer(imageLength - 1) As Byte
                Dim totalRead As Integer = 0
                While totalRead < imageLength
                    Dim read As Integer = stream.Read(imageBuffer, totalRead, imageLength - totalRead)
                    If read <= 0 Then Exit While
                    totalRead += read
                End While
                If totalRead < imageLength Then Exit While
                Using ms As New IO.MemoryStream(imageBuffer)
                    Dim bmp As Bitmap = New Bitmap(ms)
                    Me.Invoke(Sub()
                                  If PictureBox1.Image IsNot Nothing Then PictureBox1.Image.Dispose()
                                  PictureBox1.Image = DirectCast(bmp.Clone(), Image)
                              End Sub)
                End Using
                Interlocked.Increment(fpsCounter)
            Catch ex As Exception
                Exit While
            End Try
        End While
    End Sub

    Private Sub fpsTimer_Tick(sender As Object, e As EventArgs) Handles fpsTimer.Tick
        Dim pcName As String = GetClientName()
        Dim fps As Integer = Interlocked.Exchange(fpsCounter, 0)
        Me.Invoke(Sub() Me.Text = $"Экран ({pcName}) ФПС: {fps}")
    End Sub

    Private Function GetClientName() As String
        If client Is Nothing OrElse client.Client Is Nothing OrElse client.Client.RemoteEndPoint Is Nothing Then Return "Unknown"
        Dim remoteEP As IPEndPoint = TryCast(client.Client.RemoteEndPoint, IPEndPoint)
        Dim remoteIP As String = If(remoteEP IsNot Nothing, remoteEP.Address.ToString(), "Unknown")
        Dim pcName As String = remoteIP
        Try
            Dim hostEntry As IPHostEntry = Dns.GetHostEntry(remoteIP)
            If hostEntry IsNot Nothing AndAlso Not String.IsNullOrEmpty(hostEntry.HostName) Then pcName = hostEntry.HostName
        Catch
        End Try
        Return pcName
    End Function

    Private Sub ScreenViewerForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        stopRequested = True
        fpsTimer.Stop()
        Try
            If receiveThread IsNot Nothing AndAlso receiveThread.IsAlive Then receiveThread.Join(1000)
        Catch
        End Try
    End Sub
End Class
