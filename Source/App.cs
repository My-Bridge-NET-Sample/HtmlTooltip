using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip
{
    public class App
    {
        public static void Main()
        {
            var testInput = Document.GetElementById("TestInput");
            if (testInput != null)
            {
                var tooltip = new Tooltip(new TooltipOptions
                {
                    TooltipContent = "請輸入以下的項目：您的銀行帳號與密碼",
                    DisplayTime = 10000,
                    FadeInTime = 1000,
                    FadeoutTime = 3000,
                    FadeRate = 24,
                    PauseTime = 500,
                    TooltipOpacity = (float) 0.8,
                    TooltipOffsetX = 10,
                    TooltipOffsetY = 10,
                    HtmlElement = testInput
                });
            }
        }
    }
}