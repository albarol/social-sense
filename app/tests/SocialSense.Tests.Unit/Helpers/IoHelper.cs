namespace SocialSense.Tests.Unit.Helpers
{
    using System;
    using System.IO;
    using System.Web;

    public static class IoHelper
    {
        public static string ReadContent(string fileName)
        {
            var path = Environment.CurrentDirectory;
            return new StreamReader(string.Format("{0}{1}{2}", path, "/Resources/", fileName)).ReadToEnd();
        }

        public static string ReadContentWithDecode(string fileName)
        {
            return HttpUtility.HtmlDecode(ReadContent(fileName));
        }
    }
}
