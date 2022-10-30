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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        bank_dbEntities3 maBanque;
        int nbrTentatives = 0;
        string lastLogin = "";
        public Login()
        {
            InitializeComponent();
            txtuser.Focus();
            maBanque = new bank_dbEntities3();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = txtuser.Text;
            var password = txtPassword.Password;
            client guichet = (from c in maBanque.clients where c.IDClient == 2 select c).ToList()[0];
            
            if (guichet.Courriel == user && guichet.Nip == Convert.ToInt32(password))
            {
                AcceuilApp personnel = new AcceuilApp();
                personnel.Show();
                this.Close();
            }
            else if (guichet.Courriel == user && guichet.Nip != Convert.ToInt32(password))
            {
                MessageBox.Show("Attention!!! Le code d'acces Guichet est Érroné \n si le problème perciste veillez contacter " +
                    "votre superieur","Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Clear();
            }
            else if(guichet.Acces == true)
            {
                try
                {
                    client loginClient = (from c in maBanque.clients where c.Courriel == user select c).ToList()[0];
                    if(loginClient.Acces == true)
                    {
                        if (loginClient.Nip == Convert.ToInt32(password))
                        {
                            EspaceClient personnel = new EspaceClient((client)loginClient);
                            personnel.Show();
                            this.Close();
                        }
                        else if (loginClient.Nip != Convert.ToInt32(password) && nbrTentatives == 0)
                        {
                            nbrTentatives += 1;
                            lastLogin = loginClient.Courriel;
                            MessageBox.Show("Votre NIP est incorrect, \n Veuillez le corriger \n Il vous reste 2 tentatives","Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                        else if(loginClient.Nip != Convert.ToInt32(password) && nbrTentatives == 1)
                        {
                            if(loginClient.Courriel == lastLogin)
                            {
                                MessageBox.Show("Votre NIP est incorrect, \n Veuillez le corriger \n Il vous reste une seule tentative", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                nbrTentatives += 1;
                                txtPassword.Clear();
                                txtPassword.Focus();
                            }
                            else
                            {
                                nbrTentatives = 1;
                                lastLogin = loginClient.Courriel;
                                MessageBox.Show("Votre NIP est incorrect, \n Veuillez le corriger \n Il vous reste 2 tentatives", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                txtPassword.Clear();
                                txtPassword.Focus();
                            }
                        }
                        else if (loginClient.Nip != Convert.ToInt32(password) && nbrTentatives == 2)
                        {
                            if (loginClient.Courriel == lastLogin)
                            {
                                MessageBox.Show("l'accés à votre compte est bloqué \n Veuillez contacter votre banque", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                loginClient.Acces = false;
                                maBanque.SaveChanges();
                                Clean();
                            }
                            else
                            {
                                nbrTentatives = 1;
                                lastLogin = loginClient.Courriel;
                                MessageBox.Show("Votre NIP est incorrect, \n Veuillez le corriger \n Il vous reste 2 tentatives", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                txtPassword.Clear();
                                txtPassword.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("l'accés à votre compte est bloqué \n Veuillez contacter votre banque " +
                            "\n au :" + guichet.Telephone.ToString(), "Information client", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Clean();
                    }                    
                }
                catch (Exception)
                {
                    MessageBox.Show("nous n'avons aucun identifiant pour ce courriel!!");
                    Clean();
                }
                
            }
            else
            {
                MessageBox.Show("Nous sommes désolés \nLe guichet et en cours de maintenance \n Veuillez essayer plus tard.", "Information!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                Clean();
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous réellement quitter cette application ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            // Si tel est le cas, on met fin à l'application.
            {
                Application.Current.Shutdown();
            }
            // Si ce n'est pas le cas on remet tout à sa place.
            else
            {
                txtuser.Text = string.Empty;
                txtPassword.Password = string.Empty;
                txtuser.Focus();
                btnLogin.IsEnabled = false;
            }
        }

        private void txtPassword_Changed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPassword.Password.Trim())
                && txtPassword.Password.Trim().Length == 4
                && int.Parse(txtPassword.Password.ToString()) >= 0)
                {
                    btnLogin.IsEnabled = true;
                }
                else
                {
                    btnLogin.IsEnabled = false;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Le NIP est constitué éxclusivement de Chiffres!!!", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Clean()
        {
            txtPassword.Clear();
            txtuser.Clear();
            txtuser.Focus();
        }
    }
}
