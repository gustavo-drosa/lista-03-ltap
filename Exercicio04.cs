using System;
using System.ComponentModel.DataAnnotations;
using CadastroJogos;
using DataEmprestimo;

//Arrumar: Emprestimo, Devolucao feito por indice
class Exercicio04
{
  static int menu()
  {
    Console.Write(
      "*** Menu Jogos ***\n" +
      $"[1] Cadastrar Jogo\n" +
      $"[2] Mostrar Jogos\n" +
      $"[3] Procurar Jogo por Título\n" +
      $"[4] Mostrar Jogos por Console\n" +
      $"[5] Emprestar Jogo\n" +
      $"[6] Devolver Jogo\n" +
      $"[0] Sair\n" +
      "Escolha uma opção: "
      );
    return int.Parse(Console.ReadLine()!);
  }

  static void cadastrarJogo(List<Jogo> colecaoJogos)
  {
    Jogo novoJogo = new Jogo();
    novoJogo.emprestimo = new Emprestimo();

    Console.Write("Título: ");
    novoJogo.titulo = Console.ReadLine()!;
    Console.Write("Console: ");
    novoJogo.console = Console.ReadLine()!;
    Console.Write("Ano: ");
    novoJogo.ano = int.Parse(Console.ReadLine()!);
    Console.Write("Ranking: ");
    novoJogo.ano = int.Parse(Console.ReadLine()!);

    novoJogo.emprestimo.data = "";
    novoJogo.emprestimo.nomePessoa = "";
    novoJogo.emprestimo.emprestado = 'N';

    colecaoJogos.Add(novoJogo);
  }

  static void salvarDados(List<Jogo> colecaoJogos, string nomeArquivo)
  {
    StreamWriter writer = new StreamWriter(nomeArquivo);

    foreach (Jogo j in colecaoJogos)
    {
      writer.WriteLine($"{j.titulo},{j.console},{j.ano},{j.emprestimo.data},{j.emprestimo.nomePessoa},{j.emprestimo.emprestado}");
    }
    Console.WriteLine("Dados salvos com sucesso!");
    writer.Dispose();
  }

  static void carregarDados(List<Jogo> colecaoJogos, string nomeArquivo)
  {
    if (File.Exists(nomeArquivo))
    {
      string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
      foreach (string linha in linhasDoArquivo)
      {
        string[] campos = linha.Split(",");
        Jogo jogo = new Jogo();
        jogo.titulo = campos[0];
        jogo.console = campos[1];
        jogo.ano = int.Parse(campos[2]);
        jogo.emprestimo.data = campos[3];
        jogo.emprestimo.nomePessoa = campos[4];
        jogo.emprestimo.emprestado = Convert.ToChar(campos[5]);


        colecaoJogos.Add(jogo);
      }
      Console.WriteLine("Dados carregados com sucesso");
    }
    else
    {
      Console.WriteLine("Arquivo não encontrado!");
    }
  }

  static void mostrarJogos(List<Jogo> colecaoJogos)
  {
    int contador = 1;

    foreach (Jogo j in colecaoJogos)
    {
      string emprestado = "Não";

      if (j.emprestimo.emprestado == 'S')
      {
        emprestado = $"Sim\n Data: {j.emprestimo.data}; \nNome: {j.emprestimo.nomePessoa}\n";
      }

      Console.WriteLine(
        "---------------------------------\n" +
        $"JOGO {contador}\n" +
        $"Título: {j.titulo}\n" +
        $"Console: {j.console}\n" +
        $"Ano: {j.ano}\n" +
        $"Emprestimo: {emprestado}"
        );

      contador++;
    }
  }

  static bool buscarJogoTitulo(List<Jogo> colecaoJogos, string buscarTitulo)
  {
    int contador = 1;
    bool encontrado = false;

    foreach (Jogo j in colecaoJogos)
    {
      if (j.titulo.ToUpper().Contains(buscarTitulo.ToUpper()))
      {
        string emprestado = "Não";

        if (j.emprestimo.emprestado == 'S')
        {
          emprestado = $"Sim\n Data: {j.emprestimo.data}; \nNome: {j.emprestimo.nomePessoa}\n";
        }

        Console.WriteLine(
          "---------------------------------\n" +
          $"JOGO {contador}\n" +
          $"Título: {j.titulo}\n" +
          $"Console: {j.console}\n" +
          $"Ano: {j.ano}\n" +
          $"Emprestimo: {emprestado}"
          );

        encontrado = true;
        contador++;
      }
    }
    return encontrado;
  }

  static bool mostrarJogoTitulo(List<Jogo> colecaoJogos, string buscarTitulo)
  {
    int contador = 1;
    bool encontrado = false;

    foreach (Jogo j in colecaoJogos)
    {
      if (j.titulo.ToUpper().Contains(buscarTitulo.ToUpper()))
      {
        string emprestado = "Não";

        if (j.emprestimo.emprestado == 'S')
        {
          emprestado = $"Sim\n Data: {j.emprestimo.data}; \nNome: {j.emprestimo.nomePessoa}\n";
        }

        Console.WriteLine(
          "---------------------------------\n" +
          $"JOGO {contador}\n" +
          $"Título: {j.titulo}\n" +
          $"Console: {j.console}\n" +
          $"Ano: {j.ano}\n" +
          $"Emprestimo: {emprestado}"
          );

        encontrado = true;
        contador++;
      }
    }
    return encontrado;
  }

