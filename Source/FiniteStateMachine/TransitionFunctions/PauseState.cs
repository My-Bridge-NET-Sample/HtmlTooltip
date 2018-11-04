using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip.FiniteStateMachine.TransitionFunctions
{
    /// <summary>
    ///     "暫停" 狀態轉移函數
    /// </summary>
    public class PauseState : TransitionFunction
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="stateMachine">狀態機</param>
        /// <param name="tooltip">工具提示物件</param>
        public PauseState(StateMachine stateMachine, Tooltip tooltip) : base(stateMachine, tooltip)
        {
            this.State = TooltipState.Pause;
        }

        /// <summary>
        ///     滑鼠移動事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseMove(MouseEvent mouseEvent)
        {
            // 呼叫未啟動狀態的滑鼠移動事件
            var nextState =
                this.StateMachine.CallTransitionFunction(TooltipState.Inactive, TooltipEvent.MouseOver, mouseEvent);

            return nextState;
        }

        /// <summary>
        ///     滑鼠離開事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseOut(MouseEvent mouseEvent)
        {
            this.StateMachine.CancelTimer();

            return TooltipState.Inactive;
        }

        /// <summary>
        ///     時間到事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnTimeOut(MouseEvent mouseEvent)
        {
            this.StateMachine.CancelTimer();
            this.Tooltip.CreateTooltip();

            // 如果沒有指定淡入時間
            if (this.Tooltip.Options.FadeInTime == 0)
            {
                // 直接設定透明度
                this.Tooltip.Fade(this.Tooltip.Options.TooltipOpacity);

                // 開始倒數計時
                this.StateMachine.StartDisplayTimer();

                // 轉移到 "Display" 狀態
                return TooltipState.Display;
            }

            // 啟動計時器，轉移到 "FadeIn" 狀態
            this.StateMachine.StartTicker((int) (1000 / this.Tooltip.Options.FadeRate));
            return TooltipState.FadeIn;
        }
    }
}