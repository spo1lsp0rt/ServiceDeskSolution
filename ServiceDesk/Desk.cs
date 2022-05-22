using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ServiceDesk
{
    public class Desk
    {
        public RequestModel request { get; set; }

        public bool save_data()
        {
            if (request == null)
            {
                return false;
            }

            NpgsqlConnection con = new NpgsqlConnection("Server=localhost;Port=5432;Database=ServiceDesk;User Id=postgres;Password=sa;");
            con.Open();
            NpgsqlCommand Comanda = new NpgsqlCommand("insert into request(name, email, title, content) values (@name, @email, @title, @content);", con);
            Comanda.CommandType = System.Data.CommandType.Text;
            Comanda.Parameters.Clear();
            Comanda.Parameters.AddWithValue("name", request.name);
            Comanda.Parameters.AddWithValue("email", request.email);
            Comanda.Parameters.AddWithValue("title", request.title);
            Comanda.Parameters.AddWithValue("content", request.content);
            Comanda.ExecuteScalar();
            con.Close();
            return true;
        }
        public DataTable get_data(string email)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server=localhost;Port=5432;Database=ServiceDesk;User Id=postgres;Password=sa;");
            con.Open();
            NpgsqlCommand Comanda = new NpgsqlCommand("select title, content, (select name from status where id = request.status), answer from request where email = @email;", con);
            Comanda.CommandType = System.Data.CommandType.Text;
            Comanda.Parameters.Clear();
            Comanda.Parameters.AddWithValue("email", email);
            DataTable DT = new DataTable();
            DT.Columns.Add("Тема");
            DT.Columns.Add("Содержание");
            DT.Columns.Add("Статус");
            DT.Columns.Add("Ответ");
            NpgsqlDataReader Reader = Comanda.ExecuteReader();
            while (Reader.Read())
            {
                DT.Rows.Add(Reader.GetValue(0).ToString(), Reader.GetValue(1).ToString(), Reader.GetValue(2).ToString(), Reader.GetValue(3).ToString());
            }
            Reader.Close();
            con.Close();
            return DT;
        }
    }
}
