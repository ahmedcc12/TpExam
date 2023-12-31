﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TpExam.Models;
using System.Linq;

namespace TpExam
{
    public partial class Panier : ContentPage
    {
        private List<LigneCommande> cartItems;
        private bool isPageInitialized = false;

        public Panier()
        {
            InitializeComponent();
            InitializePage();
        }

        private void InitializePage()
        {
            if (!isPageInitialized)
            {
                LoadProducts();
                UpdateTotal();
                isPageInitialized = true;
            }
        }

        private void LoadProducts()
        {
            // Load and display products in the cart
            cartItems = App.Database.GetCartItems();

            // Fetch product information for each item using IdProduit
            foreach (var item in cartItems)
            {
                item.Produit = App.Database.GetProductById(item.IdProduit);
            }

            ProductsListView.ItemsSource = cartItems;
        }

        private void IncreaseQuantity(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            LigneCommande ligneCommande = (LigneCommande)button.BindingContext;
            ligneCommande.Quantite++;
            UpdateCartItem(ligneCommande);
        }

        private void DecreaseQuantity(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            LigneCommande ligneCommande = (LigneCommande)button.BindingContext;

            if (ligneCommande.Quantite > 1)
            {
                ligneCommande.Quantite--;
                UpdateCartItem(ligneCommande);
            }
            else
            {
                // Optionally: Remove the item from the cart if quantity is 1 or less
                RemoveCartItem(ligneCommande);
            }
        }

        private void UpdateCartItem(LigneCommande ligneCommande)
        {
            App.Database.UpdateCartItem(ligneCommande);
            LoadProducts(); // Reload the products in the cart
            UpdateTotal();
        }

        private void RemoveCartItem(LigneCommande ligneCommande)
        {
            App.Database.RemoveFromCart(ligneCommande.Produit);
            LoadProducts(); // Reload the products in the cart
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            try
            {
                decimal total = cartItems.Sum(item => (decimal)item.Quantite * (decimal)item.Produit.Prix);
                TotalLabel.Text = $"Total: {total:C}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in updatetotal: {e.Message}");
            }
        }
        private async void CheckoutButton_Clicked(object sender, EventArgs e)
        {
            // Prompt the user for their username
            string username = await DisplayPromptAsync("Checkout", "Enter your username:");

            if (string.IsNullOrEmpty(username))
            {
                // User canceled or entered an empty username
                return;
            }

            // Create a new Commande object
            Commande newCommande = new Commande
            {
                NomClient = username,
                LignesCommande = cartItems.Select(item => new LigneCommande
                {
                    IdProduit = item.IdProduit,
                    Quantite = item.Quantite
                }).ToList()
            };

            // Save the new command to the database
            App.Database.EnregistrerCommande(newCommande);

            // Remove the items from the cart
            foreach (var item in cartItems)
            {
                App.Database.RemoveFromCart(item.Produit);
            }

            // Clear the cartItems list
            cartItems.Clear();

            // Update the ProductsListView to reflect the changes
            ProductsListView.ItemsSource = cartItems;

            // Display a success message
            await DisplayAlert("Success", "Order placed successfully", "OK");

            // Optionally, navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
