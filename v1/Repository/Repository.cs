using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using v1.Enums;

namespace v1.Repository
{
    public interface IRepository
    {
        Task<List<T>> ExecuteQuery<T>(string query, Func<IDataReader, T> mapper);
        Task<List<T>> ExecuteQueryNoMap<T>(string query);
        bool ExecuteQuery(string query);
    }

    public class Repository : IRepository
    {
        private readonly IConfiguration _configuration;
        public Repository
        (
            IConfiguration configuration
        )
        {
            _configuration = configuration;
        }
        public bool ExecuteQuery(string query)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStrings.DefaultConnection)))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Transaction = transaction;
                            int rowsAffected = command.ExecuteNonQuery();
                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                        throw;
                    }
                }
            }
        }
        public async Task<List<T>> ExecuteQuery<T>(string query, Func<IDataReader, T> mapper)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Transaction = transaction;

                            var resultList = new List<T>();

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (reader.Read())
                                {
                                    // Mapeia cada linha do resultado para o tipo desejado e adiciona à lista
                                    var result = mapper(reader);
                                    resultList.Add(result);
                                }
                            }

                            await transaction.CommitAsync();
                            return resultList;
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async Task<List<T>> ExecuteQueryNoMap<T>(string query)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Transaction = transaction;

                            var resultList = new List<T>();

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (reader.Read())
                                {
                                    var item = MapToObject<T>(reader);
                                    resultList.Add(item);
                                }
                            }

                            await transaction.CommitAsync();
                            return resultList;
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        private T MapToObject<T>(IDataReader reader)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var item = Activator.CreateInstance<T>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var property = properties.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                if (property != null && !reader.IsDBNull(i))
                {
                    var value = reader.GetValue(i);
                    property.SetValue(item, value);
                }
            }

            return item;
        }

    }
}
