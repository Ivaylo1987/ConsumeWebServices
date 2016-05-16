namespace ClientApp.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeArrival
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime When { get; set; }

        [Required]
        public WebToken WebToken { get; set; }

    }
}
