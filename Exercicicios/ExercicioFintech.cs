namespace ExerciciosCS.Exercicios
{
    public class ExercicioFintech
    {
        public static void Executar()
        {
            // Instanciação das contas base para simulação do sistema
            ContaBancaria contaJoao = new ContaBancaria("João", 0.00m);
            ContaBancaria contaMaria = new ContaBancaria("Maria", 1000.00m);

            Console.WriteLine("Bem-vindo ao sistema Fintech!");
            
            // Variável para armazenar a escolha do usuário no menu
            string opcao = ""; 

            // Laço de repetição que mantém o menu ativo até o usuário escolher sair (opção "0")
            do
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1 - Depositar na conta do João");
                Console.WriteLine("2 - Fazer Pix do João para a Maria (R$ 30,00)");
                Console.WriteLine("3 - Exibir Extrato do João");
                Console.WriteLine("4 - Exibir Extrato da Maria");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                
                // Captura a entrada de texto do usuário
                opcao = Console.ReadLine(); 

                // Direciona o fluxo do programa com base na opção digitada
                switch (opcao)
                {
                    case "1":
                        contaJoao.Depositar(50m);
                        Console.WriteLine("Depósito de R$ 50,00 realizado com sucesso!");
                        break;
                    case "2":
                        // Executa a transferência retirando do João e enviando para a Maria
                        contaJoao.RealizarPix(30m, contaMaria);
                        Console.WriteLine("Operação de Pix finalizada.");
                        break;
                    case "3":
                        contaJoao.ExibirExtrato();
                        break;
                    case "4":
                        contaMaria.ExibirExtrato();
                        break;
                    case "0":
                        Console.WriteLine("Encerrando o sistema...");
                        break;
                    default:
                        // Tratamento para caso o usuário digite um valor fora das opções esperadas
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

            } while (opcao != "0"); 
        }
    }

    public class ContaBancaria
    {
        public string Titular { get; set; }
        
        // O set é privado para garantir que o saldo só seja alterado por métodos da própria classe
        public decimal Saldo { get; private set; }
        
        // Lista que armazena o histórico de operações da conta
        public List<Operacao> Extrato { get; private set; }

        public ContaBancaria(string titular, decimal saldoInicial)
        { 
            Titular = titular;
            Saldo = saldoInicial;
            // Inicializa a lista vazia para evitar erro de referência nula (NullReferenceException)
            Extrato = new List<Operacao>(); 
        }

        public void Depositar(decimal valor)
        {
            Saldo += valor;
            Extrato.Add(new Operacao(valor, "Depósito"));
        }

        public void RealizarPix(decimal valorPix, ContaBancaria contaDestino)
        {
            // Valida se a conta possui saldo suficiente para a transação
            if (valorPix <= Saldo)
            {
                Saldo -= valorPix;
                Extrato.Add(new Operacao(valorPix, "Pix Enviado")); 
                
                // Chama o método da conta de destino para registrar a entrada do dinheiro
                contaDestino.Depositar(valorPix); 
            }
            else
            {
                Console.WriteLine("Erro: Saldo insuficiente para realizar o Pix.");
            }
        }

        public void ExibirExtrato()
        {
            Console.WriteLine($"\n--- Extrato da conta de {Titular} ---");
            
            // Percorre cada operação registrada na lista
            foreach (Operacao op in Extrato)
            {
                Console.WriteLine($"{op.TipoTransacao}: R$ {op.ValorTransacao}");
            }
            
            Console.WriteLine($"Saldo atual: R$ {Saldo}");
            Console.WriteLine("-----------------------------------\n");
        }
    }

    public class Operacao
    {
        public decimal ValorTransacao { get; private set; }
        public string TipoTransacao { get; private set; }

        public Operacao(decimal valorTransacao, string tipoTransacao)
        {
            ValorTransacao = valorTransacao;
            TipoTransacao = tipoTransacao;
        }
    }
}