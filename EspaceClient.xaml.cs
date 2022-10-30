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
    /// Logique d'interaction pour EspaceClient.xaml
    /// </summary>
    public partial class EspaceClient : Window
    {
        bank_dbEntities3 maBanque;
        client client = new client();
        public EspaceClient(client loginClient)
        {
            InitializeComponent();
            maBanque = new bank_dbEntities3();
            client = loginClient;
            txtAcceuil.Content = "Bonjour " + client.Prenom.ToString() + ",\nMy Bank vous souhaite la bienvenue\nque voulez vous faire aujourd'hui?";
        }

        private void BtnDepot_Click(object sender, RoutedEventArgs e)
        {
            DepotCash depot = new DepotCash((client)client);
            depot.Show();
            this.Close();
        }

        private void BtnRetrait_Click(object sender, RoutedEventArgs e)
        {
            RetraitCash retrait = new RetraitCash((client)client);
            retrait.Show();
            this.Close();
        }

        private void BtnTransfert_Click(object sender, RoutedEventArgs e)
        {
            TransfertFonds transfert = new TransfertFonds((client)client);
            transfert.Show();
            this.Close();
        }

        private void BtnFacture_Click(object sender, RoutedEventArgs e)
        {
            Factures facture = new Factures((client)client);
            facture.Show();
            this.Close();
        }

        private void BtnComptes_Click(object sender, RoutedEventArgs e)
        {
            SoldeComptes soldeClient = new SoldeComptes((client) client);
            soldeClient.Show();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous réellement quitter My Bank ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on met fin à l'application.
            {
                Login retour = new Login();
                retour.Show();
                this.Close();
            }
        }
    }
}
