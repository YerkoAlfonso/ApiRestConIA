using IADatabase.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Text;

namespace IADatabase.Utils
{
    public class DatabaseSchemaService
    {

        private readonly AppDbContext _context;

        public DatabaseSchemaService(AppDbContext context)
        {
            _context = context;
        }
        public string GetShema()
        {
            string connectionString = "Server=DESKTOP-CKLDG0I\\SQLEXPRESS;Database =Tienda;Trusted_Connection=true;TrustServerCertificate=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Connect to the database then retrieve the schema information.  
                connection.Open();
                DataTable table = connection.GetSchema("Tables");
                PrintTableAndColumnNames();
                string algo = table.ToString();
            }

            return "";
        }

        private static void DisplayData(System.Data.DataTable table)
        {
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }
        }

        public string PrintTableAndColumnNames()
        {
            var model = _context.Model;
            StringBuilder ModelString = new StringBuilder();
            foreach (var entityType in model.GetEntityTypes())
            {
                // Nombre de la tabla
                var tableName = entityType.GetTableName();
                ModelString.AppendLine($"Tabla: {tableName}");
                //Console.WriteLine($"Tabla: {tableName}");

                // Nombres de las columnas
                foreach (var property in entityType.GetProperties())
                {
                    var columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName, null));
                    var typeComun = property.GetColumnType(StoreObjectIdentifier.Table(tableName, null));
                    ModelString.AppendLine($"    Columna: {columnName}  Tipo: {typeComun}");
                   // Console.WriteLine($"    Columna: {columnName}  Tipo: {typeComun}");
                }
            }

            Console.WriteLine(ModelString);
            return ModelString.ToString();
        }


    }
}
