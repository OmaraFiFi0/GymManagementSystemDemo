using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50 , MinimumLength =3 , ErrorMessage ="Name must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z]\s+$")]
        // + Meaing One or More 
        // * Meaing Zero or More
        // ? Meaing Zero or One
        // ^ Start of String
        // $ End of String
        //\s White Space
        // [a-zA-Z]\s Any Character From a to z or A to Z  or White Space
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")] // Validation
        [StringLength(100 , MinimumLength = 5,ErrorMessage ="Email must be between 5 and 100 characters")]
        [DataType(DataType.EmailAddress)] // UI Hint
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number Format")]
        [DataType(DataType.PhoneNumber)] // UI Hint
        [RegularExpression(@"^(010|011|012|015)\d{8}$",ErrorMessage ="Phone Number Must Be Valid Egyption Phone Number")]
        public string Phone { get; set; }= null!;
        [Required(ErrorMessage = "Gender is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date Of Birth is Required")]
        [DataType(DataType.Date)] // UI Hint
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Building Number is Required")]
        [Range(1 ,  2000 , ErrorMessage ="Building Number Must Be Between 1 and 2000")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street is Required")]
        [StringLength(50 , MinimumLength =2 , ErrorMessage ="Street must be between 2 and 30 characters")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "City is Required")]
        [StringLength(30 , MinimumLength =2 , ErrorMessage ="City must be between 2 and 30 characters")]
        [RegularExpression(@"^[a-zA-Z]\s+$")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "Health Record is Required")]
        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;
    }
}
