namespace ExerciciosCS.Exercicios
{
    // 1. O Catálogo: Responsável apenas por manter os dados imutáveis do produto.
    public class Produto
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public Produto(string codigo, string descricao, decimal valorUnitario)
        {
            Codigo = codigo;
            Descricao = descricao;
            ValorUnitario = valorUnitario;
        }
    }

    // 2. O Item: Responsável por conectar um Produto a uma Quantidade e calcular seu subtotal.
    public class ItemCarrinho
    {
        // Composição: O item guarda a referência do objeto Produto inteiro, não apenas o código.
        public Produto Produto { get; private set; } 
        public int Quantidade { get; private set; }

        public ItemCarrinho(Produto produto, int quantidade = 1)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public void AdicionarQuantidade(int quantidade = 1)
        {
            Quantidade += quantidade;
        }

        public decimal CalcularSubtotal()
        {
            return Produto.ValorUnitario * Quantidade;
        }
    }

    // 3. O Carrinho: Responsável por orquestrar a lista de itens e aplicar as regras de negócio.
    public class CarrinhoDeCompras
    {
        // A lista de itens pertence ao carrinho e é privada para que ninguém de fora a manipule diretamente.
        private List<ItemCarrinho> _itens;

        public CarrinhoDeCompras()
        {
            _itens = new List<ItemCarrinho>();
        }

        public void AdicionarProduto(Produto produto)
        {
            // Utilizando LINQ para verificar se o produto já existe na lista
            var itemExistente = _itens.FirstOrDefault(i => i.Produto.Codigo == produto.Codigo);

            if (itemExistente != null)
            {
                // Regra de negócio: se já existe, apenas altera a quantidade
                itemExistente.AdicionarQuantidade();
            }
            else
            {
                // Se não existe, cria um novo item e adiciona à lista
                _itens.Add(new ItemCarrinho(produto));
            }
        }

        public void VerMeuCarrinho()
        {
            if (!_itens.Any())
            {
                Console.WriteLine("O carrinho está vazio.");
                return;
            }

            Console.WriteLine("\n--- Estado Atual do Carrinho ---");
            foreach (var item in _itens)
            {
                Console.WriteLine($"{item.Quantidade}x {item.Produto.Descricao} - Subtotal: R${item.CalcularSubtotal():F2}");
            }
        }

        public void LimparCarrinho()
        {
            _itens.Clear();
            Console.WriteLine("\nCarrinho limpo com sucesso.");
        }

        public void FinalizarCompra()
        {
            if (!_itens.Any())
            {
                Console.WriteLine("\nNão é possível finalizar a compra. O carrinho está vazio.");
                return;
            }

            // Calcula o valor total somando os subtotais de cada item
            decimal valorTotal = _itens.Sum(i => i.CalcularSubtotal());
            
            Console.WriteLine($"\n=== COMPRA FINALIZADA ===");
            Console.WriteLine($"Valor total a pagar: R${valorTotal:F2}");
            
            // Opcional: Esvaziar o carrinho após finalizar
            _itens.Clear(); 
        }

        // Método estático para simular a execução do sistema
        public static void Executar()
        {
            // 1. Carga inicial dos produtos (Catálogo)
            var p1 = new Produto("1", "Camiseta P", 40.00m); // O sufixo 'm' indica que o valor é decimal
            var p2 = new Produto("2", "Camiseta M", 45.00m);
            var p3 = new Produto("3", "Camiseta G", 50.00m);
            var p4 = new Produto("4", "Camiseta GG", 55.00m);

            var carrinho = new CarrinhoDeCompras();

            // 2. Adicionando produtos (Testando a regra de não repetição)
            carrinho.AdicionarProduto(p1);
            carrinho.AdicionarProduto(p2);
            carrinho.AdicionarProduto(p1); // Deve apenas incrementar a quantidade da Camiseta P para 2

            // 3. Visualizando o estado atual
            carrinho.VerMeuCarrinho();

            // 4. Finalizando a compra
            carrinho.FinalizarCompra();
        }
    }
}