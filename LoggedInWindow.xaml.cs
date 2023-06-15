using System.Windows;

namespace bankk
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        private Account loggedInAccount;

        public LoggedInWindow(Account account)
        {
            InitializeComponent();
            loggedInAccount = account;
            UpdateAccountBalance();
        }

        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz wartość wpłaty z pola tekstowego
            if (decimal.TryParse(AmountTextBox.Text, out decimal depositAmount))
            {
                // Sprawdź, czy kwota wpłaty jest dodatnia
                if (depositAmount > 0)
                {
                    // Zaktualizuj saldo konta
                    loggedInAccount.Balance += depositAmount;

                    // Zapisz zmiany w bazie danych
                    using (BankContext _context = new BankContext())
                    {
                        _context.Accounts.Update(loggedInAccount);
                        _context.SaveChanges();
                    }

                    // Wyświetl informację o udanej wpłacie
                    MessageBox.Show($"Dokonano wpłaty. Aktualne saldo: {loggedInAccount.Balance}");

                    // Zaktualizuj wyświetlane saldo na stronie
                    UpdateAccountBalance();
                }
                else
                {
                    MessageBox.Show("Wprowadź dodatnią kwotę wpłaty.");
                }
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną kwotę wpłaty.");
            }
        }

        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz wartość wypłaty z pola tekstowego
            if (decimal.TryParse(AmountTextBox.Text, out decimal withdrawAmount))
            {
                // Sprawdź, czy kwota wypłaty jest dodatnia
                if (withdrawAmount > 0)
                {
                    if (loggedInAccount.Balance >= withdrawAmount)
                    {
                        // Zaktualizuj saldo konta
                        loggedInAccount.Balance -= withdrawAmount;

                        // Zapisz zmiany w bazie danych
                        using (BankContext _context = new BankContext())
                        {
                            _context.Accounts.Update(loggedInAccount);
                            _context.SaveChanges();
                        }

                        // Wyświetl informację o udanej wypłacie
                        MessageBox.Show($"Dokonano wypłaty. Aktualne saldo: {loggedInAccount.Balance}");

                        UpdateAccountBalance();
                    }
                    else
                    {
                        MessageBox.Show("Brak wystarczających środków na koncie.");
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadź dodatnią kwotę wypłaty.");
                }
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną kwotę wypłaty.");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            Close();
        }

        private void UpdateAccountBalance()
        {
            // Aktualizuj wyświetlane saldo na stronie
            BalanceTextBlock.Text = $"Saldo: {loggedInAccount.Balance}";
        }
    }
}
