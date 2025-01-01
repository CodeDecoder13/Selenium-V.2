using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace ProjectSelenium
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Template_Code : BaseTest
    {

        [Test]
        public void Testing()
        {
            test.Log(Status.Info, "Navigating to Url: " + baseUrl);
            driver.Navigate().GoToUrl(baseUrl);

            test.Log(Status.Info, "Looking for Elements of the UserPresence");
            IWebElement UserId = driver.FindElement(By.ClassName("UsernameOnly"));

            test.Log(Status.Info, "Verify User Presence");
            string text = UserId.Text;

            if (text.Equals("LEGAL\\PARAMIOR"))
            {
                test.Log(Status.Pass, "LEGAL\\PARAMIOR is detected");

            }
            else
            {
                test.Log(Status.Fail, $"Expected Text 'LEGAL\\PARAMIOR' but found '{text}'");
                // Take a screenshot if the assertion fails

            }

            Assert.That(text, Is.EqualTo("LEGAL\\PARAMIOR"), $"Expected Text 'LEGAL\\PARAMIOR' but found '{text}'. The element was found using ClassName.");
            Console.WriteLine("Text is comple");
        }
    }//end of the line
}