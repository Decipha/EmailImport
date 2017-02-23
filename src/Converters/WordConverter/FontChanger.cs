using Aspose.Words;

namespace WordConverter
{
    /// <summary>
    /// Class inherited from DocumentVisitor, that chnges fornt of each node to font specified in the constructor
    /// </summary>
    class FontChanger : DocumentVisitor
    {
        public FontChanger(string fontName)
        {
            mFontName = fontName;
        }

        /// <summary>
        /// Called when a FieldEnd node is encountered in the document.
        /// </summary>
        public override VisitorAction VisitFieldEnd(Aspose.Words.Fields.FieldEnd fieldEnd)
        {
            //Simply change font name
            fieldEnd.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a FieldSeparator node is encountered in the document.
        /// </summary>
        public override VisitorAction VisitFieldSeparator(Aspose.Words.Fields.FieldSeparator fieldSeparator)
        {
            fieldSeparator.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a FieldStart node is encountered in the document.
        /// </summary>
        public override VisitorAction VisitFieldStart(Aspose.Words.Fields.FieldStart fieldStart)
        {
            fieldStart.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a Footnote end is encountered in the document.
        /// </summary>
        public override VisitorAction VisitFootnoteEnd(Footnote footnote)
        {
            footnote.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a FormField node is encountered in the document.
        /// </summary>
        public override VisitorAction VisitFormField(Aspose.Words.Fields.FormField formField)
        {
            formField.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a Paragraph end is encountered in the document.
        /// </summary>
        public override VisitorAction VisitParagraphEnd(Paragraph paragraph)
        {
            paragraph.ParagraphBreakFont.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a Run node is encountered in the document.
        /// </summary>
        public override VisitorAction VisitRun(Run run)
        {
            run.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        /// <summary>
        /// Called when a SpecialChar is encountered in the document.
        /// </summary>
        public override VisitorAction VisitSpecialChar(SpecialChar specialChar)
        {
            specialChar.Font.Name = mFontName;
            return VisitorAction.Continue;
        }

        //Font by default
        private string mFontName = "Times New Roman";
    }
}