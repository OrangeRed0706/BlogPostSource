<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Queries\test\Common.linq</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	const int port = 13000;
	const string localIp = "127.0.0.1";
	const int readWriteTimeout = 5000; // 設定讀寫超時為 5000 毫秒

	var localAddr = IPAddress.Parse(localIp);
	var serverEndPoint = new IPEndPoint(localAddr, port);

	using var server = new Socket(localAddr.AddressFamily, SocketType.Stream, ProtocolType.Udp);
	server.Bind(serverEndPoint);
	server.Listen(100);

	Console.WriteLine("Server started, waiting for clients...");

	while (true)
	{
		var client = await server.AcceptAsync();
		_ = HandleClientAsync(client, readWriteTimeout); // 處理客戶端連接
	}
}

async Task HandleClientAsync(Socket client, int timeout)
{
	try
	{
		Console.WriteLine("Client connected.");

		client.ReceiveTimeout = timeout;
		client.SendTimeout = timeout;

		var buffer = new byte[1024];
		while (true)
		{
			var byteCount = await client.ReceiveAsync(buffer, SocketFlags.None);
			
			// 客戶端已經關閉連接
			if (byteCount == 0)
			{
				break;
			} 
			var request = Encoding.UTF8.GetString(buffer, 0, byteCount);

			if (!IsValid(request)) // 驗證請求資料
			{
				Console.WriteLine("Invalid request: {0}", request);
				break;
			}

			Console.WriteLine("Received: {0}", request);

			var response = "Server received your message!";
			var data = Encoding.UTF8.GetBytes(response);
			await client.SendAsync(data, SocketFlags.None);
			Console.WriteLine("Sent: {0}", response);
		}

		Console.WriteLine("Client disconnected.");
	}
	catch (Exception e)
	{
		Console.WriteLine("Exception: {0}", e.ToString());
	}
	finally
	{
		client.Close();
	}
}

static bool IsValid(string request)
{
	return !string.IsNullOrEmpty(request);
}

