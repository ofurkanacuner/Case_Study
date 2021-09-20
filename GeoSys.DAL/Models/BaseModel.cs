#region - Using

using System;

#endregion

namespace GeoSys.DAL.Models
{
    public abstract class BaseModel
    {
        #region -Properties

        public int Id { get; set; } // Id
        public bool Status { get; set; } //Durum
        public bool IsItDeleted { get; set; } //Silindi mi?
        public DateTime CreationDate { get; set; } //Oluşturma Tarihi
        public DateTime? DateOfUpdate { get; set; } //Güncellenme Tarihi

        #endregion
    }
}
