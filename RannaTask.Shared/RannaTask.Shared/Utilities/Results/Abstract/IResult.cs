using RannaTask.Shared.Utilities.Results.ComplexTypes;

namespace RannaTask.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } //ResulStatus.Succes ResultStatus.Error
        public string Message { get; }
        public Exception Exception { get; }
    }
}
