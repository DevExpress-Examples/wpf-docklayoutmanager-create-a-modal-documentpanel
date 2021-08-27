<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128643176/14.1.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T153405)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/Example1/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/Example1/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/Example1/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/Example1/MainWindow.xaml.vb))
* **[ModalHelper.cs](./CS/Example1/ModalHelper.cs) (VB: [ModalHelper.vb](./VB/Example1/ModalHelper.vb))**
<!-- default file list end -->
# How to create a modal DocumentPanel in DockLayoutManager 


<p>To make a window modal, create a class-helper with two attached properties: IsModal and WindowState. The IsModal property determines whether the window is modal. The WindowState property contains values of some BaseLayoutItem's properties, such as AllowActivate, Focusable, etc., before IsModal of any window is changed to true.</p>
<p>When IsModal is changed to true, properties of all windows should be copied to WindowState and set to false. When IsModal is set to false, all coped properties should be restored from WindowSate.</p>

<br/>


