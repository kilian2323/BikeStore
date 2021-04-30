using Core.Models;

namespace UI.Interfaces
{
    public interface IMainRequiredByLogin
    {
        public void Login(Staff e);
        public void Logout();
    }
}
