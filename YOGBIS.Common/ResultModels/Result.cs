using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace YOGBIS.Common.ResultModels
{
    public class Result<T> : IResult
    {
        public bool IsSuccess { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int TotalCount { get; set; }

        public Result()
        {
            Success = false;
            Message = string.Empty;
            Data = default(T);
        }

        public Result(bool isSuccess, string message, string komisyonId) : this(isSuccess, message, default(T))
        {

        }

        public Result(bool isSuccess, string message, T data) : this(isSuccess, message, data, 0)
        {

        }

        public Result(bool isSuccess, string message, T data, int totalCount)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            TotalCount = totalCount;
        }

        public Result(bool v, string recordRemoveSuccessfully)
        {
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return Task.FromResult(Data).GetAwaiter();
        }
    }
}
