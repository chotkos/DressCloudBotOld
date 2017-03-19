using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Awesomium.Core;

namespace Awesomium_in_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webControl1.Source = new Uri("http://dresscloud.pl/index.php");

            isActive.Text = "Aplikacja załadowana :)";
            webControl1.Visible = true;
        }



        private void webControl1_LoadingFrameComplete(object sender, Awesomium.Core.FrameEventArgs e)
        {
            if (webControl1.IsLoading || !webControl1.IsLive)
                return;

            // really all DOM loaded
            MessageBox.Show(webControl1.Title);
            webControl1.LoadingFrameComplete -= new Awesomium.Core.FrameEventHandler(webControl1_LoadingFrameComplete);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            isActive.Text = "Logowanie użytkownika...";

            Data.Login = loginText.Text;
            Data.Password = passwordBox.Text;
            webControl1.ExecuteJavascript("$('.fancybox-close').click();");
            System.Threading.Thread.Sleep(1000);
            webControl1.ExecuteJavascript("$('#logincloud>img').click();");
            //webControl1.ExecuteJavascript("$('#id_username').val('{" + Data.Login + "}'); ");
            //webControl1.ExecuteJavascript("$('#id_password').val('{" + Data.Password + "}'); ");
            //webControl1.ExecuteJavascript("$('#loginbutton').click();");

            try
            {
                dynamic document = (JSObject)webControl1.ExecuteJavascriptWithResult("document");

                if (document == null)
                    return;

                using (document)
                {
                    // This is an invocation. If the object does not contain
                    // a method named 'getElementById', you would get a 
                    // RuntimeBinderException. Of course this cannot be the
                    // case with the 'document' object.
                    dynamic username = document.getElementById("id_username");
                    dynamic pass = document.getElementById("id_password");
                    dynamic button = document.getElementById("loginbutton");

                    if (username == null || pass == null || button == null)
                        return;

                    using (username)
                        username.value = Data.Login;
                    using (pass)
                        pass.value = Data.Password;
                    using (button)
                        button.click();
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return;
            }
            catch
            {
                throw;
            }

            refreshTimer.Enabled = true;
            webControl1.Refresh();
            refreshTimer.Interval = 60*3000;
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Awesomium.Core.WebCore.Shutdown();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Liking();
        }

        private void Liking()
        {
            isActive.Text = "Aplikacja lajkuje :)";

            //checkLikes();
            System.Threading.Thread.Sleep(1000);
            webControl1.ExecuteJavascript("$('.do_like').click();");
            System.Threading.Thread.Sleep(1000);
            webControl1.Source = new Uri("http://dresscloud.pl/index.php");
        }

        private void checkLikes()
        {
            try
            {
                dynamic document = (JSObject)webControl1.ExecuteJavascriptWithResult("document");

                if (document == null)
                    return;

                using (document)
                {
                    dynamic likes = document.getElementById("klasalajków");
                    //to trzeba dokończyć

                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return;
            }
            catch
            {
                throw;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}


