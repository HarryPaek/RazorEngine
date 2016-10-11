using ConsoleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.ViewModel
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
            RelatedUsers = new List<UserModel>();
        }

        public UserModel Receiver { get; set; }
        public IList<UserModel> RelatedUsers { get; private set; }
    }
}
