using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Lab01
{
    public interface IResult
    {
        Guid ID 
        {
            get;
        }

        bool Success
        {
            get;
            set;
        }

        string Message
        {
            get;
            set;
        }

        Exception Exception
        {
            get;
            set;
        }

        List<IResult> InnerResults
        {
            get;
        }
    }
}