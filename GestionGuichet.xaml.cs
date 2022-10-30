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
    /// Logique d'interaction pour GestionGichet.xaml
    /// </summary>
    public partial class GestionGichet : Window
    {
        bank_dbEntities3 maBanque;
        int maxAjout;
        int solde;
        bool accessGuichet = false;
        public GestionGichet()
        {
            InitializeComponent();
            txtGuichet.Focus();
            maBanque = new bank_dbEntities3();
            //Recuperation somme disponible dans le guichet
            List<compte> comptes = (from c in maBanque.comptes where c.IDCompte == 1 select c).ToList();

            //Configuration affichage Solde
            compte compte = new compte();
            compte = comptes[0];
            solde = (int)compte.Montant;
            maxAjout = 20000 - solde;
            txtSolde.Text = solde.ToString()+" $";

            //Configuration boutton de dèbloquage guichet
            client guichet = new client();
            List<client> guichets = (from c in maBanque.clients where c.IDClient == 2 select c).ToList();
            guichet = guichets[0];
            if(guichet.Acces == false)
            {
                activ_bloq_dab.Content = "Débloquer les transactions";
                activ_bloq_dab.BorderBrush = Brushes.Red;
            }
        }

        private void activ_bloq_dab_Click(object sender, RoutedEventArgs e)
        {
            if ((String)activ_bloq_dab.Content == "Bloquer les transactions")
            {               
                activ_bloq_dab.Content = "Débloquer les transactions";
                activ_bloq_dab.BorderBrush = Brushes.Red;
                client guichet = new client(); 
                List<client> guichets = (from c in maBanque.clients where c.IDClient == 2 select c).ToList();
                guichet = guichets[0];
                guichet.Acces = false;
                accessGuichet = true;
                maBanque.SaveChanges();
            }
            else
            {
                activ_bloq_dab.Content = "Bloquer les transactions";
                activ_bloq_dab.BorderBrush = Brushes.Green;
                client guichet = new client();
                List<client> guichets = (from c in maBanque.clients where c.IDClient == 2 select c).ToList();
                guichet = guichets[0];
                guichet.Acces = true;
                accessGuichet = false;
                maBanque.SaveChanges();                
            }
        }

        private void btn_add_cash_Click(object sender, RoutedEventArgs e)
        {
            if(accessGuichet)
            {
                compte guichet = new compte();
                List<compte> guichets = (from c in maBanque.comptes where c.IDCompte == 1 select c).ToList();
                guichet = guichets[0];
                guichet.Montant = guichet.Montant + int.Parse(txtGuichet.Text.ToString());
                maBanque.SaveChanges();
                MessageBox.Show("Ajout réussis \n le nouveau solde est: " + ((int)guichet.Montant).ToString()+ "$.", "Information", MessageBoxButton.OK,MessageBoxImage.Information);
                txtSolde.Text = ((int)guichet.Montant).ToString()+" $";
                txtGuichet.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Il faut Bloquer l'accés au guichet \n avant d'injecter de l'argent", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            if (!accessGuichet)
            {
                AcceuilApp retour = new AcceuilApp();
                retour.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Le Guichet est non foctionnel!", "Attention!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                AcceuilApp retour = new AcceuilApp();
                retour.Show();
                this.Close();
            }
        }

        private void Verifier_Info(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(txtGuichet.Text.ToString()=="")
                {
                    btn_add_cash.IsEnabled = false;
                }
                else if (  // Verification format et intégrité du montant à ajouter pour permettre l'opération
                     !string.IsNullOrEmpty(txtGuichet.Text.Trim()) 
                     && int.Parse(txtGuichet.Text.ToString()) % 10 == 0
                     && int.Parse(txtGuichet.Text.ToString()) <= maxAjout
                    )
                {
                    btn_add_cash.IsEnabled = true;
                }
                else if(int.Parse(txtGuichet.Text.ToString()) >= maxAjout) // Informer si le montant implémenté est superieur au montant admissible
                {
                    MessageBox.Show("le montant ajouté doit être \n un multiple de 10 \n" +
                    "et inférieur à: " + maxAjout.ToString() + " $.", "Montant", MessageBoxButton.OK, MessageBoxImage.Information);
                    btn_add_cash.IsEnabled = false;
                }
                else
                {
                    btn_add_cash.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("le montant ajouté doit être \n un multiple de 10 \n" +
                    "et inférieur à: "+ maxAjout.ToString() + ".", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
