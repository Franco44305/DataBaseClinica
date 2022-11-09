using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Conexion(string consulta)
        {
            var conString = ConfigurationManager.ConnectionStrings["dbSql"].ConnectionString;
            try
            {
                using (SqlConnection Conector = new SqlConnection(conString))
                {
                    Conector.Open();
                    DataTable dt = new DataTable();
                    string query = consulta;
                    SqlCommand comando = new SqlCommand(query, Conector);
                    SqlDataAdapter da = new SqlDataAdapter(comando);
                    da.SelectCommand = comando;
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EjecutarStoredProcedure()
        {
            var conString = ConfigurationManager.ConnectionStrings["dbSql"].ConnectionString;
            try
            {
                using (SqlConnection conector = new SqlConnection(conString))
                {
                    conector.Open();
                    string query = @"AddPaciente";
                    SqlCommand comando = new SqlCommand(query, conector);
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre.Text;
                    comando.Parameters.Add("@Tipo_Nro_Documento", SqlDbType.VarChar).Value = Tipo.Text;
                    comando.Parameters.Add("@Nro_Documento", SqlDbType.Int).Value = int.Parse(Numero.Text);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Carga correcta");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion("Select * from Pacientes");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Conexion("Select * from Vista_Saldo_Pacientes");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion("Select * from Pacientes");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EjecutarStoredProcedure();
        }
    }
}
