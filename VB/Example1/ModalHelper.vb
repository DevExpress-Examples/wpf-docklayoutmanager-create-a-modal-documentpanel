Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.Docking.Base

Namespace Example

    Public Class BaseLayoutItemState

        Public Property AllowActivate As Boolean

        Public Property AllowClose As Boolean

        Public Property AllowDock As Boolean

        Public Property AllowDrag As Boolean

        Public Property AllowDrop As Boolean

        Public Property AllowFloat As Boolean

        Public Property AllowHide As Boolean

        Public Property AllowMinimize As Boolean

        Public Property AllowMaximize As Boolean

        Public Property AllowMove As Boolean

        Public Property AllowRestore As Boolean

        Public Property AllowSelection As Boolean

        Public Property AllowSizing As Boolean

        Public Property Focusable As Boolean

        Public Property IsEnabled As Boolean
    End Class

    Public Class ModalHelper

        Public Shared IsModalProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.RegisterAttached("IsModal", GetType(Boolean), GetType(Example.ModalHelper), New System.Windows.PropertyMetadata(False, New System.Windows.PropertyChangedCallback(AddressOf Example.ModalHelper.IsModalChanged)))

        Public Shared WindowStateProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.RegisterAttached("WindowState", GetType(Example.BaseLayoutItemState), GetType(Example.ModalHelper))

        Public Shared Function GetIsModal(ByVal d As System.Windows.DependencyObject) As Boolean
            Return CBool(d.GetValue(Example.ModalHelper.IsModalProperty))
        End Function

        Public Shared Sub SetIsModal(ByVal d As System.Windows.DependencyObject, ByVal value As Boolean)
            d.SetValue(Example.ModalHelper.IsModalProperty, value)
        End Sub

        Private Shared Sub IsModalChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
            Dim item As DevExpress.Xpf.Docking.BaseLayoutItem = TryCast(d, DevExpress.Xpf.Docking.BaseLayoutItem)
            If d IsNot Nothing Then
                Call Example.ModalHelper.SetIsModal(item, CBool(e.NewValue))
                If CBool(e.NewValue) Then
                    Call Example.ModalHelper.CreateStates(item)
                    Call Example.ModalHelper.LockWindows(item)
                    AddHandler DevExpress.Xpf.Docking.LayoutItemsHelper.GetDockLayoutManager(item).DockItemClosing, AddressOf Example.ModalHelper.ModalHelper_DockItemClosing
                Else
                    Call Example.ModalHelper.UnlockWindows(item)
                    RemoveHandler DevExpress.Xpf.Docking.LayoutItemsHelper.GetDockLayoutManager(item).DockItemClosing, AddressOf Example.ModalHelper.ModalHelper_DockItemClosing
                End If
            End If
        End Sub

        Private Shared Sub ModalHelper_DockItemClosing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Docking.Base.ItemCancelEventArgs)
            Call Example.ModalHelper.SetIsModal(e.Item, False)
        End Sub

        Public Shared Function GetWindowState(ByVal d As System.Windows.DependencyObject) As BaseLayoutItemState
            Return CType(d.GetValue(Example.ModalHelper.WindowStateProperty), Example.BaseLayoutItemState)
        End Function

        Public Shared Sub SetWindowState(ByVal d As System.Windows.DependencyObject, ByVal value As Example.BaseLayoutItemState)
            d.SetValue(Example.ModalHelper.WindowStateProperty, value)
        End Sub

        Private Shared Sub UnlockWindows(ByVal window As DevExpress.Xpf.Docking.BaseLayoutItem)
            If window.Parent IsNot Nothing Then
                For Each item In window.Parent.Items
                    Dim state As Example.BaseLayoutItemState = Example.ModalHelper.GetWindowState(item)
                    If state IsNot Nothing Then
                        item.AllowActivate = state.AllowActivate
                        item.AllowClose = state.AllowClose
                        item.AllowDock = state.AllowDock
                        item.AllowDrag = state.AllowDrag
                        item.AllowDrop = state.AllowDrop
                        item.AllowFloat = state.AllowFloat
                        item.AllowHide = state.AllowHide
                        item.AllowMaximize = state.AllowMaximize
                        item.AllowMinimize = state.AllowMinimize
                        item.AllowMove = state.AllowMove
                        item.AllowRestore = state.AllowRestore
                        item.AllowSelection = state.AllowSelection
                        item.AllowSizing = state.AllowSizing
                        item.Focusable = state.Focusable
                        item.IsEnabled = state.IsEnabled
                        state = Nothing
                    End If
                Next
            End If
        End Sub

        Private Shared Sub LockWindows(ByVal window As DevExpress.Xpf.Docking.BaseLayoutItem)
            If window.Parent IsNot Nothing Then
                For Each item In window.Parent.Items
                    If Not Example.ModalHelper.GetIsModal(item) Then
                        item.AllowActivate = False
                        item.AllowClose = False
                        item.AllowDock = False
                        item.AllowDrag = False
                        item.AllowDrop = False
                        item.AllowFloat = False
                        item.AllowHide = False
                        item.AllowMaximize = False
                        item.AllowMinimize = False
                        item.AllowMove = False
                        item.AllowRestore = False
                        item.AllowSelection = False
                        item.AllowSizing = False
                        item.Focusable = False
                        item.IsEnabled = False
                    End If
                Next
            End If
        End Sub

        Private Shared Sub CreateWindowState(ByVal window As DevExpress.Xpf.Docking.BaseLayoutItem)
            Dim state As Example.BaseLayoutItemState = New Example.BaseLayoutItemState() With {.AllowActivate = window.AllowActivate, .AllowClose = window.AllowClose, .AllowDock = window.AllowDock, .AllowDrag = window.AllowDrag, .AllowDrop = window.AllowDrop, .AllowFloat = window.AllowFloat, .AllowHide = window.AllowHide, .AllowMaximize = window.AllowMaximize, .AllowMinimize = window.AllowMinimize, .AllowMove = window.AllowMove, .AllowRestore = window.AllowRestore, .AllowSelection = window.AllowSelection, .AllowSizing = window.AllowSizing, .Focusable = window.Focusable, .IsEnabled = window.IsEnabled}
            Call Example.ModalHelper.SetWindowState(window, state)
        End Sub

        Private Shared Sub CreateStates(ByVal window As DevExpress.Xpf.Docking.BaseLayoutItem)
            If window.Parent IsNot Nothing Then
                For Each item In window.Parent.Items
                    Call Example.ModalHelper.CreateWindowState(item)
                Next
            End If
        End Sub
    End Class
End Namespace
