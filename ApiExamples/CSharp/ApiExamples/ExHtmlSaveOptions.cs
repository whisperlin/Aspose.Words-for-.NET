// Copyright (c) 2001-2017 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System.IO;
using Aspose.Words;
using NUnit.Framework;
using Aspose.Words.Saving;

namespace ApiExamples
{
    [TestFixture]
    internal class ExHtmlSaveOptions : ApiExampleBase
    {
        #region PageMargins

        //For assert this test you need to open HTML docs and they shouldn't have negative left margins
        [Test]
        [TestCase(SaveFormat.Html)]
        [TestCase(SaveFormat.Mhtml)]
        [TestCase(SaveFormat.Epub)]
        public void ExportPageMargins(SaveFormat saveFormat)
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ExportPageMargins.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.SaveFormat = saveFormat;
            saveOptions.ExportPageMargins = true;

            doc.Save(ArtifactsDir +"HtmlSaveOptions.ExportPageMargins" + FileFormatUtil.SaveFormatToExtension(saveFormat), saveOptions);
        }

        #endregion

        #region HtmlOfficeMathOutputMode

        [Test]
        [TestCase(SaveFormat.Html, HtmlOfficeMathOutputMode.Image)]
        [TestCase(SaveFormat.Mhtml, HtmlOfficeMathOutputMode.MathML)]
        [TestCase(SaveFormat.Epub, HtmlOfficeMathOutputMode.Text)]
        public void ExportOfficeMath(SaveFormat saveFormat, HtmlOfficeMathOutputMode outputMode)
        {
            Document doc = new Document(MyDir + "OfficeMath.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.OfficeMathOutputMode = outputMode;

            doc.Save(ArtifactsDir + "HtmlSaveOptions.ExportToHtmlUsingImage" + FileFormatUtil.SaveFormatToExtension(saveFormat), saveOptions);
        }

        #endregion

        #region ExportTextBoxAsSvg

        [Test]
        [TestCase(SaveFormat.Html, true, Description = "TextBox as svg (html)")]
        [TestCase(SaveFormat.Epub, true, Description = "TextBox as svg (epub)")]
        [TestCase(SaveFormat.Mhtml, false, Description = "TextBox as img (mhtml)")]
        public void ExportTextBoxAsSvg(SaveFormat saveFormat, bool textBoxAsSvg)
        {
            string[] dirFiles;

            Document doc = new Document(MyDir + "HtmlSaveOptions.ExportTextBoxAsSvg.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.ExportTextBoxAsSvg = textBoxAsSvg;

            doc.Save(ArtifactsDir + "HtmlSaveOptions.ExportTextBoxAsSvg" + FileFormatUtil.SaveFormatToExtension(saveFormat), saveOptions);

            switch (saveFormat)
            {
                case SaveFormat.Html:

                    dirFiles = Directory.GetFiles(ArtifactsDir, "HtmlSaveOptions.ExportTextBoxAsSvg.001.png", SearchOption.AllDirectories);
                    Assert.IsEmpty(dirFiles);
                    return;

                case SaveFormat.Epub:

                    dirFiles = Directory.GetFiles(ArtifactsDir, "HtmlSaveOptions.ExportTextBoxAsSvg.001.png", SearchOption.AllDirectories);
                    Assert.IsEmpty(dirFiles);
                    return;

                case SaveFormat.Mhtml:

                    dirFiles = Directory.GetFiles(ArtifactsDir, "HtmlSaveOptions.ExportTextBoxAsSvg.001.png", SearchOption.AllDirectories);
                    Assert.IsNotEmpty(dirFiles);
                    return;
            }
        }

        #endregion

        [Test]
        public void ControlListLabelsExportToHtml()
        {
            Document doc = new Document(MyDir + "Lists.PrintOutAllLists.doc");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions(SaveFormat.Html);

            // This option uses <ul> and <ol> tags are used for list label representation if it doesn't cause formatting loss, 
            // otherwise HTML <p> tag is used. This is also the default value.
            saveOptions.ExportListLabels = ExportListLabels.Auto;
            doc.Save(ArtifactsDir + "Document.ExportListLabels Auto.html", saveOptions);

            // Using this option the <p> tag is used for any list label representation.
            saveOptions.ExportListLabels = ExportListLabels.AsInlineText;
            doc.Save(ArtifactsDir + "Document.ExportListLabels InlineText.html", saveOptions);

            // The <ul> and <ol> tags are used for list label representation. Some formatting loss is possible.
            saveOptions.ExportListLabels = ExportListLabels.ByHtmlTags;
            doc.Save(ArtifactsDir + "Document.ExportListLabels HtmlTags.html", saveOptions);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ExportUrlForLinkedImage(bool export)
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ExportUrlForLinkedImage.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.ExportOriginalUrlForLinkedImages = export;

            doc.Save(ArtifactsDir + "HtmlSaveOptions.ExportUrlForLinkedImage.html", saveOptions);

            string[] dirFiles = Directory.GetFiles(ArtifactsDir, "HtmlSaveOptions.ExportUrlForLinkedImage.001.png", SearchOption.AllDirectories);

            if (dirFiles.Length == 0)
                DocumentHelper.FindTextInFile(ArtifactsDir + "HtmlSaveOptions.ExportUrlForLinkedImage.html", "<img src=\"http://www.aspose.com/images/aspose-logo.gif\"");
            else
                DocumentHelper.FindTextInFile(ArtifactsDir + "HtmlSaveOptions.ExportUrlForLinkedImage.html", "<img src=\"HtmlSaveOptions.ExportUrlForLinkedImage.001.png\"");
        }

        [Ignore("Bug, css styles starting with -aw, even if ExportRoundtripInformation is false")]
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ExportRoundtripInformation(bool valueHtml)
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ExportPageMargins.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.ExportRoundtripInformation = valueHtml;

            doc.Save(ArtifactsDir + "HtmlSaveOptions.RoundtripInformation.html");

            if (valueHtml)
                DocumentHelper.FindTextInFile(ArtifactsDir + "HtmlSaveOptions.RoundtripInformation.html", "<img src=\"HtmlSaveOptions.RoundtripInformation.003.png\" width=\"226\" height=\"132\" alt=\"\" style=\"margin-top:-53.74pt; margin-left:-26.75pt; -aw-left-pos:-26.25pt; -aw-rel-hpos:column; -aw-rel-vpos:page; -aw-top-pos:41.25pt; -aw-wrap-type:none; position:absolute\" /></span><span style=\"height:0pt; display:block; position:absolute; z-index:1\"><img src=\"HtmlSaveOptions.RoundtripInformation.002.png\" width=\"227\" height=\"132\" alt=\"\" style=\"margin-top:74.51pt; margin-left:-23pt; -aw-left-pos:-22.5pt; -aw-rel-hpos:column; -aw-rel-vpos:page; -aw-top-pos:169.5pt; -aw-wrap-type:none; position:absolute\" /></span><span style=\"height:0pt; display:block; position:absolute; z-index:2\"><img src=\"HtmlSaveOptions.RoundtripInformation.001.png\" width=\"227\" height=\"132\" alt=\"\" style=\"margin-top:199.01pt; margin-left:-23pt; -aw-left-pos:-22.5pt; -aw-rel-hpos:column; -aw-rel-vpos:page; -aw-top-pos:294pt; -aw-wrap-type:none; position:absolute\" />");
            else
                DocumentHelper.FindTextInFile(ArtifactsDir + "HtmlSaveOptions.RoundtripInformation.html", "<img src=\"HtmlSaveOptions.RoundtripInformation.003.png\" width=\"226\" height=\"132\" alt=\"\" style=\"margin-top:-53.74pt; margin-left:-26.75pt; -aw-left-pos:-26.25pt; -aw-rel-hpos:column; -aw-rel-vpos:page; -aw-top-pos:41.25pt; -aw-wrap-type:none; position:absolute\" /></span><span style=\"height:0pt; display:block; position:absolute; z-index:1\"><img src=\"HtmlSaveOptions.RoundtripInformation.002.png\" width=\"227\" height=\"132\" alt=\"\" style=\"margin-top:74.51pt; margin-left:-23pt; -aw-left-pos:-22.5pt; -aw-rel-hpos:column; -aw-rel-vpos:page; -aw-top-pos:169.5pt; -aw-wrap-type:none; position:absolute\" /></span><span style=\"height:0pt; display:block; position:absolute; z-index:2\"><img src=\"HtmlSaveOptions.RoundtripInformation.001.png\" width=\"227\" height=\"132\" alt=\"\" style=\"margin-top:199.01pt; margin-left:-23pt; -aw-left-pos:-22.5pt; -aw-rel-hpos:column; -aw-rel-vpos:page; -aw-top-pos:294pt; -aw-wrap-type:none; position:absolute\" />");
        }

        [Test]
        public void RoundtripInformationDefaulValue()
        {
            //Assert that default value is true for HTML and false for MHTML and EPUB.
            HtmlSaveOptions saveOptions = new HtmlSaveOptions(SaveFormat.Html);
            Assert.AreEqual(true, saveOptions.ExportRoundtripInformation);

            saveOptions = new HtmlSaveOptions(SaveFormat.Mhtml);
            Assert.AreEqual(false, saveOptions.ExportRoundtripInformation);

            saveOptions = new HtmlSaveOptions(SaveFormat.Epub);
            Assert.AreEqual(false, saveOptions.ExportRoundtripInformation);
        }

        [Test]
        public void ConfigForSavingExternalResources()
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ExportPageMargins.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.CssStyleSheetType = CssStyleSheetType.External;
            saveOptions.ExportFontResources = true;
            saveOptions.ResourceFolder = "Resources";
            saveOptions.ResourceFolderAlias = "https://www.aspose.com/";

            doc.Save(ArtifactsDir + "HtmlSaveOptions.ExportPageMargins.html", saveOptions);

            string[] imageFiles = Directory.GetFiles(ArtifactsDir + "Resources/", "*.png", SearchOption.AllDirectories);
            Assert.AreEqual(3, imageFiles.Length);

            string[] fontFiles = Directory.GetFiles(ArtifactsDir + "Resources/", "*.ttf", SearchOption.AllDirectories);
            Assert.AreEqual(1, fontFiles.Length);

            string[] cssFiles = Directory.GetFiles(ArtifactsDir + "Resources/", "*.css", SearchOption.AllDirectories);
            Assert.AreEqual(1, cssFiles.Length);

            DocumentHelper.FindTextInFile(ArtifactsDir + "HtmlSaveOptions.ExportPageMargins.html", "<link href=\"https://www.aspose.com/HtmlSaveOptions.ExportPageMargins.css\"");
        }

