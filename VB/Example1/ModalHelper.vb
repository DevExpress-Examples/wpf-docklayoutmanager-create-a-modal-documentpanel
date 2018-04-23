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
        Public Property AllowActivate() As Boolean
        Public Property AllowClose() As Boolean
        Public Property AllowDock() As Boolean
        Public Property AllowDrag() As Boolean
        Public Property AllowDrop() As Boolean
        Public Property AllowFloat() As Boolean
        Public Property AllowHide() As Boolean
        Public Property AllowMinimize() As Boolean
        Public Property AllowMaximize() As Boolean
        Public Property AllowMove() As Boolean
        Public Property AllowRestore() As Boolean
        Public Property AllowSelection() As Boolean
        Public Property AllowSizing() As Boolean
        Public Property Focusable() As Boolean
        Public Property IsEnabled() As Boolean
    End Class

    Public Class ModalHelper
        Public Shared IsModalProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsModal", GetType(Boolean), GetType(ModalHelper), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf IsModalChanged)))

        Public Shared WindowStateProperty As DependencyProperty = DependencyProperty.RegisterAttached("WindowState", GetType(BaseLayoutItemState), GetType(ModalHelper))

        Public Shared Function GetIsModal(ByVal d As DependencyObject) As Boolean
            Return DirectCast(d.GetValue(IsModalProperty), Boolean)
        End Function

        Public Shared Sub SetIsModal(ByVal d As DependencyObject, ByVal value As Boolean)
            d.SetValue(IsModalProperty, value)
        End Sub

        Private Shared Sub IsModalChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim item As BaseLayoutItem = TryCast(d, BaseLayoutItem)
            If d IsNot Nothing Then
                SetIsModal(item, DirectCast(e.NewValue, Boolean))
                If DirectCast(e.NewValue, Boolean) Then
                    CreateStates(item)
                    LockWindows(item)
                    AddHandler item.GetDockLayoutManager().DockItemClosing, AddressOf ModalHelper_DockItemClosing
                Else
                    UnlockWindows(item)
                    RemoveHandler item.GetDockLayoutManager().DockItemClosing, AddressOf ModalHelper_DockItemClosing
                End If
            End If
        End Sub

        Private Shared Sub ModalHelper_DockItemClosing(ByVal sender As Object, ByVal e As ItemCancelEventArgs)
            SetIsModal(e.Item, False)
        End Sub



        Public Shared Function GetWindowState(ByVal d As DependencyObject) As BaseLayoutItemState
            Return DirectCast(d.GetValue(WindowStateProperty), BaseLayoutItemState)
        End Function

        Public Shared Sub SetWindowState(ByVal d As DependencyObject, ByVal value As BaseLayoutItemState)
            d.SetValue(WindowStateProperty, value)
        End Sub




        Private Shared Sub UnlockWindows(ByVal window As BaseLayoutItem)
            If window.Parent IsNot Nothing Then
                For Each item In window.Parent.Items
                    Dim state As BaseLayoutItemState = GetWindowState(item)
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
                Next item
            End If
        End Sub

        Private Shared Sub LockWindows(ByVal window As BaseLayoutItem)
            If window.Parent IsNot Nothing Then
                For Each item In window.Parent.Items
                    If Not GetIsModal(item) Then
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
                Next item
            End If
        End Sub

        Private Shared Sub CreateWindowState(ByVal window As BaseLayoutItem)
            Dim state As New BaseLayoutItemState() With {.AllowActivate = window.AllowActivate, .AllowClose = window.AllowClose, .AllowDock = window.AllowDock, .AllowDrag = window.AllowDrag, .AllowDrop = window.AllowDrop, .AllowFloat = window.AllowFloat, .AllowHide = window.AllowHide, .AllowMaximize = window.AllowMaximize, .AllowMinimize = window.AllowMinimize, .AllowMove = window.AllowMove, .AllowRestore = window.AllowRestore, .AllowSelection = window.AllowSelection, .AllowSizing = window.AllowSizing, .Focusable = window.Focusable, .IsEnabled = window.IsEnabled}
            SetWindowState(window, state)
        End Sub

        Private Shared Sub CreateStates(ByVal window As BaseLayoutItem)
            If window.Parent IsNot Nothing Then
                For Each item In window.Parent.Items
                    CreateWindowState(item)
                Next item
            End If
        End Sub
    End Class
End Namespace
