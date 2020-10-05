namespace LoadFromSpreadsheet
{
    public class Application
    {
        public string? ApplicationName { get; set; }
        public string? ImportSpreadsheet { get; set; }
        public string? DeleteFiles { get; set; }

        public string? LocalDirectory { get; set; }
        public string? LocalImportDirectory { get; set; }

        public string? SeqServer { get; set; }
        public string? ImportDB { get; set; }
    }
}
