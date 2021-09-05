using NUnit.Framework;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    [TestFixture]
    public class UnicodeFileToHtmlTextConverterTests
    {
        [Test]
        public void ConvertToHtml_Returns_Valid_Encoded_Text()
        {
            var target = new UnicodeFileToHtmlTextConverter($"Files/{nameof(ConvertToHtml_Returns_Valid_Encoded_Text)}/input.txt");
            var expectedHtml = System.IO.File.ReadAllText($"Files/{nameof(ConvertToHtml_Returns_Valid_Encoded_Text)}/expectedOutput.html");

            var actualHtml = target.ConvertToHtml();

            Assert.AreEqual(expectedHtml, actualHtml);
        }
    }
}