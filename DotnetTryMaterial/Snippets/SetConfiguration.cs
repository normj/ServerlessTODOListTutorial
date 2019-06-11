
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;


namespace Snippets
{
    public class SetConfiguration
    {
        public static string GetAWSProfile()
        {
            string AWS_PROFILE;
            
            #region current_aws_profile
            AWS_PROFILE = "default";
            #endregion

            return  AWS_PROFILE;
        }

        public static void SetAWSProfile()
        {
            Console.WriteLine("Profile set to " + GetAWSProfile());
            SetConfigurationRegion("current_aws_profile", "AWS_PROFILE", GetAWSProfile());
        }


        public static string GetAWSRegion()
        {
            string AWS_REGION;
            
            #region current_aws_region
            AWS_REGION = "us-east-1";
            #endregion

            return  AWS_REGION;
        }
        
        public static void SetAWSRegion()
        {
            Console.WriteLine("AWS region to " + GetAWSRegion());
            SetConfigurationRegion("current_aws_region","AWS_REGION", GetAWSRegion());
        }        

        private static void SetConfigurationRegion(string region, string property, string value)
        {
            const string FILE_NAME = "SetConfiguration.cs";

            var directory = Directory.GetCurrentDirectory();
            do
            {
                if (File.Exists(Path.Combine(directory, FILE_NAME)))
                {
                    break;
                }
                else
                {
                    directory = Directory.GetParent(directory).FullName;
                }
            }while(!string.IsNullOrEmpty(directory));

            var fullPath = Path.Combine(directory, FILE_NAME);

            var content = File.ReadAllText(fullPath);

            var sb = new StringBuilder();
            
            var reader = new StringReader(content);
            string line;
            bool inRegion = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (!inRegion && line.Contains("#region") && line.Contains(region))
                {
                    inRegion = true;
                }
                else if (line.Contains("#endregion"))
                {
                    inRegion = false;
                }
                if (inRegion && line.Contains(property))
                {
                    var startPos = line.IndexOf("\"") + 1;
                    line = line.Substring(0, startPos) + value + "\";";
                }

                sb.AppendLine(line);
            }
            
            File.WriteAllText(fullPath, sb.ToString());
        }
    }
}
