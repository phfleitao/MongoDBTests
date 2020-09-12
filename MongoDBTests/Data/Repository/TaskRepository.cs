using MongoDBTests.Models;

namespace MongoDBTests.Data.Repository
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository() : base("Tasks") { }

        public void BeginTran()
        {
            throw new System.NotImplementedException();
        }
    }
}
