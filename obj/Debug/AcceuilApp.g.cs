#pragma checksum "..\..\AcceuilApp.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7EEF9A000B73E63F9614E106EFB7AC1711599001E1378764E172FF37B7D50C30"
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
    /// AcceuilApp
    /// </summary>
    public partial class AcceuilApp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\AcceuilApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfBanqueProjet.AcceuilApp AcceuilProjet;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\AcceuilApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\AcceuilApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNew;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\AcceuilApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGuichet;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\AcceuilApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInterret;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AcceuilApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEpargne;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AcceuilApp.xaml"
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
            System.Uri resourceLocater = new System.Uri("/WpfBanqueProjet;component/acceuilapp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AcceuilApp.xaml"
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
            this.AcceuilProjet = ((WpfBanqueProjet.AcceuilApp)(target));
            return;
            case 2:
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\AcceuilApp.xaml"
            this.btnSearch.Click += new System.Windows.RoutedEventHandler(this.Insert_Client);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnNew = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\AcceuilApp.xaml"
            this.btnNew.Click += new System.Windows.RoutedEventHandler(this.Recherche_Client);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnGuichet = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\AcceuilApp.xaml"
            this.btnGuichet.Click += new System.Windows.RoutedEventHandler(this.Action_Guichet);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnInterret = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\AcceuilApp.xaml"
            this.btnInterret.Click += new System.Windows.RoutedEventHandler(this.btnInterret_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnEpargne = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\AcceuilApp.xaml"
            this.btnEpargne.Click += new System.Windows.RoutedEventHandler(this.btnEpargne_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnQuit = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\AcceuilApp.xaml"
            this.BtnQuit.Click += new System.Windows.RoutedEventHandler(this.BtnQuit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

