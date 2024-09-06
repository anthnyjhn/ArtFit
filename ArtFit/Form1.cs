using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Roll_Calculator
{
    public partial class Form1 : Form
    {
        private List<ArtWork> artworkListRecord = new List<ArtWork>();
        private List<ArtWork> dynamicArtworkListRecord = new List<ArtWork>();
        public Form1()
        {


            MessageBox.Show(
    $"ArtFit is a specialized software designed to optimize artwork placement and estimate material consumption for scroll jobs.\n \n Innovating One Mile at a Time. \n \n ArtFit © 2024p ",
    "About ArtFit",
    MessageBoxButtons.OK,
    MessageBoxIcon.Information
);

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
            ArtWork dynamicArtWork = new ArtWork
            {
                Artwork_Id = artIdValue,
                Artwork_Quantity = artQuantityValue,
                Artwork_Name = artNameValue,
                Artwork_Width = artWidthValue,
                Artwork_Length = artLengthValue,
            };

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

                List<ArtWork> sortedArtworks = dynamicArtworkListRecord
                    .OrderByDescending(artwork => (double)artwork.Artwork_Length)
                    .ToList();

                List<PrintBatch> batchArtworkList = new List<PrintBatch>();

                int batchCount = 1;

                ArtWork findAdditionalArtwork (double excessMaterialLength)
                {

                    foreach (ArtWork artwork in sortedArtworks.ToList())
                    {
                        if (artwork.Artwork_Length < excessMaterialLength && artwork.Artwork_Quantity > 0)
                        {
                            return artwork;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    return null;
                }

                while (sortedArtworks.Any(artwork => (int)artwork.Artwork_Quantity > 0))
                {
                    double remainingLength = materialLengthValue;

                    while (Math.Floor(remainingLength) > 0)
                    {
                        bool startNewMaterial = false;
                        foreach (ArtWork currentArtwork in sortedArtworks.ToList())
                        {
                            if ((int)currentArtwork.Artwork_Quantity <= 0) continue;

                            int fullRowQuantity = (int)Math.Floor(materialWidthValue / (double)currentArtwork.Artwork_Width);
                            int lengthwiseFittableArtwork = (int)Math.Floor(remainingLength / (double)currentArtwork.Artwork_Length);

                            int maximumFittableArtworkCount = lengthwiseFittableArtwork * fullRowQuantity;

                            if (maximumFittableArtworkCount > 0)
                            {
                                int printableArtworks = Math.Min(maximumFittableArtworkCount, ((int)Math.Ceiling(currentArtwork.Artwork_Quantity / (double)fullRowQuantity) * fullRowQuantity));
                                int totalLengthUsed = (int)(Math.Ceiling((double)printableArtworks / fullRowQuantity) * currentArtwork.Artwork_Length);

                                PrintBatch batchListArtwork = new PrintBatch
                                {
                                    Print_Batch_Number = $"Batch {batchCount}",
                                    Artwork_Id = currentArtwork.Artwork_Id,
                                    Artwork_Name = currentArtwork.Artwork_Name,
                                    Print_Quantity = printableArtworks,
                                    Length_Used = totalLengthUsed
                                };

                                currentArtwork.Artwork_Quantity -= printableArtworks;
                                remainingLength -= totalLengthUsed;

                                batchArtworkList.Add(batchListArtwork);


                                bool isTooFull = false;

                                while (isTooFull == false)
                                {

                                    ArtWork additionalArtwork = findAdditionalArtwork(remainingLength);

                                    if (additionalArtwork != null)
                                    {
                                        int additionalRowQuantity = (int)Math.Floor(materialWidthValue / (double)additionalArtwork.Artwork_Width);
                                        int lengthwiseFittableAdditionalArtwork = (int)Math.Floor(remainingLength / (double)additionalArtwork.Artwork_Length);

                                        int maxFittableAdditionalArtwork = lengthwiseFittableAdditionalArtwork * additionalRowQuantity;

                                        if (maxFittableAdditionalArtwork > 0)
                                        {
                                            int printableAdditionalArtworks = Math.Min(maxFittableAdditionalArtwork, ((int)Math.Ceiling(additionalArtwork.Artwork_Quantity / (double)additionalRowQuantity) * fullRowQuantity));
                                            int additionalArtworkTotalLengthUsed = (int)(Math.Ceiling((double)printableAdditionalArtworks / additionalRowQuantity) * additionalArtwork.Artwork_Length);

                                            PrintBatch _batchListArtwork = new PrintBatch
                                            {
                                                Print_Batch_Number = $"Batch {batchCount}",
                                                Artwork_Id = additionalArtwork.Artwork_Id,
                                                Artwork_Name = additionalArtwork.Artwork_Name,
                                                Print_Quantity = printableAdditionalArtworks,
                                                Length_Used = additionalArtworkTotalLengthUsed
                                            };

                                            additionalArtwork.Artwork_Quantity -= printableAdditionalArtworks;
                                            remainingLength -= additionalArtworkTotalLengthUsed;

                                            batchArtworkList.Add(_batchListArtwork);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isTooFull = true;
                                        break;
                                    }


                                    if (!isTooFull) break;
                                }

                            }
                            else
                            {
                                break;
                            }
                        }

                        if (!startNewMaterial) break;

                    }
                    batchCount++;
                }

                SaveOutputToCsv(batchArtworkList);

                MessageBox.Show($"File saved successfully!");

                OpenFolder();
                resetMaterialInput();
                resetArtworkInput();
                resetArtworkList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveOutputToCsv(List<PrintBatch> batchListArtworks)
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
                if (batchListArtworks.Count > 0)
                {
                    var firstRecord = batchListArtworks.First();
                    //var properties = ((IDictionary<string, object>)firstRecord).Keys.ToList();

                    // Write header
                    csv.WriteField("Batch #");
                    csv.WriteField("Artwork ID");
                    csv.WriteField("Artwork Name");
                    csv.WriteField("Quantity");
                    csv.WriteField("Length Used");

                    csv.NextRecord();

                    foreach (PrintBatch record in batchListArtworks)
                    {

                        csv.WriteField(record.Print_Batch_Number);
                        csv.WriteField(record.Artwork_Name);
                        csv.WriteField(record.Print_Quantity);
                        csv.WriteField(record.Length_Used);
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
