namespace MultimediaShop.Interfaces
{
    using System;
    using System.Collections.Generic;

    public enum Genre
    {
        action,
        triller,
        criminal,
        sciFi,
        history,
        comedy,
        fantasy,
        child,
        story,
        strategy,
        adventure,
        RPG,
        horror
    }
    
    public interface IItem
    {
        string Id { get; }

        string Title { get; }

        decimal Price { get; }

        IList<Genre> Genres { get; }
    }
}
