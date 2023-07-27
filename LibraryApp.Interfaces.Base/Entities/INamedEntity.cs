using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Interfaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string Name { get; }
    }
}
