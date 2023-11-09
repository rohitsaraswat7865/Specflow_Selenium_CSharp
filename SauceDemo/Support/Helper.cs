using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Support
{
    public class Helper
    {
        private R FluentRoutine<T,R>(T t, Func<T, R> func,TimeSpan pollingInterval, TimeSpan timeOut)
        {
            var defaultWait = new DefaultWait<T>(t);
            defaultWait.PollingInterval = pollingInterval;
            defaultWait.Timeout = timeOut;
            defaultWait.Message = $" Fluent timeout on type {t.GetType()}";
            return defaultWait.Until(func);
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
                IWebElement element;
                element = this.FluentRoutine<IWebDriver, IWebElement>(
                    testObject.Driver,
                      (driver) =>
                      {
                          return driver.FindElement(by);
                      },
                      TimeSpan.FromSeconds(2),
                      TimeSpan.FromSeconds(6)
                    );

                if (element != null)
                {
                    var rslt = this.FluentRoutine<IWebElement, bool>(
                    element,
                    func,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5)
                    );

                    if(rslt == true)
                    {
                        return element;
                    }
                }
                return null;
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
        public IReadOnlyCollection<IWebElement> TryGetElements(TestObject testObject,By by, Func<IWebElement, bool> func)
        {
            try
            {
                IReadOnlyCollection<IWebElement> elements;
                elements = this.FluentRoutine<IWebDriver, IReadOnlyCollection < IWebElement >> (
                    testObject.Driver,
                      (driver) =>
                      {
                          return driver.FindElements(by);
                      },
                      TimeSpan.FromSeconds(2),
                      TimeSpan.FromSeconds(6)
                    );

                if (elements != null)
                {
                    foreach(var element in elements)
                    {
                    var rslt = this.FluentRoutine<IWebElement, bool>(
                    element,
                    func,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5)
                   
                    );
                        if (rslt == false) return null;
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
                IWebElement element;
                element = this.FluentRoutine<IWebElement, IWebElement>(
                    parentElement,
                      (parent) =>
                      {
                          return parent.FindElement(by);
                      },
                      TimeSpan.FromSeconds(2),
                      TimeSpan.FromSeconds(6)
                    );

                if (element != null)
                {
                    var rslt = this.FluentRoutine<IWebElement, bool>(
                    element,
                    func,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5)
                    );

                    if (rslt == true)
                    {
                        return element;
                    }
                }
                return null;
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
        public bool CheckIfPageIsLoaded(TestObject testObject, string subString)
        {
            try
            {
                WebDriverWait explicitWait = new WebDriverWait(testObject.Driver, TimeSpan.FromSeconds(100));
                explicitWait.Until(x => ((IJavaScriptExecutor)testObject.Driver).ExecuteScript("return document.readyState").Equals("complete"));

                return this.FluentRoutine(testObject.Driver,
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
