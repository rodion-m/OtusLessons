using System.Buffers;

namespace _19_GC.ArrayPoolExamples;


public static class DownloaderWithPooling
{
    public static async Task DownloadFileAsync(string uri, string outputPath)
    {
        var bufferPool = ArrayPool<byte>.Shared;
        var buffer = bufferPool.Rent(8192 * 2);
        try
        {
            await using var fileStream = File.OpenWrite(outputPath);
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(
                uri, HttpCompletionOption.ResponseHeadersRead);
            await using var contentStream = await response.Content.ReadAsStreamAsync();

            while (true)
            {
                int bytesRead = await contentStream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                if (bytesRead == 0)
                {
                    break;
                }
                Console.WriteLine(bytesRead);

                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
            }
        }
        finally
        {
            bufferPool.Return(buffer);
        }
    }    
}
