using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommandRepository : ICommandRepository
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 1, HowTo = "Boil an egg", Line = "Boil Water", Platform = "kettle & pan" },
                new Command { Id = 2, HowTo = "Cut bread", Line = "Get a knife", Platform = "cutting board" },
                new Command { Id = 3, HowTo = "Make cup of tea", Line = "Place teabag in cup", Platform = "kettle & cup" },
            };


            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 1, HowTo = "Boil an egg", Line = "Boil Water", Platform = "kettle & pan" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
