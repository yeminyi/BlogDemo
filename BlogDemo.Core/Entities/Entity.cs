using BlogDemo.Core.Interfaces;

namespace BlogDemo.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
