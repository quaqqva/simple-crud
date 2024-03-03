using Backend.Domain.Common;

namespace Backend.Application.Enums
{
    public class EntityActionType : BaseEnum
    {
        public static EntityActionType Created { get; } = new("created");

        public static EntityActionType Updated { get; } = new("updated");

        public static EntityActionType Deleted { get; } = new("deleted");

        private EntityActionType(string value)
            : base(value) { }
    }
}
