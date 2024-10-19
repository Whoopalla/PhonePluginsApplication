using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Domain;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;

namespace EmployeesLoaderPlugin
{
    [Author(Name = "Ivan Petrov")]
    public class Plugin : IPluggable
    {
        const string ListCmd = "ls";
        const string AddCmd = "add";
        const string RemoveCmd = "rm";
        const string QuitCmd = "q";
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Starting Viewer");
            logger.Info("Type q or quit to exit");
            logger.Info($"Available commands: {ListCmd}, {AddCmd}, {RemoveCmd}, {QuitCmd}");

            var employeesList = args.Cast<EmployeesDTO>().ToList();

            string command = "";

            while (!command.ToLower().Contains(QuitCmd))
            {
                Console.Write("> ");
                command = Console.ReadLine();

                switch (command)
                {
                    case ListCmd:
                        int index = 0;
                        foreach (var employee in employeesList)
                        {
                            Console.WriteLine($"{index} Name: {employee.Name} | Phone: {employee.Phone}");
                            ++index;
                        }
                        break;
                    case AddCmd:
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        if (String.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Provide correct name");
                            break;
                        }
                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();
                        if (String.IsNullOrEmpty(phone))
                        {
                            Console.WriteLine("Phone must be provided");
                            break;
                        }
                        var newEmp = new EmployeesDTO();
                        newEmp.Name = name;
                        newEmp.AddPhone(phone == null ? String.Empty : phone);
                        employeesList.Add(newEmp);
                        Console.WriteLine($"{name} added to employees");
                        break;
                    case RemoveCmd:
                        Console.Write("Index of employee to delete: ");
                        int indexToDelete;
                        if (!Int32.TryParse(Console.ReadLine(), out indexToDelete))
                        {
                            logger.Error("Not an index or not an int value!");
                        }
                        else
                        {
                            if (indexToDelete >= 0 && indexToDelete < employeesList.Count())
                            {
                                employeesList.RemoveAt(indexToDelete);
                            }
                        }
                        break;
                }

                Console.WriteLine("");
            }

            return employeesList.Cast<DataTransferObject>();
        }
    }
}