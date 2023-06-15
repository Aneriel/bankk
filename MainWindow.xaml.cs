using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace bankk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Customer> MyCustomers { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            using (BankContext _context = new BankContext())
            {
                // Sprawdź, czy istnieje klient o podanym emailu i haśle
                Customer loggedInCustomer = _context.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);

                if (loggedInCustomer != null)
                {
                    // Pobierz konto zalogowanego klienta
                    Account loggedInAccount = _context.Accounts.FirstOrDefault(a => a.CustomerId == loggedInCustomer.CustomerId);

                    if (loggedInAccount != null)
                    {
                        // Zalogowano pomyślnie
                        MessageBox.Show("Zalogowano pomyślnie!");

                        LoggedInWindow loggedInWindow = new LoggedInWindow(loggedInAccount);
                        loggedInWindow.ShowDialog();
                    }
                    else
                    {
                        // Konto nie zostało znalezione
                        MessageBox.Show("Nie znaleziono konta dla zalogowanego klienta.");
                    }
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe dane logowania. Spróbuj ponownie.");
                }
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUserWindow = new CreateUserWindow();
            createUserWindow.ShowDialog();
        }
    }
}
