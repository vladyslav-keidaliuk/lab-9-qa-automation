using Core;
using OpenQA.Selenium;

namespace QALabs.Automation.Core.Interaction
{
    public class TextElement : UIElement
    {
        public TextElement(By by)
            : base(by)
        {
        }

        // public TextElement JSEnterText(string textValue)
        // {
        //     SetJsProperty("value", textValue);
        //     return this;
        // }

        public void EnterNotEmptyText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                EnterText(text);
            }
        }

        // public TextElement JSEnterNotEmptyText(string textValue)
        // {
        //     if (!string.IsNullOrEmpty(textValue))
        //     {
        //         SetJsProperty("value", textValue);
        //     }
        //
        //     return this;
        // }

        public TextElement EnterText(string textValue, bool cleartext = true)
        {
            if (cleartext)
            {
                ClearText();
            }

            SeleniumWebDriver.NativeDriver.FindElement(this).SendKeys(textValue);

            return this;
        }

        public void ClearText()
        {
            SeleniumWebDriver.NativeDriver.FindElement(this).Clear();
        }

        // public TextElement TryEnterTextNotEmpty(string textValue, int timeoutInSec = 10, string expectedValue = null, bool loseFocusAfterEnter = false)
        // {
        //     expectedValue ??= textValue;
        //     WaitHelper.WaitForCondition(
        //         () =>
        //     {
        //         EnterNotEmptyText(textValue);
        //
        //         if (loseFocusAfterEnter)
        //         {
        //             TriggerBlurEvent();
        //         }
        //
        //         return GetValue() == expectedValue;
        //     }, timeoutInSec);
        //     return this;
        // }

        // public TextElement TryEnterText(string textValue, int timeoutInSec = 10, string expectedValue = null, bool loseFocusAfterEnter = false)
        // {
        //     expectedValue ??= textValue;
        //     WaitHelper.WaitForCondition(
        //         () =>
        //     {
        //         EnterText(textValue);
        //
        //         if (loseFocusAfterEnter)
        //         {
        //             TriggerBlurEvent();
        //         }
        //
        //         return GetValue() == expectedValue;
        //     }, timeoutInSec);
        //     return this;
        // }

        public void Submit()
        {
            EnterText(Keys.Enter, false);
        }

        public void EnterTextAndSubmit(string textValue)
        {
            EnterText(textValue);
            Submit();
        }

        public string GetValue()
        {
            return GetText();
        }
    }
}
