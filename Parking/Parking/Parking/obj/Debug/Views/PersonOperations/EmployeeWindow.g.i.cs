﻿#pragma checksum "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CCCDA2A290FC783DE2EBF4B107CBAE4939FF54FB8DA99D02742A0E5F040B4DAF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Parking.Helpes;
using Parking.ViewModel.PersonOperations;
using Parking.Views.PersonOperations;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;


namespace Parking.Views.PersonOperations {
    
    
    /// <summary>
    /// EmployeeWindow
    /// </summary>
    public partial class EmployeeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridClients;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox FIO;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ClientPhone;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox WorkTypeList;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusList;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox EndWorkEnable;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Parking;component/views/personoperations/employeewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\PersonOperations\EmployeeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.GridClients = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.FIO = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.ClientPhone = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 4:
            this.WorkTypeList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.StatusList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.EndWorkEnable = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

