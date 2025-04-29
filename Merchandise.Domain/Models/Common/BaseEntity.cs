using System.ComponentModel.DataAnnotations;
using Merchandise.Domain.Extensions;

namespace Merchandise.Domain.Models.Common
{
    public class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTimeOffset DateTimeCreated { get; private set; }
        public DateTimeOffset DateTimeLastUpdated { get; private set; }
        [MaxLength(36)]
        public string? CreatedBy { get; private set; }
        [MaxLength(36)]
        public string? UpdatedBy { get; private set; }
		public bool IsDeleted { get; private set; }
		public bool IsActive { get; private set; }
        public bool IsArchived { get; private set; }

        public BaseEntity()
        {
            var date = DateTimeExtensions.ToPhilippineTime(DateTime.UtcNow);

            Id = Guid.NewGuid();
            DateTimeCreated = date;
            DateTimeLastUpdated = date;
            IsDeleted = false;
            IsActive = true;
            IsArchived = false;

        }

        public void SetCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
        }

        public void SetUpdatedBy(string updatedBy)
        {
            UpdatedBy = updatedBy;
        }

        public void SetDateTimeLastUpdated()
        {
            var date = DateTimeExtensions.ToPhilippineTime(DateTime.UtcNow);
            DateTimeLastUpdated = date;
        }

        public void SetDateTimeLastUpdated(DateTime date)
        {
            DateTimeLastUpdated = date;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}
