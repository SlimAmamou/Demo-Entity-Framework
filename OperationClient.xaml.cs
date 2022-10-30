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
    /// Logique d'interaction pour OperationClient.xaml
    /// </summary>
    public partial class OperationClient : Window
    {        
        client client = new client();
        compte selectedCompte;
        bank_dbEntities3 maBanque;
        List<compte> listcomptes = new List<compte>();
        List<compte> listhypotheque = new List<compte>();        
        List<TypeCompte> listType = new List<TypeCompte>();
        List<Transaction> listOperationsFrom = new List<Transaction>();
        List<Transaction> listOperationsTo = new List<Transaction>();
        compte cptMarge;
        compte cptHypotheque;
       
        Boolean marge = false;
        public OperationClient(client selectedItem)
        {
            InitializeComponent();            
            maBanque = new bank_dbEntities3();            
            client = selectedItem;
            

            //Afficher tous les comptes client
            Affiche_count();

            //Types des comptes proposés par la banque.
            listType = (from c in maBanque.TypeComptes select c).ToList();

            //Afficher données client
            Title += " " + client.Nom + " " + client.Prenom;
            txtnom.Content = client.Nom;
            txtprenom.Content = client.Prenom;
            txtphone.Content = client.Telephone;
            txtmail.Content = client.Courriel;

            
            // Determiner si le client a un compte marge
            foreach (compte cmpt in listcomptes)
            {
                if (cmpt.Type == "marge") marge = true;
            }
            if (marge)
            {
                boxtype.DataContext = listType.Skip(1);
            }
            else
            {
                boxtype.DataContext = listType;
            }

            
        } 

        public void Affiche_count()
        {
            //Récuperation et Affichage  liste des comptes clients.
            listcomptes = (from c in maBanque.comptes where c.IDClient == client.IDClient select c).ToList();
            lstcomptes.ItemsSource = listcomptes;
            // Récuperation et Affichage  liste des comptes clients hypotheque.
            List<compte> newListHypotheque = new List<compte>();
            foreach (compte clientCompte in listcomptes)
            {
                if(clientCompte.Type == "hypotheque")
                {
                    newListHypotheque.Add(clientCompte);
                }
            }
            
            boxHypotheque.DataContext = newListHypotheque;
        }

        private void Button_Ajout_Click(object sender, RoutedEventArgs e)
        {
            compte newCmpt = new compte();
            newCmpt.IDClient = client.IDClient;
            newCmpt.Type = ((TypeCompte)boxtype.SelectedItem).Type;
            newCmpt.Montant = 0;
            MessageBox.Show("Creation de compte \n " + newCmpt.Type.ToString() + " \n éffectuée avec succe.","Validation",  MessageBoxButton.OK, MessageBoxImage.Information);
            maBanque.comptes.Add(newCmpt);
            maBanque.SaveChanges();
            //Afficher tous les comptes client
            Affiche_count();
                       
            //definir paramétres boxType;
            foreach (compte cmpt in listcomptes)
            {
                if (cmpt.Type == "marge") marge = true;
            }
            if (marge)
            {
                boxtype.DataContext = listType.Skip(1);
            }
            else
            {
                boxtype.DataContext = listType;
            }
            
        }

        private void lstcomptes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCompte = (compte)lstcomptes.SelectedItem;

            listOperationsFrom = (from c in maBanque.Transactions where c.compteFrom == selectedCompte.IDCompte select c).ToList();
            listOperationsTo = (from c in maBanque.Transactions where c.compteTo == selectedCompte.IDCompte select c).ToList();                
                
            lstransactionsfrom.ItemsSource = listOperationsFrom;
            lstransactionsto.ItemsSource = listOperationsTo;

            txtselectCompte.Content = selectedCompte.Type.ToString() + " " + selectedCompte.IDCompte.ToString();                       
        }

        private void btnEpargne_Click(object sender, RoutedEventArgs e)
        {
            Boolean cptEpargne = false;
            foreach(compte epargne in listcomptes)
            {
                if (epargne.Type == "epargne")
                {
                    epargne.Montant *= Convert.ToDecimal(1.01);
                    cptEpargne = true;
                    maBanque.SaveChanges();
                    Affiche_count();
                }
            }
            if (!cptEpargne) MessageBox.Show("Le client ne posséde pas de compte epargne","Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Affiche_count();
            btnEpargne.IsEnabled = false;
        }

        private void btnMarge_Click(object sender, RoutedEventArgs e)
        {
            Boolean margeCredit = false;
            foreach (compte marge in listcomptes)
            {
                if (marge.Type == "marge")
                {
                    marge.Montant *= Convert.ToDecimal(1.01);
                    margeCredit = true;
                    maBanque.SaveChanges();
                    Affiche_count();
                }
            }
            if (!margeCredit) MessageBox.Show("Le client ne posséde pas de marge de credit","Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Affiche_count();
            btnMarge.IsEnabled = false;
        }

        private void btnHypotheque_Click(object sender, RoutedEventArgs e)
        {
            DateTime date_time = new DateTime();
            date_time = DateTime.Now;
            //definir compte Hypoteque concerné
            cptHypotheque = (compte)boxHypotheque.SelectedItem;
            decimal retraitHypotheque = decimal.Parse(txtHypt.Text.ToString());

            //Transactions
            Transaction HypothequeTransaction = new Transaction();
            Transaction MargeTransaction = new Transaction();
            //definir compte marge
            foreach (compte clientCompte in listcomptes)
            {
                if (clientCompte.Type == "marge")
                {
                    cptMarge = clientCompte;                   
                }
            }

            if (retraitHypotheque <= (decimal)cptHypotheque.Montant)
            {
                //Transaction hypôtheque 
                cptHypotheque.Montant -= retraitHypotheque;
                MessageBox.Show("Payement Hypotheque ellectué avec succés", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                HypothequeTransaction.typeTransaction = "facture";
                HypothequeTransaction.compteFrom = cptHypotheque.IDCompte;
                HypothequeTransaction.montant = retraitHypotheque;
                HypothequeTransaction.date = date_time;
                maBanque.Transactions.Add(HypothequeTransaction);
                maBanque.SaveChanges();
            }
            else
            {
                if (marge)
                {
                    decimal balance = retraitHypotheque - (decimal)cptHypotheque.Montant;
                    int defaultCompte = 1;

                    // Entree trace de payement hypothêque sur compte hypothêque même si l'int.gralité du payement est efféctué depuis Marge!!!!!!!
                    HypothequeTransaction.typeTransaction = "facture";
                    HypothequeTransaction.compteFrom = cptHypotheque.IDCompte;
                    HypothequeTransaction.compteTo = defaultCompte;
                    HypothequeTransaction.montant = retraitHypotheque - balance;
                    maBanque.Transactions.Add(HypothequeTransaction);
                    HypothequeTransaction.date = date_time;
                    maBanque.SaveChanges();
                    MessageBox.Show("Payement Hypotheque Transferé sur Marge", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    //Transaction hypôtheque depuis Marge
                    MargeTransaction.typeTransaction = "hypotheque";
                    MargeTransaction.compteTo = defaultCompte;
                    MargeTransaction.compteFrom = (int)cptMarge.IDCompte;
                    MargeTransaction.montant = balance;
                    MargeTransaction.date = date_time;
                    maBanque.Transactions.Add(MargeTransaction);
                    maBanque.SaveChanges();

                    cptMarge.Montant += balance;
                    cptHypotheque.Montant = 0;
                    MessageBox.Show("Payement Hypotheque ellectué avec succés", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cette opération ne peut pas être éfféctuée.", "Attention!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            txtHypt.Clear();
            txtHypt.Focus();
            Affiche_count();
        }
        
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtHypt.Text.Trim()) && decimal.Parse(txtHypt.Text.ToString()) >= 0 && boxHypotheque.SelectedIndex != -1)
                {
                    btnHypotheque.IsEnabled = true;                    
                }
                else { btnHypotheque.IsEnabled = false; }
            }
            catch (Exception)
            {
                MessageBox.Show("Implémentation De caractère Invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RechercheClient nouveau = new RechercheClient();
            nouveau.Show();
             
            this.Close();
        }
    }
}
