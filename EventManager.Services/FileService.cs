using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventManager.Models;
using Microsoft.AspNetCore.Http;

namespace EventManager.Services;

public class FileService
{
    private readonly string _storageAccount = "storageeventmanager";
    private readonly string _key = "hmQ5LmdEFkdUc7AzxHJFq9AiQxWvKfanxLAEbdK0tK+UWRbO/uplQ104zzuem/WTKnu2Zi3w4ilG+AStxbUPMw==";
    private readonly BlobContainerClient _filesContainer;

    public FileService()
    {
        var credential = new StorageSharedKeyCredential(_storageAccount, _key);
        var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
        var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        _filesContainer = blobServiceClient.GetBlobContainerClient("images");
    }

    public async Task<List<BlobDto>> ListAsync()
    {
        var files = new List<BlobDto>();

        await foreach (var file in _filesContainer.GetBlobsAsync())
        {
            var uri = _filesContainer.Uri.ToString();
            var name = file.Name;
            var fullUri = $"{uri}/{name}";
            
            files.Add(new BlobDto()
            {
                Uri = fullUri,
                Name = name,
                ContentType = file.Properties.ContentType
            });
        }

        return files;
    }

    public async Task<BlobResponseDto> UploadAsync(IFormFile blob)
    {
        var response = new BlobResponseDto();
        var fileName = Guid.NewGuid().ToString();
        
        var client = _filesContainer.GetBlobClient(fileName);

        await using (var data = blob.OpenReadStream())
        {
            await client.UploadAsync(data, new BlobHttpHeaders
            {
                ContentType = blob.ContentType
            });
        }

        response.Status = $"File {blob.FileName} uploaded successfully";
        response.Error = false;
        response.Blob.Uri = client.Uri.AbsoluteUri;
        response.Blob.Name = client.Name;
        
        return response;
    }

    public async Task<BlobDto?> DownloadAsync(string blobFilename)
    {
        var file = _filesContainer.GetBlobClient(blobFilename);

        if (!await file.ExistsAsync())
        {
            return null;
        }
        
        var data = await file.OpenReadAsync();

        var content = await file.DownloadContentAsync();

        var contentType = content.Value.Details.ContentType;

        return new BlobDto
        {
            Content = data,
            Name = blobFilename,
            ContentType = contentType
        };

    }

    public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
    {
        var file = _filesContainer.GetBlobClient(blobFilename);
        
        if (!await file.ExistsAsync())
        {
            return new BlobResponseDto { Error = false, Status = $"File {blobFilename} doesn't exists"};;
        }

        await file.DeleteAsync();
        
        return new BlobResponseDto { Error = false, Status = $"File {blobFilename} has been successfully deleted"};
    }
}