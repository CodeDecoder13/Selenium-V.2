using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSelenium.Testing
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Testing_Responsiveness : BaseTest
    {
        [Test(Description = "This is a Responiveness Testing Template Code")]
        [Category("Responsive")]
        public void Dashboard_Page()  
        {
            test.AssignCategory("Dashboard_Page");

            driver.Navigate().GoToUrl(baseUrl);
            test.Log(Status.Info, "Navigating to Url: " + baseUrl);
            CaptureScreenshot("HomePage");

            var resolutions = new[]
            {
                new { Width = 1920, Height = 1080},
                new { Width = 1440, Height = 2560 },
                new { Width = 2560, Height = 1080 },
                new { Width = 3440, Height = 1440 },
                new { Width = 5120, Height = 1440 }
            };

            foreach (var resolution in resolutions)
            {
                // Resize the browser window
                driver.Manage().Window.Size = new System.Drawing.Size(resolution.Width, resolution.Height);

                // Capture a screenshot for the current resolution
                CaptureScreenshot($"Resolution_{resolution.Width}x{resolution.Height}");
                test.Log(Status.Pass, $"Resolution set to {resolution.Width}x{resolution.Height}");
            }

        }
    }
}
