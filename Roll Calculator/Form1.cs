using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        private string addNewArtworkListRecord(string artNameValue, double artWidthValue, double artLengthValue, int artQuantityValue)
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

            artworkListRecord.Add(artwork);
            refreshArtlistBox();
            return artwork.Artwork_Id;
        }

        private void addNewDynamicArtworkListRecord(string artIdValue, string artNameValue, double artWidthValue, double artLengthValue, int artQuantityValue)
        {
            dynamic dynamicArtWork = new ExpandoObject();
            dynamicArtWork.Artwork_Id = artIdValue;
            dynamicArtWork.Artwork_Name = artNameValue;
            dynamicArtWork.Artwork_Width = artWidthValue;
            dynamicArtWork.Artwork_Length = artLengthValue;
            dynamicArtWork.Artwork_Quantity = artQuantityValue;

            dynamicArtworkListRecord.Add(dynamicArtWork);
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

                if (!hasInvalidChars(artNameValue))
                {
                    var newArtworkId = addNewArtworkListRecord(artNameValue, artWidthValue, artLengthValue, artQuantityValue);
                    addNewDynamicArtworkListRecord(newArtworkId, artNameValue, artWidthValue, artLengthValue, artQuantityValue);
                    resetArtworkInput();
                }
                else
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
            return !Regex.IsMatch(str, "^[a-zA-Z0-9 ]*$");
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
            if (artListBox.SelectedItems.Count > 0)
            {
                removeOldArtworkListRecord(artworkListRecord[artListBox.SelectedIndex]);
            }
            else
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

                List<dynamic> sortedArtworks = dynamicArtworkListRecord
                    .OrderByDescending(artwork => (int)artwork.Artwork_Quantity)
                    .ToList();

                int printCount = 1;

                while (sortedArtworks.Any(artwork => (int)artwork.Artwork_Quantity > 0))
                {
                    double remainingLength = materialLengthValue;

                    while (remainingLength > 0)
                    {
                        // Try to fit as many artworks as possible in the current batch
                        foreach (var currentArtwork in sortedArtworks.ToList())
                        {
                            if ((int)currentArtwork.Artwork_Quantity <= 0) continue;

                            int widthQuantity = (int)Math.Floor(materialWidthValue / (double)currentArtwork.Artwork_Width);
                            int maxRows = (int)Math.Floor(remainingLength / (double)currentArtwork.Artwork_Length);
                            int maxArtworks = maxRows * widthQuantity;

                            if (maxArtworks > 0)
                            {
                                int artworksToPrint = Math.Min((int)currentArtwork.Artwork_Quantity, maxArtworks);

                                SavePrintBatchDetails(currentArtwork, printCount, widthQuantity, maxRows, artworksToPrint);

                                currentArtwork.Artwork_Quantity -= artworksToPrint;
                                remainingLength -= maxRows * currentArtwork.Artwork_Length;

                                if (remainingLength <= 0) break;
                            }
                        }

                        break;
                    }

                    printCount++;
                }

                SaveOutputToCsv(sortedArtworks);

                MessageBox.Show($"File saved successfully!");

                // Open the folder containing the file
                OpenFolder();
                resetMaterialInput();
                resetArtworkInput();
                resetArtworkList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SavePrintBatchDetails(dynamic currentArtwork, int printCount, int widthQuantity, int rows, int artworksToPrint)
        {
            if (!((IDictionary<string, object>)currentArtwork).ContainsKey("Print_Batch"))
            {
                currentArtwork.Print_Batch = string.Empty;
            }

            string batchDetails = $"Batch {printCount}: {artworksToPrint} pieces of {currentArtwork.Artwork_Name}\n";
            currentArtwork.Print_Batch += batchDetails;
        }


        private void SaveOutputToCsv(List<dynamic> sortedArtworks)
        {
            string fileName = RandomString(8);
            string folderFilePath = @"C:\Users\johnc\OneDrive\Desktop";
            string currentDateTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string filePath = Path.Combine(folderFilePath, $"{fileName}_{currentDateTime}.csv");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                if (sortedArtworks.Count > 0)
                {
                    var firstRecord = sortedArtworks.First();
                    var properties = ((IDictionary<string, object>)firstRecord).Keys.ToList();

                    // Write header
                    csv.WriteField("Artwork ID");
                    csv.WriteField("Artwork Name");
                    csv.WriteField("Batch Details");
                    csv.NextRecord();

                    // Write records
                    foreach (var record in sortedArtworks)
                    {
                        csv.WriteField(((IDictionary<string, object>)record)["Artwork_Id"]);
                        csv.WriteField(((IDictionary<string, object>)record)["Artwork_Name"]);
                        csv.WriteField(((IDictionary<string, object>)record)["Print_Batch"]);
                        csv.NextRecord();
                    }
                }
            }
        }


        private void OpenFolder()
        {
            string folderFilePath = @"C:\Users\johnc\OneDrive\Desktop";
            Process.Start(new ProcessStartInfo
            {
                FileName = folderFilePath,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
