using System;

namespace DIO.Series.Asaph
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario != "X") //ObterOpcaoUsuario ja deixa em Caixa alta
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Opção Invalida");
                        break;

                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Finalizando");
            Console.ReadLine();
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o ID da série: ");
            try
            {
                int indiceSerie = int.Parse(Console.ReadLine());
                repositorio.Exclui(indiceSerie);
            }
            catch(FormatException)
            {
                Console.WriteLine("ID Invalido");
            }
            
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o ID da série: ");
            try
            {


                int indiceSerie = int.Parse(Console.ReadLine());

                var serie = repositorio.RetornaPorId(indiceSerie);

                Console.WriteLine(serie);
            }
            catch (FormatException)
            {
                Console.WriteLine(" - ID Invalido");
            }
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o ID da série: ");

            try
            {
                int indiceSerie = int.Parse(Console.ReadLine());

                try
                {

                    var lista = repositorio.Lista();



                    Console.WriteLine("ID :" + lista[indiceSerie].retornaTitulo());

                    int entradaGenero = -1;
                    string entradaTitulo = "null";
                    int entradaAno = 0000;
                    string entradaDescricao = "null";
                    Insere_Altera(ref entradaGenero, ref entradaTitulo, ref entradaAno, ref entradaDescricao);


                    Serie atualizaSerie = new Serie(id: indiceSerie,
                                                genero: (Genero)entradaGenero,
                                                titulo: entradaTitulo,
                                                ano: entradaAno,
                                                descricao: entradaDescricao);

                    repositorio.Atualiza(indiceSerie, atualizaSerie);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("ID: " + indiceSerie + " Inexistente");
                }

            }

            catch (FormatException)
            {
                Console.WriteLine(" -ID Invalido \n -Digite somente numeros para um ID valido \n -Retornando ao Menu Principal ");
            }
        }
        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            int entradaGenero = -1;
            string entradaTitulo = "null";
            int entradaAno = 0000;
            string entradaDescricao = "null";
            Insere_Altera(ref entradaGenero, ref entradaTitulo, ref entradaAno, ref entradaDescricao);

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void Insere_Altera(ref int entradaGenero, ref string entradaTitulo, ref int entradaAno, ref string entradaDescricao)
        {

            #region "Titulo"  
            while (true)
            {
                Console.Write("Digite o Título da Série: ");

                string entradaString = Console.ReadLine();
                if (entradaString == "")
                {
                    Console.WriteLine("Titulo Invalido");
                    continue;
                }
                else
                {
                    entradaTitulo = entradaString;
                    break;
                }
            }
            #endregion

            #region "Genero"
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            while (true)
            {
                Console.Write("Digite o gênero entre as opções acima: ");

                string entradaString = Console.ReadLine();
                if (Int32.TryParse(entradaString, out int value))
                {
                    entradaGenero = value;
                    break;
                }
                else
                {
                    Console.WriteLine("Genero Invalido");
                    continue;
                }
            }
            #endregion

            #region "Ano"
            while (true)
            {
                Console.Write("Digite o Ano de Início da Série: ");

                string entradaString = Console.ReadLine();
                if (Int32.TryParse(entradaString, out int value))
                {
                    entradaAno = value;
                    break;
                }
                else
                {
                    Console.WriteLine("Ano Invalido");
                    continue;
                }
            }
            #endregion

            #region "Descrição"
            while (true)
            {
                Console.Write("Digite a Descrição da Série: ");

                string entradaString = Console.ReadLine();
                if (entradaString == "")
                {
                    Console.WriteLine("Descrição Invalida");
                    continue;
                }
                else
                {
                    entradaDescricao = entradaString;
                    break;
                }
            }
            #endregion

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("-- Series --");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
