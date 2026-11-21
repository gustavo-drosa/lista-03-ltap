using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Net;
using CadastroLivro;

class Exercicio03
{

  static int menu()
  {
    Console.Write(
      "*** Menu Biblioteca ***\n" +
      $"[1] Cadastrar Livro\n" +
      $"[2] Buscar Livro por Nome\n" +
      $"[3] Mostrar Livros Cadastrados\n" +
      $"[4] Buscar Livro por Ano\n" +
      $"[5] Salvar Dados\n" +
      $"[6] Carregar Dados\n" +
      $"[0] Sair\n" +
      "Escolha uma opção: "
      );

    return int.Parse(Console.ReadLine()!);
  }

  static void cadastrarLivro(List<Livro> listaLivros)
  {
    Livro novoLivro = new Livro();

    Console.WriteLine("Nome: ");
    novoLivro.nome = Console.ReadLine()!;
    Console.WriteLine("Autor: ");
    novoLivro.autor = Console.ReadLine()!;
    Console.WriteLine("Ano: ");
    novoLivro.ano = int.Parse(Console.ReadLine()!);
    Console.WriteLine("Plateleira: ");
    novoLivro.plateleira = int.Parse(Console.ReadLine()!);

    listaLivros.Add(novoLivro);
  }

  static void mostrarLivros(List<Livro> listaLivros)
  {
    int contador = 1;

    foreach (Livro l in listaLivros)
    {
      Console.WriteLine(
        "---------------------------------\n" +
        $"LIVRO {contador}\n" +
        $"Nome: {l.nome}\n" +
        $"Autor: {l.autor}\n" +
        $"Ano: {l.ano}\n" +
        $"Plateleira: {l.plateleira}"
        );
  
      contador++;
    }
  }

  static void salvarDados(List<Livro> listaLivros, string nomeArquivo)
  {
    StreamWriter writer = new StreamWriter(nomeArquivo);

    foreach (Livro l in listaLivros)
    {
      writer.WriteLine($"{l.nome},{l.autor},{l.ano},{l.plateleira}");
    }
    Console.WriteLine("Dados salvos com sucesso!");
    writer.Dispose();
  }

  static void carregarDados(List<Livro> listaLivros, string nomeArquivo)
  {
    if (File.Exists(nomeArquivo))
    {
      string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
      foreach (string linha in linhasDoArquivo)
      {
        string[] campos = linha.Split(",");
        Livro livro = new Livro();
        livro.nome = campos[0];
        livro.autor = campos[1];
        livro.ano = int.Parse(campos[2]);
        livro.plateleira = int.Parse(campos[3]);
        listaLivros.Add(livro);
      }
      Console.WriteLine("Dados carregados com sucesso");
    }
    else
    {
      Console.WriteLine("Arquivo não encontrado!");
    }
  }

  static bool buscarLivroNome(List<Livro> listaLivros, string buscarNome)
  {
    int contador = 1;
    bool encontrado = false;

    foreach (Livro l in listaLivros)
    {
      if (l.nome.ToUpper().Contains(buscarNome.ToUpper()))
      {
        Console.WriteLine(
          "---------------------------------\n" +
        $"LIVRO ENCONTRADO {contador}\n" +
        $"Nome: {l.nome}\n" +
        $"Autor: {l.autor}\n" +
        $"Ano: {l.ano}\n" +
        $"Plateleira: {l.plateleira}"
        );
        contador++;
        encontrado = true;
      }
    }

    return encontrado;
  }

  static bool buscarLivroAno(List<Livro> listaLivros, int buscarAno)
  {
    int contador = 1;
    bool encontrado = false;

    foreach (Livro l in listaLivros)
    {
      if (l.ano == buscarAno)
      {
        Console.WriteLine(
          "---------------------------------\n" +
        $"LIVRO ENCONTRADO {contador}\n" +
        $"Nome: {l.nome}\n" +
        $"Autor: {l.autor}\n" +
        $"Ano: {l.ano}\n" +
        $"Plateleira: {l.plateleira}"
        );
        contador++;
        encontrado = true;
      }
    }

    return encontrado;
  }

  static void Main()
  {
    Console.Clear();

    List<Livro> listaLivros = new List<Livro>();
    carregarDados(listaLivros, "Lista_Livros.txt");

    int opcao = -1;

    do
    {
      opcao = menu();

      switch (opcao)
      {

        case 1:
          cadastrarLivro(listaLivros);
          break;
        case 2:
          Console.Write("Buscar por nome: ");
          string buscarNome = Console.ReadLine()!;
          bool encontrarLivroNome = buscarLivroNome(listaLivros, buscarNome);
          if (!encontrarLivroNome)
            Console.WriteLine("Nenhum Livro encontrado!");
          break;
        case 3:
          mostrarLivros(listaLivros);
          break;
        case 4:
          Console.Write("Buscar por Ano: ");
          int buscarAno = int.Parse(Console.ReadLine()!);
          bool encontrarLivroAno = buscarLivroAno(listaLivros, buscarAno);
          if (!encontrarLivroAno)
            Console.WriteLine("Nenhum Livro encontrado!");
          break;
        case 5:
          // salvar
          salvarDados(listaLivros, "Lista_Livros.txt");
          break;
        case 6:
          // carregar
          carregarDados(listaLivros, "Lista_Livros.txt");

          break;
        default:
          break;
      }

      Console.ReadLine();
      Console.Clear();

    } while (opcao != 0);

    salvarDados(listaLivros, "Lista_Livros.txt");

  }
}