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
    /// Logique d'interaction pour TransfertFonds.xaml
    /// </summary>
    public partial class TransfertFonds : Window
    {
        bank_dbEntities3 maBanque;
        List<compte> listcomptes = new List<compte>();
        List<compte> listcomptesFrom = new List<compte>();
        List<compte> listcomptesTo = new List<compte>();
        client transfertClient = new client();
        Boolean compteFromSelected = false;
        Boolean compteToSelected = false;
        public TransfertFonds(client client)
        {
            InitializeComponent();
            transfertClient = client;
            maBanque = new bank_dbEntities3();
            listcomptes = (from c in maBanque.comptes where c.IDClient == client.IDClient select c).ToList();

            foreach(compte transfert in listcomptes)
            {
                if (transfert.Type == "cheque") BoxFrom.Items.Add(transfert.Type + " " + transfert.IDCompte);
                else BoxTo.Items.Add(transfert.Type + " " + transfert.IDCompte);
            }
        } 

        private void BtnComptes_Click(object sender, RoutedEventArgs e)
        {
            //Affichage la page compte client.
            SoldeComptes soldeClient = new SoldeComptes((client)transfertClient);
            soldeClient.Show();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous quitter Transfert My Bank ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on revient à la page Espace client.
            {
                EspaceClient retour = new EspaceClient((client)transfertClient);
                retour.Show();
                this.Close();
            }
        }

        private void BtnTransfert_Click(object sender, RoutedEventArgs e)
        {
            DateTime date_time;
            date_time = DateTime.Now;
            Transaction transfertTransaction = new Transaction();
            int compteFrom = int.Parse(BoxFrom.SelectedItem.ToString().Split(' ')[1]);
            int compteTo = int.Parse(BoxTo.SelectedItem.ToString().Split(' ')[1]);
            Boolean transactionOk = false;

            // Mise à jour montant compte et implementation transaction.
            foreach (compte item in listcomptes)
            {
                if(item.IDCompte == compteFrom)
                {
                    if(item.Montant<= decimal.Parse(txtMontant.Text.ToString()))
                    {
                        MessageBox.Show("Votre solde est insufisant pour effectuer \n" +
                            "cette transaction","Information",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                        BtnTransfert.IsEnabled = false;
                        txtMontant.Clear();
                        break;
                    }
                    else
                    {
                        item.Montant -= decimal.Parse(txtMontant.Text.ToString());
                        transfertTransaction.compteFrom = item.IDCompte;
                        transfertTransaction.date = date_time;
                        transfertTransaction.montant = decimal.Parse(txtMontant.Text.ToString());
                        transfertTransaction.typeTransaction = "Transfert";
                        transactionOk = true;
                    }                    
                }
                else if(item.IDCompte == compteTo)
                {
                    transfertTransaction.compteTo = item.IDCompte;
                    if(item.Type == "marge") item.Montant -= decimal.Parse(txtMontant.Text.ToString());
                    else item.Montant += decimal.Parse(txtMontant.Text.ToString());
                }
            }
            if (transactionOk)
            {
                maBanque.Transactions.Add(transfertTransaction);
                maBanque.SaveChanges();

                // Message aprés transfert,
                if (MessageBox.Show("Transfert éffectué avec Succé!!\nDésirez-vous faire un autre Transfert ?", "Question !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    BoxFrom.SelectedIndex = -1;
                    BoxTo.SelectedIndex = -1;
                    txtMontant.Clear();
                    compteFromSelected = false;
                    compteToSelected = false;
                }
                else
                {
                    EspaceClient retour = new EspaceClient((client)transfertClient);
                    retour.Show();
                    this.Close();
                }
            }            
        }

        private void txtMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtMontant.Text.Trim()))
                {
                    BtnTransfert.IsEnabled = false;
                }
                else if(compteFromSelected
                    && compteToSelected
                    && !string.IsNullOrEmpty(txtMontant.Text.Trim())
                    && decimal.Parse(txtMontant.Text.Trim().ToString()) >= 0)
                {
                    BtnTransfert.IsEnabled = true;
                }
                else
                {
                    BtnTransfert.IsEnabled = false;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("seul l'utilisation des numéros \net du virgule ',' est autorisé" , "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BoxFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            compteFromSelected = true;
            try
            {
                if (string.IsNullOrEmpty(txtMontant.Text.Trim()))
                {
                    BtnTransfert.IsEnabled = false;
                }
                else if (compteFromSelected
                    && compteToSelected
                    && !string.IsNullOrEmpty(txtMontant.Text.Trim())
                    && decimal.Parse(txtMontant.Text.Trim().ToString()) >= 0)
                {
                    BtnTransfert.IsEnabled = true;
                }
                else
                {
                    BtnTransfert.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Il est imperarif de selectionner\nun compte from", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BoxTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            compteToSelected = true;
            try
            {
                if (string.IsNullOrEmpty(txtMontant.Text.Trim()))
                {
                    BtnTransfert.IsEnabled = false;
                }
                else if (compteFromSelected
                    && compteToSelected
                    && !string.IsNullOrEmpty(txtMontant.Text.Trim())
                    && decimal.Parse(txtMontant.Text.Trim().ToString()) >= 0)
                {
                    BtnTransfert.IsEnabled = true;
                }
                else
                {
                    BtnTransfert.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Il est imperarif de selectionner\nun compte to", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
