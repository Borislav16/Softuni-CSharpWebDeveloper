namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.Helper;
    using Newtonsoft.Json;
    using System.ComponentModel;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            var creators = context.Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDto()
                {
                    FullName = c.FirstName + " " + c.LastName,
                    BoardgamesCount = c.Boardgames.Count(),
                    Boardgames = c.Boardgames
                        .Select(bg => new ExportBoardgameDto()
                        {
                            FullName = bg.Name,
                            YearOfPublishing = bg.YearPublished
                        })
                        .OrderBy(bg => bg.FullName)
                        .ToArray()
                })
                .OrderByDescending(c => c.Boardgames.Count())
                .ThenBy(c => c.FullName)
                .ToList();

            return XmlSerializationHelper.Serialize(creators, "Creators");
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers
                    .Any(bgs => bgs.Boardgame.YearPublished >= year &&
                        bgs.Boardgame.Rating <= rating))
                .Select(s => new
                {
                    s.Name,
                    s.Website,
                    Boardgames = s.BoardgamesSellers
                        .Where(bgs => bgs.Boardgame.YearPublished >= year &&
                                bgs.Boardgame.Rating <= rating)
                        .Select(bgs => new
                        {
                            bgs.Boardgame.Name,
                            bgs.Boardgame.Rating,
                            bgs.Boardgame.Mechanics,
                            Category = bgs.Boardgame.CategoryType.ToString()
                        })
                        .OrderByDescending(bgs => bgs.Rating)
                        .ThenBy(bgs => bgs.Name)
                        .ToList()

                })
                .OrderByDescending(s => s.Boardgames.Count())
                .ThenBy(s => s.Name)
                .Take(5)
                .ToList();

            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}