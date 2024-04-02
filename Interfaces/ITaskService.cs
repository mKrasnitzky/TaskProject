
using TaskProject.Models;

namespace TaskProject.Interfaces{

    public interface ITaskService
    {
        List<TaskProject.Models.Task> GetAll(int userId);

        TaskProject.Models.Task GetById(int id, int userId);

        int Add(TaskProject.Models.Task newTask, int userId);

        bool Update(int id, TaskProject.Models.Task newTask, int userId);

        bool Delete(int id, int UserId);

        void DeleteTasks(int userId);
    }

}