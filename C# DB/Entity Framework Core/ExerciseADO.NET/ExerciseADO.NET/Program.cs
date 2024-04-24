using System.Data.SqlClient;

namespace ExerciseADO.NET
{
    internal class Program
    {

        const string connectionString = @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";
        static SqlConnection? sqlConnection;

        static async Task Main(string[] args)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                //Problem 02
                //await GetVillainsWithNumberOfMinions();

                //Problem 03
                //await GetOrderedMinionsByVillainId(7,);

                //Problem 04
                string minionInfoRaw = Console.ReadLine();
                string villainInfoRaw = Console.ReadLine();

                string minionInfo = minionInfoRaw.Substring(minionInfoRaw.IndexOf(':') + 1).Trim();
                string villainName = villainInfoRaw.Substring(villainInfoRaw.IndexOf(':') + 1).Trim();

                AddMinion(minionInfo, villainName);
            }
            finally
            {
                sqlConnection.Dispose();
            }
        }

        static async Task GetVillainsWithNumberOfMinions(SqlConnection? sqlConnection)
        {

            using SqlCommand sqlCommand = new SqlCommand(SqlQueries.GetVillainsWithNumberOfMinions, sqlConnection);

            using SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlReader.Read())
            {
                Console.WriteLine($"{sqlReader["Name"]} - {sqlReader["TotalMinions"]}");

            }
        }

        static async Task GetOrderedMinionsByVillainId(int id, SqlConnection? sqlConnection)
        {
            using SqlCommand command = new SqlCommand(SqlQueries.GetVillainId, sqlConnection);
            command.Parameters.AddWithValue("@Id", id);
            var result = await command.ExecuteScalarAsync();
            if (result is null)
            {
                await Console.Out.WriteLineAsync($"No villain with ID {id} exists in the database.");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Villain: {result}");

                using SqlCommand commandGetMinionData = new SqlCommand(SqlQueries.GetOrderedMinionsByVillainId, sqlConnection);
                commandGetMinionData.Parameters.AddWithValue("@Id", id);

                var minionsReader = await commandGetMinionData.ExecuteReaderAsync();

                while (await minionsReader.ReadAsync())
                {
                    await Console.Out.WriteLineAsync($"{minionsReader["RowNum"]}. " +
                        $"{minionsReader["Name"]} {minionsReader["Age"]}");

                }

            }
        }


        static async Task AddMinion(string minionInfo, string villainName)
        {
            SqlTransaction transaction = sqlConnection.BeginTransaction();
            string[] minionData = minionInfo.Split(' ');
            string minionName = minionData[0];
            int minionAge = int.Parse(minionData[1]);
            string minionTown = minionData[2];
            await Console.Out.WriteLineAsync(minionData[0]);

            try
            {

                #region Town
                using SqlCommand cmdGetTownId = new SqlCommand(SqlQueries.GetTownByName, sqlConnection, transaction);
                cmdGetTownId.Parameters.AddWithValue("@townName", minionTown);

                var townResult = await cmdGetTownId.ExecuteScalarAsync();

                int townId = -1;
                if (townResult is null)
                {


                    using SqlCommand createTown = new SqlCommand(SqlQueries.InsertNewTown, sqlConnection, transaction);
                    createTown.Parameters.AddWithValue("@townName", minionTown);
                    townId = Convert.ToInt32(await createTown.ExecuteScalarAsync());
                    await Console.Out.WriteLineAsync($"Town {minionTown} was added to the database");
                }
                else
                {
                    townId = (int)townResult;
                }
                #endregion

                #region Villain
                using SqlCommand cmdGetVillain = new SqlCommand(SqlQueries.GetVillainByName, sqlConnection, transaction);
                cmdGetVillain.Parameters.AddWithValue("@villainName", villainName);
                var villainResult = await cmdGetVillain.ExecuteScalarAsync();

                int villainId = -1;
                if (villainResult is null)
                {
                    using SqlCommand cmdInsertNewVillain = new SqlCommand(SqlQueries.InsertNewVillain, sqlConnection, transaction);
                    cmdInsertNewVillain.Parameters.AddWithValue("@villainName", villainName);
                    cmdInsertNewVillain.Parameters.AddWithValue("@evilnessFactorID", 4);
                    villainId = Convert.ToInt32(await cmdInsertNewVillain.ExecuteScalarAsync());
                    await Console.Out.WriteLineAsync($"Villain {villainName} was added to the database");
                }

                #endregion

                #region Minion
                using SqlCommand cmdInsertMinion = new SqlCommand(SqlQueries.InsertNewMinion, sqlConnection, transaction);
                cmdInsertMinion.Parameters.AddWithValue("@minionName", minionName);
                cmdInsertMinion.Parameters.AddWithValue("@minionAge", minionName);
                cmdInsertMinion.Parameters.AddWithValue("@townId", townId);
                await Console.Out.WriteLineAsync($"Minion {minionName} was added to database");

                int minionId = Convert.ToInt32(await cmdInsertMinion.ExecuteScalarAsync());
                using SqlCommand cmdInsertMinionVillain = new SqlCommand(SqlQueries.InsertIntoMinionsVillains, sqlConnection, transaction);
                await cmdInsertMinion.ExecuteNonQueryAsync();
                await Console.Out.WriteLineAsync($"Successfully added {minionName} was added as servant to {villainName}");

                #endregion
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
    }
}
