namespace SmartDom.Client
{
    public class ClientCredentials
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public ClientCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
