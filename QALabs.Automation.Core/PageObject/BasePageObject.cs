using System.Collections.Generic;
using Core;
using OpenQA.Selenium;
using QALabs.Automation.Core.Helpers;


namespace Internal.BoardGames.Core.Web.PageObject
{
    public abstract class BasePageObject
    {
        protected BasePageObject()
        {
        }

        protected BasePageObject(UIElement rootLocator)
        {
            Root = rootLocator;
        }

        protected BasePageObject(UIElement rootLocator, UIElement parentLocator)
        {
            Root = rootLocator;
            Parent = parentLocator;

            Root.Parent = Parent;
        }

        public UIElement Root { get; set; }

        public UIElement Parent { get; set; }

        public bool HasRoot => Root != null;

        public bool HasParent => Parent != null;

        public TElement Locator<TElement>(TElement locator)
            where TElement : UIElement
        {
            if (HasRoot)
            {
                locator.Parent = Root;
            }

            return locator;
        }

        // public void TryAcceptPopup()
        // {
        //     SeleniumWebDriver.NativeDriver.TryAcceptPopup();
        // }

        // public void RefreshPage()
        // {
        //     SeleniumWebDriver.NativeDriver.RefreshPage();
        // }

        public void WaitForCountEquals(int elementsCount, int expectedCount)
        {
            WaitHelper.WaitForEquals(() => elementsCount, expectedCount);
        }

        protected TextElement TextElementByXPath(string xpath) => Locator(new TextElement(By.XPath(xpath)));

        protected TextElement TextElementById(string id) => Locator(new TextElement(By.Id(id)));

        protected TextElement TextElementByCss(string css) => Locator(new TextElement(By.CssSelector(css)));

        protected UIElement UIElementByXPath(string xpath) => Locator(new UIElement(By.XPath(xpath)));

        protected UIElement UIElementByCss(string css) => Locator(new UIElement(By.CssSelector(css)));

        protected UIElement UIElementById(string id) => Locator(new UIElement(By.Id(id)));

        // protected UIElement UiElementByXPathInFrame(string xpath, UiElement parent = null) => Locator(new UIElement(By.XPath(xpath))
        // {
        //     InFrame = true,
        //     Parent = parent,
        // });
        //
        // protected UIElement UiElementByCssInFrame(string css, UiElement parent = null) => Locator(new UIElement(By.CssSelector(css))
        // {
        //     InFrame = true,
        //     Parent = parent,
        // });
        //
        // protected UIElement UiElementByIdInFrame(string id, UiElement parent = null) => Locator(new UIElement(By.Id(id))
        // {
        //     InFrame = true,
        //     Parent = parent,
        // });

        // protected SelectElement SelectElementByXPath(string xpath) => Locator(new SelectElement(By.XPath(xpath)));
        //
        // protected SelectElement SelectElementById(string id) => Locator(new SelectElement(By.Id(id)));
        //
        // protected SelectElement SelectElementByCss(string css) => Locator(new SelectElement(By.CssSelector(css)));
        //
        // protected SelectElement SelectElementByIdInFrame(string id, UiElement parent = null) => Locator(new SelectElement(By.Id(id))
        // {
        //     InFrame = true,
        //     Parent = parent,
        // });
        //
        // protected FileInput FileInputByXPath(string xpath) => Locator(new FileInput(By.XPath(xpath)));
        //
        // protected FileInput FileInputById(string id) => Locator(new FileInput(By.Id(id)));
        //
        // protected TableBase FindTableByXPath(string xpath) => Locator(new TableBase(By.XPath(xpath)));
        //
        // protected void SwitchToLastWindow(bool isPreviousWindowExist = true)
        // {
        //     Driver.SwitchToLastWindow(isPreviousWindowExist);
        // }
        //
        // protected bool IsPageLast()
        // {
        //     return Driver.IsPageLast();
        // }
        //
        // protected string CurrentUrl()
        // {
        //     return Driver.CurrentUrl();
        // }
        //
        // protected void PressActionKey(string keys)
        // {
        //     Driver.PressKey(keys);
        // }
        //
        // protected bool WaitForPageLoadComplete(int timeInSec = 5)
        // {
        //     return Driver.WaitForPageLoad(timeInSec);
        // }
        //
        // protected bool WaitForAjaxComplete(int timeInSec = 5)
        // {
        //     return Driver.WaitForAjax(timeInSec);
        // }
        //
        // protected bool AcceptPopupIfPresent()
        // {
        //     return Driver.AcceptPopupIfPresent();
        // }
        //
        // protected void WaitForWindowCountToBe(int windowCount, int timeOutInSec = 3)
        // {
        //     WaitHelper.WaitForCondition(() => Driver.NativeDriver.WindowHandles.Count == windowCount, timeOutInSec);
        // }
        //
        // protected string GetUrlText()
        // {
        //     return Driver.GetUrl();
        // }
        //
        // protected List<string> GetWindowHandles()
        // {
        //     return Driver.GetWindowHandles();
        // }
        //
        // protected void SwitchToWindow(string switchWindow)
        // {
        //     Driver.SwitchToWindow(switchWindow);
        // }
    }
}
