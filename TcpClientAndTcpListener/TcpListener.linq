<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Queries\test\Common.linq</Reference>
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	int port = 13000;
	var localAddr = IPAddress.Parse("127.0.0.1");
	var server = new TcpListener(localAddr, port);

	server.Start();

	Console.WriteLine("Waiting for a connection...");

	while (true)
	{
		var client = server.AcceptTcpClient();
		Console.WriteLine("Connected!");

		var stream = client.GetStream();
		byte[] buffer = new byte[256];
		int i;
		while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
		{
			string data = Encoding.ASCII.GetString(buffer, 0, i);
			Console.WriteLine("Received: {0}", data);

			data = "Server received your message!";
			buffer = Encoding.ASCII.GetBytes(data);
			stream.Write(buffer, 0, buffer.Length);
			Console.WriteLine("Sent: {0}", data);
		}

		client.Close();
	}
}

