namespace Roll_Calculator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

    public class ArtWork
    {
        public string Artwork_Id { get; set; }
        public string Artwork_Name { get; set; }
        public double Artwork_Length { get; set; }
        public double Artwork_Width { get; set; }
        public int Artwork_Quantity { get; set; }
        private double Total_Length { get; set; }

        public override string ToString()
        {
            return string.Format("{1} - Qty: {0} - {2}mm x {3}mm", Artwork_Quantity, Artwork_Name, Artwork_Width, Artwork_Length);
        }
    }

    public class PrintBatch
    {
        public string Print_Batch_Number { get; set; }
        public string Artwork_Id { get; set; }
        public string Artwork_Name { get; set; }
        public int Print_Quantity { get; set; }
        public int Length_Used { get; set; }
    }
}