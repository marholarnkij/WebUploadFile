using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lib.Contract
{
    public interface IUnitOfWork
    {
        bool ExecuteTransaction(Action executeCommand);
        void BeginSave();
        void Commit();
    }
}
