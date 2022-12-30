using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using testando_api_crud.Models;
using static System.Net.Mime.MediaTypeNames;

namespace testando_api_crud.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{

    [HttpPost]
    public void Cadastra([FromBody] Produto produto)
    {
        var connex = "Server=localhost;Database=PRODUTOS;Uid=root;Pwd=sung87ju;";
        using (var conn = new MySqlConnection(connex))
        {
            conn.Open();
            var query = $"insert into produtos (Name,descricao,valor) values " +
                $"('{produto.nome}','{produto.descricao}','{produto.valor}')";
            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }

    [HttpDelete]
    public void Deleta([FromBody]int id)
    {
        var connex = "Server=localhost;Database=PRODUTOS;Uid=root;Pwd=sung87ju;";
        using (var conn = new MySqlConnection(connex))
        {
            conn.Open();
            var query = $"delete from produtos where id = {id}";
            var command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }

    [HttpGet]
    public List<Produto> testeGet()
    {
        var produtoLista = new List<Produto>();
        var connex = "Server=localhost;Database=PRODUTOS;Uid=root;Pwd=sung87ju;";

        using (var conn = new MySqlConnection(connex))
        {
            conn.Open();
            var query = "select * from produtos";
            using (var command = new MySqlCommand(query, conn))
            {
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    produtoLista.Add(new Produto
                    {
                        id = Int32.Parse(dataReader["id"].ToString()),
                        nome = dataReader["Name"].ToString(),
                        valor = Int32.Parse(dataReader["valor"].ToString()),
                        descricao = dataReader["descricao"].ToString()
                    }); ;
                }

            };
            conn.Close();
        }
        return produtoLista;
    }
}
