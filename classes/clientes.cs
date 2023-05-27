namespace classLibrary
{
    public class Cliente
    {
        public string nome;
        public string email;
        public string password;
        public Cliente() { }
        public Cliente(string nome, string email, string password)
        {
            this.nome = nome;
            this.email = email;
            this.password = password;
        }
        public virtual double pagar(int quantidade, double preco_rest)
        {
            return (preco_rest * quantidade);
        }
    }



    public class ClienteVip : Cliente
    {
        private double taxa_desconto;
        public ClienteVip() { }
        public ClienteVip(string nome, string email, string password)

        {
            this.nome = nome;
            this.email = email;
            this.password = password;
            taxa_desconto = 0.8;
        }

        public override double pagar(int quantidade, double preco_rest)
        {
            return preco_rest * quantidade * taxa_desconto;

        }
    }

}

