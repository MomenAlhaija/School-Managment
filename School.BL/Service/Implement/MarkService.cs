using School.BL.DTO;
using School.Core.Entities;
using School.DL.Repositry;
namespace School.BL.Service
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepositry _markRepositry;
        public MarkService(IMarkRepositry markRepositry)
        {
                _markRepositry=markRepositry;
        }
        public async Task InsertMark(StudentGradeDTO mark)
        {
            var markFromDb = await _markRepositry.GetMarkStudentInMaterial(mark.MaterialId, mark.StudentId);
            if (markFromDb is null)
            {
                _markRepositry.AddMark(new Mark
                {
                    StudentId = mark.StudentId,
                    ClassId = mark.ClassId,
                    MaterialId = mark.MaterialId,
                    MarkInPercent = mark.Grade
                });
            }
            else
            {
                markFromDb.MarkInPercent = mark.Grade;
                await _markRepositry.UpdateMark(markFromDb);
            }
        }
    }
}
