﻿using Inventory.Models;
using Inventory.Models.LoginPageModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private const string Url = "https://ubd-fpt-inventory.azurewebsites.net/Token";     
        private HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
            Background.Source = ImageSource.FromResource("Inventory.Image.background.jpg");
            Logo.Source = ImageSource.FromResource("Inventory.Image.mindef.png");
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            Error.Text = "";
            if (Username.Text.Equals("") || Password.Text.Equals(""))
            {
                await DisplayAlert("Notice", "Please enter username and password", "Okay");
                Error.Text = "Username and password can't be empty";
            }
            else
            {
                Loading.IsVisible = true;
                var dict = new Dictionary<string, string>();
                dict.Add("username", Username.Text);
                dict.Add("password", Password.Text);
                dict.Add("grant_type", "password");
                var client = new HttpClient();

                try
                {
                    var response = await client.PostAsync(Url, new FormUrlEncodedContent(dict));
                    if (response.IsSuccessStatusCode)
                    {
                        var text = response.Content.ReadAsStringAsync();

                        var Token = JsonConvert.DeserializeObject<Token>(text.Result);

                        Application.Current.Properties["Token"] = Token.access_token;
                        Application.Current.Properties["Name"] = Token.userName;
                        Application.Current.Properties["Type"] = Token.token_type;
                        Loading.IsVisible = false;
                        await Navigation.PushAsync(new MainMenu());
                    }
                    else
                    {
                        Loading.IsVisible = false;
                        var text = response.Content.ReadAsStringAsync();
                        var Err = JsonConvert.DeserializeObject<Error>(text.Result);
                        if (Err.error.Contains("invalid"))
                        {
                            Error.Text = "Username or password is incorrect";
                        }
                        else if (Err.error.Contains("inactive"))
                        {
                            Error.Text = "Your account has been deactived";
                        }
                        else
                        {
                            Error.Text = "Your account hasn't been confirmed";
                        }                     
                        await DisplayAlert("Error", Err.error_description, "Noticed");
                    }
                }
                catch (Exception Err)
                {
                    Loading.IsVisible = false;
                    Error.Text = "No connection to server";
                    await DisplayAlert("Error", "No connection to server", "Noticed");
                }
            }

            // await Navigation.PushAsync(new MainMenu());
        }

        protected override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("Token"))
            {
                if (Application.Current.Properties["Token"] != null)
                {
                    Navigation.PushAsync(new MainMenu());
                }
            }
            base.OnAppearing();
        }


        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (height<400)
            {
                Logo.IsVisible = false;
            }
            else
            {
                Logo.IsVisible = true;
            }
        }
    }
}

