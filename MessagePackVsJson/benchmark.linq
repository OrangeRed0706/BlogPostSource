<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Queries\test\Common.linq</Reference>
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <NuGetReference>MessagePack</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>MessagePack</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
</Query>

void Main()
{
	var summary = BenchmarkRunner.Run<Measure>();
}

public class Measure
{
	private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	public object[] fluentbitPayload;

	[GlobalSetup]
	public void Setup()
	{
		var logContent = new Dictionary<string, string>
		{
			{ "message", "2023-06-27 03:21:03.3619 | INFO | 123456789054321 | 8 | http://127.0.0.1/hello-world-benchmark-test/| 123456789054321 | Microsoft.AspNetCore.Hosting.Diagnostics | Request finished HTTP/1.1 GET http://127.0.0.1/hello-world-benchmark-test - - - 200 - - 0.2535ms" },
			{ "service_name", "TestClass" },
			{ "provisioned_service_name", "provisioned_service_name"}
		};

		fluentbitPayload = new object[3];
		fluentbitPayload[0] = "_tag";
		fluentbitPayload[1] = (DateTime.UtcNow - _unixEpoch).TotalSeconds;
		fluentbitPayload[2] = logContent;
	}

	[Benchmark]
	public byte[] MessagePack_Serialize_Bytes()
	{
		byte[] result = MessagePackSerializer.Serialize(fluentbitPayload);
		return result;
	}

	[Benchmark]
	public byte[] jsonNet_Serialize()
	{

		string jsonString = JsonConvert.SerializeObject(fluentbitPayload);
		byte[] byteResult = Encoding.UTF8.GetBytes(jsonString);
		return byteResult;
	}

	[Benchmark]
	public byte[] SystemTextJson_Serialize()
	{
		byte[] result = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(fluentbitPayload);
		return result;
	}
}
