using IADatabase.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System.Text;

namespace IADatabase.Utils
{
    public class IA
    {


        string apiKey = "";

        public async Task<string> GeneraConsultaAsync(String pregunta, AppDbContext _context)
        {
            var openIaService = new OpenAIService(new OpenAiOptions() { ApiKey = apiKey });

            DatabaseSchemaService database = new DatabaseSchemaService(_context);
            string SquemaString = database.PrintTableAndColumnNames();
            string preguntaSysmte = "Eres un administrador de base de datos";
            String preguntaFinal =  "Segun el siguiente essquema de base de datos: " + SquemaString + "  generame la consulta SQl " + "Para la siguiente pregunta "+ pregunta +
                " escribe la respuesta en formato json con el siguiente formato { 'sql_query' : } donde la consulta sql este dentro del objeto sql_query";
            var completionResult = await openIaService.ChatCompletion.CreateCompletion(
                new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromUser(preguntaFinal),
                        ChatMessage.FromSystem(preguntaSysmte)
                    },
                    Model = OpenAI.ObjectModels.Models.Gpt_4
                });

            string MesssageResponse = "";
            if (completionResult.Successful)
            {
                var response = completionResult.Choices.First().Message.Content;
                Console.WriteLine(response);
                MesssageResponse = response;
            }

            SqlResponse sqlResponse  = JsonConvert.DeserializeObject<SqlResponse>(MesssageResponse);

            string finalQuery = sqlResponse.sql_query;

            Console.WriteLine(finalQuery);



            StringBuilder respuesta = new StringBuilder();
            string connecString = @"";
            using (SqlConnection connection = new SqlConnection(connecString))
            {

                
                SqlCommand command = new SqlCommand(finalQuery,connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++) // Itera sobre todas las columnas
                        {
                            Console.Write(reader[i].ToString() + " "); // Imprime cada valor de la columna
                            respuesta.AppendLine(reader[i].ToString() + " "); 
                        }
                        Console.WriteLine(); // Nueva línea después de cada fila
                    }
                }



            }

            return respuesta.ToString();
        }

        public class SqlResponse()
        {
            public string sql_query { get; set; }
        }
    }
}
