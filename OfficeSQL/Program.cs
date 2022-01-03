using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OfficeSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Worker> workersList = new List<Worker>();

            string connetionString = "Data Source=DESKTOP-76KPC67;Initial Catalog=OfficeDB;Integrated Security=True;Pooling=False";
            SqlConnection cnn = new SqlConnection(connetionString);

            GetAllDetails(cnn);



            AddNewWorker(cnn);
        }

        static void GetAllDetails(SqlConnection connection)
        {
            try
            {
                connection.Open();
                string sqlCommand = "SELECT * FROM Workers";
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(3) + " - " + dataReader.GetValue(4));
                }
                connection.Close();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception)
            {
                Console.WriteLine("General Error");
            }
        }

        static void AddNewWorker(SqlConnection connection1)
        {
            try
            {
                connection1.Open();
                Worker newWorker = new Worker();

                Console.Write("Enter worker full name : ");
                newWorker.FullName = Console.ReadLine();
                Console.Write("Enter worker birth date : ");
                newWorker.BirthDate = int.Parse(Console.ReadLine());
                Console.Write("Enter worker email : ");
                newWorker.Email = Console.ReadLine();
                Console.Write("Enter worker salary : ");
                newWorker.Salary = int.Parse(Console.ReadLine());

                string sqlCommand = $"INSERT INTO Workers(FullName, BirthDate, Email, Salary) VALUES ('{newWorker.FullName}',  {newWorker.BirthDate}, '{newWorker.Email}', {newWorker.Salary})";
                SqlCommand command = new SqlCommand(sqlCommand, connection1);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.InsertCommand = new SqlCommand(sqlCommand, connection1);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                connection1.Close();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception)
            {
                Console.WriteLine("General Error");
            }
        }

    }
}
