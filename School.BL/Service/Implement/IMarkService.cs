﻿using School.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public interface IMarkService
    {
        Task InsertMark(StudentGradeDTO mark);
    }
}
