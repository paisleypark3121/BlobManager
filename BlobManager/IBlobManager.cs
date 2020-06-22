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
        //bool uploadBlob(string blob,string fileName);
        Task<string> UploadBlobAsync(string containerName, string blobName, Stream stream);
        /// <summary>
        /// it forces the creation of the container if not exists
        /// </summary>
        Task<string> DoUploadBlobAsync(string containerName, string blobName, Stream stream);
    }
}
