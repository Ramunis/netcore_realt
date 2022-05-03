using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCLab2.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Username { get; set; }                   	//Свойство класса

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]                 	//Сокрытие текста при вводе
        public string PW { get; set; }	//Свойство  класса
    }
}
