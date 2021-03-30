using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryElite.Classes;

namespace MilitaryElite
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var soldiers = new Dictionary<int, ISoldier>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split();

                var type = inputArgs[0];
                var id = int.Parse(inputArgs[1]);
                var firstName = inputArgs[2];
                var lastName = inputArgs[3];

                if (type == "Spy")
                {
                    var codeNumber = int.Parse(inputArgs[4]);

                    var spy = new Spy(id, firstName, lastName, codeNumber);

                    soldiers.Add(id, spy);
                }
                else
                {
                    var salary = decimal.Parse(inputArgs[4]);

                    switch (type)
                    {
                        case "Private":
                        {
                            var privateSolider = new Private(id, firstName, lastName, salary);

                            soldiers.Add(id, privateSolider);
                            break;
                        }
                        case "LieutenantGeneral":
                        {
                            var idNums = inputArgs.Skip(5)
                                .Select(int.Parse)
                                .ToArray();

                            IList<IPrivate> privates = idNums.Select(idInt => (IPrivate) soldiers[idInt]).ToList();
                            var lieutenantGeneral = new LieutenantGeneral(id, firstName, lastName, salary, privates);

                            soldiers.Add(id, lieutenantGeneral);
                            break;
                        }
                        default:
                        {
                            var crops = inputArgs[5];

                            if (crops == "Airforces" || crops == "Marines")
                            {
                                switch (type)
                                {
                                    case "Engineer":
                                    {
                                        ICollection<IRepair> repairs = new List<IRepair>();

                                        for (var i = 6; i < inputArgs.Length; i += 2)
                                        {
                                            var name = inputArgs[i];
                                            var hours = int.Parse(inputArgs[i + 1]);
                                            var repair = new Repair(name, hours);
                                            repairs.Add(repair);
                                        }

                                        var engineer = new Engineer(id, firstName, lastName, salary, crops, repairs);

                                        soldiers.Add(id, engineer);
                                        break;
                                    }
                                    case "Commando":
                                    {
                                        ICollection<IMission> missions = new List<IMission>();

                                        for (var i = 6; i < inputArgs.Length; i += 2)
                                        {
                                            var name = inputArgs[i];
                                            var state = inputArgs[i + 1];
                                            if (state == "inProgress" || state == "Finished")
                                            {
                                                var mission = new Mission(name, state);
                                                missions.Add(mission);
                                            }
                                        }

                                        var commando = new Commando(id, firstName, lastName, salary, crops, missions);

                                        soldiers.Add(id, commando);
                                        break;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }

            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.Value);
            }
        }
    }
}
