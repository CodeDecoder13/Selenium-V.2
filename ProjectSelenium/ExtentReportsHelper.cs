using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;

namespace ProjectSelenium
{
    public class ExtentReportsHelper
    {
        private static ExtentReports extent;
        private static string screenshotDirectory;
        private static string reportDirectory;
        private static string reportFilePath;
        private static string author;

        static ExtentReportsHelper()
        {
            // Ensure the extent is initialized once
            SetDirectories();
            extent = new ExtentReports();
            var reporter = new ExtentSparkReporter(reportFilePath);
            reporter.Config.Theme = Theme.Dark;  // Set the theme to Dark
            extent.AttachReporter(reporter);
            extent.AddSystemInfo("OS", "Windows 11");
        }

        public static ExtentReports GetExtent()
        {
            return extent;
        }

        public static ExtentTest CreateTest(string testName, string description = "This is Open-Source Project that automated the Testing in the WebApp", string author = "Rhuzzel Paramio")
        {
            var test = GetExtent().CreateTest(testName, description);
            if (!string.IsNullOrEmpty(author))
            {
                test.AssignAuthor(author);
            }
            return test;
        }

        public static void SetAuthor(string testAuthor)
        {
            author = testAuthor;
        }

        public static void Flush()
        {
            if (extent != null)
            {
                extent.Flush();
                Process.Start("explorer.exe", Path.GetFullPath(reportFilePath));
            }
        }

        private static void SetDirectories()
        {
            string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshot repo");
            string dateDirectory = DateTime.Now.ToString("yyyy-MM-dd");
            screenshotDirectory = Path.Combine(baseDirectory, dateDirectory);

            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }

            string reportBaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "HtmlReport");
            if (!Directory.Exists(reportBaseDirectory))
            {
                Directory.CreateDirectory(reportBaseDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            reportFilePath = Path.Combine(reportBaseDirectory, $"generated_report_{timestamp}.html");
        }

        public static string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filePath = Path.Combine(screenshotDirectory, $"{screenshotName}_{timestamp}.png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);
            return filePath;
        }
    }
}
