using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressForWeather.WebAPI.BackendModels.EFCoreModels.ViewModels
{
    [Obsolete("?")]
    public class UserViewModel
    {
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
