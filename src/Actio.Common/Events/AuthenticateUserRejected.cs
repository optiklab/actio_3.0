namespace Actio.Common.Events
{
    public class AuthenticateUserRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }

        public AuthenticateUserRejected(string email, string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}