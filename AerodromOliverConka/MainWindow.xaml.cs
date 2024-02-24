using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AerodromOliverConka.Forme;

namespace AerodromOliverConka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private string ucitanaTabela;
        private bool azuriraj;
        private DataRowView red;

        #region Upiti select

        private static string avionSelect = @"SELECT AvionID AS ID, UkupnoKarata AS 'Ukupno karata', TipAviona AS 'Tip aviona', MarkaID AS 'Id marke', ModelID AS 'Id modela' FROM tblAvion
                                             ";

        private static string avionskaKartaSelect = @"SELECT AvionskaKartaID AS ID, DatumKupovine AS 'Datum kupovine', CenaKarte AS 'Cena karte', CheckIn AS 'Check In', LetID AS 'Id leta', KlasaID AS 'Id klase', BrojSedista AS 'Broj sedista',
                                                  PutnikID as 'Id putnika' FROM tblAvionskaKarta";


        private static string letSelect = @"SELECT LetID as ID, VremePolaska AS 'Vreme polaska', StatusLeta AS 'Status leta', DatumPolaska AS 'Datum polaska', AvionID AS 'Id aviona', PilotID AS 'Id pilota',
                                            AvioKompanijaID AS 'Id avio kompanije', SlobodnaMesta AS 'Slobodna mesta', Izlaz AS 'Izlaz' FROM tblLet
                                             ";

        private static string pilotSelect = @"SELECT PilotID as ID, ImePilota AS 'Ime pilota', PrezimePilota AS 'Prezime pilota', JMBGPilota AS 'JMBG pilota', AdresaPilota AS 'Adresa pilota' FROM tblPilot";

        private static string putnikSelect = @"SELECT PutnikID as ID, ImePutnika AS 'Ime putnika', PrezimePutnika AS 'Prezime putnika', JMBGPutnika AS 'JMBG putnika', BrojPasosa AS 'Broj pasosa', AdresaPutnika AS 'Adresa putnika' FROM tblPutnik";


        private static string destinacijaSelect = @"SELECT DestinacijaID as ID, Grad as 'Grad', Drzava as 'Drzava' from tblDestinacija";

        private static string avioKompanijaSelect = @"SELECT AvioKompanijaID as ID, NazivAvioKompanije AS 'Naziv kompanije', DrzavaAvioKompanije as 'Drzava kompanije' FROM tblAvioKompanija";

        private static string markaSelect = @"SELECT MarkaID as ID, ImeMarke AS 'Ime marke' from tblMarka";

        private static string modelSelect = @"SELECT ModelID as ID, ImeModela AS 'Ime modela' from tblModel";


        #endregion


        #region Select sa uslovom

        private static string selectUslovAvion = @"SELECT * FROM tblAvion where AvionID=";

        private static string selectUslovAvionska = @"SELECT * FROM tblAvionskaKarta where AvionskaKartaID=";

        private static string selectUslovLet = @"SELECT * FROM tblLet where LetID=";

        private static string selectUslovPilot = @"SELECT * FROM tblPilot where PilotID=";

        private static string selectUslovPutnik = @"SELECT * FROM tblPutnik where PutnikID=";

        private static string selectUslovDestinacija = @"SELECT * FROM tblDestinacija where DestinacijaID=";

        private static string selectUslovAvioKomp = @"SELECT * FROM tblAvioKompanija where AvioKompanijaID=";

        private static string selectUslovMarka = @"SELECT * FROM tblMarka where MarkaID=";

        private static string selectUslovModel = @"SELECT * FROM tblModel where ModelID=";


        #endregion

        #region Delete upiti

        private static string avionDelete = @"DELETE FROM tblAvion where AvionID=";

        private static string avionskaKartaDelete = @"DELETE FROM tblAvionskaKarta where AvionskaKartaID=";

        private static string letDelete = @"DELETE FROM tblLet where LetID=";

        private static string pilotDelete = @"DELETE FROM tblPilot where PilotID=";

        private static string putnikDelete = @"DELETE FROM tblPtnik where PutnikID=";

        private static string destinacijaDelete = @"DELETE FROM tblDestinacija where DestinacijaID=";

        private static string avioKompDelete = @"DELETE FROM tblAvioKompanija where AvioKompanijaID=";

        private static string markaDelete = @"DELETE FROM tblMarka where MarkaID=";

        private static string modelDelete = @"DELETE FROM tblModel where ModelID=";

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(avionSelect);
        }
        private void UcitajPodatke(string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;
                }

                ucitanaTabela = selectUpit;
                dataAdapter.Dispose();
                dataTable.Dispose();

            }
           /* catch (SqlException)
            {
                MessageBox.Show("Greska prilikom ucitavanja tabele!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(avionSelect))
            {
                prozor = new Avion();
                prozor.ShowDialog();
                UcitajPodatke(avionSelect);
            }
            else if (ucitanaTabela.Equals(avionskaKartaSelect))
            {
                prozor = new AvionskaKarta();
                prozor.ShowDialog();
                UcitajPodatke(avionskaKartaSelect);
            }
            else if (ucitanaTabela.Equals(letSelect))
            {
                prozor = new Let();
                prozor.ShowDialog();
                UcitajPodatke(letSelect);
            }
            else if (ucitanaTabela.Equals(pilotSelect))
            {
                prozor = new Pilot();
                prozor.ShowDialog();
                UcitajPodatke(pilotSelect);
            }
            else if (ucitanaTabela.Equals(putnikSelect))
            {
                prozor = new Putnik();
                prozor.ShowDialog();
                UcitajPodatke(putnikSelect);
            }
            else if (ucitanaTabela.Equals(destinacijaSelect))
            {
                prozor = new Destinacija();
                prozor.ShowDialog();
                UcitajPodatke(destinacijaSelect);
            }
            else if (ucitanaTabela.Equals(avioKompanijaSelect))
            {
                prozor = new AvioKompanija();
                prozor.ShowDialog();
                UcitajPodatke(avioKompanijaSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                prozor = new Marka();
                prozor.ShowDialog();
                UcitajPodatke(markaSelect);
            }
            else if (ucitanaTabela.Equals(modelSelect))
            {
                prozor = new Model();
                prozor.ShowDialog();
                UcitajPodatke(modelSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(avionSelect))
            {
                PopuniFormu(selectUslovAvion);
                UcitajPodatke(avionSelect);

            }
            else if (ucitanaTabela.Equals(avionskaKartaSelect))
            {
                PopuniFormu(selectUslovAvionska);
                UcitajPodatke(avionskaKartaSelect);
            }
            else if (ucitanaTabela.Equals(letSelect))
            {
                PopuniFormu(selectUslovLet);
                UcitajPodatke(letSelect);
            }
            else if (ucitanaTabela.Equals(pilotSelect))
            {
                PopuniFormu(selectUslovPilot);
                UcitajPodatke(pilotSelect);
            }
            else if (ucitanaTabela.Equals(putnikSelect))
            {
                PopuniFormu(selectUslovPutnik);
                UcitajPodatke(putnikSelect);
            }
            else if (ucitanaTabela.Equals(destinacijaSelect))
            {
                PopuniFormu(selectUslovDestinacija);
                UcitajPodatke(destinacijaSelect);
            }
            else if (ucitanaTabela.Equals(avioKompanijaSelect))
            {
                PopuniFormu(selectUslovAvioKomp);
                UcitajPodatke(avioKompanijaSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                PopuniFormu(selectUslovMarka);
                UcitajPodatke(markaSelect);
            }
            else if (ucitanaTabela.Equals(modelSelect))
            {
                PopuniFormu(selectUslovModel);
                UcitajPodatke(modelSelect);
            }
        }

        private void PopuniFormu(object selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(avionSelect))
                    {
                        Avion prozorAvion = new Avion(azuriraj, red);
                        prozorAvion.txtUkupnoKarata.Text = citac["UkupnoKarata"].ToString();
                        prozorAvion.txtTip.Text = citac["TipAviona"].ToString();
                        prozorAvion.cmbModelId.SelectedValue = citac["ModelID"].ToString();
                        prozorAvion.cmbMarkaId.SelectedValue = citac["MarkaID"].ToString();
                        prozorAvion.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(avionskaKartaSelect))
                    {
                        AvionskaKarta prozorAvionskaKarta = new AvionskaKarta(azuriraj, red);
                        prozorAvionskaKarta.txtCena.Text = citac["CenaKarte"].ToString();
                        prozorAvionskaKarta.dtpDatum.SelectedDate = (DateTime)citac["DatumKupovine"];
                        prozorAvionskaKarta.chkCheck.IsChecked = (bool)citac["CheckIn"];
                        prozorAvionskaKarta.cmbLetId.SelectedValue = citac["LetID"].ToString();
                        prozorAvionskaKarta.cmbKlasaId.SelectedValue = citac["KlasaID"].ToString();
                        prozorAvionskaKarta.cmbPutnikID.SelectedValue = citac["PutnikID"].ToString();
                        prozorAvionskaKarta.txtBrojSedista.Text = citac["BrojSedista"].ToString();

                        prozorAvionskaKarta.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(letSelect))
                    {
                        Let prozorLet = new Let(azuriraj, red);
                        prozorLet.txtVremePolaska.Text = citac["VremePolaska"].ToString();
                        prozorLet.txtStatusLeta.Text = citac["StatusLeta"].ToString();
                        prozorLet.txtIzlaz.Text = citac["Izlaz"].ToString();
                        prozorLet.dpDatum.SelectedDate = (DateTime)citac["DatumPolaska"];
                        prozorLet.cmbAvionID.SelectedValue = citac["AvionID"].ToString();
                        prozorLet.cmbPilotID.SelectedValue = citac["PilotID"].ToString();
                        prozorLet.cmbDestinacijaID.SelectedValue = citac["DestinacijaID"].ToString();
                        prozorLet.txtSlobodnaMesta.Text = citac["SlobodnaMesta"].ToString();
                        prozorLet.cmbAvioKompID.SelectedValue = citac["AvioKompanijaID"].ToString();
                        prozorLet.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(pilotSelect))
                    {
                        Pilot prozorPilot = new Pilot(azuriraj, red);
                        prozorPilot.txtIme.Text = citac["ImePilota"].ToString();
                        prozorPilot.txtPrezime.Text = citac["PrezimePilota"].ToString();
                        prozorPilot.txtJMBG.Text = citac["JMBGPilota"].ToString();
                        prozorPilot.txtJMBG.Text = citac["AdresaPilota"].ToString();
                        prozorPilot.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(putnikSelect))
                    {
                        Putnik prozorPutnik = new Putnik(azuriraj, red);
                        prozorPutnik.txtIme.Text = citac["ImePutnika"].ToString();
                        prozorPutnik.txtPrezime.Text = citac["PrezimePutnika"].ToString();
                        prozorPutnik.txtJMBG.Text = citac["JMBGPutnika"].ToString();
                        prozorPutnik.txtBrojPasosa.Text = citac["BrojPasosa"].ToString();
                        prozorPutnik.txtAdresa.Text = citac["AdresPutnika"].ToString();
                        prozorPutnik.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(destinacijaSelect))
                    {
                        Destinacija prozorDestinacija = new Destinacija(azuriraj, red);
                        prozorDestinacija.txtGrad.Text = citac["Grad"].ToString();
                        prozorDestinacija.txtDrzava.Text = citac["Drzava"].ToString();

                        prozorDestinacija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(avioKompanijaSelect))
                    {
                        AvioKompanija prozorAvioKompanija = new AvioKompanija(azuriraj, red);
                        prozorAvioKompanija.txtNaziv.Text = citac["NazivAvioKompanije"].ToString();
                        prozorAvioKompanija.txtDrzavaKompanije.Text = citac["DrzavaAvioKompanije"].ToString();

                        prozorAvioKompanija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(markaSelect))
                    {
                        Marka prozorMarka = new Marka(azuriraj, red);
                        prozorMarka.txtImeMarke.Text = citac["ImeMarke"].ToString();
                       
                        prozorMarka.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(modelSelect))
                    {
                        Model prozorModel = new Model(azuriraj, red);
                        prozorModel.txtImeModela.Text = citac["ImeModela"].ToString();
                        
                        prozorModel.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Red nije selektovan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(avionSelect))
            {
                ObrisiZapis(avionDelete);
                UcitajPodatke(avionSelect);

            }
            else if (ucitanaTabela.Equals(avionskaKartaSelect))
            {
                ObrisiZapis(avionskaKartaDelete);
                UcitajPodatke(avionskaKartaSelect);
            }
            else if (ucitanaTabela.Equals(letSelect))
            {
                ObrisiZapis(letDelete);
                UcitajPodatke(letSelect);
            }
            else if (ucitanaTabela.Equals(pilotSelect))
            {
                ObrisiZapis(pilotDelete);
                UcitajPodatke(pilotSelect);
            }
            else if (ucitanaTabela.Equals(putnikSelect))
            {
                ObrisiZapis(putnikDelete);
                UcitajPodatke(putnikSelect);
            }
            else if (ucitanaTabela.Equals(destinacijaSelect))
            {
                ObrisiZapis(destinacijaDelete);
                UcitajPodatke(destinacijaSelect);
            }
            else if (ucitanaTabela.Equals(avioKompanijaSelect))
            {
                ObrisiZapis(avioKompDelete);
                UcitajPodatke(avioKompanijaSelect);
            }
            else if (ucitanaTabela.Equals(markaSelect))
            {
                ObrisiZapis(markaDelete);
                UcitajPodatke(markaSelect);
            }
            else if (ucitanaTabela.Equals(modelSelect))
            {
                ObrisiZapis(modelDelete);
                UcitajPodatke(modelSelect);
            }
        }
        private void ObrisiZapis(string deleteUslov)
        {
            try
            {
                konekcija.Open();
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Jeste li sigurni?", "Upozorenje!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUslov + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Red nije selektovan!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje podaci koji su povezani u drugim tabelama!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }


        private void btnAvion_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(avionSelect);
        }

        private void btnAvionskaKarta_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(avionskaKartaSelect);
        }
        private void btnLet_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(letSelect);
        }

        private void btnPilot_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(pilotSelect);
        }

        private void btnPutnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(putnikSelect);
        }

        private void btnDestinacija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(destinacijaSelect);
        }

        private void btnAvioKompanija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(avioKompanijaSelect);
        }

        private void btnMarka_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(markaSelect);
        }

        private void btnModel_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(modelSelect);
        }




    }
}