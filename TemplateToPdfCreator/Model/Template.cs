using System.ComponentModel.DataAnnotations;

namespace Template_To_PDF_Creator.Model
{
    public class Template
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string HtmlContent { get; set; } = "";
    }
}
