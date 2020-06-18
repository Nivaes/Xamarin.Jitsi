using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JitsiMavenRepositoryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://github.com/jitsi/jitsi-maven-repository
            FindDirectories(@"D:\Apl\Jitsi\jitsi-maven-repository\releases");
        }

        static void FindDirectories(string path)
        {
            var dirs = System.IO.Directory.GetDirectories(path);

            foreach(var dir in dirs)
            {
                var fileMavenMetadataPath = Path.Combine(dir, "maven-metadata.xml");
                if (File.Exists(fileMavenMetadataPath))
                {
                    var releaseVersion = ReadMavenMetadataXML(fileMavenMetadataPath);
                    var fileResource = FileResource(dir, releaseVersion);

                    CopyFile(fileResource);
                }
                else
                {
                    FindDirectories(dir);
                }
            }
        }

        static string ReadMavenMetadataXML(string fileMavenMetadataPath)
        {
            XDocument doc = XDocument.Load(fileMavenMetadataPath);
            var versioning = doc.Root.Elements("versioning");
            var release = versioning.Elements("release");
            var releaseValue = release.FirstOrDefault()?.Value;

            return releaseValue;
        }

        static string FileResource(string dirLibrary, string releaseVersion)
        {
            var parts = dirLibrary.Split("\\");
            var libName = parts.Last(); // parts[parts.Length - 1];

            var resourceFileName = Path.Combine(dirLibrary, releaseVersion, libName + "-" + releaseVersion);

            string fileAar = $"{resourceFileName}.aar";
            if(File.Exists(fileAar))
            {
                return fileAar;
            }

            string fileJar = $"{resourceFileName}.jar";
            if (File.Exists(fileJar))
            {
                return fileJar;
            }

            string filePom = $"{resourceFileName}.pom";
            if (File.Exists(filePom))
            {
                return filePom;
            }

            Console.WriteLine($"File {resourceFileName} not found");

            return string.Empty;
        }

        static void CopyFile(string fileResource)
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            var fileNameResoruce = Path.GetFileName(fileResource);

            File.Copy(fileResource, $"Resources\\{fileNameResoruce}", true);
        }
    }
}
