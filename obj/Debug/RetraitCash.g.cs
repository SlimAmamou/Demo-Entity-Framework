﻿#pragma checksum "..\..\RetraitCash.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BD1A4A86259D3DE110149B392C436A72F1D3D3542D0367529AD695F13323A2A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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
using WpfBanqueProjet;


namespace WpfBanqueProjet {
    
    
    /// <summary>
    /// RetraitCash
    /// </summary>
    public partial class RetraitCash : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\RetraitCash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox BoxRetrait;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\RetraitCash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRetrait;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\RetraitCash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnRetrait;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\RetraitCash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnComptes;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\RetraitCash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnQuit;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfBanqueProjet;component/retraitcash.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RetraitCash.xaml"
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
            this.BoxRetrait = ((System.Windows.Controls.ComboBox)(target));
            
            #line 10 "..\..\RetraitCash.xaml"
            this.BoxRetrait.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.BoxDepot_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtRetrait = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\RetraitCash.xaml"
            this.txtRetrait.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtRetrait_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnRetrait = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\RetraitCash.xaml"
            this.BtnRetrait.Click += new System.Windows.RoutedEventHandler(this.BtnRetrait_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnComptes = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\RetraitCash.xaml"
            this.BtnComptes.Click += new System.Windows.RoutedEventHandler(this.BtnComptes_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnQuit = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\RetraitCash.xaml"
            this.BtnQuit.Click += new System.Windows.RoutedEventHandler(this.BtnQuit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

