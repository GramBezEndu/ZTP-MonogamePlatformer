namespace PlatformerEngine.Controls.Buttons
{
    using System;

    public interface IButton : IDrawableComponent
    {
        EventHandler OnClick { get; set; }

        /// <summary>
        /// You can use this event handler to perform an action when button starts or stops being selected (highlighted)
        /// </summary>
        EventHandler OnSelectedChange { get; set; }

        bool Selected { get; set; }
    }
}
