using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBanqueProjet
{
    /// <summary>
    /// Logique d'interaction pour AcceuilApp.xaml
    /// </summary>
    public partial class AcceuilApp : Window
    {
        bank_dbEntities3 maBanque;        
        
        public AcceuilApp()
        {
            maBanque = new bank_dbEntities3();
            InitializeComponent();            
        }

        private void Insert_Client(object sender, EventArgs e)
        {
            RechercheClient nouveau = new RechercheClient();
            nouveau.Show();
            // On cache la fenêtre d'acceuil. 
            this.Close();
        }

        private void Recherche_Client(object sender, EventArgs e)
        {
            FormClient nouveau = new FormClient();
            nouveau.Show();
            // On cache la fenêtre d'acceuil. 
            this.Close();
        }

        private void Action_Guichet(object sender, EventArgs e)
        {
            GestionGichet nouveau = new GestionGichet();
            nouveau.Show();
            // On cache la fenêtre d'acceuil. 
            this.Close();
        }

        private void btnInterret_Click(object sender, RoutedEventArgs e)
        {
            List<compte> listcomptesMarge = new List<compte>();
            listcomptesMarge = (from c in maBanque.comptes where c.Type == "marge" select c).ToList();            
            foreach (compte marge in listcomptesMarge)
            {                
                 marge.Montant *= Convert.ToDecimal(1.05);                    
                 maBanque.SaveChanges();              
            }
            MessageBox.Show("Succé d'application interret sur marge","Information", MessageBoxButton.OK, MessageBoxImage.Information);
            
            
        }

        private void btnEpargne_Click(object sender, RoutedEventArgs e)
        {
            List<compte> listcomptesEpargne = new List<compte>();
            listcomptesEpargne = (from c in maBanque.comptes where c.Type == "epargne" select c).ToList();
            foreach (compte epargne in listcomptesEpargne)
            {
                epargne.Montant *= Convert.ToDecimal(1.01);
                maBanque.SaveChanges();
            }
            MessageBox.Show("Succé d'attribution interret sur epargne", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous réellement quitter Le guichet ?", "question !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on met fin à l'application.
            {
                Login retour = new Login();
                retour.Show();
                this.Close();
            }
        }
    }
}
