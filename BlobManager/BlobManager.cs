using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobManager
{
    public class BlobManager : IBlobManager
    {
        #region variables
        private string connectionString = null;
        #endregion

        #region storage management
        private CloudStorageAccount storageAccount = null;
        private CloudBlobClient blobClient = null;
        #endregion

        public BlobManager() { }

        public BlobManager(string _connectionString)
        {
            #region preconditions
            if (string.IsNullOrEmpty(_connectionString))
                throw new Exception("Missing mandatory parameters args");
            #endregion

            #region Manage variables
            connectionString = _connectionString;
            #endregion

            #region manage table
            storageAccount = CloudStorageAccount.Parse(connectionString);
            blobClient = storageAccount.CreateCloudBlobClient();
            #endregion
        }

        public bool CreateContainer(string name)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(name);
                blobContainer.Create();
                blobContainer.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateContainerAsync(string name)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(name);
                await blobContainer.CreateAsync();
                await blobContainer.SetPermissionsAsync(
                    new BlobContainerPermissions{PublicAccess = BlobContainerPublicAccessType.Blob}
                );

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool GetOrCreateContainer(string name)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(name);
                blobContainer.CreateIfNotExists();
                blobContainer.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public async Task<bool> GetOrCreateContainerAsync(string name)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(name);
                await blobContainer.CreateIfNotExistsAsync();
                blobContainer.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool DeleteContainer(string name)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(name);
                blobContainer.DeleteIfExists();
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteContainerAsync(string name)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(name);
                await blobContainer.DeleteIfExistsAsync();
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public string UploadBlob(string containerName, string blobName, Stream stream)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobName);
                //cloudBlockBlob.Properties.ContentType = contentToUpload.ContentType;
                cloudBlockBlob.UploadFromStream(stream);
                return cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }

        public async Task<string> UploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobName);
                //cloudBlockBlob.Properties.ContentType = contentToUpload.ContentType;
                await cloudBlockBlob.UploadFromStreamAsync(stream);
                return cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }

        public async Task<string> DoUploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                await blobContainer.CreateIfNotExistsAsync();
                blobContainer.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobName);
                //cloudBlockBlob.Properties.ContentType = contentToUpload.ContentType;
                await cloudBlockBlob.UploadFromStreamAsync(stream);
                return cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }

        public string DoUploadBlob(string containerName, string blobName, Stream stream)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                blobContainer.CreateIfNotExists();
                blobContainer.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobName);
                //cloudBlockBlob.Properties.ContentType = contentToUpload.ContentType;
                cloudBlockBlob.UploadFromStream(stream);
                return cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }

        public bool DownloadBlob(string containerName, string blobName, string target)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobName);
                if (cloudBlockBlob == null)
                    throw new Exception("Cannot retrieve blob");

                //await cloudBlockBlob.DownloadToFileAsync(target,FileMode.Create);
                cloudBlockBlob.DownloadToFile(target, FileMode.Create);

                return true;

            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DownloadBlobAsync(string containerName, string blobName,string target)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobName);
                if (cloudBlockBlob == null)
                    throw new Exception("Cannot retrieve blob");

                cloudBlockBlob.FetchAttributes();

                //await cloudBlockBlob.DownloadToFileAsync(target,FileMode.Create);
                await cloudBlockBlob.DownloadToFileAsync(target, FileMode.Create);

                using (var fs = new FileStream(target, FileMode.Create))
                {
                    await cloudBlockBlob.DownloadToStreamAsync(fs);
                }

                return true;

            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public List<string> listBlobs(string containerName)
        {
            try
            {
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                if (!blobContainer.Exists())
                    return null;

                var list=blobContainer.ListBlobs();
                if (list == null)
                    return null;

                List<string> blobs = new List<string>();
                foreach (var item in list)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        blobs.Add(blob.Name);
                    }
                    else if (item.GetType() == typeof(CloudAppendBlob))
                    {
                        CloudAppendBlob blob = (CloudAppendBlob)item;
                        blobs.Add(blob.Name);
                    }
                    
                }

                if (blobs.Count == 0)
                    return null;

                return blobs;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
        }
    }
}
