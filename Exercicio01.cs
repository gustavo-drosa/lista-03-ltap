using CadastroBandasConsoleApp2026;
using System;
using System.IO; //Biblioteca para utilizar o writer para escrever arquivos

class Exercicio01
{
    static void addBanda(List<Banda> listaBandas)
    {
        Banda novaBanda = new Banda();
        Console.WriteLine("Entre com os dados da Banda");
        Console.Write("Nome da banda:");
        novaBanda.nome = Console.ReadLine()!;
        Console.Write("Genero da banda:");
        novaBanda.genero = Console.ReadLine()!;
        Console.Write("Integrantes da banda:");
        novaBanda.integrantes = int.Parse(Console.ReadLine()!);
        Console.Write("Ranking da banda:");
        novaBanda.ranking = int.Parse(Console.ReadLine()!);
        listaBandas.Add(novaBanda); // colocando a nova banda no vetor

    }

    static void mostrarBandas(List<Banda> listaBandas)
    {
        Console.WriteLine("*** Bandas Cadastradas ***");
        for (int i = 0; i < listaBandas.Count; i++)
        {
            Console.WriteLine("*** Dados da Banda " + (i + 1) + "***");
            Console.WriteLine($"Nome:{listaBandas[i].nome}");
            Console.WriteLine($"Genero:{listaBandas[i].genero}");
            Console.WriteLine($"Integrantes:{listaBandas[i].integrantes}");
            Console.WriteLine($"Ranking:{listaBandas[i].ranking}\n");
        }

    }

