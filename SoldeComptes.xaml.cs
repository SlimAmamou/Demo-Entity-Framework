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
    /// Logique d'interaction pour SoldeComptes.xaml
    /// </summary>
    public partial class SoldeComptes : Window
    {
               
        bank_dbEntities3 maBanque;
        List<compte> listcomptes = new List<compte>();
        public SoldeComptes(client client)
        {
            InitializeComponent();
            maBanque = new bank_dbEntities3();
            // Affichage text d'acceuil
            txtSoldAcceuil.Content = "   État de comptes de \n   M. " + client.Prenom.ToString()+ " " + client.Nom.ToString() + ".";
            
            //Récuperation et Affichage  liste des comptes clients.
            listcomptes = (from c in maBanque.comptes where c.IDClient == client.IDClient select c).ToList();
            lstcomptes.ItemsSource = listcomptes;
        }
    }
}
