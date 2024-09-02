using Microsoft.Data.SqlClient;
using Model;

namespace Test.Service
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User_Model> GetUserAsync(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserName COLLATE SQL_Latin1_General_CP1_CS_AS = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new User_Model
                        {
                            Id = (int)reader["Id"],
                            UserName = reader["UserName"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            PasswordSalt = reader["PasswordSalt"].ToString(),
                            Role = reader["Role"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public async Task<int> CreateUserAsync(User_Model user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (UserName, PasswordHash, PasswordSalt, Role, Email) VALUES (@username, @passwordHash, @passwordSalt, @role, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@passwordSalt", user.PasswordSalt);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateVerificationCodeAsync(int userId, string code)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET VerificationCode = @code WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@id", userId);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }
    }

   
}
