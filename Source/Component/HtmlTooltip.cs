using System;
using Bridge.Html5;
using HtmlTooltip.FiniteStateMachine;

namespace HtmlTooltip.Component
{
    /// <summary>
    ///     工具提示
    /// </summary>
    public class Tooltip
    {
        /// <summary>
        ///     狀態機
        /// </summary>
        private readonly StateMachine StateMachine;

        /// <summary>
        ///     游標 x 位置
        /// </summary>
        private int LastCursorX;

        /// <summary>
        ///     游標 y 位置
        /// </summary>
        private int LastCursorY;

        /// <summary>
        ///     工具提示的 Div
        /// </summary>
        private HTMLDivElement TooltipDiv;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="options">參數</param>
        public Tooltip(TooltipOptions options)
        {
            this.Options = options;
            this.BindEvents();
            this.StateMachine = new StateMachine(this);
        }

        /// <summary>
        ///     目前不透明度
        /// </summary>
        public double CurrentOpacity { get; set; }

        /// <summary>
        ///     參數
        /// </summary>
        public TooltipOptions Options { get; }

        /// <summary>
        ///     建立工具提示
        /// </summary>
        public void CreateTooltip()
        {
            this.TooltipDiv = new HTMLDivElement
            {
                InnerHTML = this.Options.TooltipContent
            };

            Document.Body.AppendChild(this.TooltipDiv);
            this.SetStyle();
            this.SetPosition();
        }

        /// <summary>
        ///     刪除工具提示
        /// </summary>
        public void DeleteTooltip()
        {
            if (this.TooltipDiv == null)
            {
                return;
            }

            Document.Body.RemoveChild(this.TooltipDiv);
            this.TooltipDiv = null;
        }

        /// <summary>
        ///     改變不透明度
        /// </summary>
        /// <param name="opacityDelta">不透明度增減值</param>
        public void Fade(double opacityDelta)
        {
            this.CurrentOpacity = Math.Round((this.CurrentOpacity + opacityDelta) * 1E6) / 1E6;
            if (this.CurrentOpacity < 0)
            {
                this.CurrentOpacity = 0;
            }

            if (this.CurrentOpacity > this.Options.TooltipOpacity)
            {
                this.CurrentOpacity = this.Options.TooltipOpacity;
            }

            this.TooltipDiv.Style.Opacity = this.CurrentOpacity.ToString();
        }

        /// <summary>
        ///     移動工具提示
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public void MoveTo(int x, int y)
        {
            this.TooltipDiv.Style.Left = x + this.Options.TooltipOffsetX + "px";
            this.TooltipDiv.Style.Top = y + this.Options.TooltipOffsetY + "px";
        }

        /// <summary>
        ///     記錄游標位置
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public void SaveCursorPosition(int x, int y)
        {
            this.LastCursorX = x;
            this.LastCursorY = y;
        }

        /// <summary>
        ///     繫結事件
        /// </summary>
        private void BindEvents()
        {
            // HTML 元素滑鼠移動事件
            this.Options.HtmlElement.OnMouseMove = ev => { this.StateMachine.HandleEvent(TooltipEvent.MouseMove, ev); };

            // HTML 元素滑鼠懸停事件
            this.Options.HtmlElement.OnMouseOver = ev => { this.StateMachine.HandleEvent(TooltipEvent.MouseOver, ev); };

            // HTML 元素滑鼠離開事件
            this.Options.HtmlElement.OnMouseOut = ev => { this.StateMachine.HandleEvent(TooltipEvent.MouseOut, ev); };
        }

        /// <summary>
        ///     設定預設樣式
        /// </summary>
        private void SetDefaultStyle()
        {
            this.TooltipDiv.Style.MinWidth = "25px";
            this.TooltipDiv.Style.MaxWidth = "350px";
            this.TooltipDiv.Style.Height = "auto";
            this.TooltipDiv.Style.Border = "thin solid black";
            this.TooltipDiv.Style.Padding = "5px";
            this.TooltipDiv.Style.BackgroundColor = "yellow";
        }

        /// <summary>
        ///     設定位置
        /// </summary>
        private void SetPosition()
        {
            this.TooltipDiv.Style.Position = Position.Absolute;
            this.TooltipDiv.Style.ZIndex = "101";
            this.TooltipDiv.Style.Left = this.LastCursorX + this.Options.TooltipOffsetX + "px";
            this.TooltipDiv.Style.Top = this.LastCursorY + this.Options.TooltipOffsetY + "px";
        }

        /// <summary>
        ///     設定樣式
        /// </summary>
        private void SetStyle()
        {
            // 如果沒有指定 Class
            if (string.IsNullOrEmpty(this.Options.TooltipClassName))
            {
                this.SetDefaultStyle();
            }
            else
            {
                this.TooltipDiv.ClassName = this.Options.TooltipClassName;
            }

            this.TooltipDiv.Style.Opacity = "0";
            this.CurrentOpacity = 0;
        }
    }
}