using System.IO;
using System.Text;
using System.Web;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class UnicodeFileToHtmlTextConverter
    {
        private const string HtmlLineBreak = "<br />";
        private readonly string _fullFilenameWithPath;

        public UnicodeFileToHtmlTextConverter(string fullFilenameWithPath)
        {
            _fullFilenameWithPath = fullFilenameWithPath;
        }

        public string ConvertToHtml()
        {
            using (var unicodeFileStream = File.OpenText(_fullFilenameWithPath))
            {
                var sb = new StringBuilder();

                var line = unicodeFileStream.ReadLine();
                while (line != null)
                {
                    sb.Append(HttpUtility.HtmlEncode(line));
                    sb.Append(HtmlLineBreak);
                    line = unicodeFileStream.ReadLine();
                }

                return sb.ToString();
            }
        }
    }
}
