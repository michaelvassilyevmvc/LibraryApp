using LibraryApp.Interfaces.Base.Entities;

namespace LibraryApp.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
