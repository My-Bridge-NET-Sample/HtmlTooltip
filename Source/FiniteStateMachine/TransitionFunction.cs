using System.Collections.Generic;
using Bridge.Html5;
using HtmlTooltip.Component;

namespace HtmlTooltip.FiniteStateMachine
{
    /// <summary>
    ///     轉移函數
    /// </summary>
    public class TransitionFunction
    {
        /// <summary>
        ///     事件處理函式對照表
        /// </summary>
        private Dictionary<TooltipEvent, EventHandler> EventHandlers;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="stateMachine">狀態機</param>
        /// <param name="tooltip">工具提示物件</param>
        public TransitionFunction(StateMachine stateMachine, Tooltip tooltip)
        {
            this.StateMachine = stateMachine;
            this.Tooltip = tooltip;
            this.InitEventHandlers();
        }

        /// <summary>
        ///     此轉移函數對應的狀態
        /// </summary>
        public TooltipState State { get; set; }

        /// <summary>
        ///     工具提示物件
        /// </summary>
        protected Tooltip Tooltip { get; set; }

        /// <summary>
        ///     狀態機
        /// </summary>
        protected StateMachine StateMachine { get; set; }

        /// <summary>
        ///     處理事件
        /// </summary>
        /// <param name="tooltipEvent">事件類別</param>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public TooltipState HandledEvent(TooltipEvent tooltipEvent, MouseEvent mouseEvent)
        {
            // 取得事件對應的處理函式
            if (!this.EventHandlers.TryGetValue(tooltipEvent, out var handler))
            {
                throw new UnexpectedEventException();
            }

            // 呼叫處理函式
            var nextState = handler(mouseEvent);

            return nextState;
        }

        /// <summary>
        ///     滑鼠移動事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public virtual TooltipState OnMouseMove(MouseEvent mouseEvent)
        {
            throw new UnexpectedEventException();
        }

        /// <summary>
        ///     滑鼠離開事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public virtual TooltipState OnMouseOut(MouseEvent mouseEvent)
        {
            throw new UnexpectedEventException();
        }

        /// <summary>
        ///     滑鼠懸停事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public virtual TooltipState OnMouseOver(MouseEvent mouseEvent)
        {
            throw new UnexpectedEventException();
        }

        /// <summary>
        ///     時間到事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public virtual TooltipState OnTimeOut(MouseEvent mouseEvent)
        {
            throw new UnexpectedEventException();
        }

        /// <summary>
        ///     計時器事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        public virtual TooltipState OnTimeTicks(MouseEvent mouseEvent)
        {
            throw new UnexpectedEventException();
        }

        /// <summary>
        ///     初始化事件處理函式對照表
        /// </summary>
        private void InitEventHandlers()
        {
            this.EventHandlers = new Dictionary<TooltipEvent, EventHandler>
            {
                {TooltipEvent.MouseMove, this.OnMouseMove},
                {TooltipEvent.MouseOut, this.OnMouseOut},
                {TooltipEvent.MouseOver, this.OnMouseOver},
                {TooltipEvent.TimeOut, this.OnTimeOut},
                {TooltipEvent.TimeTicks, this.OnTimeTicks}
            };
        }

        /// <summary>
        ///     事件處理函式
        /// </summary>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>下一個狀態</returns>
        private delegate TooltipState EventHandler(MouseEvent mouseEvent);
    }
}