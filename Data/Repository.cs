using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Reto_09_tareas.Models;

namespace Reto_09_tareas.Data
{
    public class Repository
    {
        private readonly string _connStr;
        public Repository(string connStr) => _connStr = connStr;

        public List<Tarea> GetAllTareas()
        {
            var list = new List<Tarea>();
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            // INTENCIONAL: posible error de nombre de tabla o columnas (a corregir)
            using var cmd = new SqlCommand("SELECT Id, Titulo, Completada FROM ToDo", conn);
            using var rd = cmd.ExecuteReader();
            while(rd.Read())
            {
            var item = new Tarea();
            item.Id = rd.GetInt32(0);
            item.Titulo = rd.GetString(1);
            item.Completada = rd.GetBoolean(2);
            list.Add(item);
            }
            return list;
        }

        public int InsertTarea(Tarea item)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            // INTENCIONAL: Falta de validación y columnas desordenadas
            using var cmd = new SqlCommand("INSERT INTO Tareas (Id, Titulo, Completada) VALUES (@p0, @p1, @p2)", conn);
            cmd.Parameters.AddWithValue("@p0", item.Id);
            cmd.Parameters.AddWithValue("@p1", item.Titulo);
            cmd.Parameters.AddWithValue("@p2", item.Completada);
            return cmd.ExecuteNonQuery();
        }
    }
}
