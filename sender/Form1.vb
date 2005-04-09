Imports System.Net.Sockets
Imports System.Text
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim r_host As String = TextBox1.Text
        Dim r_port As String = TextBox2.Text
        Dim r_msg As String = RichTextBox1.Text
        Dim tcpClient As New System.Net.Sockets.TcpClient()
        tcpClient.Connect(r_host, r_port)
        Dim networkStream As NetworkStream = tcpClient.GetStream()
        If networkStream.CanWrite And networkStream.CanRead Then
            ' Do a simple write.
            Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(r_msg)
            networkStream.Write(sendBytes, 0, sendBytes.Length)
            ' Read the NetworkStream into a byte buffer.
            Dim bytes(tcpClient.ReceiveBufferSize) As Byte
            networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
            ' Output the data received from the host to the console.
            Dim returndata As String = Encoding.ASCII.GetString(bytes)
            Console.WriteLine(("Host returned: " + returndata))
        Else
            If Not networkStream.CanRead Then
                Console.WriteLine("cannot not write data to this stream")
                tcpClient.Close()
            Else
                If Not networkStream.CanWrite Then
                    Console.WriteLine("cannot read data from this stream")
                    tcpClient.Close()
                End If
            End If
        End If
        ' pause so user can view the console output
        Console.ReadLine()
        tcpClient.Close()
    End Sub
End Class
