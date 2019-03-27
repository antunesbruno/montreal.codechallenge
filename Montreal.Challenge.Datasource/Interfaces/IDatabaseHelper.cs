using System;
using System.Collections.Generic;
using System.Text;

namespace Montreal.Challenge.Datasource.Interfaces
{
    public interface IDatabaseHelper
    {
        void CreateDatabase(string absolutePath);
        void CreateTables();
    }
}
