using CabecaGado;
using NascimentoGado;
using System;

class Exercicio05
{

  static int menu()
  {
    Console.WriteLine(
      "*** CONTROLE DA FAZENDA DE GADO ***\n" +
      "[1] Adicionar cabeça de gado\n" +
      "[2] Listar Cabeças de gado\n" +
      "[3] Preencher Abates\n" +
      "[4] Verificar Litros de leite produzidos por semana\n" +
      "[5] Verificar quantidade de alimento consumido por semana\n" +
      "[6] Listar Animais para abate\n" +
      "[7] Salvar dados\n" +
      "[8] Carregar dados\n" +
      "[0] Sair"
    );
    return int.Parse(Console.ReadLine()!);
  }

  static void adicionarGado(List<Gado> listaGados)
  {
    Gado novoGado = new Gado();

    Console.Write("Código: ");
    novoGado.codigo = int.Parse(Console.ReadLine()!);
    Console.Write("Leite produzido (L/semana): ");
    novoGado.leite = double.Parse(Console.ReadLine()!);
    Console.Write("Alimento consumido (Kg/semana): ");
    novoGado.alim = double.Parse(Console.ReadLine()!);
    Console.Write("Nascimento (Ano): ");
    novoGado.nasc.ano = int.Parse(Console.ReadLine()!);
    Console.Write("Nascimento (Mês): ");
    novoGado.nasc.mes = int.Parse(Console.ReadLine()!);

    listaGados.Add(novoGado);
  }

  static void listarGado(List<Gado> listaGados)
  {
    DateOnly hoje = DateOnly.FromDateTime(DateTime.Today);

    int contador = 1;
    foreach (Gado g in listaGados)
    {
      DateOnly nascimentoGado = new DateOnly(g.nasc.ano, g.nasc.mes, 1);
      int idadeGado = hoje.Year - nascimentoGado.Year;
      if (hoje < nascimentoGado.AddYears(idadeGado))
      {
        idadeGado--;
      }

      Console.WriteLine(
         $"*** CABEÇA {contador} ***\n" +
         $"Código: {g.codigo}\n" +
         $"Leite (L/semana): {g.leite}\n" +
         $"Alimento Consumido (Kg/semana): {g.alim}\n" +
         $"Nascimento: {g.nasc.mes}/{g.nasc.ano}\n" +
         $"Idade: {idadeGado} anos\n" +
         $"Abate: {g.abate}"
         );

      contador++;

    }

  }

  static bool prepararAbates(List<Gado> listaGados)
  {
    bool abateDisponivel = false;

    DateOnly hoje = DateOnly.FromDateTime(DateTime.Today);

    foreach (Gado g in listaGados)
    {
      DateOnly nascimentoGado = new DateOnly(g.nasc.ano, g.nasc.mes, 1);

      int idadeGado = hoje.Year - nascimentoGado.Year;
      if (hoje < nascimentoGado.AddYears(idadeGado))
      {
        idadeGado--;
      }

      if (g.leite < 40.0 || idadeGado > 5)
      {
        g.abate = 'S';
        abateDisponivel = true;
      }
      else
      {
        g.abate = 'N';
      }
    }

    return abateDisponivel;
  }

  static double verificarProducaoLeite(List<Gado> listaGados)
  {
    // "[3] Verificar Litros de leite produzidos por semana\n" +
    double producaoTotal = 0.0;

    foreach (Gado g in listaGados)
    {
      producaoTotal += g.leite;
    }

    return producaoTotal;

  }

  static double verificarAlimentoConsumido(List<Gado> listaGados)
  {
    // "[4] Verificar quantidade de alimento consumido por semana\n" +

    double alimentoConsumidoTotal = 0.0;

    foreach (Gado g in listaGados)
    {
      alimentoConsumidoTotal += g.alim;
    }

    return alimentoConsumidoTotal;
  }

