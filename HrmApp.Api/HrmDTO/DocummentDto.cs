using System.ComponentModel.DataAnnotations;

namespace HrmApp.Api.HrmDTO
{
    public class DocummentDto
    {
        public int Id { get; internal set; }
        public int IdClient { get; set; }
        public string DocumentName { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public DateTime UploadDate { get; set; }

        public string? UploadedFileExtention { get; set; }

        public byte[]? UploadedFile { get; set; }
        public DateTime? SetDate { get; set; }
        public IFormFile? UpFile { get; set; }







    }
}
