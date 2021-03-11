using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System.Threading;

namespace CTLtest.Common
{
    public class CommonAPI
    {
        public void Common()
        {

        }

        //登入功能
        public void Login(IWebDriver driver)
        {
            String account = "thothponpon";
            String password = "12345678";
            driver.Navigate().GoToUrl("https://platform-ctl.gebtest365.net/#/login");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            driver.FindElement(By.CssSelector("input[placeholder='帳號']")).SendKeys(account);
            driver.FindElement(By.CssSelector("input[placeholder='密碼']")).SendKeys(password);
            driver.FindElement(By.CssSelector("#Login > div > div > form > div > button")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        //將input欄位值清空
        public void DeleteInput(IWebElement input)
        {
            while (input.GetAttribute("value").Length > 0)
            {
                input.SendKeys(Keys.Backspace);
            }
        }

        //將input欄位設為空值
        public void SetInputValueZero(IWebDriver driver, IWebElement input)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = '';", input);
        }
    }
}
