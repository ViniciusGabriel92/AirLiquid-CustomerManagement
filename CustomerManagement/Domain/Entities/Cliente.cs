using System;

namespace CustomerManagement.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}