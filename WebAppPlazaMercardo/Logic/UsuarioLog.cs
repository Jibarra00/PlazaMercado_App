﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using Model;

namespace Logic
{
    public class UsuarioLog
    {
        UsuarioDat objUsu = new UsuarioDat();

        public DataSet showUsuario()
        {
            // Se devuelve el DataSet que contiene de los usuarios.
            return objUsu.showUsuario();
        }
        public User showUsersMail(string mail)
        {
            return objUsu.showUserEmail(mail);
        }
        public bool saveUsuario(string _email, string _password, string _salt, string _state, DateTime _Create_Date, int _fkrol, int _fkempleado)
        {
           
            return objUsu.saveUsuario(_email, _password, _salt, _state, _Create_Date, _fkrol, _fkempleado);
        }
        public bool updateUsuario(int _id, string _email, string _password, string _salt, string _state, DateTime _Create_Date, int _fkrol, int _fkempleado)
        {
            
            return objUsu.updateUsuario(_id,_email, _password, _salt, _state, _Create_Date, _fkrol, _fkempleado);
        }
        public bool deleteUsuario(int _idUsuario)
        {
            return objUsu.deleteUsuario(_idUsuario);

        }
    }
}