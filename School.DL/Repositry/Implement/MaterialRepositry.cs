using Microsoft.EntityFrameworkCore;
using School.Core.Data;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public class MaterialRepositry : IMaterialRepositry
    {
        private readonly ApplicationDbContext _context;
        public MaterialRepositry(ApplicationDbContext context)
        {
            _context = context;
        }

        public Material AddMaterial(Material material)
        {
            _context.Materials.Add(material);
            int rowAffected = _context.SaveChanges();
            return material;
        }

        public  bool DeleteMaterial(int id)
        {
            _context.Remove(_context.Materials.Find(id));
            int rowAffected= _context.SaveChanges();
            return rowAffected > 0;
        }

        public Material GetMaterial(int id)
        {
            return _context.Materials.Find(id);
        }
        public async Task<Material?> GetMaterialAsync(int id)
        {
            return await _context.Materials.FindAsync(id);
        }
        public IQueryable<Material> GetMaterials()
        {
            return _context.Materials.AsQueryable();
        }

        public bool UpdateMaterial(Material material)
        {
            _context.Materials.Update(material);
            int rowAffect= _context.SaveChanges();
            return rowAffect > 0;
        }
    }
}