  static bool mostrarJogoConsole(List<Jogo> colecaoJogos, string buscarConsole)
  {
    int contador = 1;
    bool encontrado = false;

    foreach (Jogo j in colecaoJogos)
    {
      if (j.console.ToUpper().Contains(buscarConsole.ToUpper()))
      {
        string emprestado = "Não";

        if (j.emprestimo.emprestado == 'S')
        {
          emprestado = $"Sim\n Data: {j.emprestimo.data}; \nNome: {j.emprestimo.nomePessoa}\n";
        }

        Console.WriteLine(
          "---------------------------------\n" +
          $"JOGO {contador}\n" +
          $"Título: {j.titulo}\n" +
          $"Console: {j.console}\n" +
          $"Ano: {j.ano}\n" +
          $"Emprestimo: {emprestado}"
          );

        encontrado = true;
        contador++;
      }
    }
    return encontrado;
  }

  static bool emprestarJogo(List<Jogo> colecaoJogos, string tituloJogo, string nomePessoa)
  {
    bool emprestado = false;

    DateTime hoje = DateTime.Today;
    string dataFormatada = hoje.ToString("dd/MM/yyyy"); //conferir mas já me retorna nesse formato

    foreach (Jogo j in colecaoJogos)
    {
      if (j.titulo.ToUpper().Equals(tituloJogo.ToUpper()) && j.emprestimo.emprestado == 'N')
      {
        j.emprestimo.emprestado = 'S';
        j.emprestimo.data = dataFormatada;
        j.emprestimo.nomePessoa = nomePessoa;

        emprestado = true;
      }
    }
    return emprestado;
  }

  static int verificarDisponibilidadeJogo(List<Jogo> colecaoJogos, string tituloJogo)
  {
    foreach (Jogo j in colecaoJogos)
    {
      if (j.titulo.ToUpper().Equals(tituloJogo.ToUpper()) && j.emprestimo.emprestado == 'S')
      {
        Console.WriteLine(
          "Jogo já está emprestado no momento.\n" +
          "Informações:\n" +
          $"Data: {j.emprestimo.data}\n" +
          $"Nome: {j.emprestimo.nomePessoa}"
          );

        return 1;
      }
      else if (j.titulo.ToUpper().Equals(tituloJogo.ToUpper()) && j.emprestimo.emprestado == 'N')
      {
        return 0;
      }
    }
    return -1;
  }

  static bool devolverJogo(List<Jogo> colecaoJogos, string tituloDevolucao)
  {
    bool devolucao = false;

    foreach (Jogo j in colecaoJogos)
    {
      if (j.titulo.ToUpper().Equals(tituloDevolucao.ToUpper()) && j.emprestimo.emprestado == 'S')
      {
        j.emprestimo.nomePessoa = "";
        j.emprestimo.data = "";
        j.emprestimo.emprestado = 'N';
        devolucao = true;
      }
    }
    return devolucao;

  }


  static void Main()
  {
    Console.Clear();

    List<Jogo> colecaoJogos = new List<Jogo>();
    carregarDados(colecaoJogos, "Lista_Jogos.txt");

    int opcao = -1;

    do
    {
      opcao = menu();

      switch (opcao)
      {

        case 1:
          cadastrarJogo(colecaoJogos);
          break;
        case 2:
          mostrarJogos(colecaoJogos);
          break;
        case 3:
          Console.Write("Buscar por Título: ");
          string buscarTitulo = Console.ReadLine()!;
          bool encontrarJogoTitulo = mostrarJogoTitulo(colecaoJogos, buscarTitulo);
          if (!encontrarJogoTitulo)
            Console.WriteLine("Nenhum Jogo encontrado!");
          break;
        case 4:
          Console.Write("Buscar por Console: ");
          string buscarJogoConsole = Console.ReadLine()!;
          bool encontrarJogoConsole = mostrarJogoConsole(colecaoJogos, buscarJogoConsole);
          if (!encontrarJogoConsole)
            Console.WriteLine("Nenhum Jogo encontrado!");
          break;
        case 5:
          // emprestar jogo
          Console.Write("Nome do Jogo: ");
          string tituloJogoEmprestimo = Console.ReadLine()!;

          int disponibilidade = verificarDisponibilidadeJogo(colecaoJogos, tituloJogoEmprestimo);
          if (disponibilidade == 0)
          {
            Console.WriteLine("Jogo Disponível!");
            Console.Write("Nome de quem está emprestando: ");
            string nomePessoa = Console.ReadLine()!;
            bool emprestado = emprestarJogo(colecaoJogos, tituloJogoEmprestimo, nomePessoa);
            if (emprestado)
              Console.WriteLine("Emprestimo realizado com sucesso");
            else
              Console.WriteLine("Erro durante o emprestimo!");
          }
          else if (disponibilidade == 1)
          {
            Console.WriteLine("Jogo Não Disponível");
          }
          else
            Console.WriteLine("Jogo não existe na coleção!");

          break;
        case 6:
          // devolver jogo
          Console.Write("Nome do Jogo: ");
          string tituloDevolucao = Console.ReadLine()!;
          bool devolucao = devolverJogo(colecaoJogos, tituloDevolucao);
          if (devolucao)
          {
            Console.WriteLine("Devolvido com sucesso!");
          }
          else
          {
            Console.WriteLine("Jogo não encontrado! Devolução falhou!");
          }
          break;
        default:
          break;
      }

      Console.ReadLine();
      Console.Clear();

    } while (opcao != 0);

    salvarDados(colecaoJogos, "Lista_Jogos.txt");

  }
}