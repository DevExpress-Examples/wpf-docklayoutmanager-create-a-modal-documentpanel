using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;

namespace Example
{
    public class BaseLayoutItemState
    {
        public bool AllowActivate { get; set; }
        public bool AllowClose { get; set; }
        public bool AllowDock { get; set; }
        public bool AllowDrag { get; set; }
        public bool AllowDrop { get; set; }
        public bool AllowFloat { get; set; }
        public bool AllowHide { get; set; }
        public bool AllowMinimize { get; set; }
        public bool AllowMaximize { get; set; }
        public bool AllowMove { get; set; }
        public bool AllowRestore { get; set; }
        public bool AllowSelection { get; set; }
        public bool AllowSizing { get; set; }
        public bool Focusable { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class ModalHelper
    {
        public static DependencyProperty IsModalProperty =
            DependencyProperty.RegisterAttached("IsModal",
            typeof(bool), typeof(ModalHelper),
            new PropertyMetadata(false, new PropertyChangedCallback(IsModalChanged)));

        public static DependencyProperty WindowStateProperty =
            DependencyProperty.RegisterAttached("WindowState",
            typeof(BaseLayoutItemState), typeof(ModalHelper));

        public static bool GetIsModal(DependencyObject d)
        {
            return (bool)d.GetValue(IsModalProperty);
        }

        public static void SetIsModal(DependencyObject d, bool value)
        {
            d.SetValue(IsModalProperty, value);
        }

        private static void IsModalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseLayoutItem item = d as BaseLayoutItem;
            if (d != null)
            {
                SetIsModal(item, (bool)e.NewValue);
                if ((bool)e.NewValue)
                {
                    CreateStates(item);
                    LockWindows(item);
                    item.GetDockLayoutManager().DockItemClosing += ModalHelper_DockItemClosing;
                }
                else
                {
                    UnlockWindows(item);
                    item.GetDockLayoutManager().DockItemClosing -= ModalHelper_DockItemClosing;
                }
            }
        }

        static void ModalHelper_DockItemClosing(object sender, ItemCancelEventArgs e)
        {
            SetIsModal(e.Item, false);
        }



        public static BaseLayoutItemState GetWindowState(DependencyObject d)
        {
            return (BaseLayoutItemState)d.GetValue(WindowStateProperty);
        }

        public static void SetWindowState(DependencyObject d, BaseLayoutItemState value)
        {
            d.SetValue(WindowStateProperty, value);
        }




        private static void UnlockWindows(BaseLayoutItem window)
        {
            if (window.Parent != null)
            {
                foreach (var item in window.Parent.Items)
                {
                    BaseLayoutItemState state = GetWindowState(item);
                    if (state != null)
                    {
                        item.AllowActivate = state.AllowActivate;
                        item.AllowClose = state.AllowClose;
                        item.AllowDock = state.AllowDock;
                        item.AllowDrag = state.AllowDrag;
                        item.AllowDrop = state.AllowDrop;
                        item.AllowFloat = state.AllowFloat;
                        item.AllowHide = state.AllowHide;
                        item.AllowMaximize = state.AllowMaximize;
                        item.AllowMinimize = state.AllowMinimize;
                        item.AllowMove = state.AllowMove;
                        item.AllowRestore = state.AllowRestore;
                        item.AllowSelection = state.AllowSelection;
                        item.AllowSizing = state.AllowSizing;
                        item.Focusable = state.Focusable;
                        item.IsEnabled = state.IsEnabled;
                        state = null;
                    }
                }
            }
        }

        private static void LockWindows(BaseLayoutItem window)
        {
            if (window.Parent != null)
            {
                foreach (var item in window.Parent.Items)
                {
                    if (!GetIsModal(item))
                    {
                        item.AllowActivate = false;
                        item.AllowClose = false;
                        item.AllowDock = false;
                        item.AllowDrag = false;
                        item.AllowDrop = false;
                        item.AllowFloat = false;
                        item.AllowHide = false;
                        item.AllowMaximize = false;
                        item.AllowMinimize = false;
                        item.AllowMove = false;
                        item.AllowRestore = false;
                        item.AllowSelection = false;
                        item.AllowSizing = false;
                        item.Focusable = false;
                        item.IsEnabled = false;
                    }
                }
            }
        }

        private static void CreateWindowState(BaseLayoutItem window)
        {
            BaseLayoutItemState state = new BaseLayoutItemState()
            {
                AllowActivate = window.AllowActivate,
                AllowClose = window.AllowClose,
                AllowDock = window.AllowDock,
                AllowDrag = window.AllowDrag,
                AllowDrop = window.AllowDrop,
                AllowFloat = window.AllowFloat,
                AllowHide = window.AllowHide,
                AllowMaximize = window.AllowMaximize,
                AllowMinimize = window.AllowMinimize,
                AllowMove = window.AllowMove,
                AllowRestore = window.AllowRestore,
                AllowSelection = window.AllowSelection,
                AllowSizing = window.AllowSizing,
                Focusable = window.Focusable,
                IsEnabled = window.IsEnabled
            };
            SetWindowState(window, state);
        }

        private static void CreateStates(BaseLayoutItem window)
        {
            if (window.Parent != null)
            {
                foreach (var item in window.Parent.Items)
                    CreateWindowState(item);
            }
        }
    }
}
