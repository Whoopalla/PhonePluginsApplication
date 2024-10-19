1. No linux support. 
    dotnetFramework does not supports linux
    change to .Net core

2. Becouse there is no linux support, 
    there is no way to produce .resx file.

3. The code for adding things doesn't add anything:
``` c#
case "add":
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine($"{name} added to employees");
            break;
```
4. The code for deleting things deletes them from local copy of the resources:

```c#
var employeesList = args.Cast<EmployeesDTO>().ToList();
```
```c#
case "del":
            Console.Write("Index of employee to delete: ");
            int indexToDelete;
            if(!Int32.TryParse(Console.ReadLine(), out indexToDelete))
            {
              logger.Error("Not an index or not an int value!");
            } else {
              if(indexToDelete > 0 && indexToDelete < employeesList.Count())
              {
                employeesList.RemoveAt(indexToDelete);
              }
            }
            break;
```
5. The plugins are loaded at random order (alphabetically, i gues)
    so the EmloyeesLoaderPlugin may be loaded last and EmployeesVeiwerPlugin 
    may have nothing to show.

```c#
List<EmployeesDTO> dto = new List<EmployeesDTO>();

    foreach(var plugin in Loader.Plugins)
    {
         dto = plugin.Run(dto).Cast<EmployeesDTO>().ToList();
    }
```
