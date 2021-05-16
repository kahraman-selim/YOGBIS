using System;
using System.Collections.Generic;
using System.Text;

namespace YOGBIS.Common.ResultModels
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
