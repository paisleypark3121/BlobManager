using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlobManager.Test
{
    [TestClass]
    public class BlobManagerTest
    {
        [TestMethod]
        public void CreateContainerTest()
        {
            #region arrange
            bool expected = true;
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            #endregion

            #region act
            IBlobManager blobManager = new BlobManager(connectionString);
            bool actual =blobManager.createContainer("testcontainer");
            #endregion

            #region assert
            Assert.AreEqual(expected, actual);
            #endregion
        }

        [TestMethod]
        public void GetContainerTest()
        {
            #region arrange
            bool expected = true;
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            #endregion

            #region act
            IBlobManager blobManager = new BlobManager(connectionString);
            bool actual = blobManager.getContainer("testcontainer");
            #endregion

            #region assert
            Assert.AreEqual(expected, actual);
            #endregion
        }

        [TestMethod]
        public void UpoadFileTest()
        {
            #region arrange
            bool expected = true;
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountcmsen927e;AccountKey=X63IKbAfC70fS/jYBJPyyK3ofSFdUeKSyTvCxLiAEgrOe9US+ylyez8KnDIrQDxGx6M9WHY5dF5D2FrSb5mhXQ==;EndpointSuffix=core.windows.net";
            string container = "testcontainer";
            #endregion

            #region act
            IBlobManager blobManager = new BlobManager(connectionString);
            if (!blobManager.getContainer(container))
                Assert.IsTrue(false);
            bool actual=blobManager.uploadBlob("myblob", "Data\\Test1.txt");
            #endregion

            #region assert
            Assert.AreEqual(expected, actual);
            #endregion
        }
    }
}
