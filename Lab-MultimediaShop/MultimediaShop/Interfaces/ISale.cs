using System;
using MultimediaShop.Models;

namespace MultimediaShop.Interfaces
{
    public interface ISale
    {
        IItem Item { get; }

        DateTime DateOfPurchase { get; }
    }
}
