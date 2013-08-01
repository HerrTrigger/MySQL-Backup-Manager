﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySQLBackupLibrary;
using System.IO;
using System.Xml;

namespace MySQLBackupLibraryTest
{
    [TestClass]
    public class MySQLBackupLibraryUnitTest
    {
        [TestMethod]
        public void IsConfigurationFileCreatedAtCorrectLocationTest()
        {
            Library lib = new Library();
            Assert.IsTrue(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MySQLBackup\Configuration\Configuration.xml"));
            lib = null;
        }

        [TestMethod]
        public void IsDatabasesFileCreatedAtCorrectLocationTest()
        {
            Library lib = new Library();
            Assert.IsTrue(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MySQLBackup\Configuration\Databases.xml"));
            lib = null;
        }

        [TestMethod]
        public void ModifyBackupLocationTest()
        {
            Library lib = new Library();
            lib.ChangeBackupLocation(@"C:\MyTestBackupLocation");

            XmlDocument document = new XmlDocument();
            document.Load(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MySQLBackup\Configuration\Configuration.xml");
            XmlNode backupLocationNode = document.SelectSingleNode("Configuration/BackupLocation");

            Assert.AreEqual(@"C:\MyTestBackupLocation\", backupLocationNode.InnerText);
            lib.ChangeBackupLocation(@"C:\ProgramData\MySQLBackup\Backup\");
            lib = null;
        }

        [TestMethod]
        public void ModifyDeleteBackupsOlderThanDaysTest()
        {
            Library lib = new Library();
            lib.ChangeDeleteBackupsOlderThanDays(14);

            XmlDocument document = new XmlDocument();
            document.Load(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MySQLBackup\Configuration\Configuration.xml");
            XmlNode deleteBackupsOlderThanNode = document.SelectSingleNode("Configuration/DeleteBackupsOlderThan");

            Assert.AreEqual("14", deleteBackupsOlderThanNode.InnerText);
            lib.ChangeDeleteBackupsOlderThanDays(7);
            lib = null;
        }

        [TestMethod]
        public void RetrieveBackupLocationTest()
        {
            Library lib = new Library();
            string backupLocation = lib.GetBackupLocation();

            Assert.AreEqual(@"C:\ProgramData\MySQLBackup\Backup\", backupLocation);
            lib = null;
        }

        [TestMethod]
        public void RetrieveDeleteBackupOlderThanDaysTest()
        {
            Library lib = new Library();
            int days = lib.GetDeleteBackupsOlderThanDays();

            Assert.AreEqual(7, days);
            lib = null;
        }
    }
}