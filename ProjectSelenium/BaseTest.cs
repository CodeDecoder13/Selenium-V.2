using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSelenium
{
    public class BaseTest
    {
        public IWebDriver driver;
        public ExtentReports extent;
        public ExtentTest test;
        public IDictionary<string, object> vars { get; private set; }
        public IJavaScriptExecutor js;

        public string baseUrl = "";

        [SetUp]
        public void Initialize()
        {
            var options = new ChromeOptions();
            options.AddArguments("--start-maximized");

            // For headless mode, specify window size
            options.AddArguments("--headless");
            //options.AddArguments("--headed");
            options.AddArguments("--window-size=1920,1080");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();

            test = ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            ExtentReportsHelper.Flush();
        }


        public void CaptureScreenshot(string screenshotName)
        {
            string screenshotPath = ExtentReportsHelper.CaptureScreenshot(driver, screenshotName);
            test.AddScreenCaptureFromPath(screenshotPath);
        }
    }
}
