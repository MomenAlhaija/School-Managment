using Microsoft.EntityFrameworkCore.Migrations.Operations;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface IClassRepositry
    {
        IQueryable<Class> GetClasses();
        Task<Class> AddClass(Class input);
        Task<Class> GetClassById(int id);
        void DeleteClass(int id);
        void UpdateClass(Class input);
    }
}
