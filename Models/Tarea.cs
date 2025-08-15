using System;

namespace Reto_09_tareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Completada { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Titulo} | {Completada}";
        }
    }
}
