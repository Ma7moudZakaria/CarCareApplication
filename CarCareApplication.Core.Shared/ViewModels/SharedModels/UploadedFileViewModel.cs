using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.SharedModels
{
    public class UploadedFileViewModel
    {
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
        [JsonPropertyName("fileContent")]
        public byte[] FileContent { get; set; }
    }
}
