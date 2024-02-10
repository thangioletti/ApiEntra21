using MinhaApiBonita.Entity;

namespace MinhaApiBonita.DTO
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public UserEntity User { get; set; }
    }
}
