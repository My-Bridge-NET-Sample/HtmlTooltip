namespace HtmlTooltip.FiniteStateMachine
{
    /// <summary>
    ///     工具提示的事件
    /// </summary>
    public enum TooltipEvent
    {
        /// <summary>
        ///     滑鼠懸停
        /// </summary>
        MouseOver,

        /// <summary>
        ///     滑鼠移動
        /// </summary>
        MouseMove,

        /// <summary>
        ///     滑鼠離開
        /// </summary>
        MouseOut,

        /// <summary>
        ///     時間到
        /// </summary>
        TimeOut,

        /// <summary>
        ///     計時器
        /// </summary>
        TimeTicks
    }
}