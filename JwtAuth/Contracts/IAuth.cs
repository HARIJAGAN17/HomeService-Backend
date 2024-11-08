using JwtAuth.Model;

namespace JwtAuth.Contracts
{
    public interface IAuth
    {
        public Task<object> CheckLoginCred(Login model);

        public Task<Response> CustomerRegister(Register model);

        public Task<Response> ServiceProviderRegister(Register model);

        public Task<Response> AdminRegister(Register model);

    }
}
