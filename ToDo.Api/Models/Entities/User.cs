
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ToDo.Api.Models.Entities
{
    public class User : ModelBase
    {
        public Guid UserId { get; set; }

        [MaxLength(250, ErrorMessage = "O nome não pode ter mais que 250 caracteres")]
        [MinLength(3, ErrorMessage = "O nome não pode ter menos que 3 caracteres")]
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "O E-mail não pode ter mais que 250 caracteres")]
        [MinLength(3, ErrorMessage = "O E-mail não pode ter menos que 3 caracteres")]
        [Required(ErrorMessage = "O E-mail do usuário é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um endereço de E-mail válido")]

        public string Email { get; set; }

        [IgnoreDataMember]
        public virtual IList<ToDo> Doings { get; set; }

        public User()
        {
            Doings = new List<ToDo>();
        }

        public async Task<ICollection<User>> Get()
        {
            var usersReturn = new List<User>();
            using (var dbContext = new Context.ToDoContext())
            {
                var users = await dbContext.User.Where(n => n.Active).ToListAsync();
                foreach (var user in users)
                {
                    usersReturn.Add(new User
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Email = user.Email,
                    });
                }
                return await Task.Run(() => usersReturn);
            }
        }

        public async Task<User> Get(Guid userId)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                var user = await dbContext.User.FirstOrDefaultAsync(n => n.UserId.Equals(userId) && n.Active) ?? new User();
                var userReturn = new User
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                };
                return await Task.Run(() => userReturn);
            }
        }

        public async Task<User> Post(User user)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                user.UserId = Guid.NewGuid();
                user.Active = true;
                user.CreatedAt = DateTime.Now;
                user.UpDatedAt = DateTime.Now;
                dbContext.User.Add(user);
                await dbContext.SaveChangesAsync();
                return await Task.Run(() => user);
            }
        }

        public async Task<User> Put(Guid userId, User user)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                var userModel = await dbContext.User.FirstOrDefaultAsync(n => n.UserId.Equals(userId));
                userModel.Name = user.Name;
                userModel.Email = user.Email;
                userModel.UpDatedAt = DateTime.Now;
                await dbContext.SaveChangesAsync();
                user.UserId = userModel.UserId;
                return await Task.Run(() => user);
            }
        }

        public async Task Delete(Guid userId)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                var userModel = await dbContext.User.FirstOrDefaultAsync(n => n.UserId.Equals(userId));
                userModel.Active = false;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
