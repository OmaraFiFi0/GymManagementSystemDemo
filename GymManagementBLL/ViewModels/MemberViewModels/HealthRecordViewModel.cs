using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class HealthRecordViewModel
    {
        [Required(ErrorMessage ="Height Is Required")]
        [Range(0.1 , 354 , ErrorMessage ="Hight Must Be Greater Than 0 And Less Than 354 ") ]
        public decimal Height { get; set; }

        [Required(ErrorMessage ="Weight Is Required")]
        [Range(0.1, 400, ErrorMessage = "Weight Must Be Greater Than 0 And Less Than 400 ")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage ="Blood Type Is Required")]
        [StringLength(3,MinimumLength = 1,ErrorMessage ="Blood Type Must Between 1 and 3 Characters")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }
    }
}
