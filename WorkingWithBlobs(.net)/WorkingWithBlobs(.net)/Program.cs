using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
public class Program
{
    private const string blobServiceEndpoint = "https://storageaccount589.blob.core.windows.net/";
    private const string storageAccountName = "storageaccount589";
    private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccount589;AccountKey=zAbki9/Q5BusuSBi3w1yHJc9ujyuAPxHbNAUGexscUoE1LiIZAVZ4pVOrYYf70HTiADjb5PbAUev/EED4bzh0A==;EndpointSuffix=core.windows.net";
    public static async Task Main(string[] args)
    {
        BlobServiceClient _client = new BlobServiceClient(connectionString);
        Console.WriteLine("Connected to storage account \n");
        await Console.Out.WriteLineAsync($"Account name:\t{storageAccountName}");
        // Create a BlobServiceClient object which will be used to create a container client
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        //Create a unique name for the container
        string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

        // Create the container and return a container client object
        BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        // Create a local file in the ./data/ directory for uploading and downloading
        string localPath = "./data/";
        string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);

        // Write text to the file
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");

        // Get a reference to a blob
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

        // Upload data from the local file
        await blobClient.UploadAsync(localFilePath, true);
        Console.WriteLine("Listing blobs...");

        // List all blobs in the container
        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            Console.WriteLine("\t" + blobItem.Name);
        }
        // Download the blob to a local file
        // Append the string "DOWNLOADED" before the .txt extension 
        // so you can compare the files in the data directory
        string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

        Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

        // Download the blob's contents and save it to a file
        await blobClient.DownloadToAsync(downloadFilePath);
        // Clean up
        Console.Write("Press any key to begin clean up");
        Console.ReadLine();

        Console.WriteLine("Deleting blob container...");
        await containerClient.DeleteAsync();

        Console.WriteLine("Deleting the local source and downloaded files...");
        File.Delete(localFilePath);
        File.Delete(downloadFilePath);

        Console.WriteLine("Done");
    }
}
