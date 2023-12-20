int n = 0;
int qtdBombas = 0;
int qtdJogadas = 0;
string? readAnswer;

do
{
Console.WriteLine("Defina o tamanho 'n' da matriz nxn do campo minado (Mínimo 3)");
readAnswer = Console.ReadLine();
if (readAnswer != null)
n = int.Parse(readAnswer);
} while (n < 3);

do
{
Console.WriteLine($"Escolha a quantidade de bombas (Mínimo 5 e limite de {n*n - 1} bombas)");
readAnswer = Console.ReadLine();
if (readAnswer != null)
qtdBombas = int.Parse(readAnswer);
} while (qtdBombas < 5);

int maxJogadas = n*n*3/4;
Console.WriteLine($"Você tem até {maxJogadas} jogadas até achar a bandeira!");
Console.WriteLine("Pressione enter para continuar!");
Console.ReadLine();

int[,] campo = new int[n, n]; //matriz com posições dos elementos do campo
int[,] jogo = new int[n, n]; //matriz que registra ações do jogador

int qtdLinhas = campo.GetLength(0);
int qtdColunas = campo.GetLength(1);

for (int l = 0; l < qtdLinhas; l++)
{
    for (int c = 0; c < qtdColunas; c++)
    {
        campo[l, c] = 0;
        jogo[l, c] = -1;
    }
}

//Posicionamento aleatório da bandeira
Random gerador = new Random();
int linha = gerador.Next(qtdLinhas);
int coluna = gerador.Next(qtdColunas);
campo[linha, coluna] = 2;

//Posicionamento aleatório das bombas
int bombasPosicionadas = 0;
do
{
    linha = gerador.Next(qtdLinhas);
    coluna = gerador.Next(qtdColunas);
    if (campo[linha,coluna] == 0)
    {
        campo[linha, coluna] = 1;
        bombasPosicionadas++;
    }

} while (bombasPosicionadas < qtdBombas);

bool fimJogo = false;

do
{
    for (int l = 0; l < qtdLinhas; l++)
    {
        for (int c = 0; c < qtdColunas; c++)
        {
            Console.Write($"{jogo[l, c]} ");
        }
        Console.WriteLine();
    }

    do
    {
    Console.WriteLine($"Selecione uma linha [1-{n}]: ");
    linha = Convert.ToInt32(Console.ReadLine()) - 1;
    Console.WriteLine($"Selecione uma colunha [1-{n}]");
    coluna = Convert.ToInt32(Console.ReadLine()) - 1;
    } while (linha < 1 || coluna < 1 || linha > n || coluna > n);
    //Pesquisar diferença entre usar Convert e Parse

    switch (campo[linha, coluna])
    {
        case 0:
        jogo[linha, coluna] = 0;
        if (qtdJogadas > maxJogadas)
        {
            Console.WriteLine("Você perdeu! Ultrapassou o limite de jogadas.");
            break;
        }
        Console.WriteLine("Continue tentando.\n");
        break;

        case 1:
        jogo[linha, coluna] = 1;
        Console.WriteLine("BOOM. Você perdeu.\n");
        fimJogo = true;
        break;

        case 2:
        jogo[linha, coluna] = 2;
        Console.WriteLine("Parabéns, você ganhou!\n");
        fimJogo = true;
        break;
    }

} while (!fimJogo);