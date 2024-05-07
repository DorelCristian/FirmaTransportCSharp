using System;
using System.Windows.Forms;
using NormandiaService.ro.mpp;
using NormandiaModel.ro.mpp;


namespace NormandiaClient.ro.mpp
{
    public partial class LogIn : Form
    {
        private IRezervareServices _service;
        public LogIn(IRezervareServices service)
        {
            this._service= service;
            InitializeComponent();
        }

        

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            var hello= new Hello(_service,this);
            try
            {
                Client user = _service.Connect(username, password,hello);
                Console.WriteLine("IN BUTON User:"+user.Id+" "+user.username+" "+user.password);
               //. Client user2=
                if (user != null)
                {
                    MessageBox.Show("Log in successful!");
                    this.Hide();
                    hello.SetUser(user.username,user.password);
                   // var hello = new Hello(_service, this);
                    Console.WriteLine("here");
                    Console.WriteLine(user.username);
                    Console.WriteLine(user.password);
                    Console.WriteLine(user.Id);
                    hello.Show();
                }
                else
                {
                    MessageBox.Show("Log in failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //throw new System.NotImplementedException();
        }
    }
}