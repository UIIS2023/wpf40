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
    /// Interaction logic for Let.xaml
    /// </summary>
    public partial class Let : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;


        public Let()
        {
            InitializeComponent();
            txtSlobodnaMesta.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public Let(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtSlobodnaMesta.Focus();
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

                string vratiAvion = @"SELECT AvionID  FROM tblAvion";
                SqlDataAdapter daAvion = new SqlDataAdapter(vratiAvion, konekcija);
                DataTable dtAvion = new DataTable();
                daAvion.Fill(dtAvion);
                cmbAvionID.ItemsSource = dtAvion.DefaultView;
                cmbAvionID.DisplayMemberPath = "AvionID"; 
                cmbAvionID.SelectedValuePath = "AvionID";
                daAvion.Dispose();
                dtAvion.Dispose();


                string vratiPilot = @"SELECT PilotID FROM tblPilot";
                SqlDataAdapter daPilot = new SqlDataAdapter(vratiPilot, konekcija);
                DataTable dtPilot = new DataTable();
                daPilot.Fill(dtPilot);
                cmbPilotID.ItemsSource = dtPilot.DefaultView;
                cmbPilotID.DisplayMemberPath = "PilotID";
                cmbPilotID.SelectedValuePath = "PilotID";
                daPilot.Dispose();
                dtPilot.Dispose();

                string vratiDestinacija = @"SELECT DestinacijaID FROM tblDestinacija";
                SqlDataAdapter daDestinacija = new SqlDataAdapter(vratiDestinacija, konekcija);
                DataTable dtDestinacija = new DataTable();
                daDestinacija.Fill(dtDestinacija);
                cmbDestinacijaID.ItemsSource = dtDestinacija.DefaultView;
                cmbDestinacijaID.DisplayMemberPath = "DestinacijaID";
                cmbDestinacijaID.SelectedValuePath = "DestinacijaID";

                daDestinacija.Dispose();
                dtDestinacija.Dispose();


                string vratiKompanije = @"SELECT AvioKompanijaID FROM tblAvioKompanija";
                SqlDataAdapter daKompanija = new SqlDataAdapter(vratiKompanije, konekcija);
                DataTable dtKompanija = new DataTable();
                daKompanija.Fill(dtKompanija);
                cmbAvioKompID.ItemsSource = dtKompanija.DefaultView;
                cmbAvioKompID.DisplayMemberPath = "AvioKompanijaID";
                cmbAvioKompID.SelectedValuePath = "AvioKompanijaID";
                daKompanija.Dispose();
                dtKompanija.Dispose();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@VremePolaska", SqlDbType.NVarChar).Value = txtVremePolaska.Text;
                cmd.Parameters.Add("@DatumPolaska", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("StatusLeta", SqlDbType.NVarChar).Value = txtStatusLeta.Text;
                cmd.Parameters.Add("@AvionID", SqlDbType.Int).Value = cmbAvionID.SelectedValue;
                cmd.Parameters.Add("@PilotID", SqlDbType.Int).Value = cmbPilotID.SelectedValue;
                cmd.Parameters.Add("@DestinacijaID", SqlDbType.Int).Value = cmbDestinacijaID.SelectedValue;
                cmd.Parameters.Add("@Izlaz", SqlDbType.NChar).Value = txtIzlaz.Text;
                cmd.Parameters.Add("@AvioKompanijaID", SqlDbType.Int).Value = cmbAvioKompID.SelectedValue;
                cmd.Parameters.Add("@SlobodnaMesta", SqlDbType.NChar).Value = txtSlobodnaMesta.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblLet SET VremePolaska=@VremePolaska, DatumPolaska=@DatumPolaska,
                            AvionID=@AvionID, PilotID=@PilotID, DestinacijaID=@DestinacijaID, Izlaz=@Izlaz, AvioKompanijaID=@AvioKompanijaID, SlobodnaMesta=@SlobodnaMesta
                                       WHERE LetID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblLet(VremePolaska,DatumPolaska,AvionID,PilotID,DestinacijaID,Izlaz, AvioKompanijaID, SlobodnaMesta)
                                        VALUES (@VremePolaska,@DatumPolaska,@AvionID,@PilotID,@DestinacijaID,@Izlaz,@AvioKompanijaID, @SlobodnaMesta)";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
           /* catch (SqlException)
            {
                MessageBox.Show("Unete vrednosti nisu validne!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
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

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }

