namespace ClientApp.ViewModels
{
    using ClientApp.DataModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EmployeeArrivalViewModel
    {
        public static Expression<Func<EmployeeArrival, EmployeeArrivalViewModel>> FromDbModel
        {
            get
            {
                return e => new EmployeeArrivalViewModel()
                {
                    EmployeeId = e.EmployeeId,
                    When = e.When
                };
            }
        }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime When { get; set; }
    }
}
