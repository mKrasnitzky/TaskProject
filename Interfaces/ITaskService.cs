
using TaskProject.Models;

namespace TaskProject.Interfaces{

    public interface ITaskService
    {
        List<TaskProject.Models.Task> GetAll();

        TaskProject.Models.Task GetById(int id);

        int Add(TaskProject.Models.Task newTask);

        bool Updata(int id, TaskProject.Models.Task newTask);

        bool Delete(int id);
    }

}