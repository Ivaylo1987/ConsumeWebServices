namespace ClientApp.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class WebToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }
    }
}
