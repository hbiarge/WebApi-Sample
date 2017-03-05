using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
