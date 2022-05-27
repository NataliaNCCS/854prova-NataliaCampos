

Dictionary<char, int> linhas = new Dictionary<char, int>();
linhas.Add('A', 0);
linhas.Add('B', 1);
linhas.Add('C', 2);
linhas.Add('D', 3);
linhas.Add('E', 4);
linhas.Add('F', 5);
linhas.Add('G', 6);
linhas.Add('H', 7);
linhas.Add('I', 8);
linhas.Add('J', 9);

int[] colunas = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};


Dictionary<string, int> embarcacoesQuad = new Dictionary<string, int>();
embarcacoesQuad.Add("PS", 5);
embarcacoesQuad.Add("NT", 4);
embarcacoesQuad.Add("DS", 3);
embarcacoesQuad.Add("SB", 2);

string[] linhasEmLetras = new string[] {" ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

string[,] quadroJog1 = new string[10, 10];
string[,] quadroJog2 = new string[10, 10];

List<string> tirosJog1 = new List<string>();
List<string> tirosJog2 = new List<string>();

novoJogo:
Console.WriteLine("-------------------- BATALHA NAVAL --------------------");

Console.WriteLine(@"
    SELECIONE A OPÇÃO DESEJADA:
        1. Jogar contra um oponente real
        2. Jogar contra o computador");

int opcaoEscolhida;
bool intBool = int.TryParse(Console.ReadLine(), out opcaoEscolhida);
while (!intBool || (opcaoEscolhida != 1 && opcaoEscolhida != 2))
{
    Console.Write("Opção inválida, tente novamente: ");
    intBool = int.TryParse(Console.ReadLine(), out opcaoEscolhida);
}

#region variaveis
string jogador1;
string jogador2;
string embarcacao = " ";
bool quadranteOcupado = false;
string posicao = " ";
char linhaInicial = ' ';
char linhaFinal = ' ';
int colunaInicial = 0;
int colunaFinal = 0;
bool intBoolCI = true;
bool intBoolCF = true;
bool charBoolLI = true;
bool charBoolLF = true;
int quadrantes = 0;
int psFaltam = 1;
int ntFaltam = 2;
int dsFaltam = 3;
int sbFaltam = 4;
string[,] quadroJog1JOGO = new string[11, 21];
string[,] quadroJog2JOGO = new string[11, 21];
string[,] quadroJog1POSICIONAMENTO = new string[11, 21];
string[,] quadroJog2POSICIONAMENTO = new string[11, 21];
string posicaoTiroStr;
string linTiroStr = "1";
string colTiroStr = "1";
int linTiroInt = 0;
int colTiroInt = 0;
int[,] posicaoTiroArr = new int[10, 10];
int acertosJog1 = 0;
int acertosJog2 = 0;
int novoJogo = 0;
string posicaoInvalida = "Posição inválida, tente novamente: ";
string jaPosicionou = "Você já posicionou todas as embarcações desse tipo. Tente outra embarcação: ";
bool posicaoTiroValida = true;
bool quadranteValido = true;
#endregion

if (opcaoEscolhida == 1) // contra oponente real
{
    OponenteReal();
}
else // contra computador
{
    Console.WriteLine(@"Ainda não foi implementado.
Pressione qualquer tecla para voltar ao menu principal.");
    Console.ReadKey();
    Console.Clear();
    goto novoJogo;
    
}

Console.WriteLine("Novo jogo? [1.Sim 2.Não]");
intBool = int.TryParse(Console.ReadLine(), out novoJogo);
while(!intBool || (novoJogo != 1 && novoJogo != 2))
{
    Console.Write("Opção inválida, tente novamente:");
    intBool = int.TryParse(Console.ReadLine(), out novoJogo);
}

if (novoJogo == 1)
{
    Console.Clear();
    goto novoJogo;
}
else
    return;

// ------------------------------------------------------------ MÉTODOS ------------------------------------------------------------

void OponenteReal()
{
    tirosJog1.Clear();
    tirosJog2.Clear();

    Console.Write("Informe o nome do PRIMEIRO jogador: ");
    jogador1 = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(jogador1))
    {
        Console.Write(@"Nome inválido, tente novamente.
Informe o nome do PRIMEIRO jogador: ");
        jogador1 = Console.ReadLine();
    }

    Console.Write("Informe o nome do SEGUNDO jogador: ");
    jogador2 = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(jogador2))
    {
        Console.Write(@"Nome inválido, tente novamente. 
Informe o nome do SEGUNDO jogador: ");
        jogador2 = Console.ReadLine();
    }

    int contColunas = 1;

    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j <= 20; j++)
        {

            if (i == 0 && j % 2 == 0 && j > 0)
            {
                quadroJog1JOGO[i, j] = $"{contColunas.ToString()}";
                quadroJog2JOGO[i, j] = $"{contColunas.ToString()}";
                quadroJog1POSICIONAMENTO[i, j] = $"{contColunas.ToString()}";
                quadroJog2POSICIONAMENTO[i, j] = $"{contColunas.ToString()}";
                contColunas++;
            }
            else
            {
                quadroJog1JOGO[i, j] = " ";
                quadroJog2JOGO[i, j] = " ";
                quadroJog1POSICIONAMENTO[i, j] = " ";
                quadroJog2POSICIONAMENTO[i, j] = " ";
            }

            if (j == 0)
            {
                quadroJog1JOGO[i, j] = linhasEmLetras[i];
                quadroJog2JOGO[i, j] = linhasEmLetras[i];
                quadroJog1POSICIONAMENTO[i, j] = linhasEmLetras[i];
                quadroJog2POSICIONAMENTO[i, j] = linhasEmLetras[i];
            }
        }

    }

    Console.Clear();


    //--------------------------------------------------------------- JOGADOR 1 (UM) ---------------------------------------------------------------


    Console.WriteLine(@$"{jogador1}, vamos posicionar suas embarcações.
Você tem que posicionar:
     QTD | SIGLA |   EMBARCAÇÃO  | TAMANHO

      1  |   PS  |  Porta-Aviões | 5 quadrantes
      2  |   NT  | Navios-Tanque | 4 quadrantes
      3  |   DS  |   Destroyers  | 3 quadrantes
      4  |   SB  |   Submarinos  | 2 quadrantes

    Exemplo de input:
    Sigla: NT
    Posição: A1A4
");

   

    for (int i = 0; i < 10; i++)
    {

        if(i != 0)
            EmbarcFaltantes();

        MostrarQuadroPosicionamentoJog1();

        Console.WriteLine($"Qual a sigla da {i + 1}ª embarcação?");
        embarcacao = Console.ReadLine().ToUpper();

        EmbarcacoesJaPosicionadas();      


        while (!embarcacoesQuad.ContainsKey(embarcacao) || string.IsNullOrWhiteSpace(embarcacao))
        {
            Console.WriteLine("Embarcação inválida, tente novamente:");
            embarcacao = Console.ReadLine().ToUpper();
        }



        Console.WriteLine("Qual sua posição? (insira posição inicial e final)");
        CheckPosicaoJog1();

        while (!intBoolCI || !intBoolCF || !charBoolLI || !charBoolLF || quadrantes != embarcacoesQuad[embarcacao])
        {
            Console.Write(posicaoInvalida);
            CheckPosicaoJog1();
        }

        Console.Clear();
    }
    //-------------------------------------------------------------- JOGADOR 2 (DOIS) --------------------------------------------------------------

    psFaltam = 1;
    ntFaltam = 2;
    dsFaltam = 3;
    sbFaltam = 4;

    Console.Clear();

    Console.WriteLine(@$"{jogador2}, vamos posicionar suas embarcações.
Você tem que posicionar:
    - 1 PS - Porta-Aviões (Ocupa 5 quadrantes)
    - 2 NT - Navios-Tanque (Ocupa 4 quadrantes)
    - 3 DS - Destroyers (Ocupa 3 quadrantes)
    - 4 SB - Submarinos (Ocupa 2 quadrantes)");

    Console.WriteLine("\n\n");

    for (int i = 0; i < 10; i++)
    {

        if (i != 0)
            EmbarcFaltantes();

        MostrarQuadroPosicionamentoJog2();

        Console.WriteLine($"Qual o tipo da {i + 1}ª embarcação?");
        embarcacao = Console.ReadLine().ToUpper();

        EmbarcacoesJaPosicionadas();


        while (!embarcacoesQuad.ContainsKey(embarcacao) || string.IsNullOrWhiteSpace(embarcacao))
        {
            Console.WriteLine("Embarcação inválida, tente novamente:");
            embarcacao = Console.ReadLine().ToUpper();
        }



        Console.WriteLine("Qual sua posição? (insira posição inicial e final)");
        CheckPosicaoJog2();

        while (!intBoolCI || !intBoolCF || !charBoolLI || !charBoolLF || quadrantes != embarcacoesQuad[embarcacao])
        {
            Console.Write(posicaoInvalida);
            CheckPosicaoJog2();
        }

        Console.Clear();
    }

    Console.WriteLine("HORA DO JOGO!!");

    while (acertosJog1 < 30 && acertosJog2 < 30)
        Rodada();

    Ganhador();


}

void MostrarQuadroPosicionamentoJog1()
{
    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j < 21; j++)
        {
            Console.Write(quadroJog1POSICIONAMENTO[i, j]);
        }
        Console.WriteLine();
    }
}

void MostrarQuadroPosicionamentoJog2()
{
    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j < 21; j++)
        {
            Console.Write(quadroJog2POSICIONAMENTO[i, j]);
        }
        Console.WriteLine();
    }
}

