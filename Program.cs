using ExerciciosCS.Exercicios;

string op;

do
{
    Console.WriteLine("\nDigite qual programa você deseja visualizar (Para sair digite 0):");
    op = Console.ReadLine(); 

    switch (op)
    {
        case "0":
            Console.WriteLine("Encerrando o programa...");
            break;

        case "1":
            ExercicioFintech.Executar();
            break;

        case "2":
            CarrinhoDeCompras.Executar();
            break;

        case "3":
        Lista4.Exercicio1();
        break;

        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            break;
    }

} while (op != "0");