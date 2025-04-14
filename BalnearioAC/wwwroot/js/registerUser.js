import { apiRequest } from "./script.js";


async function registerUser(event)
{

    event.preventDefault();

    try {
        const name = document.getElementById('name').value;
        const cpf = document.getElementById('cpf').value;
        const email = document.getElementById('email').value;
        const phone = document.getElementById('telefone').value;
        const age = document.getElementById('age').value;
        const Passwd = document.getElementById('senha').value;

        if (!name || !cpf || !email || !phone || !age || !Passwd) {
            alert('Preencha todos os campos');
            return;
        }
        else{

            const response = apiRequest('http://localhost:5237/user', 'POST', { name, cpf, email, phone, age, Passwd, id_user_type: 3 });
            console.log(response);
            
        }

    } catch (error) {
        console.log(error);
        
    }

}

document.getElementById('register-form').addEventListener('submit', registerUser);

// arrumar o timestamp


// Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
//  ---> System.ArgumentException: Cannot write DateTime with Kind=Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported. Note that it's not possible to mix DateTimes with different Kinds in an array, range, or multirange. (Parameter 'value')
//    at Npgsql.Internal.Converters.DateTimeConverterResolver`1.Get(DateTime value, Nullable`1 expectedPgTypeId, Boolean validateOnly)
//    at Npgsql.Internal.Converters.DateTimeConverterResolver.<>c.<CreateResolver>b__0_0(DateTimeConverterResolver`1 resolver, DateTime value, Nullable`1 expectedPgTypeId)
//    at Npgsql.Internal.Converters.DateTimeConverterResolver`1.Get(T value, Nullable`1 expectedPgTypeId)
//    at Npgsql.Internal.PgConverterResolver`1.GetAsObjectInternal(PgTypeInfo typeInfo, Object value, Nullable`1 expectedPgTypeId)
//    at Npgsql.Internal.PgResolverTypeInfo.GetResolutionAsObject(Object value, Nullable`1 expectedPgTypeId)
//    at Npgsql.Internal.PgTypeInfo.GetObjectResolution(Object value)
//    at Npgsql.NpgsqlParameter.ResolveConverter(PgTypeInfo typeInfo)
//    at Npgsql.NpgsqlParameter.ResolveTypeInfo(PgSerializerOptions options)
//    at Npgsql.NpgsqlParameterCollection.ProcessParameters(PgSerializerOptions options, Boolean validateValues, CommandType commandType)
//    at Npgsql.NpgsqlCommand.ExecuteReader(Boolean async, CommandBehavior behavior, CancellationToken cancellationToken)
//    at Npgsql.NpgsqlCommand.ExecuteReader(Boolean async, CommandBehavior behavior, CancellationToken cancellationToken)
//    at Npgsql.NpgsqlCommand.ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
//    --- End of inner exception stack trace ---
//    at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.Storage.RelationalDatabase.SaveChangesAsync(IList`1 entries, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(IList`1 entriesToSave, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(StateManager stateManager, Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
//    at Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.NpgsqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
//    at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
//    at BalnearioAC.Controllers.UserController.PostUser(User user) in C:\Users\eskatista\Desktop\BalnearioAC\BalnearioAC\Controllers\UserController.cs:line 38
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.TaskOfIActionResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResourceFilter>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResourceExecutedContextSealed context)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
//    at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
//    at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
//    at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
//    at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
//    at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

// HEADERS
// =======
// Accept: */*
// Connection: keep-alive
// Host: localhost:5237
// User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36 OPR/117.0.0.0
// Accept-Encoding: gzip, deflate, br, zstd
// Accept-Language: pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7
// Content-Type: application/json
// Origin: http://localhost:5237
// Referer: http://localhost:5237/registerUser.html?
// Content-Length: 164
// sec-ch-ua-platform: "Windows"
// sec-ch-ua: "Not A(Brand";v="8", "Chromium";v="132", "Opera GX";v="117"
// sec-ch-ua-mobile: ?0
// Sec-Fetch-Site: same-origin
// Sec-Fetch-Mode: cors
// Sec-Fetch-Dest: empty