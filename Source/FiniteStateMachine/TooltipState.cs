namespace HtmlTooltip.FiniteStateMachine
{
    /// <summary>
    ///     工具提示的狀態
    /// </summary>
    public enum TooltipState
    {
        /// <summary>
        ///     無效的狀態
        /// </summary>
        None = 0,

        /// <summary>
        ///     未啟動
        /// </summary>
        Inactive,

        /// <summary>
        ///     暫停
        /// </summary>
        Pause,

        /// <summary>
        ///     淡入
        /// </summary>
        FadeIn,

        /// <summary>
        ///     顯示
        /// </summary>
        Display,

        /// <summary>
        ///     淡出
        /// </summary>
        FadeOut
    }
}