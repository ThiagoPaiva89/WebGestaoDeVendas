using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WebGestaoDeVendas.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Campo Obrigatório")]
        [StringLength(60 , MinimumLength = 3, ErrorMessage = "{0 } deve possuir no mínimo {2} e no máximo {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Campo Obrigatório")]
        [EmailAddress(ErrorMessage = "Entre com um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Campo Obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} Campo Obrigatório")]
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double SalarioBase { get; set; }

        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
        public ICollection<RegistroDeVendas> Vendas { get; set; } = new List<RegistroDeVendas>();

        public Vendedor()
        {

        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AddVendas(RegistroDeVendas rv) // rv = registro de vendas
        {
            Vendas.Add(rv);
        }

        public void RemoveVendas(RegistroDeVendas rv) // rv = registro de vendas
        {
            Vendas.Remove(rv);
        }

        public double TotalDeVendas(DateTime inicial, DateTime final)
        {
            return Vendas.Where(rv => rv.Data >= inicial && rv.Data <= final).Sum(rv => rv.Valor); // rv = registro de vendas
        }


    }
}
