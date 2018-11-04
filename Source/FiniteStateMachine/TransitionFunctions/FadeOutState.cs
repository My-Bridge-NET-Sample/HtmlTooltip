using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip.FiniteStateMachine.TransitionFunctions
{
    /// <summary>
    ///     "淡出" 狀態轉移函數
    /// </summary>
    public class FadeOutState : TransitionFunction
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="stateMachine">狀態機</param>
        /// <param name="tooltip">工具提示物件</param>
        public FadeOutState(StateMachine stateMachine, Tooltip tooltip) : base(stateMachine, tooltip)
        {
            this.State = TooltipState.FadeOut;
        }

        /// <summary>
        ///     滑鼠移動事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseMove(MouseEvent mouseEvent)
        {
            var nextState =
                this.StateMachine.CallTransitionFunction(TooltipState.Display, TooltipEvent.MouseMove, mouseEvent);

            return nextState;
        }

        /// <summary>
        ///     滑鼠離開事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseOut(MouseEvent mouseEvent)
        {
            return this.State;
        }

        /// <summary>
        ///     滑鼠懸停事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseOver(MouseEvent mouseEvent)
        {
            this.Tooltip.MoveTo(mouseEvent.ClientX, mouseEvent.ClientY);

            return TooltipState.FadeIn;
        }

        /// <summary>
        ///     計時器事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnTimeTicks(MouseEvent mouseEvent)
        {
            this.Tooltip.Fade(-this.Tooltip.Options.TooltipOpacity / this.Tooltip.Options.FadeRate);
            if (!(this.Tooltip.CurrentOpacity <= 0))
            {
                return this.State;
            }

            this.StateMachine.CancelTicker();
            this.Tooltip.DeleteTooltip();

            return TooltipState.Inactive;
        }
    }
}