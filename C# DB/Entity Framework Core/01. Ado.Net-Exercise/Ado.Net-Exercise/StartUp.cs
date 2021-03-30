namespace Ado.Net_Exercise
{
    class StartUp
    {
        static void Main()
        {
            var tasks = new Tasks();

            // 1. Initial Setup
            //tasks.InitialSetup();

            // 2. Villain Names
            //tasks.VillainNames();

            // 3. Minion Names
            //tasks.MinionNames();

            // 4. Add Minion
            //tasks.AddMinion();

            // 5. Change Town Names Casing
            //tasks.ChangeTownNamesCasing();

            // 6. *Remove Villain
            //tasks.RemoveVillain();

            // 7. Print All Minion Names 
            //tasks.PrintAllMinionNames();

            // 8. Increase Minion Age 
            //tasks.IncreaseMinionAge();

            // 9. Increase Age Stored Procedure  
            tasks.IncreaseAgeStoredProcedure();
        }
    }
}
