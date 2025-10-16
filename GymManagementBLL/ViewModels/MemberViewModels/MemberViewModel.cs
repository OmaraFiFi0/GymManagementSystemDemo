﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class MemberViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string Name { get; set; }= null!;
        public string Email { get; set; }=null!;
        public string? Phone { get; set; }
        public string Gender { get; set; } = null!;

    }
}
