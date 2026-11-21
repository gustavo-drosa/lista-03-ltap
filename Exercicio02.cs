using atividade_consumo;
using System;

class Exercicio02
{

    static int menu()
    {
        Console.WriteLine("=== CONTROLE DE CONSUMO DE ENERGIA RESIDENCIAL ===");
        Console.WriteLine("1 - Cadastrar Eletrodoméstico");
        Console.WriteLine("2 - Mostrar Eletrodomésticos");
        Console.WriteLine("3 - Buscar Eletrodoméstico pelo Nome");
        Console.WriteLine("4 - Buscar Eletrodomésticos que Gastam mais que X (kWh/dia)");
        Console.WriteLine("5 - Calcular Consumo Diário e Mensal da Casa (kW e R$)");
        Console.WriteLine("6 - Salvar Dados em Arquivo");
        Console.WriteLine("7 - Carregar Dados do Arquivo");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine()!);
    }


    static void addEletrodomestico(List<Eletrodomestico> listaEletros)
    {
        Eletrodomestico novoEletro = new Eletrodomestico();

        Console.Write("Nome: ");
        novoEletro.nome = Console.ReadLine()!;
        Console.Write("Potência (kW): ");
        novoEletro.potencia = double.Parse(Console.ReadLine()!);
        Console.Write("Tempo Médio Ativo (horas): ");
        novoEletro.tempoMedioAtivo = double.Parse(Console.ReadLine()!);

        listaEletros.Add(novoEletro);
    }

    static void mostrarEletrodomesticos(List<Eletrodomestico> listaEletros)
    {
        int posicao = 1;

        Console.WriteLine("\n==== Listando Eletrodomésticos ====\n");
        foreach (Eletrodomestico e in listaEletros)
        {
            Console.WriteLine(
                $"Eletrodoméstico {posicao}\n" +
                $"Nome: {e.nome}\n" +
                $"Potência: {e.potencia}\n" +
                $"Tempo Médio Ativo: {e.tempoMedioAtivo}"
                );
            Console.WriteLine("--------------------------------");
            posicao++;
        }
    }

    //case 3
    static bool buscarEletrodomestico(List<Eletrodomestico> listaEletros, string nomeBusca)
    {

        bool encontrado = false;
        Console.WriteLine("\n==== Listando Busca de Eletrodomésticos ====\n");

        foreach (Eletrodomestico e in listaEletros)
        {
            if (e.nome.ToUpper().Contains(nomeBusca.ToUpper()))
            {
                Console.WriteLine(
                $"Nome: {e.nome}\n" +
                $"Potência: {e.potencia}\n" +
                $"Tempo Médio Ativo: {e.tempoMedioAtivo}"
                );
                Console.WriteLine("--------------------------------");

                encontrado = true;
            }
        }


        return encontrado;

    }

    //case 4
    static bool buscarPorConsumo(List<Eletrodomestico> listaEletros, double valorX)
    {
        bool encontrado = false;
        double consumo = 0.0;

        Console.WriteLine("\n==== Listando Busca por Consumos ====\n");

        foreach (Eletrodomestico e in listaEletros)
        {
            consumo = e.potencia * e.tempoMedioAtivo;

            if (consumo > valorX)
            {
                Console.WriteLine(
                $"Nome: {e.nome}\n" +
                $"Potência: {e.potencia}\n" +
                $"Tempo Médio Ativo: {e.tempoMedioAtivo}\n" +
                $"Consumo: {consumo:F2}"
                );
                Console.WriteLine("--------------------------------");

                encontrado = true;
            }
        }


        return encontrado;

    }

    //case 5
    static void calcularConsumoCasa(List<Eletrodomestico> listaEletros, double valorKwh)
    {
        double consumoDiarioTotal = 0;

        foreach (Eletrodomestico e in listaEletros)
        {
            consumoDiarioTotal += e.potencia * e.tempoMedioAtivo;

        }

        double consumoMensalTotal = consumoDiarioTotal * 30;

        double consumoDiarioConv = consumoDiarioTotal * valorKwh;
        double consumoMensalConv = consumoMensalTotal * valorKwh;

        Console.WriteLine(
            $"Consumo diário: R${consumoDiarioConv:F2}\n" +
            $"Consumo mensal (30 dias): R${consumoMensalConv:F2}"
            );
    }

    //case 6
    static void salvarDados(List<Eletrodomestico> listaEletro, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);

        foreach (Eletrodomestico eletro in listaEletro)
        {
            writer.WriteLine($"{eletro.nome};{eletro.potencia};{eletro.tempoMedioAtivo}");
        }

        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }

    //case 7 
    static void carregarDados(List<Eletrodomestico> listaEletro, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(';');
                Eletrodomestico eletro = new Eletrodomestico();
                eletro.nome = campos[0];
                eletro.potencia = double.Parse(campos[1]);
                eletro.tempoMedioAtivo = double.Parse(campos[2]);
                listaEletro.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static void Main()
    {

        Console.Clear();

        List<Eletrodomestico> listaEletrodomesticos = new List<Eletrodomestico>();

        carregarDados(listaEletrodomesticos, "Lista_Eletrodomesticos.txt");

        int opcao;

        do
        {

            opcao = menu();

            switch (opcao)
            {
                case 1:
                    addEletrodomestico(listaEletrodomesticos);
                    break;
                case 2:
                    mostrarEletrodomesticos(listaEletrodomesticos);
                    break;
                case 3:
                    // Leia o nome desejado e chame a função buscarEletrodomestico
                    Console.Write("Insira o nome para busca: ");
                    string nomeBusca = Console.ReadLine()!;
                    bool encontrado = buscarEletrodomestico(listaEletrodomesticos, nomeBusca);
                    if (!encontrado)
                    {
                        Console.WriteLine("Nenhum resultado encontrado!");
                    }
                    break;
                case 4:
                    // Leia o valor X (double) e chame a função buscarPorConsumo
                    Console.Write("Insira o consumo que deseja buscar: ");
                    double valorX = double.Parse(Console.ReadLine()!);
                    buscarPorConsumo(listaEletrodomesticos, valorX);
                    break;
                case 5:
                    // Leia o valor da tarifa kW/h (R$) e chame calcularConsumoCasa
                    Console.Write("Valor da tarifa kW/h (R$):");
                    double valorTarifa = double.Parse(Console.ReadLine()!);
                    calcularConsumoCasa(listaEletrodomesticos, valorTarifa);
                    break;
                case 6:
                    // Chame salvarDados passando a lista e o nome do arquivo
                    salvarDados(listaEletrodomesticos, "Lista_Eletrodomesticos.txt");
                    break;
                case 7:
                    // Chame carregarDados passando a lista e o nome do arquivo
                    carregarDados(listaEletrodomesticos, "Lista_Eletrodomesticos.txt");
                    break;
                default:
                    break;

            }

            Console.ReadKey();
            Console.Clear();

        } while (opcao != 0);

        salvarDados(listaEletrodomesticos, "Lista_Eletrodomesticos.txt");
    }

}