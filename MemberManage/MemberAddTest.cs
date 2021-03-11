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

            CheckPhoneAndPassword(membertypechild, phoneInput, passwordInput, confirmpasswordInput);

     

        }


        public void CheckPhoneAndPassword(IWebElement membertypechild, IWebElement phoneInput, IWebElement passwordInput, IWebElement confirmpasswordInput)
        {
            Common.CommonAPI api = new Common.CommonAPI();
            String testoption;
            String inputType;

            //手機號碼欄位
            Console.WriteLine("測試手機欄位：");
            inputType = "手機";
            testoption = "未輸入";
            phoneInput.SendKeys("");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "09+7位數字";
            phoneInput.SendKeys("091234567");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "09+9位數字";
            phoneInput.SendKeys("09123456789");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "含中文";
            phoneInput.SendKeys("091234567安");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "含英文";
            phoneInput.SendKeys("091234567A");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "含符號";
            phoneInput.SendKeys("0912345**/");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "全形數字";
            phoneInput.SendKeys("０９２３４５６７８");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, phoneInput);

            testoption = "手機已存在";
            phoneInput.SendKeys("0955500099");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.DeleteInput(phoneInput);


            //密碼欄位
            testoption = "未輸入";
            inputType = "密碼";
            passwordInput.SendKeys("");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);

            testoption = "低於6位";
            passwordInput.SendKeys("12345");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);

            testoption = "高於20位";
            passwordInput.SendKeys("12345678901234567891");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);

            testoption = "含空格";
            passwordInput.SendKeys("1234567 ");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);

            testoption = "含符號";
            passwordInput.SendKeys("1234567**");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);

            //確認密碼
            testoption = "未輸入";
            inputType = "確認密碼";
            passwordInput.SendKeys("1234567");
            confirmpasswordInput.SendKeys("");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);
            api.SetInputValueZero(driver, confirmpasswordInput);

            testoption = "少一位";
            passwordInput.SendKeys("1234567");
            confirmpasswordInput.SendKeys("123456");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);
            api.SetInputValueZero(driver, confirmpasswordInput);

            testoption = "多一位";
            passwordInput.SendKeys("1234567");
            confirmpasswordInput.SendKeys("12345678");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);
            api.SetInputValueZero(driver, confirmpasswordInput);

            testoption = "大小寫顛倒";
            passwordInput.SendKeys("abcdefg");
            confirmpasswordInput.SendKeys("ABCDEFG");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);
            api.SetInputValueZero(driver, confirmpasswordInput);

            testoption = "含空格";
            passwordInput.SendKeys("1234567");
            confirmpasswordInput.SendKeys("12345 67");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.SetInputValueZero(driver, passwordInput);
            api.SetInputValueZero(driver, confirmpasswordInput);

            testoption = "含符號";
            passwordInput.SendKeys("1234567");
            confirmpasswordInput.SendKeys("1234567*-");
            PhoneWarringTip(membertypechild, inputType, testoption);
            api.DeleteInput(passwordInput);
            api.DeleteInput(confirmpasswordInput);

        }


        public void PhoneWarringTip(IWebElement membertypechild, String inputType, String option)
        {
            String warringstring;

            //點選同樣會員類別，單純為unfocus input欄位用
            driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[1]/div/div/div/input")).Click();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", membertypechild);

            //warringstring = driver.FindElement(By.CssSelector("#MemberManage > div:nth-child(7) > div > div.el-dialog__body > form > div.el-form-item.el-form-item--feedback.is-error.is-required.el-form-item--small > div > div.el-form-item__error"))
            //                                     .GetAttribute("innerText");
            //warringstring = driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[2]/div/div[2]")).GetAttribute("innerText");

            switch (inputType)
            {
                case "手機":
                    warringstring = driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[2]/div/div[2]")).GetAttribute("innerText");

                    switch (option)
                    {
                    case "未輸入":
                        if (warringstring == "手機為必填")
                            Console.WriteLine(inputType + option + ":success");
                        break;

                    case "09+9位數字":
                        if (warringstring == "09+8位數字")
                            Console.WriteLine(inputType + option + ":success");
                        break;

                    case "09+7位數字":
                        if (warringstring == "09+8位數字")
                            Console.WriteLine(inputType + option + ":success");
                        break;

                    case "含中文":
                        if (warringstring == "09+8位數字")
                            Console.WriteLine(inputType + option + ":success");
                        break;

                    case "含英文":
                        if (warringstring == "09+8位數字")
                            Console.WriteLine(inputType + option + ":success");
                        break;
                    case "含符號":
                        if (warringstring == "09+8位數字")
                            Console.WriteLine(inputType + option + ":success");
                        break;

                    case "全形數字":
                        if (warringstring == "09+8位數字")
                            Console.WriteLine(inputType + option + ":success");

                        break;

                    case "手機已存在":
                        if (warringstring == "手機已存在")
                            Console.WriteLine(inputType + option + ":success");
                        break;
                    }
                    break;

                case "密碼":
                    warringstring = driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[3]/div/div[2]")).GetAttribute("innerText");

                    switch (option)
                    {

                        case "未輸入":
                            if (warringstring == "密碼為必填")
                                Console.WriteLine(inputType + option + ":success");
                            break;

                        case "低於6位":
                            if (warringstring == "6~20位數字或英文字母")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "高於20位":
                            if (warringstring == "6~20位數字或英文字母")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "含空格":
                            if (warringstring == "6~20位數字或英文字母")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "含符號":
                            if (warringstring == "6~20位數字或英文字母")
                                Console.WriteLine(inputType + option + ":success");
                            break;

                    }

                    break;

                case "確認密碼":
                    warringstring = driver.FindElement(By.XPath("//*[@id='MemberManage']/div[7]/div/div[2]/form/div[4]/div/div[2]")).GetAttribute("innerText");

                    switch (option)
                    {
                        case "未輸入":
                            if (warringstring == "密碼確認為必填")
                                Console.WriteLine(inputType + option + ":success");
                            break;

                        case "少一位":
                            if (warringstring == "密碼不同")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "多一位":
                            if (warringstring == "密碼不同")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "含空格":
                            if (warringstring == "密碼不同")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "含符號":
                            if (warringstring == "密碼不同")
                                Console.WriteLine(inputType + option + ":success");
                            break;


                        case "大小寫顛倒":
                            if (warringstring == "密碼不同")
                                Console.WriteLine(inputType + option + ":success");
                            break;

                    }
                    break;
            }
        }


    }
}
