﻿using System;
using System.Collections.Generic;

namespace SWP391_Recipe_Organizer_BE.Repo.EntityModel
{
    public partial class PlanDetail
    {
        public string PlanDetailId { get; set; } = null!;
        public string? PlanId { get; set; }
        public string? RecipeId { get; set; }
        public DateTime? Date { get; set; }
        public int? MealOfDate { get; set; }

        public virtual Plan? Plan { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
