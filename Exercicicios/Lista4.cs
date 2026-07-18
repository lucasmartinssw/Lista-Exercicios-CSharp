namespace ExerciciosCS.Exercicios
{
    public class Lista4
    {
        public static void Exercicio1()
        {
            Console.WriteLine("Digite 5 números inteiros:");
            int[] numeros = new int[5];
            for (int i = 0; i < numeros.Length; i++)
            {
                Console.Write($"Digite o {i + 1}º número: ");
                numeros[i] = int.Parse(Console.ReadLine());
            }

            int maior = numeros[0];
            int menor = numeros[0];
            
            for (int i = 1; i < numeros.Length; i++)
            {
                if (numeros[i] > maior)
                {
                    maior = numeros[i]; 
                }

                if (numeros[i] < menor)
                {
                    menor = numeros[i]; 
                }
            }

            Console.WriteLine($"\nMaior: {maior} | Menor: {menor}");
        }
    }
}