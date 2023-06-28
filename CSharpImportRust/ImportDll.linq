<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\LINQPad Queries\test\Common.linq</Reference>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

void Main()
{
	uint[] arr1 = { 64, 34, 25, 12, 22, 11, 90, 21, 31, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 22, 33, 44, 55, 66, 77, 88, 99, 21, 24, 51, 63, 32, 75, 86 };
	uint[] arr2 = { 64, 34, 25, 12, 22, 11, 90, 21, 31, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 22, 33, 44, 55, 66, 77, 88, 99, 21, 24, 51, 63, 32, 75, 86 };
    var count = 10000;

    Stopwatch stopwatch = new Stopwatch();
    // 使用 Rust 實現的氣泡排序法
    stopwatch.Start();
    for (int i = 0; i < count; i++)
    {
        bubble_sort(arr1, (uint)arr1.Length);
    }
    stopwatch.Stop();
    Console.WriteLine("Sorted array using Rust:");
    foreach (int num in arr1)
    {
        Console.Write(num + " ");
    }
    Console.WriteLine();
    Console.WriteLine("Time taken using Rust: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

    Console.WriteLine();

    // 使用 C# 實現的氣泡排序法
    stopwatch.Reset();
    stopwatch.Start();
    for (int i = 0; i < count; i++)
    {
        BubbleSort(arr2);
    }
    stopwatch.Stop();
    Console.WriteLine("Sorted array using C#:");
    foreach (int num in arr2)
    {
        Console.Write(num + " ");
    }
    Console.WriteLine();
    Console.WriteLine("Time taken using C#: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
}

[DllImport(@"I:\LynnGit\BlogPostSource\CSharpImportRust\bubble_sort.dll")]
private static extern void bubble_sort(uint[] arr, uint len);

static void BubbleSort(uint[] arr)
{
	int n = arr.Length;
	for (int i = 0; i < n - 1; i++)
	{
		for (int j = 0; j < n - i - 1; j++)
		{
			if (arr[j] > arr[j + 1])
			{
				var temp = arr[j];
				arr[j] = arr[j + 1];
				arr[j + 1] = temp;
			}
		}
	}
}
