namespace AvanadeEstacionamento.Domain.Models
{
    public abstract class BaseModel
    {
        protected BaseModel()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            IsAtivo = true;
        }

        public Guid Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAlteracao { get; set; }

        public bool IsAtivo { get; set; }
    }
}
