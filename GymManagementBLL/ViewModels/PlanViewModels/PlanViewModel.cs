using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Name Is Required")]
        [StringLength(50, ErrorMessage = "Name Must be Less Than 51 Characters")]
        public string Name { get; set; } = null!;
        [Required (ErrorMessage = "Description Is Required")]
        [StringLength(200 , MinimumLength =5 , ErrorMessage ="Description Must be Between 5 and 200 Characters")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Duration Days Is Required")]
        [Range(1, 365, ErrorMessage = "Duration Days Must Be Between 1 and 365")]
        public int DurationDays { get; set; }
        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1, 20000, ErrorMessage = "Price Must Be Between 0.1 To 20000")]
        public decimal  Price { get; set; }
        public bool  IsActive { get; set; }
    }
}