void EmbarcacoesJaPosicionadas()
{
    while ((embarcacao == "PS" && psFaltam == 0)
            || (embarcacao == "NT" && ntFaltam == 0)
            || (embarcacao == "DS" && dsFaltam == 0)
            || (embarcacao == "SB" && sbFaltam == 0))
    {
        Console.Write(jaPosicionou);
        embarcacao = Console.ReadLine().ToUpper();

    }
}

void PosicaoLinhasEColunas()
{
    while (string.IsNullOrWhiteSpace(posicao) || (posicao.Length != 4 && posicao.Length != 5 && posicao.Length != 6))
    {
        Console.WriteLine(posicaoInvalida);
        posicao = Console.ReadLine();
    }

    if (posicao.Length == 4) 
    {
        charBoolLI = char.TryParse(posicao.Substring(0, 1), out linhaInicial);
        intBoolCI = int.TryParse(posicao.Substring(1, 1), out colunaInicial);
        charBoolLF = char.TryParse(posicao.Substring(2, 1), out linhaFinal);
        intBoolCF = int.TryParse(posicao.Substring(3, 1), out colunaFinal);
    }
    else if (posicao.Length == 5)
    {
        if (int.TryParse(posicao.Substring(1, 2), out int inteiro)) 
        {
            charBoolLI = char.TryParse(posicao.Substring(0, 1), out linhaInicial);
            intBoolCI = int.TryParse(posicao.Substring(1, 2), out colunaInicial);
            charBoolLF = char.TryParse(posicao.Substring(3, 1), out linhaFinal);
            intBoolCF = int.TryParse(posicao.Substring(4, 1), out colunaFinal);
        }
        else 
        {
            charBoolLI = char.TryParse(posicao.Substring(0, 1), out linhaInicial);
            intBoolCI = int.TryParse(posicao.Substring(1, 1), out colunaInicial);
            charBoolLF = char.TryParse(posicao.Substring(2, 1), out linhaFinal);
            intBoolCF = int.TryParse(posicao.Substring(3, 2), out colunaFinal);
        }
    }
    else if (posicao.Length == 6)
    {
        charBoolLI = char.TryParse(posicao.Substring(0, 1), out linhaInicial);
        intBoolCI = int.TryParse(posicao.Substring(1, 2), out colunaInicial);
        charBoolLF = char.TryParse(posicao.Substring(3, 1), out linhaFinal);
        intBoolCF = int.TryParse(posicao.Substring(4, 2), out colunaFinal);
    }
}

