using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PyDelivery.Behaviors
{
    class LoadListItemsBehavior : Behavior<ListView>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CommandProperty, and it is a bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LoadListItemsBehavior));

        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        #endregion

        #region Method

        /// <summary>
        /// Invoked when added sflistview to the page.
        /// </summary>
        /// <param name="bindableListView">The SfListView</param>
        protected override void OnAttachedTo(ListView bindableListView)
        {
            if (bindableListView != null)
            {
                base.OnAttachedTo(bindableListView);
                bindableListView.ItemTapped += this.BindableListView_ItemTapped;
            }
        }

        /// <summary>
        /// Invoked when exit from the page.
        /// </summary>
        /// <param name="bindableListView">The SfListView</param>
        protected override void OnDetachingFrom(ListView bindableListView)
        {
            if (bindableListView != null)
            {
                base.OnDetachingFrom(bindableListView);
                bindableListView.ItemTapped -= this.BindableListView_ItemTapped;
            }
        }

        /// <summary>
        /// Invoked when tapping the listview item.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">ItemTapped EventArgs</param>
        private void BindableListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (this.Command == null)
            {
                return;
            }

            if (this.Command.CanExecute(e.Item))
            {
                this.Command.Execute(e.Item);
            }
        }

        #endregion
    }
}
