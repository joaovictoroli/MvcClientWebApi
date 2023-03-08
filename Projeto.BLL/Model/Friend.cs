using Projeto.BLL.Model.Common;

namespace Projeto.BLL.Model
{
    public class Friend : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
