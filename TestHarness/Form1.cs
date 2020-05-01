using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestHarness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //I am Getting Country information from Database
            ShopingBusinessRepo repo = new ShopingBusinessRepo();
            //comboBox2.Items.Add("Select");
            comboBox2.DataSource = repo.getCountrys().Tables[0];
            comboBox2.ValueMember = repo.getCountrys().Tables[0].Columns[0].ToString();// "CountryID";

            comboBox2.DisplayMember = repo.getCountrys().Tables[0].Columns[1].ToString();// "countryName";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer cus = new Customer();
            int id = 0;
            if ((int.TryParse((textBox1.Text), out id))) 
            {
                cus.Id = id;
            }
            cus.FirstName = textBox2.Text;
            cus.LastName = textBox3.Text;
            cus.City = textBox4.Text;
            cus.Country = textBox5.Text;
            cus.Phone = textBox6.Text;
            ShopingBusinessRepo repo = new ShopingBusinessRepo();
            var rs = repo.Create(cus);
            MessageBox.Show(rs + " Id Created...!!!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
            {
            ShopingBusinessRepo repo = new ShopingBusinessRepo();
           var details= repo.GetCustomer(Convert.ToInt32(textBox1.Text));
            textBox2.Text = details.FirstName;
            textBox3.Text = details.LastName;
            textBox4.Text = details.City;
            textBox5.Text = details.Country;
            textBox6.Text = details.Phone;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Repository Call
            ShopingBusinessRepo Repo = new ShopingBusinessRepo();
            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = Repo.getAllCustomer();
            dataGridView1.DataSource = bindingSource;
           

        }

        private void comboBox2_OnSelectionChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                //Do something with the selected item
                var selectedIdex = comboBox2.SelectedValue;
                //Repository Call
                ShopingBusinessRepo Repo = new ShopingBusinessRepo();

                comboBox1.DataSource = Repo.getStates(Convert.ToInt32(selectedIdex)).Tables[0];
                comboBox1.ValueMember = Repo.getStates(Convert.ToInt32(selectedIdex)).Tables[0].Columns[0].ToString();// "CountryID";

                comboBox1.DisplayMember = Repo.getStates(Convert.ToInt32(selectedIdex)).Tables[0].Columns[1].ToString();// "countryName";
            }

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}

