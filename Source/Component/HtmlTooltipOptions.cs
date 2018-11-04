using Bridge.Html5;

namespace HtmlTooltip.Component
{
    /// <summary>
    ///     工具提示參數
    /// </summary>
    public class TooltipOptions
    {
        public int DisplayTime { get; set; }

        /// <summary>
        ///     淡入時間（毫秒）
        /// </summary>
        public int FadeInTime { get; set; }

        /// <summary>
        ///     淡出時間（毫秒）
        /// </summary>
        public int FadeoutTime { get; set; }

        public float FadeRate { get; set; }

        /// <summary>
        ///     Tooltip 要附加上的 HTML 元素
        /// </summary>
        public HTMLElement HtmlElement { get; set; }

        /// <summary>
        ///     暫停時間（毫秒）
        /// </summary>
        public int PauseTime { get; set; }

        public string TooltipClassName { get; set; }
        public string TooltipContent { get; set; }
        public int TooltipOffsetX { get; set; }
        public int TooltipOffsetY { get; set; }
        public float TooltipOpacity { get; set; }
    }
}