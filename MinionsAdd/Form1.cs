

using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace MinionsAdd
{
    public partial class Form1 : Form
    {
        string connstr = "server=10.42.41.221;" +//10.42.42.64;" +
            "port=3306;" +
            "user=PC1;" +
            "password=1111;" +
            "database=minions";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //List<ComboItems> comboitems = new List<ComboItems>();


            //2.
            MySqlConnection connect = new MySqlConnection(connstr);
            if(connect.State==0)   connect.Open();
            MessageBox.Show("Connection is now opened!");
            //4.
            MySqlCommand query = new MySqlCommand(
                "select * from towns", connect);
            //5.
            MySqlDataReader reader =  query.ExecuteReader();

            //6.
            List<ComboItem> items = new List<ComboItem>();
            while (reader.Read())
            {
                ComboItem item = new ComboItem();
               
                item.Id = (int)reader[0];
                item.Name = (string)reader[1];

                items.Add(item);
            }
            reader.Close();

            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";

            connect.Close();        

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"150. Ti vavvede {textBox1.Text}" +
            //    $" s godini {textBox2.Text} s grad " +
            //    $"{comboBox1.Text} -- > {comboBox1.SelectedValue}");


            string insertSQL = "INSERT INTO minions.minions " +
                "(id,`name`,age,town_id) " +
                "VALUES (140,@name,@age,@town_id)";
            //zapochva Insert query
            //podgotovka - connect to DB
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();
            //podg. na Insert -> 1.
            MySqlCommand query = new MySqlCommand(insertSQL,connect);

            ////podg. na Insert -> 2. PARAMETRI ot ekrana
            query.Parameters.AddWithValue("@name", textBox1.Text);
            query.Parameters.AddWithValue("@age", textBox2.Text);
            query.Parameters.AddWithValue("@town_id", comboBox1.SelectedValue);
            
            //izpalnenie na Insert query
            query.ExecuteNonQuery(); //въвеждането става точно ТУК!!!!
            MessageBox.Show("Add ok!");
            connect.Close();
        }
    }
}
