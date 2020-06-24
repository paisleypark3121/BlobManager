using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobManager
{
    public interface IBlobManager
    {
        bool CreateContainer(string name);
        Task<bool> CreateContainerAsync(string name);
     
        bool GetOrCreateContainer(string name);
        Task<bool> GetOrCreateContainerAsync(string name);
        
        bool DeleteContainer(string name);
        Task<bool> DeleteContainerAsync(string name);
        
        string UploadBlob(string containerName, string blobName, Stream stream);
        Task<string> UploadBlobAsync(string containerName, string blobName, Stream stream);
        
        string DoUploadBlob(string containerName, string blobName, Stream stream);
        Task<string> DoUploadBlobAsync(string containerName, string blobName, Stream stream);
        
        bool DownloadBlob(string containerName, string blobName, string target);
        Task<bool> DownloadBlobAsync(string containerName, string blobName,string target);

        List<string> listBlobs(string containerName);
    }
}
