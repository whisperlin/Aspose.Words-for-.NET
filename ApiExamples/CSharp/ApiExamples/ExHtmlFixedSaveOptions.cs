// Copyright (c) 2001-2018 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System.Text;
using System;
using System.IO;
using Aspose.Words;
using Aspose.Words.Saving;
using NUnit.Framework;

namespace ApiExamples
{
    [TestFixture]
    internal class ExHtmlFixedSaveOptions : ApiExampleBase
    {
        [Test]
        public void UseEncoding()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.Encoding
            //ExSummary:Shows how to set encoding for exporting to HTML.
            Document doc = new Document();

            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Writeln("Hello World!");

            // Encoding the document
            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.Encoding = new ASCIIEncoding();

            doc.Save(ArtifactsDir + "UseEncoding.html", htmlFixedSaveOptions);
            //ExEnd
        }

        //Note: Test doesn't contain validation result, because it's may take a lot of time for assert result
        //For validation result, you can save the document to HTML file and check out with notepad++, that file encoding will be correctly displayed (Encoding tab in Notepad++)
        [Test]
        public void ExportEmbeddedObjects()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.ExportEmbeddedCss
            //ExFor:HtmlFixedSaveOptions.ExportEmbeddedFonts
            //ExFor:HtmlFixedSaveOptions.ExportEmbeddedImages
            //ExFor:HtmlFixedSaveOptions.ExportEmbeddedSvg
            //ExSummary:Shows how to export embedded objects into HTML file.
            Document doc = DocumentHelper.CreateDocumentFillWithDummyText();

            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.Encoding = Encoding.ASCII;
            htmlFixedSaveOptions.ExportEmbeddedCss = true;
            htmlFixedSaveOptions.ExportEmbeddedFonts = true;
            htmlFixedSaveOptions.ExportEmbeddedImages = true;
            htmlFixedSaveOptions.ExportEmbeddedSvg = true;

            doc.Save(ArtifactsDir + "ExportEmbeddedObjects.html", htmlFixedSaveOptions);
            //ExEnd
        }

        //Note: Test doesn't contain validation result, because it's may take a lot of time for assert result
        //For validation result, you can save the document to HTML file and check out with notepad++, that file encoding will be correctly displayed (Encoding tab in Notepad++)
        [Test]
        public void EncodingUsingNewEncoding()
        {
            Document doc = DocumentHelper.CreateDocumentFillWithDummyText();

            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.Encoding = new UTF32Encoding();

            doc.Save(ArtifactsDir + "EncodingUsingNewEncoding.html", htmlFixedSaveOptions);
        }

        //Note: Test doesn't contain validation result, because it's may take a lot of time for assert result
        //For validation result, you can save the document to HTML file and check out with notepad++, that file encoding will be correctly displayed (Encoding tab in Notepad++)
        [Test]
        public void EncodingUsingGetEncoding()
        {
            Document doc = DocumentHelper.CreateDocumentFillWithDummyText();

            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.Encoding = Encoding.GetEncoding("utf-16");

            doc.Save(ArtifactsDir + "EncodingUsingGetEncoding.html", htmlFixedSaveOptions);
        }

        [Test]
        public void ExportFormFields()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.ExportFormFields
            //ExSummary:Show how to exporting form fields from a document into HTML file.
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.InsertCheckBox("CheckBox", false, 15);

            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.ExportFormFields = true;
            
            doc.Save(ArtifactsDir + "ExportFormFiels.html", htmlFixedSaveOptions);
            //ExEnd
        }

        [Test]
        public void CssPrefix()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.CssClassNamesPrefix
            //ExSummary:Shows how to add prefix to all class names in css file.
            Document doc = new Document(MyDir + "Bookmark.doc");

            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.CssClassNamesPrefix = "test";

            doc.Save(ArtifactsDir + "cssPrefix_Out.html", htmlFixedSaveOptions);
            //ExEnd

            DocumentHelper.FindTextInFile(ArtifactsDir + "cssPrefix_Out/styles.css", "test");
        }

        [Test]
        public void HorizontalAlignment()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.PageHorizontalAlignment
            //ExFor:HtmlFixedPageHorizontalAlignment
            //ExSummary:Shows how to set the horizontal alignment of pages in HTML file.
            Document doc = new Document(MyDir + "Bookmark.doc");

            HtmlFixedSaveOptions htmlFixedSaveOptions = new HtmlFixedSaveOptions();
            htmlFixedSaveOptions.PageHorizontalAlignment = HtmlFixedPageHorizontalAlignment.Left;

            doc.Save(ArtifactsDir + "HtmlFixedPageHorizontalAlignment.html", htmlFixedSaveOptions);
            //ExEnd
        }

        [Test]
        public void PageMarginsException()
        {
            Document doc = new Document(MyDir + "Bookmark.doc");

            HtmlFixedSaveOptions saveOptions = new HtmlFixedSaveOptions();
            Assert.That(() => saveOptions.PageMargins = -1, Throws.TypeOf<ArgumentException>());

            doc.Save(ArtifactsDir + "HtmlFixedPageMargins.html", saveOptions);
        }

        [Test]
        public void PageMargins()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.PageMargins
            //ExSummary:Shows how to set the margins around pages in HTML file.
            Document doc = new Document(MyDir + "Bookmark.doc");

            HtmlFixedSaveOptions saveOptions = new HtmlFixedSaveOptions();
            saveOptions.PageMargins = 10;

            doc.Save(ArtifactsDir + "HtmlFixedPageMargins.html", saveOptions);
            //ExEnd
        }

        [Test]
        public void UsingMachineFonts()
        {
            //ExStart
            //ExFor:HtmlFixedSaveOptions.UseTargetMachineFonts
            //ExSummary: Shows how used target machine fonts to display the document
            Document doc = new Document(MyDir + "Font.DisapearingBulletPoints.doc");

            HtmlFixedSaveOptions saveOptions = new HtmlFixedSaveOptions();
            saveOptions.UseTargetMachineFonts = true;
            saveOptions.FontFormat = ExportFontFormat.Ttf;
            saveOptions.ExportEmbeddedFonts = false;
            saveOptions.ResourceSavingCallback = new ResourceSavingCallback();

            doc.Save(ArtifactsDir + "UseMachineFonts.html", saveOptions);
        }

        private class ResourceSavingCallback : IResourceSavingCallback
        {
            /// <summary>
            /// Called when Aspose.Words saves an external resource to fixed page HTML or SVG.
            /// </summary>
            public void ResourceSaving(ResourceSavingArgs args)
            {
                args.ResourceStream = new MemoryStream();
                args.KeepResourceStreamOpen = true;

                string extension = Path.GetExtension(args.ResourceFileName);
                switch (extension)
                {
                    case ".ttf":
                    case ".woff":
                    {
                        Assert.Fail("'ResourceSavingCallback' is not fired for fonts when 'UseTargetMachineFonts' is true");
                        break;
                    }
                }
            }
        }
        //ExEnd
    }
}