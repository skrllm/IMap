using System;
using IMAP.Model;
using System.IO;

namespace IMAP.Database
{
    public class Data
    {
        private static AsyncRepository database;

        public static AsyncRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new AsyncRepository(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME)); //DB path
                }

                return database;
            }
        }
    }
}
