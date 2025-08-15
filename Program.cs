using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

using Reto_09_tareas.Models;
using Reto_09_tareas.Data;

namespace Reto_09_tareas
{
    class Program
    {
        // INTENCIONAL: conexión con placeholders; el alumno debe ajustar.
        private const string ConnectionString = "Server=localhost;Database=RetoTareasDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            Console.WriteLine("== Gestor de Tareas ==");
            Console.Write("Ingrese texto para validar (Título 3-60 chars): ");
            var input = Console.ReadLine() ?? string.Empty;

            // INTENCIONAL: posible regex demasiado permisiva/estricta
            if(!Regex.IsMatch(input, @"^.{3,60}$\\"))
            {
                Console.WriteLine("Entrada inválida segun la expresión regular.");
            }
            else
            {
                Console.WriteLine("Entrada válida.");
            }

            var repo = new Repository(ConnectionString);
            // INTENCIONAL: uso de la tabla posiblemente incorrecta adrede en Repository
            List<Tarea> items = repo.GetAllTareas();

            // INTENCIONAL: off-by-one al iterar
            for (int i = 0; i <= items.Count - 1; i++) // debería ser i < items.Count
            {
                Console.WriteLine(items[i].ToString());
            }

            // INTENCIONAL: búsqueda con comparación de mayúsculas/minúsculas confusa
            Console.Write("Filtro por texto (deja vacío para ver todos): ");
            var filtro = Console.ReadLine() ?? "";
            var filtrados = items.FindAll(x => x.ToString().Contains(filtro)); // debería normalizar
            Console.WriteLine($"Coincidencias: {filtrados.Count}");

            // INTENCIONAL: Inserción sin validar campos obligatorios
            Console.WriteLine("Intentando insertar un registro de ejemplo...");
            var nuevo = new Tarea() { Id = 0, Titulo = "", Completada = false };
            var filas = repo.InsertTarea(nuevo);
            Console.WriteLine($"Filas afectadas (esperadas 1): {filas}");
        }
    }
}
