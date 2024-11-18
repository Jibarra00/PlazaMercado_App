using System;
using Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;

namespace Logic
{
    public class PedidosLog
    {
        PedidosDat objPed = new PedidosDat();

        public DataSet showPedidos()
        {

            return objPed.showPedidos();
        }
        public bool savePedidos(DateTime _date, string _state, string _specification, int _fkCliente, int _fkproducto)
        {
            return objPed.savePedidos(_date, _state, _specification, _fkCliente, _fkproducto);
        }
        public bool updatePedidos(int _id, DateTime _date, string _state, string _specification, int _fkCliente, int _fkproducto)
        {
            return objPed.updatePedidos(_id, _date, _state, _specification, _fkCliente, _fkproducto);
        }
        public bool DeletePedidos(int _idPedidos)
        {
            return objPed.spDeletePedidos(_idPedidos);
        }
    }
}