using System;
using System.IO;
using JDI.Light.Matchers;
using static JDI.Light.Jdi;

namespace JDI.Light.Asserts
{
    public class FileAssert : BaseAssert
    {
        private readonly FileInfo _file;

        public FileAssert(string fileName) : base(fileName)
        {
            _file = new FileInfo(Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads"), fileName));
        }

        public static FileAssert AssertThatFile(string fileName)
        {
            return new FileAssert(fileName);
        }

        public FileAssert IsDownloaded()
        {
            Assert.IsTrue(_file.Exists);
            return this;
        }

        public FileAssert Text(Matcher<string> text)
        {

            Assert.IsTrue(text.IsMatch(File.ReadAllText(_file.FullName)));
            return this;
        }

        public FileAssert HasSize(Matcher<long> size)
        {
            Assert.IsTrue(size.IsMatch(_file.Length));
            return this;
        }

        public static void CleanupDownloads()
        {
            foreach (var file in Directory.GetFiles(Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads"))))
            {
                 File.Delete(file);
            }
            Logger.Info("Remove all downloads successfully");
        }
    }
}
