using ApiNetCoreStoredProcedureDemo.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNetCoreStoredProcedureDemo.Data
{
    public class ValuesRepository
    {
        private readonly string _connectionString;

        public ValuesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<IEnumerable<Value>> GetAll()
        {
            using(SqlConnection sql = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetAllValues", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Value>();
                    await sql.OpenAsync();                    
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;

                }
            }
        }
        private Value MapToValue(SqlDataReader reader)
        {
            return new Value()
            {
                Id = (int)reader.GetInt32(0),
                Value1 = (int)reader.GetInt32(1),
                Value2 = reader.GetString(2)
            };
        }
        public async Task<Value> GetById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetValueById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    Value response = null; 
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;

                }
            }
        }
        public async Task Insert(Value value)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertValue", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Value1",value.Value1);
                    cmd.Parameters.AddWithValue("@Value2", value.Value2);
                    
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task DeleteById(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteValue", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);                    

                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        
    }
}
