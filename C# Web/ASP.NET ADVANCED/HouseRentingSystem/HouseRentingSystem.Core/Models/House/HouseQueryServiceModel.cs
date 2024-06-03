namespace HouseRentingSystem.Core.Models.House
{
    public class HouseQueryServiceModel
    {
        public int TotalHouseCount { get; set; }

        public IEnumerable<HouseServiceModel> Houses { get; set; }
            = new List<HouseServiceModel>();
    }
}
