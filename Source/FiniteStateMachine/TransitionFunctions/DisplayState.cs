using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip.FiniteStateMachine.TransitionFunctions
{
    /// <summary>
    ///     "顯示" 狀態轉移函數
    /// </summary>
    public class DisplayState : TransitionFunction
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="stateMachine">狀態機</param>
        /// <param name="tooltip">工具提示物件</param>
        public DisplayState(StateMachine stateMachine, Tooltip tooltip) : base(stateMachine, tooltip)
        {
            this.State = TooltipState.Display;
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
            var nextState =
                this.StateMachine.CallTransitionFunction(TooltipState.Display, TooltipEvent.TimeOut, mouseEvent);

            return nextState;
        }

        /// <summary>
        ///     時間到事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnTimeOut(MouseEvent mouseEvent)
        {
            this.StateMachine.CancelTimer();
            if (this.Tooltip.Options.FadeoutTime > 0)
            {
                this.StateMachine.StartTicker((int) (1000 / this.Tooltip.Options.FadeRate));

                return TooltipState.FadeOut;
            }

            this.Tooltip.DeleteTooltip();

            return TooltipState.Inactive;
        }
    }
}