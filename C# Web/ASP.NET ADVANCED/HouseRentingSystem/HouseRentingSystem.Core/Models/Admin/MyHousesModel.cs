using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Models.Admin
{
    public class MyHousesModel
    {
        public IEnumerable<HouseServiceModel> AddedHouses { get; set; }
            = new List<HouseServiceModel>();
        
        public IEnumerable<HouseServiceModel> RentedHouses { get; set; } 
            = new List<HouseServiceModel>();
    }
}
