using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TpExam
{
    public partial class Administration : ContentPage
    {
        private const string UsernameKey = "Username";
        private const string PasswordKey = "Password";

        public Administration()
        {
            InitializeComponent();

            // Load saved credentials if available
            LoadSavedCredentials();
        }

        private void LoadSavedCredentials()
        {
            try
            {
                string username = SecureStorage.GetAsync(UsernameKey).Result;
                string password = SecureStorage.GetAsync(PasswordKey).Result;

                UsernameEntry.Text = username;
                PasswordEntry.Text = password;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Get entered username and password
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Perform authentication logic (replace with your actual authentication logic)
            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                // Save credentials securely
                SecureStorage.SetAsync(UsernameKey, username);
                SecureStorage.SetAsync(PasswordKey, password);

                // Navigate to the next page (replace with the desired navigation logic)
                Navigation.PushAsync(new AdministrationHomePage());
            }
            else
            {
                // Show an error message or handle unsuccessful login
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            return username == "admin" && password == "admin";
        }
    }
}