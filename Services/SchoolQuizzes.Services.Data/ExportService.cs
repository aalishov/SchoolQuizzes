namespace SchoolQuizzes.Services.Data
{
    using System.IO;

    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Quizzes;
    using Syncfusion.DocIO;
    using Syncfusion.DocIO.DLS;

    public class ExportService : IExportService
    {
        public MemoryStream ExportQuizQuestions(DetailsQuizViewModel model)
        {
            WordDocument document = new WordDocument();
            //Adds new section to the document
            IWSection section = document.AddSection();

            IWParagraph paragraph = section.AddParagraph();

            paragraph.ApplyStyle(BuiltinStyle.Heading1);
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.AppendText($"{model.CategoryName} - {model.Title}");

            foreach (var question in model.Questions)
            {
                paragraph = section.AddParagraph();
                //Applies default numbered list style
                paragraph.ListFormat.ApplyDefNumberedStyle();
                //Adds text to the paragraph
                paragraph.AppendText($"{question.Value}");

                //Continues the list defined
                paragraph.ListFormat.ContinueListNumbering();
                paragraph = section.AddParagraph();
                paragraph.ListFormat.IncreaseIndentLevel();

                foreach (var answer in question.Answers)
                {
                    
                    paragraph.AppendText($"{answer.Value}");
                    //Continues the list defined
                    paragraph.ListFormat.ContinueListNumbering();
                    paragraph = section.AddParagraph();
                }
                paragraph.ListFormat.DecreaseIndentLevel();

            }

            //Saves the Word document to  MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            stream.Position = 0;

            //Download Word document in the browser
            return stream;
        }
    }
}
