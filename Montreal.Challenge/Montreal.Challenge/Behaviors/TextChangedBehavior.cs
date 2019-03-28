using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Montreal.Challenge.Behaviors
{
    public class TextChangedBehavior : Behavior<SearchBar>
    {
        public static readonly BindableProperty FocusedCommandProperty = BindableProperty.Create("FocusedCommand", typeof(ICommand), typeof(TextChangedBehavior), null);
        public static readonly BindableProperty UnFocusedCommandProperty = BindableProperty.Create("UnFocusedCommand", typeof(ICommand), typeof(TextChangedBehavior), null);

        public ICommand FocusedCommand
        {
            get
            {
                return (ICommand)GetValue(FocusedCommandProperty);
            }
            set
            {
                SetValue(FocusedCommandProperty, value);
            }
        }

        public ICommand UnFocusedCommand
        {
            get
            {
                return (ICommand)GetValue(UnFocusedCommandProperty);
            }
            set
            {
                SetValue(UnFocusedCommandProperty, value);
            }
        }

        public SearchBar AssociatedObject
        {
            get;
            private set;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        protected override void OnAttachedTo(SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.TextChanged += Bindable_TextChanged;
            bindable.Focused += Bindable_Focused;
            bindable.Unfocused += Bindable_Unfocused;
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.TextChanged -= Bindable_TextChanged;
            bindable.Focused -= Bindable_Focused;            
            bindable.Unfocused -= Bindable_Unfocused;
        }

        private void Bindable_Focused(object sender, FocusEventArgs e)
        {
            FocusedCommand?.Execute(null);
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            UnFocusedCommand?.Execute(null);
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
        }
    }
}
