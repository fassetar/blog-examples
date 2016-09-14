using OpenQA.Selenium;
using System.Linq;

namespace LoginAutomationSolution
{
    public class Dashboard
    {
        public static bool IsAt
        {
            get
            {
                var links = Driver.Instance.FindElements(By.TagName("a"));
                if (links.Any())
                {
                    return links.Where(x => x.GetAttribute("href").Equals("https://plus.google.com/u/0/me")).Any();
                    //return links[0].Text == "Dashboard";
                }
                else
                {
                    return false;
                }                    
            }

        }
    }
}