void CheckPosicaoJog1()
{
    posicao = Console.ReadLine().ToUpper();
    PosicaoLinhasEColunas();

    var contemLinhaInicial = linhas.ContainsKey(linhaInicial);
    var contemLinhaFinal = linhas.ContainsKey(linhaFinal);
    var contemColunaInicial = Array.Exists(colunas, x => x == colunaInicial - 1);
    var contemColunaFinal = Array.Exists(colunas, x => x == colunaFinal - 1);


    if (!contemLinhaFinal || !contemLinhaFinal || !contemColunaInicial || !contemColunaFinal)
    {
        Console.WriteLine(posicaoInvalida);
        posicao = Console.ReadLine().ToUpper();
        PosicaoLinhasEColunas();
    }


    quadranteOcupado = false;

    colunaInicial--;
    colunaFinal--;

    QuadranteValidoJog1();

    SetEmbarcJog1();

}

void CheckPosicaoJog2()
{
    posicao = Console.ReadLine().ToUpper();
    PosicaoLinhasEColunas();

    var contemLinhaInicial = linhas.ContainsKey(linhaInicial);
    var contemLinhaFinal = linhas.ContainsKey(linhaFinal);
    var contemColunaInicial = Array.Exists(colunas, x => x == colunaInicial - 1);
    var contemColunaFinal = Array.Exists(colunas, x => x == colunaFinal - 1);


    if (!contemLinhaFinal || !contemLinhaFinal || !contemColunaInicial || !contemColunaFinal)
    {
        Console.WriteLine(posicaoInvalida);
        posicao = Console.ReadLine().ToUpper();
        PosicaoLinhasEColunas();
    }

    quadranteOcupado = false;

    colunaInicial--;
    colunaFinal--;

    QuadranteValidoJog2();

    SetEmbarcJog2();
}

