using MLM.Business.Interfaces;
using MLM.Common.DTO;
using MLM.DataAccess.DACs;

namespace MLM.Business
{
    public class Business: IBusiness
    {
        public Usuario GetUser(string user)
        {
            return (new LoginDAC()).getUser(user);
        }
    }
}
