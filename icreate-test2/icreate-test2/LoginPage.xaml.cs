﻿using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Json;
using Windows.Data.Json;
using System.Net.Http;

using Newtonsoft.Json;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace icreate_test2
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class LoginPage : icreate_test2.Common.LayoutAwarePage
    {
        private String username;
        private String password;
        private String domain;
        private String postString;

        public LoginPage()
        {
            this.InitializeComponent();
            hideUserInfo.Begin();
            myStoryboard.Begin();
            myStoryboard.Completed += myStoryboard_Completed;
        }

        void myStoryboard_Completed(object sender, object e)
        {
            userInfoOpacity.Begin();
            showUserInfo.Begin();
        }
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // ******************************* TO BE REMOVED *********************************************
            /*
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey("userID"))
            {
                UsernameTextBox.Text = roamingSettings.Values["userID"].ToString();
            }
            if (roamingSettings.Values.ContainsKey("password"))
            {
                PasswordBox.Password = roamingSettings.Values["password"].ToString();
            }
            if (roamingSettings.Values.ContainsKey("domain"))
            {
                DomainComboBox.SelectedItem = roamingSettings.Values["domain"].ToString();
            }
            */
            // ********************************************************************************************

            this.LoadUserCredentials();

            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageDialog noInternetDialog = new MessageDialog("There is currently no internet connection..", "Oops");
                noInternetDialog.Commands.Add(new UICommand("Go offline", new UICommandInvokedHandler(this.GoOfflineHandler)));
                noInternetDialog.ShowAsync();
            }
            else
            {
                // load userID, password and domain from setting data

                // if there have been token stored
                // load the token
<<<<<<< HEAD:icreate-test2/icreate-test2/LoginPage.xaml.cs
                if (false)
=======
                if (Utils.TokenManager.isTokenExisting())
>>>>>>> d9d84f2a13c127ec1f859e6c281dfb957de74da6:icreate-test2/icreate-test2/MainPage.xaml.cs
                {
                    // disable controls
                    UsernameTextBox.IsEnabled = false;
                    PasswordBox.IsEnabled = false;
                    DomainComboBox.IsEnabled = false;
                    LoginButton.IsEnabled = false;

                    // display progress circle
                    ProgressRing.IsActive = true;

                    // check if token is valid
<<<<<<< HEAD:icreate-test2/icreate-test2/LoginPage.xaml.cs
                    if (await Utils.TokenManager.IsTokenValid())
                    {
                        // if so, hide progress circle
                        ProgressRing.IsActive = false;
                        // navigate to main menu page
=======
>>>>>>> d9d84f2a13c127ec1f859e6c281dfb957de74da6:icreate-test2/icreate-test2/MainPage.xaml.cs


                    // if so, hide progress circle
                    ProgressRing.IsActive = false;

                    // save user credentials
                    this.SaveUserCredentials();

                    // navigate to main menu page


                    // if not, hide progress circle
                    // enable controls
                }
                else
                {
                    // do nothing?
                }
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
<<<<<<< HEAD:icreate-test2/icreate-test2/LoginPage.xaml.cs
            base.SaveState(pageState);
=======
>>>>>>> d9d84f2a13c127ec1f859e6c281dfb957de74da6:icreate-test2/icreate-test2/MainPage.xaml.cs
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageDialog noInternetDialog = new MessageDialog("There is currently no internet connection..", "Oops");
                noInternetDialog.Commands.Add(new UICommand("Go offline", new UICommandInvokedHandler(this.GoOfflineHandler)));
                await noInternetDialog.ShowAsync();
            }
            else
            {
                // disable controls
                UsernameTextBox.IsEnabled = false;
                PasswordBox.IsEnabled = false;
                DomainComboBox.IsEnabled = false;
                LoginButton.IsEnabled = false;

                // display progress ring
                ProgressRing.IsActive = true;

                username = UsernameTextBox.Text;
                password = PasswordBox.Password;
                domain = DomainComboBox.SelectedValue.ToString();

                postString = Utils.LAPI.GeneratePostString(username, password, domain);

                DataStructure.Token token = await LoginAsync(postString);
                
<<<<<<< HEAD:icreate-test2/icreate-test2/LoginPage.xaml.cs
                // hide progress ring
                ProgressRing.IsActive = false;

                if (token != null && token.TokenSuccess)
                {
                    // update token
                    Utils.TokenManager.UpdateToken(token);
                    SaveUserCredentials();
                }
                else
                {
                    MessageDialog noInternetDialog = new MessageDialog("Log in failed. Please check your userId and password", "Oops");
                    noInternetDialog.ShowAsync();
=======
                // returned JSON string for validation
                string responseString = streamRead.ReadToEnd();

                
                // remove the last "}"
                responseString = responseString.Remove(responseString.Length - 1);

                // remove the first "{" and its associated header
                responseString = responseString.Substring(responseString.IndexOf(":") + 1);

                DataStructure.Token token = JsonConvert.DeserializeObject<DataStructure.Token>(responseString);

                /*
                if (token != null && token.TokenSuccess.Equals(true))
                {
                    Utils.LAPI.token = token.TokenContent;
                    
                    Dispatcher.BeginInvoke(() =>
                    {
                        (Application.Current as App).online = true;

                        SaveCredentials();
                        loginProgressBar.IsIndeterminate = false;
                        NavigationService.Navigate(new Uri(("/MenuPage.xaml"), UriKind.Relative));
                    });
                     
                }
                else
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        loginProgressBar.IsIndeterminate = false;

                        MessageBox.Show("Log in failed");
                    });
                }
                */
                // Close the stream object
                streamResponse.Dispose();
                streamRead.Dispose();

                // Release the HttpWebResponse
                response.Dispose();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.RequestCanceled)
                {
                    /*
                    Dispatcher.BeginInvoke(() =>
                    {
                        Login();
                    });
                     */
>>>>>>> d9d84f2a13c127ec1f859e6c281dfb957de74da6:icreate-test2/icreate-test2/MainPage.xaml.cs
                }
            }

        }
