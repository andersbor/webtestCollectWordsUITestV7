using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Collections.ObjectModel;

namespace CollectWordsUITestV7
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\webDrivers";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            //_driver = new ChromeDriver(DriverDirectory);
            // _driver = new FirefoxDriver(DriverDirectory); 
            _driver = new EdgeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestSimpleStringOutput()
        {
            //_driver.Navigate().GoToUrl("http://localhost:3000/");
            _driver.Navigate().GoToUrl("file:///C:/andersb/javascript/collectwords/index.htm");
            Assert.AreEqual("Collect words", _driver.Title);

            IWebElement inputElement = _driver.FindElement(By.Id("wordInput"));
            inputElement.SendKeys("anders");

            IWebElement saveButton = _driver.FindElement(By.Id("saveButton"));
            saveButton.Click();

            IWebElement showButton = _driver.FindElement(By.Id("showButton"));
            showButton.Click();

            IWebElement outputElement = _driver.FindElement(By.Id("output"));
            string text = outputElement.Text;

            Assert.AreEqual("anders", text);

            inputElement.Clear();
            inputElement.SendKeys("bor");
            saveButton.Click();
            showButton.Click();
            text = outputElement.Text;
            Assert.AreEqual("anders,bor", text);

            IWebElement clearButton = _driver.FindElement(By.Id("clearButton"));
            clearButton.Click();
            text = outputElement.Text;
            Assert.AreEqual("", text);

            showButton.Click();
            Assert.AreEqual("empty", outputElement.Text);
        }

        [TestMethod]
        public void TestOrderedListOutput()
        {
            //_driver.Navigate().GoToUrl("http://localhost:3000/");
            _driver.Navigate().GoToUrl("file:///C:/andersb/javascript/collectwords/index.htm");
            Assert.AreEqual("Collect words", _driver.Title);

            IWebElement inputElement = _driver.FindElement(By.Id("wordInput"));
            inputElement.SendKeys("anders");

            IWebElement saveButton = _driver.FindElement(By.Id("saveButton"));
            saveButton.Click();

            IWebElement showButton = _driver.FindElement(By.Id("showButton"));
            showButton.Click();

            IWebElement outputElement = _driver.FindElement(By.Id("output"));
            string text = outputElement.Text;

            Assert.AreEqual("anders", text);

            inputElement.Clear();
            inputElement.SendKeys("bor");
            saveButton.Click();

            IWebElement listElement = _driver.FindElement(By.Id("wordlist"));
            Assert.IsTrue(listElement.Text.Contains("anders"));

            IList<IWebElement> listItems = _driver.FindElements(By.TagName("li"));
            Assert.AreEqual(2, listItems.Count);
            Assert.AreEqual("anders", listItems[0].Text);
            Assert.AreEqual("bor", listItems[1].Text);

            IWebElement clearButton = _driver.FindElement(By.Id("clearButton"));
            clearButton.Click();
            string message = _driver.FindElement(By.Id("noWordsMessage")).Text;
            Assert.AreEqual("No words", message);
        }
    }
}
