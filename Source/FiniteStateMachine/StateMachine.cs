using System.Collections.Generic;
using Bridge.Html5;
using HtmlTooltip.Component;
using HtmlTooltip.FiniteStateMachine.TransitionFunctions;

namespace HtmlTooltip.FiniteStateMachine
{
    /// <summary>
    ///     有限狀態機
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        ///     目前狀態
        /// </summary>
        private TooltipState CurrentState = TooltipState.None;

        /// <summary>
        ///     目前狀態的處理函式
        /// </summary>
        private TransitionFunction CurrentTransitionFunction;

        /// <summary>
        ///     目前計時器
        /// </summary>
        private int TickerId;

        /// <summary>
        ///     工具提示物件
        /// </summary>
        private readonly Tooltip Tooltip;

        /// <summary>
        ///     狀態轉移函式對照表
        /// </summary>
        private Dictionary<TooltipState, TransitionFunction> TransitionFunctions;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tooltip">Tooltip 物件</param>
        public StateMachine(Tooltip tooltip)
        {
            this.Tooltip = tooltip;
            this.InitTransitionFunctions();
            this.StateTransition(TooltipState.Inactive);
        }

        /// <summary>
        ///     計時器編號
        /// </summary>
        public int TimerId { get; set; }

        /// <summary>
        ///     呼叫狀態處理含式
        /// </summary>
        /// <param name="anotherState">狀態</param>
        /// <param name="anotherEvent">事件類別</param>
        /// <param name="mouseEvent">滑鼠事件</param>
        /// <returns>回傳下一個狀態</returns>
        public TooltipState CallTransitionFunction(TooltipState anotherState, TooltipEvent anotherEvent,
            MouseEvent mouseEvent)
        {
            // 取得狀態轉移函式
            if (!this.TransitionFunctions.TryGetValue(anotherState, out var transitionFunction))
            {
                throw new UnexpectedStateException();
            }

            // 呼叫狀態轉移函式
            var nextState = transitionFunction.HandledEvent(anotherEvent, mouseEvent);

            return nextState;
        }

        /// <summary>
        ///     取消
        /// </summary>
        public void CancelTicker()
        {
            if (this.TickerId != 0)
            {
                Window.ClearTimeout(this.TickerId);
            }

            this.TickerId = 0;
        }

        /// <summary>
        ///     取消計時器
        /// </summary>
        public void CancelTimer()
        {
            if (this.TimerId != 0)
            {
                Window.ClearTimeout(this.TimerId);
            }

            this.TimerId = 0;
        }

        /// <summary>
        ///     刪除工具提示
        /// </summary>
        public void DeleteTooltip()
        {
            this.Tooltip.DeleteTooltip();
        }

        /// <summary>
        ///     事件處理
        /// </summary>
        /// <param name="eveneType">事件類別</param>
        /// <param name="mouseEvent">滑鼠事件</param>
        public void HandleEvent(TooltipEvent eveneType, MouseEvent mouseEvent)
        {
            if (this.CurrentTransitionFunction == null)
            {
                throw new UnexpectedStateException();
            }

            var nextState = this.CurrentTransitionFunction.HandledEvent(eveneType, mouseEvent);
            this.StateTransition(nextState);
        }

        /// <summary>
        ///     倒數計時
        /// </summary>
        public void StartDisplayTimer()
        {
            this.TimerId = Window.SetTimeout(
                () => this.HandleEvent(TooltipEvent.TimeOut, null),
                this.Tooltip.Options.DisplayTime);
        }

        /// <summary>
        ///     倒數計時
        /// </summary>
        public void StartPauseTimer()
        {
            this.TimerId = Window.SetTimeout(
                () => this.HandleEvent(TooltipEvent.TimeOut, null),
                this.Tooltip.Options.PauseTime);
        }

        /// <summary>
        ///     啟動計時器
        /// </summary>
        /// <param name="interval">間隔毫秒</param>
        public void StartTicker(int interval)
        {
            this.TickerId = Window.SetInterval(
                () => this.HandleEvent(TooltipEvent.TimeTicks, null),
                interval);
        }

        /// <summary>
        ///     轉移至新狀態
        /// </summary>
        /// <param name="newState">新狀態</param>
        /// <returns>移轉後的狀態</returns>
        public TooltipState StateTransition(TooltipState newState)
        {
            // 狀態相同，不移轉
            if (newState == this.CurrentState)
            {
                return this.CurrentState;
            }

            // 取得新狀態的狀態轉移函式
            if (!this.TransitionFunctions.TryGetValue(newState, out this.CurrentTransitionFunction))
            {
                throw new UnexpectedStateException();
            }

            this.CurrentState = newState;

            return this.CurrentState;
        }

        /// <summary>
        ///     初始化狀態轉移函式對照表
        /// </summary>
        private void InitTransitionFunctions()
        {
            this.TransitionFunctions = new Dictionary<TooltipState, TransitionFunction>
            {
                {TooltipState.Inactive, new InactiveState(this, this.Tooltip)},
                {TooltipState.Pause, new PauseState(this, this.Tooltip)},
                {TooltipState.FadeIn, new FadeInState(this, this.Tooltip)},
                {TooltipState.Display, new DisplayState(this, this.Tooltip)},
                {TooltipState.FadeOut, new FadeOutState(this, this.Tooltip)}
            };
        }
    }
}