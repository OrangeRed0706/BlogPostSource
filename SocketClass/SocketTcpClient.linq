<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Queries\test\Common.linq</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	const int port = 13000;
	const string serverIp = "127.0.0.1";
	const int readWriteTimeout = 5000; 

	var serverAddr = IPAddress.Parse(serverIp);
	var serverEndPoint = new IPEndPoint(serverAddr, port);

	using var client = new Socket(serverAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

	Console.WriteLine("Connecting to server...");
	await client.ConnectAsync(serverEndPoint);

	if (client.Connected)
	{
		Console.WriteLine("Connected to server.");

		client.ReceiveTimeout = readWriteTimeout;
		client.SendTimeout = readWriteTimeout;

		var request = "Hello Server!";
		var data = Encoding.UTF8.GetBytes(request);

		await client.SendAsync(data, SocketFlags.None);
		Console.WriteLine("Sent: {0}", request);

		var buffer = new byte[1024];
		var byteCount = await client.ReceiveAsync(buffer, SocketFlags.None);
		var response = Encoding.UTF8.GetString(buffer, 0, byteCount);
		Console.WriteLine("Received: {0}", response);
	}
	else
	{
		Console.WriteLine("Failed to connect to server.");
	}

	client.Close();
}

