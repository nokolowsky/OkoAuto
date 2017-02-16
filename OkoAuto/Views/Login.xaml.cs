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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OkoAuto;
using OkoAuto.Logic;
namespace OkoAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            ApplicationStateManager ApplicationStateManager = new ApplicationStateManager();
            ApplicationStateManager.Initialize();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Authentications authentication = new Authentications();
            if(authentication.Authenticate(txtName.Text, txtPassword.Text))
            {
                var newForm = new Home(); //create your new form.
                newForm.Show();
                this.Close();
            }
            else
            {
                IncorrectLogin.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
}
