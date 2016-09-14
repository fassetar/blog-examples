using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;


namespace LoginAutomationSolution
{
	public class Driver
	{
		public static IWebDriver Instance { get; set; }
		public static void Initialize()
		{
			//TODO: write support for other browser if chrome not installed.
			//Take input from user on which browsers to load up.
			//Instance = new FirefoxDriver();
			//change out the -incognito with -private (for Firefox and Internet Explorer) and -newprivatetab (for Opera).
						
			//DOCS: Requirements Use ChromeDriver 2.14+            
			//Or Download:http://chromedriver.storage.googleapis.com/index.html
			var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "LoginAutomation\\Drivers");
			Instance = new ChromeDriver(@path);
			DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
			ChromeOptions options = new ChromeOptions();
			options.AddArguments("test-type");			
			capabilities.SetCapability(ChromeOptions.Capability, options);

			Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
		}

		public static void Close()
		{   			
			//DOCS: Closes the chromedriver.exe in the background!
			Instance.Quit();
		}
	}
}
