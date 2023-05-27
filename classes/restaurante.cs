
namespace classLibrary
{
    public class Restaurante
    {
        private string cozinha;
        private string nome;
        private double preco_medio;
        public double Preco_medio { get; set; }

        public void SetCozinha(string value) { cozinha = value; }
        public string GetCozinha() { return cozinha.ToUpper(); }
        public void SetNome(string value) { nome = value; }
        public string GetNome() { return nome.ToUpper(); }

    }

}