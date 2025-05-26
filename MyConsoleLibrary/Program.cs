//Sistema Biblioteca utilizando SQL e C#
//importações
using MySql.Data.MySqlClient;

class Program{
    //Configurar Conexão
    static readonly string Connect_Config = "server=localhost; user=root; database=sistema_biblioteca; port=3306; password=12345678";
    public static MySqlConnection Connect = new(Connect_Config);
    
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
    }
    public static bool pass;
    public static string Usuario;
    //Inicio da Biblioteca
    public static void Lib(){
        Console.WriteLine("\nInteraja com minha Biblioteca SQL com base nas opções a baixo:\n");
        
        //Opções
        Console.WriteLine("""
        1 - Fazer Login/Cadastro como autor
        2 - Consultar livros disponiveis 
        3 - Adicionar Livro (Login requerido !!)
        4 - Remover/alterar Livro (Login requerido !!)
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
                        Usuario = Console.ReadLine();

                        Console.Write("\nMatricula: ");
                        string Senha = Console.ReadLine();

                        //commando
                        MySqlCommand cm0 = new(){
                            CommandText = "select count(*) from pessoa where nome_pessoa = '" + Usuario + "' and matricula = '" + Senha + "'",
                            Connection = Connect
                        };
                        MySqlDataReader Consulta = cm0.ExecuteReader();

                        Consulta.Read();
                        int num123 = Convert.ToInt32(Consulta[0]);
                        Consulta.Close();

                        if(num123 > 0){
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Login Concluido...!!");
                            pass = true;
                            Console.ResetColor();
                            Lib();
                        }
                        else{
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ops... Usuario Invalido.");
                            Console.ResetColor();
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
                int espaco03 = Linha[2].ToString().Length;
                int espaco04 = Linha[3].ToString().Length;

                //espaçãmentos
                string espaco = "                                                                                                     ";
                string carac = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";

                //Alteração das cores do console
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                //par ou impar
                int num002 = espaco01 % 2; if(num002 == 0){num002 =+ 0;}else{num002 =- 1;}
                int num003 = espaco03 % 2; if(num003 == 0){num003 =+ 1;}else{num003 =- 0;}
                int num004 = espaco04 % 2; if(num004 == 0){num004 =+ 0;}else{num004 =- 1;}

                //Cabeçãlho
                Console.WriteLine($"""
                |{carac[..(20 + 5 + 50 + 20 + 4)]}|
                |{espaco[..(10 - 2)]}Nome{espaco[..(10 - 2)]}|{espaco[..(3 - 2)]}Ano{espaco[..(3 - 2 + 1)]}|{espaco[..(25 - 4)]}Descrição{espaco[..(25 - 5)]}|{espaco[..(10 - 2)]}Autor{espaco[..(10 - 3)]}|
                |{carac[..20]}|{carac[..6]}|{carac[..50]}|{carac[..20]}|
                |{espaco[..(10 - espaco01 / 2)]}{Linha[0]}{espaco[..(10 - espaco01 / 2 + num002)]}| {Linha[1]} |{espaco[..(23 + 2 - espaco03 / 2)]}{Linha[2]}{espaco[..(22 + 2 - espaco03 / 2 + num003)]}|{espaco[..(10 - espaco04 / 2)]}{Linha[3]}{espaco[..(10 - espaco04 / 2 + num004)]}|
                """);
                
                while (Linha.Read()){
                    //var tamanho
                    int nome_livro = Linha[0].ToString().Length;
                    int ano_livro = Linha[1].ToString().Length;
                    int descricao = Linha[2].ToString().Length;
                    int nome_autor = Linha[3].ToString().Length;

                    //par ou impar
                    int num02 = nome_livro % 2; if(num02 == 0){num02 =+ 0;}else{num02 =- 1;}
                    int num03 = descricao % 2; if(num03 == 0){num03 =+ 1;}else{num03 =- 0;}
                    int num04 = nome_autor % 2; if(num04 == 0){num04 =+ 0;}else{num04 =- 1;}

                    //linhas da tabela
                    Console.WriteLine($"|{espaco[..(10 - nome_livro / 2 + num02)]}{Linha[0]}{espaco[..(10 - nome_livro / 2)]}|{espaco[..(3 - ano_livro / 2)]}{Linha[1]}{espaco[..(3 - ano_livro / 2)]}|{espaco[..(23 + 2 - descricao / 2)]}{Linha[2]}{espaco[..(22 + 2 - descricao / 2 + num03)]}|{espaco[..(10 - nome_autor / 2 )]}{Linha[3]}{espaco[..(10 - nome_autor / 2 + num04)]}|");
                }
                //Reverção
                Console.ResetColor();
                Linha.Close();
                Lib();
            break;
            
            case '3':
                if (pass)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("""
                    Olá, meu querido autor.  ( ^_^ )/
                    Pronto para postar seu livro em meu catalogo ???
                    """);
                    Console.ResetColor();

                    Console.WriteLine("\nVamos começar!, Primeiro preciso de algumas informações\n");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("""
                    1 - Nome do Livro
                    2 - Ano em que ele foi lançado
                    3 - Uma descrição básica de até 50 caracteres !!
                    """);
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n?4 / obs: O nome do autor deste livro será registrado com o nome matriculado em nossa biblioteca...!");
                    Console.ResetColor();

                    // Entrada do nome do livro
                    Console.Write("\nNome do livro: ");
                    string livro = Console.ReadLine();

                    // Verificar se livro já existe
                    int existe = 0;
                    using (var cmdExisteLivro = new MySqlCommand("SELECT COUNT(*) FROM livro WHERE nome_livro = @livro", Connect))
                    {
                        cmdExisteLivro.Parameters.AddWithValue("@livro", livro);
                        existe = Convert.ToInt32(cmdExisteLivro.ExecuteScalar());
                    }

                    if (existe > 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Já existe um livro registrado com o nome: {livro}, em nosso banco de dados...");
                        Console.ResetColor();
                        Lib();
                    }
                    else
                    {
                        // Entradas ano e descrição
                        Console.Write("\nAno de lançamento: ");
                        string ano = Console.ReadLine();

                        Console.Write("\n(Max = 50 Crt) Descrição: ");
                        string descreva = Console.ReadLine();
                        string desk = descreva.Length > 50 ? descreva.Substring(0, 50) : descreva;

                        // Obter próximo ISBN
                        int isbn = 0;
                        using (var cmdIsbn = new MySqlCommand("SELECT IFNULL(MAX(CAST(isbn AS UNSIGNED)), 0) FROM livro", Connect))
                        {
                            isbn = Convert.ToInt32(cmdIsbn.ExecuteScalar()) + 1;
                        }

                        // Verificar se autor existe
                        int autorCount = 0;
                        using (var cmdAutorCount = new MySqlCommand("SELECT COUNT(*) FROM autor WHERE nome_autor = @usuario", Connect))
                        {
                            cmdAutorCount.Parameters.AddWithValue("@usuario", Usuario);
                            autorCount = Convert.ToInt32(cmdAutorCount.ExecuteScalar());
                        }

                        if (autorCount == 0)
                        {
                            // Inserir autor
                            using (var cmdAddAutor = new MySqlCommand("INSERT INTO autor(nome_autor) VALUES (@usuario)", Connect))
                            {
                                cmdAddAutor.Parameters.AddWithValue("@usuario", Usuario);
                                cmdAddAutor.ExecuteNonQuery();
                            }
                        }

                        // Obter id do autor
                        int idAutor = 0;
                        using (var cmdGetAutor = new MySqlCommand("SELECT id_autor FROM autor WHERE nome_autor = @usuario", Connect))
                        {
                            cmdGetAutor.Parameters.AddWithValue("@usuario", Usuario);
                            idAutor = Convert.ToInt32(cmdGetAutor.ExecuteScalar());
                        }

                        // Montar ISBN com zeros à esquerda (exemplo com tamanho 6)
                        string isbnStr = isbn.ToString();
                        string zeros = new string('0', 6 - isbnStr.Length);
                        string env_isbn = zeros + isbnStr;

                        // Confirmação
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Deseja publicar estas informações: ?\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"""
                        1 - Nome do livro: {livro}
                        2 - Ano de Lançamento: {ano}
                        3 - Nome do autor: {Usuario}
                        """);
                        Console.WriteLine($"\nDescrição: {desk}\n");
                        Console.ResetColor();

                        Console.Write("S para Sim, N para Não: ");
                        string op = Console.ReadLine()?.Trim().ToUpper();

                        if (op == "S")
                        {
                            using (var cmdAddBook = new MySqlCommand("INSERT INTO livro(nome_livro, ano_livro, descricao, isbn, id_autor) VALUES (@livro, @ano, @descricao, @isbn, @id_autor)", Connect))
                            {
                                cmdAddBook.Parameters.AddWithValue("@livro", livro);
                                cmdAddBook.Parameters.AddWithValue("@ano", ano);
                                cmdAddBook.Parameters.AddWithValue("@descricao", desk);
                                cmdAddBook.Parameters.AddWithValue("@isbn", env_isbn);
                                cmdAddBook.Parameters.AddWithValue("@id_autor", idAutor);
                                cmdAddBook.ExecuteNonQuery();
                            }

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("As informações foram enviadas com sucesso...!!");
                            Console.ResetColor();
                            Lib();
                        }
                        else if (op == "N")
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Envio Cancelado...");
                            Console.ResetColor();
                            Lib();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opção inválida...!!!");
                            Console.ResetColor();
                            Lib();
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Té Avisei, Faça Login para continuar...!!");
                    Console.ResetColor();
                    Lib();
                }
            break;

            case '4':
                if(pass == true){
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Olá, meu querido autor. ( ^_^ )/, O que desejas fazer ???\n");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("""
                    1 - Remover um livro de nossa biblioteca
                    2 - Atualizar as informações de um livro
                    """);
                    Console.ResetColor();

                    //entradas
                    Console.Write("\nSelecionar Opção: ");
                    char opc = char.Parse(Console.ReadLine());

                    switch(opc){
                        case '1':
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Bem..., Só algunhas regras:\n");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("""
                            1 - para você excluir um livro esté deve ser propriedade sua!
                            2 - Após ser excluido, não será possivel recuperalo  
                            """);
                            Console.ResetColor();

                            //selecionar
                            Console.Write("\nNome do Livro: ");
                            string ex_livro = Console.ReadLine();

                            //Existe?
                            MySqlCommand existe_livro = new(){
                                CommandText = $"select count(*) from livro where nome_livro = '{ex_livro}';",
                                Connection = Connect
                            };
                            MySqlDataReader existe_livro_ofc = existe_livro.ExecuteReader();

                            //execução exite
                            existe_livro_ofc.Read();
                            int exitente = Convert.ToInt32(existe_livro_ofc[0]);
                            existe_livro_ofc.Close();

                            //Consulta existe autor
                            MySqlCommand com_autor = new(){
                                CommandText = $"select count(*) from livro inner join autor on livro.id_autor = autor.id_autor where nome_livro = '{ex_livro}' and nome_autor = '{Usuario}';",
                                Connection = Connect
                            };
                            MySqlDataReader com1_autor = com_autor.ExecuteReader();

                            //execução
                            com1_autor.Read();
                            int num000 = Convert.ToInt32(com1_autor[0]);
                            com1_autor.Close();

                            //commando
                            MySqlCommand remove_livro = new(){
                                CommandText = $"delete from livro where nome_livro = '{ex_livro}';",
                                Connection = Connect
                            };

                            if(num000 > 0 & exitente > 0){
                                Console.Clear();
                                Console.WriteLine($"Deseja mesmo remover: {ex_livro}?\n");

                                //opções
                                Console.Write("S para Sim, N para Não: ");
                                string conle = Console.ReadLine();
                                char conler = char.Parse(conle.ToUpper());

                                if(conler == 'S'){
                                    //delete
                                    remove_livro.ExecuteNonQuery();

                                    //alteração no auto_increment
                                    MySqlCommand consuta_id_livro = new(){
                                        CommandText = "select id_livro from livro order by id_livro desc;",
                                        Connection = Connect
                                    };
                                    MySqlDataReader Coon_id_livro = consuta_id_livro.ExecuteReader();
                                    Coon_id_livro.Read();
                                    int inclement_id_livro = Convert.ToInt32(Coon_id_livro[0]) + 1;
                                    Coon_id_livro.Close();

                                    //autor
                                    MySqlCommand auto_increment_autor = new(){
                                        CommandText = $"alter table autor auto_increment = {inclement_id_livro};",
                                        Connection = Connect
                                    };

                                    //livro
                                    MySqlCommand auto_increment_livro = new(){
                                        CommandText = $"alter table livro auto_increment = {inclement_id_livro};",
                                        Connection = Connect
                                    };

                                    auto_increment_autor.ExecuteNonQuery();
                                    auto_increment_livro.ExecuteNonQuery();

                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Você Removeu: {ex_livro}, com sucesso...!!");
                                    Console.ResetColor();
                                    Lib();
                                }
                                if(conler == 'N'){
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Operação Cancelada...");
                                    Console.ResetColor();
                                    Lib();
                                }
                                else{
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Opção invalida...!!!");
                                    Console.ResetColor();
                                    Lib();
                                }
                            }
                            if(exitente > 0 & num000 == 0){
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Você não é proprietario deste livro!");
                                Console.ResetColor();
                                Lib();
                            }
                            if(exitente == 0 & num000 == 0){
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("O livro que busca não exite em nosso banco de dados...");
                                Console.ResetColor();
                                Lib();
                            }
                        break;

                        case '2':
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("Desculpe... Ainda falta adicionar o sistema de Update. ( --)ﾉ(._.`)");
                            Console.ResetColor();
                            Lib();
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
                else{
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Té Avisei, Façã Login para continuar...!! ");
                    Console.ResetColor();
                    Lib();
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
