using SistemaNotas;
using System;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

Conexao db = new Conexao();

db.Conectar();

int opcaoMenu = 0;
do
{
    Console.Clear();
    Console.WriteLine("===BEM-VINDO AO SISTEMA ESCOLAR===");
    Console.WriteLine("1 - Administrador");
    Console.WriteLine("2 - Professor");
    Console.WriteLine("3 - Aluno");
    Console.WriteLine("4 - Sair");
    Console.WriteLine("===================================");
    Console.Write("Escolha uma opção: ");

    if (int.TryParse(Console.ReadLine(), out opcaoMenu))
    {
        switch (opcaoMenu)
        {
            case 1:
                MenuAdministrador(db);
                break;
            case 2:
                MenuProfessor(db);
                break;
            case 3:
                MenuAluno(db);
                break;
            case 4:
                Console.Clear();
                Console.WriteLine("Saindo do sistema...");
                db.Desconectar();
                break;
        }
    }
    else
    {
        Console.WriteLine("Entrada inválida. Por favor, insira um número.");
    }

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
} while (opcaoMenu != 4);


//=================================Menu Administrador==========================================
void MenuAdministrador(Conexao db)
{
    int opcaoAdm = 0;
    do
    {
        Console.Clear();
        Console.WriteLine("===ADMINISTRADOR===");
        Console.WriteLine("1 - Cadastrar aluno");
        Console.WriteLine("2 - Consultar aluno");
        Console.WriteLine("3 - Alterar dados do aluno");
        Console.WriteLine("4 - Excluir dados do aluno");
        Console.WriteLine("5 - Sair");
        Console.WriteLine("========================================");
        Console.Write("Escolha uma opção: ");

        if (int.TryParse(Console.ReadLine(), out opcaoAdm))
        {
            switch (opcaoAdm)
            {
                case 1:
                    CadastrarAluno(db);
                    break;
                case 2:
                    ConsultarAluno(db);
                    break;
                case 3:
                    AlterarDados(db);
                    break;
                case 4:
                    ExcluirDados(db);
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Saindo do sistema...");
                    db.Desconectar();
                    break;
            }
        }
        else
        {
            Console.WriteLine("Entrada inválida. Por favor, insira um número.");
        }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    } while (opcaoAdm != 5);
}
void CadastrarAluno(Conexao db)
{

    Console.Clear();
    Console.WriteLine("===CADASTRO DE ALUNO===");

    Console.Write("Digite o nome do aluno: "); string nome = Console.ReadLine();
    Console.Write("Digite a idade do aluno: "); int idade = int.Parse(Console.ReadLine());
    Console.WriteLine("Digite a data de nascimento do aluno (dd/mm/aaaa): "); string dataNascimento = (Console.ReadLine());
    Console.Write("Digite o CPF do aluno: "); string cpf = Console.ReadLine();
    Console.Write("Digite o CEP do aluno: "); string cep = Console.ReadLine();
    Console.Write("Digite o endereço do aluno: "); string endereco = Console.ReadLine();
    Console.Write("Digite o número do endereço: "); int numero = int.Parse(Console.ReadLine());
    Console.Write("Digite o bairro do aluno: "); string bairro = Console.ReadLine();
    Console.Write("Digite a cidade do aluno: "); string cidade = Console.ReadLine();
    Console.Write("Digite o estado do aluno: "); string estado = Console.ReadLine();
    Console.WriteLine("===CADASTRO DE NOTAS===");
    Console.Write("Digite a primeira nota do aluno: "); double nota1 = double.Parse(Console.ReadLine());
    Console.Write("Digite a segunda nota do aluno: "); double nota2 = double.Parse(Console.ReadLine());
    double media = (nota1 + nota2) / 2;

    try
    {
        string sql = @"
        INSERT INTO Alunos (Nome, Idade, DataNascimento, Cpf, Cep, Endereco, Numero, Bairro, Cidade, Estado, Nota1, Nota2, Media)
        VALUES (@Nome, @Idade, @DataNascimento, @Cpf, @Cep, @Endereco, @Numero, @Bairro, @Cidade, @Estado, @Nota1, @Nota2, @Media)";

        using (SqlCommand cmd = new(sql, db.conn))
        {
            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Idade", idade);
            cmd.Parameters.AddWithValue("@DataNascimento", dataNascimento);
            cmd.Parameters.AddWithValue("@Cpf", cpf);
            cmd.Parameters.AddWithValue("@Cep", cep);
            cmd.Parameters.AddWithValue("@Endereco", endereco);
            cmd.Parameters.AddWithValue("@Numero", numero);
            cmd.Parameters.AddWithValue("@Bairro", bairro);
            cmd.Parameters.AddWithValue("@Cidade", cidade);
            cmd.Parameters.AddWithValue("@Estado", estado);
            cmd.Parameters.AddWithValue("@Nota1", nota1);
            cmd.Parameters.AddWithValue("@Nota2", nota2);
            cmd.Parameters.AddWithValue("@Media", media);

            cmd.ExecuteNonQuery();
        }
    }

    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao cadastrar aluno: {ex.Message}");
    }
}
static void ConsultarAluno(Conexao db)
{
        Console.Clear();
        Console.WriteLine("===CONSULTA DE ALUNO===");
        Console.Write("Digite o nome do aluno: "); string nome = Console.ReadLine();

    string sql = "SELECT * FROM Alunos WHERE Nome = @Nome";
    using (SqlCommand cmd = new(sql, db.conn))
    {
        cmd.Parameters.AddWithValue("@Nome", nome);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}");
                Console.WriteLine($"Nome: {reader["Nome"]}");
                Console.WriteLine($"Idade: {reader["Idade"]}");
                Console.WriteLine($"Data de Nascimento: {reader["DataNascimento"]}");
                Console.WriteLine($"CPF: {reader["Cpf"]}");
                Console.WriteLine($"CEP: {reader["Cep"]}");
                Console.WriteLine($"Endereço: {reader["Endereco"]}, Nº {reader["Numero"]}");
                Console.WriteLine($"Bairro: {reader["Bairro"]}");
                Console.WriteLine($"Cidade: {reader["Cidade"]}");
                Console.WriteLine($"Estado: {reader["Estado"]}");
                Console.WriteLine($"Nota 1: {reader["Nota1"]}");
                Console.WriteLine($"Nota 2: {reader["Nota2"]}");
                Console.WriteLine($"Média: {reader["Media"]}");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
            }
        }
    }



}
void AlterarDados(Conexao db)
{
        Console.Clear();
        Console.WriteLine("===ALTERAÇÃO DE DADOS DO ALUNO===");
        Console.Write("Digite o nome do aluno: "); string nome = Console.ReadLine();

        string sqlSelect = "SELECT * FROM Alunos WHERE Nome = @Nome";
        SqlCommand cmdSelect = new SqlCommand(sqlSelect, db.conn);
    {

        cmdSelect.Parameters.AddWithValue("@Nome", nome);
        SqlDataReader dr = cmdSelect.ExecuteReader();

        
            
            Console.Write("Nova Idade: "); int novaIdade = int.Parse(Console.ReadLine());
            Console.Write("Nova Data Nascimento: "); string novaData = Console.ReadLine();
            Console.Write("Novo CPF: "); string novoCpf = Console.ReadLine();
            Console.Write("Novo CEP: "); string novoCep = Console.ReadLine();
            Console.Write("Novo Endereço: "); string novoEndereco = Console.ReadLine();
            Console.Write("Novo Número: "); string novoNumero = Console.ReadLine();
            Console.Write("Novo Bairro: "); string novoBairro = Console.ReadLine();
            Console.Write("Nova Cidade: "); string novaCidade = Console.ReadLine();
            Console.Write("Novo Estado: "); string novoEstado = Console.ReadLine();

        dr.Close();
        try
        {

            string sqlUpdate = @"UPDATE Alunos SET Idade=@Idade, DataNascimento=@Data,
                                      Cpf=@Cpf, Cep=@Cep, Endereco=@Endereco, Numero=@Numero,
                                      Bairro=@Bairro, Cidade=@Cidade, Estado=@Estado
                                      WHERE Nome=@Nome";

            SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, db.conn);

            cmdUpdate.Parameters.AddWithValue("@Nome", nome);
            cmdUpdate.Parameters.AddWithValue("@Idade", novaIdade);
            cmdUpdate.Parameters.AddWithValue("@Data", novaData);
            cmdUpdate.Parameters.AddWithValue("@Cpf", novoCpf);
            cmdUpdate.Parameters.AddWithValue("@Cep", novoCep);
            cmdUpdate.Parameters.AddWithValue("@Endereco", novoEndereco);
            cmdUpdate.Parameters.AddWithValue("@Numero", novoNumero);
            cmdUpdate.Parameters.AddWithValue("@Bairro", novoBairro);
            cmdUpdate.Parameters.AddWithValue("@Cidade", novaCidade);
            cmdUpdate.Parameters.AddWithValue("@Estado", novoEstado);
            cmdUpdate.ExecuteNonQuery();

            Console.WriteLine("Aluno atualizado!");
        }
        catch (Exception)
        {
           Console.WriteLine("Falha ao alterar dados do aluno :(");
        }
          
    }


}
void ExcluirDados(Conexao db)
{
        Console.Clear();
        Console.WriteLine("===EXCLUSÃO DE DADOS DO ALUNO===");
        Console.Write("Digite o nome do aluno: "); string nome = Console.ReadLine();

        string sql = "DELETE FROM Alunos WHERE Nome = @Nome";
        SqlCommand cmd = new SqlCommand(sql, db.conn);
        cmd.Parameters.AddWithValue("@Nome", nome);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Aluno excluído!");
}

