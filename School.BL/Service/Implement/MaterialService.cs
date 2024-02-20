using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using School.BL.DTO;
using School.Core.Entities;
using School.DL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepositry _materialRepositry;
        private readonly IClassMaterialRepositry _classMaterialRepo;

        public void DeleteMaterial(int materialId)
        {
           _materialRepositry.DeleteMaterial(materialId); 
        }

        public MaterialService(IMaterialRepositry materialRepositry,IClassMaterialRepositry classMaterialRepo)
        {
            _materialRepositry = materialRepositry;
            _classMaterialRepo = classMaterialRepo;
        }
        public async Task<AddOrUpdateMaterialDTO> GetMaterialById(int id)
        {
            try
            {
                var material = _materialRepositry.GetMaterial(id);
                if (material != null && id == material.Id)
                {
                    return new AddOrUpdateMaterialDTO
                    {
                        Id = material.Id,
                        MaterialNameAr = material.MaterialNameAr,
                        MaterialNameEn = material.MaterialNameEn,

                    };
                }
                else
                    throw new Exception("Not Found The Materila");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<MaterialDTO>> GetAllMaterials()
        {
            var materials = _materialRepositry.GetMaterials();
            var materialDTOs = materials.Select(material => new MaterialDTO
            {
                Id = material.Id,
                MaterialNameAr = material.MaterialNameAr,
                MaterialNameEn = material.MaterialNameEn,
            });
            return materialDTOs;
        }
        public async Task AddMaterial(AddOrUpdateMaterialDTO material)
        {

            var materiles = await GetAllMaterials();
            if(materiles.Any(p=>p.MaterialNameEn.Equals(material.MaterialNameEn, StringComparison.OrdinalIgnoreCase) &&
                p.MaterialNameAr.Equals(material.MaterialNameAr, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("The Material is Already Found");
            }
            _materialRepositry.AddMaterial(new Material()
            {
                MaterialNameAr = material.MaterialNameAr,
                MaterialNameEn = material.MaterialNameEn
            });

         
        }

        public async Task UpdateMaterial(AddOrUpdateMaterialDTO material)
        {
           
            var materiles = await GetAllMaterials();
            if (materiles.Any(p => p.MaterialNameEn.Equals(material.MaterialNameEn, StringComparison.OrdinalIgnoreCase) &&
                    p.MaterialNameAr.Equals(material.MaterialNameAr, StringComparison.OrdinalIgnoreCase) && p.Id != material.Id))
            {
                throw new Exception("The Material is Already Found");
            }
            var materialFromDb = _materialRepositry.GetMaterial(material.Id.Value);
            if (materialFromDb != null && material.Id==materialFromDb.Id)
            {
                materialFromDb.MaterialNameAr = material.MaterialNameAr;
                materialFromDb.MaterialNameEn = material.MaterialNameEn;
                var result = _materialRepositry.UpdateMaterial(materialFromDb);
            }
            else
                throw new Exception("Not Found The Material");
            
          
        }
    }
}
