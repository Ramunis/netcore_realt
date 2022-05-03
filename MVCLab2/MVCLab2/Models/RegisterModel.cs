using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCLab2.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]                 	//Сокрытие текста при вводе
        public string PW { get; set; }	//Свойство  класса

        [DataType(DataType.Password)]
        [Compare("PW", ErrorMessage = "Ошибка в задании пароля")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указан Фамилия")]
        public string F { get; set; }

        [Required(ErrorMessage = "Не указан Имя")]
        public string I { get; set; }

        [Required(ErrorMessage = "Не указан Отчество")]
        public string O { get; set; }

        [Required(ErrorMessage = "Не указан Дата")]
        public DateTime DR { get; set; }

        [Required(ErrorMessage = "Не указан Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Не указан Адрес")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Не указан Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
    }
}
