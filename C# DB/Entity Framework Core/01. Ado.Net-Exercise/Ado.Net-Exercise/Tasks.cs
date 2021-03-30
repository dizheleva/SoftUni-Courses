#nullable enable
namespace Ado.Net_Exercise
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Microsoft.Data.SqlClient;

    class Tasks
    {
        private const string MasterConnectionString = @"Server=.;Database=master;Integrated Security=true";

        private const string MinionsDBConnectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        // 1. Initial Setup
        internal void InitialSetup()
        {
            // Create Database
            using var masterConnection = new SqlConnection(MasterConnectionString);
            masterConnection.Open();
            using var masterConnectionCommand = new SqlCommand(Queries.CreateDB, masterConnection);
            masterConnectionCommand.ExecuteNonQuery();

            // Create Tables
            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();
            using var command = new SqlCommand(Queries.CreateTables, connection);
            command.ExecuteNonQuery();
        }

        // 2. Villain Names
        internal void VillainNames()
        {
            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();
            using var command = new SqlCommand(Queries.CountVillainNames, connection);
            command.ExecuteNonQuery();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]} - {reader[1]}");
            }
        }

        // 3. Minion Names
        internal void MinionNames()
        {
            var villainId = int.Parse(Console.ReadLine());

            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();
            PrintVillain(connection, villainId);
            PrintMinions(connection, villainId);
        }

        // 4. Add Minion
        internal void AddMinion()
        {
            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                var minionInfo = Console.ReadLine().Split(' ');
                var minionName = minionInfo[1];
                var minionAge = int.Parse(minionInfo[2]);
                var townName = minionInfo[3];

                var townId = GetTownId(connection, transaction, townName);
                
                var villainInfo = Console.ReadLine().Split(' ');
                var villainName = villainInfo[1];

                var villainId = GetVillainId(connection, transaction, villainName);

                InsertMinion(connection, transaction, minionName, minionAge, townId);

                var minionId = GetMinionId(connection, transaction, minionName);

                AddMinionToVillain(connection, transaction, minionId, villainId);

                Console.WriteLine(Messages.AddedMinion, minionName, villainName);
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
        }

        // 5. Change Town Names Casing
        internal void ChangeTownNamesCasing() 
        {
            var countryName = Console.ReadLine();

            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();

            using var updateCommand = new SqlCommand(Queries.UpdateTownNames, connection);
            updateCommand.Parameters.AddWithValue("@countryName", countryName);
            var affectedTowns = updateCommand.ExecuteNonQuery();

            using var selectCommand = new SqlCommand(Queries.SelectTownsByCountry, connection);
            selectCommand.Parameters.AddWithValue("@countryName", countryName);
            using var reader = selectCommand.ExecuteReader();

            try
            {
                if (!reader.HasRows)
                {
                    throw new Exception(Messages.NoAffectedTowns);
                }

                var towns = new List<string>();

                while (reader.Read())
                {
                    towns.Add((string)reader[0]);
                }

                Console.WriteLine(Messages.ChangedTownsCount, affectedTowns);
                Console.WriteLine($"[{string.Join(", ", towns)}]");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // 6. *Remove Villain 
        internal void RemoveVillain()
        {
            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                var villainId = int.Parse(Console.ReadLine());
                var villainName = GetVillainName(connection, transaction, villainId);

                if (villainName == null)
                {
                    throw new ArgumentException(Messages.NotFoundVillain);
                }

                var releasedMinionsCount = CountVillainsMinions(connection, transaction, villainId);

                if (releasedMinionsCount != 0)
                {
                    DeleteVillain(connection, transaction, villainId, Queries.DeleteFromVillainMinions);
                }

                DeleteVillain(connection, transaction, villainId, Queries.DeleteVillain);

                Console.WriteLine(Messages.DeletedVillain, villainName);
                Console.WriteLine(Messages.ReleasedMinionCount, releasedMinionsCount);
                transaction.Commit();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            catch (Exception)
            {
               transaction.Rollback();
            }
        }

        // 7. Print All Minion Names 
        internal void PrintAllMinionNames()
        {
            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();

            using var command = new SqlCommand(Queries.SelectMinionNames, connection);
            using var reader = command.ExecuteReader();
            var minions = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    minions.Add((string)reader[0]);
                }
            }

            var outputOrder = new List<string>();

            for (var i = 0; i < minions.Count / 2; i++)
            {
                outputOrder.Add(minions[i]);
                outputOrder.Add(minions[minions.Count - i - 1]);
                if (minions.Count % 2 != 0 && i == minions.Count / 2 - 1)
                {
                    outputOrder.Add(minions[minions.Count - i - 2]);
                }
            }

            Console.WriteLine(string.Join("\n", outputOrder));
        }

        // 8. Increase Minion Age 
        internal void IncreaseMinionAge()
        {
            var minionIds = Console.ReadLine().Split().Select(int.Parse).ToArray();

            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();

            foreach (var minionId in minionIds)
            {
                using var updateCommand = new SqlCommand(Queries.UpdateMinionNames, connection);
                updateCommand.Parameters.AddWithValue("@minionId", minionId);
                updateCommand.ExecuteNonQuery();
            }

            using var command = new SqlCommand(Queries.SelectMinionNameAge, connection);
            command.ExecuteNonQuery();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]} {reader[1]}");
            }
        }

        // 9. Increase Age Stored Procedure  
        internal void IncreaseAgeStoredProcedure()
        {
            var minionId = int.Parse(Console.ReadLine());

            using var connection = new SqlConnection(MinionsDBConnectionString);
            connection.Open();

            using var executeCommand = new SqlCommand(Queries.ExecuteProcGetOlder, connection);
            executeCommand.Parameters.AddWithValue("@minionId", minionId);
            executeCommand.ExecuteNonQuery();

            using var command = new SqlCommand(Queries.SelectNameAgeByMinionId, connection);
            command.Parameters.AddWithValue("@minionId", minionId);
            command.ExecuteNonQuery();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]} - {reader[1]} years old");
            }
        }

        private void DeleteVillain(SqlConnection connection, SqlTransaction transaction, int villainId, string query)
        {
            using var deleteCommand = new SqlCommand(query, connection, transaction);
            deleteCommand.Parameters.AddWithValue("@villainId", villainId);

            try
            {
                deleteCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }
        private int CountVillainsMinions(SqlConnection connection, SqlTransaction transaction, int villainId)
        {
            using var command = new SqlCommand(Queries.SelectVillainsMinions, connection, transaction);
            command.Parameters.AddWithValue("@villainId", villainId);

            using var reader = command.ExecuteReader();
            var counter = 0;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    counter++;
                }
            }

            return counter;
        }
        private string? GetVillainName(SqlConnection connection, SqlTransaction transaction, in int villainId)
        {
            using var command = new SqlCommand(Queries.SelectVillainNameById, connection, transaction);
            command.Parameters.AddWithValue("@villainId", villainId);
            var villainName = (string?)command.ExecuteScalar();
            
            return villainName;
        }
        
        private void AddMinionToVillain(SqlConnection connection, SqlTransaction transaction, int minionId, int? villainId)
        {
            using var command = new SqlCommand(Queries.AddVillainMinion, connection, transaction);
            command.Parameters.AddWithValue("@minionId", minionId);
            command.Parameters.AddWithValue("@villainId", villainId);
            command.ExecuteNonQuery();
        }
        private int GetMinionId(SqlConnection connection, SqlTransaction transaction, string minionName)
        {
            using var command = new SqlCommand(Queries.SelectMinionByName, connection, transaction);
            command.Parameters.AddWithValue("@minionName", minionName);
            var minionId = command.ExecuteScalar();

            return (int)minionId;
        }
        private void InsertMinion(SqlConnection connection, SqlTransaction transaction, string minionName, int age, int? townId)
        {
            using var command = new SqlCommand(Queries.AddNewMinion, connection, transaction);
            command.Parameters.AddWithValue("@name", minionName);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@townId", townId);
            command.ExecuteNonQuery();
        }
        private void InsertVillain(SqlConnection connection, SqlTransaction transaction, string name)
        {
            using var command = new SqlCommand(Queries.AddNewVillain, connection, transaction);
            command.Parameters.AddWithValue("@villainName", name);
            command.ExecuteNonQuery();

            Console.WriteLine(Messages.VillainAdded, name);
        }
        private int? GetVillainId(SqlConnection connection, SqlTransaction transaction, string villainName)
        {
            using var command = new SqlCommand(Queries.SelectVillainIdByName, connection, transaction);
            command.Parameters.AddWithValue("@villainName", villainName);
            var villainId = (int?)command.ExecuteScalar();
            if (villainId == null)
            {
                InsertVillain(connection, transaction, villainName);
                villainId = GetVillainId(connection, transaction, villainName);
            }

            return villainId;
        }
        private void InsertTown(SqlConnection connection, SqlTransaction transaction, string townName)
        {
            using var command = new SqlCommand(Queries.AddNewTown, connection, transaction);
            command.Parameters.AddWithValue("@townName", townName);
            command.ExecuteNonQuery();

            Console.WriteLine(Messages.TownAdded, townName);
        }
        private int? GetTownId(SqlConnection connection, SqlTransaction transaction, string townName)
        {
            using var command = new SqlCommand(Queries.SelectTownIdByName, connection, transaction);
            command.Parameters.AddWithValue("@townName", townName);
            
            var townId = (int?)command.ExecuteScalar();
            
            if (townId == null)
            {
                InsertTown(connection, transaction, townName);
                townId = GetTownId(connection, transaction, townName);
            }

            return townId;
        }
       
        private void PrintMinions(SqlConnection connection, int villainId)
        {
            using var command = new SqlCommand(Queries.SelectVillainsMinions, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            using var reader = command.ExecuteReader();
            try
            {
                if (!reader.HasRows)
                {
                    throw new Exception(Messages.NoMinions);
                }

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}. {reader[1]} {reader[2]}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void PrintVillain(SqlConnection connection, int villainId)
        {
            using var command = new SqlCommand(Queries.SelectVillainNameById, connection);
            command.Parameters.AddWithValue("@villainId", villainId);
            var villainName = command.ExecuteScalar();

            try
            {
                if (villainName == null)
                {
                    throw new Exception(string.Format(Messages.NotExistingVillain, villainId));
                }

                Console.WriteLine($"Villain: {villainName}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(Environment.ExitCode);
            }

        }
        
    }
}
