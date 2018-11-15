<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/Example1/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/Example1/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/Example1/MainWindow.xaml.cs) (VB: [MainWindow.xaml](./VB/Example1/MainWindow.xaml))
* **[ModalHelper.cs](./CS/Example1/ModalHelper.cs) (VB: [ModalHelper.vb](./VB/Example1/ModalHelper.vb))**
<!-- default file list end -->
# How to create a modal DocumentPanel in DockLayoutManager 


<p>To make a window modal, create a class-helper with two attached properties: IsModal and WindowState. The IsModal property determines whether the window is modal. The WindowState property contains values of some BaseLayoutItem's properties, such as AllowActivate, Focusable, etc., before IsModal of any window is changed to true.</p>
<p>When IsModal is changed to true, properties of all windows should be copied to WindowState and set to false. When IsModal is set to false, all coped properties should be restored from WindowSate.</p>

<br/>


