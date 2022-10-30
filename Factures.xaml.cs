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
    /// Logique d'interaction pour Factures.xaml
    /// </summary>
    public partial class Factures : Window
    {
        bank_dbEntities3 maBanque;
        List<compte> listcomptes = new List<compte>();
        client factureClient = new client();
        Boolean compteSelected = false;
        compte compteMarge = new compte();
        Boolean marge = false;
        public Factures(client client)
        {
            InitializeComponent();
            factureClient = client;
            maBanque = new bank_dbEntities3();
            listcomptes = (from c in maBanque.comptes where c.IDClient == client.IDClient select c).ToList();
            foreach (compte item in listcomptes)
            {
                if(item.Type == "cheque")
                {
                    BoxFactures.Items.Add(item.Type + " " + item.IDCompte);
                }
                else if (item.Type == "marge")
                {
                    compteMarge = item;
                    marge = true;
                }
            }
        }

        private void BoxFactures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            compteSelected = true;
            try
            {
                if (string.IsNullOrEmpty(txtFacture.Text.Trim().ToString()) || string.IsNullOrEmpty(txtcompte.Text.Trim().ToString())) BtnFacture.IsEnabled = false;
                else if (compteSelected
                        && !string.IsNullOrEmpty(txtFacture.Text.Trim().ToString())
                        && decimal.Parse(txtFacture.Text.Trim().ToString()) >= 0
                        && !string.IsNullOrEmpty(txtcompte.Text.Trim().ToString()))
                {
                    BtnFacture.IsEnabled = true;
                }
                else BtnFacture.IsEnabled = false;
            }
            catch(Exception)
            {
                MessageBox.Show("Il est imperarif de selectionner\nun compte cheque", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtFacture_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFacture.Text.Trim().ToString()) || string.IsNullOrEmpty(txtcompte.Text.Trim().ToString())) BtnFacture.IsEnabled = false;
                else if (compteSelected
                        && !string.IsNullOrEmpty(txtFacture.Text.Trim().ToString())
                        && decimal.Parse(txtFacture.Text.Trim().ToString()) >= 0
                        && !string.IsNullOrEmpty(txtcompte.Text.Trim().ToString()))
                {
                    BtnFacture.IsEnabled = true;
                }
                else BtnFacture.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("seul l'utilisation des numéros \net du virgule ',' est autorisé", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnFacture_Click(object sender, RoutedEventArgs e)
        {
            DateTime date_time;
            date_time = DateTime.Now;
            Transaction factureTransaction = new Transaction();
            Transaction margeTransaction = new Transaction();
            int idCompte = int.Parse(BoxFactures.SelectedItem.ToString().Split(' ')[1]);
            Boolean transactionOk = false;
            Boolean margeEpuiser = false;
            double fraisFacture = 1.25 ;
            double montantFacture = double.Parse(txtFacture.Text.Trim().ToString()) + fraisFacture;

            foreach (compte item in listcomptes)
            {
                if(item.IDCompte == idCompte)
                {
                    if(item.Montant >= decimal.Parse(montantFacture.ToString()))
                    {
                        item.Montant -= decimal.Parse(montantFacture.ToString());
                        factureTransaction.compteFrom = idCompte;
                        factureTransaction.compteTo = 1;
                        factureTransaction.date = date_time;
                        factureTransaction.typeTransaction = "facture";
                        factureTransaction.montant = decimal.Parse(montantFacture.ToString());
                        transactionOk = true;
                    }
                    else
                    {
                        if(marge)
                        {
                            if (MessageBox.Show("Votre solde est insuffisant pour payer\nvotre facture. \nVoulez vous compenser les fonds manquants " +
                                "\ndepuis votre marge de credit ?", "Question !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                decimal balance = decimal.Parse(montantFacture.ToString()) - item.Montant;
                                //Transaction marge et mise à jour marge.Montant
                                margeTransaction.compteFrom = compteMarge.IDCompte;
                                margeTransaction.compteTo = 1;
                                margeTransaction.date = date_time;
                                margeTransaction.typeTransaction = "facture";
                                margeTransaction.montant = balance;
                                compteMarge.Montant += balance;
                                margeEpuiser = true;

                                //Transaction cheque et mise à jour cheque.Montant
                                factureTransaction.compteFrom = idCompte;
                                factureTransaction.compteTo = 1;
                                factureTransaction.date = date_time;
                                factureTransaction.typeTransaction = "facture";
                                factureTransaction.montant = item.Montant;
                                item.Montant = 0;
                                transactionOk = true;
                            }
                            else
                            {
                                txtcompte.Clear();
                                txtFacture.Clear();
                                BoxFactures.SelectedIndex = -1;
                                BtnFacture.IsEnabled = false;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Il est impossible de payer cette facture \nVeuillez alimenter votre compte!",
                                "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            txtcompte.Clear();
                            txtFacture.Clear();
                            BoxFactures.SelectedIndex = -1;
                            BtnFacture.IsEnabled = false;
                        }
                    }
                }
            }
            if (transactionOk)
            {
                maBanque.Transactions.Add(factureTransaction);
                if(margeEpuiser) maBanque.Transactions.Add(margeTransaction);
                maBanque.SaveChanges();

                // Message aprés facture,
                if (MessageBox.Show("Payement facture éffectué avec Succé!!\nDésirez-vous faire un autre Payement ?", "Question !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    txtcompte.Clear();
                    txtFacture.Clear();
                    BoxFactures.SelectedIndex = -1;
                    BtnFacture.IsEnabled = false;
                }
                else
                {
                    EspaceClient retour = new EspaceClient((client)factureClient);
                    retour.Show();
                    this.Close();
                }
            }
        }

        private void BtnComptes_Click(object sender, RoutedEventArgs e)
        {
            //Affichage la page compte client.
            SoldeComptes soldeClient = new SoldeComptes((client)factureClient);
            soldeClient.Show();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous quitter 'Facture' My Bank ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on revient à la page Espace client.
            {
                EspaceClient retour = new EspaceClient((client)factureClient);
                retour.Show();
                this.Close();
            }
        }
    }
}
