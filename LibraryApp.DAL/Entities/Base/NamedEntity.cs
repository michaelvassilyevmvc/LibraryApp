using LibraryApp.Interfaces.Base.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
