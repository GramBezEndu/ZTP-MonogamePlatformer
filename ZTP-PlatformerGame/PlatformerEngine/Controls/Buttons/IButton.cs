using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Controls.Buttons
{
    public interface IButton : IDrawableComponent
    {
        EventHandler OnClick { get; set; }
        /// <summary>
        /// You can use this event handler to perform action when button starts or stops being higlighted
        /// for example hide/show other menu components
        /// </summary>
        EventHandler OnSelectedChange { get; set; }
        bool Selected { get; set; }
    }
}
