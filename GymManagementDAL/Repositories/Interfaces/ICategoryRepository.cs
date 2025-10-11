using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ICategoryRepository
    {
        //GetAll
        IEnumerable<Category> GetAll();
        //GetById
        Category? GetById(int id);
        //Add
        int Add(Category category);
        //Update
        int Update(Category category);
        //Delete
        int Delete(int id); 
    }
}
