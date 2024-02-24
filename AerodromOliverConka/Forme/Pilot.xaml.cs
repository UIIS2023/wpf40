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
    /// Interaction logic for Pilot.xaml
    /// </summary>
    public partial class Pilot : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public Pilot()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }



        public Pilot(bool azuriraj, DataRowView red)
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
                cmd.Parameters.Add("@ImePilota", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@PrezimePilota", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@JMBGPilota", SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@AdresaPilota", SqlDbType.NVarChar).Value = txtAdresa.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblPilot SET ImePilota=@ImePilota ,PrezimePilota=@PrezimePilota, JMBGPilota=@JMBGPilota,
                            AdresaPilota=@AdresaPilota WHERE PilotID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblPilot(ImePilota,PrezimePilota,JMBGPilota,AdresaPilota)
                            VALUES(@ImePilota,@PrezimePilota,@JMBGPilota,@AdresaPilota)";
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
