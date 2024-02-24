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
    /// Interaction logic for Putnik.xaml
    /// </summary>
    public partial class Putnik : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public Putnik()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        
        
        public Putnik(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtIme.Focus();
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
                cmd.Parameters.Add("@ImePutnika", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@PrezimePutnika", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@JMBGPutnika", SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@AdresaPutnika", SqlDbType.NVarChar).Value = txtAdresa.Text;
                cmd.Parameters.Add("@BrojPasosa", SqlDbType.NVarChar).Value = txtBrojPasosa.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblPutnik SET ImePutnika=@ImePutnika,PrezimePutnika=@PrezimePutnika,JMBGPutnika=@JMBGPutnika,
                            AdresaPutnika=@AdresaPutnika, BrojPasosa=@BrojPasosa WHERE PutnikID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblPutnik(ImePutnika,PrezimePutnika,JMBGPutnika,AdresaPutnika, BrojPasosa)
                            VALUES(@ImePutnika,@PrezimePutnika,@JMBGPutnika,@AdresaPutnika, @BrojPasosa)";
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

        private void btnOtkazi_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }


