using RannaTask.Shared.Entities.Abstract;

namespace RannaTask.Entities.Concrete
{
    //[Id]
    // ,[Name]
    // ,[Code]
    // ,[Price]
    // ,[CreatedDate]
    // ,[Image]
    public class Product : EntityBase, IEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }




    }
}
