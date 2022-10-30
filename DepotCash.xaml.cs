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
    /// Logique d'interaction pour DepotCash.xaml
    /// </summary>
    public partial class DepotCash : Window
    {
        bank_dbEntities3 maBanque;
        List<compte> listcomptes = new List<compte>();
        client depotClient = new client();
        Boolean compteSelected = false;
        
        public DepotCash(client client)
        {
            InitializeComponent();
            depotClient = client;
            maBanque = new bank_dbEntities3();
            listcomptes = (from c in maBanque.comptes where c.IDClient == client.IDClient select c).ToList();
            
            foreach (compte depot in listcomptes)
            {
                if(depot.Type != "marge")
                {
                    BoxDepot.Items.Add(depot.Type + " " + depot.IDCompte);
                }
            }
        }

        private void BtnDepot_Click(object sender, RoutedEventArgs e)
        {
            DateTime date_time;
            date_time = DateTime.Now;
            Transaction DepotTransaction = new Transaction();
            string numCompte = BoxDepot.SelectedItem.ToString();
            string[] selects = numCompte.Split(' ');
            int depotCompte = int.Parse(selects[1]);

            // Mise à jour montant compte et implementation transaction.
            foreach (compte item in listcomptes)
            {
                if (item.IDCompte == depotCompte)
                {
                    item.Montant += int.Parse(txtDepot.Text);
                    DepotTransaction.typeTransaction = "depot";
                    DepotTransaction.compteFrom = 1;
                    DepotTransaction.compteTo = depotCompte;
                    DepotTransaction.date = date_time;
                    DepotTransaction.montant = int.Parse(txtDepot.Text);
                    maBanque.Transactions.Add(DepotTransaction);
                    maBanque.SaveChanges();
                }
            }

            // Message aprés dépôt,
            if(MessageBox.Show("Dépôt éffectué avec Succé!!\nDésirez-vous faire un autre dépôt ?", "Question !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                BoxDepot.SelectedIndex = -1;
                txtDepot.Clear();
                compteSelected = false;
            }
            else
            {
                EspaceClient retour = new EspaceClient((client)depotClient);
                retour.Show();
                this.Close();
            }
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous quitter Dépots My Bank ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on revient à la page Espace client.
            {
                EspaceClient retour = new EspaceClient((client) depotClient);
                retour.Show();
                this.Close();
            }
        }

        private void BtnComptes_Click(object sender, RoutedEventArgs e)
        {
            //Affichage la page compte client.
            SoldeComptes soldeClient = new SoldeComptes((client)depotClient);
            soldeClient.Show();
        }

        private void TxtDepot_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDepot.Text.Trim()))
                {
                    BtnDepot.IsEnabled = false;
                }
                else if(compteSelected
                    &&!string.IsNullOrEmpty(txtDepot.Text.Trim())  
                    && int.Parse(txtDepot.Text.Trim().ToString()) >=0
                    && int.Parse(txtDepot.Text.Trim().ToString())%10 == 0)
                {
                    BtnDepot.IsEnabled = true;
                }
                else
                {
                    BtnDepot.IsEnabled = false;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("presence de caractère non numérique \ndans le montant à déposer!", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BoxDepot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            compteSelected = true;
            try
            {
                if (string.IsNullOrEmpty(txtDepot.Text.Trim()))
                {
                    BtnDepot.IsEnabled = false;
                }
                else if (compteSelected
                    && !string.IsNullOrEmpty(txtDepot.Text.Trim())
                    && int.Parse(txtDepot.Text.Trim().ToString()) >= 0
                    && int.Parse(txtDepot.Text.Trim().ToString()) % 10 == 0)
                {
                    BtnDepot.IsEnabled = true;
                }                 
                else
                {
                    BtnDepot.IsEnabled = false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("presence de caractère non numérique \ndans le montant à déposer!", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
