using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip.FiniteStateMachine.TransitionFunctions
{
    /// <summary>
    ///     淡入狀態移轉函數
    /// </summary>
    public class FadeInState : TransitionFunction
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="stateMachine">狀態機</param>
        /// <param name="tooltip">工具提示物件</param>
        public FadeInState(StateMachine stateMachine, Tooltip tooltip) : base(stateMachine, tooltip)
        {
            this.State = TooltipState.FadeIn;
        }

        /// <summary>
        ///     滑鼠移動事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseMove(MouseEvent mouseEvent)
        {
            this.Tooltip.MoveTo(mouseEvent.ClientX, mouseEvent.ClientY);

            return this.State;
        }

        /// <summary>
        ///     滑鼠離開事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseOut(MouseEvent mouseEvent)
        {
            return TooltipState.FadeOut;
        }

        /// <summary>
        ///     計時器事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnTimeTicks(MouseEvent mouseEvent)
        {
            this.Tooltip.Fade(this.Tooltip.Options.TooltipOpacity / this.Tooltip.Options.FadeRate);
            if (this.Tooltip.CurrentOpacity < this.Tooltip.Options.TooltipOpacity)
            {
                return this.State;
            }

            this.StateMachine.CancelTicker();
            this.StateMachine.StartDisplayTimer();

            return TooltipState.Display;
        }
    }
}