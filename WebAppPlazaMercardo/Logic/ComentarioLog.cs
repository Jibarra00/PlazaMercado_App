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
    public class ComentarioLog
    {
        ComentarioDat objCat = new ComentarioDat();
        //Metodo para mostrar todas los comentarios
        public DataSet showComment()
        {
            return objCat.showComment();
        }
        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showComentDDL()
        {
            return objCat.showCommentDDL();
        }
        //Metodo para guardar un nuevo comentario
        public bool saveComment(string p_com_text, DateTime p_com_fecha, int p_com_clasificacion)
        {
            return objCat.saveComment(p_com_text, p_com_fecha, p_com_clasificacion);
        }
        //Metodo para actualizar un comentario
        public bool updateComment(int p_com_id, int p_com_clasificacion, string p_com_text, DateTime p_com_fecha)
        {
            return objCat.updateComment(p_com_id, p_com_clasificacion, p_com_text, p_com_fecha);
        }
        //Metodo para borrar un comentario
        public bool deleteComment(int p_com_id)
        {
            return objCat.deleteComment(p_com_id);
        }
    }
}