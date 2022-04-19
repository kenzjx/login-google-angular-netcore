namespace Server.Options
{
    public class Authentication
    {
        public JwtOptions Jwt {set;get;}

        public GoogleOptions Goole {set;get;}
    }

    public class JwtOptions
    {
        public string Secret {set;get;}

        public string Issuer {set;get;}

        public string Audience {set;get;}

        public string Subject {set;get;}
    }

    public class GoogleOptions
    {
        public string ClientId {set;get;}

        public string ClientSecret {set;get;}
    }

}