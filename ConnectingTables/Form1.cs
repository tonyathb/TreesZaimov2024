using MySql.Data.MySqlClient;

namespace ConnectingTables
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conectionString = "server=192.168.0.243;" +
                "user=PC1;" +
                "password=1111;" +
                "port=3306;" +
                "database=trees_zaimov";

            MySqlConnection connect = new MySqlConnection(conectionString);
            if (connect.State == 0)
            {
                connect.Open();
            }
            MessageBox.Show("Imash vrazka s Belezireva HeidiSQL");

            //formirane na query
            string insertQueryText = "INSERT INTO trees_zaimov.razred " +
                "(`name`,`name_bg`) " +
                "VALUES " +
                "(@name,@name_bg)";

            MySqlCommand query = new MySqlCommand(insertQueryText, connect);

            //svarzvane na parameters
            query.Parameters.AddWithValue("@name", txtName.Text);
            query.Parameters.AddWithValue("@name_bg", txtNameBG.Text);

            //RUN query
            int result = query.ExecuteNonQuery();
            if (result != 0)
            {
                MessageBox.Show($"Dobawi {result} records!!!");
            }
            connect.Close();
        }
    }
}
