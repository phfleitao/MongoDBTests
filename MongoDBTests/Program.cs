using MongoDBTests.Data.Repository;
using MongoDBTests.Models;
using System;
using System.Linq;

namespace MongoDBTests
{
    partial class Program
    {
        private static readonly ITaskRepository _taskRepository = new TaskRepository();
        static void Main(string[] args)
        {
            //Sample

            InsertTasks();
            UpdateFirstTask();
            DeleteLastTask();

            PrintAllTasks();
            PrintCompletedTasks();
            DeleteAllTasks();
        }

        static void PrintAllTasks()
        {
            var tasks = _taskRepository.SelectAll();
            Console.WriteLine("PRINTING ALL TASKS ...");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Description} - Completed: {task.Completed}");
            }
            Console.WriteLine("");
        }

        static void PrintCompletedTasks()
        {
            var tasks = _taskRepository.SelectWhere(o => o.Completed == true);
            Console.WriteLine("PRINTING COMPLETED TASKS ...");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task: {task.Description} - Completed: {task.Completed}");
            }
            Console.WriteLine("");
        }

        static void InsertTasks()
        {
            Console.WriteLine("INSERTING NEW TASKS ...");
            var task1 = new Task { Description = "Task 1", Completed = false };
            var task2 = new Task { Description = "Task 2", Completed = true };
            var task3 = new Task { Description = "Task 3", Completed = true };
            var task4 = new Task { Description = "Task 4", Completed = false };
            _taskRepository.Save(task1);
            _taskRepository.Save(task2);
            _taskRepository.Save(task3);
            _taskRepository.Save(task4);
        }

        static void UpdateFirstTask()
        {
            Console.WriteLine("UPDATING FIRST TASK ...");
            var task = _taskRepository.SelectAll().FirstOrDefault();
            
            task.Description = "Task 1 - Updated";
            _taskRepository.Save(task);
        }

        static void DeleteLastTask()
        {
            Console.WriteLine("DELETING LAST TASK ...");
            var task = _taskRepository.SelectAll().LastOrDefault();
            _taskRepository.Delete(task.Id);
        }

        static void DeleteAllTasks()
        {
            Console.WriteLine("DELETING ALL TASKS ...");
            var tasks = _taskRepository.SelectAll();
            foreach (var task in tasks)
            {
                _taskRepository.Delete(task.Id);
            }
        }
    }
}
