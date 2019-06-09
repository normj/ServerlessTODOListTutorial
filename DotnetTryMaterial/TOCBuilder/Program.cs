using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TOCBuilder
{
    class Program
    {
        private const string TOC_JSON_FILE = "TOC.json";

        private static string contentRoot;
        
        static void Main(string[] args)
        {
            var tocJsonPath = GetTOCJsonPath();
            contentRoot = Directory.GetParent(tocJsonPath).FullName;
            
            var root = JsonConvert.DeserializeObject(File.ReadAllText(tocJsonPath)) as JArray;

            foreach (JToken item in root)
            {
                if (item.Type == JTokenType.String)
                {
                    var toc = BuildTOC(root, item.ToString(), null);
                    WriteTOC(item.ToString(), null, toc);

                }
                else if (item.Type == JTokenType.Object)
                {
                    var folder = item["FolderName"].ToString();
                    var rootPage = item["RootPage"].ToString();
                    var pages = item["Pages"] as JArray;

                    {
                        var toc = BuildTOC(root, rootPage, folder);
                        WriteTOC(rootPage.ToString(), folder, toc);
                    }

                    foreach (var page in pages)
                    {
                        var toc = BuildTOC(root, page.ToString(), folder);
                        WriteTOC(page.ToString(), folder, toc);
                    }
                }
            }
        }

        static void WriteTOC(string fileName, string folder, string toc)
        {
            const string navHeader = "<!-- Generated Navigation -->";
            PrintTOC(fileName, toc);
            var fullPath = Path.Combine(contentRoot, folder ?? "", fileName);

            var content = File.ReadAllText(fullPath);

            var pos = content.IndexOf(navHeader);
            if(pos != -1)
            {
                content = content.Substring(0, pos).Trim();
            }

            content += $"\n\n{navHeader}\n---\n\n{toc}";
            File.WriteAllText(fullPath, content.Replace("\r\n", "\n"));
        }

        static string BuildTOC(JArray root, string currentFile, string currentFolder)
        {
            StringBuilder sb = new StringBuilder();

            foreach (JToken item in root)
            {
                if (item.Type == JTokenType.String)
                {
                    var isSelected = string.Equals(currentFile, item.ToString());
                    sb.AppendLine($"* {GetLine(isSelected, currentFolder != null, item.ToString(), null, null)}");
                }   
                else if (item.Type == JTokenType.Object)
                {
                    var name = item["Name"].ToString();
                    var folder = item["FolderName"].ToString();
                    var rootPage = item["RootPage"].ToString();
                    var pages = item["Pages"] as JArray;

                    
                    if (string.Equals(folder, currentFolder))
                    {
                        sb.AppendLine($"* {GetLine(string.Equals(currentFile, rootPage.ToString()), currentFolder != null, rootPage.ToString(), folder, null)}");
                        foreach (var page in pages)
                        {
                            sb.AppendLine($"  * {GetLine(string.Equals(currentFile, page.ToString()), currentFolder != null, page.ToString(), folder, null)}");
                        }
                    }
                    else
                    {
                        sb.AppendLine($"* {CreateLink(currentFolder != null, rootPage, folder, name)}");
                    }
                }
            }

            var links = GetPreviousAndNextPageLinks(root, currentFile, currentFolder);

            sb.AppendLine("");

            if (!string.IsNullOrEmpty(links.nextLink))
                sb.AppendLine($"Continue on to next page: {links.nextLink}\n");
            //if (!string.IsNullOrEmpty(links.previousLink))
            //    sb.AppendLine($"Return to previous page: {links.previousLink}\n");

            return sb.ToString();
        }
        
        

        private static string GetTOCJsonPath()
        {
            var directory = Directory.GetCurrentDirectory();
            while(!string.IsNullOrEmpty(directory))
            {
                var fullPath = Path.Combine(directory, TOC_JSON_FILE);
                if (File.Exists(fullPath))
                    return fullPath;

                directory = Directory.GetParent(directory).FullName;
            }

            return null;
        }

        private static string GetLine(bool isSelected, bool currentlyInDirectory, string fileName, string folder, string overrideTile = null)
        {
            if (isSelected)
            {
                return BoldTitle(fileName, folder, overrideTile);
            }
            else
            {
                return CreateLink(currentlyInDirectory, fileName, folder, overrideTile);
            }            
        }

        private static string BoldTitle(string fileName, string folder, string overrideTile = null)
        {
            var fullPath = Path.Combine(contentRoot, folder ?? "", fileName);
            var title = overrideTile ?? GetTitle(fullPath);

            return $"**{title}**";
        }

        private static string CreateLink(bool currentlyInDirectory, string fileName, string folder, string overrideTile = null)
        {
            var fullPath = Path.Combine(contentRoot, folder ?? "", fileName);
            var title = overrideTile ?? GetTitle(fullPath);
            
            StringBuilder sb = new StringBuilder("");
            sb.Append($"[{title}](");

            sb.Append(currentlyInDirectory ? "../" : "./");                

            if (!string.IsNullOrEmpty(folder))
                sb.Append($"{folder}/");

            sb.Append(fileName + ")");
            return sb.ToString();
        }

        private static string GetTitle(string fullPath)
        {
            var content = File.ReadAllText(fullPath).Trim();
            var titleLine = content.Split('\n')[0];
            return titleLine.Replace("#", "").Trim();
        }

        static (string previousLink, string nextLink) GetPreviousAndNextPageLinks(JArray root, string currentFile, string currentFolder)
        {
            string previousLink = null;
            string nextLink = null;
            bool pageFound = false;

            Action<string, string, string> processPage = (page, folder, overrideTitle) =>
            {
                var isSelected = string.Equals(currentFile, page);
                if (!isSelected && !pageFound)
                {
                    previousLink = CreateLink(currentFolder != null, page, folder, overrideTitle);
                }
                else if (isSelected)
                {
                    pageFound = true;
                }
                else if (pageFound && nextLink == null)
                {
                    nextLink = CreateLink(currentFolder != null, page, folder, overrideTitle);
                }
            };

            foreach (JToken item in root)
            {
                if (item.Type == JTokenType.String)
                {
                    processPage(item.ToString(), null, null);
                }   
                else if (item.Type == JTokenType.Object)
                {
                    var name = item["Name"].ToString();
                    var folder = item["FolderName"].ToString();
                    var rootPage = item["RootPage"].ToString();
                    processPage(rootPage, folder, name);

                    var pages = item["Pages"] as JArray;
                    foreach(var page in pages)
                    {
                        processPage(page.ToString(), folder, null);
                    }
                }
            }

            return (previousLink, nextLink);
        }
        
        static void PrintTOC(string page, string toc)
        {
            Console.WriteLine($"---- {page.ToString()} ----");
            Console.WriteLine(toc);
            Console.WriteLine();            
        }        
    }
}