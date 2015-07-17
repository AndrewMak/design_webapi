
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.Models.Entities
{
    public class ToDo : ModelBase
    {
        public Guid ToDoId { get; set; }

        [MinLength(3, ErrorMessage = "A descrição da tarefa não pode ser menor que {0}")]
        [MaxLength(250, ErrorMessage = "A descrição da tarefa não pode ser maior que {0}")]
        [Required(ErrorMessage = "Descrição da tarefa é  obrigatória")]
        public string Description { get; set; }

        public bool Completed { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public async Task<ICollection<ToDo>> Get()
        {
            var doingsReturn = new List<ToDo>();
            using (var dbContext = new Context.ToDoContext())
            {
                var doings = await dbContext.ToDo.Where(n => !n.Completed && n.Active).ToListAsync();
                foreach (var todo in doings)
                {
                    doingsReturn.Add(new ToDo
                    {
                        ToDoId = todo.ToDoId,
                        Description = todo.Description,
                        Completed = todo.Completed,
                        UserId = todo.UserId
                    });
                }
                return await Task.Run(() => doingsReturn);
            }
        }

        public async Task<ToDo> Get(Guid todoId, Guid userId)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                var todo = await dbContext.ToDo.FirstOrDefaultAsync(n => n.ToDoId.Equals(todoId) && !n.Completed && n.UserId.Equals(userId) && n.Active) ?? new ToDo();
                var todoReturn = new ToDo
                    {
                        ToDoId = todo.ToDoId,
                        Description = todo.Description,
                        Completed = todo.Completed,
                        UserId = todo.UserId
                    };
                return await Task.Run(() => todoReturn);
            }
        }

        public async Task<ICollection<ToDo>> Get(Guid userId)
        {
            var doingsReturn = new List<ToDo>();
            using (var dbContext = new Context.ToDoContext())
            {
                var doings = await dbContext.ToDo.Include(n => n.User).Where(n => n.User.UserId.Equals(userId) && !n.Completed && n.Active).ToListAsync();
                foreach (var todo in doings)
                {
                    doingsReturn.Add(new ToDo
                    {
                        ToDoId = todo.ToDoId,
                        Description = todo.Description,
                        Completed = todo.Completed,
                        UserId = todo.UserId
                    });
                }
                return await Task.Run(() => doingsReturn);
            }
        }

        public async Task<ToDo> Post(Guid userId, ToDo todo)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                dbContext.ToDo.Add(new Entities.ToDo
                {
                    ToDoId = todo.ToDoId = Guid.NewGuid(),
                    Description = todo.Description,
                    Completed = false,
                    Active = false,
                    UserId = todo.UserId = userId,
                    CreatedAt = DateTime.Now,
                    UpDatedAt = DateTime.Now
                });

                await dbContext.SaveChangesAsync();
                return await Task.Run(() => todo);
            }
        }

        public async Task<ToDo> Put(Guid todoId, ToDo todo)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                var todoModel = await dbContext.ToDo.FirstOrDefaultAsync(n => n.ToDoId.Equals(todoId));
                todoModel.Description = todo.Description;
                todoModel.Completed = todo.Completed;
                todoModel.UpDatedAt = DateTime.Now;
                await dbContext.SaveChangesAsync();
                todo.Completed = todoModel.Completed;
                todo.ToDoId = todoModel.ToDoId;
                todo.UserId = todoModel.UserId;
                return await Task.Run(() => todo);
            }
        }

        public async Task Delete(Guid todoId)
        {
            using (var dbContext = new Context.ToDoContext())
            {
                var todo = await dbContext.ToDo.FirstOrDefaultAsync(n => n.ToDoId.Equals(todoId));
                todo.Active = false;
                todo.UpDatedAt = DateTime.Now;
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
