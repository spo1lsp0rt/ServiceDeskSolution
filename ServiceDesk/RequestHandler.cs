using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace ServiceDesk
{
    public class RequestHandler
    {
        private readonly Desk _desk;
        public bool result { get; set; }
        public string message { get; set; }

        public RequestHandler()
        {
            _desk = new Desk();
            result = true;
            message = "";
        }

        private bool send_data(string name, string email, string title, string content)
        {
            _desk.request = new RequestModel(name,email,title,content);
            return _desk.save_data();
        }
        public void form_handler(string name, string email, string title, string content)
        {
            result = true;
            message = "";
            if (name == "")
            {
                result = false;
                message += "Имя заполнено некорректно.\n";
            }
            if (!isValid(email))
            {
                result = false;
                message += "Почта указана некорректно.\n";
            }
            if (title == "")
            {
                result = false;
                message += "Тема запроса заполнена некорректно.\n";
            }
            if (content.Length < 10)
            {
                result = false;
                message += "Описание запроса должно содержать больше 10 символов.\n";
            }
            if (result)
            {
                result = send_data(name, email, title, content);
                if (result)
                {
                    message += "Запрос был обработан и направлен на рассмотрение.";
                }
                else
                {
                    message += "Произошла ошибка - запрос не был отправлен.";
                }
            }
            else
            {
                message += "Повторите попытку.";
            }
        }
        public DataTable return_result(string email)
        {
            result = true;
            message = "";
            if (!isValid(email))
            {
                result = false;
                message += "Почта указана некорректно.\n";
            }
            if (result)
            {
                DataTable data = _desk.get_data(email);
                return data;
            }
            else
            {
                message += "Повторите попытку.";
            }
            return null;
        }
        public static bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email.ToLower(), pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
