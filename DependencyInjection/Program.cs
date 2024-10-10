using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DependencyInjection
{

    public class UserInterface
    {
        private string _userName;
        private string _password;

        private IBusiness _business;

        public UserInterface(IBusiness business)
        {
            _business = business;
        }
        private void GetData()
        {
            Console.Write("Enter user name:");
            _userName = Console.ReadLine();

            Console.Write("Enter password:");
            _password = Console.ReadLine();
        }

        public void Signup()
        {
            GetData();

            //var biz = new Business();
            _business.SignUp(_userName, _password);
        }
    }

    public interface IBusiness
    {
        void SignUp(string userName, string password);
    }
    public class Business : IBusiness
    {
        private IDataAccess _dataAccess;

        public Business(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public void SignUp(string userName, string password)
        {
            //var da = new DataAccessMySQL();
            _dataAccess.SignUp(userName, password);
        }
    }

    public interface IDataAccess
    {
        void SignUp(string userName, string password);
    }
    public class DataAccessSqlServer : IDataAccess
    {
        public void SignUp(string userName, string password)
        {
            // use EF to write data into Sql Server   
        }
    }

    public class DataAccessMySQL : IDataAccess
    {
        public void SignUp(string userName, string password)
        {
            // use EF to write data into My SQL  
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection collection = new ServiceCollection();// holding the metadata and mapping of types
            collection.AddScoped<IDataAccess, DataAccessMySQL>();//same even for all threads rather than Transient
            collection.AddScoped<IBusiness, Business>();
            collection.AddScoped<UserInterface>();

            IServiceProvider provider = collection.BuildServiceProvider();// creating intances and managing lifetime of the instances

            UserInterface ui = provider.GetService<UserInterface>();
            ui.Signup();
        }
    }
}
