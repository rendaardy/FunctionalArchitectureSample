﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.Domain.Todo;
using LanguageExt;
using Microsoft.Extensions.Caching.Distributed;

namespace Architecture.DataSource.MongoDb.Todo
{
    public class TodoItemDataSource : ITodoItemDataSource
    {
        private const string CacheKey = "TodoItemCacheKey";
        private readonly IDistributedCache _cache;

        public TodoItemDataSource(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Either<TodoFailure, IEnumerable<TodoItem>>> GetAll(CancellationToken token)
        {
            return Try(async () => await _cache.GetAsync(CacheKey, token))
                .ToEither()
                .Bind(encodedItems => Encoding.UTF8.GetString(encodedItems));
        }

        private Try<T> Try<T>(Func<T> fun)
        {
            return new Try<T>(fun);
        }

        private async TryAsync<T> Try<T>(Func<Task<T>> fun)
        {
            return new TryAsync<T>(fun);
        }

        public async Task<Either<TodoFailure, TodoItem>> Get(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<Either<TodoFailure, Guid>> Add(TodoItem todoItem, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<Either<TodoFailure, Unit>> Update(TodoItem todoItem, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}