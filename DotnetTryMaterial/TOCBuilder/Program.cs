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
                    PrintTOC(item.ToString(), toc);

                }
                else if (item.Type == JTokenType.Object)
                {
                    var folder = item["FolderName"].ToString();
                    var rootPage = item["RootPage"].ToString();
                    var pages = item["Pages"] as JArray;

                    {
                        var toc = BuildTOC(root, rootPage, folder);
                        PrintTOC(rootPage.ToString(), toc);
                    }

                    foreach (var page in pages)
                    {
                        var toc = BuildTOC(root, page.ToString(), folder);
                        PrintTOC(page.ToString(), toc);
                    }
                }
            }
        }

        static string BuildTOC(JArray root, string currentFile, string currentFolder)
        {
            StringBuilder sb = new StringBuilder();

            foreach (JToken item in root)
            {
                if (item.Type == JTokenType.String)
                {
                    var isSelected = string.Equals(currentFile, item.ToString());
                    sb.AppendLine($"* {GetLine(isSelected, item.ToString(), null, null)}");
                }   
                else if (item.Type == JTokenType.Object)
                {
                    var name = item["Name"].ToString();
                    var folder = item["FolderName"].ToString();
                    var rootPage = item["RootPage"].ToString();
                    var pages = item["Pages"] as JArray;

                    
                    if (string.Equals(folder, currentFolder))
                    {
                        sb.AppendLine($"* {GetLine(string.Equals(currentFile, rootPage.ToString()), rootPage.ToString(), folder, null)}");
                        foreach (var page in pages)
                        {
                            sb.AppendLine($"** {GetLine(string.Equals(currentFile, page.ToString()), page.ToString(), folder, null)}");
                        }
                    }
                    else
                    {
                        sb.AppendLine($"* {CreateLink(rootPage, folder, name)}");
                    }
                }
            }

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

        private static string GetLine(bool isSelected,  string fileName, string folder, string overrideTile = null)
        {
            if (isSelected)
            {
                return BoldTitle(fileName, folder, overrideTile);
            }
            else
            {
                return CreateLink(fileName, folder, overrideTile);
            }            
        }

        private static string BoldTitle(string fileName, string folder, string overrideTile = null)
        {
            var fullPath = Path.Combine(contentRoot, folder ?? "", fileName);
            var title = overrideTile ?? GetTitle(fullPath);

            return $"**{title}**";
        }

        private static string CreateLink(string fileName, string folder, string overrideTile = null)
        {
            var fullPath = Path.Combine(contentRoot, folder ?? "", fileName);
            var title = overrideTile ?? GetTitle(fullPath);
            
            StringBuilder sb = new StringBuilder("./");
            sb.Append($"[{title}]");
            if (!string.IsNullOrEmpty(folder))
                sb.Append($"{folder}/");

            sb.Append(fileName);
            return sb.ToString();
        }

        private static string GetTitle(string fullPath)
        {
            var content = File.ReadAllText(fullPath).Trim();
            var titleLine = content.Split('\n')[0];
            return titleLine.Replace("#", "").Trim();
        }

        static void GetPreviousAndNextPageLinks(JArray root, string currentFile, string currentFolder, out string previousLink, out string nextLink)
        {
            previousLink = null;
            nextLink = null;
            bool pageFound = false;
            foreach (JToken item in root)
            {
                if (item.Type == JTokenType.String)
                {
                    var isSelected = string.Equals(currentFile, item.ToString());
                    if (!isSelected && !pageFound)
                    {
                        previousLink = CreateLink(item.ToString(), null);
                    }
                    else if (isSelected)
                    {
                        pageFound = true;
                    }
                    else if (pageFound && nextLink == null)
                    {
                        nextLink = CreateLink(item.ToString(), null);
                        return;
                    }
                }   
                else if (item.Type == JTokenType.Object)
                {
                    var folder = item["FolderName"].ToString();
                    var rootPage = item["RootPage"].ToString();
                    var pages = item["Pages"] as JArray;

                    
                    
                }
            }            
        }
        
        static void PrintTOC(string page, string toc)
        {
            Console.WriteLine($"---- {page.ToString()} ----");
            Console.WriteLine(toc);
            Console.WriteLine();            
        }        
    }
}