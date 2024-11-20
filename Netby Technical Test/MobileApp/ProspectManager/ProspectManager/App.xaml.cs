using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace ProspectManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true; // Acepta todos los certificados, incluso los no válidos
            };

            MainPage = new AppShell();
            
        }
    }
}
