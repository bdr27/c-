﻿#pragma checksum "..\..\..\Modal\IPAddressModal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A9336BFC9D9B5221E061BB45FC681E2E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18047
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace ServerMiniCheckers.Modal {
    
    
    /// <summary>
    /// IPAddressModal
    /// </summary>
    public partial class IPAddressModal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Modal\IPAddressModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbIPAddress;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Modal\IPAddressModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sldPortNumber;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Modal\IPAddressModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbPortNumber;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Modal\IPAddressModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOkay;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Modal\IPAddressModal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLazyAss;
        
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
            System.Uri resourceLocater = new System.Uri("/ServerMiniCheckers;component/modal/ipaddressmodal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modal\IPAddressModal.xaml"
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
            this.tbIPAddress = ((System.Windows.Controls.TextBox)(target));
            
            #line 12 "..\..\..\Modal\IPAddressModal.xaml"
            this.tbIPAddress.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbIPAddress_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.sldPortNumber = ((System.Windows.Controls.Slider)(target));
            return;
            case 3:
            this.tbPortNumber = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.btnOkay = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Modal\IPAddressModal.xaml"
            this.btnOkay.Click += new System.Windows.RoutedEventHandler(this.btnOkay_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnLazyAss = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Modal\IPAddressModal.xaml"
            this.btnLazyAss.Click += new System.Windows.RoutedEventHandler(this.btnLazyAss_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