void QuadranteValidoJog1()
{
    if (linhaInicial == linhaFinal) // HORIZONTAL
    {
        quadrantes = colunaFinal - colunaInicial + 1;
        while (quadrantes != embarcacoesQuad[embarcacao]) // verifica se a posição corresponde ao número de quadrantes da embarcação
        {
            Console.WriteLine("Número de quadrantes é inválido, tente novamente: ");
            posicao = Console.ReadLine().ToUpper();
            PosicaoLinhasEColunas();
        }

        for (int col = colunaInicial; col <= colunaFinal; col++) // percorrendo para ver se está livre
        {
            while (quadroJog1[linhas[linhaInicial], col] != null)
            {
                quadranteOcupado = true;
                Console.WriteLine("Um ou mais quadrantes dentre os inseridos estão ocupados, tente novamente:");
                posicao = Console.ReadLine().ToUpper();
                PosicaoLinhasEColunas();
            }
        }

    }
    else if (colunaInicial == colunaFinal) // VERTICAL
    {
        quadrantes = linhas[linhaFinal] - linhas[linhaInicial] + 1;
        while (quadrantes != embarcacoesQuad[embarcacao]) // verifica se a posição corresponde ao número de quadrantes da embarcação
        {
            Console.WriteLine("O número de quadrantes é inválido, tente novamente:");
            posicao = Console.ReadLine().ToUpper();
            PosicaoLinhasEColunas();
        }

        for (int lin = linhas[linhaInicial]; lin <= linhas[linhaFinal]; lin++) // percorrendo para ver se está livre
        {
            while (quadroJog1[lin, colunaInicial] != null)
            {
                quadranteOcupado = true;
                Console.WriteLine("Um ou mais quadrantes dentre os inseridos estão ocupados, tente novamente:");
                posicao = Console.ReadLine().ToUpper();
                PosicaoLinhasEColunas();
            }
        }
    }
}

void QuadranteValidoJog2()
{
    if (linhaInicial == linhaFinal) // HORIZONTAL
    {
        quadrantes = colunaFinal - colunaInicial + 1;
        while (quadrantes != embarcacoesQuad[embarcacao]) // verifica se a posição corresponde ao número de quadrantes da embarcação
        {
            Console.WriteLine("Número de quadrantes é inválido, tente novamente: ");
            posicao = Console.ReadLine().ToUpper();
            PosicaoLinhasEColunas();
            quadrantes = colunaFinal - colunaInicial + 1;
        }

        for (int col = colunaInicial; col <= colunaFinal; col++) // percorrendo para ver se está livre
        {
            while (quadroJog2[linhas[linhaInicial], col] != null)
            {
                quadranteOcupado = true;
                Console.WriteLine("Um ou mais quadrantes dentre os inseridos estão ocupados, tente novamente:");
                posicao = Console.ReadLine().ToUpper();
                PosicaoLinhasEColunas();
            }
        }

    }
    else if (colunaInicial == colunaFinal) // VERTICAL
    {
        quadrantes = linhas[linhaFinal] - linhas[linhaInicial] + 1;
        while (quadrantes != embarcacoesQuad[embarcacao]) // verifica se a posição corresponde ao número de quadrantes da embarcação
        {
            Console.WriteLine("O número de quadrantes é inválido, tente novamente:");
            posicao = Console.ReadLine().ToUpper();
            PosicaoLinhasEColunas();
            quadrantes = linhas[linhaFinal] - linhas[linhaInicial] + 1;
        }

        for (int lin = linhas[linhaInicial]; lin <= linhas[linhaFinal]; lin++) // percorrendo para ver se está livre
        {
            while (quadroJog2[lin, colunaInicial] != null)
            {
                quadranteOcupado = true;
                Console.WriteLine("Um ou mais quadrantes dentre os inseridos estão ocupados, tente novamente:");
                posicao = Console.ReadLine().ToUpper();
                PosicaoLinhasEColunas();
            }
        }
    }
}

