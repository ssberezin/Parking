﻿#pragma checksum "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8641213708413161ACB6A9DBACC8CFE0E2C6C9E80721FC3AFC1803B0615EB6AD"
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
using Parking.ViewModel.ParkPlacesOps;
using Parking.Views.ParkPlacesOps;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
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


namespace Parking.Views.ParkPlacesOps {
    
    
    /// <summary>
    /// ParkPlaceEditWindow
    /// </summary>
    public partial class ParkPlaceEditWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 73 "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridOrders;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker StartDate;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox OrgNameEnable;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ColorCombo;
        
        #line default
        #line hidden
        
        
        #line 319 "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ReplaceEnable;
        
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
            System.Uri resourceLocater = new System.Uri("/Parking;component/views/parkplacesops/parkplaceeditwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ParkPlacesOps\ParkPlaceEditWindow.xaml"
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
            this.GridOrders = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.StartDate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.OrgNameEnable = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 4:
            this.ColorCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ReplaceEnable = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

