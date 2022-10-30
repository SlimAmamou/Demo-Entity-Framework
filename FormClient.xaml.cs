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
    /// Logique d'interaction pour FormClient.xaml
    /// </summary>
    public partial class FormClient : Window
    {
        bank_dbEntities3 maBanque;
        public FormClient()
        {
            InitializeComponent();
            txtPrenom.Focus();
            maBanque = new bank_dbEntities3();
            
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client newclient = new client();
                compte newcompte = new compte();

                //Paramétres nouveau client
                newclient.Prenom = txtPrenom.Text.ToString();
                newclient.Nom = txtNom.Text.ToString();
                newclient.Courriel = txtCourriel.Text.ToString();
                newclient.Telephone = long.Parse(txtTelephone.Text.ToString());
                newclient.Nip = int.Parse(txtNip.Text.ToString());
                newclient.Acces = true;
                newcompte.Montant = decimal.Parse(txtDepot.Text.ToString());
                newcompte.Type = "cheque";

                // Insertion nouveau client
                maBanque.clients.Add(newclient);
                maBanque.SaveChanges();

                // Insertion compte chéque obligatoire aprés récupération de IDClient
                var result = (from c in maBanque.clients where c.Telephone == newclient.Telephone select c).ToList();
                newcompte.IDClient = result[0].IDClient;
                maBanque.comptes.Add(newcompte);
                maBanque.SaveChanges();

                //select numéro de compte chéque
                var result2 = (from c in maBanque.comptes where c.Type == "cheque" && c.IDClient == newcompte.IDClient select c).ToList();

                MessageBox.Show("Implémentation client réussi \n N° de compte chéque: " + result2[0].IDCompte.ToString() +".", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Netoyage textBoxs
                txtTelephone.Text = String.Empty;
                txtCourriel.Text = String.Empty;
                txtDepot.Text = String.Empty;
                txtNip.Text = String.Empty;
                txtNom.Text = String.Empty;
                txtPrenom.Text = String.Empty;
                txtPrenom.Focus();

            }
            catch (Exception)
            {
                MessageBox.Show("Implémentation client échouée verifiez vos données", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            AcceuilApp retour = new AcceuilApp();
            retour.Show();
            this.Close();
        }

        private void Verifier_Info(object sender, TextChangedEventArgs e)
        {
            try
            {
                int count = txtCourriel.Text.ToString().Count(f => f == '@');
                int countPt = txtCourriel.Text.ToString().Count(f => f == '.');
                if (
                    !string.IsNullOrEmpty(txtTelephone.Text.Trim()) && long.Parse(txtTelephone.Text.ToString()) > 10000000000
                    && !string.IsNullOrEmpty(txtPrenom.Text.Trim())
                    && !string.IsNullOrEmpty(txtNom.Text.Trim())
                    && !string.IsNullOrEmpty(txtDepot.Text.Trim()) && decimal.Parse(txtNip.Text.ToString()) >= 0
                    && !string.IsNullOrEmpty(txtCourriel.Text.Trim()) && count == 1 && countPt >= 1
                    && !string.IsNullOrEmpty(txtNip.Text.Trim()) && decimal.Parse(txtDepot.Text.ToString()) > 0
                    )
                {
                    btnRecord.IsEnabled = true;
                }
                else
                {
                    btnRecord.IsEnabled = false;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Implémentation De caractère Invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