void SetEmbarcJog1()
{
    var linhaInicialFixed = linhas[linhaInicial];
    var linhaFinalFixed = linhas[linhaFinal];
    var colunaInicialFixed = colunaInicial * 2;
    var colunaFinalFixed = colunaFinal * 2;

    if (linhaInicial == linhaFinal) // HORIZONTAL
    {
        for (int col = colunaInicial; col <= colunaFinal; col++) // se estiver livre, adiciona embarcação
        {
            quadroJog1[linhaInicialFixed, col] = embarcacao;
        }

        for (int col = colunaInicialFixed; col <= colunaFinalFixed; col++)
        {
            if (col % 2 == 0)
            {
                quadroJog1POSICIONAMENTO[linhaInicialFixed + 1, col + 2] = "1"; 
            }
        }



    }
    else if (colunaInicial == colunaFinal) // VERTICAL
    {
        for (int lin = linhaInicialFixed; lin <= linhaFinalFixed; lin++) // se estiver livre, adiciona embarcação
        {
            quadroJog1[lin, colunaInicial] = embarcacao;
        }

        for (int lin = (linhaInicialFixed + 1); lin <= (linhaFinalFixed + 1); lin++)
        {
            quadroJog1POSICIONAMENTO[lin, colunaInicialFixed + 2] = "1";
        }

    }
}

void SetEmbarcJog2()
{
    var linhaInicialFixed = linhas[linhaInicial];
    var linhaFinalFixed = linhas[linhaFinal];
    var colunaInicialFixed = colunaInicial * 2;
    var colunaFinalFixed = colunaFinal * 2;

    if (linhaInicial == linhaFinal) // HORIZONTAL
    {
        for (int col = colunaInicial; col <= colunaFinal; col++) // se estiver livre, adiciona embarcação
        {
            quadroJog2[linhaInicialFixed, col] = embarcacao;
        }

        for (int col = colunaInicialFixed; col <= colunaFinalFixed; col++)
        {
            if (col % 2 == 0)
            {
                quadroJog2POSICIONAMENTO[linhaInicialFixed + 1, col + 2] = "1";
            }
        }



    }
    else if (colunaInicial == colunaFinal) // VERTICAL
    {
        for (int lin = linhaInicialFixed; lin <= linhaFinalFixed; lin++) // se estiver livre, adiciona embarcação
        {
            quadroJog2[lin, colunaInicial] = embarcacao;
        }

        for (int lin = (linhaInicialFixed + 1); lin <= (linhaFinalFixed + 1); lin++)
        {
            quadroJog2POSICIONAMENTO[lin, colunaInicialFixed + 2] = "1";
        }

    }
}

void EmbarcFaltantes()
{
    switch (embarcacao)
    {
        case "PS":
            psFaltam--;
            break;
        case "NT":
            ntFaltam--;
            break;
        case "DS":
            dsFaltam--;
            break;
        case "SB":
            sbFaltam--;
            break;
    }

    Console.Clear();
    Console.WriteLine($@"Ainda faltam: 
{psFaltam} PS - Porta-Aviões (Ocupa 5 quadrantes)
{ntFaltam} NT - Navios-Tanque (Ocupa 4 quadrantes)
{dsFaltam} DS - Destroyers (Ocupa 3 quadrantes)
{sbFaltam} SB - Submarinos (Ocupa 2 quadrantes)");

}

void MostrarQuadroJog1()
{
    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j < 21; j++)
        {
            Console.Write(quadroJog1JOGO[i, j]);
        }
        Console.WriteLine();
    }
}

void MostrarQuadroJog2()
{
    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j < 21; j++)
        {
            Console.Write(quadroJog2JOGO[i, j]);
        }
        Console.WriteLine();
    }
}

