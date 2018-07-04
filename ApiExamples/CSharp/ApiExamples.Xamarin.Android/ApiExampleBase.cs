// Copyright (c) 2001-2017 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS.Storage;
using Android.Util;
using Aspose.Words;
using Java.Lang;
using Java.Lang.Reflect;
using NUnit.Framework;
using Exception = System.Exception;
using String = System.String;

namespace ApiExamples
{
    /// <summary>
    /// Provides common infrastructure for all API examples that are implemented as unit tests.
    /// </summary>
    public class ApiExampleBase
    {
        private readonly string mSdCardPath = GetSdCardPath();
        private static readonly string mExternalAppPath = GetExternalAppPath();
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            CopyData();

            SetUnlimitedLicense();

            if (Directory.Exists(ArtifactsDir))
            {
                try
                {
                    Directory.Delete(ArtifactsDir, true);
                    Directory.CreateDirectory(ArtifactsDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(ArtifactsDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            try
            {
                Directory.Delete(ArtifactsDir, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        internal static String ArtifactsDir
        {
            get { return gArtifactsDir; }
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

        /// <summary>
        /// Gets the path to the documents used by the code examples. Ends with a back slash.
        /// </summary>
        internal static String GoldsDir
        {
            get { return gGoldsDir; }
        }

        static Android.OS.Storage.StorageManager GetStorageManager()
        {
            return (StorageManager)Application.Context.ApplicationContext.GetSystemService(Context.StorageService);
        }

        static ApiExampleBase()
        {
            GetExternalSdCardPath();

            List<String> allPaths = new List<String>();
            try
            {
                Class storageVolumeClass = Class.ForName("android.os.storage.StorageVolume");
                Method getVolumeList = GetStorageManager().Class.GetMethod("getVolumeList");
                Method getPath = storageVolumeClass.GetMethod("getPath");
                Method getState = storageVolumeClass.GetMethod("getState");
                Java.Lang.Object getVolumeResult = getVolumeList.Invoke(GetStorageManager());
                int length = Java.Lang.Reflect.Array.GetLength(getVolumeResult);

                for (int i = 0; i < length; i++)
                {
                    Java.Lang.Object storageVolumeElem = Java.Lang.Reflect.Array.Get(getVolumeResult, i);
                    String mountStatus = (String) getState.Invoke(storageVolumeElem);
                    if (mountStatus != null && mountStatus.Equals("mounted"))
                    {
                        String path = (String) getPath.Invoke(storageVolumeElem);
                        if (path != null)
                        {
                            allPaths.Add(path);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var eStackTrace = e.StackTrace;
            }

            gArtifactsDir = Path.Combine(mExternalAppPath, "Data/Artifacts/");
            gMyDir = Path.Combine(mExternalAppPath, "Data/");
            gImageDir = Path.Combine(mExternalAppPath, "Data/Images/");
            gDatabaseDir = Path.Combine(mExternalAppPath, "Data/Database/");
            gGoldsDir = Path.Combine(mExternalAppPath, "Data/Golds/");
        }

        private static readonly String gArtifactsDir;
        private static readonly String gMyDir;
        private static readonly String gImageDir;
        private static readonly String gDatabaseDir;
        private static readonly String gGoldsDir;

        internal static readonly string TestLicenseFileName = Path.Combine(mExternalAppPath, "Data/License/Aspose.Total.lic");

        public void CopyData()
        {
            string sdCardDataDir = "/mnt/media_rw/1018-1B1C/Data/";
            string dataDestinationPath = mExternalAppPath + "Data/";

            if (Directory.Exists(dataDestinationPath))
            {
                Directory.Delete(dataDestinationPath, true);
                Directory.CreateDirectory(dataDestinationPath);
            }
            else
            {
                Directory.CreateDirectory(dataDestinationPath);
            }

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sdCardDataDir, "*",
                SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sdCardDataDir, dataDestinationPath));

                string test = ExtractString(@"\w+$", dirPath);

                string destinationPath = dataDestinationPath + test;

                foreach (string newPath in Directory.GetFiles(dirPath, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(dirPath, destinationPath), true);
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sdCardDataDir, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sdCardDataDir, dataDestinationPath), true);
        }

        /// <summary>
        /// Extended SD Card path location for KitKat (Android 19 / 4.4) and upwards.
        /// Must be called only on devices >= KitKat or it'll crash, since some of these OS/API calls were 
        /// only introduced in Android SDK level 19.
        /// </summary>
        /// <returns></returns>
        internal static string GetSdCardPath()
        {
            string appExternalSdPath = mExternalAppPath;
            string externalSdPath = ExtractString(@"^[/]storage[/].+?[/]", appExternalSdPath);

            return externalSdPath;
        }

        /// <summary>
        /// Extended SD Card path location for KitKat (Android 19 / 4.4) and upwards.
        /// Must be called only on devices >= KitKat or it'll crash, since some of these OS/API calls were 
        /// only introduced in Android SDK level 19.
        /// </summary>
        /// <returns></returns>
        public static string GetExternalAppPath()
        {
            Java.IO.File[] externalFilesDirs = Application.Context.GetExternalFilesDirs(null);

            //if (Build.VERSION.SdkInt >= (BuildVersionCodes) 21)
            //{
            //    Java.IO.File[] ExternalStorage =
            //        ContextCompat.GetExternalFilesDirs(Application.Context.ApplicationContext, null);
            //    bool emulated = true;
            //    for (int i = 0; i < ExternalStorage.Length; i++)
            //    {
            //        emulated = Android.OS.Environment.InvokeIsExternalStorageEmulated(ExternalStorage[i]);
            //        if (!emulated)
            //        {
            //            string mySDPath = ExternalStorage[i].AbsolutePath;
            //            break;
            //        }
            //    }
            //}

            // if there are any items, the FIRST will always be INTERNAL storage (not the SD cardO). 
            // Any subsequent items will be removable storage which is considered 'permanently' mounted 
            // (like inside the case, in an SD card slot). 
            // "Transient" storage like external USB drives is ignored, you won't see it in these results.
            if (externalFilesDirs.Any())
            {
                // we only want the external drive, otherwise nothing!
                string appExternalSdPath = externalFilesDirs.Length > 1
                    ? externalFilesDirs[1].AbsolutePath + Path.DirectorySeparatorChar
                    : externalFilesDirs[0].AbsolutePath + Path.DirectorySeparatorChar;

                // note that in the case of an SD card, ONLY the path it returns is writeable. You can 
                // drop back to the "root" as we did with the internal one above, but that's readonly.
                return appExternalSdPath;
            }

            return string.Empty;
        }

        internal static string ExtractString(string regex, string searchString)
        {
            Regex pattern = new Regex(regex);
            Match match = pattern.Match(searchString);

            return match.Value;
        }

        /// <summary>
        /// Remember to turn on the READ_EXTERNAL_STORAGE permission, or this just comes back empty
        /// Tries to establish whether there's an external SD card present. It's
        /// a little hacky; reads /proc/mounts and looks for /storage/ references,
        /// and iterates over those looking for things like 'ext' and 'sdcard' in
        /// the same line, e.g. /storage/extSdCard or /storage/externalSd or similar.
        /// For the moment, the existence of 'ext' as part of the path (not the file system type) 
        /// is a crucial flag. If it doesn't see that, it'll bail out and assume there 
        /// isn't one (even if there is and it's  named something else). 
        /// We may have to build a list over time. 
        /// Returns: The root of the mounted directory (with no trailing '/', or empty string if there isn't one)
        /// </summary>
        /// <returns>The root of the mounted directory (with NO trailing '/'), or empty string if there isn't one)</returns>
        public static string GetExternalSdCardPath()
        {
            string procMounts = ReadProcMounts();
            string sdCardEntry = ParseProcMounts(procMounts);

            // note that IsWritable may fail if the disk is mounted elsewhere, e.g. MTP to PC
            if (!string.IsNullOrWhiteSpace(sdCardEntry))
            {
                return sdCardEntry;
            }
            return string.Empty;
        }

        /// <summary>
        /// Just returns the contents of /proc/mounts as a string.
        /// Note that you MAY need to wrap this call up in a try/catch if you
        /// anticipate permissions issues, but generally just reading from 
        /// this file is OK
        /// </summary>
        /// <returns></returns>
        public static string GetProcMountsContents()
        {
            return ReadProcMounts();
        }

        /// <summary>
        /// This is an expensive operation to call, because it physically tries to write to the media.
        /// Remember to turn on the WRITE_EXTERNAL_STORAGE permission, or this will always return false.
        /// </summary>
        /// <param name="pathToTest">The root path of the alleged SD card (e.g. /storage/externalSd), 
        /// or anywhere else you want to test (WITHOUT the trailing '/'). If you try to write to somewhere 
        /// you're not allowed to, you may get eaten by a dragon.</param>
        /// <returns>True if it could write to it, false if not</returns>
        public static bool IsWritable(string pathToTest)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(pathToTest))
            {
                const string someTestText = "some test text";
                try
                {
                    string testFile = string.Format("{0}/{1}.txt", pathToTest, Guid.NewGuid());
                    Log.Info("ExternalSDStorageHelper", "Trying to write some test data to {0}", testFile);
                    System.IO.File.WriteAllText(testFile, someTestText);
                    Log.Info("ExternalSDStorageHelper", "Success writing some test data to {0}!", testFile);
                    System.IO.File.Delete(testFile);
                    Log.Info("ExternalSDStorageHelper", "Cleaned up test data file {0}", testFile);
                    result = true;
                }
                catch (Exception ex) // shut up about it and move on, we obviously can't have it, so it's dead to us, we can't use it.
                {
                    Log.Error("ExternalSDStorageHelper", string.Format("Exception: {0}\r\nMessage: {1}\r\nStack Trace: {2}", ex, ex.Message, ex.StackTrace));
                }
            }
            return result;
        }

        /// <summary>
        /// example entries from /proc/mounts on a Samsung Galaxy S2 looks like:
        /// dev/block/dm-1 /mnt/asec/com.touchtype.swiftkey-2 ext4 ro,dirsync,nosuid,nodev,blah
        /// dev/block/dm-2 /mnt/asec/com.mobisystems.editor.office_registered-2 ext4 ro,dirsync,nosuid, blah
        /// dev/block/vold/259:3 /storage/sdcard0 vfat rw,dirsync, blah (this is NOT an external SD card)
        /// dev/block/vold/179:9 /storage/extSdCard vfat rw,dirsync,nosuid, blah (this IS an external SD card)
        /// </summary>
        /// <param name="procMounts"></param>
        /// <returns></returns>
        private static string ParseProcMounts(string procMounts)
        {
            string sdCardEntry = string.Empty;
            if (!string.IsNullOrWhiteSpace(procMounts))
            {
                var candidateProcMountEntries = procMounts.Split('\n', '\r').ToList();
                candidateProcMountEntries.RemoveAll(s => s.IndexOf("storage", StringComparison.OrdinalIgnoreCase) < 0);
                var bestCandidate = candidateProcMountEntries
                  .FirstOrDefault(s => s.IndexOf("ext", StringComparison.OrdinalIgnoreCase) >= 0
                                       && s.IndexOf("sd", StringComparison.OrdinalIgnoreCase) >= 0
                                       && s.IndexOf("fat", StringComparison.OrdinalIgnoreCase) >= 0); // you can have things like fat, vfat, exfat, texfat, etc.

                // e.g. /dev/block/vold/179:9 /storage/extSdCard vfat rw,dirsync,nosuid, blah
                if (!string.IsNullOrWhiteSpace(bestCandidate))
                {
                    var sdCardEntries = bestCandidate.Split(' ');
                    sdCardEntry = sdCardEntries.FirstOrDefault(s => s.IndexOf("/storage/", System.StringComparison.OrdinalIgnoreCase) >= 0);
                    return !string.IsNullOrWhiteSpace(sdCardEntry) ? string.Format("{0}", sdCardEntry) : string.Empty;
                }
            }
            return sdCardEntry;
        }

        /// <summary>
        /// This doesn't require you to add any permissions in your Manifest.xml, but you'll
        /// need to add READ_EXTERNAL_STORAGE at the very least to be able to determine if the external
        /// SD card is available and usable.
        /// </summary>
        /// <returns></returns>
        private static string ReadProcMounts()
        {
            Log.Info("ExternalSDStorageHelper", "Attempting to read '/proc/mounts' to see if there's an external SD card reference");
            try
            {
                string contents = System.IO.File.ReadAllText("/proc/mounts");
                return contents;
            }
            catch (Exception ex) // shut up about it and move on, we obviously can't have it, we can't use it.
            {
                Log.Error("ExternalSDStorageHelper", string.Format("Exception: {0}\r\nMessage: {1}\r\nStack Trace: {2}", ex, ex.Message, ex.StackTrace));
            }

            return string.Empty; // expect to fail by default
        }
    }
}