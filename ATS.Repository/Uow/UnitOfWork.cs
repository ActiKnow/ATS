using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Interface;
using ATS.Repository.Repo;

namespace ATS.Repository.Uow
{
    public class UnitOfWork : IDisposable
    {
        private readonly ATSDBContext _context;
        public UnitOfWork()
        {
            this._context =new ATSDBContext();
            AttemptedHistoryRepo = new AttemptHistoryRepository(_context);
            MapOptionRepo = new MapOptionRepository(_context);
            MapQuestionRepo = new MapQuestionRepository(_context);
            OptionRepo = new OptionRepository(_context);
            QuestionRepo = new QuestionRepository(_context);
            TestAssignmentRepo = new TestAssignmentRepository(_context);
            TestHistoryRepo = new TestHistoryRepository(_context);
            TypeDefRepo = new TypeDefRepository(_context);
            UserCredentialRepo = new UserCredentialRepository(_context);
            UserRepo = new UserRepository(_context);
            TestBankRepo = new TestBankRepository(_context);
            ResultRepo = new ResultRepository(_context);
        }

        public IAttemptHistoryRepository AttemptedHistoryRepo { get;private set; }
        public IMapOptionRepository MapOptionRepo { get; private set; }
        public IMapQuestionRepository MapQuestionRepo { get; private set; }
        public IOptionRepository OptionRepo { get; private set; }
        public IQuestionRepository QuestionRepo { get; private set; }
        public ITestAssignmentRepository TestAssignmentRepo { get; private set; }
        public ITestHistoryRepository TestHistoryRepo { get; private set; }
        public ITypeDefRepository TypeDefRepo { get; private set; }
        public IUserCredentialRepository UserCredentialRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }
        public ITestBankRepository TestBankRepo { get; private set; }
        public IResultRepository ResultRepo { get; private set; }

        public void Commit()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch
            {
                Dispose();
                throw;
            }
            
        }
               
        public void Dispose()
        {
            this._context.Dispose();
        }        
    }
}
