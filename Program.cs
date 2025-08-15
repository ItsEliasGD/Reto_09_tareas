using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Reto_09_tareas.Models;
using Reto_09_tareas.Services;

namespace Reto_09_tareas
{
    class Program
    {
        private static readonly Tasks _tasks = new();

        static void Main(string[] args)
        {
            Console.WriteLine("== Gestor de Tareas ==");

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\nMenú:");
                Console.WriteLine("1. Listar tareas");
                Console.WriteLine("2. Filtrar tareas");
                Console.WriteLine("3. Crear tarea");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        ListarTareas();
                        break;
                    case "2":
                        FiltrarTareas();
                        break;
                    case "3":
                        CrearTarea();
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        PauseAndClean();
                        break;
                }
            }
        }

        static void ListarTareas()
        {
            try
            {
                var items = _tasks.GetTasks();
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine($"{item.Id} - {item.Titulo} - Estado: {(item.Completada ? "Completada" : "Incompleta")}");
                    }
                }
                else
                {
                    Console.WriteLine("No hay tareas registradas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar tareas: {ex.Message}");
            }

            PauseAndClean();
        }

        static void FiltrarTareas()
        {
            Console.Write("Filtro por texto (deja vacío para ver todos): ");
            var filtro = Console.ReadLine() ?? "";

            try
            {
                var items = _tasks.GetTasks();
                var filtrados = items.FindAll(x =>
                    x.Titulo.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                );

                Console.WriteLine($"Coincidencias: {filtrados.Count}");
                foreach (var item in filtrados)
                {
                    Console.WriteLine($"{item.Id} - {item.Titulo} - Estado: {(item.Completada ? "Completada" : "Incompleta")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al filtrar tareas: {ex.Message}");
            }

            PauseAndClean();
        }

        static void CrearTarea()
        {
            Console.Write("Ingrese título de la tarea (3-60 caracteres): ");
            var titulo = Console.ReadLine() ?? string.Empty;

            if (!Regex.IsMatch(titulo.Trim(), @"^.{3,60}$"))
            {
                Console.WriteLine("Título inválido. Debe tener entre 3 y 60 caracteres.");
                PauseAndClean();
                return;
            }

            var nuevaTarea = new Tareas()
            {
                Titulo = titulo.Trim(),
                Completada = false
            };

            try
            {
                var filas = _tasks.CreateTask(nuevaTarea);
                if (filas.Count > 0)
                    Console.WriteLine("Tarea creada con éxito.");
                else
                    Console.WriteLine("No se pudo insertar la tarea.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear tarea: {ex.Message}");
            }

            PauseAndClean();
        }

        static void PauseAndClean()
        {
            Console.WriteLine("\nPulsa cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
