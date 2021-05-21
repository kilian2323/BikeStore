using Core.Models.Tables;

namespace UI.Interfaces
{
    public interface IMainRequiredByLogin
    {
        public void LoggedIn(Staff e);
        public void Logout();
    }
}
