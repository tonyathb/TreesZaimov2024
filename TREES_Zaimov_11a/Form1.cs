using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TREES_Zaimov_11a
{
    public partial class Form1 : Form
    {
        
        string connstr = "server=10.6.0.127;" +
                "port=3306;" +
                "user=PC1;" +
                "password=1111;" +
                "database=trees_zaimov";



        public Form1()
        {
            InitializeComponent();
        }
        private void LoadComboBox(string stringQuery, System.Windows.Forms.ComboBox control)
        {
            //1. CONNECT to DB tree
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            MessageBox.Show("Connection now opened!");
            
            //2. query to DB
            MySqlCommand query =
                new MySqlCommand(stringQuery, connect);
            
            //3. get the data from DB
            MySqlDataReader readerCombo = query.ExecuteReader();

            //4. if have many data from SELECT query
            //READ a selected records
            List<ComboBoxItem> items = new List<ComboBoxItem>();
            
            while (readerCombo.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = readerCombo[1].ToString();
                item.Value = (int)readerCombo[0];

                items.Add(item);
            }

            control.DataSource = items;
            control.DisplayMember = "Text";
            control.ValueMember = "Value";

            readerCombo.Close();
            connect.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            MessageBox.Show("Connection NOW opened");
            connect.Close();
            //text query
            string str = "SELECT * FROM class";
            LoadComboBox(str, cmbClass);
            str = "SELECT * FROM family";
            LoadComboBox(str, cmbFam);
            str = "SELECT * FROM otdel";
            LoadComboBox(str, cmbOtdel);
            str = "SELECT * FROM razred";
            LoadComboBox(str, cmbRazred);
            str = "SELECT * FROM rod";
            LoadComboBox(str, cmbRod);
            str = "SELECT * FROM type";
            LoadComboBox(str, cmbVid);
            pictureBox1.Image = System.Drawing.Image.FromFile(pictureBox1.ImageLocation);
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(pictureBox1.ImageLocation);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO `trees`.`tree` " +
                "(`name`, `photo`, " +
                "`otdel_id`, `class_id`,`razred_id`, `family_id`, `rod_id`, `type_id`, " +
                "`discription`)" +
                " VALUES (@name, @img, @otdel, @class,@razred,@family,@rod,@vid,@info);";
            // " VALUES ('Gigant', 0x6A6A6A, '1', '2', '1', '1', '1', ',kmkkl');";

            //1. CONNECT to DB tree
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            //2. query to DB
            MySqlCommand query = new MySqlCommand(sql, connect);

            query.Parameters.AddWithValue("@name", txtName.Text);
            query.Parameters.AddWithValue("@otdel", cmbOtdel.SelectedValue);
            query.Parameters.AddWithValue("@class", cmbClass.SelectedValue);
            query.Parameters.AddWithValue("@razred", cmbRazred.SelectedValue);
            query.Parameters.AddWithValue("@family", cmbFam.SelectedValue);
            query.Parameters.AddWithValue("@rod", cmbRod.SelectedValue);
            query.Parameters.AddWithValue("@vid", cmbVid.SelectedValue);
            query.Parameters.AddWithValue("@info", txtDescription.Text);
            query.Parameters.AddWithValue("@img", txtImageURL.Text);
            // pictureBox1.ImageLocation);

            query.ExecuteNonQuery();
            MessageBox.Show("Input OK");

            connect.Close();
        }

        private void txtImageURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = 0;

            MessageBox.Show("Choise the Id to find record to UPDATE");
            txtId.Visible = true;
            lblID.Visible = true;
            if (txtId.Visible && !string.IsNullOrEmpty(txtId.Text))
            {
                id = int.Parse(txtId.Text);
            }
            else return;

            string sql = "UPDATE `trees`.`tree` " +
                         "SET `name` = @name, `photo`=@img, `discription` = @info " +
                         " WHERE id=@id";

            //1. CONNECT to DB tree
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            //2. query to DB
            MySqlCommand query = new MySqlCommand(sql, connect);

            //3. params
            query.Parameters.AddWithValue("@id", id);
            query.Parameters.AddWithValue("@name", txtName.Text);
            query.Parameters.AddWithValue("@info", txtDescription.Text);
            query.Parameters.AddWithValue("@img", txtImageURL.Text);

            //4. Execute !!!
            int result = query.ExecuteNonQuery();
            if (result == -1)
            {
                MessageBox.Show("Error");
            }
            else
            {
                MessageBox.Show("Update OK");
            }

            connect.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string insertSQL = "INSERT INTO minions.minions " +
            //    "(id,`name`,age,town_id) " +
            //    $"VALUES (200,{t},{},{})";
            //MySqlConnection connect = new MySqlConnection(connstr);
            //connect.Open();
            //MySqlCommand query = new MySqlCommand(insertSQL, connect);
            //query.ExecuteNonQuery();
            //connect.Close();   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
