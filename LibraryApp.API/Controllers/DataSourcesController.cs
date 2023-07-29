using LibraryApp.API.Controllers.Base;
using LibraryApp.DAL.Entities;
using LibraryApp.Interfaces.Base.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers
{
    
    public class DataSourcesController : EntityController<DataSource>
    {
        public DataSourcesController(IRepository<DataSource> repository) : base(repository) { }

    }
}
