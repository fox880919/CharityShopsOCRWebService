using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CharityShopsOCRWebService.Models
{
    public class BooksDbContext : DbContext
    {


        public BooksDbContext() : base("name=MyFirstBooksDBContext") {

            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<TranscriptContextDB,
            //TranscriptAPIs.Migrations.Configuration>("MyConTranscriptsDBtextDB"));
        }

        public DbSet<BookTable> books { get; set; }

        public DbSet<User> Users { get; set; }

        public static bool doesBookExist(string isbn)
        {

            using (var db = new BooksDbContext())
            {
                BookTable book = db.books.FirstOrDefault(b => b.Isbn.Equals(isbn));

                if (book != null)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }
        }



        //<add name = "MyFirstBooksDBContext" connectionString="Data Source = .\SQLEXPRESS; Initial Catalog = BooksDB; Integrated Security = true" providerName="System.Data.SqlClient"/>

        //            <add name="MyConTranscriptsDBtextDB" connectionString="Data Source = .\SQLEXPRESS; Initial Catalog = ITSDB; Integrated Security = true" providerName="System.Data.SqlClient"/>

        /* <add name="MyConTranscriptsDBtextDB"
 connectionString="Data Source=tcp:transcriptdb2server.database.windows.net,1433;Initial Catalog=TranscriptDB2;User ID=fox880919@transcriptdb2server;Password=F@yez880919;Trusted_Connection=False;Encrypt=True;PersistSecurityInfo=True"
 providerName="System.Data.SqlClient" /> */

    }
}