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
    /// Logique d'interaction pour RetraitCash.xaml
    /// </summary>
    public partial class RetraitCash : Window
    {
        client retraitClient = new client();
        bank_dbEntities3 maBanque;
        List<compte> listcomptes = new List<compte>();
        compte compteMarge = new compte();
        Boolean marge = false;
        compte guichet = new compte();
        Boolean compteSelected = false;

        public RetraitCash(client client)
        {
            InitializeComponent();
            retraitClient = client;
            maBanque = new bank_dbEntities3();
            listcomptes = (from c in maBanque.comptes where c.IDClient == client.IDClient select c).ToList();
            guichet = (from c in maBanque.comptes where c.IDClient == 2 select c).ToList()[0];

            foreach (compte retrait in listcomptes)
            {
                if (retrait.Type == "cheque" || retrait.Type == "epargne")
                {
                    BoxRetrait.Items.Add(retrait.Type + " " + retrait.IDCompte);
                }
                else if(retrait.Type == "marge")
                {
                    marge = true;
                    compteMarge = retrait;
                }
            }
        }

        private void BtnRetrait_Click(object sender, RoutedEventArgs e)
        {
            int montantRetrait = int.Parse(txtRetrait.Text);
            DateTime date_time;
            date_time = DateTime.Now;
            string numCompte = BoxRetrait.SelectedItem.ToString();
            string[] selects = numCompte.Split(' ');
            int retraitCompte = int.Parse(selects[1]);
            Transaction RetraiTransaction = new Transaction();
            Transaction MargeTransaction = new Transaction();
            if(guichet.Montant >= montantRetrait)
            {            
            // Mise à jour montant compte et implementation transaction.
                foreach (compte item in listcomptes)
                {
                    if (montantRetrait <= item.Montant && item.IDCompte == retraitCompte)
                    {
                        item.Montant -= montantRetrait;        // mise à jour compte
                        guichet.Montant -= montantRetrait;     // mise à jour guichet 
                        // Transaction retrait
                        RetraiTransaction.compteFrom = retraitCompte;
                        RetraiTransaction.compteTo = 1;
                        RetraiTransaction.montant = montantRetrait;
                        RetraiTransaction.date = date_time;
                        RetraiTransaction.typeTransaction = "retrait";
                        maBanque.Transactions.Add(RetraiTransaction);
                        maBanque.SaveChanges();
                        MessageBox.Show("Retrait éffectué avec success", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        BoxRetrait.SelectedIndex = -1;
                        txtRetrait.Clear();
                        compteSelected = false;
                    }
                    else if(montantRetrait >= item.Montant && item.IDCompte == retraitCompte)
                    {
                        if (!marge)
                        {
                            MessageBox.Show("Impossible d'avoir acces à ce montant\nvous pouvez au maximum retirer " + (item.Montant - item.Montant % 10).ToString() + "$",
                               "Information", MessageBoxButton.OK, MessageBoxImage.Information);                        
                            txtRetrait.Clear();
                        }
                        else
                        {
                            if (MessageBox.Show("Le montant demandé est superieur à votre solde\nVoulez vous terminer l'opération \ndepuis votre Marge de credit?"
                                , "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                decimal montantMarge = montantRetrait - item.Montant;
                                item.Montant = 0;                                   // mise à jour compte
                                compteMarge.Montant += montantMarge;                // mise à jour marge 
                                guichet.Montant -= montantRetrait;                  // mise à jour guichet

                                // transaction retrait.
                                RetraiTransaction.compteFrom = retraitCompte;
                                RetraiTransaction.compteTo = 1;
                                RetraiTransaction.montant = montantRetrait - montantMarge;
                                RetraiTransaction.date = date_time;
                                RetraiTransaction.typeTransaction = "retrait";
                                // Transaction Marge.
                                MargeTransaction.compteFrom = compteMarge.IDCompte;
                                MargeTransaction.compteTo = 1;
                                MargeTransaction.montant = montantMarge;
                                MargeTransaction.date = date_time;
                                MargeTransaction.typeTransaction = "retrait";

                                maBanque.Transactions.Add(RetraiTransaction);
                                maBanque.Transactions.Add(MargeTransaction);
                                maBanque.SaveChanges();
                                MessageBox.Show("Retrait éffectué avec success", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                BoxRetrait.SelectedIndex = -1;
                                txtRetrait.Clear();
                                compteSelected = false;
                            }
                            else
                            {
                                BoxRetrait.SelectedIndex = -1;
                                txtRetrait.Clear();
                                compteSelected = false;
                            }
                        }
                    }                
                }
            }
            else
            {
                if (guichet.Montant > 0) MessageBox.Show("Vous pouvez au maximum retirer " + guichet.Montant.ToString() +"$","Information",MessageBoxButton.OK,MessageBoxImage.Information);
                else MessageBox.Show("Le guichet est videnVeuillez reessayer plus tard","Information",MessageBoxButton.OK,MessageBoxImage.Information);


            }
        }

        private void txtRetrait_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRetrait.Text.Trim()))
                {
                    BtnRetrait.IsEnabled = false;
                }
                else if(compteSelected
                    && !string.IsNullOrEmpty(txtRetrait.Text.Trim())
                    && int.Parse(txtRetrait.Text.Trim().ToString()) > 0
                    && int.Parse(txtRetrait.Text.Trim().ToString()) <= 1000
                    && int.Parse(txtRetrait.Text.Trim().ToString()) % 10 == 0)
                {
                    BtnRetrait.IsEnabled = true;
                }
                else if (int.Parse(txtRetrait.Text.Trim().ToString()) >= 1000 || (int.Parse(txtRetrait.Text.Trim().ToString()) >= 100) && int.Parse(txtRetrait.Text.Trim().ToString()) % 10 != 0)
                {
                    MessageBox.Show("Un retrait doit être un multiple de 10\net ne peut dépasser 1000$",
                        "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    BtnRetrait.IsEnabled = false;
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
                if (string.IsNullOrEmpty(txtRetrait.Text.Trim()))
                {
                    BtnRetrait.IsEnabled = false;
                }
                else if (compteSelected
                    && !string.IsNullOrEmpty(txtRetrait.Text.Trim())
                    && int.Parse(txtRetrait.Text.Trim().ToString()) > 0
                    && int.Parse(txtRetrait.Text.Trim().ToString()) <= 1000
                    && int.Parse(txtRetrait.Text.Trim().ToString()) % 10 == 0)
                {
                    BtnRetrait.IsEnabled = true;
                }
                else if(int.Parse(txtRetrait.Text.Trim().ToString()) >= 1000 || (int.Parse(txtRetrait.Text.Trim().ToString()) >= 100) && int.Parse(txtRetrait.Text.Trim().ToString())%10 != 0)
                {
                    MessageBox.Show("Un retrait doit être un multiple de 10\net ne peut dépasser 1000$",
                        "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }                 
                else
                {
                    BtnRetrait.IsEnabled = false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("presence de caractère non numérique \ndans le montant à retirer!", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void BtnComptes_Click(object sender, RoutedEventArgs e)
        {
            //Affichage la page compte client.
            SoldeComptes soldeClient = new SoldeComptes((client)retraitClient);
            soldeClient.Show();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous quitter Retrait My Bank ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on revient à la page Espace client.
            {
                EspaceClient retour = new EspaceClient((client)retraitClient);
                retour.Show();
                this.Close();
            }
        }
    }
}
