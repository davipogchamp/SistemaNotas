﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNotas
{
    public class Conexao
    {
        public SqlConnection conn = new("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SistemaNotas;Data Source=DESKTOP-MLFG6H8\\SQLEXPRESS");

        public void Conectar()
        {
            conn.Open();
        }

        public void Desconectar()
        {
            conn.Close();
        }
    }
}
   
