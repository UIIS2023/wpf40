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
    /// Interaction logic for AvionskaKarta.xaml
    /// </summary>
    public partial class AvionskaKarta : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;


        public AvionskaKarta()
        {
            InitializeComponent();
            dtpDatum.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public AvionskaKarta(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            dtpDatum.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
            PopuniPadajuceListe();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                string vratiLet = @"SELECT LetID  FROM tblLet";
                SqlDataAdapter daLet = new SqlDataAdapter(vratiLet, konekcija);
                DataTable dtLet = new DataTable();
                daLet.Fill(dtLet);
                cmbLetId.ItemsSource = dtLet.DefaultView;
                cmbLetId.DisplayMemberPath = "LetID"; // Postavite odgovarajuće ime polja koje želite prikazati
                cmbLetId.SelectedValuePath = "LetID";
                daLet.Dispose();
                dtLet.Dispose();


                string vratiKlase = @"SELECT KlasaID FROM tblKlasaKarte";
                SqlDataAdapter daKlasa = new SqlDataAdapter(vratiKlase, konekcija);
                DataTable dtKlase = new DataTable();
                daKlasa.Fill(dtKlase);
                cmbKlasaId.ItemsSource = dtKlase.DefaultView;
                cmbKlasaId.DisplayMemberPath = "KlasaID";
                cmbKlasaId.SelectedValuePath = "KlasaID";
                daKlasa.Dispose();
                dtKlase.Dispose();

                string vratiPutnika = @"SELECT PutnikID FROM tblPutnik";
                SqlDataAdapter daPutnik = new SqlDataAdapter(vratiPutnika, konekcija);
                DataTable dtPutnik = new DataTable();
                daPutnik.Fill(dtPutnik);
                cmbPutnikID.ItemsSource = dtPutnik.DefaultView;
                cmbPutnikID.DisplayMemberPath = "PutnikID";
                cmbPutnikID.SelectedValuePath = "PutnikID";
                daPutnik.Dispose();
                dtPutnik.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Niste popunili padajuce liste!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnSacuvaj_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dtpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@cenaKarte", SqlDbType.NVarChar).Value = txtCena.Text;
                cmd.Parameters.Add("@datumKupovine", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@checkIn", SqlDbType.Bit).Value = Convert.ToInt32(chkCheck.IsChecked);
                cmd.Parameters.Add("@LetID", SqlDbType.Int).Value = cmbLetId.SelectedValue;
                cmd.Parameters.Add("@KlasaID", SqlDbType.Int).Value = cmbKlasaId.SelectedValue;
                cmd.Parameters.Add("@PutnikID", SqlDbType.Int).Value = cmbPutnikID.SelectedValue;
                cmd.Parameters.Add("@BrojSedista", SqlDbType.NChar).Value = txtBrojSedista.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblAvionskaKarta SET DatumKupovine=@datumKupovine, CenaKarte=@cenaKarte,
                            CheckIn=@checkIn, LetID=@LetID, KlasaID=@KlasaID, PutnikID=@PutnikID, BrojSedista=@BrojSedista
                                       WHERE AvionskaKartaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblAvionskaKarta(DatumKupovine,CenaKarte,CheckIn,LetID,KlasaID,PutnikID, BrojSedista)
                                        VALUES (@datumKupovine,@cenaKarte,@checkIn,@LetID,@KlasaID,@PutnikID,@BrojSedista)";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unete vrednosti nisu validne!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Unesite datum!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Greska prilikom konverzije podataka!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
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
