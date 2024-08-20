using CsvHelper;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Roll_Calculator
{
    public partial class Form1 : Form
    {
        private List<ArtWork> artworkListRecord = new List<ArtWork>();
        private List<dynamic> dynamicArtworkListRecord = new List<dynamic>();
        public Form1()
        {
            InitializeComponent();
            artListBox.DataSource = artworkListRecord;
        }

        private void addNewArtworkListRecord(string artNameValue, double artWidthValue, double artLengthValue, int artQuantityValue)
        {
            string randomArtworkId = RandomString(8);
            var artwork = new ArtWork
            {
                Artwork_Id = randomArtworkId,
                Artwork_Quantity = artQuantityValue,
                Artwork_Name = artNameValue,
                Artwork_Width = artWidthValue,
                Artwork_Length = artLengthValue,
            };

            dynamic dynamicArtWork = new ExpandoObject();
            dynamicArtWork.Artwork_Id = randomArtworkId;
            dynamicArtWork.Artwork_Name = artNameValue;
            dynamicArtWork.Artwork_Width = artWidthValue;
            dynamicArtWork.Artwork_Length = artLengthValue;
            dynamicArtWork.Artwork_Quantity = artQuantityValue;

            dynamicArtWork.Print_1 = 1;

            artworkListRecord.Add(artwork);
            dynamicArtworkListRecord.Add(dynamicArtWork);

            MessageBox.Show(dynamicArtworkListRecord.ToString());

            refreshArtlistBox();
        }

        private void removeOldArtworkListRecord(ArtWork _selectedIndex)

        {
            artworkListRecord.Remove(_selectedIndex);

            string artworkId = _selectedIndex.Artwork_Id;
            var artworkToRemove = dynamicArtworkListRecord.FirstOrDefault(item =>
            {
                var dict = (IDictionary<string, object>)item;
                return dict.TryGetValue("Artwork_Id", out var value) && (string)value == artworkId;
            });

            if (artworkToRemove != null)
            {
                dynamicArtworkListRecord.Remove(artworkToRemove);
            }

            refreshArtlistBox();
        }

        private void addMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                string artNameValue = artName.Text;
                double artWidthValue = double.Parse(artWidth.Text);
                double artLengthValue = double.Parse(artLength.Text);
                int artQuantityValue = int.Parse(artQuantity.Text);

                bool hasSpecialChars = hasInvalidChars(artNameValue);

                if (!hasSpecialChars) {
                    addNewArtworkListRecord(artNameValue, artWidthValue, artLengthValue, artQuantityValue);
                    resetArtworkInput();
                } else
                {
                    MessageBox.Show("Special characters are not allowed!");
                    resetArtworkInput();
                }

                
            }
            catch (Exception ex)
            {
                resetArtworkInput();
                MessageBox.Show(ex.Message);
            }
        }

        private bool hasInvalidChars(string str)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(str)) { return false; }

            return true;
        }

        private void resetArtworkInput()
        {
            artName.Text = string.Empty;
            artWidth.Text = string.Empty;
            artLength.Text = string.Empty;
            artQuantity.Text = string.Empty;
        }

        private void resetMaterialInput()
        {
            materialLength.Text = string.Empty;
            materialWidth.Text = string.Empty;
        }

        private void resetArtworkList()
        {
            artworkListRecord.Clear();
            dynamicArtworkListRecord.Clear();
            artListBox.DataSource = null;
            artListBox.DataSource = artworkListRecord;
        }

        private void clearData_Click(object sender, EventArgs e)
        {
            resetMaterialInput();
            resetArtworkInput();
            resetArtworkList();
        }

        private void refreshArtlistBox()
        {
            artListBox.DataSource = null;
            artListBox.DataSource = artworkListRecord;
        }

        private void deleteMaterial_Click(object sender, EventArgs e)
        {
            if (artListBox.SelectedItems.Count > 0) {
                removeOldArtworkListRecord(artworkListRecord[artListBox.SelectedIndex]);
            } else
            {
                MessageBox.Show("Please select artwork");
            }
            

        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void generateData_Click(object sender, EventArgs e)
        {
            try
            {
                double materialWidthValue = double.Parse(materialWidth.Text);
                double materialLengthValue = double.Parse(materialLength.Text);
                string fileName = RandomString(8);
                string folderFilePath = $"C:\\Users\\johnc\\OneDrive\\Desktop";
                string currentDateTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                string filePath = $"{folderFilePath}\\{fileName}_{currentDateTime}.csv";

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(dynamicArtworkListRecord);
                    MessageBox.Show($"File saved at ({filePath})");
                    Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = folderFilePath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }

                // Reset all inputs
                resetMaterialInput();
                resetArtworkInput();
                resetArtworkList();

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
