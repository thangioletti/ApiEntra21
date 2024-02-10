using Dapper;
using MinhaApiBonita.Contracts.Repository;
using MinhaApiBonita.DTO;
using MinhaApiBonita.Entity;
using MinhaApiBonita.Infrastructure;

namespace MinhaApiBonita.Repository
{
    public class UserRepository : Connection, IUserRepository
    {
        public async Task Add(UserDTO user)
        {
            string sql = @"
                INSERT INTO USER (Name, Email, Password, Role)
                            VALUE (@Name, @Email, @Password, @Role)
            ";
            await Execute(sql, user);

        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM USER WHERE Id = @id";
            await Execute(sql, new { id });
        }

        public async Task<IEnumerable<UserEntity>> Get()
        {
            string sql = "SELECT * FROM USER";
            return await GetConnection().QueryAsync<UserEntity>(sql);
            
        }

        public async Task<UserEntity> GetById(int id)
        {
            string sql = "SELECT * FROM USER WHERE Id = @id";
            return await GetConnection().QueryFirstAsync<UserEntity>(sql, new { id });
        }

        public async Task<UserTokenDTO> LogIn(UserLoginDTO user)
        {
            string sql = "SELECT * FROM user WHERE Email = @Email AND Password = @Password";
            UserEntity userLogin = await GetConnection().QueryFirstAsync<UserEntity>(sql, user);
            return new UserTokenDTO
            {
                Token = Authentication.GenerateToken(userLogin),
                User = userLogin
            };
        }

        public async Task Update(UserEntity user)
        {
            string sql = @"
                UPDATE USER 
                   SET Name = @Name, 
                       Email = @Email, 
                       Password = @Password,
                       Role = @Role
                 WHERE Id = @Id
            ";
            await Execute(sql, user);
        }

        
    }
}
