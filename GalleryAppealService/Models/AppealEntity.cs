using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalleryAppealService.Models
{
    public class AppealEntity
    {
        public string CrmEntityLogicalName { get; set; }
        public string AppealType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Question { get; set; }

        public AppealEntity()
        {
            CrmEntityLogicalName = "incident";
        }
    }

    
}