void PosicaoTiro()
{
    bool ltBool = true;
    bool ctBool = true;

    ChecagemPosicaoTiro();

    ltBool = linhasEmLetras.Contains(linTiroStr);
    ctBool = int.TryParse(colTiroStr, out colTiroInt);

    while (!ltBool || !ctBool || !posicaoTiroValida)
    {
        Console.Write(posicaoInvalida);
        posicao = Console.ReadLine().ToUpper();

        ChecagemPosicaoTiro();

        ltBool = linhasEmLetras.Contains(linTiroStr);
        ctBool = int.TryParse(colTiroStr, out colTiroInt);

    }

    linTiroInt = Array.IndexOf(linhasEmLetras, linTiroStr);

}

void ChecagemPosicaoTiro()
{
    if (posicao.Length == 2)
    {
        linTiroStr = posicao.Substring(0, 1);
        colTiroStr = posicao.Substring(1, 1);
        posicaoTiroValida = true;
    }
    else if (posicao.Length == 3)
    {
        linTiroStr = posicao.Substring(0, 1);
        colTiroStr = posicao.Substring(1, 2);
        posicaoTiroValida = true;
    }
    else
    {
        posicaoTiroValida = false;
    }
}

void AcertouTiroNoJog1()
{
    if (quadroJog1POSICIONAMENTO[linTiroInt, (colTiroInt * 2)] == "1")
    {
        Console.WriteLine($"Parabéns, {jogador2}! Você acertou uma embarcação do outro jogador.");
        quadroJog1JOGO[linTiroInt, (colTiroInt * 2)] = "X";
        acertosJog2++;
    }
    else
    {
        Console.WriteLine($"Quadrante vazio, nenhuma embarcação foi atingida.");
        quadroJog1JOGO[linTiroInt, (colTiroInt * 2)] = "A";
    }

}

void AcertouTiroNoJog2()
{ 

    if (quadroJog2POSICIONAMENTO[linTiroInt, (colTiroInt * 2)] == "1")
    {
        Console.WriteLine($"Parabéns, {jogador1}! Você acertou uma embarcação do outro jogador.");
        quadroJog2JOGO[linTiroInt, (colTiroInt * 2)] = "X";
        acertosJog1++;
    }
    else
    {
        Console.WriteLine($"Quadrante vazio, nenhuma embarcação foi atingida.");
        quadroJog2JOGO[linTiroInt, (colTiroInt * 2)] = "A";
    }
}

void Rodada()
{
    Console.Clear();

    Console.WriteLine($"{jogador1}, sua vez de jogar. Escolha uma posição para atirar. Ex: A1");
    MostrarQuadroJog2();

    Console.WriteLine(@$"

PONTUAÇÃO:
{jogador1} => {acertosJog1} pontos
{jogador2} => {acertosJog2} pontos

");

    Console.Write("Posição: ");
    posicao = Console.ReadLine().ToUpper();

    while (tirosJog1.Contains(posicao))
    {
        Console.WriteLine(posicaoInvalida);
        Console.Write("Posição: ");
        posicao = Console.ReadLine().ToUpper();
    }

    tirosJog1.Add(posicao);

    PosicaoTiro();

    AcertouTiroNoJog2();

    Console.WriteLine("Pressione qualquer tecla para continuar");
    Console.ReadKey();

    // ---------------------------------------------------------

    Console.Clear();

    Console.WriteLine($"{jogador2}, sua vez de jogar. Escolha uma posição para atirar. Ex: A1");
    MostrarQuadroJog1();

    Console.WriteLine(@$"

PONTUAÇÃO:
{jogador1} => {acertosJog1} pontos
{jogador2} => {acertosJog2} pontos

");

    Console.Write("Posição: ");

    posicao = Console.ReadLine().ToUpper();

    while (tirosJog2.Contains(posicao))
    {
        Console.WriteLine(posicaoInvalida);
        Console.Write("Posição: ");
        posicao = Console.ReadLine().ToUpper();
    }

    tirosJog2.Add(posicao);

    PosicaoTiro();

    AcertouTiroNoJog1();

    Console.WriteLine("Pressione qualquer tecla para continuar");
    Console.ReadKey();
}

void Ganhador()
{
    if (acertosJog1 == 30)
    {
        Console.Clear();
        Console.WriteLine($"PARABÉNS, {jogador1}!!! Você ganhou essa partida de Batalha-Naval.");
    }
    else if (acertosJog2 == 30)
    {
        Console.Clear();
        Console.WriteLine($"PARABÉNS, {jogador2}!!! Você ganhou essa partida de Batalha-Naval.");
    }

}