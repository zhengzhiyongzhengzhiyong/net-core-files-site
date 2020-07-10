using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source_Api.Helper.AuthHelper.Authorization
{
    public interface IResourceWithCreator
    {
        string Creator { get; set; }
    }
}
