using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Source_Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(128, MinimumLength = 6, ErrorMessage = "minimum Length of {0} is 6.")]
        public string Password { get; set; }
    }
}
