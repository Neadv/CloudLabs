namespace lab1.Models
{
    public class AzureStorageSettings
    {
        public bool UseDevEmulator { get; set; }
        public string ContainerName { get; set; } = null!;
        public string? ConnectionString { get; set; }
    }
}
