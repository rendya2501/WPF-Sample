using Microsoft.Xaml.Behaviors;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ItemsControl2
{
    /// <summary>
    /// ScrollViewerのスクロール位置を同期するビヘイビア
    /// </summary>
    public class ScrollSyncronizingBehavior : Behavior<Control>
    {
        static readonly Dictionary<string, List<Control>> SyncGroups = new();

        private static readonly DependencyProperty ScrollGroupProperty = DependencyProperty.Register(
            "ScrollGroup",
            typeof(string),
            typeof(ScrollSyncronizingBehavior),
            new FrameworkPropertyMetadata((d, e) =>
            {
                if (d is ScrollSyncronizingBehavior me)
                {
                    me.RemoveSyncGroup((string)e.OldValue);
                    me.AddSyncGroup((string)e.NewValue);
                }
            })
        );
        /// <summary>
        /// スクロールグループ
        /// </summary>
        public string ScrollGroup
        {
            get { return (string)GetValue(ScrollGroupProperty); }
            set { SetValue(ScrollGroupProperty, value); }
        }


        private static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation",
            typeof(Orientation),
            typeof(ScrollSyncronizingBehavior),
            new FrameworkPropertyMetadata()
        );
        /// <summary>
        /// スクロールの向き
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AddSyncGroup(ScrollGroup);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            RemoveSyncGroup(ScrollGroup);
        }

        /// <summary>
        /// 同期グループに追加するメソッド
        /// </summary>
        /// <param name="GroupName">グループ名</param>
        /// <returns>成功したかどうか</returns>
        private bool AddSyncGroup(string GroupName)
        {
            if (string.IsNullOrEmpty(ScrollGroup))
            {
                return false;
            }

            if (AssociatedObject is ScrollViewer sv)
            {
                add();
                sv.ScrollChanged += ScrollViewerScrolled;
                return true;
            }
            if (AssociatedObject is ScrollBar sb)
            {
                add();
                sb.ValueChanged += ScrollBarScrolled;
                return true;
            }
            return false;

            void add()
            {
                if (!SyncGroups.ContainsKey(GroupName))
                {
                    SyncGroups.Add(GroupName, new List<Control>());
                }
                SyncGroups[GroupName].Add(AssociatedObject);
            }
        }

        /// <summary>
        /// 同期グループから削除するメソッド
        /// </summary>
        /// <param name="GroupName">グループ名</param>
        /// <returns>成功したかどうか</returns>
        private bool RemoveSyncGroup(string GroupName)
        {
            if (!string.IsNullOrEmpty(ScrollGroup)
                && (AssociatedObject is ScrollViewer || AssociatedObject is ScrollBar))
            {
                if (AssociatedObject is ScrollViewer sv)
                {
                    sv.ScrollChanged -= ScrollViewerScrolled;
                }
                if (AssociatedObject is ScrollBar sb)
                {
                    sb.ValueChanged -= ScrollBarScrolled;
                }

                SyncGroups[GroupName].Remove(AssociatedObject);
                if (SyncGroups[GroupName].Count == 0)
                {
                    SyncGroups.Remove(GroupName);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ScrollViewerの場合の変更通知イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewerScrolled(object sender, ScrollChangedEventArgs e)
        {
            UpdateScrollValue(
                sender,
                Orientation == Orientation.Horizontal ? e.HorizontalOffset : e.VerticalOffset
            );
        }

        /// <summary>
        /// ScrollBarの場合の変更通知イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollBarScrolled(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateScrollValue(sender, e.NewValue);
        }

        /// <summary>
        /// スクロール値を設定するメソッド
        /// </summary>
        /// <param name="sender">スクロール値を更新してきたコントロール</param>
        /// <param name="NewValue">新しいスクロール値</param>
        private void UpdateScrollValue(object sender, double NewValue)
        {
            IEnumerable<Control> others = SyncGroups[ScrollGroup].Where(p => p != sender);

            foreach (ScrollBar sb in others.OfType<ScrollBar>().Where(p => p.Orientation == Orientation))
            {
                sb.Value = NewValue;
            }
            foreach (ScrollViewer sv in others.OfType<ScrollViewer>())
            {
                if (Orientation == Orientation.Horizontal)
                {
                    sv.ScrollToHorizontalOffset(NewValue);
                }
                else
                {
                    sv.ScrollToVerticalOffset(NewValue);
                }
            }
        }
    }
}
