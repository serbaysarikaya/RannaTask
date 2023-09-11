using RannaTask.Shared.Utilities.Results.ComplexTypes;

namespace RannaTask.Shared.Entities.Abstract
{
    public class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }

    }
}
