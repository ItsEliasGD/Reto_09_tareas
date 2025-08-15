using Reto_09_tareas.Models;

namespace Reto_09_tareas.Services
{
    public class Tasks
    {
        public Tasks() { }
        private readonly RetoTareasDBContext _db = new();

        public List<Tareas> GetTasks()
        {
            return _db.Tareas.ToList();
        }

        public List<Tareas> CreateTask(Tareas tarea)
        {
            if (tarea != null)
            {
                _db.Tareas.Add(tarea);
                _db.SaveChanges();
            }

            return _db.Tareas.ToList();
        }
    }
}