        [Test]
        public void ConvertFontsAsBase64()
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ExportPageMargins.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.CssStyleSheetType = CssStyleSheetType.External;
            saveOptions.ResourceFolder = "Resources";
            saveOptions.ExportFontResources = true;
            saveOptions.ExportFontsAsBase64 = true;
            
            doc.Save(ArtifactsDir + "HtmlSaveOptions.ExportPageMargins.html", saveOptions);
}
        [TestCase(HtmlVersion.Html5)]
        [TestCase(HtmlVersion.Xhtml)]
        public void Html5Support(HtmlVersion htmlVersion)
        {
            Document doc = new Document(MyDir + "Document.doc");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.HtmlVersion = htmlVersion;
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void ExportFonts(bool exportAsBase64)
        {
            Document doc = new Document(MyDir + "Document.doc");
            
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.ExportFontResources = true;
            saveOptions.ExportFontsAsBase64 = exportAsBase64;

            switch (exportAsBase64)
            {
                case false:

                    doc.Save(ArtifactsDir + "DocumentExportFonts 1.html", saveOptions);
                    Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir, "DocumentExportFonts 1.times.ttf", SearchOption.AllDirectories)); //Verify that the font has been added to the folder
                    break;

                case true:

                    doc.Save(ArtifactsDir + "DocumentExportFonts 2.html", saveOptions);
                    Assert.IsEmpty(Directory.GetFiles(ArtifactsDir, "DocumentExportFonts 2.times.ttf", SearchOption.AllDirectories)); //Verify that the font is not added to the folder
                    break;
            }
        }

        [Test]
        public void ResourceFolderPriority()
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ResourceFolder.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.CssStyleSheetType = CssStyleSheetType.External;
            saveOptions.ExportFontResources = true;
            saveOptions.ResourceFolder = ArtifactsDir + "Resources";
            saveOptions.ResourceFolderAlias = "http://example.com/resources";

            doc.Save(ArtifactsDir + "HtmlSaveOptions.ResourceFolder.html", saveOptions);

            string[] a = Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.001.jpeg",
                SearchOption.AllDirectories);
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.001.jpeg", SearchOption.AllDirectories));
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.002.png", SearchOption.AllDirectories));
#if __MOBILE__
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.roboto-regular.ttf", SearchOption.AllDirectories));
#else
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.calibri.ttf", SearchOption.AllDirectories));
#endif
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.css", SearchOption.AllDirectories));

        }

        [Test]
        public void ResourceFolderLowPriority()
        {
            Document doc = new Document(MyDir + "HtmlSaveOptions.ResourceFolder.docx");

            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.CssStyleSheetType = CssStyleSheetType.External;
            saveOptions.ExportFontResources = true;
            saveOptions.FontsFolder = ArtifactsDir + "Fonts";
            saveOptions.ImagesFolder = ArtifactsDir + "Images";
            saveOptions.ResourceFolder = ArtifactsDir + "Resources";
            saveOptions.ResourceFolderAlias = "http://example.com/resources";

            doc.Save(ArtifactsDir + "HtmlSaveOptions.ResourceFolder.html", saveOptions);

            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Images", "HtmlSaveOptions.ResourceFolder.001.jpeg", SearchOption.AllDirectories));
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Images", "HtmlSaveOptions.ResourceFolder.002.png", SearchOption.AllDirectories));
#if __MOBILE__
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Fonts", "HtmlSaveOptions.ResourceFolder.roboto-regular.ttf", SearchOption.AllDirectories));
#else
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Fonts", "HtmlSaveOptions.ResourceFolder.calibri.ttf", SearchOption.AllDirectories));
#endif
            Assert.IsNotEmpty(Directory.GetFiles(ArtifactsDir + "Resources", "HtmlSaveOptions.ResourceFolder.css", SearchOption.AllDirectories));
        }
    }
}
