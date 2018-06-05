// Copyright (c) 2001-2017 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Android.App;
using Aspose.Words;
using NUnit.Framework;

namespace ApiExamples
{
    /// <summary>
    /// Provides common infrastructure for all API examples that are implemented as unit tests.
    /// </summary>
    public class ApiExampleBase
    {
        private readonly String artifactsPath = MyDir + "Artifacts/";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetUnlimitedLicense();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            File.Delete(artifactsPath);
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
            gMyDir = Path.Combine(GetExternalSdCardPath(), "Data/");
            gImageDir = Path.Combine(GetExternalSdCardPath(), "Data/Images/");
            gDatabaseDir = Path.Combine(GetExternalSdCardPath(), "Data/Database/");
        }

        private static readonly String gMyDir;
        private static readonly String gImageDir;
        private static readonly String gDatabaseDir;

        internal static readonly string TestLicenseFileName = Path.Combine(GetExternalSdCardPath(), "Data/License/Aspose.Total.lic");

        /// <summary>
        /// Extended SD Card path location for KitKat (Android 19 / 4.4) and upwards.
        /// Must be called only on devices >= KitKat or it'll crash, since some of these OS/API calls were 
        /// only introduced in Android SDK level 19.
        /// </summary>
        /// <returns></returns>
        internal static string GetExternalSdCardPath()
        {
            Java.IO.File[] externalFilesDirs = Application.Context.GetExternalFilesDirs(null);
            
            // if there are any items, the FIRST will always be INTERNAL storage (not the SD cardO). 
            // Any subsequent items will be removable storage which is considered 'permanently' mounted 
            // (like inside the case, in an SD card slot). 
            // "Transient" storage like external USB drives is ignored, you won't see it in these results.
            if (externalFilesDirs.Any())
            {
                // we only want the external drive, otherwise nothing!
                string appExternalSdPath = externalFilesDirs.Length > 1 ? externalFilesDirs[1].AbsolutePath : string.Empty;

                // note that in the case of an SD card, ONLY the path it returns is writeable. You can 
                // drop back to the "root" as we did with the internal one above, but that's readonly.
                var externalSdPath = ExtractString(@"^/storage/.+?/", appExternalSdPath);
                return externalSdPath;
            }

            return string.Empty;
        }

        internal static string ExtractString(string regex, string searchString)
        {
            Regex pattern = new Regex(regex);
            Match match = pattern.Match(searchString);

            return match.Value;
        }
    }
}