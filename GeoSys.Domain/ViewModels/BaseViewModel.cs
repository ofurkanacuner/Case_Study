#region - Using

using System;

#endregion

namespace GeoSys.Domain.ViewModels
{
    public abstract class BaseViewModel
    {

        public int Id { get; set; }

        public bool Status { get; set; }

        public bool IsItDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DateOfUpdate { get; set; }
    }
}
