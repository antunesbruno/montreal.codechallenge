using Montreal.Challenge.Datasource.Interfaces;
using Montreal.Challenge.Datasource.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Montreal.Challenge.Datasource.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string DATABASE_FOLDER = "montreal";
        private readonly string DATABASE_NAME = "montreal-codechallenge.db3";

        public void CreateDatabase()
        {
            //create database
            PlatformDatabase.CreateDatabase(DATABASE_FOLDER, DATABASE_NAME);
        }

        public void CreateTables()
        {
            //If has need to use database, configure the tables that need to be created here
            //following the exemple, where XXX is the entity

            //PlatformDatabase.CreateTable<XXX>();
        }
    }
}
