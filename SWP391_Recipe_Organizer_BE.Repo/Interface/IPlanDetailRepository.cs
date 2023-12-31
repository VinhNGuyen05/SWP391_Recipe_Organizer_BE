﻿using SWP391_Recipe_Organizer_BE.Repo.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Recipe_Organizer_BE.Repo.Interface
{
    public interface IPlanDetailRepository : IGenericRepository<PlanDetail>
    {
        bool RemoveRange(List<PlanDetail> planDetails);
    }
}
