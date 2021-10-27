using Autransoft.Template.EntityFramework.PostgreSQL.Lib.Exceptions;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Interfaces
{
    public interface IAutranSoftLogger<Repository>
        where Repository : class
    {
        void Error(AutranSoftEfException autranSoftEfException);
    }
}