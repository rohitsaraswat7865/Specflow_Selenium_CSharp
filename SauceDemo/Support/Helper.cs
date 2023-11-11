global using OpenQA.Selenium;
global using OpenQA.Selenium.Support.UI;
global using OpenQA.Selenium.Chrome;
global using System;
global using System.Collections.Generic;
global using System.Reflection;

namespace SauceDemo.Support
{
    public class Helper
    {
        private void FluentRoutine<T>(T t, Func<T,bool> func,TimeSpan pollingInterval, TimeSpan timeOut)
        {
            var defaultWait = new DefaultWait<T>(t)
            {
                PollingInterval = pollingInterval,
                Timeout = timeOut,
                Message = $" Fluent timeout on function -> {func.GetMethodInfo().Name} , polling internal ->{pollingInterval}, timeout -> {timeOut}"
            };
            _ = defaultWait.Until(func);//evaluates function until function returns true
        }
        /// <summary>
        /// TryGetElement
        /// </summary>
        /// <param name="testObject"></param>
        /// <param name="by"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IWebElement? TryGetElement(TestObject testObject, By by, Func<IWebElement, bool> func)
        {
            try
            {
                IWebElement? element = null;
                this.FluentRoutine<IWebDriver>(
                    testObject.Driver,
                      (driver) =>
                      {
                          try
                          {
                              element = driver.FindElement(by);
                          }
                          catch (Exception)
                          {
                              return false;
                          }
                          return true;
                      },
                      TimeSpan.FromSeconds(2),
                      TimeSpan.FromSeconds(50)
                    );

                if (element is not null)
                {
                    this.FluentRoutine<IWebElement>(
                    element,
                    func,
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(30)
                    );
                }
                return element;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        /// <summary>
        /// TryGetElements
        /// </summary>
        /// <param name="testObject"></param>
        /// <param name="by"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IReadOnlyCollection<IWebElement>? TryGetElements(TestObject testObject,By by, Func<IWebElement, bool> func)
        {
            try
            {
                IReadOnlyCollection<IWebElement>? elements = null;
                this.FluentRoutine<IWebDriver> (
                    testObject.Driver,
                      (driver) =>
                      {
                          try
                          {
                              elements = driver.FindElements(by);
                          }
                          catch (Exception)
                          {
                              return false;
                          }
                          return elements is null ? false : true;
                      },
                      TimeSpan.FromSeconds(2),
                      TimeSpan.FromSeconds(50)
                    );

                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        this.FluentRoutine<IWebElement>(
                        element,
                        func,
                        TimeSpan.FromSeconds(4),
                        TimeSpan.FromSeconds(30));
                    }                                   
                }
                return elements;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// TryGetSubElement
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="by"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IWebElement? TryGetSubElement(IWebElement parentElement, By by, Func<IWebElement, bool> func)
        {
            try
            {
                IWebElement? element = null;
                this.FluentRoutine<IWebElement>(
                    parentElement,
                      (parent) =>
                      {
                          try
                          {
                              element = parent.FindElement(by);
                          }
                          catch (Exception)
                          {
                              return false; ;
                          }
                          return true;
                      },
                      TimeSpan.FromSeconds(2),
                      TimeSpan.FromSeconds(50)
                    );

                if (element != null)
                {
                    this.FluentRoutine<IWebElement>(
                    element,
                    func,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5)
                    );
                }
                return element;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
        /// <summary>
        /// CheckIfPageIsLoaded
        /// </summary>
        /// <param name="testObject"></param>
        /// <param name="subString"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void CheckIfPageIsLoaded(TestObject testObject, string subString)
        {
            try
            {
                WebDriverWait explicitWait = new WebDriverWait(testObject.Driver, TimeSpan.FromSeconds(100));
                _ = explicitWait.Until(x => ((IJavaScriptExecutor)testObject.Driver).ExecuteScript("return document.readyState").Equals("complete"));

                this.FluentRoutine(testObject.Driver,
                    (driver) =>
                    {
                        return driver.Url.Contains(subString);
                    },
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
