using System;

namespace BusinessTrips.DAL.ViewModel
{
    public class ConfirmUserViewModel
    {
        public Guid Id { get; set; }

        public ConfirmUserViewModel(Guid userId)
        {
            Id = userId;
        }
    }
}