    static int menu()
    {
        Console.WriteLine("=== SISTEMA DE CADASTRO DE BANDAS ===");
        Console.WriteLine("1 - Adicionar Banda"); //a
        Console.WriteLine("2 - Mostrar Bandas"); //b
        Console.WriteLine("3 - Buscar Banda"); //f
        Console.WriteLine("4 - Buscar Banda por Ranking"); //d
        Console.WriteLine("5 - Buscar Banda por Genêro"); //e
        Console.WriteLine("6 - Alterar Banda"); //h
        Console.WriteLine("7 - Remover Banda"); //g
        Console.WriteLine("8 - Salvar Dados"); //c
        Console.WriteLine("9 - Carregar Dados");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine()!);
    }

    static void salvarDados(List<Banda> listadeBandas, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);

        foreach (Banda banda in listadeBandas)
        {
            writer.WriteLine($"{banda.nome},{banda.genero},{banda.integrantes},{banda.ranking}");
        }

        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }

    static void carregarDados(List<Banda> listadeBandas, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(',');
                Banda banda = new Banda();
                banda.nome = campos[0];
                banda.genero = campos[1];
                banda.integrantes = int.Parse(campos[2]);
                banda.ranking = int.Parse(campos[3]);
                listadeBandas.Add(banda);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static bool buscarBanda(List<Banda> listaBandas, string nomeBanda)
    {
        bool encontrado = false;
        for (int i = 0; i < listaBandas.Count; i++) // Percorra até a quantidade de elementos da lista
        {
            if (listaBandas[i].nome.ToUpper().Contains(nomeBanda.ToUpper()))
            {
                Console.WriteLine("*** Banda ***");
                Console.WriteLine($"Nome: {listaBandas[i].nome}");
                Console.WriteLine($"Gênero: {listaBandas[i].genero}");
                Console.WriteLine($"Integrantes: {listaBandas[i].integrantes}");
                Console.WriteLine($"Ranking: {listaBandas[i].ranking}");
                encontrado = true; // Marque que pelo menos uma banda foi localizada
            }
        }
        return encontrado;
    }

    static int buscarIndiceBanda(List<Banda> listaBandas, string nomeBanda)
    {
        for (int i = 0; i < listaBandas.Count; i++)
        {
            if (listaBandas[i].nome.ToUpper().Equals(nomeBanda.ToUpper())) // Compare se os nomes são iguais
            {
                Console.WriteLine($"*** Banda ***");
                Console.WriteLine($"Nome: {listaBandas[i].nome}");
                Console.WriteLine($"Gênero: {listaBandas[i].genero}");
                Console.WriteLine($"Integrantes: {listaBandas[i].integrantes}");
                Console.WriteLine($"Ranking: {listaBandas[i].ranking}");
                return i; // Retorne o índice onde a banda foi encontrada
            }
        }
        return -1; // Retorne o valor padrão que indica "não encontrado"
    }

    static bool atualizarBanda(List<Banda> listaBandas, string nomeBanda)
    {
        int indice = buscarIndiceBanda(listaBandas, nomeBanda); // Busque o índice da banda
        if (indice != -1)
        {
            Console.Write("Novo Nome:");
            listaBandas[indice].nome = Console.ReadLine()!;
            Console.Write("Novo Gênero:");
            listaBandas[indice].genero = Console.ReadLine()!;
            Console.Write("Novos Integrantes:");
            listaBandas[indice].integrantes = int.Parse(Console.ReadLine()!);
            Console.Write("Novo Ranking:");
            listaBandas[indice].ranking = int.Parse(Console.ReadLine()!);
            return true;
        }
        return false;
    }

    static bool removerBanda(List<Banda> listaBandas, string nomeBanda)
    {
        int indice = buscarIndiceBanda(listaBandas, nomeBanda);
        if (indice != -1)
        {
            Console.Write("Confirma a remoção da banda? (1-Sim / 2-Não): ");
            int confirmacao = int.Parse(Console.ReadLine()!);
            if (confirmacao == 1)
            {
                listaBandas.RemoveAt(indice); // Remova o elemento (RemoveAt) 
                return true;
            }
        }
        return false;
    }

    static bool buscarBandaRanking(List<Banda> listaBandas, int rankingBusca)
    {
        bool encontrado = false;

        foreach (Banda b in listaBandas)
        {
            if (b.ranking == rankingBusca)
            {
                Console.WriteLine(
                    $"Nome: {b.nome}\n" +
                    $"Gênero: {b.genero}\n" +
                    $"Integrantes: {b.integrantes}\n" +
                    $"Ranking: {b.ranking}\n"
                );
                encontrado = true;
            }
        }
        return encontrado;
    }

    static bool mostrarBandasGenero(List<Banda> listaBandas, string buscaGenero)
    {
        bool encontrado = false;
        Console.WriteLine($"*** Bandas de {buscaGenero} ***");
        for (int i = 0; i < listaBandas.Count; i++)
        {
            if (listaBandas[i].genero.ToUpper() == buscaGenero.ToUpper())
            {
                Console.WriteLine("*** Dados da Banda " + (i + 1) + "***");
                Console.WriteLine($"Nome:{listaBandas[i].nome}");
                Console.WriteLine($"Genero:{listaBandas[i].genero}");
                Console.WriteLine($"Integrantes:{listaBandas[i].integrantes}");
                Console.WriteLine($"Ranking:{listaBandas[i].ranking}\n");
                encontrado = true;
            }
        }
        return encontrado;
    }


    static void Main()
    {

        Console.Clear();

        List<Banda> listaBandas = new List<Banda>(); // vetor de bandas
        int opcao;
        carregarDados(listaBandas, "Lista_Bandas.txt");

        do
        {
            opcao = menu();
            string buscarTexto;
            int buscarValor = 0;
            bool flag = false;

            switch (opcao)
            {
                case 1:
                    addBanda(listaBandas);
                    break;
                case 2:
                    mostrarBandas(listaBandas);
                    break;
                case 3:
                    Console.Write("Digite o nome da banda que deseja buscar:");
                    buscarTexto = Console.ReadLine()!;
                    flag = buscarBanda(listaBandas, buscarTexto);
                    if (!flag)
                        Console.WriteLine("Banda não encontrada!");
                    break;
                case 4:
                    Console.Write("Digite o Ranking da banda que deseja buscar:");
                    buscarValor = int.Parse(Console.ReadLine()!);
                    flag = buscarBandaRanking(listaBandas, buscarValor);
                    if (!flag)
                        Console.WriteLine("Banda não encontrada!");
                    break;
                case 5:
                    Console.Write("Insira o gênero que deseja listar:");
                    buscarTexto = Console.ReadLine()!;
                    flag = mostrarBandasGenero(listaBandas, buscarTexto);
                    if (!flag)
                        Console.WriteLine("Banda não encontrada!");
                    break;
                case 6:
                    Console.Write("Digite o nome da banda que deseja atualizar:");
                    buscarTexto = Console.ReadLine()!;
                    flag = atualizarBanda(listaBandas, buscarTexto);
                    if (flag)
                        Console.WriteLine("Banda atualizada com sucesso!");
                    else
                        Console.WriteLine("Banda não encontrada!");
                    break;
                case 7:
                    Console.Write("Digite o nome da banda que deseja remover:");
                    buscarTexto = Console.ReadLine()!;
                    flag = removerBanda(listaBandas, buscarTexto);
                    if (flag)
                        Console.WriteLine("Banda removida com sucesso!");
                    else
                        Console.WriteLine("Banda não encontrada/removida!");
                    break;
                case 8:
                    salvarDados(listaBandas, "Lista_Bandas.txt");
                    break;
                case 9:
                    carregarDados(listaBandas, "Lista_Bandas.txt");
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    salvarDados(listaBandas, "Lista_Bandas.txt");
                    break;
            }// fim switch
            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);

    }// fim main

}
