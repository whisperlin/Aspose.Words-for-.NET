// Copyright (c) 2001-2017 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.IO;
using Aspose.Words;
using NUnit.Framework;
using Environment = Android.OS.Environment;

namespace Xamarin.Android
{
    /// <summary>
    /// Provides common infrastructure for all API examples that are implemented as unit tests.
    /// </summary>
    public class ApiExampleBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //ExecuteCommandSync();

            SetUnlimitedLicense();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //Directory.Delete(MyDir, true);
        }

        internal static void SetUnlimitedLicense()
        {
            if (File.Exists(TestLicenseFileName))
            {
                // This shows how to use an Aspose.Words license when you have purchased one.
                // You don't have to specify full path as shown here. You can specify just the 
                // file name if you copy the license file into the same folder as your application
                // binaries or you add the license to your project as an embedded resource.
                License license = new License();
                license.SetLicense(TestLicenseFileName);
            }
        }

        internal static void RemoveLicense()
        {
            License license = new License();
            license.SetLicense("");
        }

        /// <summary>
        /// Gets the path to the documents used by the code examples. Ends with a back slash.
        /// </summary>
        internal static String MyDir
        {
            get { return gMyDir; }
        }

        /// <summary>
        /// Gets the path to the images used by the code examples. Ends with a back slash.
        /// </summary>
        internal static String ImageDir
        {
            get { return gImageDir; }
        }

        /// <summary>
        /// Gets the path of the demo database. Ends with a back slash.
        /// </summary>
        internal static String DatabaseDir
        {
            get { return gDatabaseDir; }
        }

        static ApiExampleBase()
        {
            gMyDir = Environment.ExternalStorageDirectory.AbsolutePath + "/Data/";
            gImageDir = Environment.ExternalStorageDirectory.AbsolutePath + "/Data/Images/";
            gDatabaseDir = Environment.ExternalStorageDirectory.AbsolutePath + "/Data/Database/";
        }

        private static readonly String gMyDir;
        private static readonly String gImageDir;
        private static readonly String gDatabaseDir;

        internal static readonly string TestLicenseFileName = Environment.ExternalStorageDirectory.AbsolutePath + "/Data/License/Aspose.Total.lic";

        void ExecuteCommandSync()
        {
            string bat = @"X:\Test.bat";

            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.CreateNoWindow = true;
                procInfo.FileName = @"cmd.exe";
                procInfo.Verb = "runas";
                procInfo.Arguments = "/C " + bat;
                Process.Start(procInfo);  //Start that process.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}