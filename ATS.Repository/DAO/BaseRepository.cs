using System.Data.Entity;
using ATS.Repository.Interface;
namespace ATS.Repository.DAO
{
     public class BaseRepository 
    {
        public ATSDBContext GetConnection()
        {
            return new ATSDBContext();
        }
    }
}