  static void listarAbate(List<Gado> listaGados)
  {
    DateOnly hoje = DateOnly.FromDateTime(DateTime.Today);

    int contador = 1;
    // "[5] Listar Animais para abate\n" +
    foreach (Gado g in listaGados)
    {
      DateOnly nascimentoGado = new DateOnly(g.nasc.ano, g.nasc.mes, 1);
      int idadeGado = hoje.Year - nascimentoGado.Year;
      if (hoje < nascimentoGado.AddYears(idadeGado))
      {
        idadeGado--;
      }

      if (g.abate == 'S')
      {
        Console.WriteLine(
          $"*** CABEÇA {contador} ***\n" +
          $"Código: {g.codigo}\n" +
          $"Leite (L/semana): {g.leite}\n" +
          $"Alimento Consumido (Kg/semana): {g.alim}\n" +
          $"Nascimento: {g.nasc.mes}/{g.nasc.ano}\n" +
          $"Idade: {idadeGado} anos"
          );

        contador++;
      }
    }

  }

  static void salvarDados(List<Gado> listaGados, string nomeArquivo)
  {
    StreamWriter writer = new StreamWriter(nomeArquivo);

    foreach (Gado g in listaGados)
    {
      writer.WriteLine($"{g.codigo},{g.leite},{g.alim},{g.nasc.mes},{g.nasc.ano},{g.abate}");
    }
    Console.WriteLine("Dados salvos com sucesso!");
    writer.Dispose();
  }

  static void carregarDados(List<Gado> listaGados, string nomeArquivo)
  {
    if (File.Exists(nomeArquivo))
    {
      string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
      foreach (string linha in linhasDoArquivo)
      {
        string[] campos = linha.Split(",");
        Gado gado = new Gado();
        gado.codigo = int.Parse(campos[0]);
        gado.leite = double.Parse(campos[1]);
        gado.alim = double.Parse(campos[2]);
        gado.nasc.mes = int.Parse(campos[3]);
        gado.nasc.ano = int.Parse(campos[4]);
        gado.abate = Convert.ToChar(campos[5]);


        listaGados.Add(gado);
      }
      Console.WriteLine("Dados carregados com sucesso");
    }
    else
    {
      Console.WriteLine("Arquivo não encontrado!");
    }
  }

  static void Main()
  {
    Console.Clear();

    List<Gado> listaGados = new List<Gado>();

    carregarDados(listaGados, "Lista_Gado.txt");

    int opcao = 1;

    do
    {
      opcao = menu();

      switch (opcao)
      {
        case 1:
          // Adicionar cabeça de gado
          adicionarGado(listaGados);
          break;
        case 2:
          listarGado(listaGados);
          break;
        case 3:
          // Preencher Abates
          bool checarAbates = prepararAbates(listaGados);
          if (checarAbates)
            Console.WriteLine("Cabeças marcadas para abate.");
          else
            Console.WriteLine("Nenhuma nova cabeça para abate");
          break;
        case 4:
          // Verificar Litros de leite produzidos por semana
          double producaoLeite = verificarProducaoLeite(listaGados);
          Console.WriteLine($"Total de Leite produzido por semana: {producaoLeite:F2}L");
          break;
        case 5:
          // Verificar quantidade de alimento consumido por semana
          double alimentoConsumido = verificarAlimentoConsumido(listaGados);
          Console.WriteLine($"Total de Alimento consumido por semana: {alimentoConsumido:F2}Kg");
          break;
        case 6:
          // Listar Animais para abate
          listarAbate(listaGados);
          break;
        case 7:
          // Salvar dados
          salvarDados(listaGados, "Lista_Gado.txt");
          break;
        case 8:
          // Carregar dados
          carregarDados(listaGados, "Lista_Gado.txt");
          break;
        case 0:
          // Sair e salvar
          salvarDados(listaGados, "Lista_Gado.txt");
          break;
        default:
          break;
      }
      Console.ReadKey();
      Console.Clear();
    } while (opcao != 0);

  }
}