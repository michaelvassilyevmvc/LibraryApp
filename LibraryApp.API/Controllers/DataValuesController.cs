using LibraryApp.API.Controllers.Base;
using LibraryApp.DAL.Entities;
using LibraryApp.Interfaces.Base.Repositories;

namespace LibraryApp.API.Controllers
{
    public class DataValuesController : EntityController<DataValue>
    {
        public DataValuesController(IRepository<DataValue> repository) : base(repository) { }

    }
}
