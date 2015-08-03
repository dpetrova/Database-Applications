using System;
using MultimediaShop.Models;

namespace MultimediaShop.Interfaces
{
    public enum State
    {
        Pending,
        Returned,
        Overdue
    }

    public interface IRent
    {
        IItem Item { get; }

        State RentState { get; }

        decimal CalcRentFine();

        void ReturnItem();
    }
}
