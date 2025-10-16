﻿using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        //GetAll
        IEnumerable<Plan> GetAll();
        //GetById
        Plan? GetById(int id);
        //Update
        int Update(Plan plan);
    }
}
