<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Queries\test\Common.linq</Reference>
  <Namespace>System.Net.Sockets</Namespace>
</Query>

void Main()
{
	var port = 13000;
	var client = new TcpClient("127.0.0.1", port);

	var message = "Hello, Server!";
	var data = Encoding.ASCII.GetBytes(message);
	var stream = client.GetStream();
	stream.Write(data, 0, data.Length);
	
	Console.WriteLine("Sent: {0}", message);

	var responseData = String.Empty;
	var bytes = stream.Read(data, 0, data.Length);
	responseData = Encoding.ASCII.GetString(data, 0, bytes);
	Console.WriteLine("Received: {0}", responseData);

	stream.Close();
	client.Close();
}

