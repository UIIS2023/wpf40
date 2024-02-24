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
    /// Interaction logic for Destinacija.xaml
    /// </summary>
    public partial class Destinacija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public Destinacija()
        {
            InitializeComponent();
            txtGrad.Focus();
            konekcija = kon.KreirajKonekciju();

        }

        public Destinacija(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtGrad.Focus();
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
                cmd.Parameters.Add("@Grad", SqlDbType.NVarChar).Value = txtGrad.Text;
                cmd.Parameters.Add("@Drzava", SqlDbType.NVarChar).Value = txtDrzava.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblDestinacija SET Grad=@Grad, Drzava=@Drzava 
                         WHERE DestinacijaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblDestinacija(Grad, Drzava)
                            VALUES(@Grad, @Drzava)";
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

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }

