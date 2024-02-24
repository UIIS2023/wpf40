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
    /// Interaction logic for Avion.xaml
    /// </summary>
    public partial class Avion : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public Avion()
        {
            InitializeComponent();
            txtTip.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        public Avion(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtTip.Focus();
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

                string vratiModele = @"SELECT ModelID FROM tblModel";
                SqlDataAdapter daModel = new SqlDataAdapter(vratiModele, konekcija);
                DataTable dtModel = new DataTable();
                daModel.Fill(dtModel);
                cmbModelId.ItemsSource = dtModel.DefaultView;
                cmbModelId.DisplayMemberPath = "ModelID";
                cmbModelId.SelectedValuePath = "ModelID";

                daModel.Dispose();
                dtModel.Dispose();


                string vratiMarke = @"SELECT MarkaID FROM tblMarka";
                SqlDataAdapter daMarka = new SqlDataAdapter(vratiMarke, konekcija);
                DataTable dtMarka = new DataTable();
                daMarka.Fill(dtMarka);
                cmbMarkaId.ItemsSource = dtMarka.DefaultView;
                cmbMarkaId.DisplayMemberPath = "MarkaID";
                cmbMarkaId.SelectedValuePath = "MarkaID";
                daMarka.Dispose();
                dtMarka.Dispose();
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
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ukupnoKarata", SqlDbType.NVarChar).Value = txtUkupnoKarata.Text;
                cmd.Parameters.Add("@tipAviona", SqlDbType.NVarChar).Value = txtTip.Text;
                cmd.Parameters.Add("@modelID", SqlDbType.Int).Value = cmbModelId.SelectedValue;
                cmd.Parameters.Add("@markaID", SqlDbType.Int).Value = cmbMarkaId.SelectedValue;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblAvion SET UkupnoKarata=@ukupnoKarata, TipAviona=@tipAviona, 
                        ModelID=@modelID, MarkaID=@markaD WHERE AvionID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO tblAvion(UkupnoKarata, TipAviona, ModelID, MarkaD)
                            VALUES(@ukupnoKarata, @tipAviona, @modelID, @markaID)";
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

