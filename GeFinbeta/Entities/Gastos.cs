using GeFinbeta.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeFinbeta.Entities
{
    internal class Gastos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public Categoria categoria { get; set; }

        public Gastos() { }
        public Gastos(string descricao, double valor, DateTime data, Categoria categoria, int id)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            this.categoria = categoria;
            Id = id;
        }

        public override string ToString()
        {
            return $"Descrição: {Descricao}, Valor: {Valor}, Data: {Data.ToShortDateString()}, Categoria: {categoria}";
        }

        

        
       

    }
}
