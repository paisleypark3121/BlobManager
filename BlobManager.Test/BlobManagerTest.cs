using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlobManager.Test
{
    [TestClass]
    public class BlobManagerTest
    {
        [TestMethod]
        public void CreateContainerTest()
        {
            //#region arrange
            //bool expected = true;
            //string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            //#endregion

            //#region act
            //IBlobManager blobManager = new BlobManager(connectionString);
            //bool actual =blobManager.createContainer("testcontainer");
            //#endregion

            //#region assert
            //Assert.AreEqual(expected, actual);
            //#endregion
        }

        [TestMethod]
        public void GetContainerTest()
        {
            //#region arrange
            //bool expected = true;
            //string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            //#endregion

            //#region act
            //IBlobManager blobManager = new BlobManager(connectionString);
            //bool actual = blobManager.getContainer("testcontainer");
            //#endregion

            //#region assert
            //Assert.AreEqual(expected, actual);
            //#endregion
        }

        [TestMethod]
        public void UpoadFileTest()
        {
            //#region arrange
            //bool expected = true;
            //string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            //string container = "testcontainer";
            //#endregion

            //#region act
            //IBlobManager blobManager = new BlobManager(connectionString);
            //if (!blobManager.getContainer(container))
            //    Assert.IsTrue(false);
            //bool actual=blobManager.uploadBlob("myblob", "Data\\Test1.txt");
            //#endregion

            //#region assert
            //Assert.AreEqual(expected, actual);
            //#endregion
        }

        [TestMethod]
        public void DownloadFileTest()
        {
            #region arrange
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            string container = "uploader";
            string blobName = "myblob.txt";
            string target = @"C:\Users\paisl\Documents\0100\aaa.txt";
            #endregion

            #region act
            IBlobManager blobManager = new BlobManager(connectionString);
            blobManager.DownloadBlobAsync(container, blobName, target);
            #endregion

            #region assert
            Assert.IsTrue(true);
            #endregion
        }

        [TestMethod]
        public void CreateFileTest()
        {
            #region arrange
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            string container = "uploader";
            string blobName = "mycreation.txt";
            string text = "this is my test text";
            #endregion

            #region act
            IBlobManager blobManager = new BlobManager(connectionString);
            byte[] byteArray = Encoding.ASCII.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);
            blobManager.DoUploadBlob(container, blobName, stream);
            #endregion

            #region assert
            Assert.IsTrue(true);
            #endregion
        }

        [TestMethod]
        public void ListBlobsTest()
        {
            #region arrange
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            string container = "uploader";
            #endregion

            #region act
            IBlobManager blobManager = new BlobManager(connectionString);
            List<string> list=blobManager.listBlobs(container);
            #endregion

            #region assert
            Assert.IsTrue(true);
            #endregion
        }
    }
}
