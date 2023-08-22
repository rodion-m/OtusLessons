namespace _26_SynchonizationPrimitives;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class DownloaderUsingSemaphoreSlim
{
    static async Task Main(string[] args)
    {
        List<string> urls = new List<string>
        {
            "https://example.com/file1.zip",
            "https://example.com/file2.zip",
            "https://example.com/file2.zip",
            "https://example.com/file2.zip",
            "https://example.com/file2.zip",
            // Add more URLs as needed
        };

        // Limit the number of simultaneous downloads to 3
        //20 программных ядер, 30 смыслов потоков, 100 тасок
        SemaphoreSlim semaphore = new SemaphoreSlim(30);
        //AsyncLock - более удобный вариант из библиотеки Nito.AsyncEx

        List<Task> downloadTasks = new List<Task>();

        foreach (string url in urls)
        {
            var task = DownloadFileAsync(url, semaphore);
            downloadTasks.Add(task);
        }

        await Task.WhenAll(downloadTasks);
    }

    static async Task DownloadFileAsync(string url, SemaphoreSlim semaphore)
    {
        // Wait for an available slot
        await semaphore.WaitAsync();

        try
        {
            Console.WriteLine($"Starting download: {url}");

            using HttpClient httpClient = new HttpClient();
            byte[] content = await httpClient.GetByteArrayAsync(url);

            Console.WriteLine($"Finished downloading: {url}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while downloading {url}: {ex.Message}");
        }
        finally
        {
            // Release the slot for other downloads
            semaphore.Release();
        }
    }
}