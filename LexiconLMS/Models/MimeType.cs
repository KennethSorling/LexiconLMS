namespace LexiconLMS.Models
{
    public class MimeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DefaultExtension { get; set; }
        public string IconURL { get; set; }

        public MimeType() { }
        public MimeType(string name, string defaultExtension, string iconUrl)
        {
            Name = name;
            DefaultExtension = defaultExtension;
            IconURL = iconUrl;
        } 
    }
}