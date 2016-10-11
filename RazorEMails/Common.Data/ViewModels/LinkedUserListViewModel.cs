using Common.Data.Models;
using System.Collections.Generic;

namespace Common.Data.ViewModels
{
    public class LinkedUserListViewModel
    {
        public LinkedUserListViewModel()
        {
            RelatedUsers = new List<LinkedUserModel>();
        }

        public LinkedUserModel Receiver { get; set; }
        public IList<LinkedUserModel> RelatedUsers { get; private set; }
    }
}
