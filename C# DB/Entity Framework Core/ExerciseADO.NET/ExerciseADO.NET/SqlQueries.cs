using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseADO.NET
{
    public static class SqlQueries
    {
        public const string GetVillainsWithNumberOfMinions = @"SELECT v.Name, COUNT(*) AS [TotalMinions]
                                                        FROM Villains AS v
                                                        JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
                                                        GROUP BY v.Name
                                                        HAVING COUNT(*) > 3";


        public const string GetVillainId = @"SELECT Name FROM Villains WHERE Id = @Id";

        public const string GetOrderedMinionsByVillainId = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum,
                                                                                         m.Name, 
                                                                                         m.Age
                                                                                    FROM MinionsVillains AS mv
                                                                                    JOIN Minions As m ON mv.MinionId = m.Id
                                                                                   WHERE mv.VillainId = @Id
                                                                                ORDER BY m.Name";

        public const string GetTownByName = @"SELECT Id FROM Towns WHERE Name = @townName";

        public const string GetVillainByName = @"SELECT Id FROM Villains WHERE Name = @villainName";

        public const string InsertNewTown = @"INSERT INTO Towns ([Name]) OUTPUT inserted.Id VALUES (@townName)";

        public const string InsertIntoMinionsVillains = @"INSERT INTO MinionsVillains(MinionId, VillainId) VALUES (@minionId, @villainId)";

        public const string InsertNewMinion = @"INSERT INTO Minions([Name], Age, TownId) OUTPUT inserted.Id VALUES (@minionName, @minionAge, @townId)";

        public const string InsertNewVillain = @"INSERT INTO Villains([Name], EvilnessFactorId) OUTPUT inserted.Id VALUES (@villainName, @evilnessFactorID)";
    }
}