//==================================Menu Professor==============================================
void MenuProfessor(Conexao db)
{
    int opcaoProfessor = 0;
    do
    {
        Console.Clear();
        Console.WriteLine("===========PROFESSOR===============");
        Console.WriteLine("1 - Cadastrar Notas");
        Console.WriteLine("2 - Sair");
        Console.WriteLine("===================================");
        Console.Write("Escolha uma opção: ");

        if (int.TryParse(Console.ReadLine(), out opcaoProfessor))
        {
            switch (opcaoProfessor)
            {
                case 1:
                    CadastrarNotas(db);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Saindo do sistema...");
                    db.Desconectar();
                    break;

            }
        }
        else
        {
            Console.WriteLine("Entrada inválida. Por favor, insira um número.");
        }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    } while (opcaoProfessor != 2);
}
void CadastrarNotas(Conexao db)
{
    Console.Clear();
    Console.WriteLine("===CADASTRO DE NOTAS===");
    Console.Write("Digite o nome do aluno: "); string nome = Console.ReadLine();
    Console.Write("Digite a primeira nota do aluno: "); double nota1 = double.Parse(Console.ReadLine());
    Console.Write("Digite a segunda nota do aluno: "); double nota2 = double.Parse(Console.ReadLine());
    double media = (nota1 + nota2) / 2;
    try
    {
        string sql = @"
        UPDATE Alunos 
        SET Nota1 = @Nota1, Nota2 = @Nota2, Media = @Media 
        WHERE Nome = @Nome";
        using (SqlCommand cmd = new(sql, db.conn))
        {
            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Nota1", nota1);
            cmd.Parameters.AddWithValue("@Nota2", nota2);
            cmd.Parameters.AddWithValue("@Media", media);
            cmd.ExecuteNonQuery();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao cadastrar notas: {ex.Message}");
    }
}
////==================================Menu Aluno==============================================
void MenuAluno(Conexao db)
{
    int opcaoAluno = 0;
    do
    {
        Console.Clear();
        Console.WriteLine("=============ALUNO=================");
        Console.WriteLine("1 - Consultar Notas");
        Console.WriteLine("2 - Sair");
        Console.WriteLine("===================================");
        Console.Write("Escolha uma opção: ");

        if (int.TryParse(Console.ReadLine(), out opcaoAluno))
        {
            switch (opcaoAluno)
            {
                case 1:
                    ConsultarNotas(db);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Saindo do sistema...");
                    db.Desconectar();
                    break;

            }
        }
        else
        {
            Console.WriteLine("Entrada inválida. Por favor, insira um número.");
        }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    } while (opcaoAluno != 2);
}
void ConsultarNotas(Conexao db)
{
    Console.Clear();
    Console.WriteLine("===CONSULTA DE NOTAS===");
    Console.Write("Digite o nome do aluno: ");
    string nome = Console.ReadLine();

    try
    {
        string sql = "SELECT Nota1, Nota2, Media FROM Alunos WHERE Nome = @Nome";
        using (SqlCommand cmd = new(sql, db.conn))
        {
            cmd.Parameters.AddWithValue("@Nome", nome);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine($"Nota 1: {reader["Nota1"]}");
                    Console.WriteLine($"Nota 2: {reader["Nota2"]}");
                    Console.WriteLine($"Média: {reader["Media"]}");
                }
                else
                {
                    Console.WriteLine("Aluno não encontrado.");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao consultar notas: {ex.Message}");
    }
}



