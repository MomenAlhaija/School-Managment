using School.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDTO>> GetAllMaterials();
        Task AddMaterial(AddOrUpdateMaterialDTO material);
        Task<AddOrUpdateMaterialDTO> GetMaterialById(int id);
        Task UpdateMaterial(AddOrUpdateMaterialDTO material);
        void DeleteMaterial (int id);
    }
}
