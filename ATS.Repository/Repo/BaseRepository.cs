using System.Data.Entity;
using ATS.Repository.Interface;
namespace ATS.Repository.Repo
{
    public class BaseRepository 
    {
        public ATSDBContext GetConnection()
        {
            return new ATSDBContext();
        }
    }
}
