//Sistema Biblioteca utilizando SQL e C#
//importações
using MySql.Data.MySqlClient;

class Program{
    //Configurar Conexão
    public static string Connect_Config = "server=localhost; user=root; database=biblioteca_marcondes; port=3306; password=1234";
    public static MySqlConnection Connect = new(Connect_Config);
    public static bool Permicao = false;

    //Conexão
    public static void Main(){
        try{
            //Limpar console
            Console.Clear();
            
            //Confirmar conexão
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Servidor do Mysql Conectado... !");
            Console.ResetColor();
            Console.WriteLine("=================== Bem-Vindo à Biblioteca de Marcondes ===================");
            Connect.Open();
            Lib();
        }
        catch (Exception ex){
            Console.WriteLine(ex.ToString());
            Connect.Close();
        }

    //Inicio da Biblioteca
    static void Lib(){
        Console.WriteLine("\nInteraja com minha Biblioteca SQL com base nas opções a baixo:\n");

        //Opções
        Console.WriteLine("""
        1 - Fazer Login/Cadastro como autor
        2 - Consultar livros disponiveis 
        3 - Adicionar Livro (Login requerido !!)
        4 - Remover Livro (Login requerido !!)
        5 - Sair da Biblioteca
        """);

        Console.Write("\nSelecionar Opção: ");
        char Escolha = char.Parse(Console.ReadLine());

        switch (Escolha){
            case '1':
                Console.Clear();
                Console.WriteLine("O que deseja fazer ?");
                Console.WriteLine("\n1 - Login");
                Console.WriteLine("2 - Cadastro");

                Console.Write("\nSelecionar Opção: ");
                char Escolha2 = char.Parse(Console.ReadLine());

                switch(Escolha2){
                    case '1':
                        Console.Clear();

                        //formulario
                        Console.Write("Nome de Usuario: ");
                        string Usuario = Console.ReadLine();

                        Console.Write("\nMatricula: ");
                        string Senha = Console.ReadLine();

                        try{
                            MySqlCommand cm0 = new(){
                                CommandText = @"select count(*) from pessoa where nome_pessoa = '" + Usuario + "' and matricula = '" + Senha + "'",
                                Connection = Connect
                            };
                            MySqlDataReader Consulta = cm0.ExecuteReader();

                            //commando
                            while(Consulta.Read()){
                                if(Convert.ToInt32(Consulta[0]) > 0){
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Login Concluido...!!");
                                    Console.ResetColor();
                                    bool Permicao = true;
                                    Lib();
                                }
                            }
                        }
                        catch (Exception){
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ops... Usuario Invalido.");
                            Console.ResetColor();
                            bool Permicao = false;
                            Lib();
                        }
                    break;
                    case '2':
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("""
                        //===========================================\\
                        [  Bem-Vindo(a) à tela de inscrições    ||    
                        [     da Biblioteca de Marcondes !!     ||    
                        \\___________________________________________//
                        """);
                        Console.WriteLine("\nPara se cadastrar é preciso que você insira seu nome é crie um codigo de matricula (senha*)");
                        Console.ResetColor();

                        //Formulario
                        Console.Write("\nDigite seu nome de usuario: ");
                        string N_Usuario = Console.ReadLine();

                        Console.Write("\nDigite sua matricula: ");
                        string N_matricula = Console.ReadLine();

                        //consulta
                        MySqlCommand check = new(){
                            CommandText = "select count(*) from pessoa where nome_pessoa = '"+ N_Usuario +"'",
                            Connection = Connect
                        };
                        MySqlDataReader Linha00 = check.ExecuteReader();
                        Linha00.Read();
                        int num230 = Convert.ToInt32(Linha00[0]);
                        Linha00.Close();

                        //inserir
                        MySqlCommand cm3 = new(){
                            CommandText = "insert into pessoa (nome_pessoa, matricula) values ('"+ N_Usuario +"', '"+ N_matricula +"');",
                            Connection = Connect
                        };
                        if(num230 == 0){
                            //executar
                            cm3.ExecuteNonQuery();

                            //sucesso
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Usuario: {N_Usuario}, foi cadastrado com sucesso...!!");
                            Console.ResetColor();
                            Lib();
                        }
                        else{
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Usuario: {N_Usuario}, já Existe...");
                            Console.ResetColor();
                            Lib();
                        }
                    break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opção invalida...!!!");
                        Console.ResetColor();
                        
                        Lib();
                    break;
                }
            break;

            case '2':
                Console.Clear();
                MySqlCommand cmd = new(){
                    CommandText = "select nome_livro, ano_livro, descricao, nome_autor from livro inner join autor on livro.id_autor = autor.id_autor;",
                    Connection = Connect,
                };
                MySqlDataReader Linha = cmd.ExecuteReader();
                Linha.Read();
                //01
                int espaco01 = Linha[0].ToString().Length;
                int espaco001 = (espaco01 + 2 - 4) / 2 + 1;
                //02
                int espaco02 = Linha[1].ToString().Length;
                int espaco002 = (espaco02 + 2 - 3) / 2 + 1;
                //03
                int espaco03 = Linha[2].ToString().Length;
                int espaco003 = (espaco03 + 2 - 9) / 2;
                //04
                int espaco04 = Linha[3].ToString().Length;
                int espaco004 = (espaco04 + 2 - 5) / 2;
                //05
                int espaco05 = espaco01 + espaco02 + espaco03 + espaco04 + 11;

                //espaçãmentos
                string espaco = "                                                                                                     ";
                string carac = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
                string tracos = "-----------------------------------------------------------------------------------------------------";
                
                Console.WriteLine("");

                //Alteração das cores do console
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine($"""
                |{carac[..espaco05]}|
                |{espaco[..(espaco001 - 1)]}Nome{espaco[..espaco001]}|{espaco[..(espaco002-1)]}Ano{espaco[..espaco002]}|{espaco[..espaco003]}Descrição{espaco[..(espaco003 + 1)]}|{espaco[..espaco004]}Autor{espaco[..espaco004]}|
                |{carac[..(espaco01 + 2)]}|{carac[..(espaco02 + 2)]}|{carac[..(espaco03 + 2)]}|{carac[..(espaco04 + 2)]}|
                | {Linha[0]} | {Linha[1]} | {Linha[2]} | {Linha[3]} |
                """);
                while (Linha.Read()){
                    //var tamanho
                    int nome_livro = Linha[0].ToString().Length;
                    int ano_livro = Linha[1].ToString().Length;
                    int descricao = Linha[2].ToString().Length;
                    int nome_autor = Linha[3].ToString().Length;

                    //par ou impar
                    int num02 = nome_livro % 2; if(num02 == 0){num02 =+ 1;}else{num02 =- 0;}
                    int num03 = descricao % 2; if(num03 == 0){num03 =+ 1;}else{num03 =+ 2;}
                    int num04 = nome_autor % 2; if(num04 == 0){num04 =+ 1;}else{num04 =- 2;}

                    Console.WriteLine($"|{espaco[..((espaco01 + 2 - nome_livro) / 2)]}{Linha[0]}{espaco[..((espaco01 + 2 - nome_livro) / 2 + num02)]}|{espaco[..((espaco02 + 2 - ano_livro) / 2)]}{Linha[1]}{espaco[..((espaco02 + 2 - ano_livro) / 2)]}|{espaco[..((espaco03 + 2 - descricao) / 2)]}{Linha[2]}{espaco[..((espaco03 + 2 - descricao) / 2 + num03 - 1)]}|{espaco[..((espaco04 + 2 - nome_autor) / 2 + num04)]}{Linha[3]}{espaco[..((espaco04 + 2 - nome_autor) / 2)]}|");
                }
                //Reverção
                Console.ResetColor();
                Linha.Close();
                Lib();
            break;
            case '3':
                if(Permicao == true){
                    Console.WriteLine("Funciona");
                }
                else{
                    Console.WriteLine("Erro..");
                }
            break;

            case '5':
                Connect.Close();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Tchau, Espero que tenha gostado...");
                Console.ResetColor();
                break;

            default:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção invalida...!!!");
                Console.ResetColor();
                Lib();
                break;
            }
        }
    }
}
