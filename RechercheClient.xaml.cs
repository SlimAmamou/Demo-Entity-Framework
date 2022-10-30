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
    /// Logique d'interaction pour RechercheClient.xaml
    /// </summary>
    
    public partial class RechercheClient : Window
    {
        List<client> lstclients = new List<client>();        
        bank_dbEntities3 maBanque;
        
        public RechercheClient()
        {
            InitializeComponent();            
            maBanque = new bank_dbEntities3();
            lstclients = (from c in maBanque.clients select c).ToList();
            lstclients = lstclients.Skip(1).ToList();
            listClients.ItemsSource = lstclients;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var nomClient = boxName.Text;
            var prenomClient = boxPrenom.Text;
            List<client> lstrecherche = new List<client>();
            
            foreach (client membre in lstclients)
            {
                if((membre.Nom).ToLower().Contains(nomClient.ToLower()) && membre.Prenom.ToLower().Contains(prenomClient.ToLower()))
                {
                    lstrecherche.Add(membre);
                }               
                else
                {
                    continue;
                }
            }
            if (nomClient == "" && prenomClient == "")
            {
                listClients.ItemsSource = lstclients;
            }
            else
            {
                listClients.ItemsSource = lstrecherche;
            }            
        }

        private void listClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OperationClient profil = new OperationClient((client)listClients.SelectedItem);
            profil.Show();
            this.Close();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            AcceuilApp retour = new AcceuilApp();
            retour.Show();
            this.Close();
        }
    }
}
