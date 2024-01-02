﻿using System;
using Xamarin.Forms;
using TpExam.Models;
using System.Linq;

namespace TpExam
{
    public partial class CommandsPage : ContentPage
    {
        public CommandsPage()
        {
            InitializeComponent();
            LoadCommands(); // Load and display commands when the page is created
        }

        private void LoadCommands()
        {
            // Fetch and display a list of commands with associated details (including LigneCommande items)
            CommandsCollectionView.ItemsSource = App.Database.GetAllCommandsWithDetails();
        }

        private async void CommandsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle the selection of a command (if needed)
            // For example, you might navigate to a detailed view of the selected command
            if (e.CurrentSelection.FirstOrDefault() is Commande selectedCommand)
            {
                // Log or debug statement to check if this block is executed
                System.Diagnostics.Debug.WriteLine($"Selected Command ID: {selectedCommand.Id}");

                // Create an instance of CommandDetailPage and set its BindingContext
                CommandDetailPage commandDetailPage = new CommandDetailPage(selectedCommand);

                // Navigate to the CommandDetailPage
                await Navigation.PushAsync(commandDetailPage);
            }
        }
    }
}
