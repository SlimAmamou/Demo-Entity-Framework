#pragma checksum "..\..\RechercheClient.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "97AE7047B4083F54E644FDECBD5655BA8B5FE754AFD8DDDC56F4CEAF79ECA254"
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
    /// RechercheClient
    /// </summary>
    public partial class RechercheClient : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\RechercheClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfBanqueProjet.RechercheClient RechercheDeClient;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\RechercheClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox boxName;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\RechercheClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox boxPrenom;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\RechercheClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listClients;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\RechercheClient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRetour;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfBanqueProjet;component/rechercheclient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RechercheClient.xaml"
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
            this.RechercheDeClient = ((WpfBanqueProjet.RechercheClient)(target));
            return;
            case 2:
            this.boxName = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\RechercheClient.xaml"
            this.boxName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.boxPrenom = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\RechercheClient.xaml"
            this.boxPrenom.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listClients = ((System.Windows.Controls.ListView)(target));
            
            #line 16 "..\..\RechercheClient.xaml"
            this.listClients.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listClients_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnRetour = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\RechercheClient.xaml"
            this.btnRetour.Click += new System.Windows.RoutedEventHandler(this.btnRetour_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

