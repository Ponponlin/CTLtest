using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CTLtest.MemberManage
{
    [TestClass]
    public class MemberAddTest
    {
        private static IWebDriver driver;
        [ClassInitialize()]
        public static void Init(TestContext testContext)
        {
            driver = new ChromeDriver();
            Common.CommonAPI common = new Common.CommonAPI();
            common.Login(driver);
        }

        [ClassCleanup()]
        public static void CleanUp()
        {
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                driver.Quit();
            }
            catch
            {
                Console.WriteLine("Quit fail!");
            }
        }

        //新增
        [TestMethod]
        public void MemberAdd()
        {
            Common.CommonAPI  common = new Common.CommonAPI();
            IWebElement menuClass;

            //進入會員管理－新增帳號
            menuClass = driver.FindElement(By.CssSelector("ul>li:nth-child(1)"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", menuClass);
            driver.FindElement(By.CssSelector("#MemberManage > div > div > button")).Click();

            IWebElement membertype;
            IWebElement membertypechild;
            IWebElement phoneInput;
            IWebElement passwordInput;
            IWebElement confirmpasswordInput;

            phoneInput = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div > form > div> div > div > input"));
            passwordInput = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div > form > div:nth-child(3) > div > div > input"));
            confirmpasswordInput = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div > form > div:nth-child(4) > div > div > input"));

            phoneInput.SendKeys("095550098");
            passwordInput.SendKeys("111333");
            confirmpasswordInput.SendKeys("111333");

            //取消鍵
            driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[3]/span/button[1]"));
            //新增鍵
            driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[3]/span/button[2]"));

            common.DeleteInput(phoneInput);

            //選擇帳號類別
            membertype = driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[1]/div/div/div/input"));
            membertype.Click();
            membertypechild = driver.FindElement(By.XPath("/html/body/div[3]/div[1]/div[1]/ul/li[2]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", membertypechild);


        }

        [TestMethod]
        public void Addmember_Warring()
        {
            Common.CommonAPI common = new Common.CommonAPI();
            IWebElement menuClass;
            String testoption;

            //進入會員管理－新增帳號
            menuClass = driver.FindElement(By.CssSelector("ul>li:nth-child(1)"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", menuClass);
            driver.FindElement(By.CssSelector("#MemberManage > div > div > button")).Click();

            IWebElement membertype;
            IWebElement membertypechild;
            IWebElement phoneInput;
            IWebElement passwordInput;
            IWebElement confirmpasswordInput;

            //選擇帳號類別
            membertype = driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[1]/div/div/div/input"));
            membertype.Click();
            membertypechild = driver.FindElement(By.XPath("/html/body/div[3]/div[1]/div[1]/ul/li[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", membertypechild);


            phoneInput = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div > form > div> div > div > input"));
            passwordInput = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div > form > div:nth-child(3) > div > div > input"));
            confirmpasswordInput = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div > form > div:nth-child(4) > div > div > input"));

            Console.WriteLine("測試手機欄位：");
            testoption = "未輸入";
            phoneInput.SendKeys("");
            PhoneCheck(membertypechild, testoption);

            testoption = "09+8位數字";
            phoneInput.SendKeys("");
            PhoneCheck(membertypechild, testoption);

            testoption = "09+8位數字";
            phoneInput.SendKeys("");
            PhoneCheck(membertypechild, testoption);

            testoption = "手機已存在";
            phoneInput.SendKeys("");
            PhoneCheck(membertypechild, testoption);



        }


        public void PhoneCheck(IWebElement membertypechild,String option)
        {
            String warringstring;
            driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[1]/div/div/div/input")).Click();
            membertypechild = driver.FindElement(By.XPath("/html/body/div[3]/div[1]/div[1]/ul/li[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", membertypechild);

            warringstring =  driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div.el-dialog__body > form > div.el-form-item.el-form-item--feedback.is-error.is-required.el-form-item--small > div > div.el-form-item__error"))
                                                 .GetAttribute("innerText");

            
            switch (option) {
                case "未輸入":
                    if (warringstring == "手機為必填")
                        Console.WriteLine(option + ":success");
                    break;
                case "09+8位數字":
                    if (warringstring == "09+8位數字")
                        Console.WriteLine(option + ":success");
                    break;
                case "手機已存在":
                    if (warringstring == "手機已存在")
                        Console.WriteLine(option + ":success");
                    break;
                    
            }

            //if (warringstring == "09+8位數字")
            //{
            //    Console.WriteLine(option + ":success");
            //}
            //else if (warringstring == "手機為必填")
            //{
            //    Console.WriteLine(option + ":success");
            //}
            //else if(warringstring == "手機已存在")
            //{
            //    Console.WriteLine(option + ":success");
            //}
            //else
            //    Console.WriteLine(option + ":fail");

        }

    }
}
