// Dodaj potrzebne using-i

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace bankk
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the highest CustomerId from the Customers table
            int highestCustomerId = 0;
            using (BankContext _context = new BankContext())
            {
                highestCustomerId = _context.Customers.Max(c => c.CustomerId);
            }
            // Increment the highest CustomerId by 1 to set the new CustomerId
            int newCustomerId = highestCustomerId + 1;

            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Walidacja danych
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Sprawdzenie unikalności adresu email
            using (BankContext _context = new BankContext())
            {
                if (_context.Customers.Any(c => c.Email == email))
                {
                    MessageBox.Show("Email already exists. Please use a different email.");
                    return;
                }

                // Dodawanie użytkownika do bazy danych
                Customer newCustomer = new Customer
                {
                    CustomerId = newCustomerId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password
                };

                // Tworzenie nowego konta dla użytkownika
                Account newAccount = new Account
                {
                    AccountId = GenerateAccountNumber(), // Wygenerowanie unikalnego numeru konta
                    Balance = 0,
                    Customer = newCustomer
                };

                _context.Customers.Add(newCustomer);
                _context.Accounts.Add(newAccount);
                _context.SaveChanges();
            }

            MessageBox.Show("User added successfully.");
            this.Close();
        }
        private int GenerateAccountNumber()
        {
            Random random = new Random();
            int accountNumber = 1000000000 + random.Next(0, 900000000);
            return accountNumber;
        }

    }
}
