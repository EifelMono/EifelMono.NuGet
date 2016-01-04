using System;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;

namespace EifelMono.NuGet
{
    public static class MainActions
    {
        public static void NuSpecFillVersionFromDll(string dllFilename)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            #if DEBUG
            currentDirectory = "/Users/andreas/Documents/Development/github/EifelMono.Extensions";
            #endif
            var nuSpecXmls= Directory.GetFiles(currentDirectory, "*.nuspec.xml");
            if (nuSpecXmls!= null)
            {
                string fileVersion = FileVersionInfo.GetVersionInfo(dllFilename).FileVersion;
                var fileVersionParts = fileVersion.Split('.');
                switch (fileVersionParts.Length)
                {
                    case 0:
                        fileVersion= "0.0.0";
                        break;
                    case 1:
                        fileVersion= fileVersionParts[0]+ ".0.0";
                        break;
                    case 2:
                        fileVersion= fileVersionParts[0]+ "." +fileVersionParts[1]+".0";
                        break;
                    default:
                        fileVersion= fileVersionParts[0]+ "." +fileVersionParts[1]+ "." +fileVersionParts[2];
                        break;
                }
                foreach(var nuSpecXml in nuSpecXmls)
                {
                    Console.WriteLine(Path.GetFileName(nuSpecXml));
                    var xNuSpecXml = XDocument.Parse(File.ReadAllText(nuSpecXml));
                    var nameSpace = xNuSpecXml.Root.GetDefaultNamespace();
                    var xVersions = xNuSpecXml.Descendants(nameSpace+ "version");
                    if (xVersions!= null)
                        foreach(var xVersion in xVersions)
                            xVersion.Value= fileVersion;
                    string nuSpec = nuSpecXml.Replace(".xml", "");
                    Console.WriteLine(Path.GetFileName(nuSpec));
                    File.WriteAllText(nuSpec, xNuSpecXml.ToString());
                }
            }
            else
                Console.WriteLine("Nothing found nuspec.xml");   
        }
    }
}

