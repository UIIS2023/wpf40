using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AerodromOliverConka.Forme
{
    /// <summary>
    /// Interaction logic for AvioKompanija.xaml
    /// </summary>
    public partial class AvioKompanija : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public AvioKompanija()
        {
            InitializeComponent();
            txtNaziv.Focus();
            konekcija = kon.KreirajKonekciju();
            
        }

        public AvioKompanija(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtNaziv.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
            
        }

        

        private void btnSacuvaj_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@NazivAvioKompanije", SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@DrzavaAvioKompanije", SqlDbType.NVarChar).Value = txtDrzavaKompanije.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblAvioKompanija SET NazivAvioKompanije=@NazivAvioKompanije, DrzavaAvioKompanije=@DrzavaAvioKompanije 
                         WHERE AvioKompanijaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblAvioKompanija(NazivAvioKompanije, DrzavaAvioKompanije)
                            VALUES(@NazivAvioKompanije, @DrzavaAvioKompanije)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Niste uneli validne vrednosti!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
