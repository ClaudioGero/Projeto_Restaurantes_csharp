using System;
using System.Collections.Generic;

namespace classLibrary
{
    public class Control
    {
        private List<Restaurante> restaurantes = new List<Restaurante>();
        private List<Cliente> clientes = new List<Cliente>();
        private List<ClienteVip> clientesVip = new List<ClienteVip>();
        private Cliente cliente_ativo = null;

        public Control()
        {
            Console.WriteLine("Bem vindo!\n");

            //Criando alguns restaurantes e clientes para testes
            Cliente c1 = new Cliente("Claudio", "claudio@gmail.com", "Claudio123");
            Cliente c2 = new Cliente("Pedro", "pedro@gmail.com", "Pedro123");
            Cliente c3 = new Cliente("Maria", "maria@gmail.com", "Maria123");
            Restaurante r1 = new Restaurante();
            Restaurante r2 = new Restaurante();
            
            r1.SetCozinha("mexicana");
            r1.SetNome("Guacamole");
            r1.Preco_medio = 100;
            r2.SetCozinha("mexicana");
            r2.SetNome("Guakito");
            r2.Preco_medio = 150;
            clientes.Add(c1);
            clientes.Add(c2);
            clientes.Add(c3);
            restaurantes.Add(r1);
            restaurantes.Add(r2);
            
        }

        public void Menu()
        {
            Console.WriteLine("\n_________________________________________________\nInserir funcionalidade desejada.\n1 - cadastrar cliente\n2 - pesquisar\n3 - add restaurante \n4 - login\n5 - deslogar");

            try
            {
                int opcao = Convert.ToInt32(Console.ReadLine());

                if (opcao == 1)
                {
                    AddCliente();
                }
                else if (opcao == 2)
                {
                    Pesquisar();
                }
                else if (opcao == 3)
                {
                    Restaurante restaurante_add = AddRestaurante();
                    restaurantes.Add(restaurante_add);
                }
                else if (opcao == 4)
                {
                    if (cliente_ativo == null)
                        Login();
                    else
                        Console.WriteLine("Um cliente já está logado.");
                }
                else if (opcao == 5)
                {
                    Deslogar();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Insira um número válido.");
            }
            finally
            {
                Menu();
            }
        }

        public void AddCliente()
        {
            Console.WriteLine("\nNome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("\nEmail: ");
            string email = Console.ReadLine();
            Console.WriteLine("\nPassword: ");
            string password = Console.ReadLine();
            Console.WriteLine("\nDeseja criar uma conta gratuita comum ou pagar pelos serviços do cliente vip?\nDigite 1 para comum\nDigite 2 para vip");

            int opcao;
            while (!int.TryParse(Console.ReadLine(), out opcao) || (opcao != 1 && opcao != 2))
            {
                Console.WriteLine("Opção inválida. Digite novamente: ");
            }

            if (opcao == 1)
            {
                Cliente cliente_adicionado = new Cliente(nome, email, password);
                clientes.Add(cliente_adicionado);
                Menu();
            }
            else if (opcao == 2)
            {
                ClienteVip cliente_adicionado = new ClienteVip(nome, email, password);
                clientesVip.Add(cliente_adicionado);
                Menu();
            }
        }

        public void Login()
        {
            if (cliente_ativo != null)
            {
                Deslogar();
            }

            Console.WriteLine("Email de login: ");
            string email = Console.ReadLine();
            foreach (Cliente cliente in clientes)
            {
                if (cliente.email == email)
                {
                    Console.WriteLine("Senha: ");
                    string password = Console.ReadLine();
                    if (cliente.password == password)
                    {
                        Console.WriteLine($"\nLogin de {cliente.nome} concluído!");
                        cliente_ativo = cliente;
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta.");
                    }
                    return;
                }
            }

            foreach (ClienteVip clienteVip in clientesVip)
            {
                if (clienteVip.email == email)
                {
                    Console.WriteLine("Senha: ");
                    string password = Console.ReadLine();
                    if (clienteVip.password == password)
                    {
                        Console.WriteLine($"\nLogin de {clienteVip.nome} concluído!");
                        cliente_ativo = clienteVip;
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta.");
                    }
                    return;
                }
            }

            Console.WriteLine("Cliente não encontrado.");
        }

        public void Deslogar()
        {
            Console.WriteLine($"{cliente_ativo.nome} foi deslogado!");
            cliente_ativo = null;
        }

        public Restaurante AddRestaurante()
        {
            Restaurante newRestaurante = new Restaurante();
            Console.WriteLine("Cozinha: ");
            newRestaurante.SetCozinha(Console.ReadLine());
            Console.WriteLine("Nome do restaurante: ");
            newRestaurante.SetNome(Console.ReadLine());
            Console.WriteLine("Preço médio do prato: ");
            try
            {
                newRestaurante.Preco_medio = Convert.ToDouble(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Insira um número válido.");
            }
            finally
            {
                newRestaurante.Preco_medio = Convert.ToDouble(Console.ReadLine());

            }
            Console.WriteLine($"{newRestaurante.GetNome().ToLower()} foi adicionado!");

            return newRestaurante;
        }

        public void Pesquisar()
        {
            List<Restaurante> restaurantes_encontrados = new List<Restaurante>();

            Console.WriteLine("Pesquisa: ");
            string pesquisa = Console.ReadLine().ToUpper();

            foreach (Restaurante restaurante in restaurantes)
            {
                if (restaurante.GetNome().Contains(pesquisa))
                {
                    restaurantes_encontrados.Add(restaurante);
                }
            }

            int contador = 1;
            foreach (Restaurante restauranteEncontrado in restaurantes_encontrados)
            {
                Console.WriteLine($"({contador}) {restauranteEncontrado.GetNome()} encontrado!\n");
                contador++;
            }

            if (restaurantes_encontrados.Count > 0)
            {
                Console.WriteLine("Qual restaurante selecionar? Insira o número correspondente: ");

                int resposta;
                while (!int.TryParse(Console.ReadLine(), out resposta) || resposta < 1 || resposta > restaurantes_encontrados.Count)
                {
                    Console.WriteLine("Número inválido. Digite novamente: ");
                }

                Restaurante restauranteEscolhido = restaurantes_encontrados[resposta - 1];

                Console.WriteLine("Qual o número de lanches? ");
                int quantidade;
                while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
                {
                    Console.WriteLine("Número inválido. Digite novamente: ");
                }

                if (cliente_ativo != null)
                {
                    double preco_final = cliente_ativo.pagar(quantidade, restauranteEscolhido.Preco_medio);
                    Console.WriteLine($"{preco_final} foi descontado da conta do {cliente_ativo.nome}");
                }
                else
                {
                    Login();
                    double preco_final = cliente_ativo.pagar(quantidade, restauranteEscolhido.Preco_medio);
                    Console.WriteLine($"{preco_final} foi descontado da conta do {cliente_ativo.nome}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum restaurante encontrado.");
            }
        }
    }
}