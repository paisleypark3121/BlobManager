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

        //public bool uploadBlob(string blob, string fileName)
        //{
        //    try
        //    {
        //        CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blob);
        //        cloudBlockBlob.UploadFromFile(fileName);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.TraceError(ex.Message);
        //        return false;
        //    }
           
            
        //}

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
    }
}
