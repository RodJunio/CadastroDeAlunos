using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace mvc.Models
{
    public partial class Alunos
    {
        #region Propiedades
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Matricula {get; set; }

        private List<double> notas;
        public List<double> Nota 
        {
        get
        {
            if(notas == null) this.notas = new List<double>();
            return this.notas;           
        } 
        set
        {
            this.notas = value;
        }
        }
        #endregion
        public string STRNotas()
        {
            return string.Join(", ", this.Nota.ToArray());
        }
    
    }         
}