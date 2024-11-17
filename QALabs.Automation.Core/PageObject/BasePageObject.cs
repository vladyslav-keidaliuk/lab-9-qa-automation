using OpenQA.Selenium;
using QALabs.Automation.Core.Helpers;

namespace QALabs.Automation.Core.PageObject
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

    }
}
