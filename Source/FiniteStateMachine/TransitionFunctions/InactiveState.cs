using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip.FiniteStateMachine.TransitionFunctions
{
    /// <summary>
    ///     "未啟動" 狀態轉移函數
    /// </summary>
    public class InactiveState : TransitionFunction
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="stateMachine">狀態機</param>
        /// <param name="tooltip">工具提示物件</param>
        public InactiveState(StateMachine stateMachine, Tooltip tooltip) : base(stateMachine, tooltip)
        {
            this.State = TooltipState.Inactive;
        }

        /// <summary>
        ///     滑鼠移動事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseMove(MouseEvent mouseEvent)
        {
            // 呼叫未啟動狀態的滑鼠懸停事件
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
            return this.State;
        }

        /// <summary>
        ///     滑鼠懸停事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public override TooltipState OnMouseOver(MouseEvent mouseEvent)
        {
            // 取消計時器
            this.StateMachine.CancelTimer();

            // 記錄游標位置
            this.Tooltip.SaveCursorPosition(mouseEvent.ClientX, mouseEvent.ClientY);

            // 開始計時
            this.StateMachine.StartPauseTimer();

            // 轉移到 Pause 狀態
            return TooltipState.Pause;
        }
    }
}