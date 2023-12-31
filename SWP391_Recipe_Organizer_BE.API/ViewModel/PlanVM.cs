﻿using Newtonsoft.Json;
using SWP391_Recipe_Organizer_BE.Repo.EntityModel;
using System.Runtime.Serialization;

namespace SWP391_Recipe_Organizer_BE.API.ViewModel
{
    public class PlanVM
    {
        public string? PlanId { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? PlanName { get; set; }
        public string? PlanDescription { get; set; }

        public UserAccountVM? User { get; set; }
        public ICollection<PlanDetailVM>? PlanDetailVMs { get; set; }
    }
    public class PlanUpdateVM
    {
        public string? PlanId { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? PlanName { get; set; }
        public string? PlanDescription { get; set; }
        public ICollection<PlanDetailVM>? PlanDetailVMs { get; set; }
    }
    public class PlanVMReturn
    {
    }
}
