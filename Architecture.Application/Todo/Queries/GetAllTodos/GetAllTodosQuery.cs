﻿namespace Architecture.Application.Todo.Queries.GetAllTodos
{
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;
    using Architecture.Domain.Todo;
    using Architecture.Infrastructure.Todo;
    using LanguageExt;
    using MediatR;

    public class GetAllTodosQuery : IRequest<Either<TodoFailure, Seq<TodoItemModel>>> { }

    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, Either<TodoFailure, Seq<TodoItemModel>>>
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public GetAllTodosQueryHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public Task<Either<TodoFailure, Seq<TodoItemModel>>> Handle(GetAllTodosQuery request, CancellationToken token) =>
            Fetch()
                .MapT(Project);

        private async Task<Either<TodoFailure, Seq<TodoItem>>> Fetch() =>
            await _todoItemRepository.GetAllAsync().ToEither();

        private Seq<TodoItemModel> Project(Seq<TodoItem> items) =>
            items.Select(x => new TodoItemModel(x.Id.Value, x.Content.Value, x.IsDone.Value));
    }
}