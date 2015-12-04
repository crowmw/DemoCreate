using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Repository.Models;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using Microsoft.Owin.Security.Facebook;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Threading.Tasks;

namespace DemoCreate
{
    public partial class Startup
    {
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(DCContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "augFWw8vgnkzE8YBAurFbsK4t",
            //   consumerSecret: "KtAjfvgGU9Yg35QS7lsh6JBVuvRmgXJ2tHvpuDFwb5hB3aHWNI");

            //var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = "1698660540370782",
            //    AppSecret = "537b8280853f371d3d24372aea76d28d"
            //};
            //facebookAuthenticationOptions.Scope.Add("email");
            //app.UseFacebookAuthentication(facebookAuthenticationOptions);

            // Facebook : Create New App
            // https://developers.facebook.com/apps

            //string pub_profil = string.Empty;

            //    var facebookOptions = new FacebookAuthenticationOptions
            //    {
            //        AppId = "1698660540370782",
            //        AppSecret = "537b8280853f371d3d24372aea76d28d",
            //        Provider = new FacebookAuthenticationProvider
            //        {
            //            OnAuthenticated = (context) =>
            //            {
            //                //context.Identity.AddClaim(new Claim("urn:facebook:gender", )
            //                context.Identity.AddClaim(new Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
            //                context.Identity.AddClaim(new Claim("urn:facebook:public_profile", pub_profil, XmlSchemaString, "Facebook"));
            //                foreach (var x in context.User)
            //                {
            //                    var claimType = string.Format("urn:facebook:{0}", x.Key);
            //                    string claimValue = x.Value.ToString();
            //                    if (!context.Identity.HasClaim(claimType, claimValue))
            //                        context.Identity.AddClaim(new Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

            //                }
            //                return Task.FromResult(0);
            //            }
            //        }
            //    };
            //    facebookOptions.Scope.Add("email");
            //    facebookOptions.Scope.Add("public_profile");
            //    app.UseFacebookAuthentication(facebookOptions);

            app.UseFacebookAuthentication(
                appId: "1698660540370782",
                appSecret: "537b8280853f371d3d24372aea76d28d");
            //var facebookOptions = new FacebookAuthenticationOptions();
            //facebookOptions.AppId = "1698660540370782";
            //facebookOptions.AppSecret = "537b8280853f371d3d24372aea76d28d";
            //facebookOptions.Scope.Add("public_profile");
            //facebookOptions.SignInAsAuthenticationType = Microsoft.Owin.Security.AppBuilderSecurityExtensions.GetDefaultSignInAsAuthenticationType(app);

            //facebookOptions.Provider = new FacebookAuthenticationProvider()
            //{
            //    OnAuthenticated = async facebookContext =>
            //    {
            //        // Save every additional claim we can find in the user
            //        foreach (JProperty property in facebookContext.User.Children())
            //        {
            //            var claimType = string.Format("urn:facebook:{0}", property.Name);
            //            string claimValue = (string)property.Value;
            //            if (!facebookContext.Identity.HasClaim(claimType, claimValue))
            //                facebookContext.Identity.AddClaim(new Claim(claimType, claimValue,
            //                      "http://www.w3.org/2001/XMLSchema#string", "External"));
            //        }
            //    }
            //};

            //app.UseFacebookAuthentication(facebookOptions);


            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "492773902505-b1egabb9jogkpbpk2ejrdbfi204ei4f8.apps.googleusercontent.com",
            //    ClientSecret = "kYCszRkYrk5Ir1M590sS-ZLL"
            //});
        }
    }
}