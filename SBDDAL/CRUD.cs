using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SBDModelClass;

namespace SBDDAL
{
    public class CRUD
    {
        public static async Task<List<T>> ReadDataAsync<T>(string storedProcedure, T? model) where T : class, new()
        {
            try
            {
                using (var con = DbHelper.GetConnection())
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand(storedProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (model != null)
                    {
                        foreach (var property in typeof(T).GetProperties())
                        {
                            var value = property.GetValue(model);
                            if (value != null && !string.IsNullOrEmpty(value.ToString()))
                            {
                                cmd.Parameters.AddWithValue($"@{property.Name}", value);
                            }
                        }
                    }

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    List<T> entities = new List<T>();

                    while (await reader.ReadAsync())
                    {
                        T entity = new T();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (reader[property.Name] != DBNull.Value)
                            {
                                property.SetValue(entity, reader[property.Name]);
                            }
                        }
                        entities.Add(entity);
                    }

                    return entities;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static List<T> ReadData<T>(string storedProcedure, T? model) where T : class, new()
        {
            try
            {
                using (var con = DbHelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(storedProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (model != null)
                    {
                        foreach (var property in typeof(T).GetProperties())
                        {
                            var value = property.GetValue(model);
                            if (value != null && !string.IsNullOrEmpty(value.ToString()))
                            {
                                cmd.Parameters.AddWithValue($"@{property.Name}", value);
                            }
                        }
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<T> entities = new List<T>();

                    while (reader.Read())
                    {
                        T entity = new T();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (reader[property.Name] != DBNull.Value)
                            {
                                property.SetValue(entity, reader[property.Name]);
                            }
                        }
                        entities.Add(entity);
                    }

                    return entities;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task CUDAsync<T>(T entity, string storedProcedure)
        {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var property in typeof(T).GetProperties())
                    {
                        var value = property.GetValue(entity);
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            cmd.Parameters.AddWithValue($"@{property.Name}", value);
                        }
                    }

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        throw new Exception("Error executing stored procedure", ex);
                    }
                }
            }
        }

        public static string GenerateUniqueId()
        {
            Random random = new Random();
            int randomValue = random.Next(0, 65536); // Generate a random number between 0 and 65535
            string uniqueId = randomValue.ToString("X4"); // Convert to a 4-digit hexadecimal string
            return uniqueId;
        }

    }
}