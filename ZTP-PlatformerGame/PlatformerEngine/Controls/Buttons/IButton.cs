namespace PlatformerEngine.Controls.Buttons
{
    using System;

    public interface IButton : IDrawableComponent
    {
        event EventHandler OnClick;

        /// <summary>
        /// You can use this event handler to perform an action when button starts or stops being selected (highlighted)
        /// </summary>
        event EventHandler OnSelectedChanged;

        bool Selected { get; set; }
    }
}