<<<<<<< HEAD:icreate-test2/icreate-test2/LoginPage.xaml.cs
              
        private async Task<DataStructure.Token> LoginAsync(string data)
        {
            string authenticationURL = "https://ivle.nus.edu.sg/api/Lapi.svc/Login_JSON";
            HttpClient client = new HttpClient();

            HttpContent payload = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage response = client.PostAsync(authenticationURL, payload).Result;
            string responseString = await response.Content.ReadAsStringAsync();

            // remove the last "}"
            responseString = responseString.Remove(responseString.Length - 1);

            // remove the first "{" and its associated header
            responseString = responseString.Substring(responseString.IndexOf(":") + 1);

            DataStructure.Token token = JsonConvert.DeserializeObject<DataStructure.Token>(responseString);

            payload.Dispose();
            response.Dispose();
            client.Dispose();

            return token;
        }
=======
>>>>>>> d9d84f2a13c127ec1f859e6c281dfb957de74da6:icreate-test2/icreate-test2/MainPage.xaml.cs

        // loading user credential from application data settings
        private void LoadUserCredentials()
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (roamingSettings.Values.ContainsKey("userID"))
            {
                UsernameTextBox.Text = roamingSettings.Values["userID"].ToString();
            }
            if (roamingSettings.Values.ContainsKey("password"))
            {
                PasswordBox.Password = roamingSettings.Values["password"].ToString();
            }
            if (roamingSettings.Values.ContainsKey("domain"))
            {
                DomainComboBox.SelectedItem = roamingSettings.Values["domain"].ToString();
            }
        }

        // saving user credentials to application data settings
        private void SaveUserCredentials()
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            roamingSettings.Values["userID"] = UsernameTextBox.Text;
            roamingSettings.Values["password"] = PasswordBox.Password;
            roamingSettings.Values["domain"] = DomainComboBox.SelectedItem.ToString();
        }
    }
}