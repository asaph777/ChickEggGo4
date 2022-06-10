using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChickEggGo4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Cook cook = new Cook();
        Server server = new Server();
        
        private void Button1_Click(object sender, EventArgs e)
        {
            string countChickensString = textBox1.Text;
            string countEggsString = textBox2.Text;

            int countChickens = 0;
            int countEggs = 0;
            string drinks = comboBox1.SelectedItem.ToString();
            string nameCistomer = textBox3.Text;

            if (countChickensString != "")
            {
                try
                {
                    countChickens = Int32.Parse(countChickensString);
                }
                catch (Exception)
                {

                    MessageBox.Show("Enter numbers chicken quantity, not string");
                }
            }
            if (countEggsString != "")
            {
                try
                {
                    countEggs = Int32.Parse(countEggsString);
                }
                catch (Exception)
                {

                    MessageBox.Show("Enter numbers egg quantity, not string");
                }
            }
      
            if (Server.counReqs >= 8)
            {
                button1.Enabled = false;
                MessageBox.Show("error");
                return;
            }

            server.Recieve(countChickens, countEggs, drinks, nameCistomer);
            button2.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            server.Send();
            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server.Ready += (TableRequests request)=> cook.Process(request);
            cook.Processed += Cook_Processed;

            button2.Enabled = false;

        }

        private void Cook_Processed()
        {
            List<string> results =  server.Serve();
            foreach (var item in results)
            {
                listBox1.Items.Add(item);
            }
            button1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
        }
    }
}
