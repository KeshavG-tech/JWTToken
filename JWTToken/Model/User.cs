namespace JWTToken.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //Jb role add krne ho tb
        //public string Role { get; set; }
    }
}
