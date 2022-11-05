using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ItemsControl2
{
    public class WheelScrollKillBehavior : Behavior<ScrollViewer>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
        }
        void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            var newEventArgs = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = UIElement.MouseWheelEvent
            };
            ((UIElement)AssociatedObject.Parent).RaiseEvent(newEventArgs);
        }
    }
}
