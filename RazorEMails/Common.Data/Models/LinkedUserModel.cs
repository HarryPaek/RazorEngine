using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common.Data.Models
{
    public class LinkedUserModel
    {
        public LinkedUserModel()
        {
            LinkedUsers = new List<LinkedUserModel>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsPremiumUser { get; set; }

        public IList<LinkedUserModel> LinkedUsers { get; private set; }

        public string GetIsPremiumUserText()
        {
            return IsPremiumUser.ToString();
        }

        public string GetLinkedUsersAsText()
        {
            string relatedUsersAsText = string.Empty;

            switch (LinkedUsers.Count)
            {
                case 0:
                    break;

                case 1:
                    relatedUsersAsText = string.Format("{0}({1})", LinkedUsers[0].Name, LinkedUsers[0].Email);
                    break;

                default:
                    relatedUsersAsText = GetUserListAsText();
                    break;
            }

            return relatedUsersAsText;
        }

        private string GetUserListAsText()
        {
            return string.Join(", ", LinkedUsers.Select(user => string.Format("{0}({1})", user.Name, user.Email)));

            //StringBuilder builder = new StringBuilder();

            //string.Join(", ", LinkedUsers.Select(user => string.Format("{0}({1})", user.Name, user.Email)));

            //foreach (LinkedUserModel user in LinkedUsers)
            //{
            //    builder.AppendFormat(", {0}({1})", user.Name, user.Email);
            //}

            //return builder.ToString().Substring(2);
        }
    }
}
