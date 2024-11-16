using System.Text;
using Core;
using OpenQA.Selenium;

namespace QALabs.Automation.Core.Interaction
{
    public class UIElement
    {
        public UIElement(By by)
        {
            By = by;
        }

        public By By { get; set; }

        public UIElement Parent { get; set; }

        public bool InFrame { get; set; }

        public bool HasParent => Parent != null;

        public IEnumerable<UIElement> GetParents()
        {
            var parents = new Stack<UIElement>();

            var parent = Parent;
            while (parent != null)
            {
                parents.Push(parent);
                parent = parent.Parent;
            }

            return parents;
        }

        public UIElement Click()
        {
            SeleniumWebDriver.NativeDriver.FindElement(this).Click();
            return this;
        }

        public void Click(Func<IWebElement, bool> findElementCondition)
        {
            SeleniumWebDriver.NativeDriver.FindElement(this).Click();
        }

        public UIElement ClickUntil(Func<bool> condition, int timeOutInSec = 5)
        {
            WaitHelper.WaitForCondition(
                () =>
            {
                Click();
                return condition();
            }, timeOutInSec);

            return this;
        }

        // public UIElement JSClick()
        // {
        //     SeleniumWebDriver.NativeDriver.ExecuteJS("arguments[0].click()", this);
        //     return this;
        // }

        // public void ActionsClick()
        // {
        //     Driver.Actions.Click().Build().Perform();
        // }

        public void ClickByTextContent(string textContent)
        {
            Click(el => el.GetAttribute("textContent") == textContent);
        }

        public string GetText(Func<IWebElement, bool> findElementCondition = null)
        {
            return SeleniumWebDriver.NativeDriver.FindElement(this).Text;
        }

        public void ClickByTextContentWithoutRegister(string textContent)
        {
            Click(el => el.GetAttribute("textContent").ToLower() == textContent.ToLower());
        }

        public void ClickAnyAvailable()
        {
            Click(el => el.Displayed && el.Enabled);
        }

        public void TryClick(Func<IWebElement, bool> findElementCondition = null)
        {
            WaitHelper.WaitNoError(() => SeleniumWebDriver.NativeDriver.FindElement(this).Click());
        }

        // public void RightClick(Func<IWebElement, bool> findElementCondition)
        // {
        //     var element = SeleniumWebDriver.NativeDriver.FindElement(this);
        //     Driver.Actions.ContextClick(element).Perform();
        // }

        // public string GetTextContent(Func<IWebElement, bool> findElementCondition = null)
        // {
        //     return Driver.FindElement(this, findElementCondition).GetAttribute("textContent");
        // }
        //
        // public string GetSeleniumTextContent(Func<IWebElement, bool> findElementCondition = null)
        // {
        //     return Driver.FindElement(this, findElementCondition).Text;
        // }
        //
        // public string GetValueAttribute(Func<IWebElement, bool> findElementCondition = null)
        // {
        //     return Driver.FindElement(this, findElementCondition).GetAttribute("value");
        // }
        //
        // public List<string> GetElementsText()
        // {
        //     return Driver.FindElements(this).ConvertAll(el => el.Text);
        // }
        //
        // public IEnumerable<IWebElement> GetWebElements()
        // {
        //     return Driver.FindElements(this);
        // }

        // public bool WaitForVisibility(int waitInSec = 10)
        // {
        //     return WaitHelper.WaitForCondition(() => IsVisible(), waitInSec);
        // }
        //
        // public UIElement WaitForNonVisibility(int waitInSec = 10)
        // {
        //     WaitHelper.WaitForCondition(() => !IsVisible(), waitInSec);
        //     return this;
        // }
        //
        // public bool WaitForEnabled(int waitInSec = 5)
        // {
        //     return WaitHelper.WaitForCondition(() => IsEnabled(), waitInSec);
        // }

        // public bool WaitForPresent(int waitInSec = 5)
        // {
        //     return Driver.TryFindElement(this, TimeSpan.FromSeconds(waitInSec)) != null;
        // }
        //
        // public bool WaitForNonPresent(int waitInSec = 5)
        // {
        //     return WaitHelper.WaitForCondition(() => Driver.TryFindElement(this, TimeSpan.FromMilliseconds(500)) == null, waitInSec);
        // }
        //
        // public bool IsVisible(Func<IWebElement, bool> findElementCondition = null)
        // {
        //     return Driver.FindElement(this, findElementCondition).Displayed;
        // }
        //
        // public bool IsEnabled()
        // {
        //     return Driver.FindElement(this).Enabled;
        // }
        //
        // public bool IsPresent()
        // {
        //     try
        //     {
        //         Driver.FindElement(this);
        //     }
        //     catch
        //     {
        //         return false;
        //     }
        //
        //     return true;
        // }
        //
        // public UIElement ScrollIntoCenterOfView()
        // {
        //     var script = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
        //                     + "var elementTop = arguments[0].getBoundingClientRect().top;"
        //                     + "window.scrollBy(0, elementTop-(viewPortHeight/2));";
        //
        //     Driver.ExecuteJS(script, this);
        //
        //     return this;
        // }
        //
        // public string GetAttribute(string attribute)
        // {
        //     return Driver.FindElement(this).GetAttribute(attribute);
        // }
        //
        // public IEnumerable<string> GetAttributes(string attribute)
        // {
        //     return Driver.FindElements(this).Select(el => el.GetAttribute(attribute));
        // }
        //
        // public override string ToString()
        // {
        //     return By.ToString();
        // }
        //
        // public void SetJsProperty(string property, string value)
        // {
        //     Driver.ExecuteJS($"arguments[0].{property}='{value}';", this);
        // }
        //
        // public void RemoveAttribute(string attribute, string value)
        // {
        //     Driver.ExecuteJS($"arguments[0].removeAttribute('{attribute}','{value}')", this);
        // }
        //
        // public void DeleteAttribute(string attribute)
        // {
        //     Driver.ExecuteJS($"arguments[0].setAttribute('{attribute}', null);", this);
        // }
        //
        // public void TriggerEvent(string eventName)
        // {
        //     var builder = new StringBuilder();
        //
        //     builder.Append($"var event = new Event('{eventName}');");
        //     builder.Append("arguments[0].dispatchEvent(event);");
        //
        //     Driver.ExecuteJS(builder.ToString(), this);
        // }

        // public void TriggerFocusEvent() => TriggerEvent("focus");
        //
        // public void TriggerBlurEvent() => TriggerEvent("blur");
        //
        // public void TriggerChangeEvent() => TriggerEvent("change");
        //
        // public void TriggerMouseOverEvent() => TriggerEvent("onmouseover");
    }
}